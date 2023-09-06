using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net.NetworkInformation;
using System.Data;
//using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using SendGrid;
using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//using System.Web.Mail;
//using EASendMail;
//using ASPNETChatControl;
public partial class altas_login : System.Web.UI.Page
{
    /// <summary>
    /// Google Sign In
    /// </summary>
    public class Packet
    {
        public string userno { get; set; }
        public string firstlog { get; set; }
        public string lastlogout { get; set; }
        public string Allowpopup { get; set; }

    }

    //Common Class
    Common objCommon = new Common();
    public string sMarquee = string.Empty;
    public string Notice = string.Empty;
    string a = string.Empty;
    string ipAddress = string.Empty;
    int b = 0;
    string emailid = null;
    bool connection;
    string macAddress = null; //deepali

    protected void Page_Load(object sender, EventArgs e)
    {
        //college name
        string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);

        if (!Page.IsPostBack)
        {
            //Timer1.Enabled = true;
            //Set Session Timeout
            Session.RemoveAll();
            try
            {
                CheckExpiryDate();
                UpdateCaptchaText();
                //Show scrolling news
                NewsController objNC = new NewsController();
                sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
                Notice = objNC.NoticeBoard(Request.ApplicationPath);
                //Get Common Details
                SqlDataReader dr = objCommon.GetCommonDetails();
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        //Get the Error Status
                        if (Convert.ToInt32(dr["Errors"]) > 0)
                            Session["error"] = true;
                        else
                            Session["error"] = false;
                        Session["coll_name"] = dr["CollegeName"].ToString();
                        Session.Timeout = 120;
                        Page.Title = dr["CollegeName"].ToString();
                    }
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    lblStatus.Text = ex.ToString();
                else
                    lblStatus.Text = "Server Unavailable";
            }

        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Page_Load(sender, e);
    }



    private void CheckExpiryDate()
    {
        try
        {
            NewsController objNC = new NewsController();
            CustomStatus cs = (CustomStatus)objNC.UpdateByDate(DateTime.Now);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                lblStatus.Text = "default.CheckExpiryDate -> " + ex.Message + " " + ex.StackTrace;
            else
                lblStatus.Text = "Server UnAvailable";
        }
    }

    protected void lnkbtnTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TP_Reg_form.aspx");
    }

    public string GetMACAddress()
    {
        String st = String.Empty;
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            OperationalStatus ot = nic.OperationalStatus;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                st = nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return st;
    }

    protected void lnkRegCompany_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/CompanyRegistration.aspx");
        }
        catch { }
    }

    protected void lbtForgePass_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Forgot_Password.aspx");
        txtName.Text = string.Empty;
        txt_emailid.Text = string.Empty;
        txt_captcha.Text = string.Empty;
    }

    private void UpdateCaptchaText()
    {
        txt_captcha.Text = string.Empty;
        lblStatus.Visible = false;
        //Store the captcha text in session to validate
        Session["Captcha"] = Guid.NewGuid().ToString().Substring(0, 6);
    }
    // Login Button
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        txtcaptcha.Text = txtcaptcha.Text.ToUpper();
        Captcha1.ValidateCaptcha(txtcaptcha.Text.Trim());
        string defaultcaptcha = System.Configuration.ConfigurationManager.AppSettings["DefaultCaptcha"].ToString(); //Added by Kajal Jaiswal on 31-03-2023 For Default Cptcha.

        if (txt_username.Text.Trim() != "" && txt_password.Text.Trim() != "")
        {
            if (Session["Captcha"] != null)
            {
                if (txtcaptcha.Text.ToUpper() == txtcaptcha.Text)
                {

                    //Match captcha text entered by user and the one stored in session
                    if (Captcha1.UserValidated || txtcaptcha.Text == defaultcaptcha) //Added condition for Default Cptcha on 31-03-2023
                    {
                        try
                        {
                            User_AccController objUC = new User_AccController();
                            string macAddress = string.Empty;
                            string username = txt_username.Text.Trim().Replace("'", "");
                            //string password = Common.EncryptPassword(txt_password.Text.Trim().Replace("'", ""));
                            string password = txt_password.Text.Trim().Replace("'", "");
                            int userno = -1;
                            int ATTEMPT = 0;
                            string lastlogout = string.Empty;
                            emailid = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_EMAIL", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");
                            string ua_status = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_STATUS", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");

                            ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));
                            //objCommon.DisplayMessage(updLog,"Login Failed !, Please Check Your Username Or Password !", this.Page);

                            int UANO = Convert.ToInt16(objCommon.LookUp("USER_ACC WITH (NOLOCK)", "ISNULL(UA_NO,0)", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL"));
                            if (UANO != 0)
                            {

                                #region 90-Days
                                DateTime ChangePassDate = Convert.ToDateTime(objCommon.LookUp("USER_ACC WITH (NOLOCK)", "isnull(CHANGEPASSDATE,0)", "ua_name=" + "'" + username + "'"));
                                int ua_type = Convert.ToInt32(objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_TYPE", "ua_name=" + "'" + username + "'"));
                                DateTime TodayDate = DateTime.Now;
                                int Difference = (TodayDate - ChangePassDate).Days;
                                int MAILINDAYS = Convert.ToInt32(objCommon.LookUp("User_Rights WITH (NOLOCK)", "isnull(MAILINDAYS,0)", "USERTYPEID=" + ua_type));
                                int FIRSTLOGDAYS = Convert.ToInt32(objCommon.LookUp("User_Rights with (nolock)", "isnull(FIRSTLOGDAYS,0)", "USERTYPEID=" + ua_type));
                                string UA_FIRSTLOG = (objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FIRSTLOG", "ua_name=" + "'" + username + "'"));
                                // Session["firstlog"] = UA_FIRSTLOG;
                                if (FIRSTLOGDAYS <= Difference)
                                {
                                    //int status = objUC.SetFirstLog(username);
                                    //  return;
                                }
                                #endregion 90-Days

                                if (objUC.ValidateLogin(username, password, out userno) == Convert.ToInt32((CustomStatus.ValidUser)))
                                {
                                    if (userno > 0)
                                    {

                                       // Timer1.Enabled = false; //Added by Mahesh Due to Refresh Captcha after every 90 Sec.

                                        //Login Succeded
                                        UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

                                        if (objUA.UA_No != 0)
                                        {

                                            Session["OrgId"] = objUA.OrganizationId;// Added By SUnita P - 09122021
                                            Session["userno"] = objUA.UA_No.ToString();
                                            Session["idno"] = objUA.UA_IDNo.ToString();
                                            Session["username"] = objUA.UA_Name;
                                            Session["usertype"] = objUA.UA_Type;
                                            Session["userfullname"] = objUA.UA_FullName;
                                            Session["dec"] = objUA.UA_Dec.ToString();
                                            Session["userdeptno"] = objUA.UA_DeptNo.ToString();
                                            Session["colcode"] = objCommon.LookUp("reff", "college_code", string.Empty);
                                            Session["firstlog"] = objUA.UA_FirstLogin;
                                            //Session["currentsession"] = "64";
                                            //Session["sessionname"] = "2013-14 II REG";
                                            Session["ua_status"] = objUA.UA_Status;
                                            Session["ua_section"] = objUA.UA_section.ToString();
                                            Session["UA_DESIG"] = objUA.UA_Desig.ToString();
                                            Session["userEmpDeptno"] = objUA.UA_EmpDeptNo.ToString();
                                            ipAddress = Request.ServerVariables["REMOTE_HOST"];
                                            Session["ipAddress"] = ipAddress;
                                            macAddress = GetMACAddress();
                                            Session["macAddress"] = macAddress;
                                            Session["payment"] = "default";
                                            //Session["Password"] = password.ToString(); //added by deepali for 90-days
                                            if (Convert.ToString(Session["firstlog"]) == "False")
                                            {
                                                //  Response.Redirect("~/changePassword.aspx?IsReset=1");
                                                Response.Redirect("~/changePassword.aspx?IsReset=1");
                                                //ADDED on date 18/07/2018 
                                                //Response.Redirect("~/agreement.aspx");  //related to 90-Days
                                            }
                                            ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));
                                            //objCommon.DisplayMessage(updLog,"Login Failed !, Please Check Your Username Or Password !", this.Page);
                                            if (ua_status == "1")
                                            {
                                                string subject = "ERP Login Credentials";
                                                string message = "Due to the unsucessfully  " + ATTEMPT + " login attempt ,your ERP account is blocked. Please contact system administrator!";
                                                if (emailid != "")
                                                {
                                                    objCommon.sendEmail(message, emailid, subject);
                                                }
                                                //*  objCommon.DisplayMessage(updLog,"Your Account is Blocked.Please Contact system Administrator!", this.Page);
                                                objCommon.DisplayMessage("Your Account is Blocked.Please Contact system Administrator!", this.Page);
                                                lblStatus.Text = "Your Account is Blocked.Please Contact system Administrator!";
                                                return;
                                            }

                                            string lastloginid = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
                                            //FOR STORE MODULE
                                            Session["lastloginid"] = lastloginid.ToString();
                                            if (Session["lastloginid"].ToString() != string.Empty)
                                            {
                                                lastlogout = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
                                            }
                                            string Allowpopup = objCommon.LookUp("reff WITH (NOLOCK)", "ALLOWLOGOUTPOPUP", "");
                                            Session["currentsession"] = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "count(*)", "FLOCK=1")) == 0 ? "0" : objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1");
                                            Session["sessionname"] = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "count(*)", "FLOCK=1")) == 0 ? "" : objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "FLOCK=1");
                                            Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION WITH (NOLOCK)", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");
                                            Session["FeesSessionStartDate"] = "2014";
                                            Session["FeesSessionEndDate"] = "2015";

                                            ipAddress = Request.ServerVariables["REMOTE_HOST"];
                                            Session["ipAddress"] = ipAddress;
                                            //Session["college_nos"] = objUA.College_No;
                                            Session["IPADDR"] = ipAddress;
                                            Session["WorkingDate"] = DateTime.Now.ToString();
                                            Session["college_nos"] = objUA.COLLEGE_CODE;
                                            //Session["macAddress"] = "0000000";
                                            //string macAddress = GetMacAddress(ipAddress);
                                            macAddress = GetMACAddress();
                                            Session["macAddress"] = macAddress;
                                            Session["MACADDR"] = macAddress;
                                            Session["Session"] = Session["sessionname"].ToString();

                                            //Code for LogTable
                                            //=================
                                            int retLogID = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
                                            Session["logid"] = retLogID + 1;


                                            string IMAGE = string.Empty;

                                            #region FOR STORE MODULE
                                            //////FOR STORE MODULE
                                            //================================================================================
                                            if (Session["usertype"].ToString() != "2")
                                            {
                                                Application["strrefmaindept"] = objCommon.LookUp("STORE_REFERENCE WITH (NOLOCK)", "MDNO", "");
                                                Session["sanctioning_authority"] = objCommon.LookUp("STORE_REFERENCE WITH (NOLOCK)", "SANCTIONING_AUTHORITY", "");
                                                Session["Is_Mail_Send"] = objCommon.LookUp("STORE_REFERENCE WITH (NOLOCK)", "IS_MAIL_SEND", "");
                                                if (Session["userno"] != null)
                                                {
                                                    string SDNO = string.Empty;
                                                    if (Session["idno"].ToString() != "0" && Session["idno"].ToString().Trim() != "")
                                                    {
                                                        SDNO = objCommon.LookUp("PAYROLL_EMPMAS WITH (NOLOCK)", "ISNULL(SUBDEPTNO,0) SUBDEPTNO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
                                                        if (Convert.ToInt32(SDNO) > 0)
                                                        {
                                                            Session["SubDepID"] = SDNO;
                                                            Session["strdeptcode"] = objCommon.LookUp("STORE_SUBDEPARTMENT WITH (NOLOCK)", "MDNO", "PAYROLL_SUBDEPTNO=" + Convert.ToInt32(SDNO));



                                                            if (Session["strdeptcode"] != null && Session["strdeptcode"].ToString().Trim() != "")
                                                            {
                                                                Session["strdeptname"] = objCommon.LookUp("STORE_DEPARTMENT WITH (NOLOCK)", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
                                                            }
                                                            else
                                                            {
                                                                Session["strdeptname"] = null;
                                                            }
                                                        }
                                                    }
                                                    else if (Session["userno"] != null)
                                                    {
                                                        Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER WITH (NOLOCK)", "DISTINCT MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                                                        if (Session["strdeptcode"] != null && Session["strdeptcode"].ToString().Trim() != "")
                                                        {
                                                            Session["strdeptname"] = objCommon.LookUp("STORE_DEPARTMENT WITH (NOLOCK)", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
                                                        }
                                                        else
                                                        {
                                                            Session["strdeptname"] = null;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Session["strdeptname"] = objCommon.LookUp("STORE_DEPARTMENT", "MDNAME", "MDNO=" + Convert.ToInt32(Application["strrefmaindept"].ToString()));
                                                        //Session["strdeptcode"] = Application["strrefmaindept"].ToString();
                                                    }
                                                }
                                            }
                                            //================================================================================
                                            //////STORE MODULE END
                                            #endregion FOR STORE MODULE
                                            LogFile objLF = new LogFile();
                                            objLF.Ua_Name = Session["username"].ToString();
                                            objLF.LoginTime = DateTime.Now;
                                            macAddress = GetMACAddress();
                                            Session["macAddress"] = macAddress;
                                            Session["MACADDR"] = macAddress;
                                            int a = objUC.AddtoLogTran(Session["username"].ToString(), ipAddress, Session["macAddress"].ToString(), Convert.ToDateTime(DateTime.Now));
                                            Session["loginid"] = a.ToString();

                                            if (Convert.ToString(Session["firstlog"]) == "False")
                                            {
                                                Response.Redirect("~/changePassword.aspx?IsReset=1");
                                                //Response.Redirect("~/agreement.aspx");
                                            }
                                            else
                                            {
                                                if (Session["lastloginid"].ToString() != "")
                                                {
                                                    if (lastlogout == "" && Allowpopup == "1")
                                                    {
                                                        Response.Redirect("~/SignoutHold.aspx", false);
                                                    }
                                                    else
                                                    {
                                                        // if (Session["username"].ToString() == "PRINCIPAL" || Session["username"].ToString() == "admin")
                                                        if (Session["username"].ToString() == "superadmin")
                                                        {
                                                            Response.Redirect("~/RFC_CONFIG/home.aspx", false);
                                                        }
                                                        else if (Session["usertype"].ToString() == "1") // Dashboard should display through admin level login
                                                        {
                                                            Response.Redirect("~/principalHome.aspx", false);
                                                        }
                                                        else if (Session["usertype"].ToString() == "2") // Dashboard should display through admin level login
                                                        {
                                                            Response.Redirect("~/studeHome.aspx", false);
                                                        }
                                                        else if (Session["usertype"].ToString() == "3")
                                                        {
                                                            Response.Redirect("~/homeFaculty.aspx", false);
                                                        }
                                                        // Added By Shrikant Bharne on 11-11-2022 to Show Dashboard to Non Teaching Usertype.
                                                        else if (Session["usertype"].ToString() == "5")
                                                        {
                                                            //Response.Redirect("~/homeFaculty.aspx", false);
                                                            Response.Redirect("~/homeNonFaculty.aspx", false);
                                                        }
                                                        else
                                                        {
                                                            Response.Redirect("~/home.aspx", false);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));
                                        //objCommon.DisplayMessage(updLog,"Login Failed !, Please Check Your Username Or Password !", this.Page);
                                        if (ua_status == "1")
                                        {
                                            string subject = "ERP Login Credentials";
                                            string message = "Due to the unsucessfully  " + ATTEMPT + " login attempt ,your ERP account is blocked. Please contact system administrator!";
                                            if (emailid != "")
                                            {
                                                objCommon.sendEmail(message, emailid, subject);
                                            }
                                            //* objCommon.DisplayMessage(updLog,"Your Account is Blocked.Please Contact system Administrator!", this.Page);

                                            objCommon.DisplayMessage("Your Account is Blocked.Please Contact system Administrator!", this.Page);
                                            lblStatus.Text = "Your Account is Blocked.Please Contact system Administrator!";
                                            return;
                                        }
                                        else
                                        {
                                            ipAddress = Request.ServerVariables["REMOTE_HOST"];
                                            macAddress = GetMACAddress();
                                            Session["macAddress"] = macAddress;
                                            b = objUC.Failedlogin(username, ipAddress, Session["macAddress"].ToString(), Convert.ToDateTime(DateTime.Now));
                                            //objCommon.DisplayMessage(updLog,"Login Failed !, Please Check Your Username Or Password !Only " + (ATTEMPT - b) + " attempt left!!", this.Page);
                                            objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !Only " + (ATTEMPT - b) + " attempt left!!", this.Page);
                                            lblStatus.Text = "Login Failed !, Please Check Your Username Or Password !Only " + (ATTEMPT - b) + " attempt left!!";
                                            this.UpdateCaptchaText();
                                            txtcaptcha.Text = string.Empty;
                                        }

                                    }
                                }
                                else
                                {

                                    txt_username.Text = string.Empty;
                                    //* objCommon.DisplayMessage(updLog, "Login Failed !, Please Check Your Username Or Password !", this.Page);
                                    objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !", this.Page);
                                    this.UpdateCaptchaText();
                                    txtcaptcha.Text = string.Empty;
                                }
                            }
                            else
                            {
                                txt_username.Text = string.Empty;
                                //* objCommon.DisplayMessage(updLog, "Login Failed !, Please Check Your Username Or Password !", this.Page);
                                objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !", this.Page);
                                this.UpdateCaptchaText();
                                txtcaptcha.Text = string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            //* objCommon.DisplayMessage(updLog, "Login Failed !, Please Check Your Username Or Password !", this.Page);
                            objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !", this.Page);
                            this.UpdateCaptchaText();
                            txtcaptcha.Text = string.Empty;
                        }
                    }
                    else
                    {
                        //* objCommon.DisplayMessage(updLog, "Login Failed !, Captcha is not matched !", this.Page);
                        objCommon.DisplayMessage("Login Failed !, Captcha is not matched !", this.Page);
                        this.UpdateCaptchaText();
                        txtcaptcha.Text = string.Empty;
                    }
                }
                else
                {
                    //* objCommon.DisplayMessage(updLog, "Login Failed !, Captcha is not matched !", this.Page);
                    objCommon.DisplayMessage("Login Failed !, Captcha is not matched !", this.Page);
                    this.UpdateCaptchaText();
                    txtcaptcha.Text = string.Empty;
                }

            }
        }
    }

    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        UpdateCaptchaText();
    }

    protected void btn_Reset_Click(object sender, EventArgs e)// Reset button submit method// sunita
    {
        try
        {
            //UpdateCaptchaText();
            bool success = false;
            txt_captcha.Text = txt_captcha.Text.ToUpper();
            CaptchaControl1.ValidateCaptcha(txt_captcha.Text);
            if (CaptchaControl1.UserValidated)
            {
                success = true;
            }

            lblStatus.Visible = true;

            if (success)
            {
                User_AccController objUC = new User_AccController();

                string username = txtName.Text.Trim().Replace("'", "");
                string emailid = txt_emailid.Text.Trim().Replace("'", "");
                DataSet ds = null;
                string Mobileno = string.Empty;
                try
                {
                    ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME, UA_IDNO, UA_TYPE, UA_FULLNAME, UA_EMAIL, UA_MOBILE, UA_STATUS, UA_FIRSTLOG", "ISNULL(UA_NAME,'') ='" + username + "' AND ISNULL(UA_EMAIL,'') ='" + emailid + "'", "UA_NO");
                }
                catch (Exception ex1)
                {
                    lbl1.Text = "lbl1  " + ex1.ToString();
                }
                try
                {
                    Mobileno = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_MOBILE", "ISNULL(UA_NAME,'') ='" + username + "' AND ISNULL(UA_EMAIL,'') ='" + emailid + "'");
                }
                catch (Exception ex2)
                {
                    lbl2.Text = "lbl2  " + ex2.ToString();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["UA_STATUS"].ToString().Trim() == "0")
                    {
                        string pwd = GeneartePassword();
                        //string encryptpwd = EncryptPassword(pwd);
                        string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);

                        string subject = "ERP Login Credentials";
                        string message = "<b>Dear " + ds.Tables[0].Rows[0]["UA_FULLNAME"] + "</b><br />";
                        message += "Your ERP Password has been reset successfully. Your new login password for ERP is <b>" + pwd + ".</b>";
                        message += "<br /><br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                        int status = 0;
                        try
                        {
                            //status = SendMailBYSendgrid(message, emailid, subject);
                            //status = sendEmail(message, emailid, subject);
                            Task<int> task = Execute(message, emailid, subject);
                            status = task.Result;


                        }
                        catch (Exception ex3)
                        {
                            lbl3.Text = "lbl3  " + "email status " + status + " " + ex3.ToString();
                        }
                        if (status == 1)
                        {
                            //Update sent mail password in database
                            if (objUC.ValidateResetPassword(username, emailid, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
                            {
                                lblStatus.Text = "Your Password successfully reset and forwarded to your registered email id or Mobile No.";
                                lblStatus.ForeColor = System.Drawing.Color.Green;
                                //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                objCommon.DisplayMessage("Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                txt_username.Text = string.Empty;
                                txt_emailid.Text = string.Empty;
                                txt_captcha.Text = string.Empty;

                            }
                        }
                        else
                        {
                            lblStatus.Text = "Sorry, Your Application not configured with mail server, Please contact Admin Department!!!";
                            //* objCommon.DisplayMessage(updLog,"Sorry, Your Application not configured with mail server, Please contact Admin Department !!", this.Page);
                            objCommon.DisplayMessage("Sorry, Your Application not configured with mail server, Please contact Admin Department !!", this.Page);
                        }

                        if ((Mobileno != "" || Mobileno != null))
                        {
                            // objCommon.SendSMS("Your MIS Password has been reset successfully! Your new Login Password is <b>" + "" + pwd + "" + "</b>", ds.Tables[0].Rows[0]["UA_MOBILE"].ToString());
                            objCommon.SendSMS(Mobileno, "Your ERP Password has been reset successfully! Your new Login Password is <b>" + "" + pwd + "" + "</b>");
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Sorry, Your Account Already Blocked,Please contact Admin Department!";
                        //*  objCommon.DisplayMessage(updLog, "Sorry, Your Account Already Blocked,Please contact Admin Department!", this.Page);
                        objCommon.DisplayMessage("Sorry, Your Account Already Blocked,Please contact Admin Department!", this.Page);
                    }

                }
                else
                {
                    //*  objCommon.DisplayMessage(updLog, "Sorry , Your email id not registered in system, Please contact Admin Department!", this.Page);
                    objCommon.DisplayMessage("Sorry , Your email id not registered in system, Please contact Admin Department!", this.Page);
                    txtName.Text = string.Empty;
                    txt_emailid.Text = string.Empty;
                    txt_captcha.Text = string.Empty;
                }
            }
            else
            {
                //*  objCommon.DisplayMessage(updLog,"Entered captcha character not match. Please enter characters as shown in above captcha image!", this.Page);
                objCommon.DisplayMessage("Entered captcha character not match. Please enter characters as shown in above captcha image!", this.Page);

                txtName.Text = string.Empty;
                txt_emailid.Text = string.Empty;
                txt_captcha.Text = string.Empty;
            }
        }
        catch (Exception ex4)
        {
            //objCommon.DisplayMessage(updLog,"Error...", this.Page);
            lbl4.Text = "lbl3  " + ex4.ToString();
            //*  objCommon.DisplayMessage(updLog, "lbl4  " + ex4.ToString(), this.Page);
            objCommon.DisplayMessage("lbl4  " + ex4.ToString(), this.Page);
        }
    }

    private string GeneartePassword()
    {
        string allowedChars = "";
        // allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        //  allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 7; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }

    public static string EncryptPassword(string password)
    {
        string mchar = string.Empty;
        string pvalue = string.Empty;

        for (int i = 1; i <= password.Length; i++)
        {
            mchar = password.Substring(i - 1, 1);
            byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
            int no = int.Parse(bt[0].ToString());
            char ch = Convert.ToChar(no + i);
            pvalue += ch.ToString();
        }
        return pvalue;
    }
    //Added by Sunita
    //protected void txtName_TextChanged(object sender, EventArgs e)
    //{
    //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal').modal('show')", true);
    //    string username = txtName.Text.ToString();
    //    string email = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_EMAIL", "UA_NAME='" + username + "'");
    //    txt_emailid.Text = email.ToString();
    //    txt_captcha.Focus();
    //}
    //ADDED FOR SECURITY ON DATED 10/08/2022
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal').modal('show')", true);
        string username = txtName.Text.ToString();

        string email = string.Empty;
        if (Regex.IsMatch(username, "^[a-zA-Z0-9,@._]*$"))
        {
            if (checkForSQLInjection(username.Trim()))
            {
                txtName.Text = string.Empty;
                username = string.Empty;
                return;
            }
            email = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_EMAIL", "UA_NAME='" + username + "'");
            if (email.ToString() == "")
            {
                txt_emailid.Text = string.Empty;
                txtName.Text = string.Empty;
            }
            else
            {
                txt_emailid.Text = email.ToString();
                txt_captcha.Focus();
            }
        }
        else
        {
            txtName.Text = string.Empty;
            username = string.Empty;
        }

    }

    public static Boolean checkForSQLInjection(string userInput)
    {

        bool isSQLInjection = false;

        string[] sqlCheckList = { "--",";--",";","/*","*/","@@",

                                            "char",

                                           "nchar",

                                           "varchar",

                                           "nvarchar",

                                           "alter",

                                           "begin",

                                           "cast",

                                           "create",

                                           "cursor",

                                           "declare",

                                           "delete",

                                           "drop",

                                           "end",

                                           "exec",

                                           "execute",

                                           "fetch",

                                                "insert",

                                              "kill",

                                                 "select",

                                               "sys",

                                                "sysobjects",

                                                "syscolumns",

                                               "table",

                                               "update"

                                           };

        string CheckString = userInput.Replace("'", "''");

        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
        {

            if ((CheckString.IndexOf(sqlCheckList[i],

StringComparison.OrdinalIgnoreCase) >= 0))

            { isSQLInjection = true; }
        }

        return isSQLInjection;
    }
    //END SECURITY
    // Added Google Sign In on Date 21/09/2020 by Deepali
    [WebMethod]
    public static List<Packet> GetData(string SLOGINDATA, string IDTOKEN)
    {

        altas_login objdefault = new altas_login();
        Common objCommon = new Common();
        List<Packet> lstpkg = new List<Packet>();

        string test = "";
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                int ATTEMPT = 0;
                User_AccController objUC = new User_AccController();
                UserAcc objUA = objUC.GetSingleRecordByGoogleUser(SLOGINDATA, IDTOKEN);
                if (objUA.UA_No != 0)
                {
                    objdefault.GetGoogleSignIn(objUA.UA_No);

                    lstpkg.Add(new Packet
                    {
                        userno = Convert.ToString(objdefault.Session["userno"]),
                        firstlog = Convert.ToString(objdefault.Session["firstlog"]),
                        lastlogout = Convert.ToString(objdefault.Session["lastlogout"]),
                        Allowpopup = Convert.ToString(objdefault.Session["Allowpopup"])
                    });

                }
                else { }
            }
        }
        return lstpkg;
    }

    // Added Google Sign In on Date 21/09/2020 by Deepali
    private void GetGoogleSignIn(int userno)
    {
        if (userno != 0)
        {
            User_AccController objUC = new User_AccController();
            UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

            if (userno != 0)
            {

                Session["userno"] = objUA.UA_No.ToString();
                Session["idno"] = Convert.ToInt32(objUA.UA_IDNo);
                Session["username"] = objUA.UA_Name;
                Session["usertype"] = objUA.UA_Type;
                Session["userfullname"] = objUA.UA_FullName;
                Session["dec"] = objUA.UA_Dec.ToString();
                Session["userdeptno"] = objUA.UA_DeptNo.ToString();
                Session["colcode"] = objCommon.LookUp("reff with (nolock)", "College_code", string.Empty);
                Session["firstlog"] = objUA.UA_FirstLogin;
                Session["UA_DESIG"] = objUA.UA_Desig;
                Session["payment"] = "default";
                //Session["currentsession"] = "56";
                //Session["sessionname"] = "2013-14 II REG";

                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "FLOCK=1");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSION_NAME", "FLOCK=1");
                Session["FeesSessionStartDate"] = "2014";
                Session["FeesSessionEndDate"] = "2015";


                //Session["macAddress"] = "0000000";
                //string macAddress = GetMacAddress(ipAddress);
                macAddress = GetMACAddress();
                Session["macAddress"] = macAddress;
                Session["userEmpDeptno"] = objUA.UA_EmpDeptNo.ToString();


                //  Help and Freshdesk Feedback CSS  added by Shubham STATRT  // 20/09/2019
                //Session["FRESHDESK_STATUS"] = objUA.ua_Freshdesk_Status.ToString(); //deepali commented
                //  Help and Freshdesk Feedback CSS  added by Shubham END  // 20/09/2019
                //Code for LogTable
                //=================

                string lastloginid = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
                if (lastloginid != "")
                    Session["lastloginid"] = lastloginid.ToString();
                else
                    Session["lastloginid"] = "0";

                string lastlogout = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
                string Allowpopup = objCommon.LookUp("reff with (nolock)", "ALLOWLOGOUTPOPUP", "");


                //////FOR STORE MODULE
                Session["strrefmaindept"] = objCommon.LookUp("STORE_REFERENCE WITH (NOLOCK)", "MDNO", "");

                string aa = Session["strrefmaindept"].ToString();
                if (Session["userno"] != null)
                {
                    int count = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENTUSER WITH (NOLOCK)", "count(*)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                    if (count > 0)
                    {
                        Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER WITH (NOLOCK)", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                        Session["strdeptuserlevel"] = objCommon.LookUp("STORE_DEPARTMENTUSER WITH (NOLOCK)", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

                        if (Session["strdeptcode"] != null)
                            Session["strdeptname"] = objCommon.LookUp("STORE_DEPARTMENT WITH (NOLOCK)", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
                        else
                            Session["strdeptname"] = null;
                    }
                }
                //////STORE MODULE END
                LogFile objLF = new LogFile();
                objLF.Ua_Name = Session["username"].ToString();
                objLF.LoginTime = DateTime.Now;


                int a = objUC.AddtoLogTran(Session["username"].ToString(), Convert.ToString(Session["ipAddress"]), macAddress, Convert.ToDateTime(DateTime.Now));
                Session["loginid"] = a.ToString();

                //HiddenField hdffirstlog =Session["firstlog"].ToString() as HiddenField;

                //this.hdffirstlog.Value =Convert.ToString(Session["firstlog"].ToString());     //Convert.ToString(Session["firstlog"]);
                Session["lastlogout"] = lastlogout;
                Session["Allowpopup"] = Allowpopup;
                //hdfuserno.Value = Convert.ToString(Session["userno"]);
                //if (Convert.ToString(Session["firstlog"]) == "False")   //Convert.ToInt32(Session["usertype"]) == 2 && 
                //{
                //    Response.Redirect("~/changePassword.aspx?IsReset=1");
                //}
                //else
                //{
                //    if (lastlogout == "" && Allowpopup == "1")
                //    {
                //        Response.Redirect("~/SignoutHold.aspx", false);
                //    }
                //    else if (Session["userno"].ToString() == "1")
                //    {
                //        Response.Redirect("~/DashBoard_Home.aspx", false);
                //    }
                //    else
                //    {
                //        Response.Redirect("~/home.aspx", false);
                //    };

                //}
            }
            else
            {

                objCommon.DisplayMessage(UpdatePanel1, "Login Failed !, Please Check Your Username Or Password !", Page);

            }
        }
        else
        {

        }

    }
    //added by prafull
    //public int sendEmail(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
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
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //            return ret = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {


        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            //SmtpMail oMail = new SmtpMail("TryIt");



            //oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();



           // oMail.To = toEmailId;



           // oMail.Subject = sub;



           // oMail.HtmlBody = Message;




            // SmtpServer oServer = new SmtpServer("smtp.live.com");



            //SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022



           // oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
           // oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();



           // oServer.Port = 587;



            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
           // Console.WriteLine("start to send email over TLS...");



            //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();





            //oSmtp.SendMail(oServer, oMail);




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

    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;

    //    try
    //    {

    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ERP");
    //        var toAddress = new MailAddress(toEmailId, "");

    //        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ERP");
    //        var subject = sub;
    //        var to = new EmailAddress(toEmailId, "");
    //        var plainTextContent = "";
    //        var htmlContent = Message;
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //        {
    //            ret = 1;
    //        }
    //        else
    //        {
    //            ret = 0;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}
    //ADDED BY PRAFULLA
    //public int sendEmail(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
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
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //            return ret = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}
    protected void btnSendUsernamePassword_Click(object sender, EventArgs e)
    {

        VerifyEmailOtp();

    }


    protected void btnForgotPasswordMobile_Click(object sender, EventArgs e)
    {
        VerifyMobileOtp();
    }

    protected void VerifyMobileOtp()
    {
        try
        {
            if (txtOtp.Text != string.Empty || txtOtp.Text != "")
            {
                if (ViewState["OTP"].ToString().Trim() != txtOtp.Text.Trim())
                {

                    //lblVerify.Text = "Please Enter the Valid OTP";
                    //lblVerify.ForeColor = System.Drawing.Color.Red;
                    Showmessage("Please Enter Valid OTP");
                    return;
                }
                else
                {
                    lblVerify.Text = "OTP VERIFIED";
                    lblVerify.ForeColor = System.Drawing.Color.Green;
                    //Showmessage("OTP is verified successfully");
                    //txtOtp.Enabled = false;    
                    ViewState["OTP"] = string.Empty;
                    txtOtp.Text = string.Empty;
                    lblOtp.Visible = false;
                    txtOtp.Visible = false;


                    SendUsernamePasswordOnMobile();

                    return;


                }
            }
            else
            {
                Showmessage("Please Enter OTP");
                return;
            }
        }
        catch (Exception)
        {
            return;
        }
    }


    protected void VerifyEmailOtp()
    {
        try
        {
            //if (txtOtp2.Text != string.Empty || txtOtp2.Text != "")
            //{
            if (ViewState["EmailOTP"].ToString().Trim() != txtOtp2.Text.Trim())
            {

                //lblVerify2.Text = "Please Enter the Valid OTP";
                //lblVerify2.ForeColor = System.Drawing.Color.Red;
                Showmessage("Please Enter Valid OTP");
                return;
            }
            else
            {
                lblVerify2.Text = "OTP VERIFIED";
                lblVerify2.ForeColor = System.Drawing.Color.Green;
                //Showmessage("OTP is verified successfully");
                //txtOtp2.Enabled = false;

                ViewState["EmailOTP"] = string.Empty;
                txtOtp2.Text = string.Empty;
                lblotp2.Visible = false;
                txtOtp2.Visible = false;

                SendUsernamePassword();

                //btnVerifyEmailOtp.Visible = false;
                return;
            }
            //}
            //else
            //{
            //    Showmessage("Please Enter OTP");
            //    return;

            //}
        }
        catch (Exception)
        {
            return;
        }

    }


    protected void SendUsernamePasswordOnMobile()
    {

        User_AccController objUC = new User_AccController();

        string mobilenumber = txtMobile.Text.Trim().Replace("'", "");
        //string useremail = txtEmail.Text.Trim().Replace("'", "");

        if (rdoPassword.Checked == true)
        {

            try
            {
                int status = 0;
                string Password = GeneartePassword();
                string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + mobilenumber + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your PassWord for Login is :<b>" + Password + "<b>";
                message += "<br /><br />Thank You<br />";


                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //status = sendEmail(message, txtEmail.Text, "ERP || Password for Candidate");
                        Task<int> task = Execute(message, txtEmail.Text, "ERP || Password for Candidate");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
                        status = task.Result;
                        if (status == 1)
                        {


                            if (status == 1)
                            {
                                //Update sent mail password in database
                                if (objUC.ValidateResetPasswordMobile(mobilenumber, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
                                {

                                    //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                    Showmessage(" OTP is verified.....And Your Password successfully reset and forwarded to your registered email id .");

                                    txtEmail.Text = string.Empty;


                                }
                            }
                            else
                            {
                                Showmessage("Sorry, Your Application not configured with mail server, Please contact Admin Department !!");
                            }
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
                return;
            }

        }
        else
        {
            try
            {
                int status = 0;
                string username = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_EMAIL='" + mobilenumber + "' ");

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + mobilenumber + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your UserName for Login is :<b>" + username + "<b>";
                message += "<br /><br />Thank You<br />";


                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //status = sendEmail(message, txtEmail.Text, "ERP || UserName for Candidate");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP ||UserName for Candidate");
                        //status = task.Result;

                        Task<int> task = Execute(message, txtEmail.Text, "ERP || UserName for Candidate");
                        status = task.Result;
                        if (status == 1)
                        {
                            if (objUC.ValidateResetPassword(mobilenumber, username) == Convert.ToInt32((CustomStatus.ValidUser)))
                            {

                                //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                Showmessage(" OTP is verified.....And Your Username successfully reset and forwarded to your registered email id .");

                                txtEmail.Text = string.Empty;


                            }

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
                return;
            }
        }
    }


    protected void SendUsernamePassword()
    {

        User_AccController objUC = new User_AccController();
        string useremail = txtEmail.Text.Trim().Replace("'", "");

        if (rdoPassword.Checked == true)
        {

            try
            {
                int status = 0;
                string Password = GeneartePassword();
                string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your PassWord for Login is :<b>" + Password + "<b>";
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team ERP <br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                if (txtEmail.Text != string.Empty)
                {
                    try
                    {

                        //status = sendEmail(message, txtEmail.Text, "ERP || Password for Candidate");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
                        //status = task.Result;
                        Task<int> task = Execute(message, txtEmail.Text, "ERP || Password for Candidate");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
                        status = task.Result;
                        if (status == 1)
                        {


                            if (status == 1)
                            {
                                //Update sent mail password in database
                                if (objUC.ValidateResetPassword(useremail, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
                                {

                                    //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                    Showmessage("  OTP is verified.....And Your Password successfully reset and forwarded to your registered email id .");

                                    txtEmail.Text = string.Empty;


                                }
                            }
                            else
                            {
                                Showmessage("Sorry, Your Application not configured with mail server, Please contact Admin Department !!");
                            }
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
                return;
            }

        }
        else
        {
            try
            {
                int status = 0;
                string username = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_EMAIL='" + useremail + "' ");

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your UserName for Login is :<b>" + username + "<b>";
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team ERP<br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //status = sendEmail(message, txtEmail.Text, "ERP || UserName for Candidate");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP ||UserName for Candidate");
                        //status = task.Result;

                        Task<int> task = Execute(message, txtEmail.Text, "ERP || UserName for Candidate");
                        status = task.Result;
                        if (status == 1)
                        {
                            //if (objUC.ValidateResetPassword(useremail, username) == Convert.ToInt32((CustomStatus.ValidUser)))
                            //{

                            //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                            Showmessage(" OTP is verified.....And Your Username successfully reset and forwarded to your registered email id .");

                            txtEmail.Text = string.Empty;


                            // }

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
                return;
            }
        }
    }


    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
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

    protected void btnEmailotp_Click(object sender, EventArgs e)
    {
        //CheckEmail();
        string useremail = txtEmail.Text.Trim().Replace("'", "");
        string mail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_EMAIL='" + useremail + "' ");

        if (mail == useremail && txtEmail.Text.Trim() != "")
        {

            //btnVerifyEmailOtp.Visible = true;
            try
            {
                int status = 0;
                //.Visible = true;
                string otp = GenerateOTP(5);
                ViewState["EmailOTP"] = otp;

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your One Time Password (OTP) for Reset password is " + otp;
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team ERP<br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //status = SendMailBYSendgrid(Message, txtemailid.Text, "ERP || OTP for reset password");
                        //status = sendEmail(message, txtEmail.Text, "ERP || OTP for reset password");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP || OTP for reset password");
                        //status = task.Result;

                        Task<int> task = Execute(message, txtEmail.Text, "ERP || OTP for reset password");
                        status = task.Result;
                        if (status == 1)
                        {
                            Showmessage("OTP has been send on Your Email Id, Enter To Continue Reset Password Process.");
                            lblotp2.Visible = true;
                            txtOtp2.Visible = true;

                        }
                        else
                        {
                            Showmessage("Failed to send email");
                            txtOtp.Visible = false;
                        }
                    }
                    catch (Exception)
                    {
                        Showmessage("Failed to send email");
                        txtEmail.Visible = false;

                    }
                }

            }

            catch (Exception)
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
                return;
            }

        }
        else
        {

            Showmessage("Sorry,Your Entered Email Address is Not Registerd..Please Contact to Admin");
            lblotp2.Visible = false;
            txtOtp2.Visible = false;
            txtEmail.Text = string.Empty;
            return;

        }
    }


    protected void btnMobileotp_Click(object sender, EventArgs e)
    {

        int mobile = txtMobile.Text.Length;
        if (mobile == 10)
        {
            lblOtp.Visible = true;
            txtOtp.Visible = true;

            string Mobile = txtMobile.Text.Trim().Replace("'", "");
            string mobileNumber = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_MOBILE='" + Mobile + "' ");

            if (Mobile == mobileNumber && txtMobile.Text.Trim() != "")
            {

                lblOtp.Visible = true;
                txtOtp.Visible = true;
                // btnMobOtpVerify.Visible = true;
                try
                {

                    string otp = GenerateOTP(5);
                    ViewState["OTP"] = otp;
                    string Url = Session["SMSProvider"].ToString();
                    string UserId = Session["SMSSVCID"].ToString(); //"hr@iitms.co.in";
                    //string Password = Common.DecryptPassword(Session["SMSSVCPWD"].ToString());//"iitmsTEST@5448";
                    string Password = (Session["SMSSVCPWD"].ToString());
                    string MobileNo = "91" + txtMobile.Text.Trim();

                    string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + Mobile + "' ");
                    string message = "<b>Dear " + StudName + "," + "</b><br />";
                    message += "Your One Time Password (OTP) for Reset password is " + otp;

                    if (txtMobile.Text.Trim() != string.Empty)
                    {
                        SendSMS(Url, UserId, Password, MobileNo, message);
                    }

                    Showmessage("OTP has been send on Your Mobile No,Enter To Continue Reset Password Process.");
                    // txtEmail.Text = string.Empty;

                }
                catch (Exception)
                {
                    txtOtp.Visible = false;
                    return;
                }

            }

            else
            {
                lblOtp.Visible = false;
                txtOtp.Visible = false;

                Showmessage("Sorry,Your Entered Mobile Number is Not Registerd..Please Contact to Admin");
                txtMobile.Text = string.Empty;

                return;





            }

        }
        else
        {
            Showmessage("Mobile No. required 10 digit.");
            txtMobile.Text = string.Empty;
            return;
        }

    }


    public void SendSMS(string url, string uid, string pass, string mobno, string message)
    {
        try
        {
            WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
            //return urlText;
        }
        catch (Exception)
        {

        }
    }


    protected void rdoUsername_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoUsername.Checked == true)
        {
            pnlChoice.Visible = true;
            //rdoPassword.Visible = false;
            rdoMobile.Checked = false;
            rdoEmail.Checked = false;
            pnlEmail.Visible = false;
            pnlMobile.Visible = false;

        }

        else
        {
            pnlChoice.Visible = false;
        }
    }
    protected void rdoPassword_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoPassword.Checked == true)
        {
            pnlChoice.Visible = true;
            //rdoUsername.Visible = false;
            //pnlUserPass.Visible = false;
            rdoMobile.Checked = false;
            rdoEmail.Checked = false;
            pnlMobile.Visible = false;
            pnlEmail.Visible = false;


        }

        else
        {
            pnlChoice.Visible = false;

        }
    }
    protected void rdoMobile_CheckedChanged(object sender, EventArgs e)
    {

        if (rdoMobile.Checked)
        {
            pnlUserPass.Visible = true;

            pnlMobile.Visible = true;
            pnlChoice.Visible = true;
            pnlEmail.Visible = false;
        }
    }

    protected void rdoEmail_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoEmail.Checked)
        {
            pnlUserPass.Visible = true;
            pnlMobile.Visible = false;
            pnlChoice.Visible = true;
            pnlEmail.Visible = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        txtEmail.Text = string.Empty;
    }


    private int CheckEmail()
    {
        int i = 0;

        bool isEmail = Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9_]+(?:\.[a-z0-9_]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (!isEmail)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Query", "alert('Email Address Require only Alphanumeric character and @,_,. Symbol')", true);
            i = 1;
        }

        return i;
    }
}