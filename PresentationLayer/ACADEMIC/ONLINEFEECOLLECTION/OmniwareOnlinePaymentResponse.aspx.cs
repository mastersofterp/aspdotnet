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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Security.Cryptography.X509Certificates;
using mastersofterp_MAKAUAT;
using System.Net.Security;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class ACADEMIC_ONLINEFEECOLLECTION_OmniwareOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu = new StudentController();
    SemesterRegistration objsem = new SemesterRegistration();
    OrganizationController objOrg = new OrganizationController();

    string hash_seq = string.Empty;
    int degreeno = 0;
    int college_id = 0;
    #endregion

    FeeCollectionController feeController = new FeeCollectionController();
    string UserFirstPaymentStatus = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                //SqlDataReader dr = objCommon.GetCommonDetails();

                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}
                DataSet Orgds = null;
                var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                Orgds = objOrg.GetOrganizationById(Convert.ToInt32(OrgId));
                Session["OrgId"] = OrgId;
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
                string order_id = string.Empty;
                string amount = string.Empty;
                string firstname = string.Empty;
                string emailId = string.Empty;
                string Idno = string.Empty;
                string recipt = string.Empty;
                string saltkey = string.Empty;
                int payid = 0;
                int orgid = 0;
                string hash_seq = string.Empty;
                firstname = Request.Form["name"];
                emailId = Request.Form["email"];
                order_id = Request.Form["order_id"];
                ViewState["order_id"] = order_id;
                amount = Request.Form["amount"];
                Idno = Request.Form["description"];
                orgid = Convert.ToInt32(Request.Form["udf1"]);
                payid = Convert.ToInt32(Request.Form["udf2"]);
                int payactivityno;
                int installmentno;
                //if (Request.Form["udf3"] == string.Empty)
                //{
                //    payactivityno = 0;
                //}
                //else
                //{
                payactivityno = Convert.ToInt32(Request.Form["udf3"]);
                //}
                //if (Request.Form["udf4"] == string.Empty)
                //{
                //installmentno = 0;
                //}
                //else
                //{
                installmentno = Convert.ToInt32(Request.Form["udf4"]);
                //}

                ViewState["IDNO"] = Idno;
                Session["IDNO"] = Idno;
                if (Session["OrgId"].ToString() == "16")
                {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }
                if (Session["OrgId"].ToString() == "16")
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(orgid, payid, payactivityno, degreeno, college_id);
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    hash_seq = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                }
                string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString());

                DataSet ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT S ON(S.IDNO = U.UA_IDNO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO = S.BRANCHNO)", "UA_NAME", "UA_NO,UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG,B.LONGNAME", "UA_IDNO=" + Convert.ToInt32(Idno), string.Empty);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    Session["userno"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    Session["branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                }

                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");
                string semester = objCommon.LookUp("ACD_DCR_TEMP", "SEMESTERNO", "IDNO=" + ViewState["IDNO"].ToString());
                lblRegNo.Text = Regno;
                lblstudentname.Text = firstname;
                lblOrderId.Text = order_id;
                lblamount.Text = amount;
                lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                lblBranch.Text = Convert.ToString(Session["branchname"]);
                lblSemester.Text = semester;
                lblTrasactionId.Text = Request.Form["transaction_id"]; ;
                //lblTrasactionId.Text = Request.Form["mihpayid"].ToString();
                string mihpayid = Request.Form["transaction_id"];
                ViewState["saltkey"] = objCommon.LookUp("ACD_PG_CONFIGURATION", "CHECKSUM_KEY", "");

                if (Request.Form["response_code"] == "0")
                {
                    string hash = string.Empty;
                    string[] keys = Request.Form.AllKeys;
                    Array.Sort(keys);
                    string hash_string = string.Empty;
                    hash_string = ViewState["saltkey"].ToString().Trim();
                    foreach (string hash_var in keys)
                    {
                        if (Request.Form[hash_var] != "" && hash_var != "hash")
                        {
                            hash_string = hash_string + '|';
                            hash_string = hash_string + Request.Form[hash_var];
                        }
                    }
                    hash = Generatehash512(hash_string).ToUpper();
                    if (hash != Request.Form["hash"])
                    {
                        Response.Write("Hash value did not matched");
                    }
                    else
                    {

                        if (Session["OrgId"].ToString() == "5")
                        {
                            string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                            string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");
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

                        divSuccess.Visible = true;
                        divFailure.Visible = false;
                        int result = 0;
                        string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                        objsem.IdNo = Convert.ToInt32(Session["idno"]);
                        objsem.SESSIONNO = Convert.ToInt32(Session["currentsession"].ToString());
                        objsem.SemesterNO = Convert.ToInt32(lblSemester.Text);
                        objsem.paymentMode = 1;
                        objsem.OfflineMode = 0;
                        objsem.Total_Amt = Convert.ToDecimal(lblamount.Text);

                        objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                        objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");
                        int output = 0;
                        if (Convert.ToInt32(installmentno) > 0)
                        {
                            output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");
                            if (output != 1)
                            {

                                if (ViewState["First_PaymentStatus"] == "5151")
                                {
                                    string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                    UPDATE_USER(UA_IDNO, 1);

                                    //Sendmail();

                                }
                                else
                                {

                                }
                            }
                        }
                        else
                        {

                            output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Regno, "-");

                            if (output != 1)
                            {
                                if (Session["OrgId"].ToString() == "5")
                                {
                                    if (ViewState["First_PaymentStatus"].Equals("5151"))
                                    {
                                        string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                        UPDATE_USER(UA_IDNO, 1);

                                        //Sendmail();

                                    }
                                    else
                                    {

                                    }
                                }
                            }

                        }
                        btnPrint.Visible = true;
                    }
                }
                else
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                    btnPrint.Visible = false;
                }



            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
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
    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        string formID = "PostForm";
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
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
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
    protected void UPDATE_USER(string UA_NO, int FirstTimePay)
    {
        try
        {
            string UA_PWD = string.Empty;
            string password = string.Empty;
            int IDNO = 0;
            string REGNO = string.Empty;
            string Email = string.Empty;
            string UA_ACC = string.Empty;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 5)
            {
                IDNO = Convert.ToInt32(Session["IDNO"]);
                REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = '" + Session["IDNO"] + "'");

                string Username = string.Empty;
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(REGNO.ToString());
            }
            else
            {
                IDNO = Convert.ToInt32(Session["IDNO"]);
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

        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + Session["IDNO"].ToString());

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));

        Username = REGNO;
        Password = REGNO;

        Session["Enrollno"] = srnno;
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
                message += "<p><b>" + Branchname + "</>b</p>";
                message += "<p>Your fees have been submitted successfully and you have been registered for the program mentioned above.Your new Login credentials are as follows</p><p>" + MISLink + " </p><p>Username   : " + Username + " <br/>Password: " + Password + "</p>";
                message += "<p>Note for Provisional Registration only:</p>";
                message += "<p>All the documents must be uploaded on URL: <b>" + MISLink + "</b>";
                message += "<p>Process of fee payment: Login using above credentials in <b>" + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.: ";
                message += "<p>The fee payment should be made within 7 days of receiving this mail/letter, after which your claim for admission may be requested.</p>";
                message += "<p style=font-weight:bold;>Thanks<br>Team Admissions<br>admissions@jecrcu.edu.in<br>JECRC University, Jaipur</p>";

                if (email_type == "1" && email_type != "")
                {
                    int reg = TransferToEmail(EmailID, message, subject);
                }
                else if (email_type == "2" && email_type != "")
                {
                    Task<int> task = Execute(message, EmailID, subject);
                    status = task.Result;
                }
                if (email_type == "3" && email_type != "")
                {
                    OutLook_Email(message, EmailID, subject);
                }
            }
        }

        if (status == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
        }



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
                msg.From = new System.Net.Mail.MailAddress(fromAddress, "RCPIPER");
                msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                msg.Subject = subject;
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
    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
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
                Console.WriteLine("Email Sent successfully!");

            }
            else
            {
                ret = 0;
                Console.WriteLine("Fail to send Mail!");
            }
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }
    private int OutLook_Email(string Message, string toEmailId, string sub)
    {

        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnredirect_Click(object sender, EventArgs e)
    {

    }
}