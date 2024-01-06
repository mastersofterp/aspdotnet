//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// PAGE NAME     : BULK USER CREATION OF EMPLOYEES                                  
// CREATION DATE : 19-Aug-2009                                                     
// CREATED BY    :  G.V.S.KIRAN KUMAR                                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using mastersofterp_MAKAUAT;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Linq;
using BusinessLogicLayer.BusinessLogic;
using ClosedXML.Excel;
using System;
using System.Web.UI;
using System.Globalization;


public partial class ADMINISTRATION_Bulk_User_Id_Creation_Employees : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    PayMaster objPayMas = new PayMaster();
    PayHeadPrivilegesController objPayHeadController = new PayHeadPrivilegesController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); 
    //ConnectionString
   // private string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
            //Populate DropDownLists
            lblmessage.Text = "";
            PopulateDropDown();
            PopulateDropDown1(); //26-09-2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
            PopulateDropDown3(); //17-12-2022 
            //PopulateDropDownList();
            objCommon.FillDropDownList(DropDownList1, "User_Rights", "usertypeid", "userdesc", "usertypeid > 0 and usertypeid in (3,4,5)", "usertypeid DESC");

       


        }

       // divMsg.InnerHtml = string.Empty;

        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEmployeeType.SelectedIndex = 0;
        lblmessage.Text = "";
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
 {
        int id = 0;
        int count = 0;
        try
        {
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();
            //PayController objpay = new PayController();

            DataSet ds = objACC.GetEmployeeForUserCreation(Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue));
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                     foreach (ListViewDataItem itm in lvStudents.Items)
                    {
                        CheckBox chk = itm.FindControl("chkRow") as CheckBox;
                        Label lblreg = itm.FindControl("lblreg") as Label;
                        HiddenField hdnf = itm.FindControl("hidStudentId") as HiddenField;
                        Label lblstud = itm.FindControl("lblstud") as Label;

                        if (chk.Checked == true && (chk.Enabled == true))
                        {
                            //objUA.UA_IDNo = Convert.ToInt32(dtr["IDNO"]);
                            objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                            id = objUA.UA_IDNo;
                            //objUA.UA_Name = Convert.ToString(dtr["PFILENO"]);
                            objUA.UA_Name = lblreg.Text;
                            //dtr["pfileno"].ToString();
                            //string[] name = dtr["NAME"].ToString().Split(' ');
                            string pwd = string.Empty;

                            pwd = lblreg.Text;
                            objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                            // objUA.UA_Pwd = Common.EncryptPassword(pwd);
                            // objUA.UA_FullName = dtr["NAME"].ToString();
                            objUA.UA_FullName = lblstud.Text;
                            objUA.UA_Status = 0;
                            objUA.UA_Type = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                            //objUA.UA_DeptNo = Convert.ToInt32(dtr["subdeptno"]);
                            string deptno = objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + hdnf.Value +"");
                            objUA.UA_DeptNo = deptno;
                            objACC.AddEmployeeUser(objUA);

                        }
                        if (chk.Checked == true)//chk.Enabled == true && 
                        {
                            count++;
                        }
                    }
                    // ShowMessage("Login Created Successfully");
                }
            }
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username and Password Send on your Registered Email ID !');", true);
              //  ShowMessage("Login Created Successfully");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(Login Created Successfully');", true);
                lblmessage.Text = "Login Created Successfully";
               // string display = "Login Created Successfully";
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO > 0", "STAFFNO ASC");
            objCommon.FillDropDownList(ddlEmployeeType, "User_Rights", "usertypeid", "userdesc", "usertypeid > 0 and usertypeid in (3,4,5)", "usertypeid DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        PayController objpay = new PayController();
        DataSet ds = objpay.GetEmployeeForUserCreationCommon(Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue));


       // DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "CASE WHEN PFILENO IS NULL THEN '-' ELSE PFILENO END PFILENO,FNAME+' '+MNAME+' '+LNAME AS NAME,ISNULL(LOGIN_STATUS,0) AS LOGIN_STATUS", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STAFFNO=" + Convert.ToInt32(ddlStaff.SelectedValue) + " AND UA_TYPE=" + Convert.ToInt32(ddlEmployeeType.SelectedValue), "IDNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds.Tables[0];
            lvStudents.DataBind();
            Label lblh1 = lvStudents.FindControl("lblEmpCode") as Label; lblh1.Text = ds.Tables[0].Rows[0]["ColHeading"].ToString();
            //pnllistview.Visible = true;
            lvStudents.Visible = true;
            btnUpdate.Enabled = true;

        }
        else
        {
            objCommon.DisplayMessage("No record found!", this.Page);
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            //pnllistview.Visible = false;
            lvStudents.Visible = false;
            btnUpdate.Enabled = false;

        }
    }

    // 1 tab code 


    private void PopulateDropDown1()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege1, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff2, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO > 0", "STAFFNO ASC");
            objCommon.FillDropDownList(ddlEmployeeType3, "User_Rights", "usertypeid", "userdesc", "usertypeid > 0 and usertypeid in (3,4,5)", "usertypeid DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnShow1_Click(object sender, EventArgs e)
    {
         int id = 0;
        int count = 0;
        try
        {
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();
            PayController objpay = new PayController();

            DataSet ds = objpay.GetEmployeeForUserEmailSend(Convert.ToInt32(ddlEmployeeType3.SelectedValue), Convert.ToInt32(ddlCollege1.SelectedValue), Convert.ToInt32(ddlStaff2.SelectedValue));

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ListView1.Visible = true;
                    ListView1.DataSource = ds.Tables[0];
                    ListView1.DataBind();
                    Label lblh1 = ListView1.FindControl("lblEmpCode1") as Label; 
                    lblh1.Text = ds.Tables[0].Rows[0]["ColHeading"].ToString();
                    //DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    //foreach (ListViewDataItem itm in ListView1.Items)
                    //{
                    //    CheckBox chk = itm.FindControl("chkRow") as CheckBox;
                    //    Label lblreg = itm.FindControl("lblreg") as Label;
                    //    HiddenField hdnf = itm.FindControl("hidStudentId") as HiddenField;
                    //    Label lblstud = itm.FindControl("lblstud") as Label;
                    //    Label lblMobileNo = itm.FindControl("STUDENTMOBILE") as Label;
                    //    Label lblEmailId = itm.FindControl("EMAILID") as Label;

                    //}
                }
            }

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        // Send autogenerate password to email id and molile no.
        string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
        string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
        //string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
       // string loginurl = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ADMINISTRATION")));
        string loginurl = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ADMINISTRATION")));


        foreach (ListViewDataItem item in ListView1.Items)
        {
            System.Web.UI.WebControls.CheckBox chk = item.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
            //System.Web.UI.WebControls.Label lblLogin = item.FindControl("lblLogin") as System.Web.UI.WebControls.Label;

            if (chk.Checked == true)      // && (chk.Text == "CREATED") && lblLogin.Text == "")
            {

                System.Web.UI.WebControls.Label lblreg = item.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblstud = item.FindControl("lblNAME") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblEmailId = item.FindControl("lblEmailId") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lbluaname = item.FindControl("lbluaname") as System.Web.UI.WebControls.Label;

                System.Web.UI.WebControls.HiddenField hduano = item.FindControl("hdnUaNo") as System.Web.UI.WebControls.HiddenField;

                //  System.Web.UI.WebControls.Label lblPwd = item.FindControl("lblreg") as System.Web.UI.WebControls.Label;

                //string useremail = objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "a.EMAILID", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL");
                string useremail = lblEmailId.Text;
                // string useremail = "anmolsawarkar@gmail.com";
                // string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + lbluaname.Text + "'");
                string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NO='" + hduano.Value + "'");
                if (getpwd != "")
                {
                    string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);

                    //string message = "Your MIS Student Account has been create successfully! Login with Username : " + lblreg.Text + " Password : " + "" + lblPwd.Text + "" + "</b>";
                    string message = "Dear " + lblstud.Text + ",<br /> <br />";
                    message = message + "Greetings of the day!<br /> <br />";
                    message = message + "Your NEW ERP Account has been created successfully! <br /> <br />";
                    message = message + "Please Login using following details <br />";
                    message = message + "User Name : " + lbluaname.Text + "<br />";// Updated on 14092023
                    // message = message + "User Name : " + lblreg.Text + "<br />";  // old
                    //message = message + "Password : " + lblreg.Text + "<br />";
                    message = message + "Password : " + strPwd + "<br /> <br />";
                    message = message + "click  " + loginurl + " here to Login";

                    string subject = CodeStandard + " ERP || Password for Login";// "MIS Login Credentials";

                    //------------Code for sending email,It is optional---------------
                    int status = 0;

                    //if (issendgrid == "1")
                    //{
                        //Task<int> ret = Execute(message, useremail, subject);
                        //status = ret.Result;
                        status = objSendEmail.SendEmail(useremail, message, subject);
                    //}
                    //else
                    //{
                        //status = sendEmail(message, useremail, CodeStandard + " ERP || PassWord for Login");
                    //}


                    //int statuss = objCommon.sendEmail(message, useremail, subject);


                    if (status == 1)
                    {
                        //  objCommon.DisplayMessage(upduser, "User Added Successfully ,Username and Password Send on your Registered Email ID !", this.Page);
                        //string display="Username and Password Send on your Registered Email ID";
                        //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username and Password Send on your Registered Email ID !');", true);
                    }
                    else
                    {
                        //lblSubmitStatus.Text = "Sorry, Your Application not configured with mail server,Please contact Admin Department !!";
                        //lblSubmitStatus.ForeColor = System.Drawing.Color.Red;
                        //string display = "Sorry, Your Application not configured with mail server,Please contact Admin Department !!";
                        //objCommon.DisplayMessage(upduser, "Sorry, Your Application not configured with mail server,Please contact Admin Department !!", this.Page);
                        //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true); 
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry, Your Application not configured with mail server,Please contact Admin Department !!');", true);
                    }

                    //send sms to user
                    //string Mobileno = objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "STUDENTMOBILE", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL");
                    //if (Mobileno != "")
                    //{
                    //    objCommon.SendSMS(Mobileno, "Your MIS Account has been create successfully! Login with Username : " + lblreg.Text + "  Password : " + "" + lblStudName.ToolTip + "" + "");

                    //}
                }
                else
                {

                }
            }

        }
       // objCommon.DisplayMessage(updpnl, "Username and Password Send on your Registered Email ID !", this.Page);
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
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlCollege1.SelectedIndex = 0;
        ddlStaff2.SelectedIndex = 0;
        ddlEmployeeType3.SelectedIndex = 0;

    }

    //tab3

       protected void btnCancel3_Click(object sender, EventArgs e)
        {
           
            Panel3.Visible = false;
        }

       
        private void PopulateDropDown3()
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }


        //private void ShowMessage(string message)
        //{
        //    if (message != string.Empty)
        //    {
        //        divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        //    }
        //}
 

        #region CreateLogin


        protected void PopulateDropDownList()
        {
            try
            {
              //  objCommon.FillDropDownList(ddlStudAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlEmployeeType3, "User_Rights", "usertypeid", "userdesc", "usertypeid > 0 and usertypeid in (3,4,5)", "usertypeid DESC");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Uploaddata()
        {
            try
            {
                if (FileUpload2.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload2.PostedFile.FileName);
                    if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                    {
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        string FilePath = Server.MapPath(FolderPath + FileName);
                        FileUpload2.SaveAs(FilePath);
                        ExcelToDatabase(FilePath, Extension, "yes");
                        divCount.Visible = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Only .xls or .xlsx extention is allowed", this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Please select the Excel File to Upload", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                //if (Convert.ToBoolean(Session["error"]) == true)
                //    objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
                //else
                //    objUCommon.ShowError(Page, "Server UnAvailable");
                objCommon.DisplayMessage(updpnl, "Cannot access the file. Please try again.", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                return;
            }
        }

        private bool ContainsSpace(string input)
        {
            // Check if the string contains a space
            return input.Contains(" ");
        }

        private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
        {

     
            CustomStatus cs = new CustomStatus();
            string conStr = "";
         
            switch (Extension)
            {
                    
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;

            }

            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            try
            {

                System.Data.DataTable dt = new System.Data.DataTable();
                cmdExcel.Connection = connExcel;
                //Get the name of First Sheet

                connExcel.Open();
                System.Data.DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                

                string SheetName = dtExcelSchema.Rows[3]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                //Bind Excel to GridView
                DataSet ds = new DataSet();
                oda.Fill(ds);

                DataView dv1 = dt.DefaultView;


                //System.Data.DataTable dtNew = ds.Tables[0];
                //dtNew = dt.Rows.Cast<DataRow>()
                //    .Where(row => !row.ItemArray.All
                //        (f => f is DBNull || string.IsNullOrEmpty(f as string ?? f.ToString())))
                //        .CopyToDataTable();


                System.Data.DataTable dtNew = ds.Tables[0];
                dtNew = dt.Rows
                 .Cast<DataRow>()
                 .Where(row => !row.ItemArray.All(f => f is DBNull ||
                                  string.IsNullOrEmpty(f as string ?? f.ToString())))
                 .CopyToDataTable();


                //dtNew = dt.Rows
                //        .Cast<DataRow>()
                //         .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string)))
                //        .CopyToDataTable();

                lvEmployee.DataSource = dtNew; 
                lvEmployee.DataBind();
             
                lvEmployee.Visible = true;

                int i = 0;
                int count = 0;


                System.Data.DataTable dt1 = new System.Data.DataTable();
                DataRow dr = null;
                dt1.Columns.Add(new DataColumn("RowId", typeof(string)));
                dt1.Columns.Add(new DataColumn("Description", typeof(string)));

                System.Data.DataTable dt2 = new System.Data.DataTable();
                DataRow dr1 = null;
                dt2.Columns.Add(new DataColumn("RowId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Description", typeof(string)));

                bool IsErrorInUpload = false;
                string ErrorString = string.Empty;
                string message = string.Empty;
                string ErrorString1 = string.Empty;
                string messageexp = string.Empty;
                int RowNum = 0;
                int TotalRecordCount = 0;
                int TotalRecordUploadCount = 0;
                int TotalAlreadyExistsCount = 0;
                int TotalRecordErrorCount = 0;
                string RecordExist = string.Empty;
                divRecords.Visible = true;
                divtotcount.Visible = true;
                divrecupload.Visible = true;
                divrecexist.Visible = true;
                divErrorNote.Visible = true;
                divRecwitherror.Visible = true;
                lblTotalRecordCount.Text = TotalRecordCount.ToString();
                lblTotalRecordUploadCount.Text = TotalRecordUploadCount.ToString();
                lblTotalAlreadyExistsCount.Text = TotalAlreadyExistsCount.ToString();
                lblTotalRecordErrorCount.Text = TotalRecordErrorCount.ToString();

                lblValue.Text = count.ToString();
                lblValue.Text = TotalRecordCount.ToString();
                TotalRecordCount = dtNew.Rows.Count;
                //{
                //    DataTable dt = new DataTable();
                //    DataRow dr = null;
                //    dt.Columns.Add(new DataColumn("Row", typeof(string)));
                //    dt.Columns.Add(new DataColumn("Description", typeof(string)));
                //}

                ////-----start date check 05-01-2024

                //System.Data.DataTable dtdate = new System.Data.DataTable();
                //DataRow datedr = null;
                //dtdate.Columns.Add(new DataColumn("RowId", typeof(string)));
                //dtdate.Columns.Add(new DataColumn("DOB", typeof(string)));
                //dtdate.Columns.Add(new DataColumn("DOJ", typeof(string)));
                //dtdate.Columns.Add(new DataColumn("DOI", typeof(string)));
                //dtdate.Columns.Add(new DataColumn("DOR", typeof(string)));
                //for (i = 0; i < dtNew.Rows.Count; i++)
                //{

                //    datedr = dtdate.NewRow();
                //    datedr["RowId"] = (i + 1).ToString();
                //    datedr["DOB"] = dtNew.Rows[i]["Date of Birth"].ToString(); ;
                //    datedr["DOJ"] = dtNew.Rows[i]["Date of Joining"].ToString(); ;
                //    datedr["DOI"] = dtNew.Rows[i]["Date of Increment"].ToString(); ;
                //    datedr["DOR"] = dtNew.Rows[i]["Date of Retirement"].ToString(); ;
                //    dtdate.Rows.Add(datedr);
                //    objPayMas.DateValidations = dtdate;
                //}
                //DataSet dsdate = objPayHeadController.GetDateValidation(objPayMas);
               
                ////-----end date check 05-01-2024

                for (i = 0; i < dtNew.Rows.Count; i++)
                {
                    ErrorString = string.Empty;
                  
                    RowNum = RowNum + 1;
                    ErrorString = ErrorString + Environment.NewLine + "Row : " + RowNum.ToString() + " - ";
                        IsErrorInUpload = false;
                        DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                        object Regno = row[0];


                        DataSet ds1 = null;
                        string empId = dtNew.Rows[i]["Employee Id"].ToString();
                        ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "EmployeeId='" + empId + "'", "");
                   
                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count>0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{

                            message = "<span style='color:Red'><b> Employee Id Already Exists.</b></span>";
                                messageexp = " Employee Id Already Exists.";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;
                            //}
                        }
                        string SchoolName = dtNew.Rows[i]["College Name"].ToString();
                        ds1 = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "*", "COLLEGE_NAME", "COLLEGE_NAME='" + SchoolName + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee College Name is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee College Name is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                        string str = dtNew.Rows[i]["Mobile No"].ToString();
                        
                        //str = Regex.Replace(str, @"\s", "");
                        //string mobNo = str;
                        
                        //str.Any(Char.IsWhiteSpace);
                        //string mobNo = str;
                 //   Check for spaces in the string
                        if (ContainsSpace(str))
                    {
                        // Register JavaScript alert if space is found
                       // string script = "alert('Space found in the string!');";
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "SpaceAlert", script, true);

                        message = "<span style='color:Red'><b>Space found in the Mobile no.</b></span>";
                        messageexp = "Mobile no Is Already Exists.";
                        ErrorString = ErrorString + message + " | ";
                        ErrorString1 = ErrorString1 + messageexp + " | ";
                        IsErrorInUpload = true;
                      }
                else
                {
                    string mobNo = str;
                        ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "PHONENO='" + mobNo + "'", "");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            message = "<span style='color:Red'><b> Mobile no Is Already Exists.</b></span>";
                            messageexp = "Mobile no Is Already Exists.";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;
                         }
                }
                     string EmailId = dtNew.Rows[i]["E-mail ID"].ToString();
                     if (ContainsSpace(EmailId))
                     {
                         // Register JavaScript alert if space is found
                         // string script = "alert('Space found in the string!');";
                         // ScriptManager.RegisterStartupScript(this, this.GetType(), "SpaceAlert", script, true);

                         message = "<span style='color:Red'><b>Space found in the E mail Id.</b></span>";
                         messageexp = "Space found in the E mail Id.";
                         ErrorString = ErrorString + message + " | ";
                         ErrorString1 = ErrorString1 + messageexp + " | ";
                         IsErrorInUpload = true;
                     }
                     ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "EMAILID='" + EmailId + "'", "");
                     if (ds1.Tables[0].Rows.Count > 0)
                     {
                         message = "<span style='color:Red'> <b>Email Id Is Already Exists.</b></span>";
                         messageexp = "Email Id Is Already Exists.";
                         ErrorString = ErrorString + message + " | ";
                         ErrorString1 = ErrorString1 + messageexp + " | ";
                         IsErrorInUpload = true;
                     }

                     string Fname = dtNew.Rows[i]["First Name"].ToString();
                     string Lname = dtNew.Rows[i]["Last Name"].ToString();

                   
                    //------------start-----------
                    
                     //    string DOB = string.Empty;
                     //    string DOB1 = dtNew.Rows[i]["Date of Birth"].ToString();
                     //    //string DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                     //     DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                     //    ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "FNAME='" + Fname + "'and LNAME='" + Lname + "' and DOB='" + DOB + "'", "");
                     //    if (ds1.Tables[0].Rows.Count > 0)
                     //    {
                     //        message = "<span style='color:Red'><b> FName LName and Date Of Birth Is Already Exists.</b></span>";
                     //        messageexp = "FName LName and Date Of Birth Is Already Exists.";
                     //        ErrorString = ErrorString + message + " | ";
                     //        ErrorString1 = ErrorString1 + messageexp + " | ";
                     //        IsErrorInUpload = true;
                     //    }

                     //    //string DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                     //     DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                     //    ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "FNAME='" + Fname + "'and LNAME='" + Lname + "' and DOB='" + DOB + "'", "");
                     //    if (ds1.Tables[0].Rows.Count > 0)
                     //    {
                     //        message = "<span style='color:Red'><b> FName LName and Date Of Birth Is Already Exists.</b></span>";
                     //        messageexp = "FName LName and Date Of Birth Is Already Exists.";
                     //        ErrorString = ErrorString + message + " | ";
                     //        ErrorString1 = ErrorString1 + messageexp + " | ";
                     //        IsErrorInUpload = true;
                     //    }

                     if (!(dtNew.Rows[i]["Date of Birth"]).ToString().Equals(string.Empty))
                     {
                         objPayMas.RegNo = dtNew.Rows[i]["Date of Birth"].ToString();
                         string datedob = dtNew.Rows[i]["Date of Birth"].ToString();
                         DateTime date1;
                         if (DateTime.TryParseExact(datedob, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date1))
                         {

                          
                             //string DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                             string DOB = Convert.ToDateTime(datedob).ToString("dd/MM/yyyy");
                             ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "FNAME='" + Fname + "'and LNAME='" + Lname + "' and DOB='" + DOB + "'", "");
                             if (ds1.Tables[0].Rows.Count > 0)
                             {
                                 message = "<span style='color:Red'><b> FName LName and Date Of Birth Is Already Exists.</b></span>";
                                 messageexp = "FName LName and Date Of Birth Is Already Exists.";
                                 ErrorString = ErrorString + message + " | ";
                                 ErrorString1 = ErrorString1 + messageexp + " | ";
                                 IsErrorInUpload = true;
                             }
                         }
                         //else if (!(dtNew.Rows[i]["Date of Birth"]).ToString().Equals(string.Empty))
                         //{
                         //    objPayMas.RegNo = dtNew.Rows[i]["Date of Birth"].ToString();
                             //string datedob = dtNew.Rows[i]["Date of Birth"].ToString();
                             //DateTime date1;
                         else   if (DateTime.TryParseExact(datedob, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date1))
                             {

                               


                                 //string DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                                 string DOB = Convert.ToDateTime(datedob).ToString("dd/MM/yyyy");
                                 ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "FNAME='" + Fname + "'and LNAME='" + Lname + "' and DOB='" + DOB + "'", "");
                                 if (ds1.Tables[0].Rows.Count > 0)
                                 {
                                     message = "<span style='color:Red'><b> FName LName and Date Of Birth Is Already Exists.</b></span>";
                                     messageexp = "FName LName and Date Of Birth Is Already Exists.";
                                     ErrorString = ErrorString + message + " | ";
                                     ErrorString1 = ErrorString1 + messageexp + " | ";
                                     IsErrorInUpload = true;
                                 }
                             }
                        // }
                         else
                         {
                             message = "<span style='color:Red'><b> Date Of Birth is Invalid date [dd MM yyyy] format .</b></span>";
                             messageexp = "Date Of Birth is Invalid date format .";
                             ErrorString = ErrorString + message + " | ";
                             ErrorString1 = ErrorString1 + messageexp + " | ";
                             IsErrorInUpload = true;
                         }

                }
                     else
                     {
                         message = "<span style='color:Red'><b>Please enter Date of Birth </b></span>";
                         messageexp = "Please enter Date of Birth ";
                         ErrorString = ErrorString + message + " | ";
                         ErrorString1 = ErrorString1 + messageexp + " | ";
                         IsErrorInUpload = true;
                     }

                     if (!(dtNew.Rows[i]["Date of Joining"]).ToString().Equals(string.Empty))
                     {
                         objPayMas.RegNo = dtNew.Rows[i]["Date of Joining"].ToString();
                         string datedoj = dtNew.Rows[i]["Date of Joining"].ToString();
                         DateTime date2;
                         if (DateTime.TryParseExact(datedoj, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date2))
                         {

                         }
                         else if (DateTime.TryParseExact(datedoj, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date2))
                         {

                         }
                         else
                         {
                             message = "<span style='color:Red'><b> Date Of Joining is Invalid date [dd MM yyyy] format .</b></span>";
                             messageexp = "Date Of Birth is Invalid date format .";
                             ErrorString = ErrorString + message + " | ";
                             ErrorString1 = ErrorString1 + messageexp + " | ";
                             IsErrorInUpload = true;
                         }

                     }
                     else
                     {
                         message = "<span style='color:Red'><b>Please enter Date of Joining</b> </span>";
                         messageexp = "Please enter Date of Joining";
                         ErrorString = ErrorString + message + " | ";
                         ErrorString1 = ErrorString1 + messageexp + " | ";
                         IsErrorInUpload = true;
                     }


                     if (!(dtNew.Rows[i]["Date of Retirement"]).ToString().Equals(string.Empty))
                     {
                         string datedor = dtNew.Rows[i]["Date of Retirement"].ToString();
                         DateTime date3;
                         if (DateTime.TryParseExact(datedor, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date3))
                         {

                         }
                         else if (DateTime.TryParseExact(datedor, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date3))
                         {

                         }
                         else
                         {
                             message = "<span style='color:Red'><b> Date Of Retirement is Invalid date [dd MM yyyy] format .</b></span>";
                             messageexp = "Date Of Birth is Invalid date format .";
                             ErrorString = ErrorString + message + " | ";
                             ErrorString1 = ErrorString1 + messageexp + " | ";
                             IsErrorInUpload = true;
                         }
                     }

                     if (!(dtNew.Rows[i]["Date of Increment"]).ToString().Equals(string.Empty))
                     {
                         string datedoi = dtNew.Rows[i]["Date of Increment"].ToString();
                         DateTime date4;
                         if (DateTime.TryParseExact(datedoi, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date4))
                         {

                         }
                         else if (DateTime.TryParseExact(datedoi, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date4))
                         {

                         }
                         else
                         {
                             message = "<span style='color:Red'><b> Date Of Increment is Invalid date [dd MM yyyy] format .</b></span>";
                             messageexp = "Date Of Birth is Invalid date format .";
                             ErrorString = ErrorString + message + " | ";
                             ErrorString1 = ErrorString1 + messageexp + " | ";
                             IsErrorInUpload = true;
                         }
                     }


                    //-------------end---------
                   ////  string DOB1 = dtNew.Rows[i]["Date of Birth"].ToString();
                   //  string DOB = Convert.ToDateTime(dtNew.Rows[i]["Date of Birth"]).ToString("dd/MM/yyyy");
                     ////string DOB = Convert.ToDateTime(DOB1).ToString("dd/MM/yyyy");
                     ////ds1 = objCommon.FillDropDown("payroll_empmas", "*", "EmployeeId", "FNAME='" + Fname + "'and LNAME='" + Lname + "' and DOB='"+DOB+"'", "");
                     ////if (ds1.Tables[0].Rows.Count > 0)
                     ////{
                     ////    message = "<span style='color:Red'><b> FName LName and Date Of Birth Is Already Exists.</b></span>";
                     ////    messageexp = "FName LName and Date Of Birth Is Already Exists.";
                     ////    ErrorString = ErrorString + message + " | ";
                     ////    ErrorString1 = ErrorString1 + messageexp + " | ";
                     ////    IsErrorInUpload = true;
                     ////}




                        //if (!(dtNew.Rows[i]["Society Name"]).ToString().Equals(string.Empty))
                        //{
                        //    objPayMas.RegNo = dtNew.Rows[i]["Society Name"].ToString();
                        //}
                        //else
                        //{
                        //    message = "<span style='color:Red'><b>Please enter Society Name </b></span>";
                        //    messageexp = "Please enter Society Name ";
                        //    ErrorString = ErrorString + message + " | ";
                        //    ErrorString1 = ErrorString1 + messageexp + " | ";
                        //    IsErrorInUpload = true;
                        //}
                        if (!(dtNew.Rows[i]["College Name"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["College Name"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter College Name</b></span> ";
                            messageexp = "Please enter College Name";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;

                        }
                        if (!(dtNew.Rows[i]["Employee Id"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Employee Id"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter Employee Id</b></span> ";
                            messageexp = "Please enter Employee Id";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }
                        if (!(dtNew.Rows[i]["Title"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Title"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'>Please enter Title </span>";
                            messageexp = "Please enter Title ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }

                    //------start title

                        string Title = dtNew.Rows[i]["Title"].ToString();
                        ds1 = objCommon.FillDropDown("PAYROLL_TITLE", "*", "TITLE", "TITLE='" + Title + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Title is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Title is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                    //------end  title
                        if (!(dtNew.Rows[i]["First Name"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["First Name"].ToString();
                            string FName = Convert.ToString(dtNew.Rows[i]["First Name"]);

                            for (int j = 0; j < FName.Length; j++)
                            {
                                int result;
                                if (int.TryParse(FName[j].ToString(), out result))
                                {
                                    message = "<span style='color:Red'><b>Please enter Valid First Name Dont Use Numeric Value</b> </span>";
                                    messageexp = "Please enter Valid First Name Dont Use Numeric Value ";
                                    ErrorString = ErrorString + message + " | ";
                                    ErrorString1 = ErrorString1 + messageexp + " | ";
                                    IsErrorInUpload = true;
                                    // return;
                                    break;
                                }
                                else
                                {
                                    // not a number
                                }
                            }
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter First Name</b></span> ";
                            messageexp = "Please enter First Name ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;

                        }
                        if (!(dtNew.Rows[i]["Middle Name"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Middle Name"].ToString();
                            string MName = Convert.ToString(dtNew.Rows[i]["Middle Name"]);

                            for (int j = 0; j < MName.Length; j++)
                            {
                                int result;
                                if (int.TryParse(MName[j].ToString(), out result))
                                {
                                    message = "<span style='color:Red'><b>Please enter Valid Middle Name Dont Use Numeric Value </b></span>";
                                    messageexp = "Please enter Valid Middle Name Dont Use Numeric Value ";
                                    ErrorString = ErrorString + message + " | ";
                                    ErrorString1 = ErrorString1 + messageexp + " | ";
                                    IsErrorInUpload = true;
                                    break;
                                }
                                else
                                {
                                    // not a number
                                }
                            }
                        }
                        if (!(dtNew.Rows[i]["Last Name"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Last Name"].ToString();
                            string LName = Convert.ToString(dtNew.Rows[i]["Last Name"]);

                            for (int j = 0; j < LName.Length; j++)
                            {
                                int result;
                                if (int.TryParse(LName[j].ToString(), out result))
                                { 
                                    message = "<span style='color:Red'><b>Please enter Valid Last Name Dont Use Numeric Value </b></span>";
                                    messageexp = "Please enter Valid Last Name Dont Use Numeric Value ";
                                    ErrorString = ErrorString + message + " | ";
                                    ErrorString1 = ErrorString1 + messageexp + " | ";
                                    IsErrorInUpload = true;
                                    break;
                                }
                                else
                                {
                                    // not a number
                                }
                            }
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter Last Name </b></span>";
                            messageexp = "Please enter Last Name ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }

                    //------------start--gender

                        string gender = dtNew.Rows[i]["Gender"].ToString();
                        ds1 = objCommon.FillDropDown("ACD_GENDER", "*", "GENDERNAME", "GENDERNAME='" + gender + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Gender is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Gender is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                    //------------end----gender

                         if (!(dtNew.Rows[i]["Gender"]).ToString().Equals(string.Empty))
                        {
                            if (dtNew.Rows[i]["Gender"].ToString().Trim().ToUpper().Equals("FEMALE"))
                            {
                                objPayMas.gender = Convert.ToChar("F");
                            }
                            else if (dtNew.Rows[i]["Gender"].ToString().Trim().ToUpper().Equals("MALE"))
                            {
                                objPayMas.gender = Convert.ToChar("M");
                            }
                            else
                            {
                                message = "<span style='color:Red'><b>Please enter Gender in given format (MALE/FEMALE)</b></span>";
                                messageexp = "Please enter Gender in given format (MALE/FEMALE)";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;
                            }
                        }

                         if (!(dtNew.Rows[i]["Fathers Name"]).ToString().Equals(string.Empty))
                         {
                             objPayMas.RegNo = dtNew.Rows[i]["Fathers Name"].ToString();
                             string FaName = Convert.ToString(dtNew.Rows[i]["Fathers Name"]);

                             for (int j = 0; j < FaName.Length; j++)
                             {
                                 int result;
                                 if (int.TryParse(FaName[j].ToString(), out result))
                                 {
                                     //element is a number   
                                     message = "<span style='color:Red'><b>Please enter Valid Fathers Name Dont Use Numeric Value </b></span>";
                                     messageexp = "Please enter Valid Fathers Name Dont Use Numeric Value ";
                                     ErrorString = ErrorString + message + " | ";
                                     ErrorString1 = ErrorString1 + messageexp + " | ";
                                     IsErrorInUpload = true;
                                     // return;
                                     break;
                                 }
                                 else
                                 {
                                     // not a number
                                 }
                             }
                         }

                         if (!(dtNew.Rows[i]["Mothers Name"]).ToString().Equals(string.Empty))
                         {
                             objPayMas.RegNo = dtNew.Rows[i]["Mothers Name"].ToString();
                             string MoName = Convert.ToString(dtNew.Rows[i]["Mothers Name"]);

                             for (int j = 0; j < MoName.Length; j++)
                             {
                                 int result;
                                 if (int.TryParse(MoName[j].ToString(), out result))
                                 {
                                     //element is a number   
                                     message = "<span style='color:Red'><b>Please enter Valid Mothers Name Dont Use Numeric Value </b></span>";
                                     messageexp = "Please enter Valid Mothers Name Dont Use Numeric Value ";
                                     ErrorString = ErrorString + message + " | ";
                                     ErrorString1 = ErrorString1 + messageexp + " | ";
                                     IsErrorInUpload = true;
                                     // return;
                                     break;
                                 }
                                 else
                                 {
                                     // not a number
                                 }
                             }
                         }

                         if (!(dtNew.Rows[i]["Husbands Name"]).ToString().Equals(string.Empty))
                         {
                             objPayMas.RegNo = dtNew.Rows[i]["Husbands Name"].ToString();
                             string HuName = Convert.ToString(dtNew.Rows[i]["Husbands Name"]);

                             for (int j = 0; j < HuName.Length; j++)
                             {
                                 int result;
                                 if (int.TryParse(HuName[j].ToString(), out result))
                                 {
                                     //element is a number   
                                     message = "<span style='color:Red'><b>Please enter Valid Husbands Name Dont Use Numeric Value</b> </span>";
                                     messageexp = "Please enter Valid Husbands Name Dont Use Numeric Value ";
                                     ErrorString = ErrorString + message + " | ";
                                     ErrorString1 = ErrorString1 + messageexp + " | ";
                                     IsErrorInUpload = true;
                                     // return;
                                     break;
                                 }
                                 else
                                 {
                                     // not a number
                                 }
                             }
                         }

                        
                        
                        //if (!(dtNew.Rows[i]["Date of Retirement"]).ToString().Equals(string.Empty))
                        //{
                        //    objPayMas.RegNo = dtNew.Rows[i]["Date of Retirement"].ToString();
                        //}
                        //else
                        //{
                        //    message = "<span style='color:Red'><b>Please enter Date of Retirement </b></span>";
                        //    messageexp = "Please enter Date of Retirement ";
                        //    ErrorString = ErrorString + message + " | ";
                        //    ErrorString1 = ErrorString1 + messageexp + " | ";
                        //    IsErrorInUpload = true;
                        //}
                        //if (!(dtNew.Rows[i]["Date of Increment"]).ToString().Equals(string.Empty))
                        //{
                        //    objPayMas.RegNo = dtNew.Rows[i]["Date of Increment"].ToString();
                        //}
                        //else
                        //{
                        //    message = "<span style='color:Red'><b>Please enter Date of Increment</b></span> ";
                        //    messageexp = "Please enter Date of Increment";
                        //    ErrorString = ErrorString + message + " | ";
                        //    ErrorString1 = ErrorString1 + messageexp + " | ";
                        //    IsErrorInUpload = true;
                        //}

                        if (!(dtNew.Rows[i]["UID No"]).ToString().Equals(string.Empty))
                        {
                            string UID_No = Convert.ToString(dtNew.Rows[i]["UID No"]);
                            int numericValue;
                            bool isNumber = int.TryParse(UID_No, out numericValue);

                            if (isNumber==false)
                            {
                                message = "<span style='color:Red'><b>Please enter UID No In Number Foramt</b> </span>";
                                messageexp = "Please enter UID No In Number Foramt ";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;
   
                            }
                           
                        }

                        if (!(dtNew.Rows[i]["Actual Basic"]).ToString().Equals(string.Empty))
                        {
                            string Actual_Basic = Convert.ToString(dtNew.Rows[i]["Actual Basic"]);
                            int numericValue;
                            bool isNumber = int.TryParse(Actual_Basic, out numericValue);

                            if (isNumber == false)
                            {
                                message = "<span style='color:Red'><b>Please enter Actual Basic In Number Foramt </b></span>";
                                messageexp = "Please enter Actual Basic In Number Foramt";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;

                            }

                        }

                        if (!(dtNew.Rows[i]["Grade Pay"]).ToString().Equals(string.Empty))
                        {
                            string Grade_Pay = Convert.ToString(dtNew.Rows[i]["Grade Pay"]);
                            int numericValue;
                            bool isNumber = int.TryParse(Grade_Pay, out numericValue);

                            if (isNumber == false)
                            {
                                message = "<span style='color:Red'><b>Please enter Grade Pay In Number Foramt</b> </span>";
                                messageexp = "Please enter Grade Pay In Number Foramt ";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;

                            }

                        }

                        if (!(dtNew.Rows[i]["Department"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Department"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter Department </b></span>";
                            messageexp = "Please enter Department ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }


                        //------------start--Department

                        string Department = dtNew.Rows[i]["Department"].ToString();
                        ds1 = objCommon.FillDropDown("[dbo].[PAYROLL_SUBDEPT] ", "*", "SUBDEPT", "SUBDEPT='" + Department + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Department is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Department is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                        //------------end----Department



                        if (!(dtNew.Rows[i]["Designation"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Designation"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter Designation </b></span>";
                            messageexp = "Please enter Designation ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }

                        //------------start--Designation

                        string Designation = dtNew.Rows[i]["Designation"].ToString();
                        ds1 = objCommon.FillDropDown("[dbo].[PAYROLL_SUBDESIG]", "*", "SUBDESIG", "SUBDESIG='" + Designation + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Designation is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Designation is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                        //------------end----Designation


                        if (!(dtNew.Rows[i]["Staff Name"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Staff Name"].ToString();
                        }
                        else
                        {
                            message = "<span style='color:Red'><b>Please enter Staff Name </b></span>";
                            messageexp = "Please enter Staff Name ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }

                        //------------start--Staff Name

                        string StaffName = dtNew.Rows[i]["Staff Name"].ToString();
                        ds1 = objCommon.FillDropDown("[dbo].[PAYROLL_STAFF]", "*", "STAFF", "STAFF='" + StaffName + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Staff Name is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Staff Name is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }

                        //------------end----Staff Name

                        //------------start--Pay rule

                        string Payrule = dtNew.Rows[i]["Pay rule"].ToString();
                        if (Payrule!="")
                    {
                        ds1 = objCommon.FillDropDown("[dbo].[PAYROLL_RULE]", "*", "PAYRULE", "PAYRULE='" + Payrule + "'", "");

                        //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                            //if (empId1 == empId)
                            //{
                            message = " <span style='color:Red'><b>Employee Pay rule is Not Correct. Please check Master Data.</span>";
                            messageexp = "Employee Pay rule is Not Correct. Please check Master Data.";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                            //}
                        }
                    }
                        //------------end----Pay rule

                        //------------start--NATURE OF APPONIMENT

                        string NATUREOFAPPONIMENT = dtNew.Rows[i]["NATURE OF APPONIMENT"].ToString();
                        if (NATUREOFAPPONIMENT != "")
                        {
                            ds1 = objCommon.FillDropDown("[dbo].[PAYROLL_APPOINT]", "*", "APPOINT", "APPOINT='" + NATUREOFAPPONIMENT + "'", "");

                            //if (ds1.Tables[0].Rows[0]["EmployeeId"].ToString() != "")
                            if (ds1.Tables[0].Rows.Count == 0)
                            {
                                //string empId1 = ds1.Tables[0].Rows[0]["EmployeeId"].ToString();
                                //if (empId1 == empId)
                                //{
                                message = " <span style='color:Red'><b>Employee NATURE OF APPONIMENT is Not Correct. Please check Master Data.</span>";
                                messageexp = "Employee NATURE OF APPONIMENT is Not Correct. Please check Master Data.";
                                ErrorString = ErrorString + message + " | ";
                                ErrorString1 = ErrorString1 + messageexp + " | ";
                                IsErrorInUpload = true;
                                //}
                            }
                        }
                        //------------end----NATURE OF APPONIMENT

                        if (!(dtNew.Rows[i]["Mobile No"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["Mobile No"].ToString();
                             string mobileNo= dtNew.Rows[i]["Mobile No"].ToString();
                             if (mobileNo.Length < 10 || mobileNo.Length > 10)
                                 {
                               message = "<span style='color:Red'><b>Please enter Valid 10 digit Mobile Number </b></span>";
                               messageexp = "Please enter Valid 10 digit Mobile Number ";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                              }
                            
                            //Mobile Number start with 0 Validation

                            if (mobileNo.StartsWith("0"))
                                  {
                                      message = "<span style='color:Red'><b>Mobile Number Dont start with 0 </b></span>";
                                      messageexp = "Mobile Number Dont start with 0 ";
                                      ErrorString = ErrorString + message + " | ";
                                      ErrorString1 = ErrorString1 + messageexp + " | ";
                                      IsErrorInUpload = true;
                                  }
                      }
                        else
                        {
                            message = "<span style='color:Red'>Please enter Mobile No </span>";
                            messageexp = "Please enter Mobile No";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
                        }
                        if (!(dtNew.Rows[i]["E-mail ID"]).ToString().Equals(string.Empty))
                        {
                            objPayMas.RegNo = dtNew.Rows[i]["E-mail ID"].ToString();
                                 string email = dtNew.Rows[i]["E-mail ID"].ToString();
                                 var mail = new MailAddress(email);
                                 bool isValidEmail = mail.Host.Contains(".");
                                 if(!isValidEmail){
                                     message = "<span style='color:Red'><b>Please enter Valid E-mail ID</b> </span>";
                                     messageexp = "Please enter Valid E-mail ID";
                                     ErrorString = ErrorString + message + " | ";
                                     ErrorString1 = ErrorString1 + messageexp + " | ";
                                     IsErrorInUpload = true;

                                 } 
                        }
                        else
                        {
                            message = "<span style='color:Red'<b>>Please enter E-mail ID</b> </span>";
                            messageexp = "Please enter Valid E-mail ID";
                            ErrorString = ErrorString + message + " | ";
                            ErrorString1 = ErrorString1 + messageexp + " | ";
                            IsErrorInUpload = true;
      

                        }

                      


                        if (IsErrorInUpload == false)
                        {
                            TotalRecordUploadCount++;

                            message = "<span style='color:Green'> <b>Record Is Ok</b></span> ";
                            messageexp = "Record Is Ok";
                           
                               ErrorString = ErrorString + message + " | ";
                               ErrorString1 = ErrorString1 + messageexp + " | ";
                            
                        }

                        else
                        {

                            TotalRecordErrorCount++;
                            IsErrorInUpload = true;
                        }

                        if (ErrorString.Trim().EndsWith("|"))
                        {
                            ErrorString = ErrorString.Remove(ErrorString.Length - 2, 1);
                        }
                        dr = dt1.NewRow();
                        dr["RowId"] = (i + 1).ToString();
                        dr["Description"] = ErrorString;
                        dt1.Rows.Add(dr);


                        dr1 = dt2.NewRow();
                        dr1["RowId"] = (i + 1).ToString();
                        dr1["Description"] = ErrorString1;
                        dt2.Rows.Add(dr1);
                        ViewState["CurrentTable"] = dt2;

                    }

                    //dr = dt1.NewRow();
                    //dr["RowId"] = (i + 1).ToString();
                    //dr["Description"] = ErrorString;
                    //dt1.Rows.Add(dr);
                    //ViewState["CurrentTable"] = dt1;

                //}

                // Display Total count here
                lblTotalRecordCount.Text = TotalRecordCount.ToString();
                lblTotalRecordUploadCount.Text = TotalRecordUploadCount.ToString();
                lblTotalAlreadyExistsCount.Text = TotalAlreadyExistsCount.ToString();
                lblTotalRecordErrorCount.Text = TotalRecordErrorCount.ToString();

                LvDescription.DataSource = dt1;
                LvDescription.DataBind();

                ViewState["ExcelData"] = dt2;
                if (Convert.ToInt32(lblTotalRecordErrorCount.Text) > 0)
                {
                    message = "<span style='color:Red'><b> Resolve Error And Again Upload File </b></span> ";
                    lblErrorNote.Text = message;
                }
                if (lblTotalRecordCount.Text == lblTotalRecordUploadCount.Text)
                {
                   
                    int ClgCode = Convert.ToInt32(Session["colcode"]);
                    int OrgId = Convert.ToInt32(Session["OrgId"]);
                    int Uano = Convert.ToInt32(Session["userNo"]);
                    DataTable();
                    //int usertype =Convert.ToInt32( ddlEmployeeType.SelectedValue);
                    int usertype = Convert.ToInt32(DropDownList1.SelectedValue);
                    cs = (CustomStatus)objPayHeadController.SaveEmployeeExcelSheetData(ClgCode, OrgId, objPayMas, usertype, Uano);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);

                }

                if (TotalRecordErrorCount > 0)
                {
                    objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully with Errors, Please check error log!!", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully!!", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                {
                    objCommon.DisplayMessage(updpnl, "Data not available in ERP Master", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                    return;
                }
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
            }
            finally
            {
                connExcel.Close();
            }

        }


        public void DataTable()
        {
            //----------------Data Table-----------------//

            System.Data.DataTable dtemployeedata = new System.Data.DataTable();
            dtemployeedata.Columns.Add(new DataColumn("Society Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("College Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Employee Id", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("RFIDNO", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("Title", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("First Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Middle Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Last Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Gender", typeof(string)));

            dtemployeedata.Columns.Add(new DataColumn("Fathers Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Mothers Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Husbands Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Date of Birth", typeof(DateTime)));
            dtemployeedata.Columns.Add(new DataColumn("Date of Joining", typeof(DateTime)));
            dtemployeedata.Columns.Add(new DataColumn("Date of Retirement", typeof(DateTime)));
            dtemployeedata.Columns.Add(new DataColumn("Date of Increment", typeof(DateTime)));
            dtemployeedata.Columns.Add(new DataColumn("UID No", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("Actual Basic", typeof(double)));

            dtemployeedata.Columns.Add(new DataColumn("Grade Pay", typeof(double)));
            dtemployeedata.Columns.Add(new DataColumn("Department", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Designation", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Staff Name", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Mobile No", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("E-mail ID", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Pay rule", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("Pay Scale", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("NATURE OF APPONIMENT", typeof(string)));

            dtemployeedata.Columns.Add(new DataColumn("CONSOLIDATED EMPLOYEE AMOUNT", typeof(string)));
            dtemployeedata.Columns.Add(new DataColumn("TitleID", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("DepartmentID", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("DesignationID", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("StaffID", typeof(int)));
            dtemployeedata.Columns.Add(new DataColumn("IDNO", typeof(int)));






            DataRow dr1 = null;
            foreach (ListViewItem ii in lvEmployee.Items)
            {
                System.Web.UI.WebControls.HiddenField hdsocity = (System.Web.UI.WebControls.HiddenField)ii.FindControl("hdsocity");
                System.Web.UI.WebControls.Label lblSchoolName = (System.Web.UI.WebControls.Label)ii.FindControl("lblSchoolName");
                System.Web.UI.WebControls.Label lblEmployeeId = (System.Web.UI.WebControls.Label)ii.FindControl("lblEmployeeId");
                System.Web.UI.WebControls.Label lblRFIDNO = (System.Web.UI.WebControls.Label)ii.FindControl("lblRFIDNO");
                System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)ii.FindControl("lblTitle");
                System.Web.UI.WebControls.Label lblFirstName = (System.Web.UI.WebControls.Label)ii.FindControl("lblFirstName");
                System.Web.UI.WebControls.Label lblMiddleName = (System.Web.UI.WebControls.Label)ii.FindControl("lblMiddleName");
                System.Web.UI.WebControls.Label lblLastName = (System.Web.UI.WebControls.Label)ii.FindControl("lblLastName");
                System.Web.UI.WebControls.Label lblGender = (System.Web.UI.WebControls.Label)ii.FindControl("lblGender");
                System.Web.UI.WebControls.Label lblFathersName = (System.Web.UI.WebControls.Label)ii.FindControl("lblFathersName");
                System.Web.UI.WebControls.Label lblMothersName = (System.Web.UI.WebControls.Label)ii.FindControl("lblMothersName");
                System.Web.UI.WebControls.Label lblHusbandsName = (System.Web.UI.WebControls.Label)ii.FindControl("lblHusbandsName");
                System.Web.UI.WebControls.Label lblDateofBirth = (System.Web.UI.WebControls.Label)ii.FindControl("lblDateofBirth");
                System.Web.UI.WebControls.Label lblDateofJoining = (System.Web.UI.WebControls.Label)ii.FindControl("lblDateofJoining");
                System.Web.UI.WebControls.Label lblDateofRetirement = (System.Web.UI.WebControls.Label)ii.FindControl("lblDateofRetirement");
                System.Web.UI.WebControls.Label lblDateofIncrement = (System.Web.UI.WebControls.Label)ii.FindControl("lblDateofIncrement");
                System.Web.UI.WebControls.Label lblUIDNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblUIDNo");
                System.Web.UI.WebControls.Label lblActualBasic = (System.Web.UI.WebControls.Label)ii.FindControl("lblActualBasic");
                System.Web.UI.WebControls.Label lblGradePay = (System.Web.UI.WebControls.Label)ii.FindControl("lblGradePay");
                System.Web.UI.WebControls.Label lblDepartment = (System.Web.UI.WebControls.Label)ii.FindControl("lblDepartment");
                System.Web.UI.WebControls.Label lblDesignation = (System.Web.UI.WebControls.Label)ii.FindControl("lblDesignation");
                System.Web.UI.WebControls.Label lblStaffName = (System.Web.UI.WebControls.Label)ii.FindControl("lblStaffName");
                System.Web.UI.WebControls.Label lblMobileNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblMobileNo");
                System.Web.UI.WebControls.Label lblEmailID = (System.Web.UI.WebControls.Label)ii.FindControl("lblEmailID");
                System.Web.UI.WebControls.Label lblPayrule = (System.Web.UI.WebControls.Label)ii.FindControl("lblPayrule");
                System.Web.UI.WebControls.Label lblPayScale = (System.Web.UI.WebControls.Label)ii.FindControl("lblPayScale");
                System.Web.UI.WebControls.Label lblNATUREOFAPPONIMENT = (System.Web.UI.WebControls.Label)ii.FindControl("lblNATUREOFAPPONIMENT");
                System.Web.UI.WebControls.Label lblCONSOLIDATEDEMPLOYEEAMOUNT = (System.Web.UI.WebControls.Label)ii.FindControl("lblCONSOLIDATEDEMPLOYEEAMOUNT");


                dr1 = dtemployeedata.NewRow();
                dr1["Society Name"] = "";
                dr1["College Name"] = lblSchoolName.Text;
                dr1["Employee Id"] = lblEmployeeId.Text;
                if (lblRFIDNO.Text == "")
                {
                    dr1["RFIDNO"] = 0;
                }
                else
                {
                    dr1["RFIDNO"] = lblRFIDNO.Text;
                }
                dr1["Title"] = lblTitle.Text;
                dr1["First Name"] = lblFirstName.Text;
                if (lblMiddleName.Text == "")
                {
                    dr1["Middle Name"] = "";
                }
                else
                {
                    dr1["Middle Name"] = lblMiddleName.Text;
                }
                dr1["Last Name"] = lblLastName.Text;
                dr1["Gender"] = lblGender.Text;
               
                if (lblFathersName.Text == "")
                {
                    dr1["Fathers Name"] = "";
                }
                else
                {
                    dr1["Fathers Name"] = lblFathersName.Text;
                }
                if (lblMothersName.Text == "")
                {
                    dr1["Mothers Name"] = "";
                }
                else
                {
                    dr1["Mothers Name"] = lblMothersName.Text;
                }
                if (lblHusbandsName.Text == "")
                {
                    dr1["Husbands Name"] = "";
                }
                else
                {
                    dr1["Husbands Name"] = lblHusbandsName.Text;
                }
                dr1["Date of Birth"] = lblDateofBirth.Text;
                dr1["Date of Joining"] = lblDateofJoining.Text;

                if (lblDateofRetirement.Text == "")
                {
                    dr1["Date of Retirement"] = DBNull.Value;
                }
                else
                {
                    dr1["Date of Retirement"] = lblDateofRetirement.Text;
                }



                if (lblDateofIncrement.Text == "")
                {
                    dr1["Date of Increment"] = DBNull.Value;
                }
                else
                {
                    dr1["Date of Increment"] = lblDateofIncrement.Text;
                }
                if (lblUIDNo.Text == "")
                {
                    dr1["UID No"] = 0;
                }
                else
                {
                    dr1["UID No"] = lblUIDNo.Text;
                }
                if (lblActualBasic.Text == "")
                {
                    dr1["Actual Basic"] = 0.0;
                }
                else
                {
                    dr1["Actual Basic"] = lblActualBasic.Text;
                }
                if (lblGradePay.Text == "")
                {
                    dr1["Grade Pay"] = 0.0;
                }
                else
                {
                    dr1["Grade Pay"] = lblGradePay.Text;
                }
                dr1["Department"] = lblDepartment.Text;
                dr1["Designation"] = lblDesignation.Text;
                dr1["Staff Name"] = lblStaffName.Text;
                dr1["Mobile No"] = lblMobileNo.Text;
                dr1["E-mail ID"] = lblEmailID.Text;
                if (lblPayrule.Text == "")
                {
                    dr1["Pay rule"] = "";
                }
                else
                {
                    dr1["Pay rule"] = lblPayrule.Text;
                }
                if (lblPayScale.Text == "")
                {
                    dr1["Pay Scale"] = "";
                }
                else
                {
                    dr1["Pay Scale"] = lblPayScale.Text;
                }
                if (lblNATUREOFAPPONIMENT.Text == "")
                {
                    dr1["NATURE OF APPONIMENT"] = "";
                }
                else
                {
                    dr1["NATURE OF APPONIMENT"] = lblNATUREOFAPPONIMENT.Text;
                }
                if (lblCONSOLIDATEDEMPLOYEEAMOUNT.Text == "")
                {
                    dr1["CONSOLIDATED EMPLOYEE AMOUNT"] = "";
                }
                else
                {
                    dr1["CONSOLIDATED EMPLOYEE AMOUNT"] = lblCONSOLIDATEDEMPLOYEEAMOUNT.Text;
                }
                dr1["TitleID"] = 0;
                dr1["DepartmentID"] = 0;
                dr1["DesignationID"] = 0;
                dr1["StaffID"] = 0;
                dr1["IDNO"] = 0;

                dtemployeedata.Rows.Add(dr1);

            }
            objPayMas.EmployeeDataImport_TBL = dtemployeedata;


            //-------------------------------------------//
        }

        protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvEmployee.DataSource = null;
            lvEmployee.DataBind();
            lvEmployee.Visible = false;
        }




        protected void lvStudent_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (dr["REGISTRATIONNO"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).Font.Bold = true;
                }

                if (!(dr["GENDER"]).Equals(string.Empty) && (!(dr["GENDER"].Equals("MALE"))))
                {
                    if (!(dr["GENDER"].ToString().Equals("FEMALE")))
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGender")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGender")).Font.Bold = true;
                    }

                }
                if (!(dr["CATEGORY"]).ToString().Equals(string.Empty))
                {
                    string categoryno = objCommon.LookUp("ACD_CATEGORY", "COUNT(1)", "CATEGORY='" + dr["CATEGORY"].ToString() + "'");
                    if (Convert.ToInt32(categoryno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCategory")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCategory")).Font.Bold = true;

                    }
                }

                if (!(dr["PHYSICALLY_HANDICAPPED"]).ToString().Equals(string.Empty))
                {
                    if (!(dr["PHYSICALLY_HANDICAPPED"].ToString().Equals("YES")) && (!(dr["PHYSICALLY_HANDICAPPED"].ToString().Equals("NO"))))
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPH")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPH")).Font.Bold = true;
                    }

                }


                if (dr["STUDENTNAME"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).Font.Bold = true;
                }

                if (!(dr["ADMISSIONTYPE"]).ToString().Equals(string.Empty))
                {
                    if (!(dr["ADMISSIONTYPE"]).ToString().Equals("Regular") && !(dr["ADMISSIONTYPE"]).ToString().Equals("Direct Second Year(Lateral)"))
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
                    }
                }

                else if ((dr["ADMISSIONTYPE"].ToString() == string.Empty))
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
                }

                if (dr["SEMESTER"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).Font.Bold = true;
                }
                if (dr["YEAR"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).Font.Bold = true;
                }

                if (!(dr["DEGREE"]).ToString().Equals(string.Empty))
                {
                    string degreeno = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dr["DEGREE"].ToString() + "'");
                    if (Convert.ToInt32(degreeno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Font.Bold = true;

                    }
                }

                if (dr["DEGREE"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Font.Bold = true;
                }

                if (!(dr["COLLEGENAME"]).ToString().Equals(string.Empty))
                {
                    string collegeno = objCommon.LookUp("ACD_COLLEGE_MASTER", "COUNT(1)", "COLLEGE_NAME='" + dr["COLLEGENAME"].ToString() + "'");
                    if (Convert.ToInt32(collegeno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Font.Bold = true;

                    }
                }

                if (dr["COLLEGENAME"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Font.Bold = true;
                }

                if (!(dr["BRANCH"]).ToString().Equals(string.Empty))
                {
                    string branchno = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dr["BRANCH"].ToString() + "'");
                    if (Convert.ToInt32(branchno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Font.Bold = true;

                    }
                }
                if (dr["BRANCH"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Font.Bold = true;
                }

                if (!(dr["STATE"]).ToString().Equals(string.Empty))
                {
                    string stateno = objCommon.LookUp("ACD_STATE", "COUNT(1)", "STATENAME='" + dr["STATE"].ToString() + "'");
                    if (Convert.ToInt32(stateno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblState")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblState")).Font.Bold = true;

                    }
                }

                if (!(dr["DISTRICT"]).ToString().Equals(string.Empty))
                {
                    string districtno = objCommon.LookUp("ACD_DISTRICT", "COUNT(1)", "DISTRICTNAME='" + dr["DISTRICT"].ToString() + "'");
                    if (Convert.ToInt32(districtno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDistrict")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDistrict")).Font.Bold = true;

                    }
                }

                if (!(dr["BLOODGROUP"]).ToString().Equals(string.Empty))
                {
                    string bloodgroupno = objCommon.LookUp("ACD_BLOODGRP", "COUNT(1)", "BLOODGRPNAME='" + dr["BLOODGROUP"].ToString() + "'");
                    if (Convert.ToInt32(bloodgroupno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBloogGrp")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBloogGrp")).Font.Bold = true;

                    }
                }

                if (!(dr["PAYMENT_TYPE"]).ToString().Equals(string.Empty))
                {
                    string bloodgroupno = objCommon.LookUp("ACD_PAYMENTTYPE", "COUNT(1)", "PAYTYPENAME='" + dr["PAYMENT_TYPE"].ToString() + "'");
                    if (Convert.ToInt32(bloodgroupno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPayType")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPayType")).Font.Bold = true;

                    }
                }

                if (dr["MOBILENO"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).Font.Bold = true;
                }

                if (dr["EMAILID"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).Font.Bold = true;
                }

                if (!(dr["SECTION"]).ToString().Equals(string.Empty))
                {
                    string sectionno = objCommon.LookUp("ACD_SECTION", "COUNT(1)", "SECTIONNAME='" + dr["SECTION"].ToString() + "'");
                    if (Convert.ToInt32(sectionno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSection")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSection")).Font.Bold = true;

                    }
                }

            }


        #endregion

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
             {
                Uploaddata();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
            //btnExportUploadLog.Enabled = true;
        }

        

        protected void btnExportUploadLog_Click(object sender, EventArgs e)
        {
            GridView gvStudData = new GridView();
            gvStudData.DataSource = ViewState["ExcelData"];
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=DATA_IMPORT_LOG.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);
            gvStudData.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void btnCancelUpload_Click(object sender, EventArgs e)
        {
            //ddlStudAdmBatch.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            LvDescription.DataSource = null;
            LvDescription.DataBind();      
            divCount.Visible = false;
            Panel3.Visible = false;
        }



        protected void btnExport_Click(object sender, EventArgs e)
        {
            ////Response.AddHeader("Content-Disposition", "attachment;filename=Employee_Data_Migration_Format.xlsx");
            ////Response.TransmitFile(Server.MapPath("~/EXCELSHEET/Employee_Data_Migration_Format.xlsx"));
            ////Response.End();

            //WebClient req = new WebClient();
            //HttpResponse response = HttpContext.Current.Response;
            ////string filePath = lblresume.Text;
            //response.Clear();
            //response.ClearContent();
            //response.ClearHeaders();
            //response.Buffer = true;
            //response.AddHeader("Content-Disposition", "attachment;filename=Employee_Data_Migration_Format.xlsx");
            //byte[] data = req.DownloadData(Server.MapPath("~/ADMINISTRATION/EXCELSHEET/Employee_Data_Migration_Format.xlsx"));// PresentationLayer /
            //response.BinaryWrite(data);
            //response.End();


            try
            {

                DataSet ds = objPayHeadController.GetMasterData();

                ds.Tables[0].TableName = "Employee_Data_Migration_Format";
                ds.Tables[1].TableName = "College Name";
                ds.Tables[2].TableName = "Title";
                ds.Tables[3].TableName = "Gender";
                ds.Tables[4].TableName = "Department";
                ds.Tables[5].TableName = "Designation";
                ds.Tables[6].TableName = "Staff Name";
                ds.Tables[7].TableName = "Pay rule";
                ds.Tables[8].TableName = "NATURE OF APPONIMENT";
                string status = string.Empty;
                //// added by kajal jaiswal on 16-02-2023 for checking table is blank 
                //foreach (System.Data.DataTable dt in ds.Tables)
                //{
                //    if (dt.Rows.Count == 0)
                //    {
                //        status += dt.TableName + ",";

                //    }
                //}

                //if (status != string.Empty)
                //{
                //    status = status.Trim(',');
                //    objCommon.DisplayMessage(Page, "Data not available in ERP Master Table " + status, this.Page);
                //    return;
                //}



                //using (XLWorkbook wb = new XLWorkbook())
                //{
                //    foreach (System.Data.DataTable dt in ds.Tables)
                //    {
                //        //Add System.Data.DataTable as Worksheet.
                //        wb.Worksheets.Add(dt);
                //    }





                //    //Export the Excel file.
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.Charset = "";
                //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadStudentData.xlsx");
                //    using (MemoryStream MyMemoryStream = new MemoryStream())
                //    {
                //        wb.SaveAs(MyMemoryStream);
                //        MyMemoryStream.WriteTo(Response.OutputStream);
                //        Response.Flush();
                //        Response.End();
                //    }

                    //Response.ClearContent();
                    //Response.AddHeader("content-disposition", attachment);
                    //Response.ContentType = "application/vnd.MS-excel";
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //GVEmpChallan.RenderControl(htw);
                    //Response.Write(sw.ToString());
                    //Response.End();
               // }

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                        {
                            //Add System.Data.DataTable as Worksheet.
                            var ws = wb.Worksheets.Add(dt);
                            int i = 0;
                           // var ws = wb.Worksheets.Add(dt, dt.TableName.ToString());
                            for (int j = 1; j <= ds.Tables[0].Columns.Count; j++)
                            {

                                // var temp = ds.Tables[0].Columns[j];

                                if ("College Name" == ds.Tables[0].Columns[i].ToString() || "Employee Id" == ds.Tables[0].Columns[i].ToString() || "Title" == ds.Tables[0].Columns[i].ToString() || "First Name" == ds.Tables[0].Columns[i].ToString() || "Last Name" == ds.Tables[0].Columns[i].ToString() || "Gender" == ds.Tables[0].Columns[i].ToString() || "Date of Birth" == ds.Tables[0].Columns[i].ToString() || "Date of Joining" == ds.Tables[0].Columns[i].ToString() || "Department" == ds.Tables[0].Columns[i].ToString() || "Designation" == ds.Tables[0].Columns[i].ToString() || "Staff Name" == ds.Tables[0].Columns[i].ToString() || "Mobile No" == ds.Tables[0].Columns[i].ToString() || "E-mail ID" == ds.Tables[0].Columns[i].ToString())
                                {

                                    ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 64, 0); //All columns of second row
                                }
                                else
                                {
                                    ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 150, 255); //All columns of second row
                                }
                                i++;
                            }
                        }

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadEmployee_Data.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

}


