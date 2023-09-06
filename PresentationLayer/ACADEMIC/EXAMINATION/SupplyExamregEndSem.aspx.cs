#region ruleforSupplyExam
//Below conditions are applicable to all Degree students.

//Student having maximum 3 Standing arrear after his final semester result publish.
//Maximum of three courses (Student can have maximum 3 arrear including I grade)
//I grade subjects are not eligible for apply. 
//AB,U,F grade are allowed for Supplementary Registration.

//If any students try to register other than above eligible criteria, then message should display that "You are not eligible."

//Fees per subject
//UG-2000
//PG-3000
//------------------------------------------------------------------------------------------------------------------------------------ 
//Below conditions are applicable to BTECH Degree students.

//After 6th Sem Regular result publish.
//Regular students (Joined from 1st year) -If students not earned 60 credit
//Lateral students (Joined from 2nd year) -If students not earned 40 credit
//Then students can apply for maximum of 3 course, that to achieve the 60 or 40 credit.

//Example:
//Case 1: 
//Students is having 48 credits after 6th sem result publish.
//Then Student can apply for 3 course which contains 4 credit paper of 3 courses.

//Case 2: 
//Students is having 46 credits after 6th sem result publish.
//Then Student cannot apply for 3 course because in curriculum does not have more than 4 credit except project work.

//I grade subjects are not eligible for apply. 
//AB,U,F grade are allowed for Supplementary Registration.

//If any students try to register other than above eligible criteria, then message should display that "You are not eligible."

#endregion

//Created by    : Sachin A
//Created Date  : 10 May 2023
//Modified By   : 
//Modified Date :
//Used For      : Supplementary Exam Registration by Student and Admin End

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

