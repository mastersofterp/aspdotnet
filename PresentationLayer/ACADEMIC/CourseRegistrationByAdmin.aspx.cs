//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE REGISTRATION BY ADMIN                                    
// CREATION DATE : 24-JUNE-2015
// ADDED BY      : MR. MANISH WALDE
// ADDED DATE    : 
// MODIFIED BY   : PRITY KHANDAIT
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;

public partial class ACADEMIC_CourseRegistrationByAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    bool flag = false;

    #region Page Load
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;


                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                ////Check for Activity On/Off for course registration.
                //if (CheckActivity())
                //{

                ViewState["action"] = "add";
                ViewState["idno"] = "0";

                ///if (Session["usertype"].ToString().Equals("1"))     //Only Admin 
                //if (Session["dec"].ToString().Equals("1") || Session["usertype"].ToString().Equals("1"))
                //if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1"))   //Only Admin & HoD Added on 25/07/2017 // Or Faculty with HOD Rights
                //{

                if((Session["usertype"].ToString() != "2"))
                {
                    divOptions.Visible = false;
                    divCourses.Visible = true;
                    divpnlSearch.Visible = true;
                }
                else
                {
                    //if (CheckActivity())
                    //{
                        this.ShowDetails();
                        divpnlSearch.Visible = false;
                        divCourses.Visible = true;
                    //}
                    //objCommon.DisplayMessage("You are Not Authorized to View this Page. Contact Admin.", this.Page);
                }
                //}
                //else
                //{
                //    divCourses.Visible = true;
                //    divOptions.Visible = false;
                //}
            }
            DivMultipleSelect.Visible = false;
            if (Session["usertype"].ToString() != "1")
                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
            else
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(updReg, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(updReg, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(updReg, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            StudentRegist objSR = new StudentRegist();

            objSR.EXAM_REGISTERED = 0;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            }
            foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            }
            if (objSR.COURSENOS == null)
            {
                objSR.COURSENOS = "0";
                flag = 1;
            }
            else
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
            //if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0 || objSR.Audit_course.Length > 0)
            //{
            //Add registered 
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = lblRegNo.Text.Trim();
            objSR.ROLLNO = lblEnrollNo.Text.Trim();

            int ret = objSReg.InsertStudentRegistrationByAdmin(objSR);
            if (ret == 1)
            {
                if (flag == 1)
                {
                    objCommon.DisplayMessage(updReg, "Subject Removal Done Successfully!!", this.Page);
                    btnPrintRegSlip.Enabled = false;
                }
                else
                {
                    objCommon.DisplayMessage(updReg, "Subject Registration Successfully. Print the Registration Slip.", this.Page);
                    btnPrintRegSlip.Enabled = true;
                }

                txtRollNo.Enabled = false;
            }
            else
                objCommon.DisplayMessage(updReg, "Registration Failed! Error in saving record.", this.Page);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage(updReg, "Student Not Found for Entered Enrollment No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage(updReg, "Student with Enrollment No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            ////if (Session["usertype"].ToString().Equals("1"))
            //if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1"))     //Admin & HoD Added on 25/07/2017
            //{ //Admin & HoD Added on 25/07/2017

            if((Session["usertype"].ToString() != "2"))    
            {
                this.ShowDetails();
                ///Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    // objCommon.DisplayMessage(updReg, "Student with Enrollment No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                    objCommon.DisplayMessage(updReg, "Student with Enrollment No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session.", this.Page);
                    btnSubmit.Enabled = false;
                    btnPrintRegSlip.Enabled = false;
                    //return;
                }
                BindStudentDetails();
                txtRollNo.Enabled = false;
                ddlSession.Enabled = false;
                //ddlCollege.Enabled = false;
                rblOptions.Enabled = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        ddlSession.Enabled = true;
        //ddlCollege.Enabled = true;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        rblOptions.Enabled = true;

        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        lvAuditSubjects.DataSource = null;
        lvAuditSubjects.DataBind();

        tblInfo.Visible = false;
        lblmsg.Text = string.Empty;
        ddlSession.SelectedIndex = 0;
        //ddlSession.Items.Clear();
        //ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //Response.Redirect(Request.Url.ToString());
    }

    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCollege.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
    //        ddlSession.Focus();
    //    }
    //    else
    //    {
    //        ddlSession.Items.Clear();
    //        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    //    }
    //}

    #region Private Methods

    private void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "ISNULL(ActiveStatus,0) =1 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID=CM.COLLEGE_ID)", "(SESSIONNO)", "CONCAT(COLLEGE_NAME,' - ',SESSION_PNAME)", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND FLOCK=1", "SESSIONNO DESC");
        AcademinDashboardController objADEController = new AcademinDashboardController();
        DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
        ddlSession.Items.Clear();
        ddlSession.Items.Add("Please Select");
        ddlSession.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSession.DataSource = ds;
            ddlSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSession.DataTextField = ds.Tables[0].Columns[4].ToString();
            ddlSession.DataBind();
            ddlSession.SelectedIndex = 0;
        }
        this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
    }

    private void ShowDetails()
    {
        int idno = 0;
        int sessionno = 0;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            sessionno = Convert.ToInt32(Session["currentsession"]);

        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "8")
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        try
        {
            DataSet dsStudent = objSReg.GetStudInfoForCourseRegi(idno, sessionno);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    lblRegNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    ViewState["CLG_ID"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

                    //ViewState["PREV_STATUS"] = dsStudent.Tables[0].Rows[0]["ISREGULAR"].ToString();
                    tblInfo.Visible = true;
                    divCourses.Visible = true;
                    if (lblScheme.ToolTip == "0" || lblScheme.ToolTip == "")
                    {
                        objCommon.DisplayMessage(updReg, "Scheme is not alloted for searched student!!", this.Page);
                    }

                    if (ViewState["usertype"].ToString() == "2")
                    {
                        BindStudentDetails();
                    }


                }
                else
                {
                    objCommon.DisplayMessage(updReg, "Subject Registration Not Found for this Student!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updReg, "Subject Registration Not Found for this Student!!", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindStudentDetails()
    {
        int idno = 0;
        int sessionno = 0;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            sessionno = Convert.ToInt32(Session["currentsession"]);

        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "8")
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        BindAvailableCourseList();


        BindStudAppliedCourseList();

        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "IDNO", "SESSIONNO,DBO.FN_DESC('SESSIONNAME',SESSIONNO)SESSIONNAME", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND (EXTERMARK='-1.00' OR EXTERMARK='-2.00') AND IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO <" + sessionno, "");
        //if (ds != null & ds.Tables[0].Rows.Count > 0)
        //{
        //    lblmsg.Text = "Student with Enrollment No." + lblEnrollNo.Text.Trim() + " Was Detained or Absent in " + Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNAME"]) + " Session For (" + lblSemester.Text + ") semester registration in one or More Subjects.But For Special Cases You Can Directly Register Him.";
        //    BindAvailableCourseList();

        //    if (lvCurrentSubjects.Visible == true || lvBacklogSubjects.Visible == true || lvAuditSubjects.Visible == true)
        //    {
        //        btnSubmit.Enabled = true;
        //    }
        //    BindStudAppliedCourseList();
        //}
        //else
        //{
        //    flag = true;
        //    objCommon.DisplayMessage(updReg, "Subject Not offered for this Student!!", this.Page);
        //    //Commented by Rita M. on date 23/10/2019..........
        //    //string CountEligibility = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO <" + Convert.ToInt32(ddlSession.SelectedValue));
        //    //if (Convert.ToInt32(CountEligibility) == 0)
        //    //{
        //    //    BindAvailableCourseList();
        //    //}
        //    //else
        //    //{
        //    //    lblmsg.Text = "Student with Enrollment No." + lblEnrollNo.Text.Trim() + " is not eligible for current (" + lblSemester.Text + ") semester registration. Because registration for semester " + lblSemester.Text + " already exist in previous session.<br />You can register student for backlog courses if any.<br />In case of any query Please contact Programmer/Dean Academics.";
        //    //}
        //}

        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        //Check current semester registered or not  //PREV_STATUS = 0 and 
        //Bellow patch uncommented on 30.03.2022 as per discussion with umesh sir have revert the exam regisration.
        string count1 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));  // commented this exam registrerd=1 as per discussion with Umesh Ganprkar sir - 07 march 2022
        // string count1 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        //int exam_registered = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(COURSE_EXAM_REG_BOTH,0)", "")); //checking exam registered from Module Configuration.
       
        if (count1 != "0")
        {
            if (Session["OrgId"].ToString() != "5")
            {
                int org_lvl_exam_regd_flag = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG_COURSE_EXAM_REG", "COUNT(*)", "ISNULL(COLLEGE_ID,0)=0 AND ISNULL(COURSE_EXAM_REG_BOTH,0)=1"));
                int Clg_lvl_exam_regd_flag = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG_COURSE_EXAM_REG", "COUNT(*)", "ISNULL(COLLEGE_ID,0)=" + Convert.ToInt32(ViewState["CLG_ID"]) + " and ISNULL(COURSE_EXAM_REG_BOTH,0)=1"));

                int exam_registered = (org_lvl_exam_regd_flag > 0 || Clg_lvl_exam_regd_flag > 0) ? 1 : 0;

                if (exam_registered == 0)
                {
                    objCommon.DisplayMessage(updReg, "Student Exam Registration Done, You do not Modify Course Registration.", this.Page);
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
                    btnPrintRegSlip.Enabled = true;
                    return;
                }
                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnPrintRegSlip.Enabled = true;
                }
            }
        }
        else if (count != "0")
        {
            objCommon.DisplayMessage(updReg, "Student Subject Registration already done.", this.Page);
            btnSubmit.Enabled = true;
            btnSubmit.Visible = true;
            btnPrintRegSlip.Enabled = true;
        }



        if (flag.Equals(true))
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            btnSubmit.Enabled = false;
            btnPrintRegSlip.Enabled = false;
            objCommon.DisplayMessage(updReg, "Subject Not offered for this Student!!", this.Page);
        }
    }

    private void BindAvailableCourseList()
    {
        DataSet dsCurrCourses = null;
        //Show Current Semester Courses ..
        dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER, (CASE  WHEN (ISNULL(C.GLOBALELE,0)=1 AND ISNULL(C.ELECT,0)=1)  THEN 'Global Elective'  WHEN ISNULL(C.ELECT,0)=1 THEN 'Elective' ELSE 'Core' END) AS CATEGORY ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND ISNULL(COURSE_OFFERED,0)=1 AND O.SEMESTERNO = " + lblSemester.ToolTip, "C.CCODE");
        //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1", "C.CCODE");
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Enabled = true;
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCurrentSubjects);//Set label 
            lvCurrentSubjects.Visible = true;
        }
        else
        {
            btnSubmit.Enabled = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage(updReg, "No Subject found in Allotted Scheme and Semester.", this.Page);
        }
    }

    private void BindStudentFailedCourseList()
    {
        DataSet dsCurrCourses = null;
        //Show Backlog Semester Courses ..
        dsCurrCourses = objSReg.GetStudentCoursesForBacklogRegistration(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), 0);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvBacklogSubjects.DataSource = dsCurrCourses.Tables[0];
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = true;
        }
        else
        {
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
        }
    }

    #endregion

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptCourseRegSlip.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
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
            ScriptManager.RegisterClientScriptBlock(this.updReg, this.updReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //pnlLV.Visible = false;
            //divStudInfo.Visible = false;
            //divButton.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;
                        rfvDDL.Enabled = true;
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        rfvSearchtring.Enabled = true;
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }

    #region Faculty Advisor Accepting Student Registration

    private void BindStudAppliedCourseList()
    {

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataSet dsOfferedCourses = null;
            string sessionNo = string.Empty;
            dsOfferedCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT SR", "DISTINCT SR.COURSENO", "SR.CCODE, SR.SEMESTERNO, ISNULL(SR.EXAM_REGISTERED,0) AS EXAM_REGISTERED, ISNULL(SR.REGISTERED,0) AS REGISTERED", "ISNULL(SR.CANCEL,0) = 0 AND SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.IDNO = " + ViewState["idno"], "SR.CCODE");

            if (dsOfferedCourses != null)
            {
                if (dsOfferedCourses.Tables.Count > 0 && dsOfferedCourses.Tables[0].Rows.Count > 0)
                {
                    ListOperations(lvCurrentSubjects, dsOfferedCourses.Tables[0]);
                    ListOperations(lvBacklogSubjects, dsOfferedCourses.Tables[0]);
                }
            }
            else
            {
                flag = true;
                objCommon.DisplayMessage(updReg, "Subject Not offered for this Student!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ListOperations(ListView list, DataTable dt)
    {
        foreach (ListViewDataItem item in list.Items)
        {
            CheckBox cbHead = list.FindControl("cbHead") as CheckBox;
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string lblCCode = (item.FindControl("lblCCode") as Label).ToolTip;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (lblCCode == dt.Rows[i]["courseno"].ToString())
                {
                    if (dt.Rows[i]["REGISTERED"].ToString() == "1")
                    {
                        CheckBox cbHeadReg = list.FindControl("cbHeadReg") as CheckBox;
                        CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                        chkRegister.Checked = true;
                        cbHeadReg.Checked = true;
                    }
                }
            }
        }
    }

    protected void btnBackHOD_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        btnShow.Visible = false;
        divCourses.Visible = false;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        txtRollNo.Text = string.Empty;
        lblAdmBatch.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblMotherName.Text = string.Empty;
        lblName.Text = string.Empty;
        lblPH.Text = string.Empty;
        lblScheme.Text = string.Empty;
        lblSemester.Text = string.Empty;
        rblOptions.Enabled = true;
    }

    #endregion

    protected void btnSearchCriteria_Click(object sender, EventArgs e)
    {
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        lvAuditSubjects.DataSource = null;
        lvAuditSubjects.DataBind();
        tblInfo.Visible = false;
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
    }

    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnCloseCriteria_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        //tblInfo.Visible = false;
       
        if (ddlSession.SelectedIndex > 0)
        {
            pnlLV.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            LinkButton lnk = sender as LinkButton;
            string idno = lnk.CommandArgument;
            //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            txtRollNo.Text = lblenrollno.Text;
            if (idno == "")
            {
                objCommon.DisplayMessage(updReg, "Student Not Found for Entered Enrollment No.[" + txtRollNo.Text.Trim() + "]", this.Page);
            }
            else
            {
                ViewState["idno"] = idno;

                if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
                {
                    objCommon.DisplayMessage(updReg, "Student with Enrollment No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }
                ////if (Session["usertype"].ToString().Equals("1"))
                //if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1"))     //Admin & HoD Added on 25/07/2017
                //{
                if ((Session["usertype"].ToString() != "2"))
                {
                    this.ShowDetails();
                    ///Check current semester applied or not
                    string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    if (applyCount == "0")
                    {
                        // objCommon.DisplayMessage(updReg, "Student with Enrollment No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                        objCommon.DisplayMessage(updReg, "Student with Enrollment No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session.", this.Page);
                        btnSubmit.Enabled = false;
                        btnPrintRegSlip.Enabled = false;
                        //return;
                    }

                    BindStudentDetails();

                    txtRollNo.Enabled = false;
                    ddlSession.Enabled = false;
                    //ddlCollege.Enabled = false;
                    rblOptions.Enabled = false;
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(updReg, "Please Select School/Institute & Session", this.Page);
            return;
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DivMultipleSelect.Visible = false;
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            lboOfferCourse.Items.Clear();
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

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlBulkSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    ddlBulkSession.Focus();
                }
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlBulkSession.Items.Clear();
                ddlBulkSession.Items.Add(new ListItem("Please Select", "0"));
                ddlAdmBatch.Items.Clear();
                ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
                //objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
                ddlClgname.Focus();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlBulkSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
             DivMultipleSelect.Visible = false;
             lboOfferCourse.Items.Clear();
             lvStudentBulk.DataSource = null;
             lvStudentBulk.DataBind();
            if (ddlBulkSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                ddlAdmBatch.Focus();
            }
            else
            {
                //lvCourse.DataSource = null;
                // lvCourse.DataBind();
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lvStudentsRemain.DataSource = null;
                lvStudentsRemain.DataBind();
                //  pnlCourses.Visible = false;
                pnlStudents.Visible = false;
                btnBulkSubmit.Enabled = false;
                ddlBulkSession.Items.Clear();
                ddlBulkSession.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlAdmBatch.Items.Clear();
                ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
                btnBulkSubmit.Enabled = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DivMultipleSelect.Visible = false;
            lboOfferCourse.Items.Clear();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            pnlStudents.Visible = false;
         
            if (ddlAdmBatch.SelectedIndex > 0)
            {
             

                // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
                int SessionNo = Convert.ToInt32(ddlBulkSession.SelectedValue);
                DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlSemester.DataSource = ds;
                    ddlSemester.DataTextField = "SEMESTERNAME";
                    ddlSemester.DataValueField = "SEMESTERNO";
                    ddlSemester.DataBind();
                }
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SES ON(SES.ODD_EVEN = SM.ODD_EVEN)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SES.SESSIONNO =" + ddlSession.SelectedValue, "SM.SEMESTERNO");
                ddlSemester.Focus();
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DivMultipleSelect.Visible = false;
            lboOfferCourse.Items.Clear();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            pnlStudents.Visible = false;
         

            if (ddlSemester.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
                //{
                //    objCommon.DisplayMessage("Please Select Programme/Branch/Scheme", this.Page);
                //    return;
                //}
                //else
                //{

                int countsection = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT ST WITH (NOLOCK) INNER JOIN  ACD_SECTION  S WITH (NOLOCK) ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT COUNT(ISNULL(S.SECTIONNO,0))", "ST.Schemeno = " + ViewState["schemeno"] + " AND ST.Semesterno= " + ddlSemester.SelectedValue));

                if (countsection > 0)
                {
                    ddlSection.Focus();
                    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST WITH (NOLOCK) INNER JOIN  ACD_SECTION  S WITH (NOLOCK)ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + ViewState["schemeno"] + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
                }
                else
                {
                    objCommon.DisplayMessage(updBulkReg, "Please Allot Section for Selection Criteria...", this.Page);
                    ddlSection.Items.Clear();
                    ddlSection.Items.Add(new ListItem("Please Select", "0"));
                    return;
                }

                //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
                //this.BindListView();
                //}
            }
            else
            {
                ddlSection.SelectedIndex = 0;
                //   lvCourse.DataSource = null;
                //    lvCourse.DataBind();
                lvStudentBulk.DataSource = null;
                lvStudentBulk.DataBind();
                lvStudentsRemain.DataSource = null;
                lvStudentsRemain.DataBind();
                //    pnlCourses.Visible = false;
                pnlStudents.Visible = false;
                pnlStudentsReamin.Visible = false;
                btnBulkSubmit.Enabled = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                //DivMultipleSelect.Visible = true;
                this.BindListView();
                //this.MultipleSelectDropDown();
            }
            else
            {
                //  lvCourse.DataSource = null;
                // lvCourse.DataBind();
                lvStudentBulk.DataSource = null;
                lvStudentBulk.DataBind();
                lvStudentsRemain.DataSource = null;
                lvStudentsRemain.DataBind();
                // pnlCourses.Visible = false;
                pnlStudents.Visible = false;
                pnlStudentsReamin.Visible = false;
                btnBulkSubmit.Enabled = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnBulkCancel_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }
    protected void ClearAllFields()
    {
        // pnlCourses.Visible = false;

        pnlStudents.Visible = false;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlAdmBatch.Items.Clear();
        ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlBulkSession.Items.Clear();
        ddlBulkSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlClgname.SelectedIndex = 0;
        btnBulkSubmit.Enabled = false;
        DivMultipleSelect.Visible = false;
        //btnBulkReport.Visible = false;
    }

    protected void clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        txtTotStud.Text = string.Empty;
        lvStudentBulk.DataSource = null;
        lvStudentBulk.DataBind();
        // lvCourse.DataSource = null;
        // lvCourse.DataBind();
        // pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
    }

    private void BindListView()
    {
        //Get student list as per scheme & semester & secion(AND SECTIONNO = " + ddlSection.SelectedValue)
        // StudentController objSC = new StudentController();
        DataSet ds = null;
       
        //ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        ds = objSReg.GetStudentsForSchemeModifyBulk(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBulkSession.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.MultipleSelectDropDown();
                lvStudentBulk.DataSource = ds.Tables[0];
                lvStudentBulk.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentBulk);//Set label 
                pnlStudents.Visible = true;
                btnBulkSubmit.Enabled = true;
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    //btnBulkReport.Visible = true;
                }
                hftot.Value = ds.Tables[0].Rows.Count.ToString();

                //for getting student list semester wise
                foreach (ListViewDataItem item in lvStudentBulk.Items)
                {
                    CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                    String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                    if (lblReg == "1")  //IF STUDENT IS ALREADY REGISTERED THEN CHECKBOX WILL BE DISABLED
                    {
                        chkBox.Enabled = true;
                        //chkBox.Checked = true;
                        //  chkBox.BackColor = System.Drawing.Color.Green;
                    }
                    lblReg = string.Empty;
                }
                DivMultipleSelect.Visible = true; 
                //for getting courses semester wise
                //  DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO=C.COURSENO)", "C.COURSENO", "C.CCODE,C.COURSE_NAME,ISNULL(C.ELECT,0) ELECT", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND O.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SESSIONNO=" + ddlBulkSession.SelectedValue + " AND OFFERED = 1 AND ISNULL(C.ELECT,0)=0", "C.COURSENO");
                //if (dsCourse != null && dsCourse.Tables.Count > 0)
                //{
                //    if (dsCourse.Tables[0].Rows.Count > 0)
                //    {
                //        lboOfferCourse.DataSource = dsCourse;
                //        lboOfferCourse.DataBind();
                //     //   objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lboOfferCourse);//Set label 
                //     //   pnlCourses.Visible = true;
                //        lboOfferCourse.Visible = true;
                //    }
                //    else
                //    {
                //        lboOfferCourse.DataSource = null;
                //        lboOfferCourse.DataBind();
                //        lboOfferCourse.Visible = false;
                //    }
                //}
                //else
                //{
                //    lboOfferCourse.DataSource = null;
                //    lboOfferCourse.DataBind();
                //    lboOfferCourse.Visible = false;
                //}
            }
            else
            {
                objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
                lvStudentBulk.DataSource = null;
                lvStudentBulk.DataBind();
                btnBulkSubmit.Enabled = false;
                pnlStudents.Visible = false;
                DivMultipleSelect.Visible = false; 
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            pnlStudents.Visible = false;
            btnBulkSubmit.Enabled = false;
            DivMultipleSelect.Visible = false; 
        }
    }

    private void MultipleSelectDropDown()
    {
        try
        {
            // DataSet dsCourse = objCommon.FillDropDown("", , , , "C.COURSENO");
            // objCommon.FillListBox(lboOfferCourse, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO=C.COURSENO)","C.COURSENO","C.COURSE_NAME,ISNULL(C.ELECT,0) ELECT","C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND O.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SESSIONNO=" + ddlBulkSession.SelectedValue + " AND OFFERED = 1 AND ISNULL(C.ELECT,0)=0", "C.COURSENO");
            //DivMultipleSelect.Visible = true;

            DataSet dsCourse = objSReg.GetOfferedCourseListForModifyBulkCourseRegistration(Convert.ToInt32(ddlBulkSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));

            if (dsCourse != null && dsCourse.Tables.Count > 0)
            {
                if (dsCourse.Tables[0].Rows.Count > 0)
                {
                    lboOfferCourse.DataSource = dsCourse;
                    lboOfferCourse.DataTextField = "COURSE_NAME";
                    lboOfferCourse.DataValueField = "COURSENO";
                    lboOfferCourse.DataBind();
                    DivMultipleSelect.Visible = true;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    //protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    Label elective = (e.Item.FindControl("lblCourseName")) as Label;
    //    if (elective.ToolTip == "False")
    //    {
    //        ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
    //    }
    //    else
    //    {
    //        ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
    //    }
    //}

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO IN ( " + Session["userdeptno"].ToString() + " )", "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
            }

            //  lvCourse.DataSource = null;
            // lvCourse.DataBind();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            //  pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnBulkSubmit.Enabled = false;

            ddlDegree.Focus();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSchm.Items.Clear();
            ddlSchm.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string uano = Session["userno"].ToString();
            string uatype = Session["usertype"].ToString();
            string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranch.Items.Clear();
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "A.LONGNAME");
                else
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

                ddlBranch.Focus();
                ddlSchm.Items.Clear();
                ddlSchm.Items.Add(new ListItem("Please Select", "0"));
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
                ddlSchm.Items.Clear();
                ddlSchm.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }

            // lvCourse.DataSource = null;
            // lvCourse.DataBind();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            //  pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnBulkSubmit.Enabled = false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                ddlSchm.Items.Clear();
                objCommon.FillDropDownList(ddlSchm, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
                ddlSchm.Focus();
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSchm.Items.Clear();
                ddlSchm.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            //  lvCourse.DataSource = null;
            // lvCourse.DataBind();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            // pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnBulkSubmit.Enabled = false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlSchm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchm.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SES ON(SES.ODD_EVEN = SM.ODD_EVEN)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + ddlSchm.SelectedValue + " AND SES.SESSIONNO = " + ddlSession.SelectedValue, "SM.SEMESTERNO");
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
            //lvCourse.DataSource = null;
            // lvCourse.DataBind();
            lvStudentBulk.DataSource = null;
            lvStudentBulk.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            // pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnBulkSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnBulkSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            int ret = 0;
            int scount = 0;
            StudentRegist objSR = new StudentRegist();

            objSR.EXAM_REGISTERED = 0;

            foreach (ListViewDataItem dataitem in lvStudentBulk.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                {
                    int count = 0;
                   
                    foreach (ListItem Item in lboOfferCourse.Items)
                    {
                        if (Item.Selected)
                        {
                            objSR.COURSENOS += Item.Value + "$";
                            count++;

                        }
                    }

                    //if (objSR.COURSENOS == null)
                    //{
                    //    objSR.COURSENOS = "0";
                    //    flag = 1;
                    //}
                    //else
                    //{
                    //    objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                    //}
                    //objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
                    //if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0 || objSR.Audit_course.Length > 0)
                    //{

                    //Add registered 

                    objSR.SESSIONNO = Convert.ToInt32(ddlBulkSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
                    objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
                    objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                    objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
                    objSR.IPADDRESS = Session["ipAddress"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.REGNO = lblRegNo.Text.Trim();
                    objSR.ROLLNO =((dataitem.FindControl("lblRollNoForbulk")) as Label).Text;

                    ret = objSReg.AddRegisteredSubjectsModifyBulk(objSR);
                    scount++;
                    //ViewState["ret"] = ret;
                    //if (ret == 1)
                    //{
                    //    if (flag == 1)
                    //    {
                    //        objCommon.DisplayMessage(updReg, "Subject Removal Done Successfully!!", this.Page);
                    //        btnPrintRegSlip.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        objCommon.DisplayMessage(updReg, "Subject Registration Successfully.", this.Page);
                    //        btnPrintRegSlip.Enabled = true;
                    //    }
                    //    txtRollNo.Enabled = false;

                    //}
                    //else
                    //    objCommon.DisplayMessage(updReg, "Registration Failed! Error in saving record.", this.Page);
                    
                }
            }
            if (scount > 0)
            {
                objCommon.DisplayMessage(this.Page, "Subject Registration Successfully.", this.Page);
            }
           
        }
        catch (Exception ex)
        {
            throw;
        }
            

       
        








        //string roll = string.Empty;
        //StudentRegistration objSRegist = new StudentRegistration();
        //StudentRegist objSR = new StudentRegist();
        ////CheckBox cbRow = new CheckBox();

        //try
        //{
        //    foreach (ListViewDataItem dataitem in lvStudentBulk.Items)
        //    {
        //        //Get Student Details from lvStudent
        //        CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
        //        if (cbRow.Checked == true)
        //        {
        //            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        //            objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
        //            objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
        //            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
        //            objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
        //            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        //            objSR.COLLEGE_CODE = Session["colcode"].ToString();
        //            objSR.UA_NO = Convert.ToInt32(Session["userno"]);

        //            int count = 0;

        //            foreach (ListItem Item in lboOfferCourse.Items)
        //            {
        //                if (Item.Selected)
        //                {
        //                    objSR.COURSENOS += Item.Value + ",";
        //                    count++;
                           
        //                }
        //            }

        //            //Get Course Details
        //            //foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
        //            //{
        //            //    if (((dataitemCourse.FindControl("cbRow")) as CheckBox).Checked == true)
        //            //    {
        //            //        objSR.COURSENOS += ((dataitemCourse.FindControl("lblCCode")) as Label).ToolTip + "$";
        //            //        Label elective = (dataitemCourse.FindControl("lblCourseName")) as Label;
        //            //        if (elective.ToolTip == "False")
        //            //        {
        //            //            objSR.ELECTIVE += "0" + "$";
        //            //        }
        //            //        else
        //            //        {
        //            //            objSR.ELECTIVE += "1" + "$";
        //            //        }
        //            //    }
        //            //}

        //            objSR.ACEEPTSUB = "1";

        //            //Register Single Student
        //            //int prev_status = 0;    //Regular Courses
        //            //int seatno = 0;

        //            CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsModifyBulk(objSR);
        //            if (cs.Equals(CustomStatus.RecordSaved))
        //            {
        //                objSR.COURSENOS = string.Empty;
        //                objSR.ACEEPTSUB = string.Empty;
        //            }
        //        }
        //    }
        //    objCommon.DisplayMessage(updBulkReg, "Student(s) Registered Successfully!!", this.Page);
        //    clear();
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}
    }

}


