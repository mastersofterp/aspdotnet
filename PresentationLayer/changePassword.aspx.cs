//--------------------------------------------- -------------------------------------------------------------------------------
//PROJECT NAME  : UAIMS                                                           
//PAGE NAME     : TO CHANGE PASSWORD                                           
//CREATION DATE : 17-APRIL-2009                                                   
//CREATED BY    : SHEETAL RAUT                                                    
//-----------------------------------------------------------------------------------------------------------------------------
//Version   ModifiedOn      Modified By             Purpose
//-----------------------------------------------------------------------------------------------------------------------------
//1.0.1     03-04-2024      Kajal Jaiswal           Changes for sending SMS TGCPET
//--------------------------------------------- -------------------------------------------------------------------------------

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Text.RegularExpressions;
using mastersofterp_MAKAUAT;
using System.Net.Security;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Mastersoft.Security.IITMS;
using BusinessLogicLayer.BusinessLogic;
using System.Net;
using PageControlValidator;



public partial class changePassword : System.Web.UI.Page
{
    Common objCommon = new Common();
    User_AccController useracc = new User_AccController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

        this.MasterPageFile = "SiteMasterPage.master";
        if (Request.QueryString["IsReset"] == "1")
        {
            Session["IsReset"] = 1;
            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                int logid = useracc.AddtoLogTran(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), Convert.ToDateTime(DateTime.Now));
                Session["loginid"] = logid.ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    
                    lblHelp.Text = "In this Page, the user can change his existing password";
                }

                //First time login
                if (Request.QueryString["status"] != null)
                {
                    if (Request.QueryString["status"].ToString().Equals("firstlog"))
                    {
                       
                        objCommon.DisplayMessage(this.updPass, "You have logged in for the first time. Please Change your Password.", this);
                        //update the firstlog status
                        User_AccController objUA = new User_AccController();
                        objUA.UpdateFirstLogin(Session["username"].ToString());
                    }
                }
                if (Session["userno"] != null)
                {
                    DataSet uads = null;
                    User_AccController objUser = new User_AccController();
                    uads = objUser.CheckUserEmailMobile(Convert.ToInt32(Session["userno"]));
                    if (uads != null)
                    {
                        if (uads.Tables[0].Rows.Count > 0)
                        {
                            if (uads.Tables[0].Rows[0]["UA_EMAIL"] != null)
                            {
                                if (uads.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                                {
                                    txtEmailId.Text = uads.Tables[0].Rows[0]["UA_EMAIL"].ToString();
                                   
                                }
                            }
                            if (uads.Tables[0].Rows[0]["UA_MOBILE"] != null)
                            {
                                if (uads.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                                {
                                    txtMobile.Text = uads.Tables[0].Rows[0]["UA_MOBILE"].ToString();
                                    if (uads.Tables[0].Rows[0]["UA_MOBILE"].ToString().Length >= 10)
                                    {
                                        txtMobile.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/default.aspx");
                }
            }
        }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string DisplayMessage = string.Empty;


        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtOldPassword.Text, txtOldPassword.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtOldPassword.Focus();
            return;
        }

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtNewPassword.Text, txtNewPassword.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtNewPassword.Focus();
            return;
        }


        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtConfirmPassword.Text, txtConfirmPassword.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtConfirmPassword.Focus();
            return;
        }


        LogFile objLF = new LogFile();
        int mailsend = 0;
        int smssend = 0;
        try
        {
            string OldPassword = string.Empty;
            string NewPassword = string.Empty;
            string ConfirmPassword = string.Empty;

            if (SecurityThreads.ValidInput(txtNewPassword.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Password", Page);
                return;
            }

            if (SecurityThreads.CheckSecurityInput(txtNewPassword.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Password", Page);
                return;
            }

            if (SecurityThreads.ValidInput(txtConfirmPassword.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Confirm Password", Page);
                return;
            }

            if (SecurityThreads.CheckSecurityInput(txtConfirmPassword.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Confirm Password", Page);
                return;
            }

            if (txtEmailId.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.updPass, "Email Id Required", this.Page);
                return;
            }
            if (txtEmailId.Text.Trim() != null)
            {
                String mail = txtEmailId.Text.Trim();
                string expression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                Match match = Regex.Match(mail, expression, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                }
                else
                {
                    objCommon.DisplayMessage(this.updPass, "Enter Valid Email Id", this.Page);
                    return;
                }
            }
            if (txtMobile.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.updPass, "Mobile Number Required", this.Page);
                return;
            }
            if (txtMobile.Text.Trim() != null)
            {
                if (Convert.ToString(txtMobile.Text.Trim()).Length < 10)
                {
                    objCommon.DisplayMessage(this.updPass, "Enter Valid Mobile Number", this.Page);
                    return;
                }
            }
            if (txtMobile.Text.Trim() != null)
            {
                String mail = txtMobile.Text.Trim();
                string expression = @"^[0-9]+$";

                Match match = Regex.Match(mail, expression, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                }
                else
                {
                    objCommon.DisplayMessage(this.updPass, "Enter Valid Mobile Number", this.Page);
                    return;
                }
            }

            if (txtNewPassword.Text.Trim() != null && txtOldPassword.Text.Trim() != null)
                {
                    if (txtNewPassword.Text.Trim() == txtOldPassword.Text.Trim())
                    {
                        objCommon.DisplayMessage(this.updPass, "New password should not  be same as old password.", this.Page);
                        return;
                    }
                }

            if (txtNewPassword.Text.Trim() != null)
            {
                if (txtConfirmPassword.Text.Trim() != null)
                {
                    if (txtNewPassword.Text.Trim().ToString() != txtConfirmPassword.Text.Trim().ToString())
                    {
                        objCommon.DisplayMessage(this.updPass, "New password does not match with confirm password.", this.Page);
                        return;
                    }
                }
            }
            if (txtNewPassword.Text.Trim() == string.Empty || txtOldPassword.Text.Trim() == string.Empty || txtConfirmPassword.Text.Trim() == string.Empty)
            {
                
                objCommon.DisplayMessage(this.updPass, "Blank password is not allowed", this);
            }

            string useremail = txtEmailId.Text.Trim().Replace("'", "");
            string mailid = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_EMAIL='" + useremail + "' ");

           
            Regex pass = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{10,}$");
            if (!pass.IsMatch(txtConfirmPassword.Text))
            {
                objCommon.DisplayMessage(this, "Password should be at least 10 characters,Must contain at least one lower case letter, one upper case letter, one digit and one special character", this);             
                return;
            }
            else
            {
               
            }


            if (txtEmailId.Text.Trim() != "")
            {
                User_AccController objUC = new User_AccController();
                UserAcc objUA = new UserAcc();
                string UA_type = (objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NAME='" + Session["username"].ToString() + "'"));
                string UA_fullname = (objCommon.LookUp("USER_ACC", "CASE WHEN isnull(UA_FULLNAME COLLATE DATABASE_DEFAULT, '') <> ''  THEN UA_FULLNAME COLLATE DATABASE_DEFAULT ELSE UA_NAME COLLATE DATABASE_DEFAULT END", "UA_NAME='" + Session["username"].ToString() + "'"));
                objUA.UA_Name = Session["username"].ToString();
                objUA.UA_No = Convert.ToInt32(Session["userno"].ToString());
                objUA.UA_Pwd = txtNewPassword.Text.Trim();
                objUA.UA_OldPwd = txtOldPassword.Text.Trim();
                objUA.EMAIL = txtEmailId.Text.Trim();
                objUA.MOBILE = txtMobile.Text.Trim();
                objUA.IP_ADDRESS = Request.ServerVariables["REMOTE_HOST"];
                objUA.UA_Type = Convert.ToInt32(UA_type);

                DataSet ds = objUC.CheckPassword(objUA);
                
                CustomStatus cs1 = (CustomStatus)objUC.UpdateUserEmailMobile(objUA);

                if (cs1.Equals(CustomStatus.RecordUpdated))
                {

                    CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadminFirstLog_1(objUA);


                    if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    {
                        objCommon.DisplayMessage(this.updPass, "Invalid Old Password", this);
                    }
                    else
                    {
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            string subjectStandard = objCommon.LookUp("REFF", "SUBJECT_OTP", "");
                            string subject = subjectStandard + " ERP || Password Reset Successfully";
                           

                            CustomStatus cs2 = (CustomStatus)objUC.InsertChangePassword(objUA);

                            if (Convert.ToInt32(Session["usertype"]) == 2 && Convert.ToString(Session["firstlog"]) == "False")
                            {
                                string message = "Dear " + UA_fullname + ", Password is changed succesfully, Your new password is " + txtNewPassword.Text.ToString() + "";
                                if (txtEmailId.Text != string.Empty && txtNewPassword.Text != string.Empty)
                                {
                                    mailsend = objSendEmail.SendEmail(txtEmailId.Text, message, subject); //Calling Method
                                  
                                    smssend = SendSms(txtNewPassword.Text);
                                  if (mailsend == 1 && smssend == 1)
                                  {
                                      objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Email Id and Mobile Number.", this.Page);
                                  }
                                  else if (mailsend == 1 && smssend == 0)
                                  {
                                      objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Email Id.", this.Page);
                                  }
                                  else if (mailsend == 0 && smssend == 1)
                                  {
                                      objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Mobile Number.", this.Page);
                                  }
                                  else if (mailsend == 0 && smssend == 0)
                                  {
                                      objCommon.DisplayMessage(this.updPass, "Password changed successfully.", this.Page);
                                  }
                                }

                                txtOldPassword.Attributes.Add("value", OldPassword);
                                txtNewPassword.Attributes.Add("value", NewPassword);
                                txtConfirmPassword.Attributes.Add("value", ConfirmPassword);
                                txtEmailId.Text = string.Empty;
                                txtMobile.Text = string.Empty;
                                lblMessage.Visible = true;
                               
                                lnkback.Visible = true;


                            }
                            else
                            {
                                
                                string message = "Dear " + UA_fullname + ", Password is changed succesfully, Your new password is " + txtNewPassword.Text.ToString() + "";
                                if (txtEmailId.Text != string.Empty && txtNewPassword.Text != string.Empty)
                                {
                                     mailsend = objSendEmail.SendEmail(txtEmailId.Text, message, subject); //Calling Method

                                     smssend = SendSms(txtNewPassword.Text);

                                    if (mailsend == 1 && smssend == 1)
                                    {
                                        objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Email Id and Mobile Number.", this.Page);
                                    }
                                    else if (mailsend == 1 && smssend == 0)
                                    {
                                        objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Email Id.", this.Page);
                                    }
                                    else if (mailsend == 0 && smssend == 1)
                                    {
                                        objCommon.DisplayMessage(this.updPass, "Password changed successfully, New password has been sent on Mobile Number.", this.Page);
                                    }
                                    else if (mailsend == 0 && smssend == 0)
                                    {
                                        objCommon.DisplayMessage(this.updPass, "Password changed successfully.", this.Page);
                                    }
                                    
                                }
                               
                                txtOldPassword.Attributes.Add("value", OldPassword);
                                txtNewPassword.Attributes.Add("value", NewPassword);
                                txtConfirmPassword.Attributes.Add("value", ConfirmPassword);
                                txtEmailId.Text = string.Empty;
                                txtMobile.Text = string.Empty;
                                lnkback.Visible = true;
                                lblMessage.Visible = true;

                            }

                        }
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(this.updPass, "Email Id is Not Registered", this.Page);
                return;
            }


        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                lblMessage.Text = "Invalid Old Password";
            else
                lblMessage.Text = "Server UnAvailable";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["IsReset"]) == 1)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            if (Session["username"].ToString() == "superadmin")
            {
                Response.Redirect("~/RFC_CONFIG/home.aspx", false);
            }
            else if (Session["usertype"].ToString() == "1") // Dashboard should display through admin level login
            {
                Response.Redirect("~/principalHome.aspx", false);
            }
            else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14") // Dashboard should display through admin level login
            {
                Response.Redirect("~/studeHome.aspx", false);
            }
            else if (Session["usertype"].ToString() == "3")
            {
                Response.Redirect("~/homeFaculty.aspx", false);
            }
           
            else if (Session["usertype"].ToString() == "5")
            {
                
                Response.Redirect("~/homeNonFaculty.aspx", false);
            }
            else
            {
                Response.Redirect("~/home.aspx", false);
            }
        }
    }

    protected void lnkback_Click(object sender, EventArgs e)
    {
        try
        {
            LogFile objLF = new LogFile();
            objLF.Ua_Name = Session["username"].ToString();
            objLF.LogoutTime = DateTime.Now;
            objLF.ID = Convert.ToInt32(Session["loginid"].ToString());
            LogTableController.UpdateLog(objLF);
            Response.Redirect("~/default.aspx");
        }

        catch (Exception ex)
        {
            
        }
    }

    #region  sms

    public int SendSms(string newpassword)
    {
         int status = 0;
        try
        {
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            string templatename = "Change Password";
            DataSet ds = useracc.GetSMSTemplate(0, templatename);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
            }

            string message = TEMPLATE;
            StringBuilder stringBuilder = new StringBuilder();
            message = message.Replace("{#var#}", Session["username"].ToString());
            message = message.Replace("{#var1#}", newpassword);


            if (txtMobile.Text != string.Empty)
            {
                // Added By Kajal Jaiswal on 02-04-2024
                if (Session["OrgId"].ToString() == "21") // For TGPCET
                {
                    SendSMSJUST(txtMobile.Text.Trim(), message, TemplateID);

                }
                else
                {
                    SendSMS(txtMobile.Text.Trim(), message, TemplateID);
                }
            }
            status = 1;
        }
        catch
        {
             status = 0;
        }
        return status;
    }

    private void SendSMSJUST(string mobileNo, string template, string templateId)
    {
        try
        {
            string result = "";
            string Message = string.Empty;
            DataSet drCred = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (drCred != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + drCred.Tables[0].Rows[0]["SMSProvider"].ToString()));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "username=" + drCred.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "pass=" + drCred.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
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

    public void SendSMS(string Mobile, string text, string TemplateID)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;
                postDate += "&";
                postDate += "TemplateID=" + TemplateID;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }
        }
        catch
        {
            throw;
        }

    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }

    #endregion  sms
  
    #region Not_in_use

    //protected void btnEmailotp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int status = 0;
    //        string OldPassword = txtOldPassword.Text;
    //        string NewPassword = txtNewPassword.Text;
    //        string ConfirmPassword = txtConfirmPassword.Text;
    //        //divOtpEmail.Visible = true;
    //        //string otp = GenerateOTP(5);
    //        ViewState["EmailOTP"] = otp;
    //        string Message = "Your OTP for Reset Password Process is : " + otp;

    //        if (txtEmailId.Text != string.Empty)
    //        {
    //            try
    //            {
    //                //status = SendMailBYSendgrid(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
    //                //status = sendEmail(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
    //                Task<int> task = Execute(Message, txtEmailId.Text, "MAKAUT ERP || OTP for reset password");
    //                status = task.Result;
    //                if (status == 1)
    //                {
    //                    objCommon.DisplayMessage(this.updPass, "OTP has been sent to Your Email Id, Enter To Continue Reset Password Process.", this);
    //                    //Showmessage("OTP has been send on Your Email Id, Enter To Continue Reset Password Process.");

    //                    txtOldPassword.Attributes.Add("value", OldPassword);

    //                    txtNewPassword.Attributes.Add("value", NewPassword);

    //                    txtConfirmPassword.Attributes.Add("value", ConfirmPassword);
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updPass, "Failed to send email", this);
    //                    //Showmessage("Failed to send email");
    //                }
    //            }
    //            catch (Exception)
    //            {
    //                objCommon.DisplayMessage(this.updPass, "Failed to send email", this);
    //                //Showmessage("Failed to send email");
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
    //        return;
    //    }
    //}

    //private string GenerateOTP(int length)
    //{
    //    //It will generate string with combination of small,capital letters and numbers
    //    char[] charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    //    string randomString = string.Empty;
    //    Random objRandom = new Random();
    //    for (int i = 0; i < length; i++)
    //    {
    //        //Don't Allow Repetation of Characters
    //        int x = objRandom.Next(1, charArr.Length);
    //        if (!randomString.Contains(charArr.GetValue(x).ToString()))
    //            randomString += charArr.GetValue(x);
    //        else
    //            i--;
    //    }

    //    return randomString;
    //}

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

    //protected void btnEmailOtpVerify_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string OldPassword = txtOldPassword.Text;
    //        string NewPassword = txtNewPassword.Text;
    //        string ConfirmPassword = txtConfirmPassword.Text;

    //       // if (ViewState["EmailOTP"].ToString().Trim() != txtOtpEmail.Text.Trim())
    //        {
    //            objCommon.DisplayMessage(this.updPass, "Please Enter Valid OTP", this);
    //            //Showmessage("Please Enter Valid OTP");
    //            txtOldPassword.Attributes.Add("value", OldPassword);
    //            txtNewPassword.Attributes.Add("value", NewPassword);
    //            txtConfirmPassword.Attributes.Add("value", ConfirmPassword);

    //            return;
    //        }
    //       // else
    //        {
    //            objCommon.DisplayMessage(this.updPass, "OTP is verified successfully", this);
    //            //Showmessage("OTP is verified successfully");
    //            txtEmailId.Enabled = false;

    //            //if (txtmobile.Enabled == false && txtemailid.Enabled == false)
    //            //{
    //            //    ChkAgree.Visible = true;
    //            //}
    //            //ViewState["EmailOTP"] = string.Empty;
    //           // txtOtpEmail.Text = string.Empty;
    //            //divOtpEmail.Visible = false;
    //            txtOldPassword.Attributes.Add("value", OldPassword);
    //            txtNewPassword.Attributes.Add("value", NewPassword);
    //            txtConfirmPassword.Attributes.Add("value", ConfirmPassword);

    //            return;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
    //        return;
    //    }

    //}


    //public int sendEmail(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
    //        // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

    //        var smtp = new SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = sub,
    //            Body = Message,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true
    //        })
    //        {
    //            //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //            return ret = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //Response.Write(ex.Message);
    //        ret = 0;
    //    }
    //    return ret;
    //}

    #endregion Not_in_use

}
