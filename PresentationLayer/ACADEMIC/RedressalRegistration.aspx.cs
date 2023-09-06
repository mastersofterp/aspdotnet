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

public partial class ACADEMIC_RedressalRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();

    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;


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
                        GetStudentDeatlsForEligibilty();
                        ViewState["action"] = "add";
                        divNote.Visible = true;
                        pnlSearch.Visible = false;
                        //ShowDetails();
                       
                        totamtpay.Text = "";
                    }
                
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //objCommon.DisplayMessage(this.updDetails, "You Are Not Authorized To View This Page!!", this.Page);
                    //return;
                    //pnlstart.Visible = false;
                    //divNote.Visible = false;
                    //GetStudentDeatlsForEligibilty();
                    ViewState["action"] = "add";
                    pnlSearch.Visible = false;
                }
            }

        }
        divMsg.InnerHtml = string.Empty;
        PopulateDropDownList();
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
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
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
                pnlstart.Visible = false;
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

 private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
       // sessionno = Session["currentsession"].ToString();
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));

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
            pnlSearch.Visible = false;
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
        int feeitemid = 0;
        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
        {
             feeitemid = 4;
            DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();

                int index = amountth.IndexOf('.');
                result1 = amountth.Substring(0, index);

            }
        }
        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
        {
            feeitemid = 5;
            DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();

                int index = amountth.IndexOf('.');
                result1 = amountth.Substring(0, index);

            }
        }
        double totalamount = 0.00;
        string totamt = string.Empty;

        string rectype = string.Empty;
        string finalamtredress = "0";
        string finalamtpaper = "0";

        if (lvCurrentSubjects.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkRedressal") as CheckBox).Checked == true)
                {
                    totalamount = totalamount + Convert.ToDouble(result1);
                }
            }
            totamt = totalamount.ToString();
        }

        if (radiolist.SelectedValue != "")
        {
            if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
            {
                rectype = "AR";
                finalamtredress = totamt;
            }
            else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
            {
                rectype = "PS";
                finalamtpaper = totamt;
            }

            if (radiolist.SelectedValue == "1")
            {
                int result = 0;
               
               
                // result = feeController.InsertOnlinePayment_REVALUATION_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 1, Convert.ToString(Session["TOTAL_AMT"]), "0", Convert.ToString(Session["TOTAL_AMT"]));              
               // result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 1, totamtpay.Text, "0", "0", totamtpay.Text, "R");
                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 1, "0", "0", "0", "0", "0", finalamtredress, finalamtpaper, totamt, rectype);
                Session["RECIEPT_CODE"] = "RF";
                if (result > 0)
                {
                    SendTransaction();
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
                //result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 2, totamtpay.Text, "0", "0", totamtpay.Text, "R");
                result = feeController.InsertOnlinePayment_Exam_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 2, "0", "0", "0", "0", "0", finalamtredress, finalamtpaper, totamt, rectype);
                //result = feeController.InsertOnlinePayment_REVALUATION_REgistration(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["SESSIONNO"]), Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToString(lblOrderID.Text), 2, Convert.ToString(Session["TOTAL_AMT"]), "0", Convert.ToString(Session["TOTAL_AMT"]));                        
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
            //  objCommon.DisplayUserMessage(updBulkReg, "Can not proceed due to insufficient data!", this.Page);
            return;
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //ddlSession.SelectedIndex = 1;
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
            int opertion = 0;
            string RECHECKORREASS = string.Empty;
            int checkreadrepaper = 0;
            if (lvCurrentSubjects.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox chkrechecking = dataitem.FindControl("chkRedressal") as CheckBox;
                    //CheckBox chkreassesment = dataitem.FindControl("chkreassesment") as CheckBox;
                    if (chkrechecking.Checked)
                    {
                        selection = true;
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                        string ext = (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString() == "" ? "0" : (dataitem.FindControl("hdnextmark") as HiddenField).Value.ToString();
                        objSR.EXTERMARKS += ext + "$";
                        objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
                        {
                            RECHECKORREASS += 1 + "$";
                        }
                        else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
                        {
                            RECHECKORREASS += 2 + "$";
                        }
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
                    objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                    return;
                }
                if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
                {
                    checkreadrepaper = 1;
                }
                else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
                {
                    checkreadrepaper = 2;
                }
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                objSR.EXTERMARKS = objSR.EXTERMARKS.TrimEnd('$');
                objSR.CCODES = objSR.CCODES.TrimEnd('$');
                RECHECKORREASS = RECHECKORREASS.TrimEnd('$');
                //objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]);
                objSR.SESSIONNO = Convert.ToInt32(Session["SESSIONNO"].ToString());
                objSR.IDNO = Convert.ToInt32(ViewState["IDNO"]);
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
                result = objSReg.AddUpdateRevalPhotoCopyChallenegeRegisteration(objSR, 0, opertion, RECHECKORREASS, checkreadrepaper);
                if (result > 0)
                {
                    objCommon.DisplayMessage(this.updDetails, "Selected Courses Saved Sucessfully,Print the chalan and Reconcile it for the successful Payment.", this.Page);
                    //radiolist.Visible = true;
                    BtnPrntChalan.Visible = true;
                    BtnOnlinePay.Visible = false;
                }
                else
                {

                    objCommon.DisplayMessage(updDetails, "Failed To Registered Courses", this.Page);
                }
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
        ActivityController objActController = new ActivityController();
        int feeitemid = 0;
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
        sessionno = Convert.ToInt32(Session["SESSIONNO"]);
         feeitemid = 4;
        DataSet ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                int index = amountth.IndexOf('.');
                string result = amountth.Substring(0, index);


                lblRedressal.Text = result;
                lblRedressal.Visible = true;
            string amountpr = ds.Tables[0].Rows[1]["AMOUNT"].ToString();
            //if (amountpr.Contains('.'))
            //{
                //int index = amountpr.IndexOf('.');
                //string result = amountpr.Substring(0, index);
                //lblendpr.Text = result;
                //lblstuendpr.Text = result;
                //lblstuendpr.Visible = true;
            //}



        }
        feeitemid = 5;
       ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            int index = amountth.IndexOf('.');
            string result = amountth.Substring(0, index);


            lblpaper.Text = result;
            lblpaper.Visible = true;
            //string amountpr = ds.Tables[0].Rows[1]["AMOUNT"].ToString();
            //if (amountpr.Contains('.'))
            //{
            //int index = amountpr.IndexOf('.');
            //string result = amountpr.Substring(0, index);
            //lblendpr.Text = result;
            //lblstuendpr.Text = result;
            //lblstuendpr.Visible = true;
            //}



        }


    }
    private void ShowDetails()
    {
        int idno = 0;
        int sessionno = Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();



        string Session_Name = string.Empty;
        Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        lblsession.Text = Convert.ToString(Session_Name);

        string Session_No = string.Empty;
        Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        Session["SESSIONNO"] = Convert.ToInt32(Session_No);
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            ViewState["idno"] = idno;
        }
        else if (ViewState["usertype"].ToString() == "1")
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
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        ViewState["COLLEGE_ID"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        ViewState["IDNO"] = lblName.ToolTip;
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                        objCommon.FillDropDownList(ddlRevalRegSemester, "ACD_STUDENT_RESULT_HIST SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + "AND IDNO=" + ViewState["IDNO"].ToString(), "SR.SEMESTERNO");

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

        if (ddlRevalRegSemester.SelectedIndex > 0)
        {
            //Show Courses for Reassesment/Revaluation


            int sessionno = Convert.ToInt32(Session["SESSIONNO"]);


            dsCurrCourses = objSC.GetCourseFor_RevalPhotocopyChallenge(Convert.ToInt32(ViewState["IDNO"]), sessionno, Convert.ToInt32(ddlRevalRegSemester.SelectedValue), Convert.ToInt32(hdfDegreeno.Value), Convert.ToInt32(lblScheme.ToolTip));


            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {

                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

                // string retcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(PREV_STATUS)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + lblSemester.ToolTip + "AND SUBID=1 AND PREV_STATUS=1");
                //string retcount = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "COUNT(COURSENO)", "IDNO=" + Session["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND SUBID IN(1,3) AND  GRADE='F'");
                //if (Convert.ToInt32(retcount) >= 4)
                //{
                //    foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                //    {

                //        CheckBox cbRow = dataitem.FindControl("chkreassesment") as CheckBox;
                //        cbRow.Enabled = false;


                //    }
                //}

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
                totamtpay.Text = string.Empty;
                BtnPrntChalan.Visible = false;
            }
        }
        else
        {
            lvCurrentSubjects.Visible = false;
            trNote.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            //objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\n In case of any query contact Admin", this.Page);
            trNote.Visible = false;
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            totamtpay.Text = string.Empty;
            BtnPrntChalan.Visible = false;
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
        //divCourses.Visible = true;
        
        ShowDetails();
        FillExamFees();
        btnSubmit.Visible = false;
        btnPrcdToPay.Visible = false;
        //pnlSearch.Visible = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtEnrollno.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtEnrollno.Text.Trim() + "]", this.Page);
        }

        //string count = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(COURSENO)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
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
            btnProceed_Click(sender, e);
        }
    }
    protected void ddlrevalseeing_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvCurrentSubjects.Visible = false;
            trNote.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            //objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\n In case of any query contact Admin", this.Page);
            trNote.Visible = false;
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            totamtpay.Text = string.Empty;
            BtnPrntChalan.Visible = false;
            ddlRevalRegSemester.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void ddlRevalRegSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        // This is for checking the countif the payment is completed
        int exreg=0;
        int count = 0;
        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
        {
             exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=1 AND  CAN = 0 AND COM_CODE='AR' AND IDNO=" + ViewState["IDNO"]));
             count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=0 AND  CAN = 1 AND COM_CODE='AR' AND IDNO=" + ViewState["IDNO"]));
        }
        else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
        {
            exreg = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=1 AND  CAN = 0 AND COM_CODE='PS' AND IDNO=" + ViewState["IDNO"]));
            count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(1)", "sessionno=" + Session["SESSIONNO"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + " AND RECON=0 AND  CAN = 1 AND COM_CODE='PS' AND IDNO=" + ViewState["IDNO"]));
        }
        
      
        if (exreg > 0)
        {
            if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
            {
                objCommon.DisplayMessage(this.updDetails, "Selected Semester Appeal Registration Already Done! You can only print Reciept", this.Page);
            }
            else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
            {
                objCommon.DisplayMessage(this.updDetails, "Selected Semester Paper Seeing Registration Already Done! You can only print Reciept", this.Page);
            }
            btnPrintReport.Visible = true;
            // divCourses.Visible = false;
            // btnshow.Visible = false;
            //btncancel1.Visible = false;
        }
        else if (count > 0)
        {
            lblStatus.Visible = true;
            
                lblStatus.Text = "You Have Already Generated Challan For Selected Semester. Reprint From Print Challan Button!\nPlease Reconcile Your challan To confirm Registration";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            btnreprint.Visible = true;
        }
        else
        {
            lblStatus.Visible = false;
            btnreprint.Visible = false;
            BindCourseListForReval();

            divCourses.Visible = true;
            // btnshow.Visible = true;
            // btncancel1.Visible = true;
            btnPrintReport.Visible = false;
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
            //string Amount = totamtpay.Text.ToString() == "" ? "0" : totamtpay.Text.ToString();
            //Session["TOTAL_AMT"] = Convert.ToDecimal(Amount).ToString(".00");
            //int TotalAmount = Convert.ToInt32(Amount);
            //if (TotalAmount <= 0)
            //{
                //objCommon.DisplayUserMessage(updDetails, "Amount To Be Pay, Cannot Be Zero Or Less Than Zero.Please Select Atleast One Course For Reassesment & Rechecking Registration!", this.Page);

            //}
            //else
            //{
                SubmitCourses();
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
        radiolist.Visible = false;
        BtnOnlinePay.Visible = false;
        BtnPrntChalan.Visible = false;
    }
    protected void BtnPrntChalan_Click(object sender, EventArgs e)
    {
        SubmitPaymentDetails();
        string rectype = string.Empty;
        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
        {
            rectype = "AR";
           
        }
        else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
        {
            rectype = "PS";
            
        }
        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND RECIEPT_CODE='EF' and COM_CODE='"+rectype+"'"));
       // ShowReportP("ExamRegistrationSlipRevaluation", "ExamFeeCollectionRevalReport.rpt");

        ShowReportP("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["IDNO"]), "1", 1);
    }
    protected void BtnOnlinePay_Click(object sender, EventArgs e)
    {
        SubmitPaymentDetails();

    }
    protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radiolist.SelectedValue == "1")
        {
            BtnOnlinePay.Visible = true;
            BtnPrntChalan.Visible = false;

        }
        if (radiolist.SelectedValue == "2")
        {
            BtnPrntChalan.Visible = true;
            BtnOnlinePay.Visible = false;
        }
    }
    private void GetSessionValues()
    {
        Session["FirstName"] = lblName.Text;

        Session["OrderID"] = lblOrderID.Text;
    }
    // -----This report is for printing challan for revaluation Application------

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        //string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        //return param;


        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }
   
    private void ShowReportP(string rptName, int dcrNo, int studentNo, string copyNo, int param)
    {
        try
        {
            //btnReport.Visible = false;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=examReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
              url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
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

    //private void ShowReportP(string reportTitle, string rptFileName)
    //{
    //    //int sessionno = Convert.ToInt32(Session["currentsession"]);
    //    int idno = 0;
    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        idno = Convert.ToInt32(Session["idno"]);
    //    }
    //    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
    //    {
    //        idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
    //    }
    //    int semesterno = Convert.ToInt32(ddlRevalRegSemester.SelectedValue);

    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        if (ViewState["usertype"].ToString() == "2")
    //        {
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + ",@P_SEMESTERNO=" + semesterno;
    //        }
    //        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
    //        {
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + semesterno;
    //        }
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

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
        RevalExam = "AR";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ViewState["usertype"].ToString() == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + ",@P_SEMESTERNO=" + semesterno + ",@COM_CODE=" + RevalExam + ",@P_TYPE=" + Convert.ToInt32(ddlrevalseeing.SelectedValue) + ",@UserName=" + Session["username"].ToString();
            }
            else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + ",@P_SEMESTERNO=" + semesterno + ",@COM_CODE=" + RevalExam + ",@P_TYPE=" + Convert.ToInt32(ddlrevalseeing.SelectedValue) + ",@UserName=" + Session["username"].ToString();
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

    }
    protected void reprint_Click(object sender, EventArgs e)
    {
        string rectype = string.Empty;
        if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 1)
        {
            rectype = "AR";

        }
        else if (Convert.ToInt32(ddlrevalseeing.SelectedValue) == 2)
        {
            rectype = "PS";

        }
        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ddlRevalRegSemester.SelectedValue) + "AND RECIEPT_CODE='EF' and COM_CODE='" + rectype + "'"));
        // ShowReportP("ExamRegistrationSlipRevaluation", "ExamFeeCollectionRevalReport.rpt");

        ShowReportP("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["IDNO"]), "1", 1);
    }
}