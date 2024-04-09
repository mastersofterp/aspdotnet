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
using BusinessLogicLayer.BusinessEntities.RazorPay;
using Razorpay.Api;
using Newtonsoft.Json;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Security.Cryptography.X509Certificates;
using mastersofterp_MAKAUAT;
using System.Net.Security;


public partial class RazorPayOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    Ent_Pay_Response ObjPR = new Ent_Pay_Response();
    OrganizationController objOrg = new OrganizationController();
    FeeCollectionController feeController = new FeeCollectionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

    string hash_seq = string.Empty;
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataSet Orgds = null;
                var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                Session["OrgId"] = OrgId;
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

              
                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string key = string.Empty;
                string secret = string.Empty;
                string amount = string.Empty;
                double actualamount = 0;
                double amountTax = 0;
                int Installmentno = 0; 
                //string paymentId = Request.Form["hdnPaymentId"];
                
                string paymentId = Request.Form["razorpay_payment_id"];
                string razorpay_order_id = Request.Form["razorpay_order_id"];                
                string razorpay_signature = Request.Form["razorpay_signature"];
                Session["RZPay_Order_ID"] = razorpay_order_id;
                Session["ipAddress"] = Request.ServerVariables["REMOTE_HOST"];
                //Dictionary<string, object> input = new Dictionary<string, object>();
                //double tranAmt = Convert.ToDouble(Request.Form["hdnAmount"]);
                //input.Add("amount", Convert.ToInt32(Math.Round(tranAmt * 100))); // this amount should be same as transaction amount

                //Get fetch pg details
                GetFetchRazorPayPG();

                //key = Convert.ToString("rzp_test_rWxD79Cq93I4CT"); 
                //string secret = Convert.ToString("s3Uc3f3KhXUJQW0EA7SKVsBz"); 

                RazorpayClient client = new RazorpayClient(Session["RazKey"].ToString(), Session["Secrets"].ToString());
                var json = "";
                if (paymentId != null)
                {
                    Payment payment = client.Payment.Fetch(paymentId);
                    json = payment.Attributes.ToString(Formatting.None);

                }

                //dynamic JsonObj = JsonConvert.DeserializeObject<dynamic>(json);
                Ent_PaymentNew objPay = JsonConvert.DeserializeObject<Ent_PaymentNew>(json);
                long unixDate = Convert.ToInt64(objPay.created_at);
                
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime sTime = start.AddSeconds(unixDate).ToLocalTime();

                var MS_order_id = objPay.notes.Order_ID.ToString();
                Session["Order_ID"] = MS_order_id;
                ViewState["order_id"] = MS_order_id;
                Installmentno = Convert.ToInt32(objPay.description);  // use for installment no.


                #region check student details
                // int idno = Convert.ToInt32(objCommon.LookUp("ACD_UG_RAZORPAY_NOT_CAPTURE_TRANS CT LEFT JOIN USER_ACC UA ON UA.UA_NO = CT.USERNO", "UA_IDNO", "ORDER_ID = '" + MS_order_id + "' AND RAZORPAY_ORDER_ID = '" + razorpay_order_id + "'"));
                int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "IDNO", "ORDER_ID='" + MS_order_id + "'"));
                string regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = "+ idno +" ");
                Session["idno"] = idno;
                Session["regno"] = regno;
                DataSet ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT S ON(S.IDNO = U.UA_IDNO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO = S.BRANCHNO)", "UA_NAME", "UA_NO,UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG,B.LONGNAME, S.SEMESTERNO", "UA_TYPE = 2 AND UA_IDNO=" + Convert.ToInt32(Session["idno"]), string.Empty);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    Session["userno"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    Session["branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                    Session["paysemester"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                }

                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");
                #endregion

                // check payment status 
                var transaction_id = string.Empty;
                if (objPay.method == "card") 
                {
                    transaction_id = "";
                }
                if (objPay.method == "upi")
                {
                    transaction_id = objPay.acquirer_data.upi_transaction_id.ToString();
                }

                if (objPay.status == "captured")
                {
                    ObjPR.TransactionId = transaction_id; //objPay.notes.merchant_order_id.ToString();
                    ObjPR.Status = "Ok";
                    ObjPR.Message = "captured";
                    ObjPR.ErrorMessage = "NA";
                    ObjPR.ResponceTransactionId = objPay.id;
                    ObjPR.Amount = Convert.ToDouble(objPay.amount) / 100;

                    actualamount = Convert.ToDouble(objPay.amount) / 100;    //tranAmt;
                    amountTax = Convert.ToDouble(Convert.ToDouble(ObjPR.Amount) - Convert.ToDouble(actualamount));
                    ObjPR.TransactionTime = sTime;
                    ObjPR.OrderId = objPay.order_id;
                    ObjPR.CreatedBy = Convert.ToInt32(Session["userno"]);   //Session["USERNO"].ToString();
                    ObjPR.UserNo = Session["userno"].ToString();                 //Session["USERNO"].ToString();
                    ObjPR.IPAddress = Session["ipAddress"].ToString();       
                    //ObjPR.OrderId = Session["Order_ID"].ToString();
                    //ObjPR.IPAddress = Session["IPADDR"].ToString();
                    ObjPR.MACAddress = "";
                    
                    int Result = 0;

                    //Ent_Payment_notes ss = new Ent_Payment_notes();
                    Notes ss = new Notes();
                    ss = objPay.notes;
                    string comment_date = objPay.created_at.ToString();
                    lblRegNo.Text = Session["regno"].ToString();
                    lblstudentname.Text = Convert.ToString(Session["userfullname"]);
                    lblOrderId.Text = razorpay_order_id;
                    //ViewState["order_id"] = razorpay_order_id;
                    lblamount.Text = Convert.ToString(ObjPR.Amount);
                    amount = Convert.ToString(ObjPR.Amount);
                    lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    string mihpayid = ObjPR.ResponceTransactionId;
                    lblBranch.Text = Convert.ToString(Session["Branchname"]);
                    lblSemester.Text = Convert.ToString(Session["paysemester"]);
                    lblTrasactionId.Text = mihpayid;

                    divSuccess.Visible = true;
                    divFailure.Visible = false;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + MS_order_id + "'");
                    int output = 0;
                    string UA_IDNO = string.Empty;
                    string UserFirstPaymentStatus = string.Empty;
                    string UA_NAME = string.Empty;

                    //add TGPCET ORGID = ?
                    if (Session["OrgId"].ToString() == "3" || Session["OrgId"].ToString() == "4")
                    {
                        UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                        UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");

                        if (UA_IDNO == UA_NAME)
                        {
                            UserFirstPaymentStatus = "5151";
                            ViewState["First_PaymentStatus"] = "5151";

                        }
                        else
                        {
                            ViewState["First_PaymentStatus"] = "0";
                        }
                    }
                    else
                    {
                        ViewState["First_PaymentStatus"] = "0";
                    }

                   //Convert.ToInt32(Session["Installmentno"]);
                    if (Convert.ToInt32(Installmentno) > 0)
                    {
                        output = objFees.InsertInstallmentOnlinePayment_DCR(Convert.ToString(Session["idno"]), rec_code, MS_order_id, mihpayid, "O", "1", Convert.ToString(amount), "Success", Convert.ToInt32(Installmentno), "-");
                        
                        if (ViewState["First_PaymentStatus"] == "5151")
                        {
                            UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                            UPDATE_USER(UA_IDNO, 1);

                            Sendmail();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        output = objFees.InsertOnlinePayment_DCR(Convert.ToString(Session["idno"]), rec_code, MS_order_id, mihpayid, "O", "1", Convert.ToString(amount), "Success", Session["regno"].ToString(), "-");

                        if (ViewState["First_PaymentStatus"] == "5151")
                        {
                            UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                            UPDATE_USER(UA_IDNO, 1);

                            Sendmail();
                        }
                        else
                        {

                        }
                    }
                    btnPrint.Visible = true;
                   
                }
                else
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + MS_order_id + "'");
                    objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), rec_code, "O", amount, "Payment Fail", MS_order_id);

                    //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                    btnPrint.Visible = false;
                }

              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

   #region Get Fetch RazorPay PG Details
    protected void GetFetchRazorPayPG()
    {
        DataSet pg_ds = new DataSet();
        try
        {
            pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "ACCESS_CODE", "CHECKSUM_KEY, MERCHANT_ID, INSTANCE", "ACTIVE_STATUS= 1", "CONFIG_ID DESC");   //Merchant_Id
            if (pg_ds.Tables[0].Rows.Count > 0)
            {
                Session["RazKey"] = pg_ds.Tables[0].Rows[0]["ACCESS_CODE"].ToString(); 
                Session["Secrets"] = pg_ds.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                Session["MerchantId"] = pg_ds.Tables[0].Rows[0]["MERCHANT_ID"].ToString(); 
                Session["Instance"] = pg_ds.Tables[0].Rows[0]["INSTANCE"].ToString(); 

            }
        }
        catch (Exception ex)
        { }

    }
   #endregion

    #region Method
    //public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    //{
    //    try
    //    {
    //        //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
    //        //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
    //        //Img.ContentId = "MyImage";   

    //        ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
    //        //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
    //        //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
    //        string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
    //        string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();

    //        msg.From = new MailAddress(fromAddress, "NIT GOA");
    //        msg.To.Add(new MailAddress(ToID));

    //        msg.Subject = "Your transaction with MAKAUT";

    //        const string EmailTemplate = "<html><body>" +
    //                                 "<div align=\"left\">" +
    //                                 "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                                  "<tr>" +
    //                                  "<td>" + "</tr>" +
    //                                  "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
    //                                 "</tr>" +
    //                                 "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
    //                                 "</tr>" +
    //                                 "</table>" +
    //                                 "</div>" +
    //                                 "</body></html>";
    //        StringBuilder mailBody = new StringBuilder();
    //        //mailBody.AppendFormat("<h1>Greating !!</h1>");
    //        mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody5);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg1);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg2);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody4);
    //        mailBody.AppendFormat("<br />");
    //        string Mailbody = mailBody.ToString();
    //        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
    //        msg.IsBodyHtml = true;
    //        msg.Body = nMailbody;

    //        smtp.Host = "smtp.gmail.com";

    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = true;
    //        smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //        smtp.EnableSsl = true;
    //        smtp.Send(msg);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

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
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
       string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
   // Response.Redirect("~/academic/Semester_Registration.aspx?pageno=1797");
    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
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

    #region Print button event
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        if (Session["ReceiptType"] == "AEF")
        {
            ShowReport("BacklogRegistration", "rptOnlineReceiptbBacklog_ATLAS.rpt");
        }
        else if ( (Session["ReceiptType"] == "PRF") || (Session["ReceiptType"] == "RF") )
        {
            ShowReportPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptPhotoCopy_ATLAS.rpt");
        }
        else
        {
            ShowReportOnline("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
           // ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
            //ShowReport("OnlineFeePayment", "FeeCollectionReceiptForCash_ATLAS.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
        int IDNO = Convert.ToInt32((Session["idno"]));

        string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + Session["idno"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["order_id"]) + "'");
        string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + Session["idno"].ToString() + "'");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
           // url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportPhotoCopy(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;
            int IDNO = Convert.ToInt32(Session["idno"]);
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(Session["Order_ID"]) + "'"));
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT COLLEGE_ID", "IDNO=" + IDNO));
            int revalType = 0;
            string rec_code = objCommon.LookUp("ACD_DCR", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + Convert.ToString(Session["Order_ID"]) + "'");
            if (rec_code == "PRF")
            {
                revalType = 1;
            }
            else if (rec_code == "RF")
            {
                revalType = 2;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_REVALTYPE=" + revalType;

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
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportOnline(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32((Session["idno"]));
            // DCR ENTRY NO FOUND --- order_id
            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + Session["idno"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["order_id"]) + "'");
            string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + Session["idno"].ToString() + "'");
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_UA_NAME=" + Session["UAFULLNAME"];
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_UA_NAME=" + Session["UAFULLNAME"];

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }

    public void Sendmail()
    {
        string email_type = string.Empty;
        string Link = string.Empty;
        int sendmail = 0;
        string subject = string.Empty;
        string srnno = string.Empty;
        string pwd = string.Empty;
        int status = 0;
        string IDNO = Session["IDNO"].ToString();
        string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);
        string Username = string.Empty;
        string Password = string.Empty;
        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]));
        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + Session["idno"].ToString());
        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
        string COLLEGE_CODE = objCommon.LookUp("REFF", "CODE_STANDARD", "");
        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(Session["idno"]));
        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(Session["idno"]));
        Username = REGNO;
        Password = REGNO;

        DataSet ds = getModuleConfig();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
            sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

            if (sendmail == 1)
            {
                subject = "New MIS Login Credentials";
                string message = "";
                message += "<p>Dear :<b>" + Name + "</b> </p>";
                message += "<p><b>" + Branchname + "</b></p>";
                message += "<p>Your fees have been submitted successfully and you have been registered for the program mentioned above.Your new Login credentials are as follows</p><p>" + MISLink + " </p><p>Username   : " + Username + " <br/>Password    : " + Password + "</p>";
                message += "<p>Note for Provisional Registration only:</p>";
                message += "<p>All the documents must be uploaded on URL: <b>" + MISLink + "</b>";
                message += "<p>Process of fee payment: Login using above credentials in <b>" + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.: ";
                message += "<p>The fee payment should be made within 7 days of receiving this mail/letter, after which your claim for admission may be requested.</p>";
                message += "<p style=font-weight:bold;>Thanks<br>Team Admissions<br>" + COLLEGE_CODE + " University<br></p>";

                status = objSendEmail.SendEmail(EmailID, message, subject); //Calling Method
            }
        }

        if (status == 1)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }

    }

    protected void UPDATE_USER(string UA_NO, int FirstTimePay)
    {
        try
        {
            string UA_PWD = string.Empty;
            string password = string.Empty;
            //string student_Name = "ROHIT";
            int IDNO = 0;
            string REGNO = string.Empty;
            string Email = string.Empty;
            string UA_ACC = string.Empty;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 3 || Convert.ToInt32(Session["OrgId"].ToString())== 4)
            {
                IDNO = Convert.ToInt32(Session["idno"]);
                REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = '" + Session["idno"] + "'");
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(REGNO.ToString());

            }
            else
            {
                IDNO = Convert.ToInt32(Session["idno"]);
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(IDNO.ToString());
                REGNO = IDNO.ToString();
            }

            CustomStatus CS = (CustomStatus)feeController.UpdateUser(REGNO, UA_PWD, IDNO, FirstTimePay);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

}