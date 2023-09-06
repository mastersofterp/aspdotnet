using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp_MAKAUAT;
using SendGrid;

public partial class ACADEMIC_OnlineAdmUserCreate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Student objNS = new Student();
    StudentRegistration objSC = new StudentRegistration();
    string fees = string.Empty;
    string emailid = null;
    string uname = string.Empty;
    string pass = string.Empty;
    string emailId = string.Empty;
    int userno = -1;

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
        try
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
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    FillDropdown();
                    BindListView();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceReportByFaculty.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void FillDropdown()
    {
        objCommon.FillDropDownList(ddlAdmissionType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO != 3", "IDTYPENO");
        //objCommon.FillDropDownList("0", "Select City*", ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ipAddress = string.Empty, email = string.Empty, _mobileNo = string.Empty, admYear = string.Empty;
            int _catNo = 0;


            _mobileNo = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(MOBILENO,0)", "MOBILENO !='' AND MOBILENO='" + txtMobileNo.Text.Trim() + "'");
            email = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(EMAILID,0)", "EMAILID!='' AND EMAILID='" + txtEmailId.Text.Trim() + "'");
            if (_mobileNo.Equals(txtMobileNo.Text.Trim()))
            {
                txtMobileNo.Text = string.Empty;
                txtMobileNo.Focus();
                objCommon.DisplayMessage(this, "Mobile No. already exists !", this);
                return;
            }
            if (email.Equals(txtEmailId.Text.Trim()))
            {
                txtEmailId.Text = string.Empty;
                txtEmailId.Focus();
                objCommon.DisplayMessage(this, "Email Id already exists !", this);
                return;
            }

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_CONFIG", "DEGREENO", "CDBNO=" + ddlProgram.SelectedValue));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_CONFIG", "BRANCHNO", "CDBNO=" + ddlProgram.SelectedValue + " AND DEGREENO=" + degreeno));
            int ugpgot = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_CONFIG", "UGPGOT", "CDBNO=" + ddlProgram.SelectedValue + " AND DEGREENO=" + degreeno));
            if (ddlAdmissionType.SelectedValue == "2")
            {
                objNS.AdmType = 1;
            }
            else
            {
                objNS.AdmType = 0;
            }
            if (!txtStudName.Text.Trim().Equals(string.Empty)) objNS.StudName = txtStudName.Text.Trim();
            if (!txtMobileNo.Text.Trim().Equals(string.Empty)) objNS.StudentMobile = txtMobileNo.Text.Trim();
            if (!txtEmailId.Text.Trim().Equals(string.Empty)) objNS.EmailID = txtEmailId.Text.Trim();
            // if (!txtDOB.Text.Trim().Equals(string.Empty)) objNS.Dob = Convert.ToDateTime(txtDOB.Text.Trim());
            objNS.DegreeNo = Convert.ToInt32(degreeno);
            objNS.BranchNo = Convert.ToInt32(branchno);
            objNS.City = Convert.ToInt32(ddlCity.SelectedValue);
            objNS.PState = Convert.ToInt32(ddlState.SelectedValue);
            objNS.Ugpgot = ugpgot;
            objNS.Cdbno = Convert.ToInt32(ddlProgram.SelectedValue);
            // objNS.AdmissionType = Convert.ToInt32(ddlAdmissionType.SelectedValue);
            objNS.Fees = Convert.ToDouble(ViewState["Fees"]);

            //Session["uname"] = uname;
            //objNS.UserName = uname;

            pass = CreateRandomPassword(8);
            Session["password"] = pass;
            //uname = Common.EncryptPassword(uname);
            pass = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pass);
            objNS.Password = pass;

            Session["SSName"] = txtStudName.Text.Trim();
            emailId = txtEmailId.Text.Trim();
            Session["SMobNo"] = txtMobileNo.Text.Trim();

            ipAddress = GetIPAddress();
            objNS.Lock = 0;


            CustomStatus cs = (CustomStatus)objSC.Insert_Update_New_Student(objNS, ipAddress);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "MOBILENO=" + Session["SMobNo"].ToString() + "AND FIRSTNAME='" + Session["SSName"].ToString() + "' AND USER_PASSWORD ='" + pass + "'");
                Session["uname"] = UserName;

                if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()) && txtMobileNo.Text.Length == 10)
                {
                    SendSMSUser();
                }

                if (!string.IsNullOrEmpty(txtEmailId.Text.Trim()))
                {
                    //EMAIL send
                    //TransferToEmail(emailId);
                    SendMailBYSendgrid(emailId); //Added Mahesh on Dated 30/10/2019
                    //   lblMsgToUser.Text = "User ID and Password Sent to Registered email ID and Mobile";
                    objCommon.DisplayMessage(this, "Registration Successfully Completed ! Credentials sent to student email and mobile number.", this.Page);
                    BindListView();
                }


                ClearAllField();

            }
            else
            {
                objCommon.DisplayMessage(this, "Registration Not Completed, User Na/e Already Exists!!", this.Page);
            }

        }

        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);
            return;

        }
    }

    private void SendSMSUser()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");

            string Url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");//"http://smsnmms.co.in/sms.aspx";
            Session["url"] = Url;
            string UserId = ds.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
            Session["userid"] = UserId;

            string Password = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
            Session["pwd"] = Password;
            string MobileNo = "91" + txtMobileNo.Text.Trim();

            string Message = "Registration Successfully Completed at SBU-Online Admission Portal, UserID :" + Session["uname"] + " and Password : " + Session["password"];

            if (txtMobileNo.Text.Trim() != string.Empty)
            {
                //SendSMS(Url, UserId, Password, MobileNo, Message);
                SendSMS(MobileNo, Message); //Added Mahesh
            }

        }
        catch (Exception)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }

    }

    public int SendSMS(string Mobile, string text)
    {
        int result = 0;
        string user = "";
        string Password = "";
        string Msg = text;
        string sender = "ERPSMS";
        string MobileNumber = Mobile;
        string SmsURL = "";
        try
        {
            if (Mobile != string.Empty)
            {
                DataSet ds = objCommon.FillDropDown("Reff", "COMPANY_SMSSVCID", "COMPANY_SMSSVC_TOKEN,COMPANY_SMS_URL", "", "");

                user = ds.Tables[0].Rows[0]["COMPANY_SMSSVCID"].ToString();
                Password = ds.Tables[0].Rows[0]["COMPANY_SMSSVC_TOKEN"].ToString();
                SmsURL = ds.Tables[0].Rows[0]["COMPANY_SMS_URL"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    WebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + SmsURL + "?username=" + user + "&msg_token=" + Password + "&sender_id=" + sender + "&message=" + Msg + "&mobile=" + MobileNumber));
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string urlText = reader.ReadToEnd();
                    result = 1;
                    return result; //OK
                }
            }
        }
        catch (Exception ex)
        {
            result = -99;
            return result; //OK
        }
        return result;
    }

    // public bool SendMailBYSendgrid(string emailid, string CCemail)
    public bool SendMailBYSendgrid(string emailid)
    {
        bool ret = false;
        try
        {

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
            string fromAddress = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
            string user = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();
            string pwd = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
            string decrFromPwd = Common.DecryptPassword(pwd);
            //string decrFromPwd = pwd;
            var myMessage = new SendGridMessage();
            //If want to send attachment in email
            //if (data.attachment != null)
            //{
            //    MemoryStream stream = new MemoryStream(data.attachment);
            //    myMessage.AddAttachment(stream, data.fileName);
            //}

            myMessage.From = new MailAddress(fromAddress);
            myMessage.AddTo(emailid);
            myMessage.Subject = "SBU Admission || Registration Successfully Completed";


            using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_registration.html")))
            {
                myMessage.Html = reader.ReadToEnd();
            }

            myMessage.Html = myMessage.Html.Replace("{Name}", Session["SSName"].ToString());
            myMessage.Html = myMessage.Html.Replace("{UserName}", Session["uname"].ToString());
            myMessage.Html = myMessage.Html.Replace("{Password}", Session["password"].ToString());

            var credentials = new NetworkCredential(user, decrFromPwd);
            var transportWeb = new Web(credentials);
            transportWeb.Deliver(myMessage);
            return ret = true;
        }
        catch (Exception)
        {
            ret = false;
        }
        //return transportWeb.DeliverAsync(myMessage);
        return ret;
    }


    public static string CreateRandomPassword(int PasswordLength)
    {
        string _allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ"; //abcdefghijkmnopqrstuvwxyz
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlState.SelectedValue, "CITY");

    }
    protected void ddlAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmissionType.SelectedIndex == 2)
            {
                objCommon.FillDropDownList("0", "Select Program*", ddlProgram, "ACD_ADMISSION_CONFIG C INNER JOIN ACD_BRANCH B ON (C.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_DEGREE D ON (C.DEGREENO = D.DEGREENO)", "DISTINCT C.CDBNO", "(D.CODE + '-' + B.LONGNAME) AS BRANCHNAME", "GETDATE() BETWEEN CAST(CAST(ADMSTRDATE AS VARCHAR(10))+' '+REPLACE(STARTTIME,' ' ,'') AS DATETIME) AND CAST(CAST(ADMENDDATE AS VARCHAR(10))+' '+REPLACE(ENDTIME,' ' ,'') AS DATETIME) AND ISNULL(C.ADM_TYPE,0) = 1", "BRANCHNAME");
            }
            else
            {
                objCommon.FillDropDownList("0", "Select Program*", ddlProgram, "ACD_ADMISSION_CONFIG C INNER JOIN ACD_BRANCH B ON (C.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_DEGREE D ON (C.DEGREENO = D.DEGREENO)", "DISTINCT C.CDBNO", "(D.CODE + '-' + B.LONGNAME) AS BRANCHNAME", "GETDATE() BETWEEN CAST(CAST(ADMSTRDATE AS VARCHAR(10))+' '+REPLACE(STARTTIME,' ' ,'') AS DATETIME) AND CAST(CAST(ADMENDDATE AS VARCHAR(10))+' '+REPLACE(ENDTIME,' ' ,'') AS DATETIME)", "BRANCHNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        int batchno = Convert.ToInt32(objCommon.LookUp("ACD_ADMBATCH", "MAX(BATCHNO) AS BATCH", "BATCHNO=(SELECT (MAX(BATCHNO)) FROM ACD_ADMBATCH)"));

        fees = objCommon.LookUp("ACD_ADMISSION_CONFIG", "ISNULL(FEES,0) AS FEES", "ADMBATCH=" + batchno + "AND CDBNO=" + ddlProgram.SelectedValue);

        ViewState["Fees"] = fees;
    }

    [System.Web.Services.WebMethod]
    public static bool CheckEmail(string username)
    {
        bool status = false;
        string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("CheckEmailAvailability", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", username.Trim());
                conn.Open();
                status = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
        }
        return status;
    }

    [System.Web.Services.WebMethod]
    public static bool CheckMobile(string mobile)
    {
        bool status = false;
        string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("CheckMobileAvailability", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mobile", mobile.Trim());
                conn.Open();
                status = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
        }
        return status;
    }
    private void ClearAllField()
    {
        //txtCatNo.Text = string.Empty;
        txtStudName.Text = string.Empty;
        txtEmailId.Text = string.Empty;
        //txtDOB.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        ddlAdmissionType.SelectedIndex = -1;
        ddlProgram.SelectedIndex = -1;

        ddlState.SelectedIndex = -1;
        ddlCity.SelectedIndex = -1;

    }

    private void BindListView()
    {

        DataSet ds = null;

        ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "USERNAME", "FIRSTNAME,EMAILID,MOBILENO", "LOG_STATUS = 0", "USERNAME");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();

            }
        }
    }
}