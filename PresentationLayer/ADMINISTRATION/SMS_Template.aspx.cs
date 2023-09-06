//======================================================================================
// PROJECT NAME  : RFC COMMON CODE                                                                
// MODULE NAME   :                          
// CREATION DATE : 19-01-2022                                                        
// CREATED BY    : DIKSHA NANDURKAR  
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessEntities;
using BusinessLogicLayer.BusinessLogic;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;



public partial class ADMINISTRATION_SMS_Template : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TemplateType objTemplateType = new TemplateType();
    TemplateTypeController objTemplateTypeController = new TemplateTypeController();
    SmsTemplateType objSmsTemplate = new SmsTemplateType();
    SmsTemplateTypeController objSmsTemplateController = new SmsTemplateTypeController();
    string AL_NOS = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindListView();
            BindListViewSmsTemplate();
            FillDropDown();
            btnUpdate.Visible = false;
            btnUpdateSms.Visible = false;

        }
    }
    /// <summary>
    /// FillDropDown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlTemplateType, "ACD_SMS_TEMPLATE_TYPE", "TEMPLATE_ID", "TEMPLATE_TYPE", "TEMPLATE_ID>0 AND ISNULL(ACTIVE_STATUS,0)=1", "TEMPLATE_ID");
            objCommon.FillListBox(lstbxPageName, "ACCESS_LINK", "AL_No", "AL_Link", "AL_URL <> ''AND Active_Status = 1", "AL_Link");

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// Insert Template Type record into DataBase using Submit Button
    /// </summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objTemplateType.TEMPLATE_TYPE = txtTemplateType.Text.Trim();

            if (hfdStat.Value.ToLower() == "true")
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
                Clear();
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
    /// <summary>
    /// BindListView for Template Type
    /// </summary>
    private void BindListView()
    {
        try
        {
            DataSet ds = objTemplateTypeController.BindListview();
            lvTemplate.DataSource = ds;
            lvTemplate.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// Edit Template Type record using Edit Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
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

    /// <summary>
    /// ShowTemplateType method, called when the user Click on  "Edit" button   
    /// </summary>
    /// <param name="TEMPLATE_ID"></param>
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
    /// <summary>
    ///  Update Template Type record using Update Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        objTemplateType.TEMPLATE_ID = Convert.ToInt32(ViewState["tem"]);
        objTemplateType.TEMPLATE_TYPE = txtTemplateType.Text.Trim();

        if (hfdStat.Value.ToLower() == "true")
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
            Clear();
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
    /// <summary>
    ///  Clear Template Type record using Cancel Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    /// <summary>
    /// Clear method, called when the user Click on  "Cancel" button  

    /// </summary>
    private void Clear()
    {
        txtTemplateType.Text = string.Empty;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }
    /// <summary>
    /// Insert SMS Template record into DataBase using Submit Button
    /// </summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
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
        foreach (ListItem items in lstbxPageName.Items)
        {
            if (items.Selected == true)
            {
                objSmsTemplate.AL_NO = Convert.ToInt32(items.Value);
                ck = objSmsTemplateController.InsertSmsTemplateData(objSmsTemplate);
            }
        }

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
    /// <summary>
    /// Bindlistview for SMS Template
    /// </summary>
    private void BindListViewSmsTemplate()
    {
        try
        {
            DataSet dss = objSmsTemplateController.BindListviewSmsTemplateType();
            lvSmsTemplate.DataSource = dss;
            lvSmsTemplate.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// Edit SMS Template record using Edit Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditSmsType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Visible = false;
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
    /// <summary>
    /// ShowSmsTemplateType method, called when the user Click on  "Edit" button   
    /// </summary>
    /// <param name="TEMPLATE_ID"></param>
    private void ShowSmsTemplateType(int SMS_TEMPLATE_ID)
    {
        DataSet ds = objSmsTemplateController.GetSmsTemplateTypeInfo(SMS_TEMPLATE_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlTemplateType.Text = ds.Tables[0].Rows[0]["TEMPLATE_TYPE"].ToString();
            txtTemplateName.Text = ds.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString();
            lstbxPageName.Text = ds.Tables[0].Rows[0]["AL_NO"].ToString();
            txtTemplateId.Text = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
            txtTemplate.Text = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();

            hfSmsStatus.Value = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();

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
    /// <summary>
    /// Update SMS Template record using Update Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSms_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = true;
        btnUpdateSms.Visible = false;

        objSmsTemplate.SMS_TEMPLATE_ID = Convert.ToInt32(ViewState["smstem"]);
        objSmsTemplate.TEMPLATE_TYPE = ddlTemplateType.SelectedValue;
        objSmsTemplate.AL_NO = Convert.ToInt32(lstbxPageName.Text.Trim());
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
            BindListViewSmsTemplate();
            objCommon.DisplayMessage(updsms, "Record Updated Successfully..", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(updsms, "Fail Something Went Wrong..", this.Page);

        }

    }
    /// <summary>
    /// Clear Template Type record using Cancel Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelSmsTemp_Click(object sender, EventArgs e)
    {
        ClearSmsTemData();
    }
    /// <summary>
    ///  ClearSmsTemData method, called when the user Click on  "Cancel" button  
    /// </summary>
    private void ClearSmsTemData()
    {
        btnUpdateSms.Visible = false;
        btnSubmit.Visible = true;
        ddlTemplateType.SelectedIndex = 0;
        txtTemplateName.Text = "";

        foreach (ListItem items in lstbxPageName.Items)
        {
            if (items.Selected == true)
            {
                items.Selected = false;

            }
        }
        txtTemplateId.Text = "";
        txtTemplate.Text = "";


    }
}





