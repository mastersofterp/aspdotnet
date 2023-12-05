
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Electiveregistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //DataSet DetainedSubjects, CurrentSubjects, PreviousData;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ////ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));
                // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 and sessionno=28", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlAdmBatchCancel, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO AND SC.DEPTNO=DB.DEPTNO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                objCommon.FillDropDownList(ddlClgnameCancel, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO AND SC.DEPTNO=DB.DEPTNO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");

                //ddlSession.SelectedIndex = 1;
                //Fill DropDownLists
                //this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    #endregion
    #region Elective Course Registration
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string roll = string.Empty;
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        CheckBox cbRow = new CheckBox();

        try
        {
            int count = 0, choiceFor = 0; string idnos = string.Empty;
            foreach (ListViewDataItem dataitem in lvStudent.Items)
            {
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
                objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
                objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
                //objSR.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                //objSR.ABSORPTION_STATUS = Convert.ToInt32(ddlStatus.SelectedValue);
                //objSR.GDPOINT = 0;
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                //Get Student Details from lvStudent
                cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled)
                {
                    int grpNo = 0;
                    int totalElectCrs = 0;
                    int idno = Convert.ToInt32(cbRow.ToolTip);
                    idnos += cbRow.ToolTip + ',';

                    double electcreditregistred = 0;
                    double globalcreditregistred = 0;

                    electcreditregistred = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON(R.COURSENO = C.COURSENO)", "ISNULL(SUM(C.CREDITS),0)",
                       "ISNULL(C.GLOBALELE,0)=0 AND ISNULL(C.ELECT,0)=1 AND R.ACCEPTED=1 AND R.REGISTERED=1  AND ISNULL(R.CANCEL,0)=0 AND R.IDNO=" + objSR.IDNO +
                   " AND R.SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                       " AND R.SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue)));

                    globalcreditregistred = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON(R.COURSENO = C.COURSENO)", "ISNULL(SUM(C.CREDITS),0)",
                    "ISNULL(C.GLOBALELE,0)=1 AND ISNULL(C.ELECT,0)=1 AND R.ACCEPTED=1 AND R.REGISTERED=1  AND ISNULL(R.CANCEL,0)=0 AND R.IDNO=" + objSR.IDNO +
                " AND R.SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                    " AND R.SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue)));


                    bool IsGlobalCourse = Convert.ToBoolean(objCommon.LookUp("ACD_COURSE", "ISNULL(GLOBALELE,0)", " COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue)));
                    if (!IsGlobalCourse)
                    {
                        if (electcreditregistred >= Convert.ToDouble(lblTotalCredit.Text))
                        {
                            objCommon.DisplayMessage(updBulkReg, "The Total Elective Credits limits is " + lblTotalCredit.Text + ", Student " + objSR.REGNO + " Already Registered Elective Credit is " + electcreditregistred + ", Kindly check the course registration", this.Page);
                            return;
                        }
                        try
                        {
                            grpNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(GROUPNO,0)", "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));

                            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "COURSENO", "",
                                "ISNULL(ELECT,0)=1 AND ACCEPTED=1 AND REGISTERED=1  AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno +
                                " AND SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                                " AND SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue) +
                                " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"]), "COURSENO");


                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    int courseno = Convert.ToInt16(ds.Tables[0].Rows[i]["COURSENO"].ToString());
                                    int grpNo1 = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(GROUPNO,0)",
                                        "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + courseno +
                                        " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"])));

                                    if (grpNo == grpNo1)
                                        totalElectCrs += Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "1",
                                            "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + courseno + " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"])));
                                }
                            }

                            //choiceFor = Convert.ToInt16(objCommon.LookUp("ACD_ELECTGROUP", "ISNULL(CHOICEFOR,0)", "GROUPNO=" + grpNo));
                        }
                        catch (Exception EX) { }

                        if (grpNo > 0)
                        {
                            try
                            {
                                choiceFor = Convert.ToInt16(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT", "ISNULL(ELECTIVE_CHOISEFOR,0)", "ELECTIVE_GROUPNO=" + grpNo + " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"]) + " AND TERM=" + Convert.ToInt16(ddlSemester.SelectedValue)));
                            }
                            catch
                            {
                                choiceFor = 0;
                                objCommon.DisplayMessage(updBulkReg, "Please Define Choices for selected Elective Group", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updBulkReg, "Please Define Choices for selected Elective Group", this.Page);
                            return;
                        }

                        if (totalElectCrs > 0 && grpNo > 0 && choiceFor > 0 && totalElectCrs >= choiceFor)
                            count = 1;
                    }
                    else
                    {
                        if (globalcreditregistred >= Convert.ToDouble(lblTotalCredit.Text))
                        {
                            objCommon.DisplayMessage(updBulkReg, "The Total Global Credits limits is " + lblTotalCredit.Text + ", Student " + objSR.REGNO + " Already Registered Global Credit is " + globalcreditregistred + ", Kindly check course registration", this.Page);
                            return;
                        }
                    }

                    if (count > 0)
                    {
                        foreach (ListViewDataItem dataitm in lvStudent.Items)
                        {
                            CheckBox chkAccept = dataitm.FindControl("cbRow") as CheckBox;
                            if (!string.IsNullOrEmpty(idnos) && idnos.Contains(chkAccept.ToolTip) && chkAccept.Enabled)
                                chkAccept.Checked = false;
                        }

                        objCommon.DisplayMessage(updBulkReg, "You can select only " + choiceFor + " course for same group.!", this.Page);
                        return;
                    }

                    //Get Course Details
                    //foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
                    //{
                    //    if (((dataitemCourse.FindControl("cbRow")) as CheckBox).Checked == true)
                    //    {
                    //        objSR.COURSENOS += ((dataitemCourse.FindControl("lblCCode")) as Label).ToolTip + "$";
                    //        Label elective = (dataitemCourse.FindControl("lblCourseName")) as Label;
                    //        if (elective.ToolTip == "False")
                    //        {
                    //            objSR.ELECTIVE += "0" + "$";
                    //        }
                    //        else
                    //        {
                    //            objSR.ELECTIVE += "1" + "$";
                    //        }
                    //    }
                    //}
                    objSR.COURSENOS = ddlcourselist.SelectedValue;
                    objSR.ELECTIVE = "1";
                    objSR.ACEEPTSUB = "1";

                    //Register Single Student
                    int prev_status = 0;    //Regular Courses
                    //int seatno = 0;
                    CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulkElective(objSR, prev_status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objSR.COURSENOS = string.Empty;
                        objSR.ACEEPTSUB = string.Empty;
                    }
                }
                else
                {
                    if (cbRow.Checked == false)
                    {
                        objSR.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
                        objSR.COURSENO = Convert.ToInt32(ddlcourselist.SelectedValue);
                        CustomStatus cs = (CustomStatus)objSRegist.CancelElectiveCourseRegistration(objSR);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //objCommon.DisplayMessage(updBulkCancel, "Student(s) Elective Course Registration Cancel Successfully!!", this.Page);
                        }
                    }
                }
            }
            objCommon.DisplayMessage(updBulkReg, "Student(s) Registered Successfully!!", this.Page);
            clear();
            txtTotStud.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.DataSource = null;
        ddlcourselist.DataBind();
        ddlcourselist.SelectedIndex = 0;
        //ddlcourselist.SelectedIndex = 0;
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSection.SelectedIndex > 0)
        {
            if (ddlSemester.SelectedIndex <= 0 || Convert.ToInt32(ViewState["schemeno"]) <= 0)
            {
                objCommon.DisplayMessage("Please Select Semester/Scheme", this.Page);
                return;
            }
            else
            {
                this.BindListView();
            }
        }
        else
        {
            ddlcourselist.Items.Clear();
            ddlcourselist.Items.Add("Please Select");
            ddlcourselist.SelectedItem.Value = "0";
        }

    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //if (ddlBranch.SelectedIndex <= 0 || Convert.ToInt32(ViewState["schemeno"]) <= 0)
            //{
            //    objCommon.DisplayMessage("Please Select Branch/Scheme", this.Page);
            //    return;
            //}
            //else
            //{
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST INNER JOIN  ACD_SECTION  S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
            // objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            //this.BindListView();
            //}
        }
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        btnSubmit.Enabled = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        //else
        //{
        //    lvStudent.DataSource = null;
        //    lvStudent.DataBind();
        //    pnlStudents.Visible = false;

        //    lvCourse.DataSource = null;
        //    lvCourse.DataBind();
        //    pnlCourses.Visible = false;

        //    lvStudentsRemain.DataSource = null;
        //    lvStudentsRemain.DataBind();
        //    pnlStudentsReamin.Visible = false;

        //}

    }

    #region User Defined Methods

    public int cInt(string strInteger)
    {
        int i = 0; int.TryParse(strInteger, out i); return i;
    }

    private void BindListView()
    {
        //Get student list as per scheme & semester & secion(AND SECTIONNO = " + ddlSection.SelectedValue)
        StudentController objSC = new StudentController();
        DataSet ds = null;
        //ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

        ddlcourselist.Items.Clear();
        ddlcourselist.Items.Add("Please Select");
        ddlcourselist.SelectedItem.Value = "0";
        //ddlcourselist.DataSource = null;
        //ddlcourselist.DataBind();
        ////for getting courses semester wise
        //objCommon.FillDropDownList(ddlcourselist,"ACD_COURSE","COURSENO","CCODE +'-'+COURSE_NAME","SCHEMENO = "+ddlScheme.SelectedValue+"and semesterno="+ddlSemester.SelectedValue+"and offered=1 and isnull()
        //objCommon.FillDropDownList(ddlcourselist, "ACD_COURSE", "COURSENO", "CCODE +'-'+COURSE_NAME", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND OFFERED = 1 AND ISNULL(elect,0)=1", "COURSENO");
        DataSet dc = objSC.GetCourseElectiveAndGlobalElective(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
        if (dc != null && dc.Tables.Count > 0)
        {
            ddlcourselist.DataSource = dc;
            //ddlcourselist.DataValueField = dc.Tables[0].Columns[0].ToString();
            //ddlcourselist.DataTextField = dc.Tables[0].Columns[1].ToString();
            //ddlcourselist.DataBind();
            ddlcourselist.DataTextField = "COURSE_NAME";
            ddlcourselist.DataValueField = "COURSENO";
            ddlcourselist.DataBind();
            //txtTotStud.Text = dc.Tables.Count.ToString();

        }
        else
        {
            ddlcourselist.Items.Clear();
            ddlcourselist.Items.Add("Please Select");
            ddlcourselist.SelectedItem.Value = "0";
        }

        // below line commented by Shailendra on dated 15.09.2022 as per tkt no.{34300} no need of below logic we got the same data in dataset dc.
        // DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "CCODE,COURSE_NAME,ELECT", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND OFFERED = 1 AND ISNULL(elect,0) = 1", "COURSENO");
        DataSet dsCourse = dc;
        if (dsCourse != null && dsCourse.Tables.Count > 0)
        {
            if (dsCourse.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsCourse;
                lvCourse.DataBind();
                //ddlcourselist.DataSource = dsCourse.Tables[0];
                //ddlcourselist.DataTextField = "CCODE" + "COURSE_NAME";
                //ddlcourselist.DataValueField = "COURSENO";
                //ddlcourselist.DataBind();
                divcourse.Visible = true;
                pnlCourses.Visible = true;


                ds = objSC.GetStudentsBySchemeElective(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSection.SelectedValue));

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        pnlStudents.Visible = true;
                        btnSubmit.Enabled = true;
                        hftot.Value = ds.Tables[0].Rows.Count.ToString();
                        int count = 0;
                        //for getting student list semester wise
                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                            String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                            if (chkBox.Checked == true)
                                count = count + 1;

                            //if (lblReg == "1")  //IF STUDENT IS ALREADY REGISTERED THEN CHECKBOX WILL BE DISABLED
                            //{
                            //    chkBox.Enabled = false;
                            //    //chkBox.Checked = true;
                            //    chkBox.BackColor = System.Drawing.Color.Green;
                            //}
                            lblReg = string.Empty;
                        }
                        txtTotStud.Text = count.ToString();
                    }
                    else
                    {
                        lvCourse.DataSource = null;
                        divcourse.Visible = false;
                        lvCourse.DataBind();
                        pnlCourses.Visible = false;
                        objCommon.DisplayUserMessage(this.updBulkReg, "No Students Found!", this.Page);
                    }
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    pnlCourses.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updBulkReg, "No Elective courses Found!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnSubmit.Enabled = false;
                pnlStudents.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;
            btnSubmit.Enabled = false;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            else
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO >0", "BRANCHNO");

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
        }
    }

    #endregion

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label elective = (e.Item.FindControl("lblCourseName")) as Label;
        if (elective.ToolTip == "False")
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        }
        else
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        LinkButton printButton = sender as LinkButton;
        int idno = Int32.Parse(printButton.CommandArgument);
        ViewState["IDNO"] = idno;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO = " + ddlSession.SelectedValue));
        if (count > 0)
        {
            objCommon.DisplayMessage("This Student is already registered for session " + ddlSession.SelectedItem.Text, this.Page);
            ShowReport("Registered_Courses", "rptPreRegSlip.rpt");
        }
        else
        {
            objCommon.DisplayMessage("This Student is not registered for session " + ddlSession.SelectedItem.Text, this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_SESSIONNO=" + ddlSession.SelectedValue 
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue 
                + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) 
                + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue 
                + ",@UserName=" + Session["username"]
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBulkReg, this.updBulkReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
      


        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
        //        + ",@P_SESSIONNO=" + ddlSession.SelectedValue 
        //        + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
        //        + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue 
        //        + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) 
        //        + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue 
        //        + ",@UserName=" + Session["username"]
    
            
        //    //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    //divMsg.InnerHtml += " </script>";

        //    //To open new window from Updatepanel
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //    sb.Append(@"window.open('" + url + "','','" + features + "');");

        //    ScriptManager.RegisterClientScriptBlock(this.updBulkReg, this.updBulkReg.GetType(), "controlJSScript", sb.ToString(), true);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_StudentRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "SM.SEMESTERNO");
                ddlSemester.Focus();
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            btnSubmit.Enabled = false;
            divcourse.Visible = false;
            ddlcourselist.SelectedIndex = 0;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "SM.SEMESTERNO");
        //Commented by As per Requirement Of Romal Saluja Sir 

        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "SM.SEMESTERNO");

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
        if (ds != null && ds.Tables.Count > 0)
        {
            ddlSemester.DataSource = ds;
            ddlSemester.DataTextField = "SEMESTERNAME";
            ddlSemester.DataValueField = "SEMESTERNO";
            ddlSemester.DataBind();
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        btnSubmit.Enabled = false;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));

        ddlStatus.SelectedIndex = 0;

        btnSubmit.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue != "0")
        {
            ViewState["college_id"] = Convert.ToInt32(ddlCollege.SelectedValue).ToString();
        }
        //Commented by vipul on dated 26.10.2023 as per T-49243
        //ShowReport("RegistrationSlip", "rptBulkCourseRegslipelective.rpt"); 
        ShowReport("RegistrationSlip", "rptBulkCourseRegSlip_01.rpt");
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");

        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        }
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO = D.DEGREENO)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", ", "D.DEGREENO");

    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT (SESSIONNO)", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                    ddlSession.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
    #region Elective Course Cancellation
    protected void ddlSessionCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemesterCancel, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno1"]) + " AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno1"]), "SM.SEMESTERNO");
    }
    protected void ddlClgnameCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlClgnameCancel.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgnameCancel.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno1"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno1"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id1"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno1"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    objCommon.FillDropDownList(ddlSessionCancel, "ACD_SESSION_MASTER", "DISTINCT (SESSIONNO)", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id1"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                    ddlSession.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSemesterCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemesterCancel.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlSectionCancel, "ACD_STUDENT ST INNER JOIN  ACD_SECTION  S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + Convert.ToInt32(ViewState["schemeno1"]) + " AND ST.Semesterno= " + ddlSemesterCancel.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
        }
    }
    protected void ddlSectionCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionCancel.SelectedIndex > 0)
        {
            if (ddlSemesterCancel.SelectedIndex <= 0 || Convert.ToInt32(ViewState["schemeno1"]) <= 0)
            {
                objCommon.DisplayMessage("Please Select Semester/Scheme", this.Page);
                return;
            }
            else
            {
                this.BindlvCancelCourse();
                //this.BindListViewForCancel();
            }
        }
        else
        {
            ddlElective.Items.Clear();
            ddlElective.Items.Add("Please Select");
            ddlElective.SelectedItem.Value = "0";
        }

    }
    private void BindListViewForCancel()
    {
        //Get student list as per scheme & semester & secion(AND SECTIONNO = " + ddlSection.SelectedValue)
        StudentController objSC = new StudentController();
        // DataSet ds = null;
        ddlcourselist.Items.Clear();
        ddlcourselist.Items.Add("Please Select");
        ddlcourselist.SelectedItem.Value = "0";
        DataSet dc = objSC.GetCourseElectiveAndGlobalElective(Convert.ToInt32(ViewState["schemeno1"]), Convert.ToInt32(ddlSemesterCancel.SelectedValue), Convert.ToInt32(ddlSessionCancel.SelectedValue));
        if (dc != null && dc.Tables.Count > 0)
        {
            ddlElective.DataSource = dc;
            ddlElective.DataTextField = "COURSE_NAME";
            ddlElective.DataValueField = "COURSENO";
            ddlElective.DataBind();

        }
        else
        {
            ddlElective.Items.Clear();
            ddlElective.Items.Add("Please Select");
            ddlElective.SelectedItem.Value = "0";
        }
    }
    protected void btnCancelElective_Click(object sender, EventArgs e)
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        // CheckBox cbRow1 = new CheckBox();
        int count = 0;
        try
        {
            foreach (ListViewDataItem dataitem in lvCancelCourse.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRowCancel") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updBulkCancel, "Please Select atleast one Student", this);
                return;
            }
            else
            {
                foreach (ListViewDataItem dataitem in lvCancelCourse.Items)
                {
                    //Get Student Details from lvCancelCourse
                    CheckBox cbRow1 = dataitem.FindControl("cbRowCancel") as CheckBox;
                    if (cbRow1.Checked == true)
                    {
                        objSR.SESSIONNO = Convert.ToInt32(ddlSessionCancel.SelectedValue);
                        objSR.IDNO = Convert.ToInt32(cbRow1.ToolTip);
                        objSR.SEMESTERNO = Convert.ToInt32(ddlSemesterCancel.SelectedValue);
                        objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno1"]);
                        objSR.SECTIONNO = Convert.ToInt32(ddlSectionCancel.SelectedValue);
                        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                        objSR.COLLEGE_CODE = Session["colcode"].ToString();
                        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                        int COURSENO = Convert.ToInt32(((dataitem.FindControl("lblCourse")) as Label).ToolTip);
                        //int orgid=Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                        CustomStatus cs = (CustomStatus)objSRegist.CancelRegisteredSubjectsBulkElective(objSR, COURSENO);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(updBulkCancel, "Student(s) Elective Course Registration Cancel Successfully!!", this.Page);
                        }
                    }
                }
                this.BindlvCancelCourse();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void BindlvCancelCourse()
    {
        StudentController objSC = new StudentController();
        DataSet dS = objSC.GetCourseElectiveAndGlobalElectiveStudent(Convert.ToInt32(ddlAdmBatchCancel.SelectedValue), Convert.ToInt32(ddlSemesterCancel.SelectedValue), Convert.ToInt32(ddlSessionCancel.SelectedValue), Convert.ToInt32(ViewState["schemeno1"]), Convert.ToInt32(ddlSectionCancel.SelectedValue));
        if (dS.Tables[0].Rows.Count > 0 && dS.Tables[0] != null)
        {
            lvCancelCourse.DataSource = dS;
            lvCancelCourse.DataBind();
            PanelCancel.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(updBulkCancel, " Record not found!!", this.Page);
        }
    }

    protected void ddlElective_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlElective.SelectedIndex > 0)
        {
            StudentController objSC = new StudentController();
            DataSet dS = objSC.GetCourseElectiveAndGlobalElectiveStudent(Convert.ToInt32(ddlAdmBatchCancel.SelectedValue), Convert.ToInt32(ddlSemesterCancel.SelectedValue), Convert.ToInt32(ddlSessionCancel.SelectedValue), Convert.ToInt32(ViewState["schemeno1"]), Convert.ToInt32(ddlSectionCancel.SelectedValue));
            if (dS.Tables[0].Rows.Count > 0 && dS.Tables[0] != null)
            {
                lvCancelCourse.DataSource = dS;
                lvCancelCourse.DataBind();
                PanelCancel.Visible = true;
            }
        }
    }
    #endregion

    protected void ddlcourselist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcourselist.SelectedIndex > 0)
        {
            int IsGlobalCourse = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "CAST(ISNULL(GLOBALELE,0) AS INT)GLOBALELE", "COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue)));
            if (IsGlobalCourse == 1)
            {
                lblTotalCredit.Text = Convert.ToString(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT WITH (NOLOCK)", "DISTINCT ISNULL(GLOBAL_CREDIT,0) TOTAL_CREDIT", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND TERM = " + Convert.ToInt16(ddlSemester.SelectedValue)));
                string totalcredits = lblTotalCredit.Text;
                if (totalcredits.ToString() == "" || totalcredits.ToString() == "0" || totalcredits.ToString() == " ")
                {
                    objCommon.DisplayUserMessage(this.updBulkReg, "Please define Global Credit limit", this.Page);
                    return;
                }
            }
            else
            {
                lblTotalCredit.Text = Convert.ToString(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT WITH (NOLOCK)", "DISTINCT ISNULL(ELECTIVE_CREDIT,0) TOTAL_CREDIT", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND TERM = " + Convert.ToInt16(ddlSemester.SelectedValue)));
                string totalcredits = lblTotalCredit.Text;
                if (totalcredits.ToString() == "" || totalcredits.ToString() == "0" || totalcredits.ToString() == " ")
                {
                    objCommon.DisplayUserMessage(this.updBulkReg, "Please define Elective Credit limit", this.Page);
                    return;
                }
            }

            

            StudentController objSC = new StudentController();
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (S.IDNO=SR.IDNO)", "DISTINCT S.IDNO", "STUDNAME AS NAME,S.REGNO,REGISTERED", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND ISNULL(CANCEL,0) =0 AND SR.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue), "");
            //lvStudent.DataSource = ds;
            //lvStudent.DataBind();
            DataSet ds = null;
            ds = objSC.GetStudentsBySchemeElectiveCourses(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlcourselist.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            int count = 0;
            //for getting student list semester wise
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                {
                    count = count + 1;
                }
            }
            txtTotStud.Text = count.ToString();
        }
    }
    protected void cbRow_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlcourselist.SelectedIndex > 0)
        {
            int count = 0, choiceFor = 0; string idnos = string.Empty;
            foreach (ListViewDataItem dataitem in lvStudent.Items)
            {
                CheckBox chkAccept = dataitem.FindControl("cbRow") as CheckBox;

                if (chkAccept.Checked == true && chkAccept.Enabled)
                {
                    int grpNo = 0;
                    int idno = Convert.ToInt32(chkAccept.ToolTip);
                    idnos += chkAccept.ToolTip + ',';

                    double electcreditregistred = 0;
                    double globalcreditregistred = 0;

                    electcreditregistred = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON(R.COURSENO = C.COURSENO)", "ISNULL(SUM(C.CREDITS),0)",
                       "ISNULL(C.GLOBALELE,0)=0 AND ISNULL(C.ELECT,0)=1 AND R.ACCEPTED=1 AND R.REGISTERED=1  AND ISNULL(R.CANCEL,0)=0 AND R.IDNO=" + idno +
                   " AND R.SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                       " AND R.SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue)));

                    globalcreditregistred = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON(R.COURSENO = C.COURSENO)", "ISNULL(SUM(C.CREDITS),0)",
                    "ISNULL(C.GLOBALELE,0)=1 AND ISNULL(C.ELECT,0)=1 AND R.ACCEPTED=1 AND R.REGISTERED=1  AND ISNULL(R.CANCEL,0)=0 AND R.IDNO=" + idno +
                " AND R.SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                    " AND R.SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue)));

                    int totalElectCrs = 0;
                    bool IsGlobalCourse = Convert.ToBoolean(objCommon.LookUp("ACD_COURSE", "ISNULL(GLOBALELE,0)", " COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue)));
                    if (!IsGlobalCourse)
                    {
                        if (electcreditregistred >= Convert.ToDouble(lblTotalCredit.Text))
                        {
                            objCommon.DisplayMessage(updBulkReg, "The Total Elective Credits limits is " + lblTotalCredit.Text + ", Student Already Registered Elective Credit is " + electcreditregistred + ", Kindly check the course registration", this.Page);
                            return;
                        }
                        try
                        {
                            grpNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(GROUPNO,0)", "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + Convert.ToInt32(ddlcourselist.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));

                            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "COURSENO", "",
                                "ISNULL(ELECT,0)=1 AND ACCEPTED=1 AND REGISTERED=1  AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno +
                                " AND SEMESTERNO=" + Convert.ToInt16(ddlSemester.SelectedValue) +
                                " AND SESSIONNO=" + Convert.ToInt16(ddlSession.SelectedValue) +
                                " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"]), "COURSENO");


                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    int courseno = Convert.ToInt16(ds.Tables[0].Rows[i]["COURSENO"].ToString());
                                    int grpNo1 = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(GROUPNO,0)",
                                        "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + courseno +
                                        " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"])));

                                    if (grpNo == grpNo1)
                                        totalElectCrs += Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "1",
                                            "ISNULL(ELECT,0)=1 AND ISNULL(GLOBALELE,0)=0 AND COURSENO=" + courseno + " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"])));
                                }
                            }

                            //choiceFor = Convert.ToInt16(objCommon.LookUp("ACD_ELECTGROUP", "ISNULL(CHOICEFOR,0)", "GROUPNO=" + grpNo));
                        }
                        catch (Exception EX) { }



                        if (grpNo > 0)
                        {
                            //int IsTrue = Convert.ToInt16(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(IS_SELECT_CHOICEFOR_OF_ELECT_CRS_FROM_CRDIT_DEFINITION_PAGE,0)", ""));

                            //if (IsTrue == 0)
                            //    choiceFor = Convert.ToInt16(objCommon.LookUp("ACD_ELECTGROUP", "ISNULL(CHOICEFOR,0)", "GROUPNO=" + grpNo));
                            //else
                            try
                            {
                                choiceFor = Convert.ToInt16(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT", "ISNULL(ELECTIVE_CHOISEFOR,0)", "ELECTIVE_GROUPNO=" + grpNo + " AND SCHEMENO=" + Convert.ToInt16(ViewState["schemeno"]) + " AND TERM=" + Convert.ToInt16(ddlSemester.SelectedValue)));
                            }
                            catch
                            {
                                choiceFor = 0;
                                objCommon.DisplayMessage(updBulkReg, "Please Define Choices for selected Elective Group", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updBulkReg, "Please Define Choices for selected Elective Group", this.Page);
                            return;
                        }

                        if (totalElectCrs > 0 && grpNo > 0 && choiceFor > 0 && totalElectCrs >= choiceFor)
                            count = 1;
                    }
                    else
                    {
                        if (globalcreditregistred >= Convert.ToDouble(lblTotalCredit.Text))
                        {
                            objCommon.DisplayMessage(updBulkReg, "The Total Global Credits limits is " + lblTotalCredit.Text + ", Student Already Registered Global Credit is " + globalcreditregistred + ", Kindly check the course credits", this.Page);
                            return;
                        }
                    }
                }
            }

            if (count > 0)
            {
                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {
                    CheckBox chkAccept = dataitem.FindControl("cbRow") as CheckBox;
                    if (!string.IsNullOrEmpty(idnos) && idnos.Contains(chkAccept.ToolTip) && chkAccept.Enabled)
                        chkAccept.Checked = false;
                }

                objCommon.DisplayMessage(updBulkReg, "You can select only " + choiceFor + " course for same group.!", this.Page);
                return;
            }
        }
    }   
}
