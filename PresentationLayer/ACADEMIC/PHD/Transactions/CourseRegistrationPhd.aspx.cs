
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

public partial class ACADEMIC_CourseRegistrationPhd : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    int retCnt; int semcount;
    StudentController objStudent = new StudentController();

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
        //********************************
        string Sessionexam = string.Empty;
        //********************************

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
                //this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //**************************************************************************
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;
                Sessionexam = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'PhdCourseR' AND STARTED=1");
                if (Sessionexam != "")
                {
                    ViewState["Sessionexam"] = Sessionexam;
                }
                else
                {
                    ViewState["Sessionexam"] = Session["currentsession"].ToString();
                }
                //if (ViewState["usertype"].ToString() == "3")   // comment because session chk at show btn 
                //{
                //    CheckActivity();
                //}
                //*************************************************************************

                this.PopulateDropDownList();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));


                ViewState["usertype"] = ua_type;
                ViewState["ua_dec"] = ua_dec;
                if (ViewState["usertype"].ToString() == "2")
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SESSIONNO=B.SESSIONNO", "DISTINCT B.SESSIONNO", "A.SESSION_NAME", "B.SESSIONNO>0 AND B.IDNO = " + Convert.ToInt32(Session["idno"].ToString()), "B.SESSIONNO DESC");

                    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND B.IDNO = " + Convert.ToInt32(Session["idno"].ToString()), "B.SEMESTERNO");

                    ddlSem.SelectedIndex = 1;
                    ShowDetails();
                }
                //added on 30072018dd
                if (ua_type == "1" || ua_type == "4")
                {
                    if (Convert.ToString(Session["userpreviewidC"]) != string.Empty)
                    {
                        ddlSession.SelectedValue = Session["prvcoursesession"].ToString();
                        ddlSem.SelectedValue = Session["prvcoursesem"].ToString();
                        ViewState["Sessionexam"] = Session["prvcoursesession"].ToString();

                        ShowDetails();

                    }
                }
            }
            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }
        divCourses.Visible = true;
        //ddlSession.SelectedIndex = 0;
        //txtRollNo.Text = string.Empty;
        // PopulateDropDownList();
        Session["userpreviewidC"] = null;
        divMsg.InnerHtml = string.Empty;
    }

    //**********************
    private void CheckActivity()
    {
        int sessionno = 0;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSIONNO > 0 AND FLOCK=0 AND ODD_EVEN=2 AND EXAMTYPE=3");
        sessionno = Convert.ToInt32(ViewState["Sessionexam"]);
        sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin!!", this.Page);
                pnlMain.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlMain.Visible = false;
            }

        }
        else
        {
            //objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            objCommon.DisplayMessage("this Activity has been Stopped. So You Can View Details and Download Certificate .", this.Page);
            pnlMain.Visible = false;
            if (ViewState["usertype"].ToString() == "3")
            {

                pnlMain.Visible = true;
                ControlActivityOff();
                // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND B.IDNO = " + Convert.ToInt32(Session["idno"].ToString()), "B.SEMESTERNO");

                // ddlSem.SelectedIndex = 1;
                string status = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SUPERVISORSTATUS = 1 AND DRCSTATUS = 1 AND DEANSTATUS = 1 AND JOINTSUPERVISORSTATUS = 1 AND INSTITUTEFACULTYSTATUS = 1");

                if (Convert.ToInt32(status.ToString()) >= 1)
                {

                    btnPrintRegSlip.Visible = true;
                    btnConfirmed.Visible = false;
                    tr1.Visible = true;

                }
                else
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SESSIONNO=B.SESSIONNO", "DISTINCT B.SESSIONNO", "A.SESSION_NAME", "B.SESSIONNO>0 AND FLOCK = 0 AND EXAMTYPE=1 AND B.IDNO = " + Convert.ToInt32(lblName.ToolTip), "B.SESSIONNO DESC");
                    ddlSession.Enabled = true;
                    tr1.Visible = false;
                    btnPrintRegSlip.Visible = false;
                    btnConfirmed.Visible = true;
                }
            }
            if (ViewState["usertype"].ToString() == "2")
            {
                // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                string ActiveIdno = objCommon.LookUp("ACD_PHD_DGC", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                if (ActiveIdno != string.Empty)
                {
                    pnlMain.Visible = true;

                    ControlActivityOff();

                }
            }

        }
        dtr.Close();
    }
    //**********************
    public void ControlActivityOff()
    {
        ddlSemester.Enabled = txtRemark.Enabled = ddlType.Enabled = ddlOfferedCourse.Enabled = false;
        ddlSession.Enabled = true;
        btnSubmit.Visible = false;
        btnConfirmed.Visible = false;
        btnReject.Visible = false;

        foreach (RepeaterItem item in lvHistory.Items)
        {
            ImageButton btndel = item.FindControl("btnDelete") as ImageButton;
            btndel.Visible = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        string Degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "ROLLNO = '" + txtRollNo.Text.Trim() + "'");
        if (Degreeno == "6")
        {
            //**************************
            string status = string.Empty;
            DataSet ds = null;
            ds = objStudent.RetreiveBlockStudentInfo(txtRollNo.Text.Trim());
            bool flag = false;
            if (ds.Tables[0].Rows.Count <= 0)
            {
                flag = true;
            }
            else
            {
                if (ds.Tables[0].Rows[0]["STATUS1"].ToString() == "0")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            //*********************

            if (flag == true)
            {

                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <= (CASE WHEN (SELECT PHDSTATUS FROM ACD_STUDENT WHERE ROLLNO ='" + txtRollNo.Text.Trim() + "') = 1 THEN 5 ELSE 3 END) AND SEMESTERNO > 0", "SEMESTERNO");
                this.ShowDetails();
                // ddlSession.Enabled = false;
                //=========== add activity condition if activity then display only report  ============> added by dipali on  24072018
                if (ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "2")
                {

                    CheckActivity();

                }
                //======================================== ENd =================================================================

            }
            else
            {
                objCommon.DisplayMessage("Student having roll no. " + txtRollNo.Text.Trim() + " has been blocked because " + ds.Tables[0].Rows[0]["Remark"].ToString(), this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Enter PHD Student RollNo.", this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string exem = string.Empty;
        exem = ddlExem.SelectedValue;
        string type = string.Empty;
        type = ddlType.SelectedValue;

        //---   only take 1 Laboratry entire phd -- ordinance  -- 28092018   -- lab- 2 
        if (ddlType.SelectedValue == "2")
        {
            int LabType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + "AND FAC_REMARK =" + Convert.ToInt32(ddlType.SelectedValue)));
            if (LabType >= 1)
            {
                objCommon.DisplayMessage("Student Can Take Only One Laboratory Type In Entire PhD ", this.Page);
                return;
            }
            else { }
        }

        //---   only take 2 Seminar In entire phd -- ordinance  -- 28092018   -- seminar - 6
        if (ddlType.SelectedValue == "6")
        {
            int SeminarType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + "AND FAC_REMARK =" + Convert.ToInt32(ddlType.SelectedValue)));
            if (SeminarType >= 2)
            {
                objCommon.DisplayMessage("Student Can Take Only Two Seminar In Entire PhD ", this.Page);
                return;
            }
            else { }
        }

        //---   only take 2 Seminar In entire phd -- ordinance  -- 28092018   -- seminar - 6
        if (ddlType.SelectedValue == "3")
        {
            int SelfType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + "AND FAC_REMARK =" + Convert.ToInt32(ddlType.SelectedValue)));
            if (SelfType >= 2)
            {
                objCommon.DisplayMessage("Student Can Take Only Two Self Study In Entire PhD", this.Page);
                return;
            }
            else
            {
                SelfType = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + "AND FAC_REMARK =" + Convert.ToInt32(ddlType.SelectedValue) + "AND SEMESTERNO =" + ddlSem.SelectedValue));
                if (SelfType >= 1)
                {
                    objCommon.DisplayMessage("Student Can Take Only One Self Study In One Semester", this.Page);
                    return;
                }
                else { }
            }
        }
        //-------------------end -----------------------------------------------//


        if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 0 && Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 1)
        {
            //Add registered 
            StudentRegist objSR = new StudentRegist();
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = txtRollNo.Text.Trim();
            objSR.ROLLNO = txtRollNo.Text.Trim();
            objSR.COURSENOS = ddlOfferedCourse.SelectedValue;
            double credits = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + ddlOfferedCourse.SelectedValue + " AND ISNULL(COURSE_STATUS,0) = 1"));
            objSR.CREDITS = Convert.ToInt32(credits);

            int ret = objSReg.AddAddlRegisteredSubjectsPhd(objSR, exem, type);
            if (ret > 0)
            {
                objCommon.DisplayMessage("Course Registration is successfull.", this.Page);
                //ShowReport("RegistrationSlip", "rptPreRegslip.rpt");
                //Response.Redirect(Request.Url.ToString());
            }
            ShowDetails();

        }
        else
        {
            StudentRegist objSR = new StudentRegist();
            //Add the new course for the Phd 

            if (Convert.ToInt32(ddlSelectCourse.SelectedValue) > 0)
            {
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlSelectCourse.SelectedValue + " AND ISNULL(COURSE_STATUS,0) = 1");
                int cnt = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COUNT(COURSENO)", "CCODE='" + ccode + "' AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND ISNULL(COURSE_STATUS,0) = 1"));
                if (cnt > 0)
                {
                    objCommon.DisplayMessage("Already present this course", this.Page);
                }
                else
                {
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    objSR.COURSENOS = ddlSelectCourse.SelectedValue;
                    int retstatus = objSReg.AddAddCoursesForPhd(objSR);
                    //Add registered 

                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                    objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.REGNO = txtRollNo.Text.Trim();
                    objSR.ROLLNO = txtRollNo.Text.Trim();
                    string courseno = objCommon.LookUp("ACD_COURSE", "MAX(COURSENO)", "AND ISNULL(COURSE_STATUS,0) = 1");
                    objSR.COURSENOS = courseno;
                    double credits = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + courseno + " AND ISNULL(COURSE_STATUS,0) = 1"));
                    objSR.CREDITS = Convert.ToInt32(credits);
                    if ((ViewState["usertype"].ToString() == "3" && ViewState["ua_dec"] == "0") || ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "6")
                    {
                        //int ret1 = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()));
                    }
                    else
                    {
                        int ret1 = objSReg.AddAddlRegisteredSubjectsPhd(objSR, exem, type);
                        if (ret1 > 0)
                        {
                            objCommon.DisplayMessage("Course Registration is Successfull.", this.Page);
                            //ShowReport("RegistrationSlip", "rptPreRegslip.rpt");
                            //Response.Redirect(Request.Url.ToString());
                        }
                        ShowDetails();
                    }
                }
            }
        }
    }
    #region
    private void PopulateDropDownList()
    {
        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND SESSIONNO ="+Convert.ToInt32(Session["currentsession"].ToString()), "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO= "+ Convert.ToInt32(ViewState["Sessionexam"].ToString())+" AND FLOCK = 0 AND EXAMTYPE=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

        //ddlSession.SelectedIndex = 1;
        //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN SESSION_ACTIVITY A ON A.SESSION_NO=S.SESSIONNO inner join activity_master AM on (am.activity_no = a.activity_no)", "S.SESSIONNO", "S.SESSION_NAME", "AM.ACTIVITY_CODE = 'PhdCourseR' AND STARTED=1", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        //ddlSession.Focus();
        ddlSem.SelectedIndex = 0;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
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
    private void ShowDetails()
    {
        try
        {
            string idno = "0";
            string semesterNo = string.Empty;
            DataSet dsStudent = new DataSet();
            string sessionno = ddlSession.SelectedValue;
            string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO = '" + txtRollNo.Text.Trim() + "'");


             Session["userpreviewidC"] = IDNO;
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Session["idno"].ToString();
                btnConfirmed.Visible = false;
                btnReject.Visible = false;
                trremark.Visible = false;


            }
            else if (Convert.ToString(Session["userpreviewidC"]) != string.Empty)
            {
                idno = Session["userpreviewidC"].ToString();
                ViewState["ID"] = idno;

            }
            else
            {


                if (txtRollNo.Text.Trim() == string.Empty || Convert.ToInt32(ddlSession.SelectedValue) == 0)
                {
                    objCommon.DisplayMessage("Please Enter Student Roll No. and Please Select Session", this.Page);
                    return;
                }
                //btnSubmit.Visible = false;
                idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
                ViewState["ID"] = idno;

                if (string.IsNullOrEmpty(idno))
                {
                    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }
            }
            DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO", "IDNO=" + idno, "IDNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                int supervisor = Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                int uano = Convert.ToInt32(Session["userno"].ToString());
                string joinsupervisor = ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString();
                string intsitutefac = ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString();
                string drnominee = ds.Tables[0].Rows[0]["DRCNO"].ToString();
                string drchairman = ds.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString();
                if (uano == supervisor)
                {
                    string coursesupervis = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS", "IDNO=" + idno + " AND SUPERVISORSTATUS= 1");
                    if (coursesupervis == "")
                    {
                        if (supervisor.ToString() == joinsupervisor)
                        {
                            btnSubmit.Visible = true;
                            btnConfirmed.Visible = true;
                        }
                        if (supervisor.ToString() == intsitutefac)
                        {
                            btnSubmit.Visible = true;
                            btnConfirmed.Visible = true;
                        }
                        if (supervisor.ToString() == drnominee)
                        {
                            btnSubmit.Visible = true;
                            btnConfirmed.Visible = true;
                        }
                        if (supervisor.ToString() == drchairman)
                        {
                            btnSubmit.Visible = true;
                            btnConfirmed.Visible = true;
                        }
                    }
                    else if (supervisor.ToString() == drchairman || supervisor.ToString() == drnominee || supervisor.ToString() == intsitutefac || supervisor.ToString() == joinsupervisor)
                    {
                        btnSubmit.Visible = true;
                        btnConfirmed.Visible = true;
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnConfirmed.Visible = false;
                    }

                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }
            if (ViewState["usertype"].ToString() == "6")
            {
                btnReject.Visible = false;
            }
            if (ViewState["usertype"].ToString() == "4")
            {
                btnEdit.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
            }
            //check the student is present in previous session
            //int presession = Convert.ToInt32(sessionno) - 1;
            //string chkstud = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "ROLL_NO=" + txtRollNo.Text+" AND SESSIONNO="+ presession);
            //if(Convert.ToInt32(chkstud)>0)
            //{
            //check the deparment of HOD 
            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "BRANCHNO=" + branchno);
            int hoddeptno = 0;//Convert.ToInt32(Session["userdeptno"]);
            string idnonew = "3327";
            //if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            //{
            if (ViewState["usertype"].ToString() == "2")
            {
                dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) LEFT OUTER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idno, string.Empty);
            }
            else
            {
                //dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idno, string.Empty);
                dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) LEFT OUTER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idnonew, string.Empty);
            }
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = " " + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + "";
                    lblMotherName.Text = " " + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + "";
                    showRollno.Visible = false;
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    //lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    //lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    // ddlSem.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();  -- comment this line 2018
                    ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    // ADDED FOR EXTRA SUPERVISOR
                    string superrole = objCommon.LookUp("ACD_PHD_DGC", "SUPERROLE", "IDNO=" + Convert.ToInt32(lblName.ToolTip));
                    //END

                    //LOGIC FOR GENERATING REPORT OF REGISTRATION SLIP AFTER CONFIRMATION FROM SUPERVISOR,JOINTSUPERVISOR,INSTITUTEFACULTY,DRCNOMINEE,DRCCHAIRMAN,DEAN.
                    if (ViewState["usertype"].ToString() != "2")
                    {
                        int status = 0;
                        string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + idno);
                        int supe = 0, joint = 0, inst = 0, dr = 0, drcch = 0, dean = 0;
                        // ADDED FOR EXTRA SUPERVISOR
                        int secondsupe = 0;
                        //DataSet ds1 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS,DEANSTATUS", "IDNO=" + idno, "IDNO");
                        DataSet ds1 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS,DEANSTATUS,ISNULL(SECONDJOINTSUPERVISORSTATUS,0)SECONDJOINTSUPERVISORSTATUS", "IDNO=" + idno + " AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "IDNO");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            supe = ds1.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString());
                            joint = ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString());
                            inst = ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString());
                            dr = ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString());
                            drcch = ds1.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString());
                            dean = ds1.Tables[0].Rows[0]["DEANSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DEANSTATUS"].ToString());
                            secondsupe = ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString());
                        }
                        string jointoutside = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + idno);
                        string instioutside = objCommon.LookUp("ACD_PHD_DGC", "OUTINSTITUTE", "IDNO =" + idno);
                        string drcoutside = objCommon.LookUp("ACD_PHD_DGC", "OUTNOMINEE", "IDNO =" + idno);
                        if (noofdgc == "3")
                        {
                            if (instioutside != "")
                            {
                                inst = 1;
                            }
                            if (drcoutside != "")
                            {
                                dr = 1;
                            }
                            if (supe == 1 && inst == 1 && dr == 1 && drcch == 1 && dean == 1)
                            {
                                status = 1;
                            }
                        }
                        else
                        {
                            //string jointoutside = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + idno);
                            //if (jointoutside != "")
                            //{
                            //    status = Convert.ToInt32(objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + idno + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 1 AND DRCSTATUS = 1 AND DEANSTATUS = 1  AND INSTITUTEFACULTYSTATUS = 1 AND DRCCHAIRSTATUS = 1"));
                            //}
                            //status = Convert.ToInt32(objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + idno + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 1 AND DRCSTATUS = 1 AND DEANSTATUS = 1 AND JOINTSUPERVISORSTATUS = 1 AND INSTITUTEFACULTYSTATUS = 1 AND DRCCHAIRSTATUS = 1"));

                            if (jointoutside != "")
                            {
                                joint = 1;
                            }
                            if (instioutside != "")
                            {
                                inst = 1;
                            }
                            if (drcoutside != "")
                            {
                                dr = 1;
                            }
                            //if (supe == 1 && joint == 1 && inst == 1 && dr == 1 && drcch == 1 && dean == 1)
                            //{
                            //    status = 1;
                            //}
                            // ADDED FOR EXTRA SUPERVISOR 
                            if (superrole == "T")
                            {
                                if (supe == 1 && joint == 1 && inst == 1 && dr == 1 && drcch == 1 && dean == 1 && secondsupe == 1)
                                {
                                    status = 1;
                                }
                            }
                            else
                            {
                                if (supe == 1 && joint == 1 && inst == 1 && dr == 1 && drcch == 1 && dean == 1)
                                {
                                    status = 1;
                                }
                            }
                        }
                        if (status == 1)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnReject.Visible = false;
                            btnConfirmed.Visible = false;
                            tr1.Visible = true;
                        }
                        else
                        {
                            btnPrintRegSlip.Visible = false;
                            btnReject.Visible = true;
                            tr1.Visible = false;
                            btnConfirmed.Visible = true;
                            //btnConfirmed.Enabled = false;
                            //btnReject.Enabled = false;
                        }
                        int cancstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + idno + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 0 AND DRCSTATUS = 0 AND DEANSTATUS = 0 AND JOINTSUPERVISORSTATUS = 0 AND INSTITUTEFACULTYSTATUS = 0 AND DRCCHAIRSTATUS = 0"));
                       // string reason = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "REMARK", "IDNO = " + idno + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 0 AND DRCSTATUS = 0 AND DEANSTATUS = 0 AND JOINTSUPERVISORSTATUS = 0 AND INSTITUTEFACULTYSTATUS = 0 AND  DRCCHAIRSTATUS = 0");
                        if (cancstatus == 1)
                        {
                            btnPrintRegSlip.Visible = false;
                            trCancel.Visible = true;
                            //lblRemark.Text = reason;
                            //trremark.Visible = false;
                            lblReasoncan.Visible = true;
                           // lblReasoncan.Text = reason;
                            //txtRemark.Visible = false;
                            lblReasoncan.Font.Bold = true;
                            lblReasoncan.ForeColor = System.Drawing.Color.Red;
                            lblReasoncan.Font.Size = 12;
                        }
                        else
                        {
                            trCancel.Visible = false;
                        }
                    }
                    //Payment Type..
                    ddlPayType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();

                    tblInfo.Visible = true;

                    //Show Already saved in  Course table courses..
                    //***********************************************
                    //objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0", "C.COURSENO");
                    //objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 AND C.SEMESTERNO='"+ddlSemester.SelectedValue+"'", "C.COURSENO");
                    //ddlOfferedCourse.Items.Add(new ListItem("Others", "1"));
                    //ddlOfferedCourse.Items.Add(new ListItem("Add New Course", "2"));

                    //Show History of Courses..
                    DataSet dsHistCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_SEMESTER S ON (R.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = R.SESSIONNO)  LEFT OUTER JOIN ACD_COURSE CA ON(CA.COURSENO =R.COURSENO) INNER JOIN ACD_PHD_COURSE_FAC_REMARK F ON (R.FAC_REMARK = F.FAC_REMARK)", "SM.SESSIONNO,R.SEMESTERNO,R.IDNO,R.SCHEMENO", "SM.SESSION_NAME,S.SEMESTERNAME,R.COURSENO,R.CCODE,(CASE WHEN ISNULL(OFFERED,0) = 1 THEN (R.COURSENAME+' ('+'EXEMPTED'+')') WHEN ISNULL(OFFERED,0) = 1 THEN  (R.COURSENAME+'-'+'NONEXEMPTED') ELSE R.COURSENAME END)COURSENAME,F.NAME TYPE,R.GRADE,R.CREDITS", "R.IDNO = " + idno + " AND REGISTERED = 1 AND ISNULL(CA.COURSE_STATUS,0) = 1", "R.SESSIONNO,R.SEMESTERNO,R.CCODE");
                    lvHistory.DataSource = dsHistCourses.Tables[0];
                    lvHistory.DataBind();
                    divCourses.Visible = true;
                    string degreeno = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                }
                else
                {
                    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    divCourses.Visible = false;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = int.Parse(ddlSession.SelectedValue);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = int.Parse(ddlSem.SelectedValue) + 1;
            demandCriteria.PaymentTypeNo = int.Parse(ddlPayType.SelectedValue);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = int.Parse(ddlSession.SelectedValue);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = int.Parse(ddlSem.SelectedValue) + 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPayType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.ExcessAmount = 0;
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    protected void btnModifyPType_Click(object sender, EventArgs e)
    {
        try
        {
            objSReg.UpdatePaymentCategory(lblName.ToolTip, ddlPayType.SelectedValue, ddlSemester.SelectedValue);
            objCommon.DisplayMessage("Payment Category Updated Successfully!", this.Page);
            this.ShowDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnModifyPType_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {
        try
        {
            //Create Demand and Print the Challan..
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            int selectSemesterNo = Int32.Parse(ddlSem.SelectedValue);
            string studentIDs = lblName.ToolTip;

            // Check Condition Odd Even 
            string odd_even = objCommon.LookUp("ACD_SEMESTER", "ODD_EVEN", "SEMESTERNO=" + selectSemesterNo);
            string cur_odd_even = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));
            if (odd_even == cur_odd_even)
            {
                objCommon.DisplayMessage("Student does not allow to Promote semester!!", this.Page);
                return;
            }
            bool overwriteDemand = true;
            //SEMESTER PROMOTION FOR SELECTED STUDENT
            int currentSemesterNo = selectSemesterNo + 1;
            objSReg.UpdateSemesterPromotionNo(studentIDs, currentSemesterNo);
            string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + currentSemesterNo);
            if (Convert.ToInt32(demandno) <= 0)
            {
                string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, selectSemesterNo, overwriteDemand);
            }

            //Create DCR and print Challan
            string receiptno = this.GetNewReceiptNo();
            FeeDemand dcr = this.GetDcrCriteria();
            string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, selectSemesterNo, overwriteDemand, receiptno);

            //Print Challan..
            //this.ShowReport("FeeCollectionReceiptForCourseRegister.rpt", Convert.ToInt32(studentIDs), "1");

            string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + currentSemesterNo);
            if (dcrNo != string.Empty && studentIDs != string.Empty)
            {
                this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
                //btnPrePrintClallan.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CourseRegistration.btnPrintChallan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        //Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;

        this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt", sessionNo, schemeNo, semesterNo, IdNo);
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rbYes_CheckedChanged(object sender, EventArgs e)
    {
        //SEMESTER PROMOTION FOR SELECTED STUDENT
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        int currentSemesterNo = Int32.Parse(ddlSem.SelectedValue) + 1;
        lblSemester.Text = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + currentSemesterNo);
        //Show Current Semester Courses..
        DataSet dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT SR.IDNO,C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 AND C.SEMESTERNO = " + currentSemesterNo + " AND ISNULL(C.COURSE_STATUS,0) = 1", "C.CCODE");
        btnSubmit.Enabled = true;
        rbNo.Enabled = false;
    }

    protected void btnPrePrintClallan_Click(object sender, EventArgs e)
    {
        string studentIDs = lblName.ToolTip;
        int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + selectSemesterNo);
        if (dcrNo != string.Empty && studentIDs != string.Empty)
        {
            this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
        }
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            ShowReport("PhdRegistrationSlip", "rptPhdPreRegslip.rpt");
        }
    }

    protected void ddlOfferedCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        int deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + lblScheme.ToolTip));
        //int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "MAX(SEMESTERNO)", "BOS_DEPTNO=" + deptno + " AND ISNULL(COURSE_STATUS,0) = 1"));
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "MAX(SEMESTERNO)", "BOS_DEPTNO=" + deptno + " AND ISNULL(BOS_DEPTNO,0) = 1"));
        int Subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(ddlOfferedCourse.SelectedValue)));

        if (ddlOfferedCourse.SelectedIndex > 0)
        {
            if (Subid == 1)
            {
                objCommon.FillDropDownList(ddlType, "ACD_PHD_COURSE_FAC_REMARK", "FAC_REMARK", "NAME", "FAC_REMARK IN(1,3,4)", "FAC_REMARK");
            }
            if (Subid == 2)
            {
                objCommon.FillDropDownList(ddlType, "ACD_PHD_COURSE_FAC_REMARK", "FAC_REMARK", "NAME", "FAC_REMARK IN(2,5,6)", "FAC_REMARK");
            }
        }

        if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) == 1)
        {
            trDeptCourse.Visible = true;
            //objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.BOS_DEPTNO=" + deptno + " AND C.SEMESTERNO=" + semesterno + " AND C.ELECT=0 ", "C.COURSENO");
            if (Convert.ToInt32(ddlSem.SelectedValue) == 1)
                objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.BOS_DEPTNO=" + deptno + " AND C.SEMESTERNO IN (1,3,5,7) AND C.SCHEMENO <>" + lblScheme.ToolTip + " AND ISNULL(C.COURSE_STATUS,0) = 1", "C.COURSENO");
            else
                objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.BOS_DEPTNO=" + deptno + " AND C.SEMESTERNO IN (2,4,6,8) AND C.SCHEMENO <>" + lblScheme.ToolTip + " AND ISNULL(C.COURSE_STATUS,0) = 1", "C.COURSENO");
        }
        else
        {
            trDeptCourse.Visible = false;
        }
        //if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) == 2)
        //{
        //    objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID < 3", "SUBID");
        //    //FILL PARENT DEPARTMENT LIST
        //    objCommon.FillDropDownList(ddlBosDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND DEPTNO IS NOT NULL ", "DEPTNAME");
        //    //trAddCourse.Visible = true; //modified**********************
        //    //lnkAdd.Visible = true;
        //    //lnkAdd.PostBackUrl = "~/ACADEMIC/coursemaster.aspx?pageno=1017";
        //}
        //else
        //{
        //    //trAddCourse.Visible = false;//modified**********************
        //}
        if (ddlOfferedCourse.SelectedItem.Text == "-Communication Skills")
        {
            ddlExem.Visible = true;
        }
        else
        {
            ddlExem.Visible = false;
        }
        btnSubmit.Visible = true;
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;
    }

    //protected void btnSub_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        CourseController objCourse = new CourseController();
    //        Course objc = new Course();
    //        objc.CCode = txtCCode.Text.Trim();
    //        objc.CourseName = txtCourseName.Text.Replace("'", "").Trim();
    //        objc.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
    //        objc.SubID = Convert.ToInt32(ddlSubjectType.SelectedValue);
    //        objc.CollegeCode = Session["colcode"].ToString();
    //        objc.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
    //        objc.Deptno = Convert.ToInt32(ddlBosDept.SelectedValue);
    //        objc.Credits = Convert.ToInt32(txtCredit.Text);
    //        //objc.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

    //        CustomStatus cs = (CustomStatus)objCourse.AddNewPhdCourse(objc);
    //        if (cs.Equals(CustomStatus.RecordSaved))
    //        {
    //            objCommon.DisplayMessage("Course Added Successfully!!", this.Page);
    //            //clear();
    //            objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + Session["idno"].ToString() + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 ", "C.COURSENO");
    //            //ddlOfferedCourse.Items.Add(new ListItem("Others", "1"));
    //            //ddlOfferedCourse.Items.Add(new ListItem("Add New Course", "2"));
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Error!!", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Administration_courseMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void clear()
    {
        tblInfo.Visible = false;
        //trAddCourse.Visible = false; //Modified******
        txtRollNo.Text = string.Empty;
        //txtCCode.Text = string.Empty;//Modified******
        //txtCourseName.Text = string.Empty;//Modified******
        //ddlBosDept.SelectedIndex = 0;//Modified******
        //ddlSubjectType.SelectedIndex = 0;//Modified******
        //txtCredit.Text = string.Empty;//Modified******
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["usertype"].ToString() == "2")
            {
                objCommon.DisplayMessage("You are not Authorized to Reject Course!!", this.Page);
            }
            else
            {
                ImageButton btnDelete = sender as ImageButton;
                string courseno = btnDelete.CommandArgument;
                StudentRegist objSR = new StudentRegist();
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);

                int ret = objSReg.RejectRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), txtRemark.Text, courseno);
                if (ret > 0)
                {
                    objCommon.DisplayMessage("Course Registration has been Reject Successfully!!", this.Page);
                    ShowDetails();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.btnDeletePS_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnConfirmed_Click(object sender, EventArgs e)
    {
        int supe = 0; int joint = 0; int instit = 0; int dr = 0; int drcch = 0;
        //added for extra supervisor
        int secondsupe = 0;
        //end
        StudentRegist objSR = new StudentRegist();
        int ret = 0;
        string superrole = objCommon.LookUp("ACD_PHD_DGC", "SUPERROLE", "IDNO=" + Convert.ToInt32(lblName.ToolTip));
        if ((ViewState["usertype"].ToString() == "3") || ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "6")
        {
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            //DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO", "IDNO=" + Convert.ToInt32(lblName.ToolTip), "IDNO");
            //added for extra supervisor
            DataSet ds = objCommon.FillDropDown("ACD_PHD_DGC", "SUPERVISORNO,JOINTSUPERVISORNO", "INSTITUTEFACULTYNO,DRCNO,DRCCHAIRMANNO,ISNULL(SECONDJOINTSUPERVISORNO,0)SECONDJOINTSUPERVISORNO", "IDNO=" + Convert.ToInt32(lblName.ToolTip), "IDNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                int supervisor = ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                int jointsupervisor = ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["JOINTSUPERVISORNO"].ToString());
                int instituefaculty = ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["INSTITUTEFACULTYNO"].ToString());
                int drc = ds.Tables[0].Rows[0]["DRCNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["DRCNO"].ToString());
                int drcchair = ds.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["DRCCHAIRMANNO"].ToString());
                int uano = Convert.ToInt32(Session["userno"].ToString());
                //added for extra supervisor
                int secondsupervisor = ds.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString() == "0" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SECONDJOINTSUPERVISORNO"].ToString());
                //end


                if (uano == jointsupervisor)
                {
                    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "J");
                    if (ret > 0)
                    {
                        if (superrole == "S")
                        {
                            objCommon.DisplayMessage("Course Registration has been Confirmed by Experts Successfully!!", this.Page);

                        }
                        else
                        {
                            objCommon.DisplayMessage("Course Registration has been Confirmed by Joint Supervisor Successfully!!", this.Page);
                        }
                    }
                }
                if (uano == instituefaculty)
                {
                    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "I");
                    if (ret > 0)
                    {
                        objCommon.DisplayMessage("Course Registration has been Confirmed by Institute Faculty Successfully!!", this.Page);
                    }
                }
                if (uano == drc)
                {
                    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "D");
                    if (ret > 0)
                    {
                        objCommon.DisplayMessage("Course Registration has been Confirmed by DRC Nominee Successfully!!", this.Page);
                    }
                }
                //added for extra supervisor
                if (uano == secondsupervisor)
                {
                    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "T");
                    if (ret > 0)
                    {
                        objCommon.DisplayMessage("Course Registration has been Confirmed by Second Joint Supervisor Successfully!!", this.Page);
                    }
                }
                //end
                if (uano == drcchair)
                {
                    // DataSet ds1 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS", "IDNO=" + Convert.ToInt32(lblName.ToolTip), "IDNO");
                    DataSet ds1 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS,ISNULL(SECONDJOINTSUPERVISORSTATUS,0)SECONDJOINTSUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "IDNO");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        supe = ds1.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString());
                        joint = ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString());
                        instit = ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString());
                        dr = ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCSTATUS"].ToString());
                        drcch = ds1.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString());
                        string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        // added for extra supervisor
                        secondsupe = ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds1.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString());

                        if (noofdgc == "3")
                        {
                            joint = 1;
                        }
                        string jointoutside = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        if (jointoutside != "")
                        {
                            joint = 1;
                        }
                        string instioutside = objCommon.LookUp("ACD_PHD_DGC", "OUTINSTITUTE", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        if (instioutside != "")
                        {
                            instit = 1;
                        }
                        // comment to add code for extra supervisor
                        //if (supe == 1 && joint == 1 && instit == 1 && dr == 1)
                        //{
                        //    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "DR");
                        //    if (ret > 0)
                        //    {
                        //        objCommon.DisplayMessage("Course Registration has been Confirmed by DRC Chairman Successfully!!", this.Page);
                        //    }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("Please Confirm first by DGC member!!", this.Page);

                        //}

                        // added for extra supervisor
                        if (superrole == "T")
                        {

                            if (supe == 1 && joint == 1 && instit == 1 && dr == 1 && secondsupe == 1)
                            {
                                ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "DR");
                                if (ret > 0)
                                {
                                    objCommon.DisplayMessage("Course Registration has been Confirmed by DRC Chairman Successfully!!", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("Please Confirm first by DGC member!!", this.Page);
                            }
                        }
                        else
                        {
                            if (supe == 1 && joint == 1 && instit == 1 && dr == 1)
                            {
                                ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "DR");
                                if (ret > 0)
                                {
                                    objCommon.DisplayMessage("Course Registration has been Confirmed by DRC Chairman Successfully!!", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("Please Confirm first by DGC member!!", this.Page);
                            }
                        }

                        // end 

                    }
                }
                if (ViewState["usertype"].ToString() == "4")
                {
                    //DataSet ds3 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS", "IDNO=" + Convert.ToInt32(lblName.ToolTip), "IDNO");
                    // added for extra supervisor 
                    DataSet ds3 = objCommon.FillDropDown("ACD_PHD_COURSE_STATUS", "SUPERVISORSTATUS,JOINTSUPERVISORSTATUS", "INSTITUTEFACULTYSTATUS,DRCSTATUS,DRCCHAIRSTATUS,ISNULL(SECONDJOINTSUPERVISORSTATUS,0)SECONDJOINTSUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "IDNO");

                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        supe = ds3.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["SUPERVISORSTATUS"].ToString());
                        joint = ds3.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["JOINTSUPERVISORSTATUS"].ToString());
                        instit = ds3.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["INSTITUTEFACULTYSTATUS"].ToString());
                        dr = ds3.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["DRCSTATUS"].ToString());
                        drcch = ds3.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["DRCCHAIRSTATUS"].ToString());
                        string noofdgc = objCommon.LookUp("ACD_PHD_DGC", "NOOFDGC", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        // added for extra supervisor
                        secondsupe = ds3.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString() == "" ? 0 : Convert.ToInt32(ds3.Tables[0].Rows[0]["SECONDJOINTSUPERVISORSTATUS"].ToString());

                        if (noofdgc == "3")
                        {
                            joint = 1;
                        }
                        string jointoutside = objCommon.LookUp("ACD_PHD_DGC", "OUTMEMBER", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        if (jointoutside != "")
                        {
                            joint = 1;
                        }
                        string instioutside = objCommon.LookUp("ACD_PHD_DGC", "OUTINSTITUTE", "IDNO =" + Convert.ToInt32(lblName.ToolTip));
                        if (instioutside != "")
                        {
                            instit = 1;
                        }
                        // commet for extra supervisor
                        //if (supe == 1 && joint == 1 && instit == 1 && dr == 1 && drcch == 1)
                        //{
                        //    ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "");
                        //    if (ret > 0)
                        //    {
                        //        objCommon.DisplayMessage("Course Registration has been confirmed by DEAN Successfully!!", this.Page);
                        //        btnPrintRegSlip.Visible = true;
                        //    }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("Please Confirm first by DGC member && Drc Chairman!!", this.Page);
                        //    btnPrintRegSlip.Visible = false;
                        //}

                        // added for extra supervisor
                        if (superrole == "T")
                        {
                            if (supe == 1 && joint == 1 && instit == 1 && dr == 1 && drcch == 1 && secondsupe == 1)
                            {
                                ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "");
                                if (ret > 0)
                                {
                                    objCommon.DisplayMessage("Course Registration has been confirmed by DEAN Successfully!!", this.Page);
                                    btnPrintRegSlip.Visible = true;
                                    btnReject.Visible = false;
                                    btnConfirmed.Visible = false;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("Please Confirm first by DGC member && Drc Chairman!!", this.Page);
                                btnPrintRegSlip.Visible = false;

                            }
                        }
                        else
                        {
                            if (supe == 1 && joint == 1 && instit == 1 && dr == 1 && drcch == 1)
                            {
                                ret = objSReg.ConfirmedRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), "");
                                if (ret > 0)
                                {
                                    objCommon.DisplayMessage("Course Registration has been confirmed by DEAN Successfully!!", this.Page);
                                    btnPrintRegSlip.Visible = true;
                                    btnReject.Visible = false;
                                    btnConfirmed.Visible = false;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("Please Confirm first by DGC member && Drc Chairman!!", this.Page);
                                btnPrintRegSlip.Visible = false;
                            }
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Right Session!!", this.Page);
                    }
                }

            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {

        if (txtRemark.Text == "")
        {
            objCommon.DisplayMessage("Please Insert Reason for Reject Course Registration!!", this.Page);

        }
        else
        {
            StudentRegist objSR = new StudentRegist();
            string course = string.Empty;
            string courseno = string.Empty;
            if ((ViewState["usertype"].ToString() == "3") || ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "6")
            {
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
                foreach (RepeaterItem item in lvHistory.Items)
                {
                    CheckBox Chkselect = item.FindControl("chkSelect") as CheckBox;
                    course = Chkselect.ToolTip.ToString();
                    if (Chkselect.Checked == true)
                    {
                        courseno = course + ',' + courseno;
                    }

                }

                int ret = objSReg.RejectRegisteredSubjectsPhd(objSR, Convert.ToInt32(ViewState["usertype"].ToString()), txtRemark.Text, courseno);
                if (ret > 0)
                {
                    if (ViewState["usertype"].ToString() == "3")
                    {
                        objCommon.DisplayMessage("Course Registration has been Reject Successfully!!", this.Page);
                    }
                    else if (ViewState["usertype"].ToString() == "6")
                    {
                        objCommon.DisplayMessage("Course Registration has been Reject by DRC Successfully!!", this.Page);
                    }
                    else if (ViewState["usertype"].ToString() == "4")
                    {
                        objCommon.DisplayMessage("Course Registration has been Reject by DEAN Successfully!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Course Registration Not Rejected!!", this.Page);
                }

            }
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            //=========== add activity condition if activity then display only report  ============> added by dipali on  24072018
            if (semcount != 1)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND B.IDNO = " + Convert.ToInt32(Session["idno"].ToString()) + " AND B.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "B.SEMESTERNO");
                ddlSem.SelectedIndex = 1;
            }
            //CheckActivity();
            //  ---- End ---------------- 

            trremark.Visible = false;

            string accepted = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ACCEPTED", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue));
            if (accepted == "1")
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            string status = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SUPERVISORSTATUS = 1 AND DRCSTATUS = 1 AND DEANSTATUS = 1 AND JOINTSUPERVISORSTATUS = 1 AND INSTITUTEFACULTYSTATUS = 1");
            if (Convert.ToInt32(status.ToString()) >= 1)
            {
                btnPrintRegSlip.Visible = true;
                btnConfirmed.Visible = false;
                tr1.Visible = true;

            }
            else
            {
                tr1.Visible = false;
                btnPrintRegSlip.Visible = false;
                btnConfirmed.Visible = true;
            }
            string cancstatus = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 0 AND DRCSTATUS = 0 AND DEANSTATUS = 0 AND JOINTSUPERVISORSTATUS = 0 AND INSTITUTEFACULTYSTATUS = 0");
            string reason = objCommon.LookUp("ACD_PHD_COURSE_STATUS", "REMARK", "IDNO = " + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUPERVISORSTATUS = 0 AND DRCSTATUS = 0 AND DEANSTATUS = 0 AND JOINTSUPERVISORSTATUS = 0 AND INSTITUTEFACULTYSTATUS = 0");

            if (cancstatus == "1")
            {
                btnPrintRegSlip.Visible = false;
                trCancel.Visible = true;
                lblReasoncan.Visible = true;
                lblReasoncan.Text = reason;
                txtRemark.Visible = false;
                lblReasoncan.Font.Bold = true;
                lblReasoncan.ForeColor = System.Drawing.Color.Red;
                lblReasoncan.Font.Size = 12;
            }
            else
            {
                trCancel.Visible = false;
            }
        }
        if (ViewState["usertype"].ToString() == "3")
        {
            if (txtRollNo.Text.Trim() != string.Empty)
                CheckActivity();
        }
        if (ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "1")
        {
            if (lblEnrollNo.Text.Trim() != string.Empty)
            {
                txtRollNo.Text = lblEnrollNo.Text;
                ShowDetails();
            }
        }
    }

    protected void lvHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Label lbl = e.Item.FindControl("lbl") as Label;
        ////lbl.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ACADEMIC/CourseRegistrationPhdEdit.aspx?pageno=1186&session=" + Convert.ToInt32(ddlSession.SelectedValue) + "&idno=" + Convert.ToInt32(lblName.ToolTip));
    }

    protected void btnstatus_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PHDCourseRegistrationStatus", "Phd_Course_Register_Stud_Apply.rpt");
    }

    private void ShowReportApplystudent(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ViewState["usertype"].ToString() == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO= 0";
            }
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseRegistrationForPhd.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSem_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlSem.SelectedValue) > 0)
            {
                string idnos = lblName.ToolTip.ToString();
                ViewState["ID"] = idnos;
                int id = Convert.ToInt32(ViewState["ID"].ToString());//Convert.ToInt32(Session["idno"].ToString());
                objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + id + " )", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 AND C.SEMESTERNO = " + ddlSem.SelectedValue + " AND C.CCODE IS NOT NULL", "C.COURSENO");
                //objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + id + " )", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 AND C.SEMESTERNO = " + ddlSem.SelectedValue + " AND ISNULL(C.COURSE_STATUS,0) = 1 AND C.CCODE IS NOT NULL", "C.COURSENO");
                ddlOfferedCourse.SelectedIndex = 0;



                //ddlOfferedCourse.Items.Add(new ListItem("Please Select", "0"));
                //  ddlOfferedCourse.Items.Add(new ListItem("Others", "1"));
                //  ddlOfferedCourse.Items.Add(new ListItem("Add New Course", "2"));
                //semcount = 1;
                //ddlSession_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseRegistrationForPhd.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
