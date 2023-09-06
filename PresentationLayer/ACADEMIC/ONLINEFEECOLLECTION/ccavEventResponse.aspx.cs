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
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;

public partial class ccavEventResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    //FeeCollectionController objFees = new FeeCollectionController();
    EventCreationController objEC = new EventCreationController();

    string hash_seq = string.Empty;
    #endregion
    string Participant = string.Empty;
    string TitleId = string.Empty;
    string Regno = string.Empty;
    string Candidatename = string.Empty;
    string Email = string.Empty;
    string Mobile = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string workingKey = "598E8D2E71937502A7BF3A96D4E06E95";//put in the 32bit alpha numeric key in the quotes provided here
                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                //string UserNo = Session["userno"].ToString();
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }
                Participant = Params["merchant_param5"];
                Candidatename = Params["billing_name"];
                Mobile = Params["billing_tel"];
                Email = Params["billing_email"];

                string tranID = Params["tracking_id"];
                string orderno = Params["order_id"];
                ViewState["orderno"] = orderno;
                //RefNumber=Params["tracking_id"];
                string StatusF = Params["order_status"];
                string msg = "";

                TitleId = Params["billing_notes"];
                Session["TitleId"] = TitleId;
                //DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NAME", "UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG", "UA_NO=" + Convert.ToInt32(userno), string.Empty);
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                    
                //     Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                //     Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                //    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                //    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                //    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                 
                //}
               
                //Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                //Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                //Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                string EventName = objCommon.LookUp("ACD_EVENT_CREATION", "EVENT_TITLE", "EVENT_TITLE_ID=" + TitleId);
                string StartDate = objCommon.LookUp("ACD_EVENT_CREATION", "CONVERT(varchar, EVENT_START_DATE, 103)", "EVENT_TITLE_ID=" + TitleId);
                string EndDate = objCommon.LookUp("ACD_EVENT_CREATION", "CONVERT(varchar, EVENT_END_DATE, 103)", "EVENT_TITLE_ID=" + TitleId);

                Session["payment"] = "payment";

                string payId = orderno;
                hdfOrderId.Value = payId;
                string transid = tranID;
                //string recipt = "EF";
                string status = string.Empty;
                //string PaymentMode = "EXAM FEES COLLECTION";
                string CashBook = string.Empty;
                string amount = Params["amount"];
                lbltransactionId.Text = tranID;
                lblamount.Text = amount;
                lblstudentname.Text = Params["billing_name"]; 
                //lblidno.Text = Regno;
                lblEventname.Text = EventName;
                ldlresponceHandling.Text = payId;
                string ip = Request.ServerVariables["REMOTE_HOST"];
                Session["ipAddress"] = ip;
                    if (StatusF == "Success")
                    {
                        divSuccess.Visible = true;


                        int output = objEC.InsertSuccessOnlinePayment(Convert.ToInt32(TitleId), Convert.ToInt32(Participant), Convert.ToDouble(amount), orderno, transid, Candidatename, Mobile, Email, StatusF, Convert.ToString(Session["ipAddress"]));

                        if (output == -99)
                        {
                            divSuccess.Visible = false;
                            divFailure.Visible = true;
                            status = "Payment Fail";

                            //objFees.InsertOnlinePaymentlog(Idno, recipt, PaymentMode, amount, status, payId);
                        }
                        else
                        {
                            ViewState["out"] = output;

                        }

                        string message = "<b>Dear " + lblstudentname.Text + "</b><br />";
                        message += "<br />Your Event Registration done for <b>" + EventName + "</b> with payment of <b>Rs. " + amount + "</b>";
                        message += "<br />Event Date <b>" + StartDate + "</b> to <b>" + EndDate + "</b>";
                        message += "<br /><br /><br />Thank You<br />";
                        message += "<br />Team MAKAUT, WB<br />";
                        message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";
                        string subject = "MAKAUT | Event Registration";
                        Task<int> task = Execute(message, Email, subject);
                        //status = task.Result;


                    }
                    else
                    {
                        divFailure.Visible = true;
                        status = "Payment Fail";
                        //objFees.InsertOnlinePaymentlog(Idno, recipt, PaymentMode, amount, status, payId);
                        //int output = objEC.InsertSuccessOnlinePayment(Convert.ToInt32(TitleId), Convert.ToInt32(Participant), Convert.ToDouble(amount), orderno, transid, Candidatename, Mobile, Email, status, Convert.ToString(Session["ipAddress"]));
                    }

                    
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }


    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

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

        byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

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
   

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        ShowReport("OnlineEventPayment", "rptEventPaymentReceipt.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            Session["userno"] = "1";
            string orderid = Convert.ToString(ViewState["orderno"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=53,@P_ORDER_ID=" + orderid;

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
    
}