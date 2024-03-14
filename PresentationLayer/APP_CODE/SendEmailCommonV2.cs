using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using IITMS.UAIMS;
using System.Threading.Tasks;
using SendGrid;
using EASendMail;
using SendGrid.Helpers.Mail;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml;

//using SendGrid.Helpers.Mail;

namespace BusinessLogicLayer.BusinessLogic
{
    public class SendEmailCommonV2
    {

        Common objCommon = new Common();


        static byte[] ConvertDataSetToExcel(DataSet ds)
        {
            using (var ms = new MemoryStream())
            {
                using (var package = new ExcelPackage(ms))
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        var worksheet = package.Workbook.Worksheets.Add(dt.TableName);

                        // Set header row style
                        var headerRange = worksheet.Cells[1, 1, 1, dt.Columns.Count];
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                        worksheet.Cells.AutoFitColumns();
                    }

                    package.Save();
                    return ms.ToArray();
                }
            }
        }

        //Added by Nikhil L. on 02/08/2023




        public int SendEmail_New(string pageNo, string emailId, string message, string subject, string ccMails, string bccMails, DataSet ds, string attachmentfilename, byte[] bytefile, string type)
        {
            try
            {
                string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Values = string.Empty; int configType = 0;
                string providerName = string.Empty; int status = 0;
                DataSet dsCheck = null;
                SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (dsCheck.Tables[0].Rows[0]["EMAIL"].ToString().Equals("1"))
                    {
                        configType = 1;             // For email
                    }

                    SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                    SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                    SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                    dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                }
                if (configType == 1)        //Email
                {
                    for (int i = 0; i < dsCheck.Tables[1].Rows.Count; i++)
                    {
                        providerName = dsCheck.Tables[1].Rows[i]["SERVICE_PROVIDER_NAME"].ToString();
                        DataRow dr = dsCheck.Tables[1].Rows[i];
                        if (providerName.ToUpper() == "GSUIT" && providerName != "")
                        {
                            status = GSuit(emailId, message, subject, dr);
                        }
                        else if (providerName.ToUpper() == "SENDGRID_NEW" && providerName != "")
                        {
                            Task<int> ret;
                            //ret = SendGrid(Message, ToEmail, Subject, dsEmailCred);
                            if (attachmentfilename != "")
                            {
                                ret = SendGrid(message, emailId, subject, ccMails, bccMails, ds, attachmentfilename, bytefile, type, dr);
                            }
                            else
                            {
                                ret = SendGrid(message, emailId, subject, dr);
                            }
                        }
                        else if (providerName.ToUpper() == "OUTLOOK" && providerName != "")
                        {
                            if (attachmentfilename != "")
                            {
                                status = OutLook(message, emailId, subject, ccMails, bccMails, attachmentfilename, bytefile, type, dr);
                            }
                            if (ccMails != "" || bccMails != "")
                            {
                                status = OutLook(message, emailId, subject, ccMails, bccMails, dr);
                            }
                            else
                            {
                                status = OutLook(message, emailId, subject, dr);
                            }
                        }
                        else if (providerName.ToUpper() == "AMAZON" && providerName != "")
                        {
                            if (attachmentfilename != "")
                            {
                                status = Amazon(emailId, message, subject, ccMails, bccMails, ds, attachmentfilename, bytefile, type, dr);
                            }
                            else if (ccMails != "" || bccMails != "")
                            {
                                status = Amazon(emailId, message, subject, ccMails, bccMails, dr);
                            }
                            else
                            {
                                status = Amazon(emailId, message, subject, dr);
                            }
                        }
                    }
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int SendEmail_New(string pageNo, string emailId, string message, string subject, string ccMails, string bccMails)
        {
            try
            {
                string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Values = string.Empty; int configType = 0;
                string providerName = string.Empty; int status = 0;
                DataSet dsCheck = null;
                SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (dsCheck.Tables[0].Rows[0]["EMAIL"].ToString().Equals("1"))
                    {
                        configType = 1;             // For email
                    }

                    SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                    SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                    SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                    dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                }
                if (configType == 1)        //Email
                {
                    for (int i = 0; i < dsCheck.Tables[1].Rows.Count; i++)
                    {
                        providerName = dsCheck.Tables[1].Rows[i]["SERVICE_PROVIDER_NAME"].ToString();
                        DataRow dr = dsCheck.Tables[1].Rows[i];
                        if (providerName.ToUpper() == "GSUIT" && providerName != "")
                        {
                            status = GSuit(emailId, message, subject, dr);
                        }
                        else if (providerName.ToUpper() == "SENDGRID_NEW" && providerName != "")
                        {
                            Task<int> ret;
                            //ret = SendGrid(Message, ToEmail, Subject, dsEmailCred);
                            //if (attachmentfilename != "")
                            //{
                            ret = SendGrid(message, emailId, subject, ccMails, bccMails, dr);
                            //}
                            //else
                            //{
                            //    ret = SendGrid(message, emailId, subject, dr);
                            //}
                        }
                        else if (providerName.ToUpper() == "OUTLOOK" && providerName != "")
                        {
                            //if (ccMails != "" || bccMails != "")
                            //{
                            status = OutLook(message, emailId, subject, ccMails, bccMails, dr);
                            //}
                            //else
                            //{
                            //    status = OutLook(message, emailId, subject, dr);
                            //}
                        }
                        else if (providerName.ToUpper() == "AMAZON" && providerName != "")
                        {
                            //if (attachmentfilename != "")
                            //{
                            //    status = Amazon(emailId, message, subject, ccMails, bccMails, ds, attachmentfilename, bytefile, type, dr);
                            //}
                            //else if (ccMails != "" || bccMails != "")
                            //{
                            status = Amazon(emailId, message, subject, ccMails, bccMails, dr);
                            //}
                            //else
                            //{
                            //    status = Amazon(emailId, message, subject, dr);
                            //}
                        }
                    }
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int SendEmail_New(string pageNo, string emailId, string message, string subject)
        {
            try
            {
                string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Values = string.Empty; int configType = 0;
                string providerName = string.Empty; int status = 0;
                DataSet dsCheck = null;
                SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (dsCheck.Tables[0].Rows[0]["EMAIL"].ToString().Equals("1"))
                    {
                        configType = 1;             // For email
                    }

                    SP_Name = "PKG_ACD_CHECK_EMAIL_SMS_WHATSAPP_LINK";
                    SP_Parameters = "@P_AL_NO,@P_CONFIG_TYPE";
                    SP_Values = "" + Convert.ToInt32(pageNo) + "," + configType + "";
                    dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                }
                if (configType == 1)        //Email
                {
                    for (int i = 0; i < dsCheck.Tables[1].Rows.Count; i++)
                    {
                        providerName = dsCheck.Tables[1].Rows[i]["SERVICE_PROVIDER_NAME"].ToString();
                        DataRow dr = dsCheck.Tables[1].Rows[i];
                        if (providerName.ToUpper() == "GSUIT" && providerName != "")
                        {
                            status = GSuit(emailId, message, subject, dr);
                        }
                        else if (providerName.ToUpper() == "SENDGRID_NEW" && providerName != "")
                        {
                            Task<int> ret;
                            //ret = SendGrid(Message, ToEmail, Subject, dsEmailCred);
                            //if (attachmentfilename != "")
                            //{
                            //    ret = SendGrid(message, emailId, subject, ccMails, bccMails, ds, attachmentfilename, bytefile, type, dr);
                            //}
                            //else
                            //{
                            ret = SendGrid(message, emailId, subject, dr);
                            //}
                        }
                        else if (providerName.ToUpper() == "OUTLOOK" && providerName != "")
                        {
                            //if (ccMails != "" || bccMails != "")
                            //{
                            //    status = OutLook(message, emailId, subject, ccMails, bccMails, dr);
                            //}
                            //else
                            //{
                            status = OutLook(message, emailId, subject, dr);
                            //}
                        }
                        else if (providerName.ToUpper() == "AMAZON" && providerName != "")
                        {
                            //if (attachmentfilename != "")
                            //{
                            //    status = Amazon(emailId, message, subject, ccMails, bccMails, ds, attachmentfilename, bytefile, type, dr);
                            //}
                            //else if (ccMails != "" || bccMails != "")
                            //{
                            //    status = Amazon(emailId, message, subject, ccMails, bccMails, dr);
                            //}
                            //else
                            //{
                            status = Amazon(emailId, message, subject, dr);
                            //}
                        }
                    }
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        static async Task<int> SendGrid(string Message, string toEmailId, string sub, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                Common objCommon = new Common();
                string codeStandard = dsCred["CODE_STANDARD"].ToString();
                var apiKey = dsCred["CKEY_USERID"].ToString();
                var client = new SendGridClient(apiKey);
                var from = new SendGrid.Helpers.Mail.EmailAddress(dsCred["EMAILID"].ToString(),
                codeStandard.ToString());
                var subject = sub;
                var to = new SendGrid.Helpers.Mail.EmailAddress(toEmailId, "");
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
        //added by Swapnil Sendgrid method with Attachment on 21-07-2023
        // modified by Nikhil L. on Sendgrid method with Attachment on 23/08/2023
        static async Task<int> SendGrid(string Message, string toEmailId, string sub, string ccemails, string bccemails, DataSet ds, string attachmentfilename, byte[] bytefile, string type, DataRow dsCred)
        {
            int ret = 0;

            try
            {
                Common objCommon = new Common();
                string codeStandard = dsCred["CODE_STANDARD"].ToString();
                var apiKey = dsCred["CKEY_USERID"].ToString();
                var client = new SendGridClient(apiKey);
                var from = new SendGrid.Helpers.Mail.EmailAddress(dsCred["EMAILID"].ToString(),
               codeStandard.ToString());
                var subject = sub;
                var htmlContent = Message;
                var emails = toEmailId.Split(',');
                var to = new List<EmailAddress>();
                foreach (var i in emails)
                {
                    to.Add(new EmailAddress(i));
                }
                var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                {
                    From = new EmailAddress(dsCred["EMAILID"].ToString(), codeStandard.ToString()),
                    Subject = sub,
                    HtmlContent = Message
                };
                var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);

                if (type == "excel")
                {
                    byte[] fileBytes = ConvertDataSetToExcel(ds);
                    var file = Convert.ToBase64String(fileBytes);
                    msg1.AddAttachment(attachmentfilename, file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
                if (type == "pdf")
                {
                    msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = Convert.ToBase64String(bytefile),
                        Filename = attachmentfilename,
                        Type = "application/pdf",
                        Disposition = "attachment"
                    }
                };
                }
                else
                {
                    msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = Convert.ToBase64String(bytefile),
                        Filename = attachmentfilename,
                        Type = type,
                        Disposition = "attachment"
                    }
                };
                }
                if (ccemails != "")
                {
                    var ccemail = ccemails.Split(',');
                    var cc = new List<EmailAddress>();
                    foreach (var i in ccemail)
                    {
                        cc.Add(new EmailAddress(i));
                        if (cc.Count > 0)
                        {
                            msg1.AddCcs(cc);
                        }
                    }
                }
                if (bccemails != "")
                {
                    var bccemail = bccemails.Split(',');
                    var bcc = new List<EmailAddress>();
                    foreach (var i in bccemail)
                    {
                        bcc.Add(new EmailAddress(i));
                        if (bcc.Count > 0)
                        {
                            msg1.AddBccs(bcc);
                        }
                    }
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                Console.WriteLine(msg1.Serialize());
                var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
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
        static async Task<int> SendGrid(string Message, string toEmailId, string sub, string ccemails, string bccemails, DataRow dsCred)
        {
            int ret = 0;

            try
            {

                Common objCommon = new Common();
                if (dsCred != null)
                {
                    string codeStandard = dsCred["CODE_STANDARD"].ToString();

                    var apiKey = dsCred["CKEY_USERID"].ToString();
                    var client = new SendGridClient(apiKey);
                    var from = new SendGrid.Helpers.Mail.EmailAddress(dsCred["EMAILID"].ToString(),
                   codeStandard.ToString());
                    var subject = sub;
                    var htmlContent = Message;
                    var emails = toEmailId.Split(',');
                    var to = new List<EmailAddress>();
                    foreach (var i in emails)
                    {
                        to.Add(new EmailAddress(i));
                    }
                    var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                    {
                        From = new EmailAddress(dsCred["EMAILID"].ToString(), codeStandard.ToString()),
                        Subject = sub,
                        HtmlContent = Message
                    };

                    var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);
                    if (ccemails != null)
                    {
                        var ccemail = ccemails.Split(',');
                        var cc = new List<EmailAddress>();
                        foreach (var i in ccemail)
                        {
                            cc.Add(new EmailAddress(i));
                            if (cc.Count > 0)
                            {
                                msg1.AddCcs(cc);
                            }
                        }
                    }
                    if (bccemails != "")
                    {
                        var bccemail = bccemails.Split(',');
                        var bcc = new List<EmailAddress>();
                        foreach (var i in bccemail)
                        {
                            bcc.Add(new EmailAddress(i));
                            if (bcc.Count > 0)
                            {
                                msg1.AddBccs(bcc);
                            }
                        }
                    }
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                    Console.WriteLine(msg1.Serialize());
                    var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
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
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        private int GSuit(string useremail, string message, string subject, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                if (dsCred != null)
                {
                    string fromAddress = dsCred["EMAILID"].ToString();
                    string fromPassword = dsCred["PASSWORDS"].ToString();
                    string shortcode = dsCred["CODE_STANDARD"].ToString();
                    MailMessage msg = new MailMessage();
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    msg.From = new System.Net.Mail.MailAddress(fromAddress, shortcode);
                    msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                    msg.Subject = subject;
                    msg.IsBodyHtml = true;
                    msg.Body = message;
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
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        private int GSuit(string useremail, string message, string subject, string ccemails, string bccemails, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                if (dsCred != null)
                {
                    string fromAddress = dsCred["EMAILID"].ToString();
                    string fromPassword = dsCred["PASSWORDS"].ToString();
                    string shortcode = dsCred["CODE_STANDARD"].ToString();
                    MailMessage msg = new MailMessage();
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    msg.From = new System.Net.Mail.MailAddress(fromAddress, shortcode);
                    msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                    if (ccemails != "")
                    {
                        msg.CC.Add(ccemails);
                    }
                    if (bccemails != "")
                    {
                        msg.Bcc.Add(ccemails);
                    }
                    msg.Subject = subject;
                    msg.IsBodyHtml = true;
                    msg.Body = message;
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

                ret = 0;
            }
            return ret;

        }
        private int OutLook(string Message, string toEmailId, string sub, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = dsCred["EMAILID"].ToString().ToString();
                oMail.To = toEmailId;
                oMail.Subject = sub;
                oMail.HtmlBody = Message;
                SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
                oServer.User = dsCred["EMAILID"].ToString().ToString();
                oServer.Password = dsCred["PASSWORDS"].ToString();
                oServer.Port = 587;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                Console.WriteLine("start to send email over TLS...");
                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);
                ret = 1;
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
                ret = 0;
            }
            return ret;
        }

        private int OutLook(string Message, string toEmailId, string sub, string ccemails, string bccemails, DataRow dsCred)
        {

            int ret = 0;
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = dsCred["EMAILID"].ToString().ToString();

                oMail.To = toEmailId;
                if (ccemails != "")
                {
                    oMail.Cc = ccemails;
                }
                if (bccemails != "")
                {
                    oMail.Bcc = bccemails;
                }

                oMail.Subject = sub;
                oMail.HtmlBody = Message;
                SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
                oServer.User = dsCred["EMAILID"].ToString().ToString();
                oServer.Password = dsCred["PASSWORDS"].ToString();
                oServer.Port = 587;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                Console.WriteLine("start to send email over TLS...");
                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);
                Console.WriteLine("email sent successfully!");
                ret = 1;
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
                ret = 0;
            }
            return ret;
        }
        private int Amazon(string useremail, string message, string subject, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                if (dsCred != null)
                {
                    string shortcode = dsCred["CODE_STANDARD"].ToString();
                    var smtpClient = new System.Net.Mail.SmtpClient(dsCred["SMTP_SERVER"].ToString(), Convert.ToInt32(dsCred["SMTP_PORT"].ToString()))
                    {

                        Credentials = new NetworkCredential(dsCred["CKEY_USERID"].ToString(), dsCred["PASSWORDS"].ToString()),
                        EnableSsl = true
                    };
                    var messageNew = new MailMessage
                    {
                        From = new System.Net.Mail.MailAddress(dsCred["EMAILID"].ToString(), shortcode),
                        Subject = subject,//"Test Email",
                        Body = message,//"This is the body of the email."
                        IsBodyHtml = true
                    };
                    messageNew.To.Add(useremail);

                    smtpClient.Send(messageNew);

                    if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                    {
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        //added by Swapnil Amazon method with Attachment on 21-07-2023
        // modified by Nikhil L. on Amazon method with Attachment on 04/09/2023
        private int Amazon(string useremail, string message, string subject, string ccemails, string bccemails, DataSet ds, string attachmentfilename, byte[] bytefile, string type, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                if (dsCred != null)
                {
                    string shortcode = dsCred["CODE_STANDARD"].ToString();

                    var smtpClient = new System.Net.Mail.SmtpClient(dsCred["SMTP_SERVER"].ToString(), Convert.ToInt32(dsCred["SMTP_PORT"].ToString()))

                    {
                        Credentials = new NetworkCredential(dsCred["CKEY_USERID"].ToString(), dsCred["PASSWORDS"].ToString()),
                        EnableSsl = true
                    };
                    var messageNew = new MailMessage
                    {
                        From = new System.Net.Mail.MailAddress(dsCred["EMAILID"].ToString(), shortcode),
                        Subject = subject,//"Test Email",
                        Body = message,//"This is the body of the email."
                        IsBodyHtml = true
                    };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;



                    if (type == "excel")
                    {
                        byte[] fileBytes = ConvertDataSetToExcel(ds);
                        MemoryStream stream = new MemoryStream(fileBytes);
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, attachmentfilename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        messageNew.Attachments.Add(attachment);
                    }
                    if (type == "pdf")
                    {
                        MemoryStream stream = new MemoryStream(bytefile);
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, attachmentfilename, "application/pdf");
                        messageNew.Attachments.Add(attachment);
                    }
                    else
                    {
                        MemoryStream stream = new MemoryStream(bytefile);
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, attachmentfilename, "application/octet-stream");
                        messageNew.Attachments.Add(attachment);
                    }

                    messageNew.To.Add(useremail);
                    if (ccemails != "")
                    {
                        messageNew.CC.Add(ccemails);
                    }
                    if (bccemails != "")
                    {
                        messageNew.Bcc.Add(bccemails);
                    }
                    smtpClient.Send(messageNew);

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
                //}
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        private int Amazon(string useremail, string message, string subject, string ccemails, string bccemails, DataRow dsCred)
        {

            int ret = 0;
            try
            {
                if (dsCred != null)
                {
                    string shortcode = dsCred["CODE_STANDARD"].ToString();

                    var smtpClient = new System.Net.Mail.SmtpClient(dsCred["SMTP_SERVER"].ToString(), Convert.ToInt32(dsCred["SMTP_PORT"].ToString()))

                    {
                        Credentials = new NetworkCredential(dsCred["CKEY_USERID"].ToString(), dsCred["PASSWORDS"].ToString()),
                        EnableSsl = true
                    };

                    var messageNew = new MailMessage
                    {
                        From = new System.Net.Mail.MailAddress(dsCred["EMAILID"].ToString(), shortcode),

                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

                    messageNew.To.Add(useremail);
                    if (ccemails != "")
                    {
                        messageNew.CC.Add(ccemails);
                    }
                    if (bccemails != "")
                    {
                        messageNew.Bcc.Add(bccemails);
                    }
                    smtpClient.Send(messageNew);

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

                ret = 0;
            }
            return ret;
        }


        private int OutLook(string Message, string toEmailId, string sub, string ccemails, string bccemails, string attachmentfilename, byte[] bytefile, string type, DataRow dsCred)
        {
            int ret = 0;
            try
            {
                DataSet dsconfig = null;
                Common objCommon = new Common();
                dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
                string ReffEmail = Convert.ToString(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString());
                string ReffPassword = Convert.ToString(dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString());
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage msg = new MailMessage();
                msg.To.Add(new System.Net.Mail.MailAddress(toEmailId));
                //Add CC and BCC Email Id 
                if (ccemails != "")
                {
                    msg.CC.Add(ccemails);
                    //oMail.Cc = ccemails;
                }
                if (bccemails != "")
                {
                    msg.Bcc.Add(bccemails);
                }
                msg.From = new System.Net.Mail.MailAddress(ReffEmail);
                msg.Subject = sub;
                StringBuilder sb = new StringBuilder();
                msg.Body = Message;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(new MemoryStream(bytefile), "" + attachmentfilename + ".pdf");
                msg.Attachments.Add(attachment);
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ReffEmail, ReffPassword);
                client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
                client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                try
                {
                    client.Send(msg);
                    //lblText.Text = "Message Sent Succesfully";
                }
                catch (Exception ex)
                {

                }
                ret = 1;
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
                ret = 0;
            }
            return ret;
        }




        public DataSet SendSMS_New(string pageNo, string mobileNo, string templateName, string templateId, string template)
        {
            try
            {
                string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Values = string.Empty;
                string providerName = string.Empty;
                DataSet dsCheck = new DataSet();
                SP_Name = "PKG_ACD_GET_SMS_TEMPLATE_AND_CREDENTIAL";
                SP_Parameters = "@P_AL_NO,@P_TEMPLATE_NAME,@P_TEMPLATE_ID";
                SP_Values = "" + Convert.ToInt32(pageNo) + "," + templateName + "," + templateId + "";
                dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (templateId != "" && template != "")
                    {
                        for (int i = 0; i < dsCheck.Tables[0].Rows.Count; i++)
                        {
                            providerName = dsCheck.Tables[0].Rows[i]["SERVICE_PROVIDER_NAME"].ToString();
                            DataRow dr = dsCheck.Tables[0].Rows[i];
                            if (providerName.ToString().Equals("SMSNMMS.CO.IN/SMS.ASPX".ToUpper()))
                            {
                                SendSMSNMMS(mobileNo, templateId, template, dr);
                            }
                            if (providerName.ToString().Equals("Text Local"))
                            {
                                SendTextLocal(mobileNo, templateId, template, dr);
                            }
                            if (providerName.ToString().Equals("SMSJUST"))
                            {
                                SendSMSJUST(mobileNo, templateId, template, dr);
                            }
                        }
                    }
                }
                return dsCheck;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //added by jay t. SMSJUST method with Attachment on 14-02-2024
        private void SendSMSJUST(string mobileNo, string templateId, string template, DataRow drCred)
        {
            try
            {
                string result = "";
                string Message = string.Empty;
                if (drCred != null)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + drCred["SMSPROVIDER"].ToString()));
                    request.ContentType = "text/xml; charset=utf-8";
                    request.Method = "POST";

                    string postDate = "username=" + drCred["SMSSVCID"].ToString();
                    postDate += "&";
                    postDate += "pass=" + drCred["SMSSVCPWD"].ToString();
                    postDate += "&";
                    postDate += "senderid=GPGNGP";
                    postDate += "&";
                    postDate += "message=" + template;
                    postDate += "&";
                    postDate += "dest_mobileno=91" + mobileNo;
                    postDate += "&";
                    postDate += "msgtype=TXT";
                    postDate += "&";
                    postDate += "response=Y";

                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                    request.ContentType = "application/x-www-form-urlencoded";

                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse _webresponse = request.GetResponse();
                    dataStream = _webresponse.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendSMSNMMS(string mobileNo, string templateId, string template, DataRow drCred)
        {
            try
            {
                string result = "";
                string Message = string.Empty;
                if (drCred != null)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + drCred["SMSPROVIDER"].ToString() + "?"));
                    request.ContentType = "text/xml; charset=utf-8";
                    request.Method = "POST";

                    string postDate = "ID=" + drCred["SMS_User_ID"].ToString();
                    postDate += "&";
                    postDate += "Pwd=" + drCred["PASSWORDS"].ToString();
                    postDate += "&";
                    postDate += "PhNo=91" + mobileNo;
                    postDate += "&";
                    postDate += "Text=" + template;
                    postDate += "&";
                    postDate += "TemplateID=" + templateId;

                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                    request.ContentType = "application/x-www-form-urlencoded";

                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse _webresponse = request.GetResponse();
                    dataStream = _webresponse.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
                }
                else
                {
                    result = "0";

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendTextLocal(string mobileNo, string templateId, string template, DataRow drCred)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            String result;
            string apiKey = drCred["SMS_API"].ToString();
            string numbers = mobileNo; // in a comma seperated list
            string message = template;
            string sender = "ERPSMS";

            //String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            String url = drCred["SMS_URL"].ToString() + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
        }



        public void SendWhatsApp_New(string mobileNo, string pageNo, string bodys, DataSet dsCheck)
        {

            try
            {
                string providerName = "";
                if (dsCheck.Tables[0].Rows.Count < 2)
                {
                    for (int i = 0; i < dsCheck.Tables[0].Rows.Count; i++)
                    {
                        providerName = dsCheck.Tables[0].Rows[i]["SERVICE_PROVIDER_NAME"].ToString();
                        if (providerName.Equals("Aisensy"))
                        {
                            SendAisensy_New(mobileNo, dsCheck, bodys);
                        }
                        if (providerName.Equals("Web WPSSMS"))
                        {
                            sendwpsms_New(mobileNo, dsCheck, bodys);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendAisensy_New(string mobileNo, DataSet drCred, string bodys)
        {
            try
            {
                int Mobile_le = mobileNo.Length;
                if (Mobile_le == 10)
                {
                    mobileNo = "91" + mobileNo.ToString();
                }
                string API_URL = drCred.Tables[0].Rows[0]["WHATSAAP_API_URL"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL.ToString());
                httpWebRequest.Method = "POST";                 //httpWebRequest.Headers.Add("aftership-api-key:********fdbfd93980b8c5***");
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var body = bodys;
                    //var bodys = @"{""apiKey"":" + '"' + API_KEY.ToString() + '"' + "," + "\n" +
                    //   @"""campaignName"":""erpattendance_rcpit""," + "\n" +
                    //   @"""destination"":" + '"' + mobileNo.ToString() + '"' + "," + "\n" +
                    //   @"""userName"":" + '"' + UserName.ToString() + '"' + "," + "\n" +
                    //   @"""templateParams"":[" + '"' + Name.ToString() + '"' + "," + '"' + att.ToString() + '"' + "," + '"' + course.ToString() + '"' + "," + '"' + Dept.ToString() + '"' + "]}";
                    streamWriter.Write(bodys);
                    streamWriter.Flush();
                    streamWriter.Close(); var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sendwpsms_New(string mobileNo, DataSet drCred, string bodys)
        {
            try
            {
                int Mobile_le = mobileNo.Length;
                if (Mobile_le == 10)
                {
                    mobileNo = "91" + mobileNo.ToString();
                }
                string API_URL = drCred.Tables[0].Rows[0]["WHATSAAP_API_URL"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL.ToString());
                httpWebRequest.Method = "POST";                 //httpWebRequest.Headers.Add("aftership-api-key:********fdbfd93980b8c5***");
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var body = bodys;
                    //var bodys = @"{""apiKey"":" + '"' + API_KEY.ToString() + '"' + "," + "\n" +
                    //   @"""campaignName"":""erpattendance_rcpit""," + "\n" +
                    //   @"""destination"":" + '"' + mobileNo.ToString() + '"' + "," + "\n" +
                    //   @"""userName"":" + '"' + UserName.ToString() + '"' + "," + "\n" +
                    //   @"""templateParams"":[" + '"' + Name.ToString() + '"' + "," + '"' + att.ToString() + '"' + "," + '"' + course.ToString() + '"' + "," + '"' + Dept.ToString() + '"' + "]}";
                    streamWriter.Write(bodys);
                    streamWriter.Flush();
                    streamWriter.Close();
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
