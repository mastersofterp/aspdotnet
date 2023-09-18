using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Course_Registration_Approval : System.Web.UI.Page
{
    //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Populate the DropDownList 
                PopulateDropDownList();
            }
            Session["reportdate"] = null;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Course_Registration_Approval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Course_Registration_Approval.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        string clg_Nos = objCommon.LookUp("USER_ACC", "DISTINCT ISNULL(UA_COLLEGE_NOS,0)AS UA_COLLEGE_NOS", "UA_TYPE=" + Session["usertype"].ToString() + " AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND UA_NO=" + Session["userno"].ToString());

        if (Session["usertype"].ToString() != "1")
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH D INNER JOIN ACD_COLLEGE_MASTER C ON C.COLLEGE_ID=D.COLLEGE_ID AND C.ORGANIZATIONID=D.ORGANIZATIONID ", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID IN(" + clg_Nos + ") AND C.ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND D.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "C.COLLEGE_ID"); //OR (" + Session["userdeptno"].ToString() + ")
        else
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + clg_Nos + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", " COLLEGE_ID IN (" + ddlCollege.SelectedValue + ") AND isnull(FLOCK,0)=1 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "DEGREENO");
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
        lvStudentCore.DataSource = null;
        lvStudentCore.DataBind();
        lvStudentElect.DataSource = null;
        lvStudentElect.DataBind();
        lvStudentGlobal.DataSource = null;
        lvStudentGlobal.DataBind();
        divCore.Visible = false;
        divElect.Visible = false;
        divGlobal.Visible = false;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            lvApproveCourse.DataSource = null;
            lvApproveCourse.DataBind();
            lvApproveCourse.Visible = false;
            lvStudentCore.DataSource = null;
            lvStudentCore.DataBind();
            lvStudentElect.DataSource = null;
            lvStudentElect.DataBind();
            lvStudentGlobal.DataSource = null;
            lvStudentGlobal.DataBind();
            divCore.Visible = false;
            divElect.Visible = false;
            divGlobal.Visible = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH AD ON ( B.BRANCHNO = AD.BRANCHNO )", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND AD.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND AD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));

            }
            else
            {
                objCommon.DisplayMessage(uplReg, "Please select college/school", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int userno = 0;
            DataSet ds2 = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE", "*", "", "", "GLOBAL_OFFER_ID");
            CourseController objCC = new CourseController();
            if (Session["userno"].ToString() != string.Empty)
                userno = int.Parse(Session["userno"].ToString());
            else
                Response.Redirect("~/default.aspx", false);
            CustomStatus cs = CustomStatus.Error;

            bool cbChecked = false;
            foreach (ListViewDataItem dataitem in lvApproveCourse.Items)
            {
                CheckBox cbApprove = (CheckBox)dataitem.FindControl("cbApprove");
                if (cbApprove.Checked)
                {
                    cbChecked = true;
                    break;
                }
            }

            if (!cbChecked)
            {
                objCommon.DisplayMessage(uplReg, "Please Select at least One Student.", this.Page);
                return;
            }


            foreach (ListViewDataItem dataitem in lvApproveCourse.Items)
            {
                CheckBox cbApprove = (CheckBox)dataitem.FindControl("cbApprove");
                if (cbApprove.Checked)
                {
                    Label IDNO = (Label)dataitem.FindControl("lblIDNO");
                    Label StudentName = (Label)dataitem.FindControl("lblStudentName");
                    Student objS = new Student();

                    objS.Uano = userno;
                    objS.IdNo = Convert.ToInt32(IDNO.ToolTip); //Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);                       
                    objS.RegNo = IDNO.Text;
                    objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    objS.College_ID = Convert.ToInt32(ddlCollege.SelectedValue);
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];
                    cs = (CustomStatus)objCC.UpdateCourseRegApproval(objS, ipAddress);
                }
            }

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                btnShow.Enabled = true;
                //btnSubmit.Enabled = false;
                objCommon.DisplayMessage(uplReg, " Courses Approved successfully.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            int clgID = Convert.ToInt32(ddlCollege.SelectedValue);
            int sessionID = Convert.ToInt32(ddlSession.SelectedValue);
            int degreeID = Convert.ToInt32(ddlDegree.SelectedValue);
            int branchID = Convert.ToInt32(ddlBranch.SelectedValue);
            //int studtype = 0;
            //if (rdoCourseRegDone.Checked == true)
            //{
            //    studtype = 1;
            //}
            //else if (rdoCoursePending.Checked == true)
            //{
            //    studtype = 2;
            //}
            CourseController objC = new CourseController();
            DataSet ds = objC.GetCourseRegistrationApprvlListModified(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlFilter.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvApproveCourse.DataSource = ds;
                lvApproveCourse.DataBind();
                if (ddlFilter.SelectedValue == "1" || ddlFilter.SelectedValue == "4")
                {
                    lvStudentCore.DataSource = ds;
                    lvStudentCore.DataBind();
                    lvStudentElect.DataSource = null;
                    lvStudentElect.DataBind();
                    lvStudentGlobal.DataSource = null;
                    lvStudentGlobal.DataBind();
                    divCore.Visible = true;
                    divElect.Visible = false;
                    divGlobal.Visible = false;
                }
                else if (ddlFilter.SelectedValue == "2" || ddlFilter.SelectedValue == "5")
                {
                    lvStudentCore.DataSource = null;
                    lvStudentCore.DataBind();
                    lvStudentElect.DataSource = ds;
                    lvStudentElect.DataBind();
                    lvStudentGlobal.DataSource = null;
                    lvStudentGlobal.DataBind();
                    divCore.Visible = false;
                    divElect.Visible = true;
                    divGlobal.Visible = false;
                }
                else if (ddlFilter.SelectedValue == "3" || ddlFilter.SelectedValue == "6")
                {
                    lvStudentCore.DataSource = null;
                    lvStudentCore.DataBind();
                    lvStudentElect.DataSource = null;
                    lvStudentElect.DataBind();
                    lvStudentGlobal.DataSource = ds;
                    lvStudentGlobal.DataBind();
                    divCore.Visible = false;
                    divElect.Visible = false;
                    divGlobal.Visible = true;
                }
                
                lvApproveCourse.Visible = true;
                dvStudentInfo.Visible = false;
                tblInfo.Visible = true;
                btnSubmit.Enabled = true;
            }
            else
            {
                lvApproveCourse.DataSource = null;
                lvApproveCourse.DataBind();
                lvStudentCore.DataSource = null;
                lvStudentCore.DataBind();
                lvStudentElect.DataSource = null;
                lvStudentElect.DataBind();
                lvStudentGlobal.DataSource = null;
                lvStudentGlobal.DataBind();
                divCore.Visible = false;
                divElect.Visible = false;
                divGlobal.Visible = false;
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage(uplReg, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Course_Registration_Approval.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgEditBtn = sender as ImageButton;
        int IDNO = Convert.ToInt32(imgEditBtn.CommandArgument);
        ShowDetails(IDNO);

        dvStudentInfo.Visible = true;
        btnSubmit.Enabled = false;
        lvUniCoreSub.Enabled = true;
        lvGlobalSubjects.Enabled = true;
        btnPrintRegSlip.Enabled = true;
        //lvValueAddedGroup.Visible = false;
        if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 9)
        {
            string facAdvFlag = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(FACULTY_ADVISOR_APPROVAL,0)", "");
            if (facAdvFlag == "0")
            {
                lvUniCoreSub.Enabled = false;
                lvGlobalSubjects.Enabled = false;
            }
           // lvValueAddedGroup.Visible = true;
        }
    }

    private void ShowDetails(int IDNO)
    {
        try
        {
            StudentFeedBackController SFB = new StudentFeedBackController();
            // string feedback_status;
            int sessionno = Convert.ToInt16(ddlSession.SelectedValue);
            int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", "IDNO=" + IDNO + ""));
            if (IDNO <= 0)
            {
                objCommon.DisplayMessage(uplReg, "Student with Roll No." + IDNO + " Not Exists!", this.Page);
                divCourses.Visible = false;
                return;
            }
            //check the deparment of HOD
            DataSet dsStudent = objSReg.Get_Student_Details_for_Course_Registration(IDNO, 84, 2);   // Convert.ToInt32(Session["usertype"])
            ViewState["DataSet_Student_Details"] = dsStudent;

            if (dsStudent != null && dsStudent.Tables[1].Rows.Count > 0)
            {
                if (dsStudent.Tables[1].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[1].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[1].Rows[0]["IDNO"].ToString();

                    if (dsStudent.Tables[1].Rows[0]["FATHERNAME"].ToString() != null)
                        lblFatherName.Text = dsStudent.Tables[1].Rows[0]["FATHERNAME"].ToString();
                    else
                        lblFatherName.Text = dsStudent.Tables[1].Rows[0]["FATHERFIRSTNAME"].ToString();

                    lblMotherName.Text = dsStudent.Tables[1].Rows[0]["MOTHERNAME"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[1].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[1].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[1].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[1].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[1].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[1].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[1].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[1].Rows[0]["SEMESTERNO"].ToString();
                    //ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[1].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[1].Rows[0]["ADMBATCH"].ToString();
                    hdfDegreeno.Value = dsStudent.Tables[1].Rows[0]["DEGREENO"].ToString();
                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[1].Rows[0]["PH"].ToString();
                    tblInfo.Visible = true;
                    //DataSet dsTotRegCredits = (DataSet)ViewState["DataSet_Student_Details"]; //objSReg.GetTotalCreditsCount(Convert.ToInt32(hdfDegreeno.Value), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip));
                    lblOfferedRegCredits.Text = (dsStudent != null && dsStudent.Tables[2].Rows.Count > 0) ? Convert.ToString(dsStudent.Tables[2].Rows[0]["TOT_CREDIT_GROUP"]) : "0";
                    lblOfferedRegCreditsFrom.Text = (dsStudent != null && dsStudent.Tables[2].Rows.Count > 0) ? Convert.ToString(dsStudent.Tables[2].Rows[0]["TOT_CREDIT_GROUP_FROM"]) : "0";

                    #region Core Course
                    //Show Current Semester Courses ..
                    DataSet dsCurrCourses = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip),
                        Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 1, Convert.ToInt32(Session["usertype"].ToString()));

                    lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                    lvCurrentSubjects.DataBind();
                    hdnCount.Value = dsCurrCourses.Tables[0].Rows.Count.ToString();
                    //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                    //{
                    //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                    //    Label lblIntake = item.FindControl("lblIntake") as Label;
                    //    //if (lblIntake.Text != string.Empty && lblIntake.Text != "NA")
                    //    //{
                    //    //    if (Convert.ToInt32(lblIntake.Text) <= 0)
                    //    //        chkAccept.Enabled = false;
                    //    //}

                    //    //bool chk = Convert.ToBoolean(chkAccept.ToolTip);
                    //    //if (chk == true)
                    //    //{
                    //    //    chkAccept.Enabled = true;
                    //    //    lblIntake.Text = "Registered";
                    //    //}
                    //    //else
                    //    //{
                    //    //    chkAccept.Checked = true;
                    //    //    chkAccept.Enabled = false;
                    //    //}
                    //}

                    #endregion

                    #region elective Course
                    /************************************************Commented on 21-10-2021 by Dileep Kare Becoz no need get separate course list**************************************************/
                    DataSet dsUniCodeSub = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip),
                        Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 2, Convert.ToInt32(Session["usertype"].ToString()));

                    lvUniCoreSub.DataSource = dsUniCodeSub.Tables[0];
                    lvUniCoreSub.DataBind();
                    #endregion

                    #region Global Course
                    //DataSet dsGlobalCodeSub = objCommon.FillDropDown("ACD_COURSE C LEFT JOIN acd_course_teacher CT ON(C.SCHEMENO=CT.SCHEMENO AND CT.SEMESTERNO=CT.SEMESTERNO AND C.COURSENO=CT.COURSENO) LEFT JOIN USER_ACC U ON (U.UA_NO=CT.UA_NO) LEFT JOIN ACD_SECTION SE ON(SE.SECTIONNO=CT.SECTIONNO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_ELECTGROUP P ON (C.GROUPNO=P.GROUPNO)", "DISTINCT C.COURSENO", "C.CCODE,c.GROUPNO,P.GROUPNAME,ISNULL(P.CHOICEFOR,0) AS CHOICEFOR,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS as CREDITS,S.SUBNAME,(CASE WHEN (SELECT EXAM_REGISTERED FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND COURSENO=C.COURSENO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=0)=1 THEN 1 ELSE 0 END)EXAM_REGISTERED,ISNULL(CT.INTAKE,0)-(select COUNT(ISNULL(COURSENO,0)) from ACD_STUDENT_RESULT where  SEMESTERNO=CT.SEMESTERNO AND CCODE=CT.CCODE AND SESSIONNO=CT.SESSIONNO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AS INTAKE,U.UA_FULLNAME,SE.SECTIONNAME,isnull(SE.SectionNO,0) as SectionNO,U.UA_NO", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1 AND  C.ELECT=1 AND ISNULL(C.GLOBALELE,0)=0", "C.ELECT,C.GROUPNO");
                    DataSet dsGlobalCodeSub = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip),
                        Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 3, Convert.ToInt32(Session["usertype"].ToString()));

                    lvGlobalSubjects.DataSource = dsGlobalCodeSub.Tables[0];
                    lvGlobalSubjects.DataBind();
                    /************************************************above Commented on 21-10-2021 Becoz no need get separate course list**************************************************/
                    #endregion

                    #region Value Added

                    //DataSet dsValueAdded = objCommon.FillDropDown(@"ACD_GROUP_MASTER_SPECIALIZATION GS LEFT JOIN ACD_VALUEADDED_COURSE VC ON VC.GROUPID=GS.GROUPID",
                    //                                               " DISTINCT VC.GROUPID,GS.GROUP_NAME",
                    //                                               " (SELECT 1 FROM ACD_STUDENT_VALUE_ADDED_GROUP WHERE GROUPID=VC.GROUPID AND IDNO=" + Convert.ToInt32(lblName.ToolTip)
                    //                                             + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip)
                    //                                             + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                    //                                             + " AND ISNULL(ACTIVE_STATUS,0)=1) REGISTERED",
                    //                                               " VC.SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip),
                    //                                               " VC.GROUPID");

                    DataSet dsValueAdded = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip),
                        Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 4, Convert.ToInt32(Session["usertype"].ToString()));

                    if (dsValueAdded != null && dsValueAdded.Tables[0].Rows.Count > 0)
                    {
                        lvValueAddedGroup.DataSource = dsValueAdded.Tables[0];
                        lvValueAddedGroup.DataBind();
                        lvValueAddedGroup.Visible = true;
                    }
                    else
                        lvValueAddedGroup.Visible = false;

                    #endregion

                    TotalRegisterCreditsCount();
                    divCourses.Visible = true;
                    tblInfo.Visible = false;
                }
                else
                {
                    objCommon.DisplayMessage(uplReg, "Scheme not found, Please contact your department.!!!", this.Page);
                    divCourses.Visible = false;
                    return;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnCourseUptForStud_Click(object sender, EventArgs e)
    {
        double TotRegCreditsCount = 0;
        int status = 0;
        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true) status++;
        }
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true) status++;
        }
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true) status++;
        }
        if (status != 0)
        {
            string studentIDs = lblName.ToolTip;
            //Add registered 
            StudentRegist objSR = new StudentRegist();
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = lblEnrollNo.Text.Trim();
            objSR.ROLLNO = string.Empty;
            objSR.Audit_course = "0";
            objSR.EXAM_REGISTERED = 1;  ////// Update as REGISTERED =1;
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                    TotRegCreditsCount += Convert.ToDouble(Credits);
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                }
            }

            foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    int checkelectivecapacity = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(IS_DEPARTMENT_ELECTIVE_CAPACITY_CHECK,0) IS_DEPARTMENT_ELECTIVE_CAPACITY_CHECK",
                                                                       "OrganizationId = " + Convert.ToInt32(Session["OrgId"])));
                    if (checkelectivecapacity > 0)
                    {
                        string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;
                        string maxSeatsForGLobalSubj = objCommon.LookUp("ACD_OFFERED_COURSE", "ISNULL(CAPACITY,0)",
                                                                        " ISNULL(COURSE_OFFERED,0)=1 AND COURSENO = " + Convert.ToInt32(Code) +
                                                                        "AND SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(objSR.SCHEMENO));


                        int studcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_OFFERED_COURSE OC ON(R.COURSENO=OC.COURSENO AND R.SESSIONNO=OC.SESSIONNO AND R.SEMESTERNO = OC.SEMESTERNO)", "ISNULL(COUNT(R.COURSENO),0)",
                                                                           "R.COURSENO = " + Convert.ToInt32(Code) +
                                                                           "AND ISNULL(R.CANCEL,0)= 0 AND R.IDNO = " + Convert.ToInt32(objSR.IDNO) + " AND R.SESSIONNO =" + Convert.ToInt32(objSR.SESSIONNO) + " AND R.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND ISNULL(OC.COURSE_OFFERED,0)=1"));
                        // " AND TO_SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip));
                        //" AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) +
                        if (studcount > 0)
                        {

                        }
                        else
                        {

                            if (Convert.ToInt32(maxSeatsForGLobalSubj) > 0)
                            {
                                int TotalRegisteredcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_OFFERED_COURSE OC ON(R.COURSENO=OC.COURSENO AND R.SESSIONNO=OC.SESSIONNO AND R.SEMESTERNO = OC.SEMESTERNO)", "ISNULL(COUNT(R.COURSENO),0)",
                                                                           "R.COURSENO = " + Convert.ToInt32(Code) +
                                                                           "AND ISNULL(R.CANCEL,0)= 0 AND R.SCHEMENO=" + Convert.ToInt32(objSR.SCHEMENO) + " AND R.SESSIONNO =" + Convert.ToInt32(objSR.SESSIONNO) + " AND R.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND ISNULL(OC.COURSE_OFFERED,0)=1"));

                                //" AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) +
                                int intakeAvailable = Convert.ToInt32(maxSeatsForGLobalSubj) - TotalRegisteredcount;

                                //int GlobalSubAvailable = objSReg.GetGlobalCoursesAvailableSeats(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(Code),Convert.ToInt32(lblBranch.ToolTip));
                                if (intakeAvailable <= 0)
                                {
                                    string lblCCode = (dataitem.FindControl("lblCCode") as Label).Text;
                                    string lblCourseName = (dataitem.FindControl("lblCourseName") as Label).Text;
                                    objCommon.DisplayMessage(uplReg, "This Elective Courses " + lblCCode + " - " + lblCourseName + " seats are Full.. ", this.Page);
                                    return;
                                }

                            }
                        }
                    }

                    string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                    TotRegCreditsCount += Convert.ToDouble(Credits);
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                }
            }

            foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                   
                    string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;
                    string maxSeatsForGLobalSubj = objCommon.LookUp("ACD_GLOBAL_OFFERED_COURSE", "ISNULL(CAPACITY,0)",
                                                                    " ISNULL(GLOBAL_OFFERED,0)=1 AND COURSENO = " + Convert.ToInt32(Code) +
                                                                    "AND " + Convert.ToInt32(lblSemester.ToolTip) + "  IN (SELECT VALUE FROM DBO.SPLIT(TO_SEMESTERNO,','))");

                    int studcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                                                                       "R.COURSENO = " + Convert.ToInt32(Code) +
                                                                       "AND ISNULL(R.CANCEL,0)= 0 AND R.IDNO = " + Convert.ToInt32(objSR.IDNO) + " AND R.SESSIONNO IN(SELECT SESSIONNO FROM ACD_GLOBAL_OFFERED_COURSE WHERE R.COURSENO = COURSENO AND ISNULL(GLOBAL_OFFERED,0)=1)"));
                    if (studcount > 0)
                    {

                    }
                    else
                    {
                        if (Convert.ToInt32(maxSeatsForGLobalSubj) > 0)
                        {
                            int TotalRegisteredcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                                                                         "R.COURSENO = " + Convert.ToInt32(Code) +
                                                                         "AND ISNULL(R.CANCEL,0)= 0 AND R.SESSIONNO IN(SELECT SESSIONNO FROM ACD_GLOBAL_OFFERED_COURSE WHERE R.COURSENO = COURSENO AND ISNULL(GLOBAL_OFFERED,0)=1)"));
                            //int GlobalSubAvailable = objSReg.GetGlobalCoursesAvailableSeats(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(Code), Convert.ToInt32(lblBranch.ToolTip));
                            int intakeAvailable = Convert.ToInt32(maxSeatsForGLobalSubj) - TotalRegisteredcount;

                            if (intakeAvailable <= 0)
                            {
                                string lblCCode = (dataitem.FindControl("lblCCode") as Label).Text;
                                string lblCourseName = (dataitem.FindControl("lblCourseName") as Label).Text;
                                objCommon.DisplayMessage(uplReg, "This Global Courses " + lblCCode + " - " + lblCourseName + " seats are Full.. ", this.Page);
                                return;
                            }
                        }
                    }
                    string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                    TotRegCreditsCount += Convert.ToDouble(Credits);
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                }
            }

            if (lvValueAddedGroup.Visible == true)
            {
                int grpsCnt = 0;
                foreach (ListViewDataItem dataitem in lvValueAddedGroup.Items)
                {
                    if ((dataitem.FindControl("chkValueAddedGroup") as CheckBox).Checked == true)
                    { grpsCnt++; }
                }
                if (grpsCnt > 0)
                {
                    if (grpsCnt != 2)
                    {
                        objCommon.DisplayMessage(this.Page, "You can Select Only Two Group.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
                    return;
                }
                int IsRecordUpdated = objSReg.UpdateGroupsForValueAddedCourse(objSR); 

                foreach (ListViewDataItem dataitem in lvValueAddedGroup.Items)
                {
                    CheckBox chkAccept = dataitem.FindControl("chkValueAddedGroup") as CheckBox;
                    if (chkAccept.Checked == true)
                    {
                        int ret1 = objSReg.UpSertGroupsForValueAddedCourse(objSR, Convert.ToInt32(chkAccept.ToolTip));
                        DataSet ds = objSReg.GetCourseSectionOfValueAddedGrp(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(chkAccept.ToolTip));
                        string credits = string.Empty;
                        string crsNo = string.Empty;
                        string secNo = string.Empty;
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            credits = ds.Tables[0].Rows[0]["CREDITS"].ToString();
                            crsNo = ds.Tables[0].Rows[0]["COURSENO"].ToString();
                            secNo = ds.Tables[0].Rows[0]["SECTIONNO"].ToString();
                        }
                        else
                        {
                            credits = string.Empty;
                            crsNo = string.Empty;
                            secNo = string.Empty;
                        }
                        TotRegCreditsCount += Convert.ToDouble(credits);
                        objSR.COURSENOS += crsNo + "$";
                        objSR.SECTIONNOS += secNo + "$";
                    }
                }
            }
            int ret = objSReg.AddAddlRegisteredSubjectsApprovalLogin(objSR);
            if (ret > 0)
            {
                BindListView();
                dvStudentInfo.Visible = false;
                btnSubmit.Enabled = true;
                btnPrintChallan.Enabled = true;
                objCommon.DisplayMessage(this.Page, "Course Registration Done Successfully.!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(uplReg, "Please Select atleast One Course in course list for Course Registration..!!", this.Page);
        }
    }
    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptCourseRegSlip.rpt");
    }
    protected void btnPrePrintClallan_Click(object sender, EventArgs e)
    {
        string studentIDs = lblName.ToolTip;
        int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
        string dcrNo = objCommon.LookUp("ACD_DCR WITH (NOLOCK)", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + selectSemesterNo);
        if (dcrNo != string.Empty && studentIDs != string.Empty)
            ShowReport("FeeCollectionReceiptForSemCourseRegister.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        ////Show Tabulation Sheet
        //LinkButton btn = sender as LinkButton;
        //string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        //string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        //string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        //string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;
        //this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt",sessionNo,schemeNo,semesterNo,IdNo);
    }

    protected void chklvUniCoreSub_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dtCourse = null;
        DataTable dtCT = null;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        dtCourse = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
        dtCT = (ds != null && ds.Tables[4].Rows.Count > 0) ? ds.Tables[4] : null;

        int examregcount = 0;
        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            if (chkAccept.Enabled == false)
            {
                examregcount++;
            }
        }

        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblCourseTeacher = dataitem.FindControl("lblCourseTeacher") as Label;
            Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            if (examregcount > 0 && chkAccept.Checked == true && chkAccept.Enabled == true)
            {
                objCommon.DisplayMessage(uplReg, "Exam You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
                chkAccept.Checked = false;
                return;
            }

            if (chkAccept.Checked == true && chkAccept.Enabled == true)
            {
                TotalRegisterCreditsCount();
                CourseDuplicateGroupCheck(Convert.ToInt32(chkAccept.ToolTip));
                ElectiveCourseDuplicateSelectionCheck(Convert.ToInt32(lblCCode.ToolTip));
            }

            if (chkAccept.Checked == true && lblIntake.Text != "NA")
            {
                int RegCoureCount = 0;
                int IntakeCoureCount = 0;
                int courseno = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
                int Sectionno = Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip);
                if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip)) : 0;
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) = 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip) && row.Field<int>("SECTIONNO") == Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip)) : 0;
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) = 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno)
                                                       select new { intake1 = intake["INTAKE"] });

                    //Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip != string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno)
                                                       select new { intake1 = intake["INTAKE"] });
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip != string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                        intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new { intake1 = intake["INTAKE"] });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new { intake1 = intake["INTAKE"] });
                }

                int BalCourseCount = IntakeCoureCount - RegCoureCount;
                lblIntake.Text = Convert.ToString(BalCourseCount);
            }


        }
    }

    protected void chkGlobalSubjects_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dtCourse = null;
        DataTable dtCT = null;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        dtCourse = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
        dtCT = (ds != null && ds.Tables[4].Rows.Count > 0) ? ds.Tables[4] : null;
        int examregcount = 0;
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            if (chkAccept.Enabled == false)
            {
                examregcount++;
            }
        }
        
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblCourseTeacher = dataitem.FindControl("lblCourseTeacher") as Label;
            Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            if (examregcount > 0 && chkAccept.Checked == true && chkAccept.Enabled == true)
            {
                objCommon.DisplayMessage(uplReg, "Exam You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
                chkAccept.Checked = false;
                return;
            }

            if (chkAccept.Checked == true && chkAccept.Enabled == true)
            {
                TotalRegisterCreditsCount();
                CourseDuplicateGloblaGroupCheck(Convert.ToInt32(chkAccept.ToolTip));
                GlobalElectiveCourseDuplicateSelectionCheck(lblCCode.Text);
            }

            if (chkAccept.Checked == true && lblIntake.Text != "NA")
            {
                int RegCoureCount = 0;
                int IntakeCoureCount = 0;
                int courseno = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
                int Sectionno = Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip);
                if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip)) : 0;
                else
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip) && row.Field<int>("SECTIONNO") == Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip)) : 0;


                if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno)
                                                       select new { intake1 = intake["INTAKE"] });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "   AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip != string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno)
                                                       select new { intake1 = intake["INTAKE"] });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip != string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new { intake1 = intake["INTAKE"] });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "' AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new { intake1 = intake["INTAKE"] });
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "' AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                int BalCourseCount = IntakeCoureCount - RegCoureCount;
                lblIntake.Text = Convert.ToString(BalCourseCount);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        // int sessionno = Convert.ToInt32(Session["currentsession"].ToString());
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(Session["idno"]);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.uplReg, this.uplReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Somethingwent Wrong.!!", this.Page);
        }
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + GetReportParameters(studentNo, dcrNo, copyNo);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(uplReg, uplReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void TotalRegisterCreditsCount()
    {
        double Count = 0;
        foreach (ListViewDataItem item in lvCurrentSubjects.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
                Credits = "0";

            if (chkAccept.Checked == true)
                Count += Convert.ToDouble(Credits);
        }

        foreach (ListViewDataItem item in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
                Credits = "0";

            if (chkAccept.Checked == true)
                Count += Convert.ToDouble(Credits);
        }

        foreach (ListViewDataItem item in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
                Credits = "0";

            if (chkAccept.Checked == true)
                Count += Convert.ToDouble(Credits);
        }

    
        foreach (ListViewDataItem dataitem in lvValueAddedGroup.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkValueAddedGroup") as CheckBox;
            if (chkAccept.Checked == true)
            {
                //string credits = objCommon.LookUp(@"ACD_COURSE C INNER JOIN ACD_VALUEADDED_COURSE VC ON C.COURSENO=VC.COURSENO AND C.SEMESTERNO=VC.SEMESTERNO",
                //                       " SUM (ISNULL( C.CREDITS ,0))AS CREDITS",
                //                       " VC.SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) +
                //                       " AND  VC.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) +
                //                       " AND VC.GROUPID=" + Convert.ToInt32(chkAccept.ToolTip));

                DataSet ds = objSReg.GetTotalCreditOfValueAddedGrp(Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(chkAccept.ToolTip));

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string credits = ds.Tables[0].Rows[0]["CREDITS"].ToString();
                    if (credits == string.Empty)
                        credits = "0";
                    Count += Convert.ToDouble(credits);
                }
            }
        }
        lblTotalRegCredits.Text = !string.IsNullOrEmpty(Convert.ToString(Count)) ? String.Format("{0:F2}", Count) : Convert.ToString(Count);
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    private void ElectiveCourseDuplicateSelectionCheck(int Courseno)
    {
        // int count = 0;
        int sectioncount0 = 0, sectioncount1 = 0, sectioncount2 = 0, sectioncount3 = 0, sectioncount4 = 0;
        int sectioncount5 = 0, sectioncount6 = 0, sectioncount7 = 0, sectioncount8 = 0, sectioncount9 = 0;
        int sectioncount10 = 0, sectioncount11 = 0, sectioncount12 = 0, sectioncount13 = 0, sectioncount14 = 0;
        int sectioncount15 = 0, sectioncount16 = 0, sectioncount17 = 0, sectioncount18 = 0, sectioncount19 = 0;

        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            if (chkAccept.Checked == true)
            {
                if (Convert.ToInt32(lblCCode.ToolTip) == Courseno)
                {
                    //count++;
                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 0)
                        sectioncount0++;

                    if (sectioncount0 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 1)
                        sectioncount1++;

                    if (sectioncount1 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 2)
                        sectioncount2++;

                    if (sectioncount2 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 3)
                        sectioncount3++;

                    if (sectioncount3 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 4)
                        sectioncount4++;

                    if (sectioncount4 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 5)
                        sectioncount5++;

                    if (sectioncount5 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 6)
                    {
                        sectioncount6++;
                    }
                    if (sectioncount6 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 7)
                    {
                        sectioncount7++;
                    }
                    if (sectioncount7 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 8)
                    {
                        sectioncount8++;
                    }
                    if (sectioncount8 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 9)
                    {
                        sectioncount9++;
                    }
                    if (sectioncount9 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 10)
                    {
                        sectioncount10++;
                    }
                    if (sectioncount10 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 11)
                    {
                        sectioncount11++;
                    }
                    if (sectioncount11 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 12)
                    {
                        sectioncount12++;
                    }
                    if (sectioncount12 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 13)
                    {
                        sectioncount13++;
                    }
                    if (sectioncount13 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 14)
                    {
                        sectioncount14++;
                    }
                    if (sectioncount14 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 15)
                    {
                        sectioncount15++;
                    }
                    if (sectioncount15 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 16)
                    {
                        sectioncount16++;
                    }
                    if (sectioncount16 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 17)
                    {
                        sectioncount17++;
                    }
                    if (sectioncount17 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 18)
                    {
                        sectioncount18++;
                    }
                    if (sectioncount18 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 19)
                    {
                        sectioncount19++;
                    }
                    if (sectioncount19 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                }
            }
        }
    }

    private void GlobalElectiveCourseDuplicateSelectionCheck(string CCode)
    {
        int sectioncount0 = 0, sectioncount1 = 0, sectioncount2 = 0, sectioncount3 = 0, sectioncount4 = 0;
        int sectioncount5 = 0, sectioncount6 = 0, sectioncount7 = 0, sectioncount8 = 0, sectioncount9 = 0;
        int sectioncount10 = 0, sectioncount11 = 0, sectioncount12 = 0, sectioncount13 = 0, sectioncount14 = 0;
        int sectioncount15 = 0, sectioncount16 = 0, sectioncount17 = 0, sectioncount18 = 0, sectioncount19 = 0;
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;

            if (chkAccept.Checked == true)
            {
                if (lblCCode.Text == CCode)
                {
                    //count++;
                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 0)
                        sectioncount0++;

                    if (sectioncount0 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 1)
                    {
                        sectioncount1++;
                    }
                    if (sectioncount1 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 2)
                    {
                        sectioncount2++;
                    }
                    if (sectioncount2 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 3)
                    {
                        sectioncount3++;
                    }
                    if (sectioncount3 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 4)
                    {
                        sectioncount4++;
                    }
                    if (sectioncount4 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 5)
                    {
                        sectioncount5++;
                    }
                    if (sectioncount5 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 6)
                    {
                        sectioncount6++;
                    }
                    if (sectioncount6 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 7)
                    {
                        sectioncount7++;
                    }
                    if (sectioncount7 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 8)
                    {
                        sectioncount8++;
                    }
                    if (sectioncount8 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 9)
                    {
                        sectioncount9++;
                    }
                    if (sectioncount9 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 10)
                    {
                        sectioncount10++;
                    }
                    if (sectioncount10 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 11)
                    {
                        sectioncount11++;
                    }
                    if (sectioncount11 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 12)
                    {
                        sectioncount12++;
                    }
                    if (sectioncount12 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 13)
                    {
                        sectioncount13++;
                    }
                    if (sectioncount13 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 14)
                    {
                        sectioncount14++;
                    }
                    if (sectioncount14 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 15)
                    {
                        sectioncount15++;
                    }
                    if (sectioncount15 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 16)
                    {
                        sectioncount16++;
                    }
                    if (sectioncount16 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 17)
                    {
                        sectioncount17++;
                    }
                    if (sectioncount17 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 18)
                    {
                        sectioncount18++;
                    }
                    if (sectioncount18 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 19)
                    {
                        sectioncount19++;
                    }
                    if (sectioncount19 > 1)
                    {
                        objCommon.DisplayMessage(uplReg, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }
                }
            }

            //if (count > 1)
            //{
            //    objCommon.DisplayMessage(uplReg, "You Can not select the same course", this.Page);
            //    chkAccept.Checked = false;
            //    return;
            //}
        }

    }

    private void CourseDuplicateGroupCheck(int GroupNO)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            
            if (chkAccept.Checked == true && (Convert.ToInt32(chkAccept.ToolTip) == GroupNO))
                count++;

            if (count > Convert.ToInt32(lblIntake.ToolTip))
            {
                objCommon.DisplayMessage(uplReg, "You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
                chkAccept.Checked = false;
                TotalRegisterCreditsCount();
                return;
            }
        }
    }

    //private void CourseDuplicateValueAddedCheck(string GroupNOs)
    //{
    //    int count = 0, cnt = 0;
    //    string[] grps = GroupNOs.Split(',');

    //    foreach (ListViewDataItem dataitem in lvValueAddedGroup.Items)
    //    {
    //        CheckBox chkAccept = dataitem.FindControl("chkValueAddedGroup") as CheckBox;
    //        if (chkAccept.Checked == true)
    //            cnt++;

    //        if (chkAccept.Checked == true && (Convert.ToInt32(chkAccept.ToolTip) == GroupNO))
    //            count++;

           
    //    }
    //}

    private void CourseDuplicateGloblaGroupCheck(int GroupNO)
    {
        int count = 0;
       
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            Label lblExamRegistred = dataitem.FindControl("lblExamRegistred") as Label;
            if (chkAccept.Checked == true && (Convert.ToInt32(chkAccept.ToolTip) == GroupNO))
            {
                count++;
            }

            if (count > Convert.ToInt32(lblIntake.ToolTip))
            {
                objCommon.DisplayMessage(uplReg, "You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
                chkAccept.Checked = false;
                TotalRegisterCreditsCount();
                return;
            }
        }
    }

    protected void btnCancelUptForStud_Click(object sender, EventArgs e)
    {
        BindListView();
        dvStudentInfo.Visible = false;
        tblInfo.Visible = true;
        btnSubmit.Enabled = true;
    }

    protected void chkValueAddedGroup_CheckedChanged(object sender, EventArgs e)
    {
        TotalRegisterCreditsCount();
        //int grps = 0; string group = string.Empty;
        //foreach (ListViewDataItem items in lvValueAddedGroup.Items)
        //{
        //    CheckBox chkAccept = items.FindControl("chkValueAddedGroup") as CheckBox;
        //    if (chkAccept.Checked == true)
        //    {
        //        grps++;             
        //    }           
        //}
        //group = group.TrimEnd(',');
        //if (grps > 0)
        //{
        //    if (grps != 2)
        //    {
        //        objCommon.DisplayMessage(this.Page, "You can Select Only Two Group.", this.Page);
        //        return;
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Please select Groups.", this.Page);
        //    return;
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvStudentInfo.Visible = false;
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
    }

    //public int GetGlobalCoursesAvailableSeats(int sessionno, int semesterno, int schemeno, int COURSNO, int branchNo)
    //{
    //    int retStatus = Convert.ToInt32(CustomStatus.Others);
    //    try
    //    {
    //         string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
    //        SqlParameter[] objParams = null;

    //        objParams = new SqlParameter[6];
    //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
    //        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
    //        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
    //        objParams[3] = new SqlParameter("@P_COURSENO", COURSNO);
    //        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
    //        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
    //        objParams[5].Direction = ParameterDirection.Output;

    //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSEREGISTRATION_SP_GET_GLOBAL_COURSES_AVAILABLE_SEATS", objParams, true);

    //        if (Convert.ToInt32(ret) == -99)
    //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
    //        else
    //            retStatus = Convert.ToInt32(ret);
    //    }
    //    catch (Exception ex)
    //    {
    //        retStatus = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetGlobalCoursesAvailableSeats-> " + ex.ToString());
    //    }



    //    return retStatus;

    //}
    
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
        lvStudentCore.DataSource = null;
        lvStudentCore.DataBind();
        lvStudentElect.DataSource = null;
        lvStudentElect.DataBind();
        lvStudentGlobal.DataSource = null;
        lvStudentGlobal.DataBind();
        divCore.Visible = false;
        divElect.Visible = false;
        divGlobal.Visible = false;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
        lvStudentCore.DataSource = null;
        lvStudentCore.DataBind();
        lvStudentElect.DataSource = null;
        lvStudentElect.DataBind();
        lvStudentGlobal.DataSource = null;
        lvStudentGlobal.DataBind();
        divCore.Visible = false;
        divElect.Visible = false;
        divGlobal.Visible = false;
        ddlFilter.SelectedIndex = 0;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
        lvStudentCore.DataSource = null;
        lvStudentCore.DataBind();
        lvStudentElect.DataSource = null;
        lvStudentElect.DataBind();
        lvStudentGlobal.DataSource = null;
        lvStudentGlobal.DataBind();
        divCore.Visible = false;
        divElect.Visible = false;
        divGlobal.Visible = false;
    }
}