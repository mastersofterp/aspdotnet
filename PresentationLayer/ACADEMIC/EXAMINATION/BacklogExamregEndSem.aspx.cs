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

public partial class ACADEMIC_EXAMINATION_BacklogExamregEndSem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
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
            ViewState["Sessionno"] = "";
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
                    FillExamFees();
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    pnlSearch.Visible = false;
                }
                else
                {
                    pnlStart.Enabled = false;
                    div_enrollno.Visible = false; //**********
                    div_btn.Visible = false; //**********
                }

                ViewState["count"] = null;
                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

            }

            ViewState["idno"] = 0;

            //Fill Current Session.
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");
        }

        divMsg.InnerHtml = string.Empty;
        //btnReport1.Visible = false;
        this.FillExamFees();
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else////with Proxy detection
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
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
      
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));

        if (dtr.Read())
        {
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        int Count = 0;
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
            objSR.SESSIONNO = Convert.ToInt32(Session["SESSIONNO"].ToString());
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        }

        objSR.IDNO = idno;
        objSR.REGNO = lblEnrollNo.Text;
        objSR.SCHEMENO = Convert.ToInt32(schemeno);
        objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.COURSENOS = string.Empty;
        string ExistCourses = string.Empty;
        string ddlValue = string.Empty;

        if (lvFailCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {

                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    Count++;
                }
              
            }

            if (Count > 2)
            {
                objCommon.DisplayMessage(this.updDetails, "You can not register more than two backlog course in Current Semester", this.Page);
                return;
            }

            if (objSR.COURSENOS.Length==0)
            {
                objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsback(objSR, ExistCourses, ddlValue,0);

            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this.updDetails, "Selected Backlog Exam Courses Saved Sucessfully", this.Page);
                ShowDetails();
                BtnPrntChalan.Visible = false;
            }

        }


    }

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

    /// <summary>
    /// Show Student Detail
    /// </summary>
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

                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

                        hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip) + " and a.EXAM_REGISTERED=1 and isnull(a.CANCEL,0)=0");

                        ViewState["IDNO"] = lblName.ToolTip;

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        DataSet dasem=null;
                        dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL' and regno='" + lblEnrollNo.Text + "'", "SEMESTERNO");

                        string BacklogSem = string.Empty;
                        if ((Convert.ToInt32(lblSemester.ToolTip)) % 2 == 0)
                        {
                            //Bind Even Semester Backlog Course.
                            for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
                            {
                                if (BacklogSem != string.Empty && Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 == 0)
                                {
                                    BacklogSem += ",";
                                }
                                if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 == 0)
                                {
                                    BacklogSem += dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString();
                                }
                            }

                            ////--temp block code---
                            //for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
                            //{
                            //    BacklogSem += dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString();
                            //}
                            //if (BacklogSem != string.Empty)
                            //{
                            //    StudentBacklogSubject(BacklogSem);
                            //}
                        }
                        else
                        {
                            //Bind Odd Semester Backlog Course.
                            for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
                            {
                                if (BacklogSem != string.Empty && Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 != 0)
                                {
                                    BacklogSem += ",";
                                }
                                if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 != 0)
                                {
                                    BacklogSem += dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString();
                                }
                            }
                        }

                        if (BacklogSem != string.Empty)
                        {
                            StudentBacklogSubject(BacklogSem);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updDetails, "Backlog Semester is not Present.!", this.Page);
                            return;
                        }

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

    #region Approve Status
    protected void btnExamRegistrationApprove_Click(object sender, EventArgs e)
    {
        //StudentRegistration objSRegist = new StudentRegistration();
        //StudentRegist objSR = new StudentRegist();
        //int idno = Convert.ToInt32(lblName.ToolTip);
        //int schemeno = Convert.ToInt32(lblScheme.ToolTip);

        //objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        //objSR.IDNO = idno;
        //objSR.REGNO = lblEnrollNo.Text;
        //objSR.SCHEMENO = Convert.ToInt32(schemeno);
        //objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
        //objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        //objSR.UA_NO = Convert.ToInt32(Session["userno"]);

        //CustomStatus cs = (CustomStatus)objSRegist.UpdateBackLogExamRegisteredStatus(objSR);
        //if (cs == CustomStatus.RecordSaved)
        //{
        //    objCommon.DisplayMessage(this.updDetails, "Backlog Exam Registration Approve Successfully.", this.Page);
        //    btnReport1.Visible = true;
        //    btnExamRegistrationApprove.Visible = false;
        //    divRegMsg.Visible = true;
        //    divAppPenRegMsg.Visible = false;
        //}
        //else
        //{
        //    objCommon.ShowError(Page, "Server Unavailable.");
        //}

        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        int Count = 0;
      
        idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        int schemeno = Convert.ToInt32(lblScheme.ToolTip);

        objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
       

        objSR.IDNO = idno;
        objSR.REGNO = lblEnrollNo.Text;
        objSR.SCHEMENO = Convert.ToInt32(schemeno);
        objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.COURSENOS = string.Empty;
        string ExistCourses = string.Empty;
        string ddlValue = string.Empty;

        if (lvFailCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {

                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    Count++;
                }

            }

            if (Count > 2)
            {
                objCommon.DisplayMessage(this.updDetails, "You can not register more than two backlog course in Current Semester", this.Page);
                return;
            }

            if (objSR.COURSENOS.Length == 0)
            {
                objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsback(objSR, ExistCourses, ddlValue,1);

            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this.updDetails, "Selected Backlog Exam Courses Saved & Approved Sucessfully", this.Page);
                ShowDetails();
            }

        }

    }
    #endregion

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
        //ShowReport("ExamRegistrationSlip", "PaymentReceipt_Exam_Registered_Courses.rpt");
        ShowReport("ExamRegistrationSlip", "rptBackLogCourseRegSlip.rpt");
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
            divCourses.Visible = true;
           
        }
        divNote.Visible = false;
        
        trFailList.Visible = false;       
        //trCourseList.Visible = false;
        ShowDetails();
    }
    
    private void ShowReport1(string rptName, int dcrNo, int studentNo, string copyNo, int param)
    {
        try
        {
            btnReport.Visible = false;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
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
            sb.Append(@"window.open('" + url + "','','" + features + "');");//int sessionno = Convert.ToInt32(Session["currentsession"]);
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);//int idno = 0;
            
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
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //int sessionno = Convert.ToInt32(Session["currentsession"]);
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
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) +",@UserName=" + Session["username"].ToString() ;
            }
            else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + ",@UserName=" + Session["username"].ToString();
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void studentlist(string BackLogSem)
    {
        //Fail subjects List
        int count = 0;
        int idno = 0;
        string subReg = string.Empty;
        string CheckRecord = string.Empty;

        int schemeno = Convert.ToInt32(lblScheme.ToolTip);
        StudentController objSC = new StudentController();
        StudentRegistration objSRegist = new StudentRegistration();
        DataSet dsFailSubjects=null;
       

        if (ViewState["usertype"].ToString() == "2")
        {
            btnSubmit.Visible = true;

            Session["SESSIONNO"] = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            idno = Convert.ToInt32(Session["idno"]);
            int accept = 0;
            accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + " AND SEMESTERNO in(" + lblSemester.ToolTip + ") AND isnull(PREV_STATUS,0)=1 AND ACCEPTED=1 AND REGISTERED=1 and isnull(EXAM_REGISTERED,0)=0 AND isnull(CANCEL,0) = 0 AND IDNO=" + idno));
            if (accept > 0)
            {
                //lvFailCourse.Enabled = false;
                //btnSubmit.Enabled = false;
                //divRegMsg.Visible = false;
                //btnReport1.Visible = false;
                //divAppPenRegMsg.Visible = true;
                lvFailCourse.Enabled = true;
                btnSubmit.Enabled = true;
                divRegMsg.Visible = false;
                btnReport1.Visible = false;
                divAppPenRegMsg.Visible = true;
            }

            accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + " AND SEMESTERNO in(" + lblSemester.ToolTip + ") AND isnull(PREV_STATUS,0)=1 AND ACCEPTED=1 AND REGISTERED=1 AND isnull(EXAM_REGISTERED,0)=1 AND isnull(CANCEL,0) = 0 AND IDNO=" + idno));
            if (accept>0)
            {
                divRegMsg.Visible = true;
                btnReport1.Visible = true;
                lvFailCourse.Enabled = false;
                btnSubmit.Enabled = false;
                divAppPenRegMsg.Visible = false;
            }
           
            btnExamRegistrationApprove.Visible = false;
            dsFailSubjects = objSRegist.GetStudentBacklogExamSubjects(idno, BackLogSem, Convert.ToInt32(Session["SESSIONNO"].ToString()), Convert.ToInt32(lblSemester.ToolTip));
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            btnSubmit.Visible = false;
            btnExamRegistrationApprove.Visible = true;
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            ViewState["idno"] = idno;

            int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO in(" + lblSemester.ToolTip + ") AND isnull(PREV_STATUS,0)=1 AND ACCEPTED=1  AND isnull(EXAM_REGISTERED,0)=1 AND REGISTERED=1 AND CANCEL = 0 AND IDNO=" + idno));
            if (accept > 0)
            {
                divRegMsg.Visible = true;
                btnReport1.Visible = true;
                divAppPenRegMsg.Visible = false;
                btnExamRegistrationApprove.Enabled = false;
                lvFailCourse.Enabled = false;
            }
            else
            {
                divRegMsg.Visible = false;
                btnReport1.Visible = false;
                divAppPenRegMsg.Visible = false;
            }


            //Approve Button Show as per student course Registration.
            int ApproveStatus = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO in(" + lblSemester.ToolTip + ") AND isnull(PREV_STATUS,0)=1 AND ACCEPTED=1 AND REGISTERED=1 AND isnull(EXAM_REGISTERED,0)=0 AND CANCEL = 0 AND IDNO=" + idno));
            if (ApproveStatus > 0)
            {
                btnExamRegistrationApprove.Enabled = true;
                divRegMsg.Visible = false;
                btnReport1.Visible = false;
                divAppPenRegMsg.Visible = true;
            }
           

            dsFailSubjects = objSRegist.GetStudentBacklogExamSubjects(idno, BackLogSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblSemester.ToolTip));
        }

       
        if (dsFailSubjects != null && dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            trFailList.Visible = true;
           
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            
            ViewState["count"] = count;
            lvFailCourse.Visible = true;
            divCourses.Visible = true;
            
            //btnSubmit.Visible = true;
            btnRemoveList.Visible = true;
            trNote.Visible = true;
            lblcred.Text = hdfcurrcredits.Value;
            credits.Visible = true;
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
        
            lvFailCourse.Visible = false;
        
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            trNote.Visible = false;
            lblcred.Text = "0";
            credits.Visible = false;
        }
      
    }

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

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

    //online pay
    private void SendTransaction()
    {
        // hdnCollege.Value = dr["COLLEGE_CODE"].ToString();

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
            // objCommon.DisplayUserMessage(updBulkReg, "Error occurred while fetching college details!", this.Page);
            return;
        }



        int TotalAmount = Convert.ToInt32(totamtpay.Text);
        string txnrefno = string.Empty; string txnrefno1 = string.Empty;
        string amt = string.Empty;
        string amt1 = string.Empty;
        if ((TotalAmount != null || TotalAmount != 0) && (Session["idno"] != null || Session["idno"] != ""))
        {
            amt1 = encryptFile(TotalAmount.ToString(), ds.Tables[0].Rows[0]["AESKey"].ToString());//******************UNCOMMENT THIS LINE FOR LIVE SERVER*******************************
            //amt1 = encryptFile("1", ds.Tables[0].Rows[0]["AESKey"].ToString());      
            amt = TotalAmount.ToString();      //******************FOR TESTING ONLY*******************************
            submerchant_id1 = encryptFile(Session["idno"].ToString().Trim(), ds.Tables[0].Rows[0]["AESKey"].ToString());
            submerchant_id = Session["idno"].ToString().Trim();
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
        string mandatory_fields = "";
        ////string mandatory_fields1 = "";//****************

        string paymode = encryptFile("9", ds.Tables[0].Rows[0]["AESKey"].ToString());
        //  string return_url = encryptFile("http://localhost:27007/PresentationLayer/response.aspx", ds.Tables[0].Rows[0]["AESKey"].ToString());
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
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(ViewState["IDNO"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ddlBackLogSem.SelectedValue) + ir);
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


    //online pay
    private void SubmitPaymentDetails()
    {
        int semester = 0;
        string COM_CODE1 = string.Empty;

        CreateCustomerRef();
        GetNewReceiptNo();
        string result1 = string.Empty;
        semester = Convert.ToInt32(lblSemester.ToolTip);
        ActivityController objActController = new ActivityController();
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
        sessionno = Convert.ToInt32(Session["SESSIONNO"]);
        int feeitemid = 1;
        DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);
      
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();

            if (amountth.Contains('.'))
            {
                int index = amountth.IndexOf('.');
                result1 = amountth.Substring(0, index);
            }
        }
        double totalamount = 0.00;
        string totamt = string.Empty;
        if (lvFailCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    totalamount = totalamount + Convert.ToDouble(result1);
                }
            }
            totamt = totalamount.ToString();
        }
        if (radiolist.SelectedValue != "")
        {
            if (radiolist.SelectedValue == "1")
            {
                int result = 0;

                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToString(lblOrderID.Text), 1, "0", totamt, "0","0","0","0","0", totamt, "BE");
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
                //result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToString(lblOrderID.Text), 2, "0", totamt, "0", "0", "0", totamt, "");
                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlBackLogSem.SelectedValue), Convert.ToString(lblOrderID.Text), 2, "0", totamt, "0", "0", "0","0","0", totamt, "BE");
                //Session["RECIEPT_CODE"] = "EF";
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
            // rdbPayOption.Focus();
        }
    }

    protected void btnRemoveList_Click(object sender, EventArgs e)
    {
        // trCourseList.Visible = false;
        trFailList.Visible = false;
        // trAllCoureWithoutPass.Visible = false;
        btnSubmit.Visible = false;
        btnRemoveList.Visible = false;
        btnReport.Visible = false;
        radiolist.Visible = false;
        BtnPrntChalan.Visible = false;
        ddlBackLogSem.SelectedValue = "0";
        trNote.Visible = false;
        hdfcurrcredits.Value = objCommon.LookUp("acd_student_result a inner join acd_student s on (a.idno=s.idno and a.semesterno=s.semesterno)", "sum(a.CREDITS)", "a.idno=" + Convert.ToInt32(lblName.ToolTip) + " and a.EXAM_REGISTERED=1 and isnull(a.CANCEL,0)=0");
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

    //protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND RECON=1 AND  CAN = 0 AND COM_CODE='BE' AND IDNO=" + ViewState["IDNO"]));
    //    if (exreg > 0)
    //    {
    //        objCommon.DisplayMessage(this.updDetails, "Selected Semester Exam Registration Already Done !", this.Page);
          
    //        btnReport1.Visible = true;
    //        btnSubmit.Visible = false;
    //    }
    //    else
    //    {
    //        this.studentlist();
    //        btnPrcdToPay.Visible = false;
    //        totamtpay.Text = "";
           
    //    }
    //}

    private void StudentBacklogSubject(string BacklogSem)
    {
        int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO IN(" + BacklogSem + ") AND RECON=1 AND  CAN = 0 AND COM_CODE='BE' AND IDNO=" + ViewState["IDNO"]));
        if (exreg > 0)
        {
            objCommon.DisplayMessage(this.updDetails, "Backlog Registration Already Done for Selected Session.!", this.Page);

            btnReport1.Visible = true;
            btnSubmit.Visible = false;
        }
        else
        {
            this.studentlist(BacklogSem);
            btnPrcdToPay.Visible = false;
            totamtpay.Text = "";

        }
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
        SubmitPaymentDetails();
        SendTransaction();
    }

    protected void BtnPrntChalan_Click(object sender, EventArgs e)
    {
        SubmitPaymentDetails();
       
        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "AND RECIEPT_CODE='EF'"));
       
        ShowReport1("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["IDNO"]), "1", 1);
       
    }

    private void FillExamFees()
    {
        ActivityController objActController = new ActivityController();

        string session = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"] + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
         //int  sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"] + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
        int sessionno=0;
        if (session != string.Empty)
        {
            sessionno = Convert.ToInt32(session);
        }

        int feeitemid = 1;
        DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno,feeitemid);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            if (amountth.Contains('.'))
            {
                int index = amountth.IndexOf('.');
                string result = amountth.Substring(0, index);
                
                lblstuendth.Text = result;
                lblstuendth.Visible = true;
            }
            
        }
    }

}