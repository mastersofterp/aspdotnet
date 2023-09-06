//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   :                                                                 
// PAGE NAME     : TO RESET PASSWORD                                  
// CREATION DATE : 02-06-2018                                                   
// CREATED BY    : PRASHANT WANKAR                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
//using mastersofterp;
using IITMS;
//using IITMS.NITPRM;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Linq;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using mastersofterp_MAKAUAT;
using SendGrid;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using SendGrid.Helpers.Mail;
using BusinessLogicLayer.BusinessLogic;

public partial class Agreement : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    private static string isServer = System.Configuration.ConfigurationManager.AppSettings["isServer"].ToString();
    string defaultPage = "";
    string defPage = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (isServer == "true")
        {
           defaultPage="~/default.aspx";
           defPage = "default.aspx";
        }
        else
        {
            defaultPage = "~/default_crescent.aspx";
            defPage = "default_crescent.aspx";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect(defaultPage);
            }
            else
            {
                Clear();
                ShowUserDetails();
                Showterms();
                divReset.Visible = false;
                Panel1_ModalPopupExtender.Show();
            }
            ViewState["valid"] = "invalid";
            Img1.Src = "~/showimage.aspx?id=0&type=college"; //Added By Rishabh B. ON 29/12/2021
        }
    }

    protected void btnCondition_Click(object sender, EventArgs e)
    {
        try
        {
            Panel1_ModalPopupExtender.Show();
            //Panel2_ModalPopupExtender.Show();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkAgree.Checked == false)
            {
                Showmessage("Please check terms and conditions");
                Panel1_ModalPopupExtender.Show();

                return;
            }
            else
            {
                divReset.Visible = true;
            }


        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void Showterms()
    {
        try
        {
            StudentController objECC = new StudentController();

            DataTableReader dtr = objECC.TermsCondtionDetails(Convert.ToInt32(1));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblterms.Text = dtr["BODY"].ToString();

                }
                dtr.Close();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowEmpDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowUserDetails()
    {
        try
        {
            StudentController objECC = new StudentController();

            DataTableReader dtr = objECC.UserDetails(Convert.ToInt32(Session["userno"]));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtemailid.Text = dtr["UA_EMAIL"].ToString();
                    txtmobile.Text = dtr["UA_MOBILE"].ToString();

                }
                dtr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowEmpDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Clear()
    {
        oldpass.Text = string.Empty;
        password.Text = string.Empty;
        cpwd.Text = string.Empty;

    }


    private static string EncryptPasswordnew(string password)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(password);
        bool useHashing = true;

        string key = "MA$TER$OFTERP";

        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    protected void btnCloseAggre_Click(object sender, EventArgs e)
    {
        Response.Redirect(defaultPage);
    }

    protected void ResetPassword_Click(object sender, EventArgs e)
    {
        User_AccController objUC = new User_AccController();
        UserAcc objUA = new UserAcc();
        Common objC = new Common();
        try
        {
            long res = 0;
            // string Old_pwd_reset = clsTripleLvlEncyrpt.ThreeLevelEncrypt(oldpass.Text.Trim());

            if (oldpass.Text == string.Empty)
            {
                Showmessage("Please enter old Password");
                return;
            }

            int Ua_no = Convert.ToInt32(Session["userno"]);
            //string pwd = objC.GetUserPassword(Ua_no);

            string pass = clsTripleLvlEncyrpt.ThreeLevelDecrypt(objC.GetUserPassword(Ua_no));

            if (pass.ToString() != oldpass.Text.Trim())
            {
                Showmessage("Please Enter Valid Old Password");
                return;
            }
            if (oldpass.Text.Trim() == password.Text.Trim())
            {
                Showmessage("New password & old password is same. Please change new password");
                return;
            }
            else if (password.Text == string.Empty)
            {
                Showmessage("Please enter new Password");
                return;
            }
            else if (cpwd.Text == string.Empty)
            {
                Showmessage("Please enter confirm Password");
                return;
            }
            if (txtemailid.Enabled != false)//if (txtmobile.Enabled != false && txtemailid.Enabled != false)
            {
                Showmessage("Please verify Email Id");
                return;
            }
            if (chkAgree.Checked == false)
            {
                Showmessage("Please Check terms and conditions");
                return;
            }
            if (txtOtpEmail.Text != string.Empty && txtOtpEmail.Enabled == true)
            {
                Showmessage("Please submit Email OTP.");
                return;
            }

            if (ViewState["valid"].ToString() == "invalid")
            {
                Showmessage("Please verify Email Id");
                return;

            }
            string new_pwd = string.Empty;
            string Old_pwd = string.Empty;
            //old Password
            //  Old_pwd = Common.EncryptPassword(oldpass.Text.Trim());
            //Old_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(Old_pwd);
            //New Password
            // new_pwd = Common.EncryptPassword(password.Text.Trim());
            // new_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(new_pwd);

            string oldpassword = objCommon.LookUp("USER_ACC", "UA_PWD", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " AND UA_TYPE=" + Convert.ToInt32(Session["usertype"]));
            // string olddecrypt = Common.DecryptPassword(oldpassword);
            Old_pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(oldpass.Text.Trim());
            string olddecrypt = clsTripleLvlEncyrpt.ThreeLevelDecrypt(oldpassword);
            //Common.DecryptPassword(oldpassword);
            //Old_pwd = Common.DecryptPassword(oldpass.Text.Trim());
            Old_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(Old_pwd);
            new_pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(password.Text.Trim());

            //To check password history
            string UA_type = (objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NAME='" + Session["username"].ToString() + "'"));
            objUA.UA_Name = Session["username"].ToString();
            objUA.UA_No = Convert.ToInt32(Session["userno"].ToString());
            objUA.UA_Pwd = password.Text.Trim();
            objUA.UA_Type = Convert.ToInt32(UA_type);
            objUA.IP_ADDRESS = Request.ServerVariables["REMOTE_HOST"];
            objUA.MOBILE = txtmobile.Text.Trim();
            objUA.MAC_ADDRESS = Session["macAddress"].ToString();
            objUA.EMAIL = txtemailid.Text.Trim();
            objUA.UA_OldPwd = oldpassword;

            DataSet ds = objUC.CheckPassword(objUA);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable Data = ds.Tables[0];
                //foreach (DataRow dr in Data.Rows)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string db_pwd = ds.Tables[0].Rows[i]["UA_PWD"].ToString();
                    db_pwd = clsTripleLvlEncyrpt.RSADecryption(db_pwd.ToString());
                    string user_pwd = clsTripleLvlEncyrpt.EncryptPassword(objUA.UA_Pwd);       // Encrypt withMasterSoft Logic
                    user_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(user_pwd);
                    if (db_pwd == user_pwd)
                    {
                        Showmessage("You can not enter last five passwords.");
                        cpwd.Text = string.Empty;
                        password.Text = string.Empty;
                        return;
                    }
                }

            }

            //res = objCommon.UpdateUserinfo(Old_pwd, new_pwd, txtemailid.Text.Trim(), Convert.ToInt32(Session["userno"].ToString()), txtmobile.Text.Trim(), Session["ipAddress"].ToString(), Session["macAddress"].ToString());
            res = objCommon.UpdateUserinfo(objUA);
            if (res == -99)
            {
                Showmessage("Error Occured");
                return;
            }
            if (res == 0)
            {
                return;
            }
            if (res > 0)
            {
                CustomStatus cs2 = (CustomStatus)objUC.InsertChangePassword(objUA);
                int Status = 0;
                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " AND UA_TYPE=" + Convert.ToInt32(Session["usertype"]));
                //string message = "Hello " + Session["username"] + ", Password is changed succesfully, Your new password is " + password.Text.ToString() + "";

                //added by tejas jaiswal
                DataSet dsconfig = null;
                dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
                string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your Password is changed successfully for Username - " + (Session["username"]).ToString() + "." + " Your New Password is " + "" + password.Text.ToString() + "" + "</b>";
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team " + SUBJECT_OTP + "<br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";
                if (txtemailid.Text != string.Empty && password.Text != string.Empty)
                {
                    //added by tejas jaiswal

                    if (Convert.ToInt32(SENDGRID_STATUS) == 1)
                    {
                        //status = SendMailBYSendgrid(Message, txtemailid.Text, "SBU ERP || OTP for reset password");
                        //status = sendEmail(Message, txtemailid.Text, "SBU ERP || OTP for reset password");
                        //Task<int> task = Execute(message, txtemailid.Text, SUBJECT_OTP + " || OTP for reset password");
                        //Status = task.Result;
                         //added by rohit m on 04-06-2023
                       // Status = TransferToEmailAmazon(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password");

                        Status = objSendEmail.SendEmail(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password"); //Calling Method
                    }
                    else
                    {
                         //added by rohit m on 04-06-2023
                        // Status = TransferToEmailAmazon(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password");
                         Status = objSendEmail.SendEmail(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password"); //Calling Method
                        //Status = sendEmail(message, txtemailid.Text, SUBJECT_OTP + " || OTP for reset password");
                    }
                }

                if (txtemailid.Text != string.Empty) //(txtmobile.Text != string.Empty || txtemailid.Text != string.Empty) //commented on date 14/042019
                {
                    Clear();
                    Session.Abandon();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Password is changed successfully, Please check your Email Inbox for new password.');window.location ='" + defPage + "';", true);
                }
            }

        }
        catch (Exception)
        {
            return;
        }
    }


    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    public void SendSMS(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                //url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;


                //WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                //string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;
                //Session["result"] = 1;
            }
        }
        catch (Exception)
        {
        }
    }

    //public bool SendMailBYSendgrid(string message, string emailid, string subject)
    //{
    //    bool ret = false;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
    //        string fromAddress = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
    //        string user = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();
    //        string pwd = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
    //        string decrFromPwd = Common.DecryptPassword(pwd);
    //        //string decrFromPwd = pwd;
    //        var myMessage = new SendGridMessage();
    //        //If want to send attachment in email
    //        //if (data.attachment != null)
    //        //{
    //        //    MemoryStream stream = new MemoryStream(data.attachment);
    //        //    myMessage.AddAttachment(stream, data.fileName);
    //        //}
    //        myMessage.From = new MailAddress(fromAddress);
    //        myMessage.AddTo(emailid);
    //        myMessage.Subject = subject;
    //        myMessage.Html = message;


    //        var credentials = new NetworkCredential(user, decrFromPwd);
    //        var transportWeb = new Web(credentials);
    //        transportWeb.Deliver(myMessage);
    //        return ret = true;
    //    }
    //    catch (Exception)
    //    {
    //        ret = false;
    //    }
    //    //return transportWeb.DeliverAsync(myMessage);
    //    return ret;
    //}

    public void sendmail(string toEmailId, string Sub, string body)
    {

        try
        {

            var fromAddress = new MailAddress(Session["EMAILSVCID"].ToString(), "");
            var toAddress = new MailAddress(toEmailId, "");
            string fromPassword = Common.DecryptPassword(Session["EMAILSVCPWD"].ToString());

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Sub,
                Body = body,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    protected void btnotp_Click(object sender, EventArgs e)
    {
        try
        {
            /*
             Session["SMSSVCID"]
             Session["SMSSVCPWD"]
             Session["SMSURL"] 
             Session["EMAILSVCID"] 
             Session["EMAILSVCPWD"] */

            // submit.Visible = false;
            divMobOtp.Visible = true;
            string otp = GenerateOTP(5);
            ViewState["OTP"] = otp;
            //string Url = Session["SMSProvider"].ToString();//"http://smsnmms.co.in/sms.aspx";
            //string UserId = Session["SMSSVCID"].ToString(); //"hr@iitms.co.in";
            //// string Password = Common.DecryptPassword(Session["SMSSVCPWD"].ToString());//"iitmsTEST@5448";
            //string Password = (Session["SMSSVCPWD"].ToString());
            //string MobileNo = "91" + txtmobile.Text.Trim();

            string messageTemplate = "Your OTP is " + otp + " to reset your password . OTP is valid for 15 Minutes or 1 Successfull Attempt MSERP";

            if (txtmobile.Text.Trim() != string.Empty)
            {
                SendSMS(txtmobile.Text.Trim(), messageTemplate, "1007696446809661273");

                //SendSMS(Url, UserId, Password, MobileNo, message);
            }

            Showmessage("OTP has been send on Your Mobile No,Enter To Continue Reset Password Process.");
        }
        catch (Exception)
        {
            return;
        }

    }
    protected void btnEmailotp_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            divOtpEmail.Visible = true;
            string otp = GenerateOTP(5);
            ViewState["EmailOTP"] = otp;
            // string Message = "Your OTP for Reset Password Process is : " + otp;


            //added by tejas jaiswal
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
            //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
            string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
            string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

            string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " AND UA_TYPE=" + Convert.ToInt32(Session["usertype"]));
            string message = "<b>Dear " + StudName + "," + "</b><br />";
            message += "Your One Time Password (OTP) for Reset password is " + otp;
            message += "<br /><br /><br />Thank You<br />";
            message += "<br />Team " + SUBJECT_OTP + "<br />";
            message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

            if (txtemailid.Text != string.Empty)
            {
                try
                {
                    //added by tejas jaiswal

                    if (Convert.ToInt32(SENDGRID_STATUS) == 1)
                    {
                        //status = SendMailBYSendgrid(Message, txtemailid.Text, "SBU ERP || OTP for reset password");
                        //status = sendEmail(Message, txtemailid.Text, "SBU ERP || OTP for reset password");
                        //Task<int> task = Execute(message, txtemailid.Text, SUBJECT_OTP + " || OTP for reset password");
                        //status = task.Result;
                        //added by rohit m on 04-06-2023
                         //status = TransferToEmailAmazon(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password");

                    status = objSendEmail.SendEmail(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password"); //Calling Method
                    }
                    else
                    {
                        //added by rohit m on 04-06-2023
                       status = objSendEmail.SendEmail(txtemailid.Text, message, SUBJECT_OTP + " || OTP for reset password"); //Calling Method
                       // status = sendEmail(message, txtemailid.Text, SUBJECT_OTP + " || OTP for reset password");
                    }

                    if (status == 1)
                    {
                        Showmessage("OTP has been send on Your Email Id, Enter To Continue Reset Password Process.");
                    }
                    else
                    {
                        Showmessage("Failed to send email");
                    }
                }
                catch (Exception)
                {
                    Showmessage("Failed to send email");
                }
            }
        }
        catch (Exception)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }
    }


    private string GenerateOTP(int length)
    {
        //It will generate string with combination of small,capital letters and numbers
        char[] charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string randomString = string.Empty;
        Random objRandom = new Random();
        for (int i = 0; i < length; i++)
        {
            //Don't Allow Repetation of Characters
            int x = objRandom.Next(1, charArr.Length);
            if (!randomString.Contains(charArr.GetValue(x).ToString()))
                randomString += charArr.GetValue(x);
            else
                i--;
        }

        return randomString;
    }
    protected void btnMobOtpVerify_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["valid"] = "invalid";
            if (ViewState["OTP"].ToString().Trim() != txtMobOtp.Text.Trim())
            {
                Showmessage("Please Enter Valid OTP");
                ViewState["valid"] = "invalid";
                return;
            }
            else
            {
                Showmessage("OTP is verified successfully");
                txtmobile.Enabled = false;
                //if (txtmobile.Enabled == false && txtemailid.Enabled == false)
                //{
                //    ChkAgree.Visible = true;
                //}
                ViewState["OTP"] = string.Empty;
                txtMobOtp.Text = string.Empty;
                divMobOtp.Visible = false;
                return;
            }
        }
        catch (Exception)
        {

            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }
    }

    protected void btnEmailOtpVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["EmailOTP"].ToString().Trim() != txtOtpEmail.Text.Trim())
            {
                Showmessage("Please Enter Valid OTP");
                return;
            }
            else
            {
                Showmessage("OTP is verified successfully");
                txtemailid.Enabled = false;
                //if (txtmobile.Enabled == false && txtemailid.Enabled == false)
                //{
                //    ChkAgree.Visible = true;
                //}
                ViewState["EmailOTP"] = string.Empty;
                txtOtpEmail.Text = string.Empty;
                divOtpEmail.Visible = false;
                ViewState["valid"] = "valid";
                return;
            }
        }
        catch (Exception)
        {

            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Panel1_ModalPopupExtender.Hide();

            oldpass.Text = string.Empty;
            password.Text = string.Empty;
            cpwd.Text = string.Empty;
            // txtmobile.Text = string.Empty;
            // txtmobile.Enabled = true;
            txtemailid.Text = string.Empty;
            txtemailid.Enabled = true;
            divMobOtp.Visible = false;
            divOtpEmail.Visible = false;
            txtMobOtp.Text = string.Empty;
            txtOtpEmail.Text = string.Empty;
            ShowUserDetails();
        }
        catch (Exception)
        {

            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }

    }

    //protected void ChkAgree_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ChkAgree.Checked == true)
    //        {
    //            submit.Visible = true;
    //        }
    //        else
    //        {
    //            submit.Visible = false;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
    //        return;
    //    }
    //}

    public int sendEmail(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName,SUBJECT_OTP", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");
            // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = sub,
                Body = Message,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
            {
                //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                ServicePointManager.ServerCertificateValidationCallback =
               delegate(object s, X509Certificate certificate,
               X509Chain chain, SslPolicyErrors sslPolicyErrors)
               { return true; };
                smtp.Send(message);
                return ret = 1;
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            ret = 0;
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
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");

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

    public int TransferToEmailAmazon(string useremail, string message, string subject)
        {
        int ret = 0;
        try
            {
            var smtpClient = new System.Net.Mail.SmtpClient("email-smtp.ap-south-1.amazonaws.com", 587)
            {
                Credentials = new NetworkCredential("AKIAUVZ5FSTMFA3CG74W", "BLYE5zzrcQkbKZEqICN3S+lhS3EdwBLl9Sl8n3EUbHEU"),
                EnableSsl = true
            };

            var messageNew = new MailMessage
            {
                From = new System.Net.Mail.MailAddress("no-reply@iitms.co.in"),
                Subject = subject,//"Test Email",
                Body = message,//"This is the body of the email."
                IsBodyHtml = true
            };

            //messageNew.To.Add("yograj.chaple@mastersofterp.co.in");
            messageNew.To.Add(useremail);

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
        catch (Exception ex)
            {

            ret = 0;
            }
        return ret;

        }

}