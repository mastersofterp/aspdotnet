using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Json;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ISGPayOnlinePaymentRequest : System.Web.UI.Page
{
    #region Class declaration
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    ISGPayReturnParameter isgPayReqParams = null;
    System.Collections.SortedList transactionData = null;
    string hash_seq = string.Empty;
    #endregion

    #region Global variable  declaration
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    public string txnid1 = string.Empty;
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string tokenid = string.Empty;
    int degreeno = 0;
    int college_id = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
              
                SqlDataReader dr = objCommon.GetCommonDetails();

                if (dr != null)
                {
                    if (dr.Read())
                    {
                        lblCollege.Text = dr["COLLEGENAME"].ToString();
                        lblAddress.Text = dr["College_Address"].ToString();
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }

                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                //firstname.Text = Convert.ToString(Session["payStudName"]);

                lblSemester.Text = Convert.ToString(Session["paysemester"]);
                // email.Text = Convert.ToString(Session["studEmail"]);
                // phone.Text = Convert.ToString(Session["studPhone"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int ConfigID = Convert.ToInt32(Session["ConfigID"]);
                lblYear.Text = Session["YEARNO"].ToString();

                DataSet ds1 = objFees.GetOnlinePaymentConfigurationAllDetailsV2(ConfigID);

                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();            //MERCHANT_ID;
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();      // PASS_CODE
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();                   //ENCYPTION_KEY
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();               //SECURE_SECRET
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();

                    Session["SubMerchant_id"] = ds1.Tables[0].Rows[0]["SUBMERCHANT_ID"].ToString();
                    Session["BankFee_Type"] = ds1.Tables[0].Rows[0]["BANKFEE_TYPE"].ToString();    //Bind Value is MERCHANT_CATEGORY_CODE + BANK ID
                    Session["ResponseUrl"] = ResponseUrl;
                    Session["RequestUrl"] = RequestUrl;
                    Session["merchentkey"] = merchentkey;
                    Session["saltkey"] = saltkey;
                    Session["accesscode"] = accesscode;
                    Session["PassCode"] = hashsequence;
                    //Session["Instance"] = ds1.Tables[0].Rows[0]["INSTANCE"].ToString();

                }

                BindAndCheckPayDetails();
                //FetchISGPay_Details();
               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region ISGPay Payment Gateway Fetch Details  Added by Gopal M. 05122023  Ticket#51702

    // Fetch student Detailes
    protected void BindAndCheckPayDetails()
    {        
        string UserId = Convert.ToString(Session["userno"]);
        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    Reprocess:
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        Random ram = new Random();
        int i = ram.Next(1, 9);
        int j = ram.Next(21, 51);
        int k = ram.Next(471, 999);
        int l = System.DateTime.Today.Day;
        int m = System.DateTime.Today.Month;
        string txnid1 = (i + "" + j + "" + k + "" + l + "" + m).ToString() + "-" + UserId;
        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + txnid1 + "'");

        Session["OrderId"] = txnid1;
        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }


        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
        }
        else if (Session["ReceiptType"].ToString() == "PRF" || Session["ReceiptType"].ToString() == "RF" || Session["ReceiptType"].ToString() == "SEF")
        {
            result = objFees.InsertPayment_Log_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Session["semesternos"].ToString(), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }

        string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");

        if (orderid != "" || orderid != string.Empty || orderid == txnid1)
        {
            //Get Fetch initiatePayment Details
            var output = FetchISGPay_Details(txnid1);
            // tokenid = Session["tokenid"].ToString(); 
            if (output == "pass")
            {
                result = objFees.InsertISGPayOnlinePaymentlog(Convert.ToInt32(Session["idno"]), Convert.ToDecimal(Session["studAmt"]), txnid1, tokenid, Session["paysemester"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            }
        }

    }

    //Check ISPPay details and payment initiated process event
    public string FetchISGPay_Details(string orderId)
    {
        string returnVal = string.Empty;
        string SECURE_SECRET = Session["accesscode"].ToString();  //"E59CD2BF6F4D86B5FB3897A680E0DD3E";
        string ENCYPTION_KEY = Session["saltkey"].ToString();         //"5EC4A697141C8CE45509EF485EE7D4B1";
        string MCC_BANKID = Session["BankFee_Type"].ToString();    //4112_000004
        Panel_Debug.Visible = false;
        Panel_StackTrace.Visible = false;

        // define message string for errors
        string message = "No Messages";
        string LINK = string.Empty;
        var PASS_CODE = string.Empty;
        var BANK_ID = string.Empty;
        var TERMINAL_ID = string.Empty;
        var MERCHANT_CATEGORY_CODE = string.Empty;

        try
        {

            if (Convert.ToInt32(Session["Instance"]) == 1)
            {
                LINK = "https://sandbox.isgpay.com/ISGPay/request.action";     //UAT -https://sandbox.isgpay.com:8443/ISGPay/request.action";  
            }
            else if (Convert.ToInt32(Session["Instance"]) == 2)
            {
                LINK = "https://sandbox.isgpay.com/ISGPay/request.action";
            }
            else
            {
                LINK = "https://sandbox.isgpay.com/ISGPay/request.action";
            }

            /* For Page.Request.Form as parameter*/
            ISGPayEncryption encObj = new ISGPayEncryption();

            /* For SortedList as parameter*/
            transactionData = new System.Collections.SortedList(new ISGPayHashGeneration());
            //Random rnd = new Random();
            //int ordNO = rnd.Next(1111111, 9999999);
            //var TxnRefNo = "TEST-" + ordNO;

            // SVPL4257&000004&10100781&4112

            if (MCC_BANKID.ToString() != null && MCC_BANKID.ToString() != "")
            {
                var splt = MCC_BANKID.ToString().Split('_');
                MERCHANT_CATEGORY_CODE = splt[0];
                BANK_ID = splt[1];
                if (BANK_ID.Length == 1)
                {
                    BANK_ID = "00000" + BANK_ID;
                }
            }

            double amt = Convert.ToDouble(Session["studAmt"]);
            string feeAmt = amt.ToString("N0");
            // Compulsory information
            transactionData.Add("Version", "1");
            transactionData.Add("TxnRefNo", orderId);
            transactionData.Add("Amount", feeAmt);
            transactionData.Add("PassCode", Session["PassCode"].ToString());             //SVPL4257;
            transactionData.Add("BankId", BANK_ID);                                                  //000004);
            transactionData.Add("TerminalId", Session["SubMerchant_id"].ToString());  //10100781);
            transactionData.Add("MerchantId", Session["merchentkey"].ToString());     //101000000000781
            transactionData.Add("MCC", MERCHANT_CATEGORY_CODE);                     //4112
            transactionData.Add("Currency", "356");  //na

            transactionData.Add("TxnType", "Pay");
            transactionData.Add("ReturnURL", Session["ResponseUrl"].ToString());    // "http://localhost:50472/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ISGPayOnlinePaymentResponse.aspx");   //Session["ResponseUrl"].ToString();
            //transactionData.Add("UDF01", Session["idno"].ToString());

            transactionData.Add("OrderInfo", Session["idno"].ToString()); //optinal passing student -Idno values
            ////Optinal 
            //transactionData.Add("Email ", "abz@isg.com");
            //transactionData.Add("Phone  ", "919012345678");
            ////Conditional
            //transactionData.Add("payOpt", "dc");
            //transactionData.Add("CardNumber", "5453010000095323");
            //transactionData.Add("ExpiryDate", "022025");
            //transactionData.Add("CardSecurityCode", "123");
            //transactionData.Add("BankCode", "ISG001");
            ////Optinal 
            //transactionData.Add("FirstName", "JOHN");
            //transactionData.Add("LastName", "WILLIAMS");
            //transactionData.Add("Street", "GUNBOW STREET");
            //transactionData.Add("City", "MUMBAI");
            //transactionData.Add("ZIP", "400001");
            //transactionData.Add("State", "MAHARASHTRA");

            //transactionData.Add("UDF01", Session["payStudName"].ToString());
            //transactionData.Add("UDF02", Session["Branchname"].ToString());
            //transactionData.Add("UDF03", Session["paysemester"].ToString());
            //transactionData.Add("UDF04", Session["Installmentno"].ToString());

            isgPayReqParams = encObj.Encrypt(transactionData, ENCYPTION_KEY, SECURE_SECRET);
            /* For SortedList as parameter End*/
            MerchantId.Value = isgPayReqParams.MerchantId;
            TerminalId.Value = isgPayReqParams.TerminalId;
            Version.Value = isgPayReqParams.Version;
            BankId.Value = isgPayReqParams.BankId;
            EncData.Value = isgPayReqParams.EncData;

            returnVal = "pass";
        }
        catch (Exception ex)
        {
            message = "(51) Exception encountered. " + ex.Message;
            if (ex.StackTrace.Length > 0)
            {
                Label_StackTrace.Text = ex.ToString();
                Panel_StackTrace.Visible = true;
                returnVal = "fail";
            }

        }
        return returnVal;

    }
    #endregion

    #region  Common method event call 
    public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    {
        try
        {
            //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";   

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
            //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
            string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
            string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "NIT GOA");
            msg.To.Add(new MailAddress(ToID));

            msg.Subject = "Your transaction with MAKAUT";

            const string EmailTemplate = "<html><body>" +
                                     "<div align=\"left\">" +
                                     "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                      "<tr>" +
                                      "<td>" + "</tr>" +
                                      "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</div>" +
                                     "</body></html>";
            StringBuilder mailBody = new StringBuilder();
            //mailBody.AppendFormat("<h1>Greating !!</h1>");
            mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody5);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg1);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg2);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody4);
            mailBody.AppendFormat("<br />");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            msg.IsBodyHtml = true;
            msg.Body = nMailbody;

            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;

    }
    #endregion


}