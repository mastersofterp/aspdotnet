//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : GET THE DECRYPTED PASSWORD                                      
// CREATION DATE : 
// CREATED BY    : SHEETAL RAUT 
// MODIFIED BY   : ASHISH DHAKATE
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
using System.Net.Mail;
using System.Data.SqlClient;
 
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

using System.Net;
using System.IO;


public partial class getPassword : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Userpassword;
    PasswordController objPas = new PasswordController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
           // CheckPageAuthorization();
            //GetDetails();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
        }
    }
    
    protected void btnGetPassword_Click(object sender, EventArgs e)
    {
        try
        {

            GetDetails();

            //////string retPwd = objPc.GetPassword(ua_name);
           
            ////////Show Password
            //////if (!retPwd.Equals(string.Empty))
            //////{
            //////    lblpassword.Text = Common.DecryptPassword(retPwd);
            //////    lblStatus.Text = string.Empty;
            //////}
            //////else
            //////{
            //////    lblpassword.Text = string.Empty;
            //////    lblStatus.Text = "Username Invalid";
            //////}
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.btnGetPassword_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetDetails()
    {
         //show password 

            PasswordController objPc = new PasswordController();
            string ua_name = txtUserName.Text.Replace("'", "").Trim();
         //Add UA_TYPE for login user
            string ua_type = Session["usertype"].ToString();

            DataSet ds = objPc.GetPassword(ua_name,ua_type);
            if (ds != null && ds.Tables[0].Rows.Count==0)
            {
                objCommon.DisplayMessage(this.updPassword,"Username Invalid", this.Page);
                this.clear();
            }
            else 
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //trPwd.Visible = false;
                    lvGetStud.DataSource = ds;
                    lvGetStud.DataBind();

                    foreach (ListViewDataItem item in lvGetStud.Items)
                    {
                        Label lblUserpass = item.FindControl("lblUserpass") as Label;
                        //lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());
                    }
                    lvGetStud.Visible = true;
                }
                
            }
    }
    protected void rdobtn_OnCheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.clear();
    }

    private void clear()
    {
        txtUserName.Text = string.Empty;
        lblpassword.Text = string.Empty;
        lvGetStud.Visible = false;
        //trPwd.Visible = false;
        trResetPwd.Visible = false;
        //btnReSetPassword.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=getpassword.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    //Reset the user password
    protected void btnReSetPassword_Click(object sender, EventArgs e)
    {
           StudentAttendanceController dsStudentDetails = new StudentAttendanceController();
            PasswordController objPc = new PasswordController();
            string username = txtUserName.Text.Trim().Replace("'", "");

            UserAcc objuser = new UserAcc();

            foreach (ListViewDataItem item in lvGetStud.Items)
            {
                RadioButton rdb = item.FindControl("rdobtn") as RadioButton;
                Label lbl = item.FindControl("lblUaname") as Label;
                if (rdb.Checked == true)
                {
                    username = lbl.Text;

                    ViewState["username"] = username;
                }

            }

            if (ViewState["username"] != null)
            {

                //objPc.GetPassword(username);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                DataSet dsconfig = null;
                string uano = string.Empty;

                int userno = -1;
                uano = objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ViewState["username"] + "' AND UA_NAME IS NOT NULL");
                if (uano != "")
                {
                    userno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ViewState["username"] + "' AND UA_NAME IS NOT NULL"));
                    User_AccController objUC = new User_AccController();

                    //if (txtReSetPassword.Text.Trim() != string.Empty)
                    //{

                    //if (objUC.ValidateLogin(username, password, out userno) == Convert.ToInt32((CustomStatus.ValidUser)))
                    //{
                    UserAcc objUA = new UserAcc();
                    objUA.UA_Name = ViewState["username"].ToString();
                    objUA.UA_No = userno;
                    string pwd = GeneartePassword();
                  //  string encryptpwd = EncryptPassword(pwd);

                    string encryptpwd = pwd;
                    objUA.UA_Pwd = encryptpwd;
                    //objUA.UA_OldPwd = password;
                    string Mobileno = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_NAME='" + ViewState["username"] + "' and UA_NAME IS NOT NULL");
                    string emailid = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + ViewState["username"] + "' and UA_NAME IS NOT NULL");
                    CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadmin(objUA);

                    if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    {
                        //lblStatus.Text = "Invalid Old Password";
                        objCommon.DisplayMessage(this.updPassword, "Invalid Old Password", this.Page);
                    }
                    else
                    {
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {


                            objCommon.DisplayMessage(this.updPassword, "Password reset successfully & forwarded to registered EmailID and Mobile!", this.Page);

                            //***********************
                            ////dsStudentDetails.SENDMSG("Your password for indus university application has been reset successfully! Your usename is " + ViewState["username"].ToString() + " and  Password is " + pwd + "" + "", Mobileno);

                            if ((Mobileno != "" || Mobileno != null))
                            {
                                objCommon.SendSMS(Mobileno, "Your password for ERP application has been reset successfully! Your Usename : " + ViewState["username"].ToString() + " Password : " + pwd + "" + "");
                            }
                            //***********************

                            if ((emailid != "" || emailid != null))
                            {
                                //TO SEND MAIL TO THE STUDENT//
                                string subject = "MIS Login Credentials";
                                ////string Message = "Thanks for showing interest in Indus University. Your Login Credentials are <br />Username : <b>" + txtREGNo.Text.Trim() + "</b><br /> Password : <b>" + "" + ViewState["Otp"].ToString() + "" + "</b>";
                                string Message = "Your password for ERP application has been reset successfully! Your Usename : <b>" + ViewState["username"].ToString() + "</b>  Password : <b>" + "" + pwd + "" + "</b>";
                                objCommon.sendEmail(Message, emailid , subject);
                            }

                            ////if ((emailid != "" || emailid != null))
                            ////{
                            ////    dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
                            ////    if (dsconfig != null)
                            ////    {
                            ////        string emaili1 = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                            ////        string pwd1 = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();


                            ////        mail.From = new MailAddress(emaili1);
                            ////        string MailFrom = emaili1;
                            ////        SmtpServer.Port = 587;
                            ////        SmtpServer.Credentials = new System.Net.NetworkCredential(emaili1, pwd1);
                            ////        SmtpServer.EnableSsl = true;
                            ////        string aa = string.Empty;
                            ////        mail.Subject = "Login Password:";
                            ////        mail.To.Clear();
                            ////        mail.To.Add(emailid);

                            ////        mail.IsBodyHtml = true;
                            ////        mail.Body = "Your MZU E-Governance Application Password has been reset successfully! Your UserName is <B>" + ViewState["username"].ToString() + "</b>  Password is <b>" + "" + pwd + "" + "</b>";
                            ////        SmtpServer.Send(mail);
                            ////        ////if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                            ////        ////{

                            ////        ////    //Storing the details of sent email


                            ////        ////}
                            ////    }
                            ////}
                            txtReSetPassword.Text = string.Empty;
                            lblpassword.Text = string.Empty;
                            trResetPwd.Visible = false;

                            //GetDetails();

                        }
                    }

                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.updPassword, "Password reset can not be blank", this.Page);
                    //}
                    //}

                    //catch (Exception ex)
                    //{
                    //    if (Convert.ToBoolean(Session["error"]) == true)
                    //        lblStatus.Text = "Invalid Old Password";
                    //    else
                    //        lblStatus.Text = "Server UnAvailable";
                    //}
                }
                else
                {
                    objCommon.DisplayMessage(this.updPassword, "Not a valid Username!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updPassword, "Please Select User from the list!", this.Page);
            }
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ua_no = int.Parse(btnEdit.CommandArgument);
            ShowUserDetails(ua_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowUserDetails(int idno)
    {
        try
        {
            User_AccController objACC = new User_AccController();
            DataTableReader dtr;

            dtr = objACC.GetUserByUANo(idno);
            
                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        trResetPwd.Visible = true;
                        btnReSetPassword.Visible = true;

                        txtUserName.Text = dtr["UA_NAME"] == DBNull.Value ? string.Empty : dtr["UA_NAME"].ToString();
                        //hfUano.Value = dtr["UA_PWD"] == DBNull.Value ? string.Empty : dtr["UA_PWD"].ToString();                    
                    }
                }
           
                
           dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.ShowUserDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnUnblock_Click(object sender, EventArgs e)
    {
        UserAcc objuser = new UserAcc();
        int a = 0;
        foreach (ListViewDataItem item in lvGetStud.Items)
        {
            RadioButton rdb = item.FindControl("rdobtn") as RadioButton;
            if (rdb.Checked == true)
            {
                objuser.UA_No = Convert.ToInt32(rdb.ToolTip.ToString());

                string status = objCommon.LookUp("USER_ACC", "UA_STATUS", "UA_NO=" + Convert.ToInt32(rdb.ToolTip.ToString()));
                if (status == "1")
                {
                    objuser.UA_Status = 0;
                    a = objPas.UpdateBlockUser(objuser);
                    if (a > 0)
                    {
                        //ClientScript.RegisterStartupScript(updPassword.GetType(), "ALERT", "<script>alert('User Login has been blocked')</script>");
                        objCommon.DisplayMessage(this.updPassword, "User login has been Un blocked Successfully!!", this.Page);
                        //GetDetails();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updPassword, "User login Already Unblocked!!", this.Page);
                }
            }
        }
        if (a == 0)
        {
            objCommon.DisplayMessage(this.updPassword, "Please Select User from list!!", this.Page);
        }
    }
    protected void btnblock_Click(object sender, EventArgs e)
    { 
        UserAcc objuser=new UserAcc();
        int a = 0;
        foreach(ListViewDataItem item in lvGetStud.Items)
        {
            RadioButton rdb = item.FindControl("rdobtn") as RadioButton;
            if (rdb.Checked == true)
            {
                objuser.UA_No = Convert.ToInt32(rdb.ToolTip.ToString());
                string status = objCommon.LookUp("USER_ACC", "UA_STATUS", "UA_NO=" + Convert.ToInt32(rdb.ToolTip.ToString()));
                if (status == "0")
                {
                    objuser.UA_Status = 1;
                    a = objPas.UpdateBlockUser(objuser);
                    if (a > 0)
                    {
                        //ClientScript.RegisterStartupScript(updPassword.GetType(), "ALERT", "<script>alert('User Login has been blocked')</script>");
                        objCommon.DisplayMessage(this.updPassword, "User login has been blocked Successfully!!", this.Page);
                        //GetDetails();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updPassword, "User login Already Blocked!!", this.Page);
                }
            }

        }
        if (a == 0)
        {
            objCommon.DisplayMessage(this.updPassword, "Please Select User from list!!", this.Page);
        }
    }
    protected void Btnview_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text != "")
        {
            DataSet ds = objPas.GetUSER(txtUserName.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGetStud.DataSource = ds;
                lvGetStud.DataBind();
            }
            else
            {
                lvGetStud.DataSource = null;
                lvGetStud.DataBind();
                objCommon.DisplayMessage(this.updPassword, "Record Not found!!", this.Page);

            }
        }
        else
        {
            objCommon.DisplayMessage(this.updPassword, "Please Enter Username here!!", this.Page);
        }
    }
    protected void lvGetStud_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //RadioButton rdo= e.Item.FindControl("rdobtn") as RadioButton;
        //string uano = rdo.ToolTip.ToString();
        //string status = objCommon.LookUp("USER_ACC", "UA_STATUS", "UA_NO=" + Convert.ToInt32(uano));
        //if (rdo.Checked == true)
        //{
        //    if (status == "1")
        //    {
        //        btnblock.Enabled = false;
        //        btnUnblock.Enabled = true;
        //    }
        //    else
        //    {
        //        btnblock.Enabled = true;
        //        btnUnblock.Enabled = false;
        //    }
        //}
    }

    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.SendSMS-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
