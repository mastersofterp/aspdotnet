﻿using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net.NetworkInformation;
using System.Data;
using System.Net.Mail;
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
using EASendMail;
using BusinessLogicLayer.BusinessLogic;
using Mastersoft.Security.IITMS;
using PageControlValidator;

public partial class Default_PCE : System.Web.UI.Page
{
    public class Packet
    {
        public string userno { get; set; }
        public string firstlog { get; set; }
        public string lastlogout { get; set; }
        public string Allowpopup { get; set; }

    }

    //Common Class
    Common objCommon = new Common();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

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
        Session["OrgId"] = objCommon.LookUp("reff with (nolock)", "OrganizationId", string.Empty);

        if (!Page.IsPostBack)
        {
            // Timer1.Enabled = true;
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

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    Page_Load(sender, e);
    //}

    protected Boolean ValidateSecurityEmail()    //Added by sachin Lohakare On 31-08-2023
    {
        try
        {
            string DisplayMessage = string.Empty;

            DisplayMessage = ValidateControls.ValidateTextBoxLength(txtEmail.Text, txtEmail.MaxLength);
            if (DisplayMessage != "")
            {
                objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
                txtEmail.Focus();
                return false;
            }

            if (txtEmail.Text == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Email Id", Page);
                return false;
            }

            if (txtEmail.Text != string.Empty)
            {
                if (SecurityThreads.ValidInput(txtEmail.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                    return false;
                }

                if (SecurityThreads.CheckSecurityInput(txtEmail.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                    return false;
                }
            }

            if (txtMobile.Text != string.Empty)
            {
                if (SecurityThreads.ValidInput(txtMobile.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                    return false;
                }

                if (SecurityThreads.CheckSecurityInput(txtMobile.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                    return false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "default.ValidateSecurity()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return true;
    }


    protected Boolean ValidateSecurityMobile()  //Added by sachin Lohakare On 31-08-2023
    {
        try
        {

            string DisplayMessage = string.Empty;

            DisplayMessage = ValidateControls.ValidateTextBoxLength(txtMobile.Text, txtMobile.MaxLength);
            if (DisplayMessage != "")
            {
                objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
                txtMobile.Focus();
                return false;
            }


            if (txtMobile.Text == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Mobile No", Page);
                return false;
            }
            if (txtEmail.Text != string.Empty)
            {
                if (SecurityThreads.ValidInput(txtEmail.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                    return false;
                }

                if (SecurityThreads.CheckSecurityInput(txtEmail.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                    return false;
                }
            }

            if (txtMobile.Text != string.Empty)
            {
                if (SecurityThreads.ValidInput(txtMobile.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                    return false;
                }

                if (SecurityThreads.CheckSecurityInput(txtMobile.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                    return false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "default.ValidateSecurity()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return true;
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

                                        //Timer1.Enabled = false; //Added by Mahesh Due to Refresh Captcha after every 90 Sec.

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
                                               // Response.Redirect("~/changePassword.aspx?IsReset=1");
                                                //ADDED on date 18/07/2018 
                                                Response.Redirect("~/agreement.aspx",false);  //related to 90-Days
                                            }
                                            ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));
                                            //objCommon.DisplayMessage(updLog,"Login Failed !, Please Check Your Username Or Password !", this.Page);
                                            if (ua_status == "1")
                                            {
                                                string subject = "ERP Login Credentials";
                                                string message = "Due to the unsucessfully  " + ATTEMPT + " login attempt ,your ERP account is blocked. Please contact system administrator!";
                                                if (emailid != "")
                                                {
                                                    //objCommon.sendEmail(message, emailid, subject);
                                                    objSendEmail.SendEmail(emailid, message, subject); //Calling Method
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
                                            if (Session["usertype"].ToString() != "2" && Session["usertype"].ToString() != "14")
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
                                                //Response.Redirect("~/changePassword.aspx?IsReset=1");
                                                Response.Redirect("~/agreement.aspx",false);
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
                                                        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14") // Dashboard should display through admin level login
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
                                                //objCommon.sendEmail(message, emailid, subject);
                                                objSendEmail.SendEmail(emailid, message, subject); //Calling Method

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
                            status = objSendEmail.SendEmail(emailid, message, subject); //Calling Method
                            //Task<int> task = Execute(message, emailid, subject);
                            //status = task.Result;
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

        Default_PCE objdefault = new Default_PCE();
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

    public int sendEmail(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

            var smtp = new System.Net.Mail.SmtpClient
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
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
                return ret = 1;
            }
        }
        catch (Exception ex)
        {
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
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

    protected void btnSendUsernamePassword_Click(object sender, EventArgs e)
    {
        string DisplayMessage = string.Empty;

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtEusername.Text, txtEusername.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtEusername.Focus();
            return;
        }

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtEnewpass.Text, txtEnewpass.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtnewpass.Focus();
            return;
        }

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtEconfirmPass.Text, txtEconfirmPass.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtEconfirmPass.Focus();
            return;
        }

        string useremailchk = txtEmail.Text.Trim().Replace("'", "");
        string mail = objCommon.LookUp("USER_ACC", "isnull(UA_EMAIL,'')UA_EMAIL", "UA_NAME='" + txtEusername.Text.ToString() + "'  ");

        if (mail != useremailchk)
        {
            objCommon.DisplayMessage(this.UpdatePanel2, "Eamil Not Register For This User Name ", this);
            return;
        }

        if (txtEusername.Text.Trim() == string.Empty || txtEusername.Text.Trim() == "")
        {

            objCommon.DisplayMessage(this.UpdatePanel2, "Please Enter the User Name", this);
            return;
        }
        string UserName = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_NAME='" + txtEusername.Text.ToString() + "' ");

        if (UserName.ToString().Trim() != txtEusername.Text.Trim())
        {
            Showmessage("Please Enter Valid User Name");
            return;
        }
        else
        {

            if (txtEnewpass.Text.Trim() == string.Empty || txtEnewpass.Text.Trim() == string.Empty)
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "Blank password is not allowed", this);

                return;
            }

            if (txtEconfirmPass.Text.Trim() == string.Empty || txtEconfirmPass.Text.Trim() == "")
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "Please Enter Confirm Password", this);

                return;
            }
            if (txtEnewpass.Text.Trim() != txtEconfirmPass.Text.Trim())
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "New & Confirm Password Not Matching", this);
                txtEnewpass.Text = string.Empty;
                txtEconfirmPass.Text = string.Empty;
                return;
            }
            // Regex pass = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{4,8})$");
            //Regex pass = new Regex("^.*(?=.{10,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");

            Regex pass = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{10,}$");



            if (!pass.IsMatch(txtEconfirmPass.Text))
            {
                objCommon.DisplayMessage(this.UpdatePanel2, "Password should be at least 10 characters,Must contain at least one  lower case letter, one upper case letter, one digit and one special character", this);
                txtEnewpass.Text = string.Empty;
                txtEconfirmPass.Text = string.Empty;
                return;
            }
            else
            {
                //Success.
            }
            //
            if (txtEnewpass.Text.Trim() != "")
            {

                try
                {
                    string useremail = txtEmail.Text.Trim().Replace("'", "");

                    User_AccController objUC = new User_AccController();
                    UserAcc objUA = new UserAcc();
                    string UA_type = (objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NAME='" + txtEusername.Text.ToString() + "'"));
                    objUA.UA_Name = txtEusername.Text.ToString();
                    string UA_No = (objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + txtEusername.Text.ToString() + "'"));
                    objUA.UA_No = Convert.ToInt32(UA_No);
                    objUA.UA_Pwd = txtEnewpass.Text.Trim();
                    string UA_OldPass = (objCommon.LookUp("USER_ACC", "UA_PWD", "UA_NAME='" + txtEusername.Text.ToString() + "'"));

                    objUA.UA_OldPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(UA_OldPass.ToString().Trim());


                    objUA.IP_ADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objUA.UA_Type = Convert.ToInt32(UA_type);



                    CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadminFirstLog_1(objUA);


                    if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel2, "Invalid User Name Password", this);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        CustomStatus cs2 = (CustomStatus)objUC.InsertChangePassword(objUA);
                        try
                        {
                            int status = 0;
                            string Password = GeneartePassword();
                            string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);


                            //added by Tejas jaiswal 16/12/2021
                            DataSet dsconfig = null;
                            dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                            //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
                            string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                            string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

                            string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NAME='" + txtEusername.Text.ToString() + "'");

                            string message = "<b>Dear " + StudName + "," + "</b><br />";
                            message += "Your ERP password has been reset successfully";
                            message += "<br /><br /><br />Thank You<br />";
                            message += "<br />Team " + SUBJECT_OTP + " <br />"; //added by Tejas Jaiswal
                            message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                            if (txtEmail.Text != string.Empty)
                            {
                                try
                                {


                                    //status = SendMailBYSendgrid(Message, txtEmailId.Text, "ERP || OTP for reset password");
                                    //status = sendEmail(Message, txtEmailId.Text, "ERP || OTP for reset password");
                                    //Task<int> task = Execute(message, txtEmail.Text, SUBJECT_OTP + " ERP || OTP for reset password");
                                    //status = task.Result;

                                    status = objSendEmail.SendEmail(txtEmail.Text, message, SUBJECT_OTP + " ERP || OTP for reset password"); //Calling Method

                                    if (status == 1)
                                    {


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
                        // objCommon.DisplayMessage(this.UpdatePanel2, "Password changed successfully.", this.Page);
                        // Showmessage("Password changed successfully.");
                        //  clear();
                        //  Response.Redirect(Request.RawUrl);
                        ModalPopupExtender2.Show();
                    }
                }
                catch (Exception)
                {
                    objCommon.DisplayMessage(this.UpdatePanel2, "something went wrong please try again", this);
                    return;
                }

            }

        }


        // VerifyEmailOtp();

    }

    protected void btnForgotPasswordMobile_Click(object sender, EventArgs e)
    {
        string DisplayMessage = string.Empty;

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtusername.Text, txtusername.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtusername.Focus();
            return;
        }

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtnewpass.Text, txtnewpass.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtnewpass.Focus();
            return;
        }

        DisplayMessage = ValidateControls.ValidateTextBoxLength(txtconfirmpass.Text, txtconfirmpass.MaxLength);
        if (DisplayMessage != "")
        {
            objCommon.DisplayMessage(this.Page, "" + DisplayMessage + "", Page);
            txtconfirmpass.Focus();
            return;
        }

        string Mobile = txtMobile.Text.Trim().Replace("'", "");

        string mobileNumber = objCommon.LookUp("USER_ACC", "isnull(UA_MOBILE,'')UA_MOBILE", "UA_NAME='" + txtusername.Text + "'  ");

        if (Mobile != mobileNumber)
        {
            objCommon.DisplayMessage(this.UpdatePanel2, "Mobile Number Not Register For This User Name ", this);
            return;
        }

        if (txtusername.Text.Trim() == string.Empty || txtusername.Text.Trim() == "")
        {

            objCommon.DisplayMessage(this.UpdatePanel2, "Please Enter the User Name", this);
            return;
        }
        string UserName = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_NAME='" + txtusername.Text.ToString() + "' ");

        if (UserName.ToString().Trim() != txtusername.Text.Trim())
        {
            Showmessage("Please Enter Valid User Name");
            return;
        }
        else
        {

            if (txtnewpass.Text.Trim() == string.Empty || txtnewpass.Text.Trim() == "")
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "Blank password is not allowed", this);
                return;
            }
            if (txtconfirmpass.Text.Trim() == string.Empty || txtconfirmpass.Text.Trim() == "")
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "Please Enter Confirm Password", this);
                return;
            }

            if (txtnewpass.Text.Trim() != txtconfirmpass.Text.Trim())
            {
                //lblMessage.Text = "Blank password is not allowed";
                objCommon.DisplayMessage(this.UpdatePanel2, "New & Confirm Password Not Matching", this);
                txtnewpass.Text = string.Empty;
                txtconfirmpass.Text = string.Empty;
                return;
            }
            // Regex pass = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{4,8})$");
            //Regex pass = new Regex("^.*(?=.{10,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");

            Regex pass = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{10,}$");

            if (!pass.IsMatch(txtconfirmpass.Text))
            {
                objCommon.DisplayMessage(this.UpdatePanel2, "Password should be at least 10 characters,Must contain at least one  lower case letter, one upper case letter, one digit and one special character", this);
                txtnewpass.Text = string.Empty;
                txtconfirmpass.Text = string.Empty;
                return;
            }
            else
            {
                //Success.
            }
            //
            if (txtnewpass.Text.Trim() != "" && txtconfirmpass.Text.Trim() != "")
            {
                try
                {
                    string mobilenumber = txtMobile.Text.Trim().Replace("'", "");

                    User_AccController objUC = new User_AccController();
                    UserAcc objUA = new UserAcc();
                    string UA_type = (objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NAME='" + txtusername.Text.ToString() + "'"));
                    objUA.UA_Name = txtusername.Text.ToString();
                    string UA_No = (objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + txtusername.Text.ToString() + "'"));
                    objUA.UA_No = Convert.ToInt32(UA_No);
                    objUA.UA_Pwd = txtnewpass.Text.Trim();
                    string UA_OldPass = (objCommon.LookUp("USER_ACC", "UA_PWD", "UA_NAME='" + txtusername.Text.ToString() + "'"));
                    objUA.UA_OldPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(UA_OldPass.ToString().Trim());


                    objUA.IP_ADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objUA.UA_Type = Convert.ToInt32(UA_type);



                    CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadminFirstLog_1(objUA);

                    if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel2, "Invalid User Name And Password..!", this);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        CustomStatus cs2 = (CustomStatus)objUC.InsertChangePassword(objUA);
                        string Password = GeneartePassword();
                        string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);

                        string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_MOBILE='" + mobilenumber + "' ");


                        string TemplateID = string.Empty;
                        string TEMPLATE = string.Empty;
                        string templatename = "Update Password";
                        DataSet ds = objUC.GetSMSTemplate(0, templatename);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                            TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                        }



                        string message = TEMPLATE;
                        StringBuilder stringBuilder = new StringBuilder();
                        //message = message.Replace("{#var#}", Password);
                        message = message.Replace("{#var#}", StudName);


                        if (txtMobile.Text != string.Empty)
                        {
                            SendSMS(txtMobile.Text.Trim(), message, TemplateID);

                        }
                        //  Showmessage("Password changed successfully.");
                        // objCommon.DisplayMessage(this.UpdatePanel2, "Password changed successfully.", this.Page);
                        //  clear();
                        //  Response.Redirect(Request.RawUrl);
                        ModalPopupExtender2.Show();
                    }
                }
                catch (Exception)
                {
                    objCommon.DisplayMessage(this.UpdatePanel2, "something went wrong please try again", this);
                    return;
                }

            }

        }


        //  VerifyMobileOtp();
    }

    protected void VerifyMobileOtp()
    {
        try
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
                string Mobile = txtMobile.Text;
                string StudName = objCommon.LookUp("USER_ACC", "ISNULL(UA_NAME,'')UA_NAME", "UA_MOBILE='" + Mobile + "' ");
                txtusername.Text = StudName.ToString();


                btnSendotp.Enabled = false;
                btnMobOtpVerify.Enabled = false;

                lblVerify.Text = "OTP VERIFIED";
                lblVerify.ForeColor = System.Drawing.Color.Green;

                //Showmessage("OTP is verified successfully");
                //txtOtp.Enabled = false;    
                ViewState["OTP"] = string.Empty;
                txtOtp.Text = string.Empty;
                lblOtp.Visible = false;
                txtOtp.Visible = false;
                btnMobOtpVerify.Visible = false;



                if (rdoPassword.Checked == true)
                {
                    divLoginId.Visible = true;
                    divnewpass.Visible = true;
                    divconfirmpass.Visible = true;
                    btnForgotPasswordMobile.Visible = true;

                }
                else
                {
                    divLoginId.Visible = false;
                    divnewpass.Visible = false;
                    divconfirmpass.Visible = false;
                    btnForgotPasswordMobile.Visible = false;

                    SendUsernamePasswordOnMobile();
                }

                // return;


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
            if (ViewState["EmailOTP"].ToString().Trim() != txtOtp2.Text.Trim())
            {

                //lblVerify2.Text = "Please Enter the Valid OTP";
                //lblVerify2.ForeColor = System.Drawing.Color.Red;
                Showmessage("Please Enter Valid OTP");
                return;
            }
            else
            {
                string useremail = txtEmail.Text.Trim().Replace("'", "");
                string username = objCommon.LookUp("USER_ACC", "isnull(UA_NAME,'')UA_NAME", "UA_EMAIL='" + useremail + "' ");
                txtEusername.Text = username.ToString();

                benotop2.Enabled = false;
                btnVerifyEmailOtp.Enabled = false;

                lblVerify2.Text = "OTP VERIFIED";
                lblVerify2.ForeColor = System.Drawing.Color.Green;
                //Showmessage("OTP is verified successfully");
                //txtOtp2.Enabled = false;

                ViewState["EmailOTP"] = string.Empty;
                txtOtp2.Text = string.Empty;
                lblotp2.Visible = false;
                txtOtp2.Visible = false;
                btnVerifyEmailOtp.Visible = false;


                if (rdoPassword.Checked == true)
                {
                    divEloginId.Visible = true;
                    divEnewPass.Visible = true;
                    divEconfirmpass.Visible = true;
                    btnSendUsernamePassword.Visible = true;
                }
                else
                {
                    divEloginId.Visible = false;
                    divEnewPass.Visible = false;
                    divEconfirmpass.Visible = false;
                    btnSendUsernamePassword.Visible = false;

                    SendUsernamePassword();
                }
                //btnVerifyEmailOtp.Visible = false;
                //return;
            }
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

        //if (rdoPassword.Checked == true)
        //{

        //    try
        //    {
        //        //1007668552339828780

        //        //int status = 0;
        //        string Password = GeneartePassword();
        //        string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);

        //        string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_MOBILE='" + mobilenumber + "' ");


        //        string TemplateID = string.Empty;
        //        string TEMPLATE = string.Empty;
        //        string templatename = "Reset Password";
        //        DataSet ds = useracc.GetSMSTemplate(0, templatename);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
        //            TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
        //        }



        //        string message = TEMPLATE;
        //        StringBuilder stringBuilder = new StringBuilder();
        //        message = message.Replace("{#var#}", Password);
        //        //string message = "Dear " + StudName + ", \nYour Password for Login is : " + Password + "\nThank You\nMSERP";

        //        if (txtMobile.Text != string.Empty)
        //        {
        //            SendSMS(txtMobile.Text.Trim(), message, TemplateID);
        //            //Task<int> task = Execute(message, txtEmail.Text, " ERP || PassWord for Candidate");  
        //        }

        //        Showmessage(" OTP is verified.....And Your Password successfully reset and forwarded to your registered Mobile NO. ");
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }

        //}
        //else
        {
            try
            {
                //int status = 0;
                string username = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_MOBILE='" + mobilenumber + "' ");

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_MOBILE='" + mobilenumber + "' ");


                string message = "Dear " + StudName + ", \nYour Username for Login is : " + username + "\nThank You\nMSERP";


                if (txtMobile.Text != string.Empty)
                {
                    SendSMS(txtMobile.Text.Trim(), message, "1007931470339467866");
                    //Task<int> task = Execute(message, txtEmail.Text, " ERP || PassWord for Candidate");  
                }

                Showmessage(" OTP is verified.....And Your Username is forwarded to your registered Mobile NO. ");
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

        //if (rdoPassword.Checked == true)
        //{

        //    try
        //    {
        //        int status = 0;
        //        string Password = GeneartePassword();
        //        string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);


        //        //added by Tejas jaiswal 16/12/2021
        //        DataSet dsconfig = null;
        //        dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
        //        //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
        //        string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
        //        string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

        //        string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
        //        string message = "<b>Dear " + StudName + "," + "</b><br />";
        //        message += "Your PassWord for Login is :<b>" + Password + "<b>";
        //        message += "<br /><br /><br />Thank You<br />";
        //        message += "<br />Team " + SUBJECT_OTP + " <br />"; //added by Tejas Jaiswal
        //        message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

        //        if (txtEmail.Text != string.Empty)
        //        {
        //            try
        //            {



        //                if (Convert.ToInt32(SENDGRID_STATUS) == 1)
        //                {
        //                    //status = SendMailBYSendgrid(Message, txtEmailId.Text, "ERP || OTP for reset password");
        //                    //status = sendEmail(Message, txtEmailId.Text, "ERP || OTP for reset password");
        //                    Task<int> task = Execute(message, txtEmail.Text, SUBJECT_OTP + " ERP || OTP for reset password");
        //                    status = task.Result;

        //                }
        //                else
        //                {

        //                    status = sendEmail(message, txtEmail.Text, SUBJECT_OTP + " ERP || OTP for reset password");

        //                }
        //                if (status == 1)
        //                {


        //                    if (status == 1)
        //                    {
        //                        //Update sent mail password in database
        //                        if (objUC.ValidateResetPassword(useremail, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
        //                        {

        //                            //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
        //                            Showmessage("  OTP is verified.....And Your Password successfully reset and forwarded to your registered email id .");

        //                            txtEmail.Text = string.Empty;


        //                        }
        //                    }
        //                    else
        //                    {
        //                        Showmessage("Sorry, Your Application not configured with mail server, Please contact Admin Department !!");
        //                    }
        //                }
        //                else
        //                {
        //                    Showmessage("Failed to send email");
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                Showmessage("Failed to send email");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }

        //}
        //else
        {
            try
            {
                int status = 0;
                string username = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_EMAIL='" + useremail + "' ");


                DataSet dsconfig = null;
                dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
                string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

                string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
                string message = "<b>Dear " + StudName + "," + "</b><br />";
                message += "Your UserName for Login is :<b>" + username + "<b>";
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team " + SUBJECT_OTP + " <br />"; //added by Tejas Jaiswal
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //added by Tejas jaiswal 16/12/2021
                        if (Convert.ToInt32(SENDGRID_STATUS) == 1)
                        {
                            //status = SendMailBYSendgrid(Message, txtEmailId.Text, "ERP || OTP for reset password");
                            //status = sendEmail(Message, txtEmailId.Text, "ERP || OTP for reset password");
                            //Task<int> task = Execute(message, txtEmail.Text, SUBJECT_OTP + " ERP || User Name for Candidate");
                            //status = task.Result;

                            status = objSendEmail.SendEmail(txtEmail.Text, message, SUBJECT_OTP + " ERP || User Name for Candidate"); //Calling Method

                        }
                        else
                        {

                            //status = sendEmail(message, txtEmail.Text, SUBJECT_OTP + " ERP || User Name for Candidate");

                            status = objSendEmail.SendEmail(txtEmail.Text, message, SUBJECT_OTP + " ERP || User Name for Candidate"); //Calling Method

                        }
                        if (status == 1)
                        {
                            // if (objUC.ValidateResetPassword(useremail, username) == Convert.ToInt32((CustomStatus.ValidUser)))
                            {

                                //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
                                Showmessage(" OTP is verified.....And Your Username forwarded to your registered email id .");

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


    //protected void SendUsernamePassword()
    //{

    //    User_AccController objUC = new User_AccController();
    //    string useremail = txtEmail.Text.Trim().Replace("'", "");

    //    if (rdoPassword.Checked == true)
    //    {

    //        try
    //        {
    //            int status = 0;
    //            string Password = GeneartePassword();
    //            string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(Password);

    //            string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
    //            string message = "<b>Dear " + StudName + "," + "</b><br />";
    //            message += "Your PassWord for Login is :<b>" + Password + "<b>";
    //            message += "<br /><br /><br />Thank You<br />";
    //            message += "<br />Team ERP <br />";
    //            message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

    //            if (txtEmail.Text != string.Empty)
    //            {
    //                try
    //                {

    //                    //status = sendEmail(message, txtEmail.Text, "ERP || Password for Candidate");
    //                    //Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
    //                    //status = task.Result;
    //                    //string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
    //                    //if (issendgrid == "1")
    //                    //{
    //                    //    Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
    //                    //    status = task.Result;
    //                    //}
    //                    //else
    //                    //{
    //                    //    status = sendEmail(message, txtEmail.Text, "ERP || Password for Candidate");
    //                    //}
    //                    string email_type = string.Empty;
    //                    string Link = string.Empty;
    //                    int sendmail = 0;
    //                    DataSet ds = getModuleConfig();
    //                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
    //                        Link = ds.Tables[0].Rows[0]["LINK"].ToString();

    //                    }

    //                    //------------Code for sending email,It is optional---------------
    //                    // int status = sendEmail(message, useremail, subject);
    //                    //int reg = TransferToEmail(objS.EmailID, message, subject); //--tempoary Commented

    //                    if (email_type == "1" && email_type != "")
    //                    {
    //                        status = sendEmail(message, txtEmail.Text, "ERP || Password for Candidate");
    //                    }
    //                    else if (email_type == "2" && email_type != "")
    //                    {
    //                        Task<int> task = Execute(message, txtEmail.Text, "ERP || PassWord for Candidate");
    //                        status = task.Result;
    //                    }
    //                    if (email_type == "3" && email_type != "")
    //                    {
    //                        status = OutLook_Email(message, txtEmail.Text, "ERP || PassWord for Candidate");
    //                    }


    //                    if (status == 1)
    //                    {


    //                        if (status == 1)
    //                        {
    //                            //Update sent mail password in database
    //                            if (objUC.ValidateResetPassword(useremail, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
    //                            {

    //                                //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
    //                                Showmessage("  OTP is verified.....And Your Password successfully reset and forwarded to your registered email id .");

    //                                txtEmail.Text = string.Empty;


    //                            }
    //                        }
    //                        else
    //                        {
    //                            Showmessage("Sorry, Your Application not configured with mail server, Please contact Admin Department !!");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Showmessage("Failed to send email");
    //                    }
    //                }
    //                catch (Exception)
    //                {
    //                    Showmessage("Failed to send email");
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return;
    //        }

    //    }
    //    else
    //    {
    //        try
    //        {
    //            int status = 0;
    //            string username = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_EMAIL='" + useremail + "' ");

    //            string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
    //            string message = "<b>Dear " + StudName + "," + "</b><br />";
    //            message += "Your UserName for Login is :<b>" + username + "<b>";
    //            message += "<br /><br /><br />Thank You<br />";
    //            message += "<br />Team ERP<br />";
    //            message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

    //            if (txtEmail.Text != string.Empty)
    //            {
    //                try
    //                {
    //                    status = sendEmail(message, txtEmail.Text, "ERP || UserName for Candidate");
    //                    //Task<int> task = Execute(message, txtEmail.Text, "ERP ||UserName for Candidate");
    //                    //status = task.Result;
    //                    if (status == 1)
    //                    {
    //                        if (objUC.ValidateResetPassword(useremail, username) == Convert.ToInt32((CustomStatus.ValidUser)))
    //                        {

    //                            //* objCommon.DisplayMessage(updLog,"Your Password successfully reset and forwarded to your registered email id or Mobile No.", this.Page);
    //                            Showmessage(" OTP is verified.....And Your Username successfully reset and forwarded to your registered email id .");

    //                            txtEmail.Text = string.Empty;


    //                        }

    //                    }
    //                    else
    //                    {
    //                        Showmessage("Failed to send email");
    //                    }
    //                }
    //                catch (Exception)
    //                {
    //                    Showmessage("Failed to send email");
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return;
    //        }
    //    }
    //}


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
        bool val = ValidateSecurityEmail();

        if (val == false)
        {
            return;
        }
        string useremail = txtEmail.Text.Trim().Replace("'", "");
        string mail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_EMAIL='" + useremail + "' ");
        //added by Tejas jaiswal 16/12/2021
        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
        //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
        string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
        string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();
        if (mail == useremail && txtEmail.Text.Trim() != "")
        {
            lblotp2.Visible = true;
            txtOtp2.Visible = true;
            btnVerifyEmailOtp.Visible = true;
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
                message += "<br />Team " + SUBJECT_OTP + "<br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                if (txtEmail.Text != string.Empty)
                {
                    try
                    {
                        //status = SendMailBYSendgrid(Message, txtemailid.Text, "ERP || OTP for reset password");
                        //status = sendEmail(message, txtEmail.Text, "ERP || OTP for reset password");
                        //Task<int> task = Execute(message, txtEmail.Text, "ERP || OTP for reset password");
                        //status = task.Result;

                        status = objSendEmail.SendEmail(txtEmail.Text, message, SUBJECT_OTP + " ERP || OTP for reset password"); //Calling Method

                        if (status == 1)
                        {

                            //Added By Roshan Patil 19/12/2022  for Whatsaap kaleyra 
                            //start
                            DataSet dss = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME,UA_MOBILE", "UA_EMAIL='" + useremail.ToString() + "'", "");
                            if (dss.Tables[0].Rows.Count > 0 || dss.Tables[0].Rows.Count == null)
                            {
                                string Mobiles = "91" + dss.Tables[0].Rows[0]["UA_MOBILE"].ToString();
                                string Name = dss.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                                //WhatsappOtp(ViewState["EmailOTP"].ToString(), Mobiles, Name);
                            }
                            //end
                            //added by Tejas jaiswal 16/12/2021

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
        bool val = ValidateSecurityMobile();

        if (val == false)
        {
            return;
        }
        User_AccController useracc = new User_AccController();
        lblOtp.Visible = true;
        txtOtp.Visible = true;

        string Mobile = txtMobile.Text.Trim().Replace("'", "");
        string mobileNumber = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_MOBILE='" + Mobile + "' ");

        if (Mobile == mobileNumber && txtMobile.Text.Trim() != "")
        {

            lblOtp.Visible = true;
            txtOtp.Visible = true;
            btnMobOtpVerify.Visible = true;
            try
            {
                DataSet dsconfig = null;
                //dsconfig = objCommon.FillDropDown("REFF", "SMSProvider,SMSSVCID", "SMSSVCPWD", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));

                string otp = GenerateOTP(5);
                ViewState["OTP"] = otp;
                //string Url =  dsconfig.Tables[0].Rows[0]["SMSProvider"].ToString();  //"http://smsnmms.co.in/sms.aspx"; 
                //string UserId = dsconfig.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
                ////string Password = Common.DecryptPassword(Session["SMSSVCPWD"].ToString());//"iitmsTEST@5448";
                //string Password = dsconfig.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                //string MobileNo = "+91" + txtMobile.Text.Trim();
                //string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_MOBILE='" + Mobile + "' ");


                //string messageTemplate = "Dear User, Your OTP is " + otp + " to reset your password . OTP is valid for 15 Minutes or 1 Successful Attempt CRESEN";
                //string messageTemplate = "Your OTP is " + otp + " to reset your password . OTP is valid for 15 Minutes or 1 Successfull Attempt MSERP";


                //Added By Roshan Patil 19/12/2022  for Whatsaap kaleyra 
                //start
                DataSet dss = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME,UA_MOBILE", "UA_MOBILE='" + Mobile.ToString() + "'", "");
                if (dss.Tables[0].Rows.Count > 0 || dss.Tables[0].Rows.Count == null)
                {
                    string Mobiles = "91" + dss.Tables[0].Rows[0]["UA_MOBILE"].ToString();
                    string Name = dss.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    //WhatsappOtp(ViewState["OTP"].ToString(), Mobiles, Name);
                }
                // end


                string TemplateID = string.Empty;
                string TEMPLATE = string.Empty;
                string templatename = "Reset Password OTP";
                DataSet ds = useracc.GetSMSTemplate(0, templatename);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                    TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                }

                string messageTemplate = TEMPLATE;
                string OTP = otp;
                StringBuilder stringBuilder = new StringBuilder();
                messageTemplate = messageTemplate.Replace("{#var#}", otp);

                if (txtMobile.Text.Trim() != string.Empty)
                {
                    SendSMS(txtMobile.Text.Trim(), messageTemplate, TemplateID);
                }

                Showmessage("OTP has been send on Your Mobile No,Enter To Continue Reset Password Process.");
                // txtEmail.Text = string.Empty;
            }
            catch (Exception)
            {
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


    protected void rdoUsername_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoUsername.Checked == true)
        {
            pnlChoice.Visible = true;
            //rdoPassword.Visible = false;
            rdoMobile.Checked = false;
            rdoEmail.Checked = false;

            pnlMobile.Visible = false;
            pnlEmail.Visible = false;

            btnForgotPasswordMobile.Visible = false;
            btnSendUsernamePassword.Visible = false;

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

            btnForgotPasswordMobile.Visible = false;
            btnSendUsernamePassword.Visible = false;


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

            if (rdoUsername.Checked == true)
            {
                btnForgotPasswordMobile.Visible = false;
            }
        }
        else
        {
            pnlUserPass.Visible = false;

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

            if (rdoUsername.Checked == true)
            {
                btnSendUsernamePassword.Visible = false;
            }

        }
        else
        {
            pnlUserPass.Visible = false;
            pnlChoice.Visible = false;
            pnlEmail.Visible = false;
        }

    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(7));
        return ds;
    }

    private int OutLook_Email(string Message, string toEmailId, string sub)
    {

        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
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

    //added by tanu 01/11/2022
    protected void btnMobOtpVerify_Click(object sender, EventArgs e)
    {
        VerifyMobileOtp();
    }
    protected void btnVerifyEmailOtp_Click(object sender, EventArgs e)
    {
        VerifyEmailOtp();
    }
    protected void btnCancelp_Click(object sender, EventArgs e)
    {
        txtMobile.Text = string.Empty;
        lblVerify.Visible = false;
        lblVerify.Text = "";

        txtOtp.Text = string.Empty;
        btnMobOtpVerify.Visible = false;

        txtusername.Text = string.Empty;
        txtnewpass.Text = string.Empty;
        txtconfirmpass.Text = string.Empty;
        clear();
        Response.Redirect(Request.RawUrl);
    }
    protected void btnEmailCancel_Click(object sender, EventArgs e)
    {
        txtEmail.Text = string.Empty;
        lblVerify2.Visible = false;
        lblVerify2.Text = "";

        txtOtp2.Text = string.Empty;
        btnVerifyEmailOtp.Visible = false;

        txtEusername.Text = string.Empty;
        txtEnewpass.Text = string.Empty;
        txtEconfirmPass.Text = string.Empty;
        clear();
        Response.Redirect(Request.RawUrl);
    }

    protected void clear()
    {

        rdoUsername.Checked = false;
        rdoPassword.Checked = false;
        rdoEmail.Checked = false;
        rdoMobile.Checked = false;


        txtMobile.Text = string.Empty;
        btnSendotp.Visible = false;
        lblVerify.Visible = false;
        lblVerify.Text = "";


        txtOtp.Text = string.Empty;
        btnMobOtpVerify.Visible = false;

        txtusername.Text = string.Empty;
        txtnewpass.Text = string.Empty;
        txtconfirmpass.Text = string.Empty;
        btnForgotPasswordMobile.Visible = false;
        btnCancelp.Visible = false;

        txtEmail.Text = string.Empty;
        benotop2.Visible = false;
        lblVerify2.Visible = false;
        lblVerify2.Text = "";

        txtOtp2.Text = string.Empty;
        btnVerifyEmailOtp.Visible = false;

        txtEusername.Text = string.Empty;
        txtEnewpass.Text = string.Empty;
        txtEconfirmPass.Text = string.Empty;

        btnSendUsernamePassword.Visible = false;
        btnEmailCancel.Visible = false;

        pnlChoice.Visible = false;
        pnlMobile.Visible = false;
        pnlEmail.Visible = false;


    }

    protected void btnclosepop_Click(object sender, EventArgs e)
    {
        clear();
        Response.Redirect(Request.RawUrl);
    }
    protected void clsepop_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        Response.Redirect(Request.RawUrl);
    }

    // Added By Roshan Patil 19/12/2022  for Whatsaap kaleyra 
    protected void WhatsappOtp(string Otp, string ToMobileNo, string Name)
    {
        bool success = true;
        string Account_SID = "HXIN1700894763IN";
        string api_key = "A2a2b94ce1945b32e4eeb7e784aac9ac8";
        string API_URL = "https://api.kaleyra.io/v1/" + Account_SID.ToString() + "/messages?";
        try
        {
            string from = "919645081287";

            using (WebClient client = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //InfoData payloadObj = new InfoData() { to = "919503244325", from = from, type ="template", channel = "WhatsApp", template_name = "otperp", lang_code = "en", @params = "Roshan,253132" };

                string Para = '"' + Name.ToString() + '"' + "," + '"' + Otp.ToString() + '"';
                string myParamet = "from=" + from.ToString() + "&" + "to=" + ToMobileNo.ToString() + "&" + "type=template" + "&" + "channel=WhatsApp" + "&" + "params=" + Para.ToString() + "&template_name=otperp" + "&" + "lang_code=en";

                using (WebClient wc = new WebClient())
                {
                    // wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers["api-key"] = api_key.ToString();
                    wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(API_URL, myParamet);
                }
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            success = false;
        }

    }
}