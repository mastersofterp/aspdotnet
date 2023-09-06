//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : THIS IS A DEFAULT/LOGIN PAGE                                     
// CREATION DATE : 
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.NetworkInformation;
using System.Diagnostics ;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Mail;
using mastersofterp_MAKAUAT;

 
public partial class _default : System.Web.UI.Page
{
    //Common Class
    Common objCommon = new Common();
   // public string sMarquee = string.Empty;
    public string Notice = string.Empty;
    //Connection String
    //private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;  
    private string DirPath = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        //college name
        string colname = objCommon.LookUp("reff", "collegename", string.Empty);
        string coladdress = objCommon.LookUp("reff", "college_address", string.Empty);
        //string govt = objCommon.LookUp("reff", "govt", string.Empty);
        //if (objCommon.LookUp("REFF", "COLLEGE_LOGO", string.Empty) == null)
        //    imgLogo.ImageUrl = "~/images/logo.gif";
        //else
        //    imgLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";

        //if (string.IsNullOrEmpty(colname))
        //    spnHead.InnerText = "IT IS THE MASTERS SOFTWARE NAGPUR";
        //else
        //{
        //    Session["coll_name"] = colname;
        //    spnHead.InnerText = colname;
        //    SpnAddress.InnerText = coladdress;
           
        //}

        if (!Page.IsPostBack)
        {
            //Set Session Timeout
            Session.RemoveAll();

            try
            {
                UpdateCaptchaText();
                //Check ExpiryDate for the News/Notice
              
                //for news
              
                //Show scrolling news
                NewsController objNC = new NewsController();
                //sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
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
                        Session.Timeout = 60;

                        Page.Title = Session["coll_name"].ToString();
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
    
   

    private void UpdateCaptchaText()
    {
        txt_captcha .Text  = string.Empty;
        lblStatus.Visible = false;
        //Store the captcha text in session to validate
        Session["Captcha"] = Guid.NewGuid().ToString().Substring(0, 6);
    }





    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
           {
        bool success = false;
        if (Session["Captcha"] != null)
        {
            //Match captcha text entered by user and the one stored in session
            if (Convert.ToString(Session["Captcha"]) == txt_captcha.Text.Trim())
            {
                success = true;
            }
        }

        lblStatus.Visible = true;
        if (success)
        {

                 User_AccController objUC = new User_AccController();

                string username = txt_username.Text.Trim().Replace("'", "");
                string emailid = txt_emailid .Text.Trim().Replace("'", "");
                
                string pwd = GeneartePassword();
               // string encryptpwd = EncryptPassword(pwd);
                string encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);

                string useremail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");
                string usermobile = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");
                if (useremail == null)
                {
                    lblStatus.Text = "Sorry , Your email id or username not registered in system ,Please contact Admin Section!";
                    objCommon.DisplayMessage("Sorry , Your email id not registered in system ,Please contact Admin Section!", this.Page);
                    return;

                }
                if (useremail.ToString() == "")
                {
                    lblStatus.Text = "Sorry , Your email id or username not registered in system ,Please contact Admin Section!";
                    objCommon.DisplayMessage("Sorry , Your email id not registered in system ,Please contact Admin Section!", this.Page);
                    return;

                }
                if (emailid.ToString().Trim() != useremail.ToString().Trim())
                {
                    lblStatus.Text = "Sorry , Your email id or username not registered in system ,Please contact Admin Section!";
                    objCommon.DisplayMessage("Sorry , Your email id not registered in system ,Please contact Admin Section!", this.Page);
                    return;

                }
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                DataSet dsconfig = null;
                dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
                if (dsconfig != null)
                {
                    string emaili1 = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                    string pwd1 = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();


                    mail.From = new MailAddress(emaili1);
                    string MailFrom = emaili1;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emaili1, pwd1);
                    SmtpServer.EnableSsl = true;
                    string aa = string.Empty;
                    mail.Subject = "MIS MIZORAM-UNIVERSITY-Login Credentials:";
                    mail.To.Clear();
                    mail.To.Add(emailid);
                    mail.IsBodyHtml = true;
                    mail.Body = "Your MIS MIZORAM-UNIVERSITY Password has been reset successfully!Your new Login Password is <b>" + "" + pwd + "" + "</b>";
                    SmtpServer.Send(mail);
                    if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                    {

                        //Storing the details of sent email


                    }

                    if (objUC.ValidateResetPassword(username, emailid, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
                    {


                        lblStatus.Text = "Your password successfully reset and forworded to your email id or mobile number";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        objCommon.DisplayMessage("Your Password successfully reset and forworded to your email id", this.Page);
                        txt_username.Text = string.Empty;
                        txt_emailid.Text = string.Empty;
                        txt_captcha.Text = string.Empty;
                    }
                    else
                    {
                        lblStatus.Text = "Sorry , Your email id or username not registered in system ,Please contact Admin Section!";
                        objCommon.DisplayMessage("Sorry , Your email id not registered in system ,Please contact Admin Section!", this.Page);
                    }
                }
            // Code for sending SMS,It is optional
                if (objUC.ValidateResetPassword(username, emailid, encryptpwd) == Convert.ToInt32((CustomStatus.ValidUser)))
                {
                    if (usermobile != null)
                    {
                        if (usermobile.ToString() != "")
                        {
                            StudentAttendanceController dsStudentDetails = new StudentAttendanceController();
                            dsStudentDetails.SENDMSG("Your MIS MIZORAM-UNIVERSITY Password has been reset successfully!Your new Login Password is " + "" + pwd + "" + "", usermobile);
                        }
                    }
                }

        }
        else
        {
            lblStatus.Text = "captcha charactor not match";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
      
            }
            catch (Exception ex)
            {
                objCommon.DisplayMessage("Error...", this.Page);
            }
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

    /// <summary>
    /// Decrypts the Password
    /// </summary>
    /// <param name="password">Decrypt password in the form of string as per this password</param>
    /// <returns>Return decrypted password in the form of string</returns>
    public static string DecryptPassword(string password)
    {
        string mchar = string.Empty;
        string pvalue = string.Empty;

        for (int i = 1; i <= password.Length; i++)
        {
            mchar = password.Substring(i - 1, 1);
            byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
            int no = int.Parse(bt[0].ToString());
            char ch = Convert.ToChar(no - i);
            pvalue += ch.ToString();
        }
        return pvalue;
    }
    private string GeneartePassword()
    {
        string allowedChars = "";
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
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

        //-----------------OR---------------------
        //Random randNum = new Random();
        //int PasswordLength = 10;
        //char[] chars = new char[PasswordLength];
        //int allowedCharCount = allowedChars.Length;
        //for (int i = 0; i < PasswordLength; i++)
        //{
        //    chars[i] = allowedChars[(int)((allowedChars.Length) * randNum.NextDouble())];
        //}
        //return new string(chars);
        //--------------------------------------
    }


    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        UpdateCaptchaText();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
}