public partial class ACADEMIC_EXAMINATION_SupplyExamregEndSem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentFees objStudentFees = new StudentFees();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;

    #region Page_PreInit
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #endregion

    #region Page_Load
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

                 
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    GetStudentDeatlsForEligibilty();
                    pnlSearch.Visible = false;
                    div_enrollno.Visible = false;
                    lsScheme.Visible = false;
                    FillExamFees();
                }
                else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                {
                    pnlSearch.Visible = false;
                    FillDropdown();
                }
                else
                {
                    pnlStart.Enabled = false;
                    div_enrollno.Visible = false; //**********
                    div_btn.Visible = false; //**********
                }

                ViewState["count"] = null;
                ViewState["ipAddress"] = GetUserIPAddress();
            }

            ViewState["idno"] = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS  LIKE '%" + Session["COLLEGE_ID"].ToString() + "%')", "");
            }
        }

        divMsg.InnerHtml = string.Empty;
        this.FillExamFees();


    }
    #endregion

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];
        }
        return User_IPAddress;
    }

    private void CheckActivity()
    {
        try
        {
            string sessionno = string.Empty;
           // sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" Session["COLLEGE_ID"] + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS  LIKE '%" + Session["COLLEGE_ID"].ToString() + "%')");
            //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%') AND COLLEGE_ID=" + College_id + "AND DEGREENO=" + Convert.ToInt32(Session["DEGREENO"].ToString()) + "AND SEMESTER=" + Convert.ToInt32(Session["SEMESTERNO"].ToString()) + "AND BRANCH=" + Convert.ToInt32(Session["BRANCHNO"].ToString()));
            sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + " AND COLLEGE_IDS LIKE '%" + Session["COLLEGE_ID"].ToString() + "%' AND DEGREENO LIKE '%" + Degreeno + "%' AND BRANCH LIKE '%" + branchno + "%' AND SEMESTER LIKE '%" + Semesterno + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");

            ActivityController objActController = new ActivityController(); 
            if (sessionno == "")
            {
                objCommon.DisplayMessage(this.updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                pnlStart.Visible = false;
                return;
            }  
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));
            ViewState["Sessionno"] = sessionno;
              
            if (dtr.Read())
            {
                Session["currentsession"] = sessionno;
                  
                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(this.updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                    pnlStart.Visible = false;

                }
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SupplyExamregEndSem.CheckActivity --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");
    }

    #region btnSubmit_Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        int College_id = 0;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }

        int schemeno = Convert.ToInt32(lblScheme.ToolTip);
        if (ViewState["usertype"].ToString() == "2")
        {
            objSR.SESSIONNO = Convert.ToInt32(ViewState["Sessionno"]) == 0 ? Convert.ToInt32(Session["currentsession"]) : Convert.ToInt32(ViewState["Sessionno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            // College_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"])));
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + ddlSession.SelectedValue + " AND RECON=1 AND  CAN = 0  AND IDNO=" + lblName.ToolTip + "AND RECIEPT_CODE= 'SEF' AND SEMESTERNO=" + ddlBackLogSem.SelectedValue));  //AND COM_CODE=''              
            if (exreg > 0)
            {
                objCommon.DisplayMessage(this.Page, "Selected Semester Exam Registration Already Done", this.Page);
                btnSubmit.Visible = false;
                btnRemoveList.Visible = false;
                return;
            }
        } 
        //int College_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
        //int Sessionno=Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER","SESSIONNO","COLLEGE_ID="+ College_id +"AND IS_ACTIVE=1 AND FLOCK=1"));

        objSR.IDNO = idno;
        objSR.REGNO = lblEnrollNo.Text;
        objSR.SCHEMENO = Convert.ToInt32(schemeno);
        objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        //objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.COURSENOS = string.Empty;
        string ExistCourses = string.Empty;
        string ddlValue = string.Empty;
        string SEMESTERNO = string.Empty;
        string Semesternos = string.Empty;
        decimal fees = 0;
        //int count = 0;
        string subids = string.Empty;
        if (lvFailCourse.Items.Count > 0)
        {
            int Semcount = 0; int course = 0;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                    SEMESTERNO += ((dataitem.FindControl("lblSemesterno")) as Label).ToolTip + ",";
                    subids += ((dataitem.FindControl("lblCourseName")) as Label).ToolTip + ",";
                    Semesternos += ((dataitem.FindControl("lblSemesterno")) as Label).ToolTip + "$";
                    Semcount++;
                }
            }

            //objSR.SESSIONNO = Convert.ToInt32(ViewState["Sessionno"]);
            if (Semcount < 0)
            {
                Session["semesternos"] = Semesternos;
            }
            else
            {
                //string s = Semesternos;
                //string result = s.Remove(s.Length - 1);
                string result = Semesternos.TrimEnd('$');
                Session["semesternos"] = result;
            }

            if (objSR.COURSENOS.Length == 0)
            {
                objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                return;
            }
            if (totamtpay.Text == "" || totamtpay.Text == "0")
            {
                objCommon.DisplayMessage(this.Page, "Fees not defined for current Session Please Contact Admin", this.Page);
                return;
            }  
            else
            {
                fees = Convert.ToDecimal(totamtpay.Text);
            }
            CustomStatus cs = 0;
            if (Session["usertype"].ToString() == "2")//for student
            {
                cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsback(objSR, subids, fees, SEMESTERNO);
                // CustomStatus cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsbackSupply(objSR, Convert.ToInt32(Session["Sub_Id"]), fees);
            }
            else  //for admin 
            {
                cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsbackByAdmin(objSR, subids, fees, SEMESTERNO);
            }

            if (cs == CustomStatus.RecordSaved)
            { 
                BtnPrntChalan.Visible = false;
                btnSubmit.Visible = false;
                if (ViewState["usertype"].ToString() == "2")
                {
                    objCommon.DisplayMessage(this.Page, "Selected Courses Saved Sucessfully,But will be confirm only after Successful Payment.", this.Page);
                    BtnOnlinePay.Visible = true;
                    int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Convert.ToString(Session["SESSIONNO"]) + " AND RECON=1 AND  CAN = 0  AND IDNO=" + lblName.ToolTip + "AND RECIEPT_CODE= 'SEF' "));  //AND COM_CODE=''           
                    if (exreg > 0)
                    {
                        checkSubject();
                    }
                    BtnPrntChalan.Visible = false;
                    //btnReport1.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Selected Courses Saved Sucessfully,Print the chalan and Reconcile it for the successful Payment.", this.Page);
                    checkSubjectForAdmin();
                    BtnPrntChalan.Visible = false;
                    btnReport1.Visible = true;
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Something went wrong!", this.Page);    //Student course already exist or payment already done 
                return;
            }
        }
    }
    #endregion

    #region

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
    private void ShowDetails()
    {

        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();

        int idno = 0;
        int sessionno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            sessionno = Convert.ToInt32(Session["currentsession"]) == 0 ? Convert.ToInt32(ViewState["Sessionno"]) : Convert.ToInt32(Session["currentsession"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }
        StudentController objSC = new StudentController();   // Commented below code because we added drop dwon list for session

        string Session_Name = string.Empty;
        Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + sessionno + "");

        Session["SESSIONNO"] = Convert.ToInt32(sessionno) == 0 ? Convert.ToInt32(ViewState["Sessionno"]) : sessionno;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            lblsession.Text = Convert.ToString(Session_Name);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            //Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO =" + ddlSession.SelectedValue);
            lblsession.Text = Session_Name;
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

                        lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        //   lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

                        hdfcurrcredits.Value = "0";
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();     //Added dt on 11082023

                        ViewState["IDNO"] = lblName.ToolTip;
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                        objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL' and regno='" + lblEnrollNo.Text + "'", "SEMESTERNO");
                        if (Session_Name != "")
                        {
                            lblsession.Text = Convert.ToString(Session_Name);
                        }

                        // ViewState["idtype"] = dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString();
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
    #endregion

    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO,COLLEGE_ID", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                Session["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                Session["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Session["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                Session["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        ShowReport("ExamRegistrationSlip", "PaymentReceipt_Exam_Registered_Courses.rpt");
        btnReport1.Visible = true;
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            pnlSearch.Visible = true;
            divCourses.Visible = false;
        }
        if (ViewState["usertype"].ToString() == "2")
        {
            pnlSearch.Visible = true;
            div_college.Visible = false;
            //div_enrollno.Visible = true;
            div_regno.Visible = false;
            div_btn.Visible = false;
            divCourses.Visible = true;
            ddlBackLogSem.Visible = false;          //Added dt on 26122022
            divArrearSem.Visible = false;
            //this.GetFailExamDetails(); 
        }
        divNote.Visible = false;
        lblsession.Text = string.Empty;
        trFailList.Visible = false;
        ShowDetails();


        if (ViewState["usertype"].ToString() == "2") //Added dt on 26122022
        {
            //int Sessionno1 = Convert.ToInt32(ViewState["Sessionno"]);
            //int Sessiono = Sessionno1 + 1;
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "COURSENO", "COURSENAME", "SEMESTERNO=" + ddlBackLogSem.SelectedValue + "AND GDPOINT=0 AND SESSIONNO <" + Sessiono + "AND IDNO=" + Convert.ToInt32(Session["idno"]), "COURSENO");

            int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Convert.ToString(Session["SESSIONNO"]) + " AND RECON=1 AND  CAN = 0  AND IDNO=" + lblName.ToolTip + "AND RECIEPT_CODE= 'SEF' "));
            if (exreg > 0)
            {
                objCommon.DisplayMessage(this.Page, "Selected Semester Exam Registration Already Done !", this.Page); //Session["SESSIONNO"]
                GetFailExamDetails();
                checkSubject();
                btnReport1.Visible = true;
                btnSubmit.Visible = false;
                btnRemoveList.Visible = false;
            }
            else
            {
                this.studentlist();
                btnPrcdToPay.Visible = false;
                totamtpay.Text = "";
                btnReport1.Visible = false;
            }
        }
    }

    #region ShowReport
    private void ShowReport(string reportTitle, string rptFileName)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        string EndSemexam = string.Empty;
        EndSemexam = "E";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ViewState["usertype"].ToString() == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + ",@P_SEMESTERNO=" + semesterno;
            }
            else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + semesterno;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport1(string rptName, int dcrNo, int studentNo, string copyNo, int param)
    {
        try
        {
            btnReport.Visible = false;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=examReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
            if (param == 1)
            {
                url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            }
            else
            {
                url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_PREV_STATUS=1,@P_SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue);
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }
    #endregion

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        this.studentlist();
        btnPrcdToPay.Visible = false;
    }

    #region
    private void studentlist()
    {
        //Fail subjects List
        int count = 0;
        int idno = 0;
        string subReg = string.Empty;
        string CheckRecord = string.Empty;
        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        int schemeno = Convert.ToInt32(lblScheme.ToolTip);
        StudentController objSC = new StudentController();
        StudentRegistration objSRegist = new StudentRegistration();


        if (ViewState["usertype"].ToString() == "2")
        {
            Session["SESSIONNO"] = Convert.ToString(ViewState["Sessionno"]) == string.Empty ? Convert.ToString(Session["currentsession"]) : Convert.ToString(ViewState["Sessionno"]);

            idno = Convert.ToInt32(Session["idno"]);
            ViewState["id_no"] = lblName.ToolTip;

            int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(Session["SESSIONNO"]) + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND CANCEL = 0 AND IDNO=" + idno));
            if (accept > 0)
            {
                lvFailCourse.Enabled = false;
            }
            else
            {
                lvFailCourse.Enabled = true;
                btnSubmit.Enabled = true;
            }
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            ViewState["idno"] = idno;
            ViewState["id_no"] = idno;

        }

        //  subReg = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "count(distinct courseno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue));
        subReg = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "count(distinct courseno)", "ISNULL(CANCEL,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToString(Session["SESSIONNO"]) + "");
        int exreg = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Convert.ToString(Session["SESSIONNO"]) + " AND RECON=1 AND  CAN = 0  AND IDNO=" + lblName.ToolTip + "AND RECIEPT_CODE= 'SEF' "));  //AND COM_CODE=''           
        }
        else
        {
            exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + ddlSession.SelectedValue + " AND RECON=1 AND  CAN = 0  AND IDNO=" + lblName.ToolTip + "AND RECIEPT_CODE= 'SEF' AND SEMESTERNO=" + ddlBackLogSem.SelectedValue));  //AND COM_CODE=''         
        }

        if (exreg > 0)
        {
            if (ViewState["usertype"].ToString() == "2")
            {
                objCommon.DisplayMessage(this.Page, "Selected Semester Exam Registration Already Done !", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Selected Semester Exam Registration Already Done !", this.Page);
                GetFailExamDetails();
                checkSubjectForAdmin();
                return;
            }
        }
        else if (!string.IsNullOrEmpty(subReg))
        {
            trFailList.Visible = false;
            btnRemoveList.Visible = true;
            GetFailExamDetails();
            //if (ViewState["usertype"].ToString() == "2")
            //{
            //    checkSubject();
            //} 
        }
        else
        {
            GetFailExamDetails();
        }
    }
    #endregion

    private void GetFailExamDetails() //Seperate Method Added By Tejas Thakre 17-12022
    {
        DataSet dsFailSubjects;
        StudentRegistration objSRegist = new StudentRegistration();
        int sesssion = Convert.ToInt32(Session["SESSIONNO"]);
        int idno = 0;
        if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = Convert.ToInt32(ViewState["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        dsFailSubjects = objSRegist.GetStudentFailExamSubjects(idno, Convert.ToString(ddlBackLogSem.SelectedValue), sesssion);
        if (dsFailSubjects != null && dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            // objCommon.DisplayMessage(this.Page, "Selected Semester Exam Registration Already Done. Do you want to register?", this.Page);
            trFailList.Visible = true;
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            Session["Sub_Id"] = dsFailSubjects.Tables[0].Rows[0]["SUBID"];      //Added By Tejas Thakre on 17-11-2022
            //foreach (ListViewDataItem item in lvFailCourse.Items)
            //{
            //}
            //ViewState["count"] = count;
            lvFailCourse.Visible = true;
            divCourses.Visible = true;
            btnSubmit.Visible = true;
            btnRemoveList.Visible = true;
            trNote.Visible = true;
            //credits.Visible = true;

        }
        else
        {
            objCommon.DisplayMessage(this.Page, "You are not eligible.", this.Page);
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();

            lvFailCourse.Visible = false;
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            trNote.Visible = false;
            //credits.Visible = true;
        }
    }

    #region getIPAddress
    public string getIPAddress()
    {
        string direction;
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        WebResponse response = request.GetResponse();
        StreamReader stream = new StreamReader(response.GetResponseStream());
        direction = stream.ReadToEnd();
        stream.Close();
        response.Close(); //Search for the ip in the html
        int first = direction.IndexOf("Address: ") + 9;
        int last = direction.LastIndexOf("</b");
        direction = direction.Substring(first, last - first);
        return direction.ToString();
    }

    private bool CheckConnection()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create("http://www.google.com/") as HttpWebRequest;
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region online pay not in use
    //online pay
    public static string encryptFile(string textToencrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToencrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }
    //online pay

    private void SendTransaction()
    {
        string merchant_id = "";
        string submerchant_id = ""; string submerchant_id1 = "";
        DataSet ds = null;
        ds = feeController.Get_COLLEGE_PAYMENTDATA(Convert.ToInt32(hdnCollege.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            merchant_id = ds.Tables[0].Rows[0]["ICID"].ToString();
        }
        else
        {
            return;
        }

        int TotalAmount = Convert.ToInt32(totamtpay.Text);
        string txnrefno = string.Empty; string txnrefno1 = string.Empty;
        string amt = string.Empty;
        string amt1 = string.Empty;
        if ((TotalAmount != null || TotalAmount != 0) && (Session["idno"] != null || Session["idno"] != ""))
        {
            amt1 = encryptFile(TotalAmount.ToString(), ds.Tables[0].Rows[0]["AESKey"].ToString());//******************UNCOMMENT THIS LINE FOR LIVE SERVER*******************************
            amt = TotalAmount.ToString();      //******************FOR TESTING ONLY*******************************
            submerchant_id1 = encryptFile(Session["idno"].ToString().Trim(), ds.Tables[0].Rows[0]["AESKey"].ToString());
            submerchant_id = Session["idno"].ToString().Trim();
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
        string mandatory_fields = "";

        string paymode = encryptFile("9", ds.Tables[0].Rows[0]["AESKey"].ToString());
        string return_url = encryptFile("https://indusuni.mastersofterp.in/response.aspx", ds.Tables[0].Rows[0]["AESKey"].ToString());


        txnrefno1 = encryptFile(lblOrderID.Text, ds.Tables[0].Rows[0]["AESKey"].ToString());//**************
        txnrefno = lblOrderID.Text;//**************
        mandatory_fields = encryptFile((txnrefno + "|" + submerchant_id + "|" + amt + "|" + lblEnrollNo.Text), ds.Tables[0].Rows[0]["AESKey"].ToString());



        string url = string.Empty;

        if (txnrefno != string.Empty && submerchant_id != string.Empty && amt != string.Empty && lblEnrollNo.Text != string.Empty && paymode != string.Empty)
        {


            url = "https://eazypay.icicibank.com/EazyPG?merchantid=" + merchant_id + ""
                      + "&mandatory fields=" + mandatory_fields
                      + "&optional fields="
                      + "&returnurl=" + return_url
                      + "&Reference No=" + txnrefno1  //*********
                      + "&submerchantid=" + submerchant_id1
                      + "&transaction amount=" + amt1
                      + "&paymode=" + paymode;

            Response.Redirect(url, false);
        }
        else
        {
            return;
        }
    }

    private void CreateCustomerRef()
    {
    Reprocess:
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);

        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + Convert.ToString(ir) + "'");          //Added by Sachin A on 24082022 for unique order id
        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }
        lblOrderID.Text = Convert.ToString(Convert.ToString(ViewState["idno"]) + Convert.ToString(ir));

        ViewState["ORDERID"] = lblOrderID.Text;
    }

    //online Pay
    //  get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", 1, "EF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
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
    #endregion

    #region SubmitPaymentDetails
    //online pay
    private void SubmitPaymentDetails()
    {
        int semester = 0;
        string COM_CODE1 = string.Empty;

        CreateCustomerRef();
        GetNewReceiptNo();
        string result1 = string.Empty;
        semester = Convert.ToInt32(lblSemester.ToolTip);
        int sessionno = 0;
        ActivityController objActController = new ActivityController();
        if (ViewState["usertype"].ToString() == "2")
        {
            sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS  LIKE '%" + Session["COLLEGE_ID"].ToString() + "%')"));
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }
        int feeitemid = 2;

        //DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);
        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
        //    if (amountth.Contains('.'))
        //    {
        //        int index = amountth.IndexOf('.');
        //        result1 = amountth.Substring(0, index);
        //    }
        //}

        double totalamount = 0.00;
        string totamt = string.Empty;
        //if (lvFailCourse.Items.Count > 0)
        //{
        //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //    {
        //        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
        //        {
        //            totalamount = totalamount + Convert.ToDouble(result1);
        //        }
        //    }

        //    totamt = totalamount.ToString();
        //}

        totamt = Convert.ToString(totamtpay.Text); ;
        return;

        if (radiolist.SelectedValue != "")
        {
            if (radiolist.SelectedValue == "1")
            {
                int result = 0;
                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToString(lblOrderID.Text), 1, "0", "0", totamt, "0", "0", "0", "0", totamt, "SF");
                Session["RECIEPT_CODE"] = "EF";
                if (result > 0)
                {

                }
                else
                {
                    objCommon.DisplayUserMessage(updDetails, "Failed to Continue.", this.Page);
                    return;
                }
            }
            else if (radiolist.SelectedValue == "2")
            {
                int result = 0;
                GetNewReceiptNo();
                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToString(lblOrderID.Text), 2, "0", "0", totamt, "0", "0", "0", "0", totamt, "SF");
                if (result > 0)
                {
                }
                else
                {
                    objCommon.DisplayUserMessage(updDetails, "Failed To Continue.", this.Page);
                    return;
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updDetails, "Please Select Payment Option!", this.Page);
        }
    }
    #endregion

    protected void btnRemoveList_Click(object sender, EventArgs e)
    {
        trFailList.Visible = false;
        btnSubmit.Visible = false;
        btnRemoveList.Visible = false;
        btnReport.Visible = false;
        radiolist.Visible = false;
        //BtnPrntChalan.Visible = false;
        ddlBackLogSem.SelectedValue = "0";
        trNote.Visible = false;
        hdfcurrcredits.Value = "0";
        lblsession.Text = string.Empty;
        BtnOnlinePay.Visible = false;
        btnReport.Visible = false;
        btnReport1.Visible = false;
        BtnPrntChalan.Visible = false;
        btnPrcdToPay.Visible = false;
        totamtpay.Text = string.Empty;
    }
    protected void lvCurrentSubjects_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlCourseCateg = e.Item.FindControl("ddlCourseCategory") as DropDownList;
        objCommon.FillDropDownList(ddlCourseCateg, "ACD_COURSE_CATEGORY", "CATEGORYNO", "CATEGORYNAME", "CATEGORYNO > 0", "CATEGORYNO");
        ddlCourseCateg.Focus();
    }
    protected void lvCourseswithoutPass_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlCourseCateg1 = e.Item.FindControl("ddlCourseCategory1") as DropDownList;
        objCommon.FillDropDownList(ddlCourseCateg1, "ACD_COURSE_CATEGORY", "CATEGORYNO", "CATEGORYNAME", "CATEGORYNO > 0", "CATEGORYNO");
        ddlCourseCateg1.Focus();
    }
    protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnReport1.Visible = false;
        int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + ddlSession.SelectedValue + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND RECON=1 AND  CAN = 0 AND RECIEPT_CODE= 'SEF' AND IDNO=" + Convert.ToString(ViewState["IDNO"])));  //AND COM_CODE=''
        if (exreg > 0)
        {
            //objCommon.DisplayMessage(this.updDetails, "Selected Semester Exam Registration Already Done !", this.Page); //Session["SESSIONNO"]

            if (ViewState["usertype"].ToString() != "2")
            {
                this.studentlist();
                checkSubjectForAdmin();
                btnSubmit.Visible = true;
            }
            else
            {
                checkSubject();
                btnSubmit.Visible = false;
            }
            btnReport1.Visible = true;
        }
        else
        {
            this.studentlist();
            btnPrcdToPay.Visible = false;
            totamtpay.Text = "";
            BtnOnlinePay.Visible = false;
        }
        //if (ViewState["usertype"].ToString() == "2")
        //{
        //    int Sessionno1 = Convert.ToInt32(ViewState["Sessionno"]);
        //    int Sessiono = Sessionno1 + 1;
        //    //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "COURSENO", "COURSENAME", "SEMESTERNO=" + ddlBackLogSem.SelectedValue + "AND GDPOINT=0 AND SESSIONNO <" + Sessiono + "AND IDNO=" + Convert.ToInt32(Session["idno"]), "COURSENO");
        //}
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO desc");
            //ddlDegree.Focus();
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radiolist.SelectedValue == "1")
        {
            BtnOnlinePay.Visible = true;
            BtnPrntChalan.Visible = false;
        }
        else
        {
            BtnPrntChalan.Visible = true;
            BtnOnlinePay.Visible = false;
        }
    }
    protected void BtnOnlinePay_Click(object sender, EventArgs e)
    {
        BillDeskPaymentGateway();
        //SubmitPaymentDetails();
        //SendTransaction();
    }
    protected void BtnPrntChalan_Click(object sender, EventArgs e)
    {
        SubmitPaymentDetails();
        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "AND RECIEPT_CODE='EF'"));
        ShowReport1("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["IDNO"]), "1", 1);
    }

    #region FillExamFees
    private void FillExamFees()
    {
        try
        {
            ActivityController objActController = new ActivityController();

            int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS  LIKE '%" + Session["COLLEGE_ID"].ToString() + "%')"));
            int feeitemid = 2;
            DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();

                if (amountth.Contains('.'))
                {
                    int index = amountth.IndexOf('.');
                    string result = amountth.Substring(0, index);
                    //lblstuendth.Text = result;
                    //lblstuendth.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SupplyExamregEndSem.FillExamFees --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    //protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    //{
    //    decimal Fee = 0;
    //    foreach (ListViewDataItem item in lvFailCourse.Items)
    //    {
    //        Fee = Convert.ToDecimal((item.FindControl("hdfprev_status") as HiddenField).Value);
    //        totamtpay.Text += Fee;
    //    }
    //}

    #region BillDeskPaymentGateway
    protected void BillDeskPaymentGateway()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        #region "Online Payment"
        try
        {
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND RECIEPT_CODE = 'SEF' AND RECON = 1 AND ISNULL(CAN,0)=0"));

            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage(updDetails, "Photo Copy Reval Fee has been paid already. Can't proceed with the transaction !", this);
                BtnOnlinePay.Visible = false;
                BtnPrntChalan.Visible = true;
                return;
            }
            int sessionno = 0;
            if (ViewState["usertype"].ToString() == "2")
            { 
                sessionno = Convert.ToInt32(ViewState["Sessionno"]) == 0 ? Convert.ToInt32(Session["currentsession"]) : Convert.ToInt32(ViewState["Sessionno"]);   
            }
            else
            {
                sessionno = Convert.ToInt32(ddlSession.SelectedValue);           // Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS  LIKE '%" + Session["COLLEGE_ID"].ToString() + "%')"));
            }
            int result = 0;
            int logStatus = 0;

            CreateCustomerRef();
            GetSessionValues();

            //ViewState["Final_Amt"] = totamtpay.Text.ToString();
            Session["Final_Amt"] = objCommon.LookUp("ACD_DEMAND", "SUM(TOTAL_AMT)", "RECIEPT_CODE='SEF' AND ISNULL(CAN,0)=0 AND SESSIONNO=" + sessionno + "AND IDNO=" + Convert.ToInt32(ViewState["id_no"])); //Added on 24082022

            if (Convert.ToDouble(Session["Final_Amt"]) == 0)
            {
                objCommon.DisplayMessage(updDetails, "You are not eligible for Fee Payment !", this);
                return;
            }

            objStudentFees.UserNo = Convert.ToInt32(ViewState["id_no"]);
            objStudentFees.Amount = Convert.ToDouble(Session["Final_Amt"]);
            objStudentFees.SessionNo = Convert.ToString(sessionno);
            objStudentFees.OrderID = lblOrderID.Text;
            //objStudentFees.TransDate = System.DateTime.Today;  
            int IDNO = Convert.ToInt32(ViewState["id_no"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            string COLLEGE_CODE = Session["colcode"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);

            //insert in acd_fees_log
            // result = feeController.AddPhotoRevalFeeLogSupply(objStudentFees, 1, 1, "SUPEF", 3);

            //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO ", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["idno"] + "'", "");
            DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(ViewState["id_no"]) + "'", "");
            ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
            ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
            ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
            ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
            ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
            ViewState["SESSIONNO"] = sessionno;
            ViewState["SEM"] = lblSemester.ToolTip.ToString();
            ViewState["RECIEPT"] = "SEF";

            ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
            ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());

            if (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == "" || d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty)
            {
                ViewState["MOBILENO"] = "NA";
            }
            if (d.Tables[0].Rows[0]["REGNO"].ToString() == "" || d.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty)
            {
                ViewState["REGNO"] = "NA";
            }
            if (d.Tables[0].Rows[0]["ENROLLNO"].ToString() == "" || d.Tables[0].Rows[0]["ENROLLNO"].ToString() == string.Empty)
            {
                ViewState["ENROLLNO"] = "NA";
            }
            string info = string.Empty;
            //ViewState["info"] = "PRF" + ViewState["REGNO"] + "," + ViewState["SESSIONNO"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];

            ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
            ViewState["basicinfo"] = ViewState["ENROLLNO"];

            ViewState["Semester"] = objCommon.LookUp("ACD_DEMAND", "TOP 1 SEMESTERNO", "RECIEPT_CODE='SEF' AND SESSIONNO=" + sessionno + "AND IDNO=" + ViewState["id_no"]); //Added on 24082022

            //ViewState["basicinfo"] = ViewState["REGNO"] + "," + ViewState["ENROLLNO"] + "," + ViewState["SHORTNAME"];
            // PostOnlinePayment();

            int status1 = 0;
            int Currency = 1;
            string amount = string.Empty;
            amount = Convert.ToString(Session["Final_Amt"]);

            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            //    DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            // string PaymentMode = "ONLINE EXAM FEES";
            string PaymentMode = "Supply Exam";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = amount;
            ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
            // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            Session["studName"] = ViewState["STUDNAME"].ToString() == string.Empty ? "NA" : ViewState["STUDNAME"].ToString();
            Session["studPhone"] = ViewState["MOBILENO"].ToString() == string.Empty ? "0" : ViewState["MOBILENO"].ToString();
            Session["studEmail"] = ViewState["EMAILID"].ToString() == string.Empty ? "NA" : ViewState["EMAILID"].ToString();

            Session["ReceiptType"] = "SEF";
            Session["idno"] = Convert.ToInt32(ViewState["id_no"].ToString());
            Session["paysession"] = sessionno;           // hdfSessioNo.Value;

            Session["homelink"] = "SupplyExamregEndSem.aspx";
            Session["regno"] = ViewState["REGNO"].ToString() == string.Empty ? "0" : ViewState["REGNO"].ToString();
            Session["payStudName"] = ViewState["STUDNAME"].ToString() == string.Empty ? "NA" : ViewState["STUDNAME"].ToString();
            Session["paymobileno"] = ViewState["MOBILENO"].ToString() == string.Empty ? "0" : ViewState["MOBILENO"].ToString();
            Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
            Session["Branchname"] = ViewState["SHORTNAME"].ToString() == string.Empty ? "NA" : ViewState["SHORTNAME"].ToString();
            Session["orgid"] = objCommon.LookUp("REFF", "OrganizationId", "");

            Session["studrefno"] = lblOrderID.Text;
            Session["paysemester"] = ViewState["Semester"].ToString() == string.Empty ? "0" : ViewState["Semester"].ToString();

            int activityno = 0;
            activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "TOP 1 ACTIVITYNO", "ACTIVITYNAME like '%Supplementary%' AND ACTIVESTATUS=1"));
            string paymentconfig =  objCommon.LookUp("ACD_PG_CONFIGURATION", "TOP 1 ACTIVITY_NO", "ACTIVITY_NO="+ activityno);
            if (paymentconfig == "")
            {
                objCommon.DisplayMessage(this.Page, "Payment Gateway not define", this.Page);
                return;
            } 
            Session["payactivityno"] = activityno;

            DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 1, activityno);    // Convert.ToInt32(Session["payactivityno"]
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {
                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl, false);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }
    #endregion

    #region GetSessionValues
    private void GetSessionValues()
    {
        ViewState["FirstName"] = lblName.Text;
        //ViewState["MobileNo"] = lblPH.Text; 
        //ViewState["OrderID"] = lblOrderID.Text;
        ViewState["TOTAL_AMT"] = totamtpay.Text;
    }
    #endregion

    #region  BILL DESK PAYMENT GATEWAY
    //----  BILL DESK PAYMENT GATEWAY ----------------//
    protected void PostOnlinePayment()
    {
        //Added on 20082022   
        FeeCollectionController FeeCollection = new FeeCollectionController();
        int payId = 1;
        int orgId = Convert.ToInt32(objCommon.LookUp("REFF", "OrganizationId", ""));
        string merchId = string.Empty; string checkSumKey = string.Empty; string requestUrl = string.Empty; string responseUrl = string.Empty; Session["CHECKSUM_KEY"] = string.Empty;
        Session["Order_id"] = string.Empty;
        int activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%Supply%'"));

        string pgPageUrl = string.Empty; string accCode = string.Empty;
        DataSet dsGetPayConfig = FeeCollection.GetOnlinePaymentConfigurationDetails(orgId, payId, activityno);
        if (dsGetPayConfig.Tables[0].Rows.Count > 0)
        {
            merchId = dsGetPayConfig.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
            checkSumKey = dsGetPayConfig.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
            requestUrl = dsGetPayConfig.Tables[0].Rows[0]["REQUEST_URL"].ToString();
            responseUrl = dsGetPayConfig.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
            pgPageUrl = dsGetPayConfig.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            accCode = dsGetPayConfig.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
            //Session["CHECKSUM_KEY"] = checkSumKey;   //Added on 21082022
        }

        #region Declarations
        string feeAmount = string.Empty;
        string Transacionid = "NA";
        string TransactionFor = string.Empty;
        string TSPLTxnCode = string.Empty;
        string TSPLtxtITC = string.Empty;
        #endregion

        #region Get Payment Details
        feeAmount = (Session["Final_Amt"]).ToString();
        #endregion

        #region Payment Log for Different Transaction Id
        string TransactionCode = string.Empty;
        TransactionCode = lblOrderID.Text;          // This may be configured from Database for Different Running Number
        #endregion

        #region BillDesk Data Declaration
        string MerchantID = string.Empty;
        string UniTranNo = string.Empty;
        string NA1 = string.Empty;
        string txn_amount = string.Empty;
        string NA2 = string.Empty;
        string NA3 = string.Empty;
        string NA4 = string.Empty;
        string CurrencyType = string.Empty;
        string NA5 = string.Empty;
        string TypeField1 = string.Empty;
        string SecurityID = string.Empty;
        string NA6 = string.Empty;
        string NA7 = string.Empty;
        string TypeField2 = string.Empty;
        string additional_info1 = string.Empty;
        string additional_info2 = string.Empty;
        string additional_info3 = string.Empty;
        string additional_info4 = string.Empty;
        string additional_info5 = string.Empty;
        string additional_info6 = string.Empty;
        string additional_info7 = string.Empty;
        string ReturnURL = string.Empty;
        string ChecksumKey = string.Empty;
        #endregion

        #region Set Bill Desk Param Data
        //MerchantID = ConfigurationManager.AppSettings["MerchantID"];
        MerchantID = checkSumKey;

        UniTranNo = TransactionCode;
        txn_amount = feeAmount;
        CurrencyType = "INR";
        // SecurityID = ConfigurationManager.AppSettings["SecurityCode"];
        SecurityID = accCode;

        additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
        additional_info2 = ViewState["id_no"].ToString();  // Project Code
        additional_info3 = ViewState["RECIEPT"].ToString(); // Transaction for??
        additional_info4 = ViewState["info"].ToString(); // Payment Reason
        additional_info5 = feeAmount; // Amount Passed
        additional_info6 = ViewState["basicinfo"].ToString(); // stud basic details
        //additional_info6 = Transacionid; // Record Id
        additional_info7 = ViewState["SESSIONNO"].ToString();

        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx";

        //ReturnURL = "http://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "http://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx"; 
        //ReturnURL = "https://svcetest.mastersofterp.in/Academic/PhotoReval_Response.aspx";

        //ChecksumKey = ConfigurationManager.AppSettings["ChecksumKey"];
        ChecksumKey = checkSumKey;
        #endregion

        #region Generate Bill Desk Check Sum

        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append("DIRECT").Append("|");
        billRequest.Append("R").Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("F").Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ReturnURL);

        string data = billRequest.ToString();

        String hash = String.Empty;
        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        string msg = data + "|" + hash;


        Session["Order_id"] = UniTranNo;
        #endregion

        #region Post to BillDesk Payment Gateway

        // string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;
        string PaymentURL = responseUrl + msg;

        //Response.Redirect(PaymentURL, false);
        Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
        Response.Write("</script>");
        Response.Write("<script language='javascript' >");
        Response.Write("</script>");
        Response.Write("</form> ");
        Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
        #endregion
    }

    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    #endregion

    #region
    private void checkSubject()
    {
        DataSet ds = null;

        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "IDNO", "COURSENO", "EXAM_REGISTERED= 1 AND ISNULL(PREV_STATUS,0)  = 1 AND IDNO=" + lblName.ToolTip + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + ViewState["Sessionno"].ToString(), "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvFailCourse.Items)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                        if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                        {
                            chkAccept.Checked = true;
                            chkAccept.Enabled = false;
                            chkAccept.BackColor = System.Drawing.Color.Red;
                            i++;
                        }
                        else
                        {
                            chkAccept.Enabled = false;
                        }
                    }
                }
            }
        }
    }

    private void checkSubjectForAdmin()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "COURSENO", "IDNO", "EXAM_REGISTERED= 1 AND ISNULL(PREV_STATUS,0)  = 1 AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + ddlSession.SelectedValue + "AND SEMESTERNO=" + ddlBackLogSem.SelectedValue, "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvFailCourse.Items)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                        if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                        {
                            chkAccept.Checked = true;
                            chkAccept.Enabled = false;
                            chkAccept.BackColor = System.Drawing.Color.Red;
                            i++;
                        }
                        //else
                        //{
                        //    chkAccept.Enabled = false;
                        //} 
                    }
                }
            }
        }
    }

    #endregion

    static decimal coursecount = 0;
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            if (cbRow.Checked == true)
            {
                coursecount = coursecount + 1;
            }
            if (coursecount > 3)
            {
                chk.Checked = false;
            }
        }
    }
    protected void chkAccept_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        string totalCredits = Convert.ToString(objCommon.LookUp("ACD_TRRESULT H", " MAX(CUMMULATIVE_CREDITS)", "SESSIONNO IN (SELECT MAX(SESSIONNO) FROM ACD_TRRESULT H WHERE IDNO =" + Convert.ToString(lblName.ToolTip) + " ) AND IDNO=" + Convert.ToString(lblName.ToolTip) + "GROUP BY IDNO"));
        decimal cntCredit = 0;
        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            HiddenField hdnCredit = dataitem.FindControl("hdncurcredits") as HiddenField;
            if (cbRow.Checked == true)
            {
                coursecount = coursecount + 1;
                cntCredit = Convert.ToDecimal(totalCredits) + Convert.ToDecimal(hdnCredit.Value);
            }
            if (coursecount > 3 || (cntCredit > 60 && Convert.ToInt32(ViewState["idtype"]) == 1) || (cntCredit > 40 && Convert.ToInt32(ViewState["idtype"]) == 2))  //if course greater than 3 then msg display / credits more than 60 for idtype 1 and idtype 2 40 credits not eligible for reg as per open ticket 40642.
            {
                chk.Checked = false;
                objCommon.DisplayMessage(this.Page, "You have reached maximum limit", this.Page);
                return;
            }
        }
    }
}