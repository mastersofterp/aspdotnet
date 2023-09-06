using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;


public partial class ACADEMIC_EXAMINATION_Dropcourses : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    FeeCollectionController feeController = new FeeCollectionController();

    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    int idno = 0;
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

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    GetStudentDeatlsForEligibilty();
                    divCourses.Visible = false;
                    pnlSearch.Visible = false;
                    ShowDetails();
                    // FillExamFees();
                    //lblstuintth.Visible = true;
                    //lblstuintpr.Visible = true;
                    // ShowStudentDetails();
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //objCommon.DisplayMessage(this.updDetails, "You Are Not Authorized To View This Page!!", this.Page);
                    // return;
                    // divNote.Visible = false;

                    pnlSearch.Visible = true;
                    // CheckActivity();
                    //lblintpr.Visible = false;
                    // lblintth.Visible = false;
                }
                else
                {
                    pnlStart.Enabled = false;
                    div_enrollno.Visible = false; //**********
                    div_btn.Visible = false; //**********
                }
                // btnSubmit.Visible = false;

            }

            ViewState["idno"] = 0;
            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");

        }


    }




    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {

            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                CheckActivity();

            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);

                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.GetStudentDeatlsForEligibilty --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }


    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        if (sessionno == "")
        {
            sessionno = "0";
        }
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlStart.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlStart.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlStart.Visible = false;
        }
        dtr.Close();
    }

    private void ShowDetails()
    {

        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();

        int idno = 0;
        int sessionno = Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();

        // Commented below code because we added drop dwon list for session

        string Session_Name = string.Empty;
        Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        lblsession.Text = Convert.ToString(Session_Name);

        string Session_No = string.Empty;
        Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        if (Session_No == "")
        {
            Session_No = "0";
        }
        Session["SESSIONNO"] = Convert.ToInt32(Session_No);

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
        {

            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            if (idno > 0)
            {
                divCourses.Visible = true;
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();

                        lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip));
                        //hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip) + " and a.EXAM_REGISTERED=1 and isnull(a.CANCEL,0)=0");

                        // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        ViewState["IDNO"] = lblName.ToolTip;
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                        studentlist();





                    }
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

    private void studentlist()
    {
        //Fail subjects List
        int count = 0;
        int idno = 0;
        string subReg = string.Empty;
        string CheckRecord = string.Empty;
        ////int sessionno = Convert.ToInt32(Session["currentsession"]);
        // int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        // int schemeno = Convert.ToInt32(lblScheme.ToolTip);
        StudentController objSC = new StudentController();

        DataSet dsFailSubjects;
        //DataSet dsDetainedStudent = null;

        if (ViewState["usertype"].ToString() == "2")
        {
            Session["SESSIONNO"] = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (Session["SESSIONNO"] == "")
            {
                Session["SESSIONNO"] = "0";
            }
            idno = Convert.ToInt32(Session["idno"]);

        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            ViewState["idno"] = idno;
        }

        StudentRegistration objSRegist = new StudentRegistration();
        int totalcredits = Convert.ToInt32(hdfcurrcredits.Value);
        dsFailSubjects = objSRegist.GetStudentDropSubjects(idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue));
        if (dsFailSubjects != null && dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            trFailList.Visible = true;
            //  trCourseList.Visible = false;
            //  trAllCoureWithoutPass.Visible = false;
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            int n = 0;

            foreach (ListViewDataItem item in lvFailCourse.Items)
            {

                CheckBox chkaccept = item.FindControl("chkAccept") as CheckBox;
                Label lblactinestatus = item.FindControl("lblstat") as Label;
                HiddenField hdfcredts = item.FindControl("hdncurcredits") as HiddenField;
                if (lblactinestatus.Text.ToUpper() == "APPROVED")
                {
                    lblactinestatus.Style.Add("color", "GREEN");
                }
                else if (lblactinestatus.Text.ToUpper() == "PENDING")
                {
                    lblactinestatus.Style.Add("color", "RED");
                }
                else
                {
                    lblactinestatus.Style.Add("color", "BLUE");
                }
                if (dsFailSubjects.Tables[0].Rows[n]["STATUS"].ToString() == "0" || dsFailSubjects.Tables[0].Rows[n]["STATUS"].ToString() == "1")
                {
                    chkaccept.Checked = true;
                    if (Convert.ToInt32(dsFailSubjects.Tables[0].Rows[n]["STATUS"]) == 1)
                    {
                        chkaccept.Enabled = false;
                    }
                    totalcredits = totalcredits - Convert.ToInt32(dsFailSubjects.Tables[0].Rows[n]["CREDITS"]);
                }
                n++;

            }
            ViewState["count"] = count;
            lvFailCourse.Visible = true;
            divCourses.Visible = true;
            //btnReport.Visible = true;
            btnSubmit.Visible = true;
            btnRemoveList.Visible = true;
            //trNote.Visible = true;
            lblcred.Text = totalcredits.ToString();
            hdfcurrcredits.Value = totalcredits.ToString();

            //credits.Visible = true;
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();

            lvFailCourse.Visible = false;
            // divCourses.Visible = false;

            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            //trNote.Visible = false;
            lblcred.Text = "0";

            //credits.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean selection = false;
            int result = 0;
            int status = 0;
            string reason = string.Empty;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox chkdropcourse = dataitem.FindControl("chkAccept") as CheckBox;
                    //CheckBox chkreassesment = dataitem.FindControl("chkreassesment") as CheckBox;

                    if (chkdropcourse.Checked && chkdropcourse.Enabled)
                    {
                        selection = true;
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                       
                        objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                       
                        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    }
                  
                }
                if (!selection)
                {
                    objSR.COURSENOS = "0";
                    objSR.EXTERMARKS = "0";
                    objSR.CCODES = "0";
                    objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                    return;
                }
                //if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
                //{
                //    checkreadrepaper = 1;
                //}
                //else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
                //{
                //    checkreadrepaper = 2;
                //}
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                //objSR.EXTERMARKS = objSR.EXTERMARKS.TrimEnd('$');
                objSR.CCODES = objSR.CCODES.TrimEnd('$');
                //RECHECKORREASS = RECHECKORREASS.TrimEnd('$');
                //objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]);
                objSR.SESSIONNO = Convert.ToInt32(Session["SESSIONNO"].ToString());
                objSR.IDNO = Convert.ToInt32(ViewState["IDNO"]);
                //objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["usertype"]);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                reason = txtreason.Text;
                //if (ViewState["action"] == "add")
                //{
                //    opertion = 0;
                //}
                //else
                //{
                //    opertion = 1;
                //}
                result = objSReg.AddUpdatedropcourses(objSR, reason);
                if (result > 0)
                {
                    objCommon.DisplayMessage(this.updDetails, "Selected Courses Saved Sucessfully.", this.Page);
                    //radiolist.Visible = true;
                    //  BtnPrntChalan.Visible = true;
                    // BtnOnlinePay.Visible = false;
                }
                else
                {

                    objCommon.DisplayMessage(updDetails, "Failed To Registered Courses", this.Page);
                }
                ShowDetails();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();


            int sessionno = Convert.ToInt32(Session["currentsession"]);
            StudentController objSC = new StudentController();

            // Commented below code because we added drop dwon list for session

            string Session_Name = string.Empty;
            Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            lblsession.Text = Convert.ToString(Session_Name);

            string Session_No = string.Empty;
            Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (Session_No == "")
            {
                Session_No = "0";
            }
            Session["SESSIONNO"] = Convert.ToInt32(Session_No);

            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
            {

                idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            }
            try
            {
                if (idno > 0)
                {
                    divCourses.Visible = true;
                    DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                            lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                            lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                            lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                            lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                            lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();

                            lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                            hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip));
                            //hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip) + " and a.EXAM_REGISTERED=1 and isnull(a.CANCEL,0)=0");

                            // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            ViewState["IDNO"] = lblName.ToolTip;
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                            showdropcourses();

                        }
                    }
                    divreason.Visible = false;
                    btnapprove.Visible = true;
                    divnote.Visible = false;
                    credits.Visible = false;
                    btnRemoveList.Visible = true;
                    divremark.Visible = true;
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
        catch (Exception ex)
        {

        }
    }

    public void showdropcourses()
    {
        DataSet dsdropcourse = new DataSet();
        lvdropcourse.Visible = true;
        try
        {

            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            dsdropcourse = objSReg.GetDropCourseList(idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue));
            if (dsdropcourse != null && dsdropcourse.Tables[0].Rows.Count > 0)
            {
                lvdropcourse.DataSource = dsdropcourse;
                lvdropcourse.DataBind();
                foreach (ListViewDataItem item in lvdropcourse.Items)
                {

                    CheckBox chkaccept = item.FindControl("chkAccept") as CheckBox;
                    Label lblactinestatus = item.FindControl("lblstat") as Label;

                    if (lblactinestatus.Text.ToUpper() == "APPROVED")
                    {
                        lblactinestatus.Style.Add("color", "GREEN");
                    }
                    else if (lblactinestatus.Text.ToUpper() == "PENDING")
                    {
                        lblactinestatus.Style.Add("color", "RED");
                    }
                    else
                    {
                        lblactinestatus.Style.Add("color", "BLUE");
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        bool selection = false;
        int result=0;
        int courseno = 0;
        int userno=0;
        string remark=string.Empty;
        string ccode = string.Empty;
        try
        {
            if (lvdropcourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvdropcourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox chkdropcourse = dataitem.FindControl("chkAccept") as CheckBox;
                    //CheckBox chkreassesment = dataitem.FindControl("chkreassesment") as CheckBox;

                    if (chkdropcourse.Checked)
                    {
                        selection = true;
                        courseno = Convert.ToInt32(((dataitem.FindControl("lblCCode") as Label).ToolTip));

                        ccode = (dataitem.FindControl("lblCCode") as Label).Text;
                       
                        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                          objSR.SESSIONNO = Convert.ToInt32(Session["SESSIONNO"].ToString());
                objSR.IDNO = Convert.ToInt32(ViewState["IDNO"]);
                objSR.UA_NO = Convert.ToInt32(Session["usertype"]);
                        userno=Convert.ToInt32(Session["userno"]);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                        remark=txtRemark.Text;
                          result = objSReg.AddUpdatedropcoursesstatus(objSR,courseno,ccode,userno,remark);
                          if (result > 0)
                          {
                              objCommon.DisplayMessage(this.updDetails, "Selected Courses Approved Sucessfully.", this.Page);
                              showdropcourses();
                          }
                         
                    }
                }
                if (!selection)
                {
                    objSR.COURSENOS = "0";
                    objSR.EXTERMARKS = "0";
                    objSR.CCODES = "0";
                    objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                    return;
                }

              
                
              
               
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnRemoveList_Click(object sender, EventArgs e)
    {
        try
        {

            lvFailCourse.DataSource = null;
            lvFailCourse.Visible = false;
            btnSubmit.Visible = false;
            btnapprove.Visible = false;
            btnRemoveList.Visible = false;
            
            lvdropcourse.DataSource = null;
            lvdropcourse.Visible = false;
            divCourses.Visible = false;
            txtRemark.Text = string.Empty;
            txtreason.Text = string.Empty;

        }
        catch (Exception ex)
        {

        }
    }
}
