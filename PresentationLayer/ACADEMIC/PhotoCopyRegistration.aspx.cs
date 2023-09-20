//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHOTOCOPY REGISTRATION BY STUDENT AND ADMIN                                     
// CREATION DATE : 05-APRIL-2016
// ADDED BY      : MR.MANISH WALDE
// MODIFIED BY   : SACHIN A
// MODIFIED DATE : 25-AUG-2022
// MODIFIED DESC : New Enhancement As per Req. & Add BillDesk Razor Payment Gateway Project                                           
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
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;


public partial class ACADEMIC_PhotoCopyRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();
    FeeCollectionController ObjFCC = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();
    BacklogRegistration objBackReg = new BacklogRegistration();

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                this.PopulateDropDownList();

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;
                ViewState["action"] = "add";
                ViewState["idno"] = "0";


                //ViewState["sessionno"] = "41";   //Added for test dt on 08092023

                //Check for Activity On/Off for Reval registration.
                if (Session["usertype"].ToString() == "2")
                {
                    divRollNo.Visible = false;
                    if (CheckRegistrationActivity() == false)   //Added for test dt on 08092023
                        return;
                    this.ShowPhotoCopyDetails();
                }
                else
                {
                    if (CheckActivityAdmin() == false)
                        return;
                    divRollNo.Visible = true;

                }

                divCourses.Visible = true;
                tblSession.Visible = true;
                txtRollNo.Text = string.Empty;

                //if (Convert.ToInt32(Session["OrgId"]) == 9)
                //{
                if (Session["usertype"].ToString() == "2")
                {
                    //if (CheckActivityStudent() == false)
                    //    return;
                    ddlSession.Visible = false;
                    btnShow.Visible = false;
                    btnCancel.Visible = false;
                    divSession.Visible = false;

                }
                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    if (Session["usertype"].ToString() == "2")
                    {
                        btnChallan.Visible = false;
                    }
                    else
                    {
                        btnChallan.Visible = true;
                    }
                }
                else
                {
                    btnChallan.Visible = false;
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label - 
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
            }
        }
        //Set the Page Title
        Page.Title = Session["coll_name"].ToString();
        divMsg.InnerHtml = string.Empty; 
    }
   
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            objCommon.RecordActivity(int.Parse(Session["userno"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhotoCopyRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show thePW page
            Response.Redirect("~/notauthorized.aspx?page=PhotoCopyRegistration.aspx");
        }
    }

    private bool CheckActivity()
    {
        try
        {
            bool ret = true;
            int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + Convert.ToString(Session["userno"])));
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "DEGREENO,BRANCHNO,SEMESTERNO,COLLEGE_ID", "ISNULL(ADMCAN,0)=0 AND IDNO =" + idno, string.Empty);
            string degreeno = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
            string branchno = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
            string semesterno = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            string collegeid = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + "AND COLLEGE_IDS=" + collegeid + "and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            ViewState["sessionno"] = sessionno;

            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                // DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), degreeno, branchno, semesterno);                                 

                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }


    private bool CheckActivityStudent()
    {
        try
        {
            bool ret = true;
            //string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");


            string degreeno = objCommon.LookUp("ACD_STUDENT", "DISTINCT DEGREENO", "DEGREENO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));

            string branchno = objCommon.LookUp("ACD_STUDENT", "DISTINCT BRANCHNO", "BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));
            string collegeid = objCommon.LookUp("ACD_STUDENT", "DISTINCT COLLEGE_ID", "BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));
            //string semesterno = objCommon.LookUp("ACD_STUDENT", "DISTINCT SEMESTERNO", "SEMESTERNO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));

            // string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + " AND COLLEGE_IDS=" + collegeid + " AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            string semesterno = objCommon.LookUp("ACD_STUDENT_RESULT_HIST H", "max(semesterno)SEMESTER", "ISNULL(CANCEL,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]) + " GROUP BY SESSIONNO");


            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + " AND COLLEGE_IDS LIKE '%" + collegeid + "%' AND DEGREENO LIKE '%" + degreeno + "%' AND BRANCH LIKE '%" + branchno + "%' AND SEMESTER LIKE '%" + semesterno + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");

            if (sessionno != "")
            {
                ViewState["sessionno"] = sessionno;
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), degreeno, branchno, semesterno);

                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }
    }

    private void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%') AND ISNULL(IS_ACTIVE,0)= 1 AND FLOCK=1", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");

        //ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
    }



    #endregion

    #region Show Functionality

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Session["usertype"].ToString()))
            {
                int idno = 0;
                FeeCollectionController feeController = new FeeCollectionController();
                if (Session["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"]);

                    divRollNo.Visible = false;
                }
                else
                {
                    idno = feeController.GetStudentIdByEnrollmentNo(txtRollNo.Text.Trim());
                    divRollNo.Visible = true;
                }
                //ViewState["idno"] = Convert.ToInt32(Session["idno"]);
                ViewState["idno"] = idno;


                if (!string.IsNullOrEmpty(ViewState["idno"].ToString()))
                {
                    string TRRESULTLOCK = string.Empty; 

                    string sp_proc = "PKG_ACD_CHECK_RESULT_DATA";
                    string sp_para = "@P_UA_NO,@P_SESSIONNO,@P_IDNO,@P_STATUS";
                    string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(ViewState["sessionno"]) + "," + Convert.ToInt32(ViewState["idno"]) + "," + 1 + "";   //Status 1 for Photocopy TotAmount

                    DataSet dsEligible = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                    if (dsEligible.Tables[0].Rows.Count > 0 && dsEligible.Tables != null && dsEligible.Tables[0] != null)
                    {
                        TRRESULTLOCK = dsEligible.Tables[0].Rows[0]["LOCK"].ToString();
                    }

                    //TRRESULTLOCK = (objCommon.LookUp("ACD_TRRESULT", "DISTINCT ISNULL(LOCK,0) LOCK", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO= " + Convert.ToInt32(ViewState["idno"]) + ""));
                    if (TRRESULTLOCK != "")//to check result published or not
                    {
                        if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
                        {
                            objCommon.DisplayMessage(updDetails, "Student with Univ. Reg. No. Or Admission No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                            return;
                        }

                        if (Session["usertype"].ToString() == "2")
                        {
                            this.ShowDetails();

                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = false;
                            //txtRollNo.Enabled = false;

                            lblTotalAmount.Text = "0";
                            CourseAmt = 0;
                            divTotalCourseAmount.Visible = false;

                            // ViewState["action"] = "edit";
                            //FillSemester();
                            ddlSemester.Enabled = false;
                            divSem.Visible = false;
                            //divNote.Visible = true;
                            divRegCourses.Visible = false;
                            //divRegisteredCoursesTotalAmt.Visible = false;

                            if (ddlSession.SelectedIndex > 0)
                            {
                                BindCourseListForPHOTOCOPY();
                                //IsPHOTOCOPYApproved();
                            }
                            else
                            {
                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                            }

                        }
                        else if (Session["usertype"].ToString() == "1")         //Added dt on 23112022 for Admin login apply
                        {
                            this.ShowDetails();

                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = false;
                            //txtRollNo.Enabled = false;

                            lblTotalAmount.Text = "0";
                            CourseAmt = 0;
                            divTotalCourseAmount.Visible = false;

                            // ViewState["action"] = "edit";
                            //FillSemester();
                            ddlSemester.Enabled = false;
                            divSem.Visible = false;
                            //divNote.Visible = true;
                            divRegCourses.Visible = false;
                            //divRegisteredCoursesTotalAmt.Visible = false;

                            if (ddlSession.SelectedIndex > 0)
                            {
                                BindCourseListForPHOTOCOPY();
                                //IsPHOTOCOPYApproved();
                            }
                            else
                            {
                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                            }
                            this.ReportStatus();
                        }
                        else
                        {
                            //to check already record or not of that particular student
                            string RevalCount = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DISTINCT 1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 and CAST(APP_TYPE AS NVARCHAR)='PHOTO COPY' ");

                            if (RevalCount == "1")
                            {
                                string RECON = objCommon.LookUp("ACD_DCR", "Distinct isnull(RECON,0) RECON", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='PRF' ");

                                if (RECON == "1" || RECON == "True")
                                {
                                    this.ShowDetails();
                                    BindCourseListForPHOTOCOPY();
                                    ddlSemester.Enabled = false;
                                    divSem.Visible = false;
                                    //divNote.Visible = true;

                                    txtRollNo.Enabled = false;
                                    ddlSession.Enabled = false;
                                    btnPrintRegSlip.Visible = true;
                                    if (Session["usertype"].ToString() == "2")         //Added dt on 23112022 for Admin login apply
                                    {
                                        btnPaymentReport.Visible = true;
                                    }
                                    else
                                    {
                                        btnPaymentReport.Visible = false;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updDetails, "Photocopy Registration is Pending of this Student!", this.Page);
                                    ddlSession.SelectedIndex = 0;
                                    txtRollNo.Text = "";
                                    ddlSession.Focus();

                                    btnSubmit.Visible = false;
                                    btnPrintRegSlip.Visible = false;
                                    lvCurrentSubjects.DataSource = null;
                                    lvCurrentSubjects.DataBind();
                                    lvCurrentSubjects.Visible = false;
                                    tblInfo.Visible = false;
                                    divRegCourses.Visible = false;
                                    // divNote.Visible = false;
                                    lblTotalAmount.Text = "0";
                                    CourseAmt = 0;
                                    divTotalCourseAmount.Visible = false;
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(updDetails, "Photocopy Registration is Pending of this Student!", this.Page);
                                ddlSession.SelectedIndex = 0;
                                txtRollNo.Text = "";
                                ddlSession.Focus();

                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                                tblInfo.Visible = false;
                                divRegCourses.Visible = false;
                                //divNote.Visible = false;
                                lblTotalAmount.Text = "0";
                                CourseAmt = 0;
                                divTotalCourseAmount.Visible = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetails, "Result Not Published Yet!", this.Page);
                        ddlSession.SelectedIndex = 0;
                        return;
                    }

                }
            }
        }
        catch { }
    }

    //Show Selected Student Information 
    private void ShowDetails()
    {
        try
        {

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER COLL ON (S.COLLEGE_ID = COLL.COLLEGE_ID) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH, COLL.COLLEGE_NAME,S.STUDENTMOBILE,S.COLLEGE_ID", "ISNULL(S.ADMCAN,0)=0 AND S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    lblCollegeName.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    hfDegreeNo.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                    ViewState["college_id"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

                    tblInfo.Visible = true;
                    divSem.Visible = true;
                    divCourses.Visible = true;
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

    //private void FillSemester()
    //{
    //    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + ViewState["idno"].ToString(), "SR.SEMESTERNO");
    //}

    private void BindCourseListForPHOTOCOPY()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            sessionno = Convert.ToInt32(ViewState["sessionno"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        DataSet dsCurrCourses = null;

        //Show Courses for Photo Copy
        dsCurrCourses = objSC.GetCourseFor_RevalOrPhotoCopy(Convert.ToInt32(ViewState["idno"]), sessionno, 1);

        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            divAllCoursesFromHist.Visible = true;
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;

            Session["semester_reg"] = dsCurrCourses.Tables[0].Rows[0]["SEMESTERNO"].ToString() == string.Empty ? "0" : dsCurrCourses.Tables[0].Rows[0]["SEMESTERNO"].ToString();

            string RECON = objCommon.LookUp("ACD_DCR", "Distinct isnull(RECON,0) RECON", "SESSIONNO=" + sessionno + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='PRF' ");

            if (RECON == "1" || RECON == "True")
            {
                if (Session["usertype"].ToString() == "2")
                {
                    checkSubject();
                }
                else
                {
                    checkSubjectForAdmin();
                }

                string subcount = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DISTINCT 1)", "SESSIONNO=" + sessionno + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 AND CAST(APP_TYPE AS NVARCHAR)='PHOTO COPY' ");
                if (subcount == "1")
                {
                    //#region Semesternos
                    //string semesternos = string.Empty;
                    //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                    //{
                    //    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    //    Label lblSemesterno = dataitem.FindControl("lblSEMSCHNO") as Label;
                    //    if (cbRow.Checked == true)
                    //    {
                    //        semesternos += lblSemesterno.ToolTip;
                    //    }
                    //}
                    //#endregion

                    #region TOTALAMOUNT
                    string TOTALAMOUNT = string.Empty;
                    string sp_proc = "PKG_ACD_GET_EXAM_REG_AMOUNT";
                    string sp_para = "@P_UA_NO,@P_SESSIONNO,@P_SEMESTERNO,@P_IDNO,@P_DEGREENO,@P_PAGE_LINK,@P_STATUS";
                    string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + sessionno + "," + Convert.ToString(Session["semester_reg"]) + "," + Convert.ToInt32(ViewState["idno"]) + "," + hfDegreeNo.Value + "," + Request.QueryString["pageno"].ToString() + "," + 3 + "";   //Status 3 for Photocopy registered courses fee  

                    DataSet dsTotAmt = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                    if (dsTotAmt.Tables[0].Rows.Count > 0 && dsTotAmt.Tables != null && dsTotAmt.Tables[0] != null)
                    {
                        TOTALAMOUNT = dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString() == null ? "0" : dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                    }
                    #endregion

                    divTotalCourseAmount.Visible = true;
                    lblTotalAmount.Text = TOTALAMOUNT;

                    if (Session["usertype"].ToString() == "2")
                    {
                        btnSubmit.Visible = false;
                        btnPrintRegSlip.Visible = true;
                        divRegCourses.Visible = false;
                        btnPaymentReport.Visible = true;
                    }
                    else
                    {
                        int count = 0;
                        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                        {
                            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                            if (cbRow.Checked == true)
                            {
                                count = Convert.ToInt16(count) + Convert.ToInt16(1);
                            }
                        }

                        //if (count == 5)
                        //{
                        //    btnSubmit.Visible = false;
                        //    btnPrintRegSlip.Visible = true;
                        //    divRegCourses.Visible = false;
                        //    checkSubject();
                        //}  
                        btnSubmit.Visible = true;
                        btnPrintRegSlip.Visible = true;
                        divRegCourses.Visible = false;
                        if (Session["usertype"].ToString() == "2")
                        {
                            btnPaymentReport.Visible = true;
                        }
                        else
                        {
                            btnPaymentReport.Visible = false;
                        }
                        this.ReportStatus();
                    }

                }
                else
                {
                    btnSubmit.Visible = true;
                    btnPrintRegSlip.Visible = false;
                    divRegCourses.Visible = false;
                    this.ReportStatus();
                }
            }
            else
            {
                btnSubmit.Visible = true;
                btnPrintRegSlip.Visible = false;
                //divRegisteredCoursesTotalAmt.Visible = false;
                divRegCourses.Visible = false;
                this.ReportStatus();
            }
        }
        else
        {
            btnSubmit.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;

            //divRegisteredCoursesTotalAmt.Visible = false;
            divRegCourses.Visible = false;
            objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }
    }

    private void checkSubject()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "IDNO", "CAST(APP_TYPE AS NVARCHAR) = 'PHOTO COPY' AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + Convert.ToString(ViewState["sessionno"]), "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvCurrentSubjects.Items)
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
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "IDNO", "CAST(APP_TYPE AS NVARCHAR) = 'PHOTO COPY' AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + Convert.ToString(ViewState["sessionno"]), "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvCurrentSubjects.Items)
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


    //private void IsPHOTOCOPYApproved()
    //{
    //    string ApproveStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "ISNULL(REV_APPROVE_STAT,0) = 1 AND APP_TYPE = 'PHOTO COPY' AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue));
    //    if (ApproveStatus != "0")
    //    {
    //        btnPrintRegSlip.Visible = true;
    //    }

    //}


    //private void IsPHOTOCOPYRegistered()//Added by Priority on date 11-12-2015
    //{
    //    string RegisteredStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "REV_APPROVE_STAT=0 AND APP_TYPE = 'PHOTO COPY' AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue));
    //    if (RegisteredStatus != "0")
    //    {
    //        btnPrintRegSlip.Visible = true;
    //    }
    //    else
    //    {
    //        btnPrintRegSlip.Visible = false;
    //    }
    //}


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            BindCourseListForPHOTOCOPY();
            //IsPHOTOCOPYApproved();
        }
        else
        {
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
        }
        lblTotalAmount.Text = "0";
        CourseAmt = 0;
        divTotalCourseAmount.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        //ddlSession.Enabled = false;
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        ddlSession.Enabled = true;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        tblInfo.Visible = false;
        divSem.Visible = false;
        btnSubmit.Visible = false;
        lblErrorMsg.Text = string.Empty;

        lblTotalAmount.Text = "0";
        CourseAmt = 0;
        divTotalCourseAmount.Visible = false;
        //divNote.Visible = false;
        btnPrintRegSlip.Visible = false;

        divAllCoursesFromHist.Visible = false;

        divRegCourses.Visible = false;
        //divRegisteredCoursesTotalAmt.Visible = false;
    }

    #endregion

    #region Submit Functionality

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string courseno = "";
            courseno = getcourseno();
            if (courseno == "0")
            {
                objCommon.DisplayMessage(updDetails, "Please Select At least One Subject from list!!", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true)
                {
                    count = Convert.ToInt16(count) + Convert.ToInt16(1);
                }
            }
            //if (count <= 5)
            //{
            SubmitCourses();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updDetails, "Please Select only 5 Subjects !! You Selected : " + count + " Subjects !!", this.Page);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //to get courseno having Approval_status as Not Applied
    private string getcourseno()
    {
        try
        {
            string retCNO = string.Empty;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = item.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked && cbRow.Enabled)
                {
                    if (retCNO.Length == 0) retCNO = ((item.FindControl("lblCCode")) as Label).ToolTip.ToString();
                    else
                        retCNO += "," + ((item.FindControl("lblCCode")) as Label).ToolTip.ToString();
                }
            }
            if (retCNO.Equals(""))
            {
                return "0";
            }
            else
            {
                return retCNO;
            }
        }
        catch { return null; }
    }

    public void SubmitCourses()
    {
        try
        {
            int result = 0;
            Boolean selection = false;
            int opertion = 0;
            string COURSENOS = string.Empty, EXTERMARKS = string.Empty, CCODES = string.Empty, SEMESTERNOS = string.Empty, GRADES = string.Empty;
            ViewState["Coursenos"] = string.Empty;
            ViewState["Extermarks"] = string.Empty;
            ViewState["Codes"] = string.Empty;
            ViewState["Smesterno"] = string.Empty;
            ViewState["Extermarks"] = string.Empty;
            ViewState["ExtermarkTot"] = string.Empty;
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                //HiddenField hfMarks = dataitem.FindControl("hfMarks") as HiddenField;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    selection = true;
                    COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    GRADES += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                    CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                    SEMESTERNOS += (dataitem.FindControl("lblSEMSCHNO") as Label).ToolTip + "$";

                    EXTERMARKS += ((dataitem.FindControl("lblMarks")) as Label).Text + "$";
                }
                objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblCourseName") as Label).ToolTip);
            }

            objSR.COURSENOS = COURSENOS.TrimEnd('$');
            objSR.EXTERMARKS = GRADES.TrimEnd('$');
            objSR.CCODES = CCODES.TrimEnd('$');
            objSR.SEMESTERNOS = SEMESTERNOS.TrimEnd('$');
            EXTERMARKS = EXTERMARKS.TrimEnd('$');

            if (!selection)
            {
                objSR.COURSENOS = "0";
                objSR.EXTERMARKS = "0";
                objSR.CCODES = "0";
                objSR.SEMESTERNOS = "0";
            }

            string college_id = objCommon.LookUp("ACD_STUDENT", "DISTINCT COLLEGE_ID", "IDNO=" + ViewState["idno"].ToString());

            objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionno"]);
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = college_id;                         //Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            //objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);

            ViewState["Coursenos"] = objSR.COURSENOS.ToString();    //Added on 25082022    
            ViewState["Grades"] = objSR.EXTERMARKS.ToString();
            ViewState["ccodes"] = objSR.CCODES.ToString();
            ViewState["Semesternos"] = objSR.SEMESTERNOS.ToString();
            ViewState["Extermarks"] = EXTERMARKS.ToString();
            ViewState["Schemeno"] = objSR.SCHEMENO.ToString();
            Session["semesternos"] = objSR.SEMESTERNOS.ToString();
            ViewState["ExtermarkTot"] = EXTERMARKS.ToString();
            ViewState["Semesterno"] = objSR.SEMESTERNOS;
          
            ////to generate demand and dcr
            //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER COLL ON (S.COLLEGE_ID = COLL.COLLEGE_ID) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,B.BRANCHNO,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,YEAR,PTYPE", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);

            if ((Session["usertype"].ToString() == "2") && (Convert.ToInt32(Session["OrgId"]) == 2))//for student
            {
                result = objSReg.AddPhotoCopyRegisteration(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));    //,Convert.ToString(ViewState["ORDERID"]));           //For Crescent Client
                // result = objSReg.AddPhotoCopyRegisterationAtlas(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]), Convert.ToString(ViewState["ORDERID"]));         //Add New Method dt on 07112022
                btnChallan.Visible = false;
            }
            else if ((Session["usertype"].ToString() == "2") && (Convert.ToInt32(Session["OrgId"]) == 6))
            {
                result = objBackReg.AddPhotoCopyRegisteration_Rcpiper(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
                btnChallan.Visible = false;
            }
            else if (Session["usertype"].ToString() == "2")
            {
                result = objSReg.AddPhotoCopyRegisteration(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
                btnChallan.Visible = false;
            }
            else //for admin
            {
                result = objSReg.AddPhotoRevalRegByAdmin(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
            }


            if (result > 0)
            {
                lblTotalAmount.Text = "0";
                CourseAmt = 0;
                divTotalCourseAmount.Visible = false;
                btnSubmit.Visible = false;

                if (Convert.ToInt32(Session["OrgId"]) == 2)   //Added 03112022 for challan report 
                {
                    if (Session["usertype"].ToString() == "2")
                    {
                        objCommon.DisplayMessage(updDetails, "Photo Copy Details Applied Successfully.But will be confirm only after Successful Payment", this.Page);
                        btnChallan.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetails, "Photo Copy Details Applied Successfully", this.Page);
                        btnChallan.Visible = true;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Photo Copy Details Applied Successfully.But will be confirm only after Successful Payment", this.Page);
                }
                /////////////////////////////////////////////
                //to hide all courses
                //BindCourseListForPHOTOCOPY();
                divAllCoursesFromHist.Visible = false;


                /////////////////////////////////////////////
                divRegCourses.Visible = true;

                LoadTotalRegisteredAmount();

                divTotalCourseAmount.Visible = true;

                //divRegisteredCoursesTotalAmt.Visible = true;
                //ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt");

                if (Session["usertype"].ToString() == "2")
                {
                    divRollNo.Visible = false;
                    btnPayOnline.Visible = true;
                    // btnChallan.Visible = false;
                    //to show registeredcourses
                    BindRegisteredCoursesofPHOTOCOPY();
                }
                else
                {
                    divRollNo.Visible = true;
                    btnPayOnline.Visible = false;
                    btnChallan.Visible = false;
                    BindCourseListForPHOTOCOPY();
                   // BindRegisteredCoursesofPHOTOCOPY();
                }

                if (Convert.ToInt32(Session["OrgId"]) == 9)   //Added 03112022 for challan report disabled 
                {
                    btnChallan.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("No Subjects Registered.", this.Page);   // it means record already exist in fees log recon=1
            }

        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.SubmitCourses() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed...", this.Page);
                return;
            }
        }
    }


    public void LoadTotalRegisteredAmount()
    {
        decimal RegTotalAmt = 0.00M;

        #region Semesternos
        //string semesternos = string.Empty;
        //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        //{
        //    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //    Label lblSemesterno = dataitem.FindControl("lblSEMSCHNO") as Label;
        //    if (cbRow.Checked == true)
        //    {
        //        semesternos += lblSemesterno.ToolTip + ",";
        //    }
        //}
        //semesternos = semesternos.TrimEnd(',');
        #endregion

        string sp_proc = "PKG_ACD_GET_EXAM_REG_AMOUNT";
        string sp_para = "@P_UA_NO,@P_SESSIONNO,@P_SEMESTERNO,@P_IDNO,@P_DEGREENO,@P_PAGE_LINK,@P_STATUS";
        string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(ViewState["sessionno"]) + "," + Convert.ToString(Session["semester_reg"]) + "," + Convert.ToInt32(ViewState["idno"]) + "," + hfDegreeNo.Value + "," + Request.QueryString["pageno"].ToString() + "," + 3 + "";   //Status 3 for Photocopy Coursewise Reg amount

        DataSet dsTotAmt = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

        if (dsTotAmt.Tables[0].Rows.Count > 0 && dsTotAmt.Tables != null && dsTotAmt.Tables[0] != null)
        {
            RegTotalAmt = Convert.ToDecimal(dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString()) == null ? 0 : Convert.ToDecimal(dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString());
        }

        lblTotalAmount.Text = RegTotalAmt.ToString();
        ViewState["Exam_Amout"] = string.Empty;
        ViewState["Exam_Amout"] = RegTotalAmt.ToString();   //Added on 02112022
    }


    decimal PhotoCopy_Amt = 0.00M;
    public void LoadPhotoCopyFeeAmount()
    {

        //string semesternos = string.Empty;
        //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        //{
        //    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //    Label lblSemesterno = dataitem.FindControl("lblSEMSCHNO") as Label;
        //    if (cbRow.Checked == true)
        //    {
        //        semesternos += lblSemesterno.ToolTip + ",";  
        //    }
        //} 
        //semesternos = semesternos.TrimEnd(',');

        string sp_proc = "PKG_ACD_GET_EXAM_REG_AMOUNT";
        string sp_para = "@P_UA_NO,@P_SESSIONNO,@P_SEMESTERNO,@P_IDNO,@P_DEGREENO,@P_PAGE_LINK,@P_STATUS";
        string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(ViewState["sessionno"]) + "," + Convert.ToString(Session["semester_reg"]) + "," + Convert.ToInt32(ViewState["idno"]) + "," + hfDegreeNo.Value + "," + Request.QueryString["pageno"].ToString() + "," + 1 + "";   //Status 1 for Photocopy TotAmount

        DataSet dsTotAmt = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

        if (dsTotAmt.Tables[0].Rows.Count > 0 && dsTotAmt.Tables != null && dsTotAmt.Tables[0] != null)
        {
            PhotoCopy_Amt = Convert.ToDecimal(dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString()) == null ? 0 : Convert.ToDecimal(dsTotAmt.Tables[0].Rows[0]["TOTAL_AMT"].ToString());
        }

    }

    static decimal CourseAmt = 0;
    static decimal CourseCount = 0;
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            divTotalCourseAmount.Visible = true;
            LoadPhotoCopyFeeAmount();
            CheckBox chk = sender as CheckBox;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                Label lblExtermark = dataitem.FindControl("lblExtermark") as Label;

                // Label lblSemesterno = dataitem.FindControl("lblSEMSCHNO") as Label;


                if (cbRow.Checked == true)
                {
                    CourseCount = CourseCount + 1;

                    //if (CourseCount <= 5)
                    CourseAmt = Convert.ToDecimal(CourseAmt) + Convert.ToDecimal(PhotoCopy_Amt);

                    // semesternos += lblSemesterno.ToolTip;
                    //if (CourseCount > 5)
                    //{
                    //    chk.Checked = false;
                    //}
                }

            }

            lblTotalAmount.Text = CourseAmt.ToString();

            //if (CourseCount > 5)
            //{
            //    objCommon.DisplayMessage(updDetails, "Maximum 5 Subjects Limit Reached.", this.Page);
            //}

            CourseAmt = 0;
            CourseCount = 0;
        }
        catch { }
    }

    #endregion

    #region Report Functionality

    //Show Revauation Registertion Slip
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                // ShowReportPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptPhotoCopy_ATLAS.rpt");       //Payment Slip 
                ShowReportPhotoCopyReval("Photo Copy Registration Slip", "rptPhotoRevaluationATLAS.rpt");
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                ShowReportPhotoCopyReval("Photo Copy Registration Slip", "rptPhotoRevaluation_Rcpiper.rpt");
            }
            else
            {
                //ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt");  
                ShowReportNew("Photo Copy Registration Slip", "rptPhotoRevaluationCRESCENT.rpt");

            }
        }
        catch { }
    }

    private void ShowReportNew(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ViewState["sessionno"]);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + sessionno + ",@P_REVAL_TYPE=1";
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


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int idno = Convert.ToInt32(lblName.ToolTip);
            string semester = objCommon.LookUp("ACD_REVAL_RESULT", "DISTINCT SEMESTERNO", "IDNO=" + idno + "AND APP_TYPE= 'PHOTO COPY' " + "AND SESSIONNO=" + sessionno);
            // string college_id = objCommon.LookUp("ACD_STUDENT","COLLEGE_ID","IDNO=" +idno);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"]) + ",@P_SEMESTERNO=" + semester + ",@P_TYPE=1" + ",@P_RECIEPT_CODE=PRF ";     //Added reciept code condition
            //   url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"]) + ",@P_SEMESTERNO=" + semester + ",@P_TYPE=1" + ",@P_RECIEPT_CODE=PRF ";     //Added reciept code condition
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

    #endregion


    #region "Online Payment Functions and transactions"

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

    private void GetSessionValues()
    {
        ViewState["FirstName"] = lblName.Text;
        //ViewState["RegNo"] = lblapp.Text;
        ViewState["MobileNo"] = lblPH.Text;
        //ViewState["EMAILID"] = lblEmail.Text;
        ViewState["OrderID"] = lblOrderID.Text;
        ViewState["TOTAL_AMT"] = lblTotalAmount.Text;
        //ViewState["TOTAL_AMT"] = "1";
    }

    //----  BILL DESK PAYMENT GATEWAY ----------------//
    protected void PostOnlinePayment()
    {
        //Added on 20082022   
        FeeCollectionController FeeCollection = new FeeCollectionController();
        //Added on 19082022
        int orgId = 2; int payId = 1;
        // int activityno = 10; 
        //int activityno = Convert.ToInt32(Session["payactivityno"]);      //Added on 11012022
        string merchId = string.Empty; string checkSumKey = string.Empty; string requestUrl = string.Empty; string responseUrl = string.Empty; Session["CHECKSUM_KEY"] = string.Empty;
        Session["Order_id"] = string.Empty;
        int activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%PhotoCopy%'"));

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
        feeAmount = (ViewState["Final_Amt"]).ToString();
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
        additional_info2 = ViewState["IDNO"].ToString();  // Project Code
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


    #region "Registered Subjects For Photo Copy"

    private void BindRegisteredCoursesofPHOTOCOPY()
    {
        lvFinalCourses.DataSource = null;
        lvFinalCourses.DataBind();

        DataSet dsRegCourses = null;
        //0 for showing registered courses
        //Show Reg. Courses for Photo Copy
        dsRegCourses = objSC.GetCourseFor_RevalOrPhotoCopy(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["sessionno"]), 0);
        if (dsRegCourses != null && dsRegCourses.Tables.Count > 0 && dsRegCourses.Tables[0].Rows.Count > 0)
        {
            lvFinalCourses.DataSource = dsRegCourses.Tables[0];
            lvFinalCourses.DataBind();
            pnlFinalCourses.Visible = true;
        }
        else
        {
            lvFinalCourses.DataSource = null;
            lvFinalCourses.DataBind();
            pnlFinalCourses.Visible = false;
            objCommon.DisplayMessage(updDetails, "No Registered Subjects found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }

    }
    #endregion


    #region "Print Challan"
    protected void btnChallan_Click(object sender, EventArgs e)
    {
        try
        {
            int CheckRecon = 0;
            int result = 0;
            string pay_mode = string.Empty;
            string Pay_mode_Details = string.Empty;
            CheckRecon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToString(ViewState["sessionno"]) + "AND RECIEPT_CODE ='PRF' AND ISNULL(CAN,0) = 0 AND ISNULL(RECON,0) = 1 "));

            pay_mode = (objCommon.LookUp("ACD_DCR", "ISNULL(PAY_MODE_CODE,'') PAY_MODE_CODE", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToString(ViewState["sessionno"]) + "AND RECIEPT_CODE ='PRF' AND ISNULL(CAN,0) = 0"));

            //to check payment already done or not
            if (CheckRecon == 1)
            {
                if (pay_mode == "C")
                {
                    Pay_mode_Details = "Cash Payment";
                }
                else if (pay_mode == "O")
                {
                    Pay_mode_Details = "Online Payment";
                }
                objCommon.DisplayMessage(updDetails, "Photo Reval Fees Already Done For this Session through " + Pay_mode_Details + "..!", this.Page);

                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                return;
            }
            string courseno = Convert.ToString(ViewState["Coursenos"]);
            string grade = Convert.ToString(ViewState["Grades"]);
            string ccode = Convert.ToString(ViewState["ccodes"]);
            string semesternos = Convert.ToString(ViewState["Semesternos"]);
            string Total_Exter_Marks = Convert.ToString(ViewState["ExtermarkTot"]);      ///ViewState["Extermarks"]); 
            string schemeno = Convert.ToString(ViewState["Schemeno"]);

            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int idno = Convert.ToInt32(ViewState["idno"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            string college_code = Session["colcode"].ToString();                         //Session["colcode"].ToString();
            int ua_no = Convert.ToInt32(Session["userno"]);
            int User_Type = Convert.ToInt32(Session["usertype"]);

            //to generate challan
            //if (pay_mode != "C")
            //{
            //to update challan details
            //result = objSReg.AddChallanDetails(objSR, "PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
            result = objSReg.AddRevaluationChallanDetails(idno, sessionno, schemeno, courseno, IPADDRESS, semesternos, college_code, ua_no, grade, ccode, "PHOTO COPY", Total_Exter_Marks, User_Type);

            // int status = objSC.UpdateChallanDetails(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["sessionno"]), 1); //1 for photo copy

            if (result == 1)
            {
                this.ShowReport("Payment_Details", "rptPhotoRevalChallanSummary.rpt", "PRF");
                //btnPayOnline.Visible = false;
                //btnChallan.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Something went Wrong!", this.Page);
            }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updDetails, "Challan Already Generated!", this.Page);
            //}
        }
        catch { }
    }

    //used for to Showing the report on hostel challan fees and challan fees.
    private void ShowReport(string reportTitle, string rptFileName, string Reciepttype)
    {
        try
        {
            //string dcrno = string.Empty;

            //dcrno = objCommon.LookUp("ACD_DEMAND", "distinct DM_NO ", "IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()) + " AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) +  "AND RECIEPT_CODE ='" + Reciepttype + "' AND ISNULL(CAN,0) = 0");


            //if (!string.IsNullOrEmpty(dcrno))
            //{
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()) + ",@P_SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + ",@P_RECEIPTTYPE=" + Reciepttype + ",@P_CHALLAN_TYPE=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(updDetails, updDetails.GetType(), "controlJSScript", sb.ToString(), true);

            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoCopyRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region not in used

    protected void btnRegistrationSlip_Click(object sender, EventArgs e)
    {
        try
        {
            //ViewState["OrderID"]
            if (ViewState["OrderID"] != null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["OrderID"] + "'");
                if (rec_code == "PRF")
                {
                    ShowReportt("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 1); //1 for photo copy
                }
                else if (rec_code == "RF")
                {
                    ShowReportt("Revaluation Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 2); //2 for reval
                }
                else if (rec_code == "AEF")
                {
                    //ShowAEFReport("ExamRegistrationSlip", "PaymentReceipt_Exam_Registered_Courses.rpt", rec_code);
                }
                else if (rec_code == "CF")
                {
                    ShowReportt("CondonationSlip", "StudCondonation.rpt", rec_code, 4);
                }
                else if (rec_code == "REF")
                {
                    ShowReportt("Review fee Detail", "rptPhotoRevaluation.rpt", rec_code, 3); //3 for review  on 06052022
                }
            }
        }
        catch { }
    }
    protected void btnReports_Click(object sender, EventArgs e)
    {
        try
        {
            //string orderid =   //"47333780";
            if (ViewState["ORDERID"] == null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");
                if (rec_code == "PRF")
                {
                    this.ShowReport_NEW("Photo Reval Payment_Details", "PhotoRevalPaymentReceipt.rpt", 1);//1 for photo copy
                }
                else if (rec_code == "RF")
                {
                    this.ShowReport_NEW("Revaluation Payment_Details", "PhotoRevalPaymentReceipt.rpt", 2);//2 for reval
                }
                else if (rec_code == "AEF")
                {
                    this.ShowReport_NEW("Arrear Payment_Details", "PhotoRevalPaymentReceipt.rpt", 3);//3 for Arrear
                }
                else if (rec_code == "CF")
                {
                    this.ShowReport_NEW("Condonation Payment_Details", "PhotoRevalPaymentReceipt.rpt", 4);//4 for Condonation
                }
                else if (rec_code == "REF")
                {
                    this.ShowReport_NEW("Review_Fee_Payment_Details", "PhotoRevalPaymentReceipt.rpt", 5);//5 for REVIEW//
                }

            }
        }
        catch { }
    }
    //GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. 
    private void ShowReport_NEW(string reportTitle, string rptFileName, int reval_type)
    {
        try
        {
            string orderid = "47333780";
            InitializeSession();
            //string col = Session["colcode"].ToString();
            //string userno = Session["userno"].ToString();

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ADMISSION")));
            //url += "Reports/CommonReport.aspx?";

            //string url = "0";
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_ORDER_ID=" + orderid + ",@P_REVAL_TYPE=" + reval_type + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            //url += "&param=@P_ORDER_ID=" + ViewState["Orderid"] + ",@P_REVAL_TYPE=1,@P_COLLEGE_CODE=50";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void InitializeSession()
    {
        try
        {
            //int IDNO = Convert.ToInt32(Request.QueryString["id"].ToString());
            int IDNO = 4733;               // Convert.ToInt32(ViewState["IDNO"]);
            //int IDNO = Convert.ToInt32(ViewState["IDNO"].ToString());
            //Session["colcode"] = 50;
            int userno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO = " + IDNO + ""));

            //Get Common Details
            SqlDataReader dr = objCommon.GetCommonDetails();
            if (dr != null)
            {
                if (dr.Read())
                {
                    Session["coll_name"] = dr["CollegeName"].ToString();
                }
            }

            User_AccController objUC = new User_AccController();
            UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

            DataSet ds = objCommon.FillDropDown("ACD_ACCESS_MASTER A INNER JOIN ACD_MACHINE_TYPE_MASTER B ON (B.MACTYPENO=A.MACTYPENO AND B.COLLEGE_CODE=A.COLLEGE_CODE)", "A.MACADD", "B.MACTYPE_STATUS", "A.UA_NO=" + objUA.UA_No + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //    if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) == 0)
                //    {
                //        Session["USER_MAC"] = Convert.ToString(ds.Tables[0].Rows[0][0]);
                //    }
                //    else
                //    {
                //        Session["USER_MAC"] = "0";
                //    }
            }

            //Session["userno"] = objUA.UA_No.ToString();
            //Session["idno"] = objUA.UA_IDNo.ToString();
            //Session["username"] = objUA.UA_Name;
            //Session["usertype"] = objUA.UA_Type;
            //Session["userfullname"] = objUA.UA_FullName;
            //Session["dec"] = objUA.UA_Dec.ToString();
            //Session["userdeptno"] = objUA.UA_DeptNo.ToString();
            //DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            //Session["colcode"] = dsReff.Tables[0].Rows[0]["COLLEGE_CODE"].ToString(); //Added by Irfan Shaikh on 20190424
            //Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0 AND FLOCK=1");
            //Session["firstlog"] = objUA.UA_FirstLogin;
            //Session["ua_status"] = objUA.UA_Status;
            //Session["ua_section"] = objUA.UA_section.ToString();
            //Session["UA_DESIG"] = objUA.UA_Desig.ToString();
            //string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            //Session["ipAddress"] = ipAddress;
            ////string macAddress = GetMACAddress();
            ////Session["macAddress"] = macAddress;

            ////int retLogID = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
            ////Session["logid"] = retLogID + 1;
            ////Session["loginid"] = retLogID.ToString();

            //string lastlogout = string.Empty;
            //string lastloginid = objCommon.LookUp("LOGFILE", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' AND UA_NAME IS NOT NULL");
            //Session["lastloginid"] = lastloginid.ToString();
            //if (Session["lastloginid"].ToString() != string.Empty)
            //{
            //    lastlogout = objCommon.LookUp("LOGFILE", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
            //}

            //Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "FLOCK=1");
            //Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");
            //Session["WorkingDate"] = DateTime.Now.ToString();
            //Session["college_nos"] = objUA.COLLEGE_CODE;
            //Session["Session"] = Session["sessionname"].ToString();


        }
        catch { }
    }

    private void ShowReportt(string reportTitle, string rptFileName, string rec_code, int reval_type)
    {
        try
        {
            InitializeSession();
            string orderid = ViewState["ORDERID"].ToString();       //"47333780";
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "ORDER_ID = " + orderid + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID = " + orderid + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));

            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            ////string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //// string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            // url += "Reports/CommonReport.aspx?";
            // url += "pagetitle=" + reportTitle;
            // url += "&path=~,Reports,Academic," + rptFileName;

            // //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(idno) + ",@P_SESSIONNO=" + SessionNo + ",@P_REVAL_TYPE='" + rec_code + "'";

            // divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            // divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            // divMsg.InnerHtml += " </script>";



            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(idno) + ",@P_SESSIONNO=" + SessionNo + ",@P_REVAL_TYPE='" + rec_code + "'";
            //url += "&param=@P_ORDER_ID=" + ViewState["Orderid"] + ",@P_REVAL_TYPE=1,@P_COLLEGE_CODE=50";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region btnPayOnline_Click1
    //Added Chashfree Payment Gateway
    protected void btnPayOnline_Click1(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 9)      //CashfreePayment Gateway  //ATLAS
        {
            CashFreePaymentGateway();
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 6)      //CashfreePayment Gateway   RCPIPER
        {
            PayUPaymentGateway();   //BillDesk Payment Gateway   
        }
        else
        {
            BillDeskPaymentGateway();   //BillDesk Payment Gateway   //Crescent
        }
    }
    #endregion

    private string CreateToken(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    #region BillDeskPaymentGateway
    protected void BillDeskPaymentGateway()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        #region "Online Payment"
        try
        {
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND RECIEPT_CODE = 'PRF' AND RECON = 1 AND ISNULL(CAN,0)=0"));

            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage(updDetails, "Photo Copy Reval Fee has been paid already. Can't proceed with the transaction !", this);
                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                btnPaymentReport.Visible = true;
                return;
            }

            int result = 0;
            int logStatus = 0;

            CreateCustomerRef();
            GetSessionValues();

            //ViewState["Final_Amt"] = lblTotalAmount.Text.ToString();
            //ViewState["Final_Amt"] = "1";
            ViewState["Final_Amt"] = Convert.ToString(ViewState["Exam_Amout"]);
            Session["Order_id"] = Convert.ToString(ViewState["ORDERID"]);

            if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
            {
                objCommon.DisplayMessage(updDetails, "You are not eligible for Fee Payment !", this);
                return;
            }

            objStudentFees.UserNo = Convert.ToInt32(ViewState["idno"]);
            objStudentFees.Amount = Convert.ToDouble(ViewState["Final_Amt"]);
            objStudentFees.SessionNo = Convert.ToString(ViewState["sessionno"]);                //(ddlSession.SelectedValue.ToString());
            objStudentFees.OrderID = lblOrderID.Text;
            //objStudentFees.TransDate = System.DateTime.Today;
            //objStudentFees.BranchName = lblbranch.Text;


            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            string COLLEGE_CODE = Session["colcode"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            //objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);

            string Courenos = ViewState["Coursenos"].ToString();        //Added on 25082022
            string Grades = ViewState["Grades"].ToString();
            string ccodes = ViewState["ccodes"].ToString();  //ViewState["Codes"]
            string Semesternos = ViewState["Semesternos"].ToString();
            string Extermarks = ViewState["Extermarks"].ToString();
            string Schemeno = ViewState["Schemeno"].ToString();


            //insert in acd_fees_log
            result = ObjFCC.AddPhotoRevalFeeLog(objStudentFees, 1, 1, "PRF", 1); //1 for photo copy

            //logStatus = ObjFCC.AddPhotoRevalFeeLogDcrTemp(objStudentFees, Schemeno, Courenos, IPADDRESS, Semesternos, COLLEGE_CODE, UA_NO, Grades, ccodes, "PHOTO COPY", Extermarks, Convert.ToInt32(Session["usertype"]));
            //"PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));  //Added on 25082022


            if (result > 0)
            {

                //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO ", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["idno"] + "'", "");
                DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(ViewState["idno"]) + "'", "");
                ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                ViewState["SESSIONNO"] = ViewState["sessionno"].ToString();   //ddlSession.SelectedValue;
                ViewState["SEM"] = lblSemester.ToolTip.ToString();
                ViewState["RECIEPT"] = "PRF";

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

                ViewState["Semester"] = objCommon.LookUp("ACD_DEMAND", "TOP 1 SEMESTERNO", "RECIEPT_CODE='PRF' AND SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + "AND IDNO=" + ViewState["IDNO"]); //Added on 24082022

                //ViewState["basicinfo"] = ViewState["REGNO"] + "," + ViewState["ENROLLNO"] + "," + ViewState["SHORTNAME"];
                //  PostOnlinePayment();


                //Added on 20082022
                int status1 = 0;
                int Currency = 1;
                string amount = string.Empty;
                amount = Convert.ToString(ViewState["Final_Amt"]);

                Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                //    DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
                // string PaymentMode = "ONLINE EXAM FEES";
                string PaymentMode = "PhotoCopy and Revalution";
                Session["PaymentMode"] = PaymentMode;
                Session["studAmt"] = amount;
                ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
                // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
                Session["studName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                Session["studPhone"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                Session["studEmail"] = ViewState["EMAILID"].ToString(); // lblMailId.Text;

                Session["ReceiptType"] = "PRF";
                Session["idno"] = Convert.ToInt32(ViewState["IDNO"].ToString()); //hdfIdno.Value;
                Session["paysession"] = Convert.ToString(ViewState["sessionno"]);                //Convert.ToInt32(ddlSession.SelectedValue);            //ViewState["sessionnonew"].ToString(); // hdfSessioNo.Value;
                //Session["paysemester"] = ViewState["SEM"].ToString(); // ddlSemester.SelectedValue;
                Session["homelink"] = "PhotoCopyRegistration.aspx";
                Session["regno"] = ViewState["REGNO"].ToString(); // lblRegno.Text;
                Session["payStudName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                Session["paymobileno"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
                Session["Branchname"] = ViewState["SHORTNAME"].ToString(); //lblBranchName.Text;

                Session["studrefno"] = lblOrderID.Text;                 //Added on 23082022
                Session["paysemester"] = ViewState["Semester"].ToString();  //Added on 23082022

                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%PhotoCopy%'"));
                Session["payactivityno"] = activityno;
                //Session["CHECKSUM_KEY"] = string.Empty;     //Added on 21082022
                DataSet ds1 = ObjFCC.GetOnlinePaymentConfigurationDetails(OrganizationId, 1, activityno);    // Convert.ToInt32(Session["payactivityno"]
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 1)
                    {

                    }
                    else
                    {
                        //Session["CHECKSUM_KEY"] = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();     //Added on 21082022
                        Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                        string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                        Response.Redirect(RequestUrl, false);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed !.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }
    #endregion

    #region CashFreePaymentGateway
    protected void CashFreePaymentGateway()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        #region "Online Payment"
        try
        {
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND RECIEPT_CODE = 'PRF' AND RECON = 1 AND ISNULL(CAN,0)=0"));

            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage(updDetails, "Photo Copy Reval Fee has been paid already. Can't proceed with the transaction !", this);
                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                return;
            }

            int result = 0;
            int logStatus = 0;

            CreateCustomerRef();
            GetSessionValues();

            ViewState["Final_Amt"] = lblTotalAmount.Text.ToString();
            //ViewState["Final_Amt"] = "1";


            if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
            {
                objCommon.DisplayMessage(updDetails, "You are not eligible for Fee Payment !", this);
                return;
            }

            objStudentFees.UserNo = Convert.ToInt32(ViewState["idno"]);
            objStudentFees.Amount = Convert.ToDouble(ViewState["Final_Amt"]);
            objStudentFees.SessionNo = Convert.ToString(ViewState["sessionno"]);                     //(ddlSession.SelectedValue.ToString());
            objStudentFees.OrderID = lblOrderID.Text;
            //objStudentFees.TransDate = System.DateTime.Today;
            //objStudentFees.BranchName = lblbranch.Text;


            int sessionno = Convert.ToInt32(ViewState["sessionno"]);            //Convert.ToInt32(ddlSession.SelectedValue);
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            string COLLEGE_CODE = Session["colcode"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            //objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);

            string Courenos = ViewState["Coursenos"].ToString();        //Added on 25082022
            string Grades = ViewState["Grades"].ToString();
            string ccodes = ViewState["ccodes"].ToString();  //ViewState["Codes"]
            string Semesternos = ViewState["Semesternos"].ToString();
            string Extermarks = ViewState["Extermarks"].ToString();
            string Schemeno = ViewState["Schemeno"].ToString();
            Session["Session"] = Convert.ToString(ViewState["SESSIONNO"]);

            //insert in acd_fees_log
            result = ObjFCC.AddPhotoRevalFeeLog(objStudentFees, 1, 1, "PRF", 1); //1 for photo copy            

            if (result > 0)
            {

                //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO ", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["idno"] + "'", "");
                DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(ViewState["idno"]) + "'", "");
                ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                ViewState["SESSIONNO"] = Convert.ToString(ViewState["sessionno"]);                       //ddlSession.SelectedValue;
                ViewState["SEM"] = lblSemester.ToolTip.ToString();
                ViewState["RECIEPT"] = "PRF";

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

                ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                ViewState["basicinfo"] = ViewState["ENROLLNO"];

                ViewState["Semester"] = objCommon.LookUp("ACD_DEMAND", "TOP 1 SEMESTERNO", "RECIEPT_CODE='PRF' AND SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + "AND IDNO=" + ViewState["IDNO"]); //Added on 24082022
                ViewState["Transaction"] = ViewState["ORDERID"].ToString();                 //ViewState["OrderID"].ToString();
                Session["ReceiptType"] = "PRF";
                Session["paysession"] = Convert.ToString(ViewState["sessionno"]);                //ddlSession.SelectedValue.ToString();

                ////////////////////////////////////////////////
                #region For payment Online
                int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                // Add For UAT 
                // Session["payactivityno"] = "2";

                Session["payactivityno"] = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%PhotoCopy%'"));
                int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
                int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])));

                DataSet ds1 = ObjFCC.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 1, Convert.ToInt32(Session["payactivityno"]), DEGREENO, cid);

                //DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 1, Convert.ToInt32(Session["payactivityno"]));
                // return;
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 1)
                    {

                    }
                    else
                    {
                        string url = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                        string secret = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                        string Merchantkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                        string Responseurl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();


                        //string url = ConfigurationManager.AppSettings["CashFree_URL"];
                        //string secret = ConfigurationManager.AppSettings["CashFree_secret"];
                        //string Merchantkey = ConfigurationManager.AppSettings["CashFree_Merchantkey"];

                        // ViewState["Orderid"] = ViewState["OrderID"].ToString();

                        StudentController objSC1 = new StudentController();
                        DataSet dsStudent1 = objSC1.GetStudentDetailsExam(Convert.ToInt32(ViewState["idno"]));
                        string orderID = Convert.ToString(ViewState["ORDERID"]);               //ViewState["OrderID"].ToString();
                        //string Amount = FinalTotal.Text;
                        string Amount = Convert.ToString(ViewState["Exam_Amout"]);          //"1.00";
                        string Name = dsStudent1.Tables[0].Rows[0]["STUDNAME"].ToString(); ;
                        string Phone_no = dsStudent1.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        string Email = dsStudent1.Tables[0].Rows[0]["EMAILID"].ToString();

                        //string url = ConfigurationManager.AppSettings["CashFree_URL"];
                        //string secret = ConfigurationManager.AppSettings["CashFree_secret"];
                        // string Merchantkey = ConfigurationManager.AppSettings["CashFree_Merchantkey"];


                        string data = "";
                        SortedDictionary<string, string> formParams = new SortedDictionary<string, string>();
                        formParams.Add("appId", Merchantkey);
                        formParams.Add("orderId", orderID);
                        formParams.Add("orderAmount", Amount);
                        formParams.Add("customerName", Name);
                        formParams.Add("customerPhone", Phone_no);
                        formParams.Add("customerEmail", Email);

                        formParams.Add("returnUrl", Responseurl);
                        // formParams.Add("returnUrl", "http://localhost:60066/PresentationLayer/Atlas_Payment_Response.aspx");
                        // formParams.Add("returnUrl", "http://atlasuniversityuat.mastersofterp.in/PresentationLayer/Atlas_Payment_Response.aspx");
                        foreach (var kvp in formParams)
                        {
                            data = data + kvp.Key + kvp.Value;
                        }

                        string signature = CreateToken(data, secret);


                        //Save record to DCR_Temp Added on 02112022
                        result = ObjFCC.InsertPayment_Log_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Session["semesternos"].ToString(), ViewState["ORDERID"].ToString(), 1, Convert.ToString(Session["ReceiptType"]), data);
                        //////////////////////

                        //Console.Write(signature);
                        string outputHTML = "<html>";
                        outputHTML += "<head>";
                        outputHTML += "<title>Merchant Check Out Page</title>";
                        outputHTML += "</head>";
                        outputHTML += "<body>";
                        outputHTML += "<center>Please do not refresh this page...</center>";  // you can put h1 tag here
                        //outputHTML += "<form id='redirectForm' method='post' action='https://www.gocashfree.com/checkout/post/submit'>";
                        // outputHTML += "<form id='redirectForm' method='post' action='https://test.cashfree.com/billpay/checkout/post/submit'>";
                        outputHTML += "<form id='redirectForm' method='post' action='" + url + "'>";
                        //outputHTML += "<form id='redirectForm' method='post' action='"+ url+"'>";

                        outputHTML += "<input type='hidden' name='appId' value='" + Merchantkey + "'/>";
                        outputHTML += "<input type='hidden' name='orderId' value='" + orderID + "'/>";
                        outputHTML += "<input type='hidden' name='orderAmount' value='" + Amount + "'/>";
                        outputHTML += "<input type='hidden' name='customerName' value='" + Name + "'/>";
                        outputHTML += "<input type='hidden' name='customerEmail' value='" + Email + "'/>";
                        outputHTML += "<input type='hidden' name='customerPhone' value='" + Phone_no + "'/>";
                        outputHTML += "<input type='hidden' name='returnUrl' value='" + Responseurl + "'/>";
                        // outputHTML += "<input type='hidden' name='returnUrl' value='http://localhost:60066/PresentationLayer/Atlas_Payment_Response.aspx'/>";//59566
                        //outputHTML += "<input type='hidden' name='returnUrl' value='http://atlasuniversityuat.mastersofterp.in/PresentationLayer/Atlas_Payment_Response.aspx'/>";//59566

                        outputHTML += "<input type='hidden' name='signature' value='" + signature + "'/>";
                        outputHTML += "<table border='1'>";
                        outputHTML += "<tbody>";
                        foreach (string keys in formParams.Keys)
                        {
                            outputHTML += "<input type='hidden' name='" + keys + "' value='" + formParams[keys] + "'>";
                        }
                        outputHTML += "</tbody>";
                        outputHTML += "</table>";
                        outputHTML += "<script type='text/javascript'>";
                        outputHTML += "document.getElementById('redirectForm').submit();";
                        outputHTML += "</script>";
                        outputHTML += "</form>";
                        outputHTML += "</body>";
                        outputHTML += "</html>";
                        Response.Write(outputHTML);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Something Went Wrong !!", this.Page);

                }

                #endregion
                ////////////////////////////////////////////////

            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed !.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }
    #endregion

    #region PayUPaymentGateway
    protected void PayUPaymentGateway()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        #region "Online Payment"
        try
        {
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND RECIEPT_CODE = 'PRF' AND RECON = 1 AND ISNULL(CAN,0)=0"));

            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage(updDetails, "Photo Copy Reval Fee has been paid already. Can't proceed with the transaction !", this);
                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                btnPaymentReport.Visible = true;
                return;
            }

            int result = 0;
            int logStatus = 0;

            CreateCustomerRef();
            GetSessionValues();

            //ViewState["Final_Amt"] = "1";
            ViewState["Final_Amt"] = Convert.ToString(ViewState["Exam_Amout"]);
            Session["Order_id"] = Convert.ToString(ViewState["ORDERID"]);

            if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
            {
                objCommon.DisplayMessage(updDetails, "You are not eligible for Fee Payment !", this);
                return;
            }

            objStudentFees.UserNo = Convert.ToInt32(ViewState["idno"]);
            objStudentFees.Amount = Convert.ToDouble(ViewState["Final_Amt"]);
            objStudentFees.SessionNo = Convert.ToString(ViewState["sessionno"]);
            objStudentFees.OrderID = lblOrderID.Text;
            //objStudentFees.TransDate = System.DateTime.Today; 

            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            string IPADDRESS = Session["ipAddress"].ToString();
            string COLLEGE_CODE = Session["colcode"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);

            string Courenos = ViewState["Coursenos"].ToString();
            string Grades = ViewState["Grades"].ToString();
            string ccodes = ViewState["ccodes"].ToString();
            string Semesternos = ViewState["Semesternos"].ToString();
            string Extermarks = ViewState["Extermarks"].ToString();
            string Schemeno = ViewState["Schemeno"].ToString();

            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                //insert in acd_fees_log
                result = ObjFCC.AddPhotoRevalFeeLog(objStudentFees, 1, 1, "PRF", 1); //1 for photo copy 
                //logStatus = ObjFCC.AddPhotoRevalFeeLogDcrTemp(objStudentFees, Schemeno, Courenos, IPADDRESS, Semesternos, COLLEGE_CODE, UA_NO, Grades, ccodes, "PHOTO COPY", Extermarks, Convert.ToInt32(Session["usertype"]));   //"PHOTO COPY", EXTERMARKS, Convert.ToInt32(Session["usertype"]));   
            }
            else
            {
                result = 1;   //Fees log maintined in other client except crescent 
            }

            if (result > 0)
            {
                //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO ", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["idno"] + "'", "");
                DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME,ISNULL(YEAR,0)Yearno,ISNULL(DEGREENO,0)DEGREENO,ISNULL(COLLEGE_ID,0)COLLEGE_ID", "IDNO = '" + Convert.ToInt32(ViewState["idno"]) + "'", "");
                ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                ViewState["SESSIONNO"] = ViewState["sessionno"].ToString();   //ddlSession.SelectedValue;
                ViewState["SEM"] = lblSemester.ToolTip.ToString();
                ViewState["RECIEPT"] = "PRF";

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
                ViewState["Yearno"] = string.Empty;
                if (d.Tables[0].Rows[0]["Yearno"].ToString() == "" || d.Tables[0].Rows[0]["Yearno"].ToString() == string.Empty)
                {
                    ViewState["Yearno"] = "NA";
                }
                else
                {
                    ViewState["Yearno"] = d.Tables[0].Rows[0]["Yearno"].ToString();
                }
                int degreeno = 0;
                if (d.Tables[0].Rows[0]["DEGREENO"].ToString() != "" || d.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty)
                {
                    degreeno = Convert.ToInt32(d.Tables[0].Rows[0]["DEGREENO"]);
                }
                int college_id = 0;
                if (d.Tables[0].Rows[0]["COLLEGE_ID"].ToString() != "" || d.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == string.Empty)
                {
                    college_id = Convert.ToInt32(d.Tables[0].Rows[0]["COLLEGE_ID"]);
                }
                string info = string.Empty;
                //ViewState["info"] = "PRF" + ViewState["REGNO"] + "," + ViewState["SESSIONNO"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];

                ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                ViewState["basicinfo"] = ViewState["ENROLLNO"];

                ViewState["Semester"] = objCommon.LookUp("ACD_DEMAND", "TOP 1 SEMESTERNO", "RECIEPT_CODE='PRF' AND SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + "AND IDNO=" + ViewState["IDNO"]); //Added on 24082022

                int status1 = 0;
                int Currency = 1;
                string amount = string.Empty;
                amount = Convert.ToString(ViewState["Final_Amt"]);

                Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                string PaymentMode = "PhotoCopy and Revalution";
                Session["PaymentMode"] = PaymentMode;
                Session["studAmt"] = amount;
                ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
                // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
                Session["studName"] = ViewState["STUDNAME"].ToString() == string.Empty ? "NA" : ViewState["STUDNAME"].ToString();
                Session["studPhone"] = ViewState["MOBILENO"].ToString() == string.Empty ? "NA" : ViewState["MOBILENO"].ToString();
                Session["studEmail"] = ViewState["EMAILID"].ToString() == string.Empty ? "NA" : ViewState["EMAILID"].ToString();

                Session["ReceiptType"] = "PRF";
                Session["idno"] = Convert.ToInt32(ViewState["IDNO"].ToString()) == null ? 0 : Convert.ToInt32(ViewState["IDNO"].ToString()) ; //hdfIdno.Value;
                Session["paysession"] = Convert.ToString(ViewState["sessionno"]) == string.Empty ? "0" : Convert.ToString(ViewState["sessionno"]);                //Convert.ToInt32(ddlSession.SelectedValue);            //ViewState["sessionnonew"].ToString(); // hdfSessioNo.Value;
                //Session["paysemester"] = ViewState["SEM"].ToString(); // ddlSemester.SelectedValue;
                Session["homelink"] = "PhotoCopyRegistration.aspx";
                Session["regno"] = ViewState["REGNO"].ToString() == string.Empty ? "0" : ViewState["REGNO"].ToString();
                Session["payStudName"] = ViewState["STUDNAME"].ToString() == string.Empty ? "NA" : ViewState["STUDNAME"].ToString();
                Session["paymobileno"] = ViewState["MOBILENO"].ToString()==string.Empty ? "0" : ViewState["MOBILENO"].ToString();
                Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
                Session["Branchname"] = ViewState["SHORTNAME"].ToString() == string.Empty ? "NA" : ViewState["SHORTNAME"].ToString();

                Session["studrefno"] = lblOrderID.Text;
                Session["paysemester"] = ViewState["Semester"].ToString();
                Session["YEARNO"] = Convert.ToString(ViewState["Yearno"]) == string.Empty ? "0" : Convert.ToString(ViewState["Yearno"]);

                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME like '%PhotoCopy%'"));
                Session["payactivityno"] = activityno;
               // Session["semesternos"] = ViewState["Semesterno"].ToString();

                int PAYID = 0;
                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%BillDesk%'"));
                    Session["PAYID"] = PAYID;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 7 || Convert.ToInt32(Session["OrgId"]) == 6)//rajagiri and rcpiper
                {
                    PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PAYU%'"));
                    Session["PAYID"] = PAYID;
                }
                else
                {
                    PAYID = 1;               //Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PAYU%'"));
                    Session["PAYID"] = PAYID;
                }
                DataSet ds1 = null;
                if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ds1 = ObjFCC.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt32(Session["PAYID"]), Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
                }
                else
                {
                    ds1 = ObjFCC.GetOnlinePaymentConfigurationDetails(OrganizationId, Convert.ToInt32(Session["PAYID"]), activityno);
                }
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                    }
                    else
                    {
                        //Session["CHECKSUM_KEY"] = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();     //Added on 21082022
                        Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                        string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                        Response.Redirect(RequestUrl, false);

                        //string requesturl = System.Configuration.ConfigurationManager.AppSettings["pgPageUrl"].ToString();                //ConfigurationManager.AppSettings["pgPageUrl"].ToString();
                        //Response.Redirect(requesturl, false);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Payment Gateway not define", this.Page);
                    return;
                } 
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed !.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }
    #endregion

    #region ShowReportPhotoCopy
    private void ShowReportPhotoCopy(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + ViewState["idno"].ToString()));
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + IDNO + "AND SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + "AND RECIEPT_CODE= 'PRF' "));
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + IDNO));
            string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_REVALTYPE=" + 1; ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region ShowReportPhotoCopyReval
    private void ShowReportPhotoCopyReval(string reportTitle, string rptFileName)
    {
        try
        {
            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int idno = Convert.ToInt32(lblName.ToolTip);
            string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno);
            //int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT SEMESTERNO", "IDNO=" + idno));
            int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "TOP 1 SEMESTERNO", "RECIEPT_CODE='PRF' AND SESSIONNO=" + sessionno + "AND IDNO=" + idno)); //Added on 24082022
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            int PayStatus = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + idno + "AND SESSIONNO=" + sessionno + "AND ISNULL(RECON,0)=" + 1 + "AND RECIEPT_CODE= 'PRF' "));
            if (PayStatus > 0)
            {
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + college_id + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"]) + ",@P_SEMESTERNO=" + semesterno + ",@P_TYPE=1" + ",@P_RECIEPT_CODE=PRF";     //Added reciept code condition
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefiniption.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region ShowPhotoCopyDetails
    private void ShowPhotoCopyDetails()
    {
        try
        {
            if (!string.IsNullOrEmpty(Session["usertype"].ToString()))
            {
                int idno = 0;
                FeeCollectionController feeController = new FeeCollectionController();
                if (Session["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"]);

                    divRollNo.Visible = false;
                }
                else
                {
                    idno = feeController.GetStudentIdByEnrollmentNo(txtRollNo.Text.Trim());
                    divRollNo.Visible = true;
                }
                //ViewState["idno"] = Convert.ToInt32(Session["idno"]);
                ViewState["idno"] = idno;
                int Sessionno = Convert.ToInt32(ViewState["sessionno"]);

                if (!string.IsNullOrEmpty(ViewState["idno"].ToString()))
                {
                    string TRRESULTLOCK = "";

                    string sp_proc = "PKG_ACD_CHECK_RESULT_DATA";
                    string sp_para = "@P_UA_NO,@P_SESSIONNO,@P_IDNO,@P_STATUS";
                    string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(ViewState["sessionno"]) + "," + Convert.ToInt32(ViewState["idno"]) + "," + 1 + "";   //Status 1 for Photocopy TotAmount

                    DataSet dsEligible = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                    if (dsEligible.Tables[0].Rows.Count > 0 && dsEligible.Tables != null && dsEligible.Tables[0] != null)
                    {
                        TRRESULTLOCK = dsEligible.Tables[0].Rows[0]["LOCK"].ToString();
                    }

                   // TRRESULTLOCK =  (objCommon.LookUp("ACD_TRRESULT", "DISTINCT ISNULL(LOCK,0) LOCK", "SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + " AND IDNO= " + Convert.ToInt32(ViewState["idno"]) + ""));
                    if (TRRESULTLOCK != "")//to check result published or not
                    {
                        if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
                        {
                            objCommon.DisplayMessage(updDetails, "Student with Univ. Reg. No. Or Admission No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                            return;
                        }

                        if (Session["usertype"].ToString() == "2")
                        {
                            this.ShowDetails();

                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = false;


                            lblTotalAmount.Text = "0";
                            CourseAmt = 0;
                            divTotalCourseAmount.Visible = false;

                            // ViewState["action"] = "edit";
                            //FillSemester();
                            ddlSemester.Enabled = false;
                            divSem.Visible = false;
                            //divNote.Visible = true;

                            divRegCourses.Visible = false;
                            //divRegisteredCoursesTotalAmt.Visible = false;

                            if (Sessionno > 0)
                            {
                                BindCourseListForPHOTOCOPY();
                                //IsPHOTOCOPYApproved();
                            }
                            else
                            {
                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                            }


                        }
                        else
                        {
                            //to check already record or not of that particular student
                            string RevalCount = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DISTINCT 1)", "SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 and CAST(APP_TYPE AS NVARCHAR)='PHOTO COPY' ");

                            if (RevalCount == "1")
                            {
                                string RECON = objCommon.LookUp("ACD_DCR", "Distinct isnull(RECON,0) RECON", "SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='PRF' ");

                                if (RECON == "1" || RECON == "True")
                                {
                                    this.ShowDetails();
                                    BindCourseListForPHOTOCOPY();
                                    ddlSemester.Enabled = false;
                                    divSem.Visible = false;
                                    //divNote.Visible = true;

                                    txtRollNo.Enabled = false;
                                    ddlSession.Enabled = false;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updDetails, "Photocopy Registration is Pending of this Student!", this.Page);
                                    //ddlSession.SelectedIndex = 0;
                                    txtRollNo.Text = "";
                                    // ddlSession.Focus();

                                    btnSubmit.Visible = false;
                                    btnPrintRegSlip.Visible = false;
                                    lvCurrentSubjects.DataSource = null;
                                    lvCurrentSubjects.DataBind();
                                    lvCurrentSubjects.Visible = false;
                                    tblInfo.Visible = false;
                                    divRegCourses.Visible = false;
                                    // divNote.Visible = false;
                                    lblTotalAmount.Text = "0";
                                    CourseAmt = 0;
                                    divTotalCourseAmount.Visible = false;
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(updDetails, "Photocopy Registration is Pending of this Student!", this.Page);
                                txtRollNo.Text = "";
                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                                tblInfo.Visible = false;
                                divRegCourses.Visible = false;
                                //divNote.Visible = false;
                                lblTotalAmount.Text = "0";
                                CourseAmt = 0;
                                divTotalCourseAmount.Visible = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetails, "Result Not Published Yet!", this.Page);
                        return;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhotoCopyRegistration.ShowPhotoCopyDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    #endregion

    #region CheckActivity for Admin
    private bool CheckActivityAdmin()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");

            ViewState["sessionno"] = sessionno;
            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }

    #endregion

    #region ShowReportPaymentPhotoCopy
    private void ShowReportPaymentPhotoCopy(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;

            int sessionno = Convert.ToInt32(ViewState["sessionno"]);
            int idno = Convert.ToInt32(lblName.ToolTip);

            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DISTINCT DCR_NO", "RECIEPT_CODE IN ('PRF') AND ISNULL(RECON,0)=1 AND IDNO=" + idno + "AND SESSIONNO=" + sessionno + ""));
            int collegecode = Convert.ToInt32(objCommon.LookUp("REFF", "COLLEGE_CODE", ""));
            int PayStatus = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + idno + "AND SESSIONNO=" + sessionno + "AND ISNULL(RECON,0)=" + 1 + "AND RECIEPT_CODE= 'PRF' "));

            if (PayStatus > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + idno + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_REVALTYPE=" + 1;

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";

                ////To open new window from Updatepanel
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                //sb.Append(@"window.open('" + url + "','','" + features + "');");

                //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PhotoCopyRegstrationCrescent.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #region btnPaymentReport_Click
    protected void btnPaymentReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                ShowReportPaymentPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptPhotoCopyCRESCENT.rpt");
            }
            else
            {
                ShowReportPayment("OnlineFeePayment", "rptOnlineReceipt.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "btnPaymentReport_Click.ShowReportPaymentPhotoCopy() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ShowReportPayment(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(lblName.ToolTip);
            string DcrNo = objCommon.LookUp("ACD_DCR", "DISTINCT DCR_NO", "RECIEPT_CODE IN ('PRF') AND ISNULL(RECON,0)=1 AND IDNO=" + lblName.ToolTip + "AND SESSIONNO=" + Convert.ToString(ViewState["sessionno"]) + "");
            int collegecode = Convert.ToInt32(objCommon.LookUp("REFF", "COLLEGE_CODE", ""));
            int PayStatus = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + lblName.ToolTip + "AND SESSIONNO=" + ViewState["sessionno"] + "AND ISNULL(RECON,0)=" + 1 + "AND RECIEPT_CODE= 'PRF' "));

            if (PayStatus > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "btnPaymentReport_Click.ShowReportPaymentPhotoCopy() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void ReportStatus()
    {
        if ((Convert.ToInt32(Session["OrgId"]) == 6) && (Session["usertype"].ToString() == "1"))
        {
            btnSubmit.Visible = false;
            btnChallan.Visible = false;
        }
    }
    private bool CheckRegistrationActivity()
    {
        try
        {
            bool ret = true;
            string sp_proc = "PKG_ACD_CHECK_REGISTRATION_ACTIVITY";
            string sp_para = "@P_UA_NO,@P_PAGE_LINK,@P_UA_TYPE";
            string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Request.QueryString["pageno"].ToString() + "," + Session["usertype"] + "";

            DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables[0] != null)
            {
                ViewState["sessionno"] = ds.Tables[0].Rows[0]["SESSION_NO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                ViewState["SESSIONNO"] = ds.Tables[0].Rows[0]["SESSION_NO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["SESSION_NO"].ToString();

                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }
    }

}