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
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;


public partial class PayUOnlinePaymentResponse : System.Web.UI.Page
    {
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    OrganizationController objOrg = new OrganizationController();

    string hash_seq = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
        {
        if (!IsPostBack)
            {
            try
                {              
                //SqlDataReader dr = objCommon.GetCommonDetails();

                //if (dr != null)
                //    {
                //    if (dr.Read())
                //        {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //        }
                //    }

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

                string responsevalue = string.Empty;
                string resposestring = string.Empty;
                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                    {
                    responsevalue = HttpContext.Current.Request.Form[key];
                    resposestring = resposestring + key + ":'" + responsevalue + "'|";
                    }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ResponseAlert", "alert(" + resposestring + ")", true);
                string messageString = string.Empty;
                string urlPath = string.Empty;
                urlPath = HttpContext.Current.Request.Url.AbsoluteUri;
                string value = HttpContext.Current.Request.Form["Response Code"];
                string mandatoryFields = Request.Form["mandatory fields"];
                string[] mandatoryFieldsArray = mandatoryFields.Split(new char[] { '|' });
              // lblmessage.Text = value;
              // lblmandfileds.Text = Request.Form["mandatory fields"];
              // lblmerchantid.Text = Request.Form["merchantid"];
               lblOrderId.Text = Request.Form["ReferenceNo"];
               ViewState["Order_id"] = Request.Form["ReferenceNo"];

               lblamount.Text = mandatoryFieldsArray[2];
               lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
               string Idno = string.Empty;
               Idno = mandatoryFieldsArray[3];

              string Studname = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Idno)));
              string BRANCHNO = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Idno)));
              string BranchName = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + BRANCHNO );
              string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Idno));
              string SEMSTERNO = mandatoryFieldsArray[6];
              string RECIPT_CODE = objCommon.LookUp("ACD_DEMAND", "RECIEPT_CODE", "IDNO=" + Convert.ToInt32(Idno) + " AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(SEMSTERNO));

             // lbl1.Text = Idno;
            //  lbl2.Text = Studname;
             // lbl3.Text = BranchName;
              lblRegNo.Text = Regno;
            
              Session["ReceiptType"] = RECIPT_CODE;
              ViewState["IDNO"] = Idno;
              ViewState["userno"] = objCommon.LookUp("USER_ACC", "ISNULL(UA_NO,0) UA_NO", " UA_TYPE=2 AND UA_IDNO=" + Convert.ToInt32(Idno)); //mandatoryFieldsArray[5];
              lblstudentname.Text = Studname;
              lblBranch.Text = BranchName;
              lblSemester.Text = SEMSTERNO;
              int installmentno = Convert.ToInt32(mandatoryFieldsArray[5]);
              string UniqueRefNumber = string.Empty;//Transaction ID              
              string PaymentMode = string.Empty;             
              string TotalAmount = string.Empty;            
              string TransactionAmount = string.Empty;
              string merchantid = string.Empty;             
              string returnurl = string.Empty;
              string ReferenceNo = string.Empty;
              string sub_merchant_id = string.Empty;
              string transaction_amount = string.Empty;
              string paymode = string.Empty;
              string Recipt_Code = string.Empty;
              string firstname = string.Empty;
              string APPLICATION_USERNO = string.Empty;//udf1
              string APPLICATION_ID = string.Empty;//udf2
              string DegreeNo = string.Empty;//udf3
              string BranchNo = string.Empty;//udf4
              string DegreeBranchNo = string.Empty;
              string order_id = string.Empty;
              string amount = Request.Form["Transaction Amount"];
              string emailId = string.Empty;             
              string saltkey = string.Empty;           
              string hash_seq = string.Empty;
              order_id = Request.Form["ReferenceNo"];                        
              string StatusF = string.Empty;
              lblTrasactionId.Text = Request.Form["Unique Ref Number"];            
              string tranID = Request.Form["Unique Ref Number"];
              if (value == "E000")
                  {

                  divSuccess.Visible = true;
                  btnPrint.Visible = true;
                 
                  divFailure.Visible = false;

                  int result = 0;
                  string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;

                 int output = 0;               
                  if (Convert.ToInt32(installmentno) > 0) //added by rohit m for checking installment payment
                      {
                      output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, Session["ReceiptType"].ToString(), order_id, tranID, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");
                      }
                  else
                      {
                      output = objFees.InsertOnlinePayment_DCR(Idno, Session["ReceiptType"].ToString(), order_id, tranID, "O", "1", amount, "Success", Regno, messageString);
                      }

                  if (output == -99)
                      {
                      divSuccess.Visible = false;
                      divFailure.Visible = true;
                      }
                  else
                      {
                      ViewState["out"] = output;

                      }

                  }
              else
                  {
                  divSuccess.Visible = false;
                  divFailure.Visible = true;
                  //divStudDetails.Visible = false;
                  int result = 0;
                  btnPrint.Visible = false;
                  }
                }
            catch (Exception ex)
                {
                ////lblData2.Text = ex.Message;
                ////During Payment Exception catch
                divSuccess.Visible = false;
                divFailure.Visible = true;
                //divStudDetails.Visible = false;
                int result = 0;
                btnPrint.Visible = false;

                objCommon.DisplayMessage(this.Page, "Oops! Something went wrong. " + ex.Message, this.Page);
                return;
                Response.Write(ex.Message);
                }
            }
        }

    public static string decryptEazyPayData(string textToEncrypt, string key)
        {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
            {
            len = keyBytes.Length;
            }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateDecryptor(rijndaelCipher.Key, rijndaelCipher.IV);
        string plaintext = null;
        byte[] cipherText = Convert.FromBase64String(textToEncrypt);
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                   transform, CryptoStreamMode.Read))
                {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                    plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        return plaintext;
        }


    public void sendPaidSuccssEmailToApplicant(string ToEmailID, string applicantName, string applicantIDs, string amount, string orderId, string transId, string transDate)
        {
        try
            {

            string EmailTemplate = "<html>" +
           "<body><div align=\"left\"><table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
           "<tr><td></td></tr><tr><td width=\"80%\" >Dear <b>" + applicantName + "</b> ,</td></tr>" +
           "<tr></tr>" +
           "<tr><td></td></tr><tr><td>Greetings from DA-IICT.</td></tr>" +
           "<tr></tr>" +
           "<tr><td></td></tr><tr><td><h4>Payment done successfully with following details:</h4></td></tr></table>" +
           "<table cellpadding=\"0\" cellspacing=\"0\" width=\"80%\" align=\"left\" border=\"1\">" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Application ID :</td><td style=\"padding:10px 0px 10px 10px;\">" + applicantIDs + "</td></tr>" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Student Name :</td><td style=\"padding:10px 0px 10px 10px;\">" + applicantName + "</td></tr>" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Amount :</td><td style=\"padding:10px 0px 10px 10px;\">" + amount + "</td></tr>" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Order Id :</td><td style=\"padding:10px 0px 10px 10px;\">" + orderId + "</td></tr>" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Transaction Id :</td><td style=\"padding:10px 0px 10px 10px;\">" + transId + "</td></tr>" +
           "<tr><td style=\"padding:10px 0px 10px 10px;\">Transaction Date :</td><td style=\"padding:10px 0px 10px 10px;\">" + transDate + "</td></tr></table>" +
           "<table cellpadding=\"0\" cellspacing=\"0\" width=\"70%\" align=\"left\">" +
           "<tr></tr>" +
           "<tr></tr>" +
           "<tr><td>Regards,</td></tr>" +
           "<tr><td>DA-IICT Admission Team</td></tr></table></div></body></html>";
            //"<tr><td>rcssadmission@rajagiri.edu</td></tr></table></div></body></html>";

            string nMailbody = EmailTemplate;//.Replace("#content", Mailbody);
            string Subject = "Your Application Transaction with DA-IICT";
            //Task<int> ret = Execute(nMailbody, ToEmailID, Subject);
            TransferToEmail(ToEmailID, nMailbody, Subject);

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EasyPayOnlinePaymentResponse.sendPaidSuccssEmailToApplicant-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
        {
        int ret = 0;

        try
            {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            //var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
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

    public int TransferToEmail(string useremail, string message, string subject)
        {
        int ret = 0;
        try
            {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
                {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                // fromPassword = Common.DecryptPassword(fromPassword);
                msg.From = new System.Net.Mail.MailAddress(fromAddress, "DAIICT");
                msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                msg.Subject = subject;

                msg.Body = message;
                msg.IsBodyHtml = true;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                    return true;
                    };

                smtp.Send(msg);
                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                    {
                    return ret = 1;
                    //Storing the details of sent email
                    }
                else
                    {
                    return ret = 0;
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return ret;

        }

    #region Method
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

    #region other click events
    protected void btnBack_Click(object sender, EventArgs e)
        {
        //string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        ////Response.Redirect(returnpageurl);
        ////  Response.Redirect("~/default.aspx");
        //string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
        //string AppUrl = "" + loginurl + "/default_DAIICT.aspx";
        //Response.Redirect(AppUrl, false);
        //Response.Redirect("~/default.aspx");
        string returnpageurl = Convert.ToString(ViewState["ReturnpageUrl"]);
        //Response.Redirect(returnpageurl);
        Response.Redirect("~/default.aspx");
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        ShowReport("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
    }

    //private void ShowReport(string reportTitle, string rptFileName)
    //    {
    //    try
    //        {
    //        int IDNO = Convert.ToInt32(ViewState["IDNO"]);
    //        //string collegecode = objCommon.LookUp("reff", "college_code", "");
    //        Session["userno"] = ViewState["userno"];
    //        string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + IDNO + "' AND ORDER_ID ='" + ViewState["Order_id"].ToString() + "'");

    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;

    //        url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(72) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        //To open new window from Updatepanel

    //        }
    //    catch (Exception ex)
    //        {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);
            Session["userno"] = ViewState["userno"];

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + ViewState["IDNO"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["Order_id"]) + "'");
            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(IDNO)));
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_UA_NAME=" + Session["UAFULLNAME"];

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


    #endregion
    }