//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE/MODIFY EXISTING USER                                  
// CREATION DATE : 13-April-2009                                                   
// CREATED BY    : ASHWINI BARBATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
//-----------------------------------------------------------------------------------------------------------------------------
//--Version   Modified    On Modified         By Purpose
//-----------------------------------------------------------------------------------------------------------------------------
//--1.0.1    20-02-2024     Rutuja         Changes for Parent type
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
using mastersofterp_MAKAUAT;
using System.Net.Security;
using System.Data.SqlClient;
//REMOVED BY JIT
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using mastersofterp_MAKAUAT;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using Mastersoft.Security.IITMS;
using BusinessLogicLayer.BusinessLogic;
using System.Net;


public partial class create_user : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    User_AccController useracc = new User_AccController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        divMsg.InnerHtml = string.Empty;

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();

                //Populate the user dropdownlist with username and userid
                PopulateDropDownList();
                txtPassword.Enabled = false;
                //ShowPanel();
                ViewState["CheckBtn"] = true;
                ViewState["ExistUser"] = false;
                ViewState["action"] = null;
                ViewState["action"] = "add";
                BindCheckList_ForCollegeName();
                ShowUserDetails(Convert.ToInt32(Session["userno"].ToString()));
               
                if (Session["usertype"].ToString() == "4")
                {
                    chkDEC.Checked = true;
                    chkDEC.Enabled = false;
                }
                else
                    if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
                    {
                        chkDEC.Checked = false;
                        chkDEC.Enabled = false;
                    
                    }
                DataSet dsconfig = null;
                dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP,USER_PROFILE_SENDERNAME", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                if (dsconfig != null && dsconfig.Tables[0].Rows.Count > 0)
                {
                    ViewState["SUBJECT_OTP"] = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();
                    ViewState["SENDGRID_STATUS"] = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                    ViewState["USER_PROFILE_SENDERNAME"] = dsconfig.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();
                    //tvLinks.Attributes.Add("onclick", "OnTreeClick(event)");
                }
            }
            //ClearFileds();

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }
        btnExporttoExcel.Enabled = false;
        divmessage.Visible = false;
       
    }
   
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    #endregion

    #region Click Events


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        //ViewState["CheckBtn"] = true;
        string pwd = string.Empty;
        string encryptpwd = string.Empty;
        string OldMobileno = string.Empty;
        string OldEmailid = string.Empty;
        string collegeId = string.Empty;
        string TemplateID = string.Empty;
        string TEMPLATE = string.Empty;
        
        try
        {
            if (txtEmail.Text == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Email Id", Page);
                return;
            }
            if (txtMobile.Text == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Mobile No", Page);
                return;
            }
            if (SecurityThreads.ValidInput(txtEmail.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                return;
            }
            if (SecurityThreads.ValidInput(txtMobile.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                return;
            }
            if (SecurityThreads.CheckSecurityInput(txtEmail.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Email Id", Page);
                return;
            }
            if (SecurityThreads.CheckSecurityInput(txtMobile.Text))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Valid Mobile No", Page);
                return;
            }


            if (Convert.ToBoolean(ViewState["CheckBtn"]) == true)
            {
                User_AccController objUC = new User_AccController();
                UserAcc objUA = new UserAcc();
                if (Convert.ToBoolean(ViewState["ExistUser"]) == true)
                {
                    objUA.UA_Acc = objCommon.LookUp("USER_ACC", "UA_ACC", "UA_NO=" + Convert.ToInt32(txtUsername.ToolTip.ToString() == "" ? "0" : txtUsername.ToolTip.ToString()));
                    if (objUA.UA_Acc == "")
                    {
                        objUA.UA_Acc = "0,500";
                    }
                }
                else
                {
                    //objUA.UA_Acc = objCommon.GetLinks(tvLinks);
                    objUA.UA_Acc = "0,500";
                }
                objUA.UA_SourcePageNo = Convert.ToInt32(Request.QueryString["pageno"].ToString());
                objUA.UA_Userno = Convert.ToInt32(Session["userno"].ToString());
                objUA.OrganizationId = Convert.ToInt32(ddlOrg.SelectedValue);
                objUA.UA_Dec = chkDEC.Checked ? 1 : 0;
                objUA.UA_Desig = txtDesignatio.Text.Replace("'", "`").Trim();
                objUA.UA_Email = txtEmail.Text.Replace("'", "`").Trim();
                objUA.UA_EmpDeptNo = Convert.ToInt32((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "11" || Session["usertype"].ToString() == "1" && ddlSubDept.SelectedIndex > 0 ? ddlSubDept.SelectedValue.ToString() : "0");
                objUA.Parent_UserType = Convert.ToInt16(Session["usertype"].ToString());
                objUA.UA_EmpSt = "0";
                objUA.UA_FullName = txtFName.Text.Replace("'", "`").Trim();
                //objUA.UA_DeptNo = Convert.ToInt32((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1" && ddlDept.SelectedIndex > 0 ? ddlDept.SelectedValue.ToString() : "0");
                objUA.MOBILE = txtMobile.Text.Trim();
                string department = string.Empty;
                foreach (ListItem items in ddlDept.Items)
                {
                    if (items.Selected == true)
                    {
                        department += items.Value + ',';
                    }
                }
                //department = department.Length - 1;
                if (department.Length > 1)
                {
                    department = department.Remove(department.Length - 1);
                    //department = (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1" && ddlDept.SelectedIndex > 0 ? department.ToString() : "0";
                }//if()

                //if (Convert.ToInt32(ddlOrg.SelectedValue) == 0 && ddlUserType.SelectedValue == "5")
                //{
                //    collegeId = "0";
                //    objUA.College_No = "0";
                //}
                //else
                //{
                //added by rutuja 12/02/24
                string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
                    foreach (ListItem item in chkCollegeName.Items)
                    {
                        if (item.Selected == true)
                        {
                            collegeId = collegeId + item.Value.ToString() + ",";
                        }
                    }
                    if (collegeId != "")
                    {
                        if (collegeId.Substring(collegeId.Length - 1) == ",")
                        {
                            collegeId = collegeId.Substring(0, collegeId.Length - 1);
                            objUA.College_No = collegeId.ToString();

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Atleast One College Name!!", this.Page);

                        return;
                    }
                //}

                objUA.UA_No = Convert.ToInt32(txtUsername.ToolTip.ToString() == "" ? "0" : txtUsername.ToolTip.ToString());
                objUA.UA_Name = txtUsername.Text.Replace("'", "`").Trim();
               
                //if (txtPassword.Text.Replace("'", "`").Trim() == string.Empty)
                if (hdfPassword.Value.Replace("'", "`").Trim() != string.Empty)
                {
                    if (txtPassword.Text.Trim() == string.Empty)
                    {
                        objUA.UA_Pwd = hdfPassword.Value;
                    }
                    else
                    {
                       
                            pwd = txtPassword.Text.Trim();
                        encryptpwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);

                        if (hdfPassword.Value == encryptpwd)
                        {
                            objUA.UA_Pwd = hdfPassword.Value;
                        }
                        else
                        {
                            objUA.UA_Pwd = encryptpwd; // clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                        }
                    }
                  
                    //objUA.UA_Pwd = hdfPassword.Value;
                    OldMobileno = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_NAME='" + objUA.UA_Name + "' and UA_NAME IS NOT NULL");
                    OldEmailid = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + objUA.UA_Name + "' and UA_NAME IS NOT NULL");
                }
                else
                {
                    //objUA.UA_Pwd = Common.EncryptPassword(txtPassword.Text.Replace("'", "`").Trim());
                    //if  (ViewState["action"].ToString() == "edit")
                    //{
                        
                    //}
                    //else if (ViewState["action"].ToString() == "add") 
                    //{

                        pwd = GeneartePassword();
                        objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                    
                    //}
                    //  objUA.UA_Pwd = Common.EncryptPassword(pwd);
                }

                objUA.UA_QtrNo = txtQtrRoomNo.Text.Replace("'", "`").Trim();
                objUA.UA_Status = chkActive.Checked ? 0 : 1;
                objUA.UA_Type = Convert.ToInt32(ddlUserType.SelectedValue.ToString());
                //objUA.DRCSTATUS = ddlSupervisorstatus.SelectedValue;
                objUA.UA_Remark = txtRemark.Text;

                if (Convert.ToBoolean(ViewState["ExistUser"]) == false)
                {
                    objUA.UA_Type = Convert.ToInt32(ddlUserType.SelectedValue.ToString());
                }
                else
                {
                    if (Convert.ToInt32(ddlNewUserType.SelectedValue.ToString()) == 0)
                    {
                        objUA.UA_Type = Convert.ToInt32(ddlUserType.SelectedValue.ToString());
                    }
                    else
                    {
                        objUA.UA_Type = Convert.ToInt32(ddlNewUserType.SelectedValue.ToString());
                    }
                }

                if (Convert.ToBoolean(ViewState["ExistUser"]) == false)
                {
                    if (objUC.CheckUser(txtUsername.Text.Replace("'", "`").Trim()) == false)
                    {
                        lblSubmitStatus.Text = "This Username already exists!! Try another.";
                        lblSubmitStatus.ForeColor = System.Drawing.Color.Red;
                        txtUsername.Focus();
                        return;
                    }
                    else
                    {
                        //create new user
                        int ret = objUC.AddUser(objUA, department);

                        if (ret != 1001 || ret != -1001)
                        {
                            string user_type = objCommon.LookUp("USER_ACC", "UA_TYPE", " UA_DEC = 0  AND UA_NO =" + ret);
                            if (user_type == "3" || user_type == "3")
                            {
                                PStaffController staff = new PStaffController();
                                staff.AddNewInternalStaff(ret);
                            }
                            
                            // Send autogenerate password to email id and molile no.
                            //string useremail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + txtUsername.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL");
                           //// string message = "Your MIS  Account has been Created Successfully! Login with Username : " + objUA.UA_Name + ", Password : " + "" + pwd + "";
                           // string message = "Dear " + objUA.UA_FullName + "<br />";
                           // message = message + "Your MIS  Account has been Created Successfully! <br />";
                           // message = message + "Please Login using following details <br />";
                           // message = message + " Username : " + objUA.UA_Name + "<br />";
                           // message = message + " Password : " + "" + pwd + "<br />";
                           // message = message + "click  " + loginurl + " here to Login";
                           // string ss = ViewState["SUBJECT_OTP"] == null ? "" : ViewState["SUBJECT_OTP"].ToString();
                            //string subject = ss + "-MIS Login Credentials";
                            //added by rutuja 12/02/24
                            string useremail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + txtUsername.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL");
                            int reg = 0;
                            string ss = "";
                            //------------Code for sending email,It is optional---------------
                           DataSet dsconfig1 = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP,USER_PROFILE_SENDERNAME", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                            if (dsconfig1 != null && dsconfig1.Tables[0].Rows.Count > 0)
                            {
                                 ss = dsconfig1.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();
                            }
                            
                            int TemplateTypeId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE='User Login'"));
                            int TemplateId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATE", "TOP 1 TEMPLATEID", "TEMPTYPEID=" + TemplateTypeId + " AND TEMPLATENAME = 'Login Credentials'"));

                            string message = "";
                            DataSet ds_mstQry1 = objCommon.FillDropDown("ACD_ADMP_EMAILTEMPLATE AEM", "TOP 1 TEMPLATETEXT", "", "TEMPLATEID=" + TemplateId + "", "AEM.TEMPTYPEID ");

                            if (ds_mstQry1 != null)
                            {
                                message = ds_mstQry1.Tables[0].Rows[0]["TEMPLATETEXT"].ToString();
                                message = message.Replace("[FIRST NAME]", objUA.UA_FullName);
                                message = message.Replace("[LOGIN NAME]", objUA.UA_Name);
                                message = message.Replace("[USERPASSWORD]", pwd);
                                message = message.Replace("[CLICKHERELOGIN]", loginurl);
                                }
                            string subject = ss + "-MIS Login Credentials";
                            SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation 
                            reg = objSendEmail.SendEmail(useremail, message, subject); //Calling Method
                           
                            if (reg == 1)
                            {
                               // lblSubmitStatus.Text = "User Added Successfully ,Username and Password Send on your Registered Email ID and Mobile!!";
                                lblSubmitStatus.ForeColor = System.Drawing.Color.Green;
                                objCommon.DisplayMessage(this.Page, "User Created Successfully, Username and Password sent on your Registered Email ID and Mobile Number.", this.Page);

                            }
                            else
                            {
                                lblSubmitStatus.Text = "Sorry, Your Application not configured with mail server,Please contact Admin Department !!";
                                lblSubmitStatus.ForeColor = System.Drawing.Color.Red;
                                objCommon.DisplayMessage(this.Page, "Sorry, Your Application not configured with mail server,Please contact Admin Department !!", this.Page);
                            }

                            //send sms to user
                            string templatename = "MIS Account Creation";
                            DataSet ds = useracc.GetSMSTemplate(0, templatename);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                            }

                            string messageTemplate = TEMPLATE;
                            string Ua_name = objUA.UA_Name;
                            string password = pwd;
                            StringBuilder stringBuilder = new StringBuilder();
                            messageTemplate = messageTemplate.Replace("{#var#}", Ua_name);
                            messageTemplate = messageTemplate.Replace("{#var1#}", password);

                            if (txtMobile.Text.Trim() != string.Empty)
                            {
                                SendSMS(txtMobile.Text.Trim(), messageTemplate, TemplateID);
                            }

                            ShowPanel();
                            ShowPanelStudent();
                            // objCommon.DisplayMessage(this, "User Created Successfully !!", this.Page);
                            //ClearTxt();
                        }
                        else
                            lblSubmitStatus.Text = "Duplicate Record Found!";
                    }

                }
                else if (Convert.ToBoolean(ViewState["ExistUser"]) == true)
                {
                    CustomStatus cs = (CustomStatus)objUC.UpdateUserAcc(objUA, department);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                       // lblSubmitStatus.Text = "User Details Updated Successfully!!!";

                        if (OldEmailid != "")
                        {
                            if (OldEmailid != objUA.UA_Email)
                            {
                                string message = "Your MIS Account Email Id has been changed from " + OldEmailid + " to " + objUA.UA_Email + "";
                                string subject = "MIS Account Detail Modified:";

                                //------------Code for sending email,It is optional---------------
                                SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
                                objSendEmail.SendEmail(OldEmailid, message, subject);

                            }
                        }

                        if (OldMobileno != "")
                        {
                            if (OldMobileno != objUA.MOBILE)
                            {
                                string templatename = "MIS Account Mobile No. Change";
                                DataSet ds = useracc.GetSMSTemplate(0, templatename);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                    TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                }

                                if (txtMobile.Text.Trim() != string.Empty)
                                {
                                    SendSMS(OldMobileno, TEMPLATE.ToString(), TemplateID);
                                }
                            }
                        }
                        ShowPanel();
                        ShowPanelStudent();
                        objCommon.DisplayMessage(this.Page, "User info Updated Successfully !!", this.Page);
                        //ClearTxt();
                    }
                }
            }
            else
            {
                lblSubmitStatus.Text = "Plz Check User name By Click On The Check Button";
            }
            ShowPanel();
            ClearFileds();
            btnExporttoExcel.Enabled = true;
            ddlUserType.Enabled = true;
            ddlUserType.SelectedIndex = 0;
            ddlDept.ClearSelection();
            txtDesignatio.Text = string.Empty;   
            txtPassword.Enabled = false;
            ddlNewUser.Visible = false;
            txtPassword.Attributes["value"] = null;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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

            }
        }
        catch (Exception)
        {
        }
    }
 
    private void BindCheckList_ForCollegeName()
    {
        try
        {
            if (Session["usertype"].ToString() == "8")
            {
                // DataSet ds = objCommon.FillDropDown("USER_ACC U CROSS APPLY DBO.Split(UA_COLLEGE_NOS,',')S INNER JOIN ACD_COLLEGE_MASTER C ON (S.VALUE = C.COLLEGE_ID)", "COLLEGE_ID", "ISNULL(SHORT_NAME,'') + (CASE WHEN LOCATION IS NULL THEN '' ELSE ' ( 'END) +ISNULL(LOCATION,'') +  (CASE WHEN LOCATION IS NULL THEN '' ELSE ' ) 'END)+'-'+CODE AS SHORTNAME", "COLLEGE_ID>0 AND UA_NO = " + Convert.ToInt32(Session["userno"]) + "", "CODE");
                DataSet ds = objCommon.FillDropDown("USER_ACC U CROSS APPLY DBO.Split(UA_COLLEGE_NOS,',')S INNER JOIN ACD_COLLEGE_MASTER C ON (S.VALUE = C.COLLEGE_ID)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') AS COLLEGE_NAME", "COLLEGE_ID>0 AND UA_NO = " + Convert.ToInt32(Session["userno"]) + "", "CODE");

                if (ds.Tables[0].Rows[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    chkCollegeName.DataSource = ds;
                    chkCollegeName.DataTextField = ds.Tables[0].Columns["COLLEGE_NAME"].ToString();
                    chkCollegeName.DataValueField = ds.Tables[0].Columns["COLLEGE_ID"].ToString();
                    chkCollegeName.DataBind();
                }
                else
                {
                    chkCollegeName.DataSource = null;
                    chkCollegeName.DataBind();
                }
            }
            else
            {
                // DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "COLLEGE_ID", "SHORT_NAME+'('+CODE+')' AS SHORTNAME", "COLLEGE_ID>0", "CODE");
                //DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(SHORT_NAME,'') + (CASE WHEN LOCATION IS NULL THEN '' ELSE ' ( 'END) +ISNULL(LOCATION,'') +  (CASE WHEN LOCATION IS NULL THEN '' ELSE ' ) 'END)+'-'+CODE AS SHORTNAME", "COLLEGE_ID>0", "CODE");
                DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') AS COLLEGE_NAME", "COLLEGE_ID>0 AND OrganizationId=" + Convert.ToInt32(ddlOrg.SelectedValue), "CODE");
                if (ds.Tables[0].Rows[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    chkCollegeName.DataSource = ds;
                    chkCollegeName.DataTextField = ds.Tables[0].Columns["COLLEGE_NAME"].ToString();
                    chkCollegeName.DataValueField = ds.Tables[0].Columns["COLLEGE_ID"].ToString();
                    chkCollegeName.DataBind();
                }
                else
                {
                    chkCollegeName.DataSource = null;
                    chkCollegeName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id=")));
        else
            Response.Redirect(Request.Url.ToString());

    }

    //check for user is exist or not
    protected void btnCheckID_Click(object sender, EventArgs e)
    {
        ViewState["CheckBtn"] = true;
        ViewState["ExistUser"] = false;
        try
        {
            User_AccController objUC = new User_AccController();

            bool val = Convert.ToBoolean(objUC.CheckUser(txtUsername.Text));
            if (val.Equals(Convert.ToBoolean(false)))
            {

                objCommon.DisplayMessage(this, "User name already Exists !!", this.Page);
                //lblStatus.Text = "User id already Exists";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
                //Show Existing UserDetails
                SqlDataReader dr = objUC.GetSingleRecordByUAID(Convert.ToInt32(txtUsername.Text), Convert.ToInt32(ddlUserType.SelectedValue));
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        txtFName.Text = dr["Ua_FullName"] == null ? string.Empty : dr["Ua_FullName"].ToString().Trim();
                        txtEmail.Text = dr["UA_Email"] == null ? string.Empty : dr["UA_Email"].ToString().Trim();
                        // txtQtrRoomNo.Text = dr["UA_QtrNo"] == null ? string.Empty : dr["UA_QtrNo"].ToString().Trim();
                        txtDesignatio.Text = dr["UA_Desig"] == null ? string.Empty : dr["UA_Desig"].ToString().Trim();

                        if (dr["UA_DeptNo"] != null && !dr["UA_DeptNo"].ToString().Equals("0"))
                            ddlDept.SelectedValue = dr["UA_DeptNo"].ToString();

                        if (dr["UA_EmpDeptNo"] != null && !dr["UA_EmpDeptNo"].ToString().Equals("0"))
                            ddlSubDept.SelectedValue = dr["UA_EmpDeptNo"].ToString();

                        txtUsername.Text = dr["Ua_Name"] == null ? string.Empty : dr["Ua_Name"].ToString().Trim();
                        chkActive.Checked = dr["Ua_Status"].ToString().Equals("0") ? chkActive.Checked = false : chkActive.Checked = true;
                        chkDEC.Checked = dr["Ua_DEC"].ToString().Equals("0") ? chkActive.Checked = false : chkActive.Checked = true;
                        //Fill_TreeLinks(tvLinks, dr["Ua_ACC"].ToString());
                        txtMobile.Text = dr["UA_MOBILE"] == null ? string.Empty : dr["UA_MOBILE"].ToString().Trim();
                        ViewState["CheckBtn"] = true;
                        ViewState["ExistUser"] = false;
                    }
                    else
                    {
                        lblStatus.Text = "Create New User";
                    }
                    dr.Close();
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "User name Available For New Creation !!", this.Page);
                //lblStatus.Text = "This user id Available";
                //lblStatus.ForeColor = System.Drawing.Color.Green;

                ViewState["CheckBtn"] = true;
                ViewState["ExistUser"] = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.btnCheckID_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int ua_no = int.Parse(btnEdit.CommandArgument);
            //lnkID_Click(ua_no.ToString());
            ShowUserDetails(ua_no);

            btnSubmit.Enabled = true;
            ViewState["ExistUser"] = true;
            btnCheckID.Enabled = false;

            lblSubmitStatus.Text = "";
            btnExporttoExcel.Enabled = true;
            
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;
            txtFName.Enabled = true;
            txtMobile.Enabled = true;

            ddlUserType.Enabled = false;

            //<1.0.1>
            if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
            {
                divmessage.Visible = true;
            }
            else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
            {
                divmessage.Visible = false;
            }
            //</1.0.1>
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("UserAccountInfo", "rptUserAccountInfo_Parentwise.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Other Events
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearFileds();

        ShowPanel();

        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            chkActive.Checked = true;
            lvlinks.DataSource = null;
            lvlinks.DataBind();
            //ddlSubDept.Enabled = false;
            txtDesignatio.Enabled = false;
            txtFName.Enabled = false;
            txtMobile.Enabled = false;
            txtEmail.Enabled = false;

            divmessage.Visible = true;
            //added by satish-31102017
            pnlStudent.Visible = true;
            trSubDept.Visible = false;
            trDept.Visible = false;
            objCommon.FillDropDownList(ddlCollege, "ACD_college_master", "college_id", "college_name", "college_id>0 and ActiveStatus>0", "college_id");

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
  
           
        } //<1.0.1>      
        else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
        {

            chkActive.Checked = true;
            //ddlSubDept.Enabled = true;
            txtDesignatio.Enabled = true;
            divmessage.Visible = false;
            txtFName.Enabled = true;
            txtMobile.Enabled = true;
            txtEmail.Enabled = true;
            //added by satish-31102017
            trDept.Visible = true;
            trSubDept.Visible = true;
            lvlinks.DataSource = null;
            lvlinks.DataBind();
            pnlStudent.Visible = true;
            objCommon.FillDropDownList(ddlCollege, "ACD_college_master", "college_id", "college_name", "college_id>0 and ActiveStatus>0", "college_id");
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));


        }//</1.0.1>
        else
        {
            chkActive.Checked = true;
            //ddlSubDept.Enabled = true;
            txtDesignatio.Enabled = true;
            divmessage.Visible = false;
            txtFName.Enabled = true;
            txtMobile.Enabled = true;
            txtEmail.Enabled = true;
            //added by satish-31102017
            pnlStudent.Visible = false;
            trDept.Visible = true;
            trSubDept.Visible = true;
            //trpaydept.Visible = true;
            // lvlinks.DataSource = null;
            // lvlinks.DataBind();
        }
        //ClearCheck();
        lblStatus.Text = string.Empty;
        lblSubmitStatus.Text = string.Empty;
        btnExporttoExcel.Enabled = true;

        // Added By MR. MANISH WALDE on 09-JAN-2015
        // By default Load tree view links with the selected user type
       
        //tvLinks.Visible = true;
    }

    private void ClearFileds()
    {
        
        txtDesignatio.Text = string.Empty;
        //txtDesignatio.Text = "";
        txtEmail.Text = string.Empty;
        txtFName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtQtrRoomNo.Text = string.Empty;
        // txtUserID.Text = string.Empty;
        // txtUsername.Text = string.Empty;
        //ddlDept.SelectedIndex = 0;
        //ddlSubDept.SelectedIndex = 0;
        ddlNewUserType.SelectedIndex = 0;
        txtUsername.Text = string.Empty;
        //ddlUserType.SelectedIndex = 0;
        
        hdfPassword.Value = string.Empty;
        chkActive.Checked = false;
        chkDEC.Checked = false;
        lblPassStatus.Text = string.Empty;
        txtRemark.Text = string.Empty;

        ddlSubDept.ClearSelection();
        //ddlUserType.SelectedIndex = 0;
        // lvUsers.DataSource = null;
        // lvUsers.DataBind();
        // ViewState["ExistUser"] = null;
        //rfvPassword.Visible = true;
        txtMobile.Text = string.Empty;
        chkCollegeName.ClearSelection();

        ddlDegree.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        
    }
    /// <summary>
    /// Added By MR. MANISH WALDE on 09-JAN-2015
    /// By default Load tree view links with the selected user type
    /// </summary>
    /// <param name="usertypeid"></param>
    private void ShowDetails(int usertypeid)
    {
        UserRightsController objURC = new UserRightsController();
        SqlDataReader dr = objURC.GetSingleRecord(usertypeid);

        if (dr != null)
        {
            if (dr.Read())
            {
                //Bind the TreeView By default according to the rights of selected user
                string lnks = dr["USERRIGHTS"] == null ? "" : dr["USERRIGHTS"].ToString();
                //Fill_Default_TreeLinks(tvLinks, lnks);
            }
        }

        if (dr != null)
            dr.Close();
    }


    //protected void chkActive_CheckedChanged(object sender, EventArgs e)
    //{
    //    SetStatus();
    //}
    #endregion

    #region User Methods

    private void PopulateDropDownList()
    {
        try
        {
            DataSet dsOrg = objCommon.FillDropDown("tblConfigOrganizationMaster", "OrganizationId", "OrgName", "ISNULL(ActiveStatus,0)=1 AND OrganizationId =" + Convert.ToInt32(Session["OrgId"]), string.Empty);
            if (dsOrg.Tables != null && dsOrg.Tables.Count > 0 && dsOrg.Tables[0].Rows.Count > 0)
            {
                ddlOrg.DataSource = dsOrg;
                ddlOrg.DataTextField = dsOrg.Tables[0].Columns[1].ToString();
                ddlOrg.DataValueField = dsOrg.Tables[0].Columns[0].ToString();
                ddlOrg.DataBind();
                ddlOrg.SelectedIndex = 1;
            }

            if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "11" || Session["userdeptno"] != null || Session["usertype"].ToString() == "1")
            {
                trDept.Visible = true;
                trSubDept.Visible = true;
                ddlDept.Items.Clear();
                if (Session["userdeptno"] != null && Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
                {
                    //ddlDept.Items.Add(new ListItem("Please Select", "0"));
                    ddlDept.Items.Clear();
                    DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACDDEPT");
                    ddlDept.DataSource = dsAcd;
                    ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
                    ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
                    ddlDept.DataBind();
                    //objCommon.FillListBox(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO=" + Session["userdeptno"].ToString(), string.Empty);

                }
                else
                    if (Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1")
                    {
                        ddlDept.Items.Clear();
                        DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACDDEPT");
                        ddlDept.DataSource = dsAcd;
                        ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
                        ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
                        ddlDept.DataBind();
                        //objCommon.FillListBox(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO > 0", string.Empty);


                        //Payroll Department 
                        DataSet dsPayroll = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLPAYROLLDEPT");
                        ddlSubDept.DataSource = dsPayroll;
                        ddlSubDept.DataTextField = dsPayroll.Tables[0].Columns[1].ToString();
                        ddlSubDept.DataValueField = dsPayroll.Tables[0].Columns[0].ToString();
                        ddlSubDept.DataBind();
                    }
                    else
                    {
                        trDept.Visible = false;
                        trSubDept.Visible = false;
                        ddlDept.Items.Clear();
                        ddlSubDept.Items.Clear();
                    }
            }
            else
            {
                trDept.Visible = false;
                trSubDept.Visible = false;
                ddlDept.Items.Clear();
            }
            DataSet dsUser = null;
            if (Session["usertype"].ToString() == "1")
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");
            else
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") ? "USERTYPEID = 3" : "PARENT_USERTYPE = " + Session["usertype"].ToString(), "USERTYPEID");

            ddlUserType.DataSource = dsUser;
            ddlUserType.DataTextField = dsUser.Tables[0].Columns[1].ToString();
            ddlUserType.DataValueField = dsUser.Tables[0].Columns[0].ToString();
            ddlUserType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void FillNewUserList(int ua_type) 
    {
        DataSet dsUser = null;
        if (ua_type != 2 && ua_type != 14)
        {
          
            if (Session["usertype"].ToString() == "1")
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID<>0 and USERTYPEID<>2 and USERTYPEID <> " + ddlUserType.SelectedValue + " ", "USERTYPEID");
            else
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") ? "USERTYPEID = 3" : "PARENT_USERTYPE = " + Session["usertype"].ToString() + " and USERTYPEID<>2 and USERTYPEID <> " + ddlUserType.SelectedValue + " ", "USERTYPEID");

            ddlNewUserType.Items.Clear();
            ddlNewUserType.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlNewUserType.DataSource = dsUser;
            ddlNewUserType.DataTextField = dsUser.Tables[0].Columns[1].ToString();
            ddlNewUserType.DataValueField = dsUser.Tables[0].Columns[0].ToString();
            ddlNewUserType.DataBind();

            ddlNewUser.Visible = true;
        }
        else
        {
            ddlNewUserType.DataSource = dsUser;
            ddlNewUserType.DataBind();
            ddlNewUser.Visible = false;
        }

    }
    private void ClearTxt()
    {
        txtDesignatio.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtQtrRoomNo.Text = string.Empty;
        // txtUserID.Text = string.Empty;
        //txtUsername.Text = string.Empty;
        //ddlDept.SelectedIndex = 0;
        ddlSubDept.SelectedIndex = 0;
        ddlNewUserType.SelectedIndex = 0;

        hdfPassword.Value = string.Empty;
        chkActive.Checked = false;
        chkDEC.Checked = false;
        lblPassStatus.Text = string.Empty;
        ddlUserType.SelectedIndex = 0;
        //lvUsers.DataSource = null;
        //lvUsers.DataBind();
        //ViewState["ExistUser"] = null;
        //rfvPassword.Visible = true;
        txtMobile.Text = string.Empty;

    }

    private void lnkID_Click(string id)
    {
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + id);
    }

    //private void bindlist(string category, string searchtext)
    //{
    //    User_AccController objUAC = new User_AccController();
    //    DataSet ds = objUAC.FindUser(searchtext, category);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvUsers.DataSource = ds;
    //        lvUsers.DataBind();
    //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
    //    }
    //    else
    //        lblNoRecords.Text = "Total Records : 0";
    //}

    private void ShowUserDetails(int idno)
    {
        try
        {
            User_AccController objACC = new User_AccController();
            DataTableReader dtr;

            //dtr = objACC.GetUserByUANo(idno);
            dtr = objACC.GetUserByUANo_New(idno, Convert.ToInt32(Session["OrgId"]));
            if (ViewState["action"].ToString() == "edit")
            {
                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        ddlUserType.SelectedValue = dtr["UA_TYPE"] == DBNull.Value ? "0" : dtr["UA_TYPE"].ToString();
                        txtFName.Text = dtr["UA_FULLNAME"] == DBNull.Value ? string.Empty : dtr["UA_FULLNAME"].ToString();
                        txtDesignatio.Text = dtr["UA_DESIG"] == DBNull.Value ? string.Empty : dtr["UA_DESIG"].ToString();
                        //ddlSubDept 
                        //if(ddlUserType.SelectedValue=="3" || ddlUserType.SelectedValue=="11")
                        //    ddlSubDept.SelectedValue = dtr["UA_EMPDEPTNO"] == DBNull.Value ? "0" : dtr["UA_EMPDEPTNO"].ToString();
                        //else
                        ddlSubDept.SelectedValue = dtr["UA_EMPDEPTNO"] == DBNull.Value ? "0" : dtr["UA_EMPDEPTNO"].ToString();
                        //ddlDept.Items.Add(new ListItem("Please Select", "0"));
                        ddlDept.Items.Clear();
                        DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACDDEPT");
                        ddlDept.DataSource = dsAcd;
                        ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
                        ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
                        ddlDept.DataBind();
                        string dept = dtr["UA_DEPTNO"].ToString();
                        string[] DeptNo = new string[50];
                        DeptNo = dept.Split(',');

                        for (int i = 0; i < DeptNo.Length; i++)
                        {
                            for (int j = 0; j < ddlDept.Items.Count; j++)
                            {
                                if (ddlDept.Items[j].Value.ToString() == DeptNo[i]) //.Split(',')[100]
                                {
                                    ddlDept.Items[j].Selected = true;
                                }
                            }

                        }
                        if (!DeptNo.ToString().Equals(string.Empty))
                        {

                        }

                        txtQtrRoomNo.Text = dtr["UA_QTRNO"] == null ? string.Empty : dtr["UA_QTRNO"].ToString();
                        txtUsername.Text = dtr["UA_NAME"] == DBNull.Value ? string.Empty : dtr["UA_NAME"].ToString();
                        txtUsername.ToolTip = idno.ToString();
                        hdfPassword.Value = dtr["UA_PWD"] == DBNull.Value ? string.Empty : dtr["UA_PWD"].ToString();
                        txtPassword.Attributes["value"]  = clsTripleLvlEncyrpt.ThreeLevelDecrypt(hdfPassword.Value);
                       
                        chkActive.Checked = dtr["UA_STATUS"].ToString() == "0" ? true : false;
                        chkDEC.Checked = dtr["UA_DEC"].ToString() == "1" ? true : false;

                        txtEmail.Text = dtr["UA_EMAIL"] == DBNull.Value ? string.Empty : dtr["UA_EMAIL"].ToString();
                        lblPassStatus.Text = "To use existing password, keep blank.";
                        txtMobile.Text = dtr["UA_MOBILE"] == DBNull.Value ? string.Empty : dtr["UA_MOBILE"].ToString();

                        txtRemark.Text = dtr["REMARK"] == DBNull.Value ? string.Empty : dtr["REMARK"].ToString();


                        //Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == DBNull.Value ? string.Empty : dtr["UA_ACC"].ToString());
                        //ddlSupervisorstatus.SelectedValue = dtr["DRCSTATUS"] == DBNull.Value ? string.Empty : dtr["DRCSTATUS"].ToString();
                        //ddlSupervisorstatus.Enabled = false;

                        ViewState["ExistUser"] = null;
                        //rfvPassword.Visible = false;
                        string[] num = new string[50];
                        string col = dtr["UA_COLLEGE_NOS"] == DBNull.Value ? string.Empty : dtr["UA_COLLEGE_NOS"].ToString();
                        num = col.ToString().Split(',');
                        for (int m = 0; m < num.Length; m++)
                        {
                            for (int i = 0; i <= chkCollegeName.Items.Count - 1; i++)
                            {
                                if (chkCollegeName.Items[i].Value == num[m])
                                {
                                    chkCollegeName.Items[i].Selected = true;
                                }
                            }
                        }
                        int ua_type = Convert.ToInt32(dtr["UA_TYPE"] == DBNull.Value ? "0" : dtr["UA_TYPE"].ToString());
                        FillNewUserList(ua_type);
                        
                    }
                }
            }
            else
                if (dtr.Read())
                {
                    //if (Convert.ToInt16(Session["userno"].ToString()) == idno) 
                    Session["userlinks"] = dtr["UA_ACC"].ToString();
                    //Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == null ? string.Empty : dtr["UA_ACC"].ToString());
                }
            dtr.Close();



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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

    private void SetStatus()
    {
        if (chkActive.Checked == true)
        {
            chkActive.ForeColor = System.Drawing.Color.Green;
            chkActive.Text = "Active";
        }
        else
        {
            chkActive.ForeColor = System.Drawing.Color.Red;
            chkActive.Text = "InActive";
        }
    }

    private void ShowPanel()
    {
        DataSet ds = null;
        int dept = 0;
        if (ddlDept.SelectedValue == null || ddlDept.SelectedValue == "")
        {
            dept = 0;
        }
        else
        {
            dept = 0;
            //dept = Convert.ToInt32(ddlDept.SelectedValue);
        }
        if (Session["usertype"].ToString() == "1")
        {
            if (ddlUserType.SelectedValue == "2")
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0", "UA_TYPE,UA_NO");

                //comment by satish
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "A.UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "A.UA_NO IS NOT NULL AND  UA_STATUS = 0 AND ISNULL(CAN,0) = 0  AND UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
            //<1.0.1>
            else if (ddlUserType.SelectedValue == "14")
            {
                // ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "UA_NO,UA_FULLNAME,UA_NAME,STUDNAME,REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL  AND  UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue) + " AND CAN=0 AND ADMCAN=0 AND (DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " OR " + Convert.ToInt32(ddlCollege.SelectedValue) + "=0)", "UA_FULLNAME ,UA_TYPE,UA_NO");
            }
            else
            {

                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME ,'' STUDNAME,'' REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL  AND (UA_DEPTNO=" + Convert.ToInt32(dept) + " OR " + Convert.ToInt32(dept) + "=0) AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
            if (Convert.ToInt32(ddlSubDept.SelectedValue) > 0)
            {
                //// ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND (UA_EMPDEPTNO=" + Convert.ToInt32(ddlSubDept.SelectedValue) + " OR " + Convert.ToInt32(ddlSubDept.SelectedValue) + "=0)" + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID ", "UA_NO,UA_FULLNAME,UA_NAME,'' STUDNAME,'' REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL  AND (UA_EMPDEPTNO=" + Convert.ToInt32(ddlSubDept.SelectedValue) + " OR " + Convert.ToInt32(ddlSubDept.SelectedValue) + "=0)" + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
        }
        else
            ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID ", "UA_NO,UA_FULLNAME,UA_NAME,'' STUDNAME,'' REGNO", "' ' AS UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0 " + (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1" ? " AND UA_TYPE= 3 AND (UA_DEC IS NULL OR UA_DEC = 0) AND UA_DEPTNO= " + Session["userdeptno"].ToString() : Session["usertype"].ToString() == "4" ? " AND UA_TYPE= 3 AND UA_DEC = 1" : " AND R.PARENT_USERTYPE =" + Session["usertype"].ToString()), "UA_TYPE,UA_NO,UA_NAME");
        //</1.0.1>

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                // pnlUser.Visible = true;
                // lblEmpty.Visible = false;
                foreach (RepeaterItem item in lvlinks.Items)
                {
                    //Label lblUserpass = item.FindControl("lblUserpass") as Label;
                    //lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());

                    ///STATUS COLOR CHANGE
                    Label Status = item.FindControl("lblUStatus") as Label;
                    if (Status.Text.Trim().ToUpper() == "0")
                    {
                        Status.Text = "Active";
                        Status.Style.Add("color", "Green");
                    }
                    if (Status.Text.Trim().ToUpper() == "1")
                    {
                        Status.Text = "InActive";
                        Status.Style.Add("color", "Red");
                    }
                }
                lvlinks.Visible = true;
            }
            else
            {
                // objCommon.DisplayMessage(this.updEdit, "No Record Found!", this.Page);
                //lblEmpty.Visible = true;
                lvlinks.Visible = false;
                // pnlUser.Visible = false;
            }
        }
    }

    private void ShowPanelStudent()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1")
        {
            //<1.0.1>
            if (ddlUserType.SelectedValue == "2")
            {

                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "A.UA_NO,UA_FULLNAME,UA_NAME,S.STUDNAME,S.REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue) + " AND CAN=0 AND ADMCAN=0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "   AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "UA_FULLNAME");

            }
            else if (ddlUserType.SelectedValue == "14")
            {
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "UA_NO,UA_FULLNAME,UA_NAME,S.STUDNAME,S.REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL  AND  UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue) + " AND CAN=0 AND ADMCAN=0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), " UA_TYPE,UA_NO");
            }
            else
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME,'' STUDNAME,'' REGNO", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL  AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
        }
        else
            ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID ", "UA_NO,UA_FULLNAME,UA_NAME,'' STUDNAME,'' REGNO", "' ' AS UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0 " + (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1" ? " AND UA_TYPE= 3 AND (UA_DEC IS NULL OR UA_DEC = 0) AND UA_DEPTNO= " + Session["userdeptno"].ToString() : Session["usertype"].ToString() == "4" ? " AND UA_TYPE= 3 AND UA_DEC = 1" : " AND R.PARENT_USERTYPE =" + Session["usertype"].ToString()), "UA_TYPE,UA_NO,UA_NAME");
        //</1.0.1>
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                // pnlUser.Visible = true;
                // lblEmpty.Visible = false;
                foreach (RepeaterItem item in lvlinks.Items)
                {
                    //Label lblUserpass = item.FindControl("lblUserpass") as Label;
                    //lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());

                    ///STATUS COLOR CHANGE
                    Label Status = item.FindControl("lblUStatus") as Label;
                    if (Status.Text.Trim().ToUpper() == "0")
                    {
                        Status.Text = "Active";
                        Status.Style.Add("color", "Green");
                    }
                    if (Status.Text.Trim().ToUpper() == "1")
                    {
                        Status.Text = "InActive";
                        Status.Style.Add("color", "Red");
                    }
                }
                lvlinks.Visible = true;
            }
            else
            {
                // objCommon.DisplayMessage(this.updEdit, "No Record Found!", this.Page);
                // lblEmpty.Visible = true;
                lvlinks.Visible = false;
                // pnlUser.Visible = false;
            }
        }
        else
        {
            // objCommon.DisplayMessage(this.updEdit, "Error!", this.Page);
            // pnlUser.Visible = false;
            // lblEmpty.Visible = true;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("create_user")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //   // url += "&param=@P_PARENT_UA_NO	=" + Session["userno"].ToString() + ",@P_PARENT_UA_TYPE=" + ddlUserType.SelectedValue.ToString() + ",@P_PARENT_DEPT_NO=" + Session["userdeptno"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

        //    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PARENT_DEPT_NO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_PARENT_UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue);
        //     // + ",@P_COLLEGEID =" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);

        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PARENT_DEPT_NO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_PARENT_UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue)
        //       + ",@P_COLLEGEID =" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //    sb.Append(@"window.open('" + url + "','','" + features + "');");

        //    ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "create_user.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("create_user")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PARENT_UA_TYPE=" + ddlUserType.SelectedValue + ",@P_PARENT_DEPT_NO=" + ddlDept.SelectedValue +
                ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue +
                ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue.Trim();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_rptRevaluationAndScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
    #endregion


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
        //<1.0.1>
        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            divmessage.Visible = true;
        }
        else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
        {
            divmessage.Visible = false;
        }
        //</1.0.1>
        
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPanelStudent();
        //<1.0.1>
        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            divmessage.Visible = true;
        }
        else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
        {
            divmessage.Visible = false;
        }
        //</1.0.1>

    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowPanel();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "a.DEGREENO");
        }
        int COLLEGEID = Convert.ToInt32(ddlCollege.SelectedValue);
        chkCollegeName.SelectedValue = COLLEGEID.ToString();
        //<1.0.1>
        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            divmessage.Visible = true;
        }
        else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
        {
            divmessage.Visible = false;
        }
        //</1.0.1>
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
        }
        //<1.0.1>
        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            divmessage.Visible = true;
        }
        else if (Convert.ToInt32(ddlUserType.SelectedValue) == 14)
        {
            divmessage.Visible = false;
        }
        //</1.0.1>
    }
    protected void ddlSubDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPanel();
    }
  
    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCheckList_ForCollegeName();
    }

    //Added BY Ruchika Dhakate on 13.09.2022
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        try
        {

            int PARENTTYE = Convert.ToInt32(ddlUserType.SelectedValue);
            string PARENTDEPT = "0";
            int COLLEGEID = Convert.ToInt32(ddlCollege.SelectedValue);
            int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            int BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
            int SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);


            DataSet ds = Export_Excel(PARENTTYE, PARENTDEPT, COLLEGEID, DEGREENO, BRANCHNO, SEMESTERNO);
            GridView GVEmpChallan = new GridView();

            if (ds.Tables[0].Rows.Count > 0)
            {

                GVEmpChallan.DataSource = ds.Tables[0];
                GVEmpChallan.DataBind();

                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = "NEW USER " + "-" + ddlUserType.SelectedItem.Text;
                HeaderCell.ColumnSpan = ds.Tables[0].Columns.Count;
                HeaderCell.BackColor = System.Drawing.Color.Navy;
                HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 15;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


                ////Header Row 2
                //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell HeaderCell3 = new TableCell();

                //// HeaderCell3.Text = " Admission Batch : " + ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                //HeaderCell3.Text = " Admission Batch : " + "-" + ddlDept.SelectedItem.Text;
                //HeaderCell3.ColumnSpan = 18;   //Added by column no 14-06-2022 
                //HeaderCell3.BackColor = System.Drawing.Color.Navy;
                //HeaderCell3.ForeColor = System.Drawing.Color.White;
                //HeaderCell3.Font.Bold = true;
                //HeaderCell3.Font.Size = 15;
                //HeaderCell3.Attributes.Add("style", "text-align:center !important;");
                //HeaderGridRow1.Cells.Add(HeaderCell3);
                //GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


                string attachment = "attachment; filename=" + "New User Excel.xls";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVEmpChallan.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    //objCommon.ShowError(Page, "create_user.aspx.btnExporttoExcel() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Added BY Ruchika Dhakate on 13.09.2022
    public DataSet Export_Excel(int PARENTTYE, string PARENTDEPT, int COLLEGEID, int DEGREENO, int BRANCHNO, int SEMESTERNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_PARENT_UA_TYPE ", PARENTTYE);
            objParams[1] = new SqlParameter("@P_PARENT_DEPT_NO ", PARENTDEPT);
            objParams[2] = new SqlParameter("@P_COLLEGEID ", COLLEGEID);
            objParams[3] = new SqlParameter("@P_DEGREENO  ", DEGREENO);
            objParams[4] = new SqlParameter("@P_BRANCHNO ", BRANCHNO);
            objParams[5] = new SqlParameter("@P_SEMESTERNO ", SEMESTERNO);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_USER_ACC_INFO_PARENTWISE", objParams);


        }
        catch (Exception ex)
        {
            return ds;
            //   throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.EmployeeARREAR_DIFF-> " + ex.ToString());
        }
        return ds;
    }

    protected void ddlNewUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //PopulateDropDownList();
    }
}