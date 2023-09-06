//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVALUATION REGISTRATION BY STUDENT AND ADMIN                                     
// CREATION DATE : 02/03/2020
// ADDED BY      : SARANG MUTKURE
// ADDED DATE    : 
// MODIFIED BY   : 
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



public partial class ACADEMIC_RecheckingandReassesment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();

    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();


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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //To Get IP Address of user 
                ViewState["ipAddress"] = GetUserIPAddress();

                //Check for Activity On/Off for Reval registration.                
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {

                    CheckActivity();
                    ViewState["action"] = "add";
                    divNote.Visible = true;
                    pnlSearch.Visible = false;
                    ShowDetails();
                    FillExamFees();
                    totamtpay.Text = "";
                }

                else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                {
                    CheckActivity();
                    pnlstart.Visible = true;
                    divNote.Visible = false;
                    PopulateDropDownList();
                }
            }

        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion
    #region Methods

    //private void checkbacklogcount()
    //{
    //    string retcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(PREV_STATUS)", "IDNO=" + Session["idno"] + "AND PREV_STATUS=1");

    //    if (Convert.ToInt32(retcount) > 3)
    //    {
    //        objCommon.DisplayMessage("You are not allowed for Reassesment Because you have More Than 3 Backlogs", this.Page);
    //        pnlstart.Visible = false;
    //    }
    //    else { return; }

    //}

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            //or
            //Client_IPAddress = Request.UserHostAddress;
            //or
            //User_IPAddress = Request.ServerVariables["REMOTE_HOST"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];
            //User_IPAddress = IP_Array[0];
        }
        return User_IPAddress;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Default.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Default.aspx");
        }
    }
    private void CheckActivity()
    {
        string sessionno = string.Empty;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlstart.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlstart.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlstart.Visible = false;
            return;
        }
        dtr.Close();
    }
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        // lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToInt32(ViewState["SESSIONNO"]) + Convert.ToInt32(ViewState["semesterno"]) + ir);
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(ViewState["IDNO"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ddlRevalRegSemester.SelectedValue) + ir);
    }
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", 1, "RF");
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
    //private void SubmitPaymentDetails()
    //{
    //    int semester = 0;
    //    string COM_CODE1 = string.Empty;
    //    CreateCustomerRef();
    //    GetNewReceiptNo();

    //    semester = Convert.ToInt32(lblSemester.ToolTip);

    //    if (radiolist.SelectedValue != "")
    //    {
    //        if (radiolist.SelectedValue == "1")
    //        {
    //            int result = 0;


    //            // result = feeController.InsertOnlinePayment_REVALUATION_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 1, Convert.ToString(Session["TOTAL_AMT"]), "0", Convert.ToString(Session["TOTAL_AMT"]));              
    //            result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 1, totamtpay.Text, "0", "0", totamtpay.Text, "R");
    //            Session["RECIEPT_CODE"] = "RF";
    //            if (result > 0)
    //            {
    //                SendTransaction();
    //            }
    //            else
    //            {
    //                objCommon.DisplayUserMessage(updDetails, "Failed to Continue.", this.Page);
    //                return;
    //            }
    //        }
    //        else if (radiolist.SelectedValue == "2")
    //        {
    //            int result = 0;
    //            result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 2, totamtpay.Text, "0", "0", totamtpay.Text, "R");
    //            //result = feeController.InsertOnlinePayment_REVALUATION_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 2, Convert.ToString(Session["TOTAL_AMT"]), "0", Convert.ToString(Session["TOTAL_AMT"]));                        
    //            if (result > 0)
    //            {

    //            }
    //            else
    //            {
    //                objCommon.DisplayUserMessage(updDetails, "Failed To Continue.", this.Page);
    //                return;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayUserMessage(updDetails, "Please Select Payment Option!", this.Page);

    //    }
    //}
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
    private void SendTransaction()
    {
        // hdnCollege.Value = dr["COLLEGE_CODE"].ToString();

        string merchant_id = "";
        string submerchant_id = ""; string submerchant_id1 = "";
        DataSet ds = null;
        //ds = feeController.Get_COLLEGE_PAYMENTDATA(Convert.ToInt32(hdnCollege.Value), "OTHER");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    merchant_id = ds.Tables[0].Rows[0]["ICID"].ToString();
        //}
        //else
        //{
        //    // objCommon.DisplayUserMessage(updBulkReg, "Error occurred while fetching college details!", this.Page);
        //    return;
        //}



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
            //  objCommon.DisplayUserMessage(updBulkReg, "Can not proceed due to insufficient data!", this.Page);
            return;
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
    }
    public void SubmitCourses()
    {
        try
        {
            CreateCustomerRef();
            GetSessionValues();
            GetNewReceiptNo();
            int result = 0;
            Boolean selection = false;
            int idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            int opertion = 0;
            string RECHECKORREASS = string.Empty;
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                //Get Student Details from lvStudent
                CheckBox chkrechecking = dataitem.FindControl("chkrechecking") as CheckBox;
                CheckBox chkreassesment = dataitem.FindControl("chkreassesment") as CheckBox;
                if (chkrechecking.Checked)
                {
                    selection = true;
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    string ext = (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString() == "" ? "0" : (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString();
                    objSR.EXTERMARKS += ext + "$";
                    objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                    RECHECKORREASS += 1 + "$";
                    objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("hdnschemeno") as HiddenField).Value);
                }
                //else if (chkreassesment.Checked)
                //{
                //    selection = true;
                //    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                //    string ext = (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString() == "" ? "0" : (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString();
                //    objSR.EXTERMARKS += ext + "$";

                //    objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                //    RECHECKORREASS += 2 + "$";
                //    objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("hdnschemeno") as HiddenField).Value);
                //}
            }
            if (!selection)
            {
                objSR.COURSENOS = "0";
                objSR.EXTERMARKS = "0";
                objSR.CCODES = "0";
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            objSR.EXTERMARKS = objSR.EXTERMARKS.TrimEnd('$');
            objSR.CCODES = objSR.CCODES.TrimEnd('$');
            RECHECKORREASS = RECHECKORREASS.TrimEnd('$');
            //objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]);
            objSR.SESSIONNO = Convert.ToInt32(Session["SESSIONNO"].ToString());
            objSR.IDNO = Convert.ToInt32(idno);
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["usertype"]);
            objSR.SEMESTERNO = Convert.ToInt32(ddlRevalRegSemester.SelectedValue);

            if (ViewState["action"] == "add")
            {
                opertion = 0;
            }
            else
            {
                opertion = 1;
            }
            result = objSReg.AddUpdateRevalPhotoCopyChallenegeRegisteration(objSR, 0, opertion, RECHECKORREASS);
            if (result > 0)
            {

                //radiolist.Visible = true;
                int count = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND  IDNO=" + ViewState["IDNO"]));
                lblstatusnew.Visible = true;
                //lblstatusnew.Text = "You Have registered for " + count + " COURSE(S) and your Total Amount to be pay is :-  " + totamtpay.Text + " Rs. ";
                lblstatusnew.ForeColor = System.Drawing.Color.Red;
                btnSubmit.Visible = false;
                lvCurrentSubjects.Visible = false;
                //btnback.Visible = true;
                objCommon.DisplayMessage(updDetails, "Re-Checking Registration Done Successfully", this.Page);
                btnRemoveList.Visible = false;
            }
            else
            {

                objCommon.DisplayMessage(updDetails, "Failed To Registered Courses", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }
    private void FillExamFees()
    {
        //int sessionno = Convert.ToInt32(Session["currentsession"]);
        int sessionno = Convert.ToInt32(Session["SESSIONNO"]);

        DataSet ds = objActController.GetFeeItemAmounntRevaluation(sessionno);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountrea = ds.Tables[0].Rows[0]["AMOUNT"].ToString();

            if (amountrea.Contains("."))
            {
                int index = amountrea.IndexOf('.');
                string result = amountrea.Substring(0, index);
                lblstureath.Text = result;

            }
            string amountrec = ds.Tables[0].Rows[1]["AMOUNT"].ToString();
            if (amountrec.Contains("."))
            {
                int index = amountrec.IndexOf('.');
                string result = amountrec.Substring(0, index);
                lblsturecth.Text = result;
            }

            lblstureath.Visible = true;
            lblsturecth.Visible = true;
        }

    }
    private void ShowDetails()
    {
        try
        {
            int idno = 0;
            int sessionno = Convert.ToInt32(Session["currentsession"]);
            StudentController objSC = new StudentController();



            string Session_Name = string.Empty;
            Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (Session_Name != "")
            {
                lblsession.Text = Convert.ToString(Session_Name);

                string Session_No = string.Empty;
                Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
                Session["SESSIONNO"] = Convert.ToInt32(Session_No);
                if (ViewState["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"]);
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
                }

                if (idno > 0)
                {
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
                            hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                            hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                            hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            ViewState["COLLEGE_ID"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            ViewState["IDNO"] = lblName.ToolTip;
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                            objCommon.FillDropDownList(ddlRevalRegSemester, "ACD_STUDENT_RESULT_HIST SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + "AND IDNO=" + ViewState["IDNO"].ToString(), "SR.SEMESTERNO");
                            divCourses.Visible = true;
                            tblInfo.Visible = true;
                            //pnlstart.Visible = false;
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
    private void BindCourseListForReval()
    {
        DataSet dsCurrCourses = null;
        btnback.Visible = false;
        if (ddlRevalRegSemester.SelectedIndex > 0)
        {
            //Show Courses for Reassesment/Revaluation

            int idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            int sessionno = Convert.ToInt32(Session["SESSIONNO"]);


            dsCurrCourses = objSC.GetCourseFor_RevalPhotocopyChallenge(Convert.ToInt32(idno), sessionno, Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToInt32(hdfDegreeno.Value), Convert.ToInt32(lblScheme.ToolTip));


            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;
                divCourses.Visible = true;
                //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                //{
                //    CheckBox chk = item.FindControl("chkrechecking") as CheckBox;
                //    for (int i = 0; i < dsCurrCourses.Tables[0].Rows.Count; i++)
                //    {
                //        if (dsCurrCourses.Tables[0].Rows[i]["Apporve_status"].ToString() == "Approved")
                //        {
                //            chk.Checked = true;
                //        }
                //        else
                //        {
                //            chk.Checked = false;
                //        }
                //    }
                //}
               

                // string retcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(PREV_STATUS)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + lblSemester.ToolTip + "AND SUBID=1 AND PREV_STATUS=1");
                //string retcount = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "COUNT(COURSENO)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND SUBID IN(1,3) AND  GRADE='F'");

                string retcount = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "COUNT(COURSENO)", "IDNO=" + Session["idno"] + " AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND SUBID IN(1,3) AND  GRADE='F'");
                //DataSet Registration = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "IDNO", "IDNO=" + idno + " AND SESSIONNO=" + Session["SESSIONNO"].ToString() + " AND SCHEMENO=" + lblScheme.ToolTip + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue), "");
                //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                //{
                //    if (Registration.Tables[0].Rows.Count > 0)
                //    { 

                //    }
                //}
                if (Convert.ToInt32(retcount) > 3)
                {
                    foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                    {

                        CheckBox cbRow = dataitem.FindControl("chkreassesment") as CheckBox;
                        cbRow.Enabled = false;


                    }
                }

                trNote.Visible = true;
                // btnshow.Visible = false;
                // btncancel1.Visible = false;
                btnSubmit.Visible = true;
                btnRemoveList.Visible = true;
            }

            else
            {
                // btnshow.Visible = true;
                // btncancel1.Visible = true;
                lvCurrentSubjects.Visible = false;
                trNote.Visible = false;
                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;
                objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\n In case of any query contact Admin", this.Page);
                trNote.Visible = false;
                btnSubmit.Visible = false;
                btnRemoveList.Visible = false;

            }
        }
        else
        {
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            lvCurrentSubjects.Visible = false;
        }
    }


    //private void IsRevaluationApproved()
    //{
    //    string ApproveStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "ISNULL(REV_APPROVE_STAT,0)=1 AND IDNO=" + Session["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue));
    //    if (ApproveStatus != "0")
    //    {
    //        btnPrintRegSlip.Visible = true;
    //    }
    //    else
    //    {
    //        btnPrintRegSlip.Visible = false;
    //    }
    //}
    //private void IsRevaluationRegistered()
    //{
    //    string RegisteredStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "REV_APPROVE_STAT=0 AND IDNO=" + Session["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue));
    //    if (RegisteredStatus != "0")
    //    {
    //        btnPrintRegSlip.Visible = false;
    //    }
    //    else
    //    {
    //        btnPrintRegSlip.Visible = false;
    //    }
    //}
    #endregion
    protected void btnProceed_Click(object sender, EventArgs e)
    {

        divNote.Visible = false;
        divCourses.Visible = true;
        ShowDetails();
        btnSubmit.Visible = false;
        //btnPrcdToPay.Visible = false;
        pnlSearch.Visible = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedValue = null;
        txtEnrollno.Text = string.Empty;
        divCourses.Visible = false;
        tblInfo.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtEnrollno.Text.Trim() + "'");
        string count = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(COURSENO)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtEnrollno.Text.Trim() + "]", this.Page);
        }


        //if (count == "0")
        //{
        //    objCommon.DisplayMessage("Student does not apply for Revaluation.", this.Page);
        //}
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtEnrollno.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            this.ShowDetails();
        }
    }
    protected void ddlRevalRegSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        // This is for checking the countif the payment is completed
        //int exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=1 AND  CAN = 0 AND COM_CODE='R' AND IDNO=" + ViewState["IDNO"]));
        int exreg = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND IDNO=" + ViewState["IDNO"]));
        // This is for checking the count if Paid Through challan
        int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=0 AND  CAN = 1 AND COM_CODE='R' AND IDNO=" + ViewState["IDNO"]));
        if (exreg > 0)
        {
            objCommon.DisplayMessage(this.updDetails, "Selected Semester Rechecking Revaluation Registration Already Done!", this.Page);
            BindCourseListForReval();
            //btnPrintReport.Visible = true;
            // lvCurrentSubjects.DataSource = null;
            //totamtpay.Text = null;
            ////lvCurrentSubjects.DataBind();
            ////divCourses.Visible = false;
            ////btnshow.Visible = false;
            //btnRemoveList.Visible = false;
            //btnSubmit.Visible = false;
            //btnreprint.Visible = false;
            ////radiolist.Visible = false;
            //BtnOnlinePay.Visible = false;
            //btnback.Visible = false;
            //lblstatusnew.Visible = false;
            //lblStatus.Visible = false;
            //trNote.Visible = false;
            //btnSubmit.Visible = false;
            //btnRemoveList.Visible = false;
        }
        else if (count > 0)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "You Have Already Generated Challan For Selected Semester. Reprint From Print Challan Button!\nPlease Reconcile Your challan To confirm Registration";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            totamtpay.Text = null;
            lblstatusnew.Visible = false;
            btnreprint.Visible = true;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            btnRemoveList.Visible = false;
            btnSubmit.Visible = false;
            btnPrintReport.Visible = false;
            BtnPrntChalan.Visible = false;
            //radiolist.Visible = false;
            BtnOnlinePay.Visible = false;
            btnback.Visible = false;
            trNote.Visible = false;

        }
        else
        {
            BindCourseListForReval();
            //radiolist.Visible = false;
            totamtpay.Text = null;
            lblStatus.Visible = false;
            lblstatusnew.Visible = false;
            btnback.Visible = false;
            //divCourses.Visible = true;
            //btnRemoveList.Visible = true;
            //btnSubmit.Visible = true;
            btnPrintReport.Visible = false;
            btnreprint.Visible = false;
            btnPrintReport.Visible = false;
            BtnPrntChalan.Visible = false;
            btnback.Visible = false;
            //trNote.Visible = true;
        }
    }
    //protected void btnshow_Click(object sender, EventArgs e)
    //{
    //    if (ddlRevalRegSemester.SelectedIndex <= 0)
    //    {
    //        objCommon.DisplayMessage(updDetails, "Please Select Applying Semester", this.Page);

    //    }
    //    else
    //    {
    //        BindCourseListForReval();

    //    }              
    //}
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        ddlRevalRegSemester.SelectedIndex = 0;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            string Amount = totamtpay.Text.ToString() == "" ? "0" : totamtpay.Text.ToString();
            Session["TOTAL_AMT"] = Convert.ToDecimal(Amount).ToString(".00");
            int TotalAmount = Convert.ToInt32(Amount);
            //if (TotalAmount <= 0)
            //{
            //    objCommon.DisplayUserMessage(updDetails, "Amount To Be Pay, Cannot Be Zero Or Less Than Zero.Please Select Atleast One Course For Reassesment & Rechecking Registration!", this.Page);

            //}
            //else
            //{
            //radiolist.SelectedIndex = -1;
            SubmitCourses();
            BindCourseListForReval();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnRemoveList_Click(object sender, EventArgs e)
    {
        totamtpay.Text = "";
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.Visible = false;
        ddlRevalRegSemester.SelectedIndex = 0;
        btnSubmit.Visible = false;
        btnRemoveList.Visible = false;
        //btncancel1.Visible = false ;
        //btnshow.Visible = false;
        trNote.Visible = false;
        //radiolist.Visible = false;
        BtnOnlinePay.Visible = false;
        BtnPrntChalan.Visible = false;
        lblstatusnew.Visible = false;
        lblStatus.Visible = false;
        btnback.Visible = false;
    }
    protected void BtnPrntChalan_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        //SubmitPaymentDetails();
        // radiolist.Visible = false;
        ShowReportP("ExamRegistrationSlipRevaluation", "ExamFeeCollectionRevalReport.rpt");
    }
    protected void BtnOnlinePay_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        // radiolist.Visible = false;
        //SubmitPaymentDetails();

    }
    //protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (radiolist.SelectedValue == "1")
    //    {
    //        BtnOnlinePay.Visible = true;
    //        BtnPrntChalan.Visible = false;
    //        btnSubmit.Visible = false;

    //    }
    //    if (radiolist.SelectedValue == "2")
    //    {
    //        BtnPrntChalan.Visible = true;
    //        BtnOnlinePay.Visible = false;
    //        btnSubmit.Visible = false;
    //    }
    //}
    private void GetSessionValues()
    {
        Session["FirstName"] = lblName.Text;

        Session["OrderID"] = lblOrderID.Text;
    }
    // -----This report is for printing challan for revaluation Application------
    private void ShowReportP(string reportTitle, string rptFileName)
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
        int semesterno = Convert.ToInt32(ddlRevalRegSemester.SelectedValue);

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

    // ----This report is for printing Rechecking & Reassesment Application------
    private void ShowReport(string reportTitle, string rptFileName)
    {

        //int sessionno = Convert.ToInt32(Session["currentsession"]);
        int sessionno = Convert.ToInt32(Session["SESSIONNO"]);
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        int semesterno = Convert.ToInt32(ddlRevalRegSemester.SelectedValue);
        string RevalExam = string.Empty;
        RevalExam = "R";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ViewState["usertype"].ToString() == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEMESTERNO=" + semesterno + ",@COM_CODE=" + RevalExam + ",@UserName=" + Session["username"].ToString();
            }
            else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + semesterno + ",@COM_CODE=" + RevalExam + ",@UserName=" + Session["username"].ToString();
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
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        ShowReport("Recheck&RevalRegistrationSlip", "rptExamRecheckReassRegslipNit.rpt");
        //radiolist.Visible = false;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();


    }
    protected void reprint_Click(object sender, EventArgs e)
    {
        ShowReportP("ExamRegistrationSlipRevaluation", "ExamFeeCollectionRevalReport.rpt");
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        BtnPrntChalan.Visible = false;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        //BindCourseListForReval();
        totamtpay.Text = null;
        lblstatusnew.Visible = false;
        btnPrintReport.Visible = false;
        btnreprint.Visible = false;
        //radiolist.Visible = false;
        BtnPrntChalan.Visible = false;




    }
}


