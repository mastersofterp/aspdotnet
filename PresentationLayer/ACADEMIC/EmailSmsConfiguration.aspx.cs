using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

//Email Namespcae

using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Text;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
public partial class ACADEMIC_EmailSmsConfiguration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    ConfigController CFC = new ConfigController();
    TemplateType objTemplateType = new TemplateType();
    TemplateTypeController objTemplateTypeController = new TemplateTypeController();
    SmsTemplateType objSmsTemplate = new SmsTemplateType();
    SmsTemplateTypeController objSmsTemplateController = new SmsTemplateTypeController();

    // ConfigController CFC = new ConfigController();
    CustomStatus cs = new CustomStatus();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindListViewDataServiceProvider();
                BindListViewEmailServices();
                BindListViewSMS();
                ListViewBindWhatsaap();
                objCommon.FillDropDownList(ddlServiceProvider, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=1", "");
                objCommon.FillDropDownList(ddlServiceProviderSMS, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=2", "");
                objCommon.FillDropDownList(ddlServiceProviderWhat, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=3", "");
                objCommon.FillDropDownList(ddlServiceProviderName, "SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICENAME", "", "");
                ViewState["SERVICE_NO"] = "Add";
                ViewState["EMAIL_NO"] = "Add";
                ViewState["SMS_NO"] = "Add";
                ViewState["WHATSAAP_NO"] = "Add";
                PopulateDropDownList();
                //objCommon.SetLabelData("0");//for label
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022


                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                BindListView();
                FillDropDown();
                BindListViewSmsTemplate();
                BindWAListView();
                BindListViewWhatsappTemplate();
            }

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EmailSmsConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EmailSmsConfiguration.aspx");
        }
    }
    #region Service Provider Start
    //***************************************Service Provider Start******************************************************** 
    protected void btnService_Click(object sender, EventArgs e)
    {
        try
        {
            int CongiNo = Convert.ToInt32(ddlConfiguration.SelectedValue);
            string CongiName = ddlConfiguration.SelectedItem.Text;
            string ServicePName = ddlServiceProviderName.SelectedItem.Text;// txtServiceProvideName.Text;
            int Serviceno = 0;
            //int priority = Convert.ToInt32(ddlPriority.SelectedValue);
            //if (CongiNo == 1)
            //{
            //    string sp_Name = string.Empty; string sp_Paramters = string.Empty; string sp_Values = string.Empty; DataSet dsCheckPriority = null;
            //    sp_Name = "PKG_SP_CHECK_PRIORITY_FOR_EMAIL";
            //    sp_Paramters = "@P_PRIORITY,@P_SERVICE";
            //    sp_Values = "" + priority + "," + ServicePName;
            //    dsCheckPriority = objCommon.DynamicSPCall_Select(sp_Name, sp_Paramters, sp_Values);
            //    if (dsCheckPriority.Tables.Count > 0 && dsCheckPriority.Tables[0].Rows.Count > 0)
            //    {
            //        if (dsCheckPriority.Tables[0].Rows[0]["OUTPUT"].ToString() != "")
            //        {
            //            string op = dsCheckPriority.Tables[0].Rows[0]["OUTPUT"].ToString().TrimEnd();
            //            objCommon.DisplayMessage(this.updRule, "The selected priority " + priority + " is already set to " + op + ".", this.Page);
            //            return;
            //        }
            //    }
            //}
            if (ViewState["SERVICE_NO"].ToString() != "Add")
            {
                Serviceno = Convert.ToInt32(ViewState["SERVICE_NO"].ToString());
            }
            else
            {
                int count = Convert.ToInt32(objCommon.LookUp("EMAIL_SMS_SERVICE_PROVIDER_NAME", "COUNT(SERVICE_NO)", "SERVICE_PROVIDER_NAME='" + ServicePName.ToString() + "'"));
                if (count != 0)
                {
                    objCommon.DisplayMessage(this, "Service Provider Alredy Exists..", this.Page);
                    return;
                }
            }
            string Parameter = "";
            foreach (ListItem items in lstParameter.Items)
            {
                if (items.Selected == true)
                {
                    Parameter += items.Value + ',';
                }
            }
            Parameter = Parameter.TrimEnd(',');
            int activeStatus = 0;
            if (hfdProv.Value.ToLower() == "true")
            {
                activeStatus = 1;
            }
            else
            {
                activeStatus = 0;
            }
            CustomStatus cs = (CustomStatus)CFC.AddServiceProvider(CongiNo, CongiName, ServicePName, Serviceno, Parameter,// priority,  Added by Nikhil L.
            activeStatus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["SERVICE_NO"] = "Add";
                ddlConfiguration.SelectedValue = "0";
                //txtServiceProvideName.Text = "";
                ddlServiceProviderName.SelectedValue = "0";
                BindListViewDataServiceProvider();
                Clear();
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                return;
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["SERVICE_NO"] = "Add";
                ddlConfiguration.SelectedValue = "0";
                //txtServiceProvideName.Text = "";
                ddlServiceProviderName.SelectedValue = "0";
                BindListViewDataServiceProvider();
                Clear();
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                return;
            }
        }
        catch { }
    }
    protected void Clear()
    {
        ddlConfiguration.SelectedValue = "0";
        //txtServiceProvideName.Text = "";
        ddlServiceProviderName.SelectedValue = "0";
        lstParameter.Items.Clear();
        ddlConfiguration.Enabled = true;
        ddlServiceProvider.SelectedValue = "0";
        ddlServiceProvider_SelectedIndexChanged(new object(), new EventArgs());
        ddlServiceProviderSMS.SelectedValue = "0";
        ddlServiceProviderSMS_SelectedIndexChanged(new object(), new EventArgs());
        ddlServiceProviderWhat.SelectedValue = "0";
        ddlServiceProviderWhat_SelectedIndexChanged(new object(), new EventArgs());
        //ddlPriority.SelectedIndex = 0;
        //divPriority.Visible = false;
        ddlServiceProviderName.Enabled = true;
    }
    protected void btnServiceCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void BindListViewDataServiceProvider()
    {
        objCommon.FillDropDownList(ddlServiceProvider, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=1", "");
        objCommon.FillDropDownList(ddlServiceProviderSMS, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=2", "");
        objCommon.FillDropDownList(ddlServiceProviderWhat, "EMAIL_SMS_SERVICE_PROVIDER_NAME", "SERVICE_NO", "SERVICE_PROVIDER_NAME", "CONFIG_NO=3", "");

        string SP_Name2 = "PKG_GET_SERVICE_PROVIDER_NAME";
        string SP_Parameters2 = "@P_CONFIG_NO";
        string Call_Values2 = "" + 1 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        //    DataSet ds = objCommon.FillDropDown("EMAIL_SMS_SERVICE_PROVIDER_NAME", "*", "SERVICE_NO", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvServiceProvider.DataSource = ds;
            lvServiceProvider.DataBind();
            foreach (ListViewDataItem lv in lvServiceProvider.Items)
            {
                Label lblStatus = lv.FindControl("lblStatusProv") as Label;
                if (lblStatus.Text == "Active")
                {
                    lblStatus.CssClass = "badge badge-success";
                }
                else
                {
                    lblStatus.CssClass = "badge badge-danger";
                }
            }
        }
        else
        {
            lvServiceProvider.DataSource = null;
            lvServiceProvider.DataBind();
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int Serviceno = int.Parse(btnEdit.CommandArgument);
        ViewState["SERVICE_NO"] = Serviceno;
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_SERVICE_PROVIDER_NAME", "ISNULL(SER_PRONO,0) AS SERPRONO,*", "SERVICE_NO", "SERVICE_NO=" + Serviceno, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            //txtServiceProvideName.Text = ds.Tables[0].Rows[0]["SERVICE_PROVIDER_NAME"].ToString();
            ddlServiceProviderName.SelectedValue = ds.Tables[0].Rows[0]["SERPRONO"].ToString();
            ddlServiceProviderName.Enabled = false;
            ddlConfiguration.Enabled = false;
            ddlConfiguration.SelectedValue = ds.Tables[0].Rows[0]["CONFIG_NO"].ToString();
            ddlConfiguration_SelectedIndexChanged(new object(), new EventArgs());
            string[] Parameter = Convert.ToString(ds.Tables[0].Rows[0]["PARAMETER"]).Split(',');

            foreach (string s in Parameter)
            {
                foreach (ListItem item in lstParameter.Items)
                {
                    if (s == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            string status = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().Trim();

            if (status == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetStateProvider(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetStateProvider(false);", true);
            }
            //if (ds.Tables[0].Rows[0]["PRIORITY_SEQ"].ToString().Equals("0") || ds.Tables[0].Rows[0]["PRIORITY_SEQ"].ToString() == "" || ds.Tables[0].Rows[0]["PRIORITY_SEQ"].ToString() == null)
            //{

            //}
            //else
            //{
            //    ddlPriority.SelectedValue = ds.Tables[0].Rows[0]["PRIORITY_SEQ"].ToString();
            //}
        }
    }
    protected void ddlConfiguration_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(lstParameter, "EMAIL_SMS_CONFIG_LABLE", "LB_NO", "LABEL_NAME", "EMAIL_SMSNO=" + ddlConfiguration.SelectedValue, "");
        //if (ddlConfiguration.SelectedValue == "1")
        //{
        //    divPriority.Visible = true;
        //}
        //else
        //{
        //    divPriority.Visible = false;
        //}
    }
    #endregion

    #region Email Configuration Start
    //***************************************Email Configuration Start******************************************************** 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ServiceNo = Convert.ToInt32(ddlServiceProvider.SelectedValue);
            string SMTP_Server = txtSMTPServer.Text;
            string SMTP_Server_Port = txtSMTPServerPort.Text;
            string CKey_UserId = txtCKey.Text;
            string Email_ID = txtEmailID.Text;
            string Password = txtPassword.Text;
            int Active = 0;
            if (hfdStat.Value == "true")
            {
                Active = 1;
            }
            int EMAIL_NO = 0;
            if (ViewState["EMAIL_NO"].ToString() != "Add")
            {
                EMAIL_NO = Convert.ToInt32(ViewState["EMAIL_NO"].ToString());
            }
            string apiKey = txtAPI.Text.ToString().Equals("") ? "" : txtAPI.Text.ToString().TrimEnd();
            CustomStatus cs = (CustomStatus)CFC.AddEmailServiceProvider(EMAIL_NO, ServiceNo, SMTP_Server, SMTP_Server_Port, CKey_UserId, Email_ID, Password, Active, Convert.ToInt32(Session["userno"].ToString()), apiKey);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["EMAIL_NO"] = "Add";
                BindListViewEmailServices();
                EmailClear();
                objCommon.DisplayMessage(this, "Record Saved  Successfully.", this.Page);
                return;

            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["EMAIL_NO"] = "Add";
                BindListViewEmailServices();
                EmailClear();
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                return;
            }
        }
        catch { }
    }

    protected void BindListViewEmailServices()
    {
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION  SC INNER JOIN EMAIL_SMS_SERVICE_PROVIDER_NAME SS ON (SC.SERVICE_NO=SS.SERVICE_NO)", "*", "SERVICE_NO", "CONFIG_TYPE=1", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvEmailServices.DataSource = ds;
            lvEmailServices.DataBind();
        }
        else
        {
            lvEmailServices.DataSource = null;
            lvEmailServices.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvEmailServices.Items)
        {
            Label Status = dataitem.FindControl("lblEmailStatus") as Label;
            string Statuss = (Status.Text);
            if (Statuss == "0")
            {
                Status.Text = "Deactive";
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.Text = "Active";
                Status.CssClass = "badge badge-success";
            }

        }

    }
    protected void btnEditEmail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int EmailNo = int.Parse(btnEdit.CommandArgument);
        ViewState["EMAIL_NO"] = EmailNo;
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION", "*", "EMAILSMS_NO", "EMAILSMS_NO=" + EmailNo, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlServiceProvider.Enabled = false;
            ddlServiceProvider.SelectedValue = ds.Tables[0].Rows[0]["SERVICE_NO"].ToString();
            ddlServiceProvider_SelectedIndexChanged(new object(), new EventArgs());
            txtSMTPServer.Text = ds.Tables[0].Rows[0]["SMTP_SERVER"].ToString();
            txtSMTPServerPort.Text = ds.Tables[0].Rows[0]["SMTP_PORT"].ToString();
            txtCKey.Text = ds.Tables[0].Rows[0]["CKEY_USERID"].ToString();
            txtEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0]["PASSWORDS"].ToString();
            txtAPI.Text = ds.Tables[0].Rows[0]["SENDGRID_API_KEY"].ToString();
            string status = ds.Tables[0].Rows[0]["STATUS"].ToString().Trim();

            if (status == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetEmail(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetEmail(false);", true);
            }
        }
    }
    protected void EmailClear()
    {
        ddlServiceProvider.SelectedValue = "0";
        txtSMTPServer.Text = "";
        txtSMTPServerPort.Text = "";
        txtCKey.Text = "";
        txtEmailID.Text = "";
        txtPassword.Text = "";
        txtAPI.Text = "";
        ddlServiceProvider.Enabled = true;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EmailClear();
    }
    protected void ddlServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivSMTPServer.Visible = false; SMTPServerPort.Visible = false; DivCKey.Visible = false; DivEmailID.Visible = false; DivPassword.Visible = false; divAPI.Visible = false;
        string PARAMETER = objCommon.LookUp("EMAIL_SMS_SERVICE_PROVIDER_NAME", "PARAMETER", "SERVICE_NO='" + ddlServiceProvider.SelectedValue + "'");
        string[] PARAMETERS = PARAMETER.ToString().Split(',');
        foreach (string s in PARAMETERS)
        {
            if (s == "1")
            {
                DivSMTPServer.Visible = true;
            }
            else if (s == "2")
            {
                SMTPServerPort.Visible = true;
            }
            else if (s == "3")
            {
                DivCKey.Visible = true;
            }
            else if (s == "4")
            {
                DivEmailID.Visible = true;
            }
            else if (s == "5")
            {
                DivPassword.Visible = true;
            }
            else if (s == "13")
            {
                divAPI.Visible = true;
            }
        }

    }
    #endregion

    #region SMS Configuration Start
    //***************************************SMS Configuration Start******************************************************** 

    protected void btnSubmitSMS_Click(object sender, EventArgs e)
    {
        try
        {
            int ServiceNo = Convert.ToInt32(ddlServiceProviderSMS.SelectedValue);
            string SMSAPI = TxtSMSAPI.Text;
            string SMSUserID = txtSMSUserID.Text;
            string EmailSMS = txtEmailSMS.Text;
            string PasswordSMS = txtPasswordSMS.Text;
            string SMSParameterI = txtSMSParameterI.Text;
            string SMSParameterII = txtSMSParameterII.Text;
            string SMSProvider = txtSMSProvider.Text.Equals("") ? "" : txtSMSProvider.Text.ToString().Trim();
            string SMSTempID = txtTemplateId.Text.Equals("") ? "" : txtTemplateId.Text.ToString().Trim();
            string SMSTemp = txtTemplate.Text.Equals("") ? "" : txtTemplate.Text.ToString().Trim();
            string SMSTemplateName = txtTemplateName.Text.Equals("") ? "" : txtTemplateName.Text.ToString().Trim();
            string SMSUrl = txtSMSUrl.Text.ToString().Equals("") ? "" : txtSMSUrl.Text.ToString().Trim();
            int Active = 0;
            if (hdnSms.Value == "true")
            {
                Active = 1;
            }
            int SMS_NO = 0;
            if (ViewState["SMS_NO"].ToString() != "Add")
            {
                SMS_NO = Convert.ToInt32(ViewState["SMS_NO"].ToString());
            }
            CustomStatus cs = (CustomStatus)CFC.AddSMSServiceProvider(SMS_NO, ServiceNo, SMSAPI, SMSUserID, EmailSMS, PasswordSMS, SMSParameterI, SMSParameterII, Active, Convert.ToInt32(Session["userno"].ToString()),
                SMSProvider, SMSTempID, SMSTemp, SMSTemplateName, SMSUrl);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["SMS_NO"] = "Add";
                BindListViewSMS();
                SMSClear();
                objCommon.DisplayMessage(this, "Record Saved  Successfully.", this.Page);
                return;
                //PKG_SMS_SERVICE_PROVIDER_CONFIGURATION

            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["SMS_NO"] = "Add";
                BindListViewSMS();
                SMSClear();
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                return;
            }
        }
        catch { }

    }
    protected void btnCancelSMS_Click(object sender, EventArgs e)
    {
        SMSClear();
    }
    protected void SMSClear()
    {
        ddlServiceProviderSMS.SelectedValue = "0";
        TxtSMSAPI.Text = "";
        txtSMSUserID.Text = "";
        txtPasswordSMS.Text = "";
        txtEmailSMS.Text = "";
        txtSMSParameterI.Text = "";
        txtSMSParameterII.Text = "";
        txtSMSProvider.Text = "";
        txtTemplateId.Text = "";
        txtTemplateName.Text = "";
        txtTemplate.Text = "";
        txtSMSUrl.Text = "";
        ddlServiceProviderSMS.Enabled = true;


    }
    protected void BindListViewSMS()
    {
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION  SC INNER JOIN EMAIL_SMS_SERVICE_PROVIDER_NAME SS ON (SC.SERVICE_NO=SS.SERVICE_NO)", "*", "SERVICE_NO", "CONFIG_TYPE=2", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvsmsData.DataSource = ds;
            lvsmsData.DataBind();
        }
        else
        {
            lvsmsData.DataSource = null;
            lvsmsData.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvsmsData.Items)
        {
            Label Status = dataitem.FindControl("lblSMSStatus") as Label;
            string Statuss = (Status.Text);
            if (Statuss == "0")
            {
                Status.Text = "Deactive";
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.Text = "Active";
                Status.CssClass = "badge badge-success";
            }

        }
    }
    protected void btnEditSms_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int EmailNo = int.Parse(btnEdit.CommandArgument);
        ViewState["SMS_NO"] = EmailNo;
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION", "*", "EMAILSMS_NO", "EMAILSMS_NO=" + EmailNo, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlServiceProviderSMS.Enabled = false;
            ddlServiceProviderSMS.SelectedValue = ds.Tables[0].Rows[0]["SERVICE_NO"].ToString();
            ddlServiceProviderSMS_SelectedIndexChanged(new object(), new EventArgs());
            TxtSMSAPI.Text = ds.Tables[0].Rows[0]["SMS_API"].ToString();
            txtSMSUserID.Text = ds.Tables[0].Rows[0]["SMS_User_ID"].ToString();
            txtPasswordSMS.Text = ds.Tables[0].Rows[0]["PASSWORDS"].ToString();
            txtEmailSMS.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtSMSParameterI.Text = ds.Tables[0].Rows[0]["SMS_PARAMETER_I"].ToString();
            txtSMSParameterII.Text = ds.Tables[0].Rows[0]["SMS_PARAMETER_II"].ToString();

            txtSMSProvider.Text = ds.Tables[0].Rows[0]["SMSPROVIDER"].ToString();
            txtTemplateId.Text = ds.Tables[0].Rows[0]["SMS_TEMP_ID"].ToString();
            txtTemplateName.Text = ds.Tables[0].Rows[0]["SMS_TEMPLATE_NAME"].ToString();
            txtTemplate.Text = ds.Tables[0].Rows[0]["SMS_TEMPLATE"].ToString();
            txtSMSUrl.Text = ds.Tables[0].Rows[0]["SMS_URL"].ToString();

            string status = ds.Tables[0].Rows[0]["STATUS"].ToString().Trim();

            if (status == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSMS(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSMS(false);", true);
            }
        }
    }
    protected void ddlServiceProviderSMS_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivSMSAPI.Visible = false; DivSMSUserID.Visible = false; DivPasswordSMS.Visible = false;
        DivEmailSMS.Visible = false; DivSMSParameterI.Visible = false; DivSMSParameterII.Visible = false;
        divSMSProvider.Visible = false;  divSMSUrl.Visible = false;
        string PARAMETER = objCommon.LookUp("EMAIL_SMS_SERVICE_PROVIDER_NAME", "PARAMETER", "SERVICE_NO='" + ddlServiceProviderSMS.SelectedValue + "'");
        string[] PARAMETERS = PARAMETER.ToString().Split(',');
        foreach (string s in PARAMETERS)
        {
            if (s == "6")
            {
                DivSMSAPI.Visible = true;
            }
            else if (s == "7")
            {
                DivSMSUserID.Visible = true;
            }
            else if (s == "8")
            {
                DivPasswordSMS.Visible = true;
            }
            else if (s == "9")
            {
                DivEmailSMS.Visible = true;
            }
            else if (s == "10")
            {
                DivSMSParameterI.Visible = true;
            }
            else if (s == "11")
            {
                DivSMSParameterII.Visible = true;
            }
            else if (s == "16")
            {
                divSMSProvider.Visible = true;
            }
            //else if (s == "17")
            //{
            //    divTempId.Visible = true;
            //}
            //else if (s == "18")
            //{
            //    divTemp.Visible = true;
            //}
            //else if (s == "19")
            //{
            //    divTempName.Visible = true;
            //}
            else if (s == "20")
            {
                divSMSUrl.Visible = true;
            }
        }

    }
    #endregion


    #region Whatsaap Configuration Start
    //***************************************Whatsaap Configuration Start******************************************************** 
    protected void btnSubmitWhatsApp_Click(object sender, EventArgs e)
    {
        try
        {
            int ServiceNo = Convert.ToInt32(ddlServiceProviderWhat.SelectedValue);
            string API_URL = txtURL.Text;
            string Token = txtToken.Text;
            string MobileNo = txtMobileNo.Text;
            string UserName = txtUserName.Text;
            string AccountID = txtAccountID.Text;
            string API_Key = txtAPIKey.Text;
            //string UserName_WhatsApp = txtUserName.Text;
            int Active = 0;
            if (hdnWhatsaap.Value == "true")
            {
                Active = 1;
            }
            int WhatsAAP_NO = 0;
            if (ViewState["WHATSAAP_NO"].ToString() != "Add")
            {
                WhatsAAP_NO = Convert.ToInt32(ViewState["WHATSAAP_NO"].ToString());
            }
            CustomStatus cs = (CustomStatus)CFC.AddWhatsappServiceProvider(WhatsAAP_NO, ServiceNo, API_URL, Token, MobileNo, AccountID, UserName, Active, Convert.ToInt32(Session["userno"].ToString()),API_Key);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["WHATSAAP_NO"] = "Add";
                ListViewBindWhatsaap();
                WhatsaapClear();
                objCommon.DisplayMessage(this, "Record Saved  Successfully.", this.Page);
                return;
                //PKG_SMS_SERVICE_PROVIDER_CONFIGURATION

            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["WHATSAAP_NO"] = "Add";
                ListViewBindWhatsaap();
                WhatsaapClear();
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                return;
            }
        }
        catch { }
    }
    protected void btnCnacelWhatsApp_Click(object sender, EventArgs e)
    {
        WhatsaapClear();
    }
    protected void WhatsaapClear()
    {
        ddlServiceProviderWhat.SelectedValue = "0";
        txtAccountID.Text = "";
        txtURL.Text = "";
        txtToken.Text = "";
        txtMobileNo.Text = "";
        txtUserName.Text = "";
        ddlServiceProviderWhat.Enabled = true;
        txtAPIKey.Text = "";
        txtUserName.Text = "";

    }
    protected void ListViewBindWhatsaap()
    {
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION  SC INNER JOIN EMAIL_SMS_SERVICE_PROVIDER_NAME SS ON (SC.SERVICE_NO=SS.SERVICE_NO)", "*", "SERVICE_NO", "CONFIG_TYPE=3", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvWhatsapp.DataSource = ds;
            lvWhatsapp.DataBind();
        }
        else
        {
            lvWhatsapp.DataSource = null;
            lvWhatsapp.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvWhatsapp.Items)
        {
            Label Status = dataitem.FindControl("lblWhatstatus") as Label;
            string Statuss = (Status.Text);
            if (Statuss == "0")
            {
                Status.Text = "Deactive";
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.Text = "Active";
                Status.CssClass = "badge badge-success";
            }
        }
    }
    protected void btnEditWhatsapp_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int EmailNo = int.Parse(btnEdit.CommandArgument);
        ViewState["WHATSAAP_NO"] = EmailNo;
        DataSet ds = objCommon.FillDropDown("EMAIL_SMS_CONFIGURATION", "*", "EMAILSMS_NO", "EMAILSMS_NO=" + EmailNo, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlServiceProviderWhat.Enabled = false;
            ddlServiceProviderWhat.SelectedValue = ds.Tables[0].Rows[0]["SERVICE_NO"].ToString();
            ddlServiceProviderWhat_SelectedIndexChanged(new object(), new EventArgs());
            txtAccountID.Text = ds.Tables[0].Rows[0]["WHATSAAP_ACCOUNT_SID"].ToString();
            txtURL.Text = ds.Tables[0].Rows[0]["WHATSAAP_API_URL"].ToString();
            txtToken.Text = ds.Tables[0].Rows[0]["WHATSAAP_API_KEY"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["WHATSAAP_MOBILE"].ToString();
            string status = ds.Tables[0].Rows[0]["STATUS"].ToString().Trim();
            txtUserName.Text = ds.Tables[0].Rows[0]["WHATSAAP_USER"].ToString().Trim();
            txtAPIKey.Text = ds.Tables[0].Rows[0]["API_KEY"].ToString().Trim();
            if (status == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetWhats(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetWhats(false);", true);
            }
        }
    }
    protected void ddlServiceProviderWhat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivURL.Visible = false; DivToken.Visible = false; DivAccountID.Visible = false;
        DivMobileNo.Visible = false; divUserWhatsApp.Visible = false; divAPI_Key.Visible = false;
        string PARAMETER = objCommon.LookUp("EMAIL_SMS_SERVICE_PROVIDER_NAME", "PARAMETER", "SERVICE_NO='" + ddlServiceProviderWhat.SelectedValue + "'");
        string[] PARAMETERS = PARAMETER.ToString().Split(',');
        foreach (string s in PARAMETERS)
        {
            if (s == "12")
            {
                DivURL.Visible = true;
            }
            else if (s == "13")
            {
                DivToken.Visible = true;
            }
            else if (s == "14")
            {
                DivAccountID.Visible = true;
            }
            else if (s == "15")
            {
                DivMobileNo.Visible = true;
            }
            else if (s == "22")
            {
                divUserWhatsApp.Visible = true;
            }
            else if (s == "21")
            {
                divAPI_Key.Visible = true;
            } 
            //else if (s == "10")
            //{
            //    DivSMSParameterI.Visible = true;
            //}
            //else if (s == "11")
            //{
            //    DivSMSParameterII.Visible = true;
            //}
        }

    }
    #endregion
    #region Link Assing Start
    //***************************************Link Assing Start******************************************************** 
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlDomain, "ACC_SECTION S INNER JOIN  ACCESS_LINK A ON (S.AS_NO=AL_ASNO)", "DISTINCT AS_NO", "AS_TITLE", "ISNULL(ACTIVE_STATUS,0)=1 AND ISNULL(TRANCACTION,0)=1", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDomain_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataSet dsLink = objCommon.FillDropDown("ACC_SECTION  S INNER JOIN ACCESS_LINK AL ON (AL.AL_ASNO = S.AS_NO) LEFT JOIN EMAIL_SMS_WHATSAAP_LINK ES ON (ES.AS_DOMAIN_NO = S.AS_NO AND ES.AL_PAGE_NO=AL.AL_NO)", "*", "AS_TITLE", "Active_Status=1 AND ISNULL(TRANCACTION,0)=1 AND AL_ASNO =" + ddlDomain.SelectedValue, "srno");

            if (dsLink.Tables[0].Rows.Count > 0)
            {
                DivLinkButton.Visible = true;
                lvALinks.DataSource = dsLink;
                lvALinks.DataBind();
                //foreach (ListViewItem item in lvALinks.Items)
                //{
                //    Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
                //    if (lblactinestatus.Text == "1")
                //    {
                //        lblactinestatus.Text = "Active";
                //        lblactinestatus.Style.Add("color", "Green");
                //    }
                //    else
                //    {
                //        lblactinestatus.Text = "DeActive";
                //        lblactinestatus.Style.Add("color", "Red");
                //    }
                //}
            }

            else
            {
                DivLinkButton.Visible = false;
                lvALinks.DataSource = null;
                lvALinks.DataBind();
            }

        }

        //if (lblactivestatus.Text == "1")
        //{
        //    lblactivestatus.Text = "Active";
        //    lblactivestatus.Style.Add("color", "Green");
        //}
        //else
        //{
        //    lblactivestatus.Text = "De-Active";
        //    lblactivestatus.Style.Add("color", "Red");
        //}
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void btnLinkAssin_Click(object sender, EventArgs e)
    {
        string que_out1 = "0";
        int Domainno = Convert.ToInt32(ddlDomain.SelectedValue);
        int count = 0;
        foreach (ListViewDataItem dataitem in lvALinks.Items)
        {
            CheckBox chkcheck = dataitem.FindControl("check") as CheckBox;
            CheckBox ckhEmail = dataitem.FindControl("ckhEmail") as CheckBox;
            CheckBox ChkSms = dataitem.FindControl("ChkSms") as CheckBox;
            CheckBox ChkWhatssp = dataitem.FindControl("ChkWhatssp") as CheckBox;
            //CheckBox chkMultiple = dataitem.FindControl("chkMultiple") as CheckBox;
            Label PageName = dataitem.FindControl("lblPageName") as Label;
            Label Pageno = dataitem.FindControl("lblalno") as Label;
            int Email_Status = 0;
            int SMS_Status = 0;
            int Whatssapp_Status = 0;
            int multiple_Status = 1;        //Define as flag 1 because by default it should set as multiple,by Nikhil L. on 26/06/2023
            if (chkcheck.Checked == true)
            {
                count++;
                if (ckhEmail.Checked == true)
                {
                    Email_Status = 1;
                }
                if (ChkSms.Checked == true)
                {
                    SMS_Status = 1;
                }
                if (ChkWhatssp.Checked == true)
                {
                    Whatssapp_Status = 1;
                }
                //if (chkMultiple.Checked == true)
                //{
                    multiple_Status = 1;
                //}
                string SP_Name1 = "PKG_INSERT_EMAIL_SMS_WHATSAAP_LINK";
                string SP_Parameters1 = "@P_AS_DOMAIN_NO,@P_PAGE_NAME,@P_AL_PAGE_NO,@P_EMAIL_STATUS,@P_SMS_STATUS,@P_WHATSAAP_STATUS,@P_UA_NO,@P_MULTIPLE_STATUS,@P_OUT";
                string Call_Values1 = "" + Domainno + "," + PageName.Text + "," + Convert.ToInt32(Pageno.Text) + "," + Email_Status + "," + SMS_Status + "," + Whatssapp_Status + "," + Convert.ToInt32(Session["userno"]) + "," + multiple_Status + "," + 0 + "";
                //que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
                que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true);
            }

        }
        if (count == 0)
        {
            objCommon.DisplayMessage(this, "Please Select At List One..!!", this.Page);
            return;
        }
        if (que_out1 == "1")
        {
            ddlDomain_SelectedIndexChanged(new object(), new EventArgs());
            objCommon.DisplayMessage(this, "Record Save Successfully.. !!", this.Page);
            return;
        }
        else
        {
            ddlDomain_SelectedIndexChanged(new object(), new EventArgs());
            objCommon.DisplayMessage(this, "Server Error... !!", this.Page);
        }
    }
    protected void btnCancelLink_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion
    protected void btnSaveTemp_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name = "";
            string SP_Parameters = "";
            string SP_Values = "";
            string smsId = "";

        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtTemplateId.Text = "";
            txtTemplate.Text = "";
            txtTemplateName.Text = "";
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnEditTemp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int smsId =Convert.ToInt32(btnEdit.CommandArgument);
            string SP_Name = string.Empty;
            string SP_Parameters = string.Empty;
            string SP_Values = string.Empty;
            SP_Name = "PKG_ACD_GET_AND_BIND_SMS_TEMPLATE";
            SP_Parameters = "@P_SMS_TEMPLATE_ID";
            SP_Values = "" + smsId + "";
            DataSet dsBindTemplate = new DataSet();
            dsBindTemplate = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
            if (dsBindTemplate.Tables[0].Rows.Count > 0)
            {
                txtTemplateId.Text = dsBindTemplate.Tables[0].Rows[0]["TEM_ID"].ToString();
                txtTemplateName.Text = dsBindTemplate.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString();
                txtTemplate.Text = dsBindTemplate.Tables[0].Rows[0]["TEMPLATE"].ToString();
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    //private void BindTemplate(int smsId)
    //{
    //    try
    //    {
    //        string SP_Name = string.Empty;
    //        string SP_Parameters = string.Empty;
    //        string SP_Values = string.Empty;
    //        SP_Name = "PKG_ACD_GET_AND_BIND_SMS_TEMPLATE";
    //        SP_Parameters = "@P_SMS_TEMPLATE_ID";
    //        SP_Values = ""+smsId+"";
    //        DataSet dsBindTemplate = new DataSet();
    //        dsBindTemplate = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
    //        if (dsBindTemplate.Tables[0].Rows.Count > 0)
    //        {
    //            lvSmsTemplate.DataSource = dsBindTemplate;
    //            lvSmsTemplate.DataBind();
    //            foreach (ListViewDataItem lv in lvSmsTemplate.Items)
    //            {
    //                Label lblStatus = lv.FindControl("lblActiveStatus") as Label;
    //                if (lblStatus.Text.Equals("Active"))
    //                {
    //                    //lblStatus.Text = "Active";
    //                    lblStatus.CssClass = "badge badge-success";
    //                }
    //                else
    //                {
    //                    //lblStatus.Text = "In active";
    //                    lblStatus.CssClass = "badge badge-danger";
    //                }
    //            }
                    
    //        }
    //    }
    //    catch (Exception)
    //    {            
    //        throw;
    //    }
    //}
    protected void btnCancelTempType_Click(object sender, EventArgs e)
    {
        ClearTempType();
    }
    private void ClearTempType()
    {
        txtTemplateType.Text = string.Empty;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objTemplateType.TEMPLATE_TYPE = txtTemplateType.Text.Trim();

            if (hfdStatTempType.Value.ToLower() == "true")
            {
                objTemplateType.ActiveStatus = true;
            }
            else
            {
                objTemplateType.ActiveStatus = false;
            }
            int count = objTemplateTypeController.InsertTemplateType(objTemplateType);
            if (count > 0)
            {
                ClearTempType();
                BindListView();
                objCommon.DisplayMessage(updtmptyp, "Record Saved Successfully..", this.Page);
                FillDropDown();
                return;
            }
            else
            {
                objCommon.DisplayMessage(updtmptyp, "Fail Something Went Wrong..", this.Page);

            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objTemplateTypeController.BindListview();
            lvTempType.DataSource = ds;
            lvTempType.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlTemplateType, "ACD_SMS_TEMPLATE_TYPE", "TEMPLATE_ID", "TEMPLATE_TYPE", "TEMPLATE_ID>0 AND ISNULL(ACTIVE_STATUS,0)=1", "TEMPLATE_ID");
            objCommon.FillDropDownList(ddlwhatsapptemp, "ACD_WHATSAPP_TEMPLATE_TYPE", "WHATSAPP_TEMPLATE_ID", "TEMPLATE_TYPE", "WHATSAPP_TEMPLATE_ID>0 AND ISNULL(ACTIVE_STATUS,0)=1", "WHATSAPP_TEMPLATE_ID");

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        objTemplateType.TEMPLATE_ID = Convert.ToInt32(ViewState["tem"]);
        objTemplateType.TEMPLATE_TYPE = txtTemplateType.Text.Trim();

        if (hfdStatTempType.Value.ToLower() == "true")
        {
            objTemplateType.ActiveStatus = true;
        }
        else
        {
            objTemplateType.ActiveStatus = false;
        }
        int count = objTemplateTypeController.UpdateTemplateType(objTemplateType);
        if (count > 0)
        {
            ClearTempType();
            BindListView();
            objCommon.DisplayMessage(updtmptyp, "Record Updated Successfully..", this.Page);
            FillDropDown();
            return;
        }
        else
        {
            objCommon.DisplayMessage(updtmptyp, "Fail Something Went Wrong..", this.Page);

        }
    }
    protected void btnEditTempType_Click(object sender, ImageClickEventArgs e)
    {
        btnSave.Visible = false;
        btnUpdate.Visible = true;
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["tem"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowTemplateType(editno);
            ViewState["actiontemtyp"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowTemplateType(int TEMPLATE_ID)
    {
        DataSet ds = objTemplateTypeController.GetTemplateTypeInfo(TEMPLATE_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtTemplateType.Text = ds.Tables[0].Rows[0]["TEMPLATE_TYPE"].ToString();

            hfdStat.Value = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatTemType(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatTemType(false);", true);
            }
        }
    }
    protected void btnSubmit_Temp_Click(object sender, EventArgs e)
    {
        string _al_nos = string.Empty;
        int ck = 0;

        objSmsTemplate.TEMPLATE_TYPE = ddlTemplateType.Text.Trim();
        objSmsTemplate.TEMPLATE_NAME = txtTemplateName.Text.Trim();
        objSmsTemplate.TEM_ID = txtTemplateId.Text.Trim();
        objSmsTemplate.TEMPLATE = txtTemplate.Text.Trim();
        if (hfSmsStatus.Value.ToLower() == "true")
        {
            objSmsTemplate.ActiveStatus = true;
        }
        else
        {
            objSmsTemplate.ActiveStatus = false;
        }
        objSmsTemplate.VARIABLE_COUNT = Convert.ToInt32(txtVarCount.Text.Trim());
        //foreach (ListItem items in lstbxPageName.Items)
        //{
        //    if (items.Selected == true)
        //    {
                //objSmsTemplate.AL_NO = Convert.ToInt32(items.Value);
                ck = objSmsTemplateController.InsertSmsTemplateData(objSmsTemplate);
        //    }
        //}

        if (ck == 1)
        {
            ClearSmsTemData();
            BindListViewSmsTemplate();
            objCommon.DisplayMessage(updsms, "Record Saved Successfully..", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(updsms, "Fail Something Went Wrong..", this.Page);

        }
    }
    private void ClearSmsTemData()
    {
        btnUpdateSms.Visible = false;
        btnSubmit_Temp.Visible = true;
        ddlTemplateType.SelectedIndex = 0;
        txtTemplateName.Text = "";

        //foreach (ListItem items in lstbxPageName.Items)
        //{
        //    if (items.Selected == true)
        //    {
        //        items.Selected = false;

        //    }
        //}
        txtTemplateId.Text = "";
        txtTemplate.Text = "";
        txtVarCount.Text = "";
    }
    private void BindListViewSmsTemplate()
     {
        try
        {
            int smsId = 0;
            string SP_Name = string.Empty;
            string SP_Parameters = string.Empty;
            string SP_Values = string.Empty;
            SP_Name = "PKG_ACD_GET_AND_BIND_SMS_TEMPLATE";
            SP_Parameters = "@P_SMS_TEMPLATE_ID";
            SP_Values = "" + smsId + "";
            DataSet dss = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
            lvSmsTemplate.DataSource = dss;
            lvSmsTemplate.DataBind();
            foreach (ListViewDataItem lv in lvSmsTemplate.Items)
            {
                Label lblStatus = lv.FindControl("lblActiveStatus") as Label;
                if (lblStatus.Text == "Active")
                {
                    lblStatus.CssClass = "badge badge-success";
                }
                else
                {
                    lblStatus.CssClass = "badge badge-danger";
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnUpdateSms_Click(object sender, EventArgs e)
    {
        btnSubmit_Temp.Visible = true;
        btnUpdateSms.Visible = false;

        objSmsTemplate.SMS_TEMPLATE_ID = Convert.ToInt32(ViewState["smstem"]);
        objSmsTemplate.TEMPLATE_TYPE = ddlTemplateType.SelectedValue;
        //objSmsTemplate.AL_NO = Convert.ToInt32(lstbxPageName.Text.Trim());
        objSmsTemplate.TEM_ID = (txtTemplateId.Text.Trim());
        objSmsTemplate.TEMPLATE = txtTemplate.Text.Trim();
        objSmsTemplate.TEMPLATE_NAME = txtTemplateName.Text.Trim();

        if (hfSmsStatus.Value == "true")
        {
            objSmsTemplate.ActiveStatus = true;
        }
        else
        {
            objSmsTemplate.ActiveStatus = false;
        }
        int count = objSmsTemplateController.UpdateSmsTemplateType(objSmsTemplate);
        if (count > 0)
        {
            ClearSmsTemData();
            //BindTemplate(0);
            BindListViewSmsTemplate();
            objCommon.DisplayMessage(updsms, "Record Updated Successfully..", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(updsms, "Fail Something Went Wrong..", this.Page);

        }
    }
    protected void btnCancelSmsTemp_Click(object sender, EventArgs e)
    {
        ClearSmsTemData();
    }
    protected void btnEditSmsType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit_Temp.Visible = false;
            btnUpdateSms.Visible = true;
            ImageButton btnEditSms = sender as ImageButton;
            int edit = int.Parse(btnEditSms.CommandArgument);
            int SMS_TEMPLATE_ID = Convert.ToInt32(btnEditSms.CommandArgument);
            ViewState["smstem"] = Convert.ToInt32(btnEditSms.CommandArgument);
            ShowSmsTemplateType(SMS_TEMPLATE_ID);
            ViewState["actionsmstemtyp"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowSmsTemplateType(int SMS_TEMPLATE_ID)
    {
        DataSet ds = objSmsTemplateController.GetSmsTemplateTypeInfo(SMS_TEMPLATE_ID);
        char delimiterChars = ',';
        char delimiter = ',';
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlTemplateType.Text = ds.Tables[0].Rows[0]["TEMPLATE_TYPE"].ToString();
            txtTemplateName.Text = ds.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString();
            lstbxPageName.Text = ds.Tables[0].Rows[0]["AL_NO"].ToString();
            txtTemplateId.Text = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
            txtTemplate.Text = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
            hfSmsStatus.Value = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
            txtVarCount.Text = ds.Tables[0].Rows[0]["VARIABLE_COUNT"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active" || ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().ToLower() == "true")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSmsStatTemType(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSmsStatTemType(false);", true);
            }
        }
        else
        {
            objCommon.DisplayMessage(updsms, "Selected Template Type is InActive..", this.Page);
        }
    }
    protected void btnWhatsAppSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objTemplateType.TEMPLATE_TYPE = txtWAtemp.Text.Trim();

            if (hfdWhatsappType.Value.ToLower() == "true")
            {
                objTemplateType.ActiveStatus = true;
            }
            else
            {
                objTemplateType.ActiveStatus = false;
            }
            int count = objTemplateTypeController.InsertWhatsappTemplateType(objTemplateType);
            if (count > 0)
            {
                Clear1();
                BindWAListView();
                objCommon.DisplayMessage(updtmptyp, "Record Saved Successfully..", this.Page);
                FillDropDown();
                return;
            }
            else
            {
                objCommon.DisplayMessage(updtmptyp, "Fail Something Went Wrong..", this.Page);

            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnWhatsAppUpdate_Click(object sender, EventArgs e)
    {
        try
        {
             btnWhatsAppSubmit.Visible = true;
        btnWhatsAppUpdate.Visible = false;
        objTemplateType.TEMPLATE_ID = Convert.ToInt32(ViewState["tem1"]);
        objTemplateType.TEMPLATE_TYPE = txtWAtemp.Text.Trim();

        if (hfdWhatsappType.Value.ToLower() == "true")
        {
            objTemplateType.ActiveStatus = true;
        }
        else
        {
            objTemplateType.ActiveStatus = false;
        }
        int count = objTemplateTypeController.UpdateWhatsappTemplateType(objTemplateType);
        if (count > 0)
        {
            Clear1();
            BindWAListView();
            objCommon.DisplayMessage(updtmptyp, "Record Updated Successfully..", this.Page);
            FillDropDown();
            return;
        }
        else
        {
            objCommon.DisplayMessage(updtmptyp, "Fail Something Went Wrong..", this.Page);

        }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        try
        {
            Clear1();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnEdit1_Click(object sender, ImageClickEventArgs e)
    {
        btnWhatsAppSubmit.Visible = false;
        btnWhatsAppUpdate.Visible = true;
        try
        {
            ImageButton btnEdit1 = sender as ImageButton;
            int editno = int.Parse(btnEdit1.CommandArgument);
            ViewState["tem1"] = Convert.ToInt32(btnEdit1.CommandArgument);
            ShowWATemplateType(editno);
            ViewState["actionwatemtyp"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void Clear1()
    {
        txtWAtemp.Text = string.Empty;
        btnWhatsAppSubmit.Visible = true;
        btnWhatsAppUpdate.Visible = false;
    }
    private void BindWAListView()
    {
        try
        {
            DataSet ds = objTemplateTypeController.BindListWhatsappttypeview();
            lvWhatsAppTemplate.DataSource = ds;
            lvWhatsAppTemplate.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowWATemplateType(int TEMPLATE_ID)
    {
        DataSet ds = objTemplateTypeController.GetTemplateWhatsappTypeInfo(TEMPLATE_ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtWAtemp.Text = ds.Tables[0].Rows[0]["TEMPLATE_TYPE"].ToString();
            hfdWhatsappType.Value = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetwhatsappTemType(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetwhatsappTemType(false);", true);
            }
        }
    }
    protected void btnwhatsapptemp_Click(object sender, EventArgs e)
    {
        try
        {
            string _al_nos = string.Empty;
        int ck = 0;

        objSmsTemplate.TEMPLATE_TYPE = ddlwhatsapptemp.Text.Trim();
        objSmsTemplate.TEMPLATE_NAME = txtwhatsapp.Text.Trim();
        //objSmsTemplate.TEM_ID = txtwhatsapptempid.Text.Trim();

        objSmsTemplate.TEMPLATE = txtwhatsapptemp.Text.Trim();


        if (hfWhatsappstatus.Value.ToLower() == "true")
            {
            objSmsTemplate.ActiveStatus = true;
            }
        else
            {
            objSmsTemplate.ActiveStatus = false;
            }
        //objSmsTemplate.VARIABLE_COUNT = Convert.ToInt32(txtwhatsappcount.Text.Trim());
        //foreach (ListItem items in lvlistwhatsapp.Items)
        //    {
        //    if (items.Selected == true)
        //        {
        //        objSmsTemplate.AL_NO = Convert.ToInt32(items.Value);
        //        ck = objSmsTemplateController.InsertWhatsappTemplateData(objSmsTemplate);
        //        }
        //    }
        ck = objSmsTemplateController.InsertWhatsappTemplateData(objSmsTemplate);
        if (ck == 1)
            {
            ClearWhatsappTemData();
            BindListViewWhatsappTemplate();
            objCommon.DisplayMessage(updsms, "Record Saved Successfully..", this.Page);
            return;
            }
        else
            {
            objCommon.DisplayMessage(updsms, "Fail Something Went Wrong..", this.Page);
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    protected void btnwhatsapptemplateupdate_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Visible = true;
        btnUpdateSms.Visible = false;

        objSmsTemplate.SMS_TEMPLATE_ID = Convert.ToInt32(ViewState["whatsapptem"]);
        objSmsTemplate.TEMPLATE_TYPE = ddlwhatsapptemp.SelectedValue;
        //foreach (ListItem items in lvlistwhatsapp.Items)
        //    {
        //    if (items.Selected == true)
        //        {
        //        objSmsTemplate.AL_NO = Convert.ToInt32(items.Value);
                
        //        }
        //    }

        //objSmsTemplate.AL_NO = Convert.ToInt32(ddlwhatsapppage.SelectedItem.Values());
        //objSmsTemplate.TEM_ID = (txtwhatsapptempid.Text.Trim());
        objSmsTemplate.TEMPLATE = txtwhatsapptemp.Text.Trim();
        objSmsTemplate.TEMPLATE_NAME = txtwhatsapp.Text.Trim();

        if (hfWhatsappstatus.Value == "true")
            {
            objSmsTemplate.ActiveStatus = true;
            }
        else
            {
            objSmsTemplate.ActiveStatus = false;
            }

        int count = objSmsTemplateController.UpdateWhatsappTemplateType(objSmsTemplate);
        if (count > 0)
            {
            ClearWhatsappTemData();
            BindListViewWhatsappTemplate();
            objCommon.DisplayMessage(updsms, "Record Updated Successfully..", this.Page);
            return;
            }
        else
            {
            objCommon.DisplayMessage(updsms, "Fail Something Went Wrong..", this.Page);

            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnwhatsappCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearWhatsappTemData();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnEditwhatsappType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnwhatsapptemp.Visible = false;
            btnwhatsapptemplateupdate.Visible = true;
            ImageButton btnEditSms = sender as ImageButton;
            int edit = int.Parse(btnEditSms.CommandArgument);
            int WHATSAPP_TEMPLATE_ID = Convert.ToInt32(btnEditSms.CommandArgument);
            ViewState["whatsapptem"] = Convert.ToInt32(btnEditSms.CommandArgument);
            ShowWhatsappTemplateType(WHATSAPP_TEMPLATE_ID);
            ViewState["actionsmstemtyp"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearWhatsappTemData()
    {
        try
        {
            btnwhatsapptemplateupdate.Visible = false;
            btnwhatsapptemp.Visible = true;
            ddlwhatsapptemp.SelectedIndex = 0;
            txtwhatsapptemp.Text = "";
            txtwhatsapp.Text = "";
        }
        catch (Exception)
        {            
            throw;
        }       
    }
    private void BindListViewWhatsappTemplate()
    {
        try
        {
            DataSet dss = objSmsTemplateController.BindListviewwhastappTemplateType_New();
            lvwhatsapptempnew.DataSource = dss;
            lvwhatsapptempnew.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowWhatsappTemplateType(int WHATSAPP_TEMPLATE_ID)
    {
        DataSet ds = objSmsTemplateController.GetWhatsappTemplateTypeInfo(WHATSAPP_TEMPLATE_ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlwhatsapptemp.Text = ds.Tables[0].Rows[0]["TEMPLATE_TYPE"].ToString();
            txtwhatsapp.Text = ds.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString();
            lvlistwhatsapp.Text = ds.Tables[0].Rows[0]["AL_NO"].ToString();
            txtwhatsapptempid.Text = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
            txtwhatsapptemp.Text = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
            txtwhatsappcount.Text = ds.Tables[0].Rows[0]["VARIABLE_COUNT"].ToString();
            hfWhatsappstatus.Value = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active" || ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().ToLower() == "true")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWhatsappStatTemType(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWhatsappStatTemType(false);", true);
            }
        }
        else
        {
            objCommon.DisplayMessage(updsms, "Selected Template Type is InActive..", this.Page);
        }
    }
}