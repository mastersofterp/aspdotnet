﻿using System;
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
using paytm;
using Paytm;
using Newtonsoft.Json;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;


public partial class PaytmOnlinePaymentRequest : System.Web.UI.Page
{
    #region Class declaration
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    ISGPayReturnParameter isgPayReqParams = null;
    OrganizationController objOrg = new OrganizationController();

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
    string OrderId = "";
   public string merchantKey = "";
   public string MID = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                DataSet Orgds = null;
                var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                Orgds = objOrg.GetOrganizationById(Convert.ToInt32(OrgId));
                byte[] imgData = null;
                if (Orgds.Tables != null)
                {
                    if (Orgds.Tables[0].Rows.Count > 0)
                    {

                        if (Orgds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
                        {
                            imgData = Orgds.Tables[0].Rows[0]["Logo"] as byte[];
                            imgCollegeLogo.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                        }
                        else
                        {
                            // hdnLogoOrg.Value = "0";
                        }

                    }
                }
               
                #region PGConfig Details

                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                lblSemester.Text = Convert.ToString(Session["paysemester"]);
                lblYear.Text = Session["YEARNO"].ToString();
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                var stud_email = Convert.ToString(Session["studEmail"]);
                var stud_phone = Convert.ToString(Session["studPhone"]);
            
                //DataSet ds1 = objFees.GetOnlinePaymentConfigurationAllDetailsV2(ConfigID);
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]), Convert.ToInt32(degreeno), Convert.ToInt32(college_id));

                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string MerchentID = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();            
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();      // PASS_CODE
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();                   //ENCYPTION_KEY
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();               //SECURE_SECRET
                    var ActivityName = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();
                    lblActivityName.Text = ActivityName;

                    Session["MerchentID"] = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString(); 
                    Session["SubMerchant_id"] = ds1.Tables[0].Rows[0]["SUBMERCHANT_ID"].ToString();
                    Session["BankFee_Type"] = ds1.Tables[0].Rows[0]["BANKFEE_TYPE"].ToString();    //Bind Value is MERCHANT_CATEGORY_CODE + BANK ID
                    Session["ResponseUrl"] = ResponseUrl;
                    Session["RequestUrl"] = RequestUrl;
                    Session["MerchentId"] = MerchentID;
                    Session["saltkey"] = saltkey;
                    Session["ClientId"] = accesscode;
                    Session["IndustryType_Or_Website"] = hashsequence;
                    Session["Instance"] = ds1.Tables[0].Rows[0]["INSTANCE"].ToString();

                }
                #endregion

                BindAndCheckPayDetails();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region Paytm Payment Gateway Fetch Details- Added by Gopal M. 05012024  Ticket#52554

    //Genrate OrderId With Fetch student Detailes
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
            result = objFees.InsertInstallmentOnlinePayment_TempDCR_PAYTM(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-", Session["saltkey"].ToString());
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR_PAYTM(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-", Session["saltkey"].ToString());
        }

        string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");

        if (orderid != "" || orderid != string.Empty || orderid == txnid1)
        {
            //Get Fetch initiatePayment Details
            var output = FetchPaytmPay_Details(txnid1);
            // tokenid = Session["tokenid"].ToString(); 
            if (output == "pass")
            {
                result = objFees.InsertPAYTMOnlinePaymentlog(Convert.ToInt32(Session["idno"]), Convert.ToDecimal(Session["studAmt"]), txnid1, tokenid, Session["paysemester"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            }
        }

    }

    //Check paytm payment initiated process event
    public string FetchPaytmPay_Details(string orderId)
    {
        string returnVal = string.Empty;
        string SECURE_SECRET = Session["accesscode"].ToString(); 
        string ENCYPTION_KEY = Session["saltkey"].ToString();        
        string MCC_BANKID = Session["BankFee_Type"].ToString();   

        // define message string for errors
        string LINK = string.Empty;
        string BANK_ID = string.Empty;

        var IndustryOrWebsite = Session["IndustryType_Or_Website"].ToString();    // WEB_Retail_WEBSTAGING
        var splt = IndustryOrWebsite.Split('_');
        var CHANNEL_ID = splt[0].ToString();
        var INDUSTRY_TYPE_ID = splt[1].ToString();
        var WEBSITE = splt[2].ToString();

        try
        {

            if (Convert.ToInt32(Session["Instance"]) == 1)
            {
                LINK = Session["RequestUrl"].ToString();   //"--https://securegw-stage.paytm.in/order/process?";   
            }
            else if (Convert.ToInt32(Session["Instance"]) == 2)
            {
                LINK = Session["RequestUrl"].ToString();  //"--https://secure.paytm.in/order/process?";
            }
          
            #region client details bind
            String merchantKey = Session["saltkey"].ToString();   
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("MID", Session["MerchentId"].ToString());
            parameters.Add("CHANNEL_ID", CHANNEL_ID);                     // "WEB"
            parameters.Add("INDUSTRY_TYPE_ID", INDUSTRY_TYPE_ID);  // "Retail"
            parameters.Add("WEBSITE", WEBSITE);                                 // "WEBSTAGING"
            parameters.Add("EMAIL", "gmjain11@gmail.com");                 //Session["studEmail"].ToString()
            parameters.Add("MOBILE_NO", "8552960810");                      //Session["studPhone"].ToString()
            parameters.Add("CUST_ID", Session["idno"].ToString());
            parameters.Add("ORDER_ID", orderId);
            parameters.Add("TXN_AMOUNT", Session["studAmt"].ToString());
            parameters.Add("CALLBACK_URL", Session["ResponseUrl"].ToString());  //--http://localhost:55403/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/PaytmOnlinePaymentResponse.aspx

            string paytmURL = "https://securegw-stage.paytm.in/order/process?";
            #endregion

            #region check checksum
            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            //checksumData.Text = checksum;
            //string checksum = CheckSumWeb();
            string outputHTML = "";

            //outputHTML = "<html>";
            //outputHTML += "<head>";
            //outputHTML += "<title>Merchant Check Out Page</title>";
            //outputHTML += "</head>";
            //outputHTML += "<body>";
            ////outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            //outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            //outputHTML += "<table border='1'>";
            //outputHTML += "<tbody>";

            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            //outputHTML += "</tbody>";
            //outputHTML += "</table>";
            ////outputHTML += "<script type='text/javascript'>";
            ////outputHTML += "document.f1.submit();";
            ////outputHTML += "</script>";
            //outputHTML += "</form>";
            //outputHTML += "</body>";
            //outputHTML += "</html>";
            //HttpContext.Current.Response.Write(outputHTML);

            hidParams.InnerHtml = outputHTML;
          
            returnVal = "pass";
            #endregion

        }
        catch (Exception ex)
        {
           var message = "(51) Exception encountered. " + ex.Message;
            if (ex.StackTrace.Length > 0)
            {           
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

    //protected void btnPay_Click(object sender, EventArgs e)
    //{
    //    BindAndCheckPayDetails();
    //}

}

