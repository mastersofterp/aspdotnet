using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.PostAdmission;
using mastersofterp_MAKAUAT;

public partial class ACADEMIC_POSTADMISSION_LetterTemplate : System.Web.UI.Page
{
    Common objCommon = new Common();
    LetterTemplate objLT = new LetterTemplate();
    LetterTemplateController objLC = new LetterTemplateController();
    LetterTemplateFieldData Obj_ltfd = new LetterTemplateFieldData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            FillDropDown();
            BindListView();
            BindLstDataField();
            HiddenField1.Value = "0";
        }
    }


    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlLetterType, "ACD_ADMP_LETTER_TEMPLATE_TYPE", "LETTER_TYPE_ID", "LETTER_TEMPLATE_TYPE", "LETTER_TYPE_ID > 0 AND ACTIVE_STATUS=1 AND MODULE_TYPE='ADMP'", "LETTER_TYPE_ID");
            objCommon.FillDropDownList(ddllettertypeid, "ACD_ADMP_LETTER_TEMPLATE_TYPE", "LETTER_TYPE_ID", "LETTER_TEMPLATE_TYPE", "LETTER_TYPE_ID > 0 AND ACTIVE_STATUS=1 AND MODULE_TYPE='ADMP'", "LETTER_TYPE_ID");
       
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "LetterTemplate.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindListView()
    {
        try
        {
            objLT.LetterTemplateId = 0;
            DataSet ds = objLC.GetLetterTemplate(objLT);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlLetterTemplate.Visible = true;
                lvLetterTemplate.DataSource = ds;
                lvLetterTemplate.DataBind();
            }
            else
            {

                pnlLetterTemplate.Visible = false;
                lvLetterTemplate.DataSource = null;
                lvLetterTemplate.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvLetterTemplate.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;
                string status = (Status.Text);
                if (status == "Inactive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlLetterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDataField.Items.Clear();
            if (ddlLetterType.SelectedIndex > 0)
            {
                BindDataField();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "LoadTinyMCE();", true);
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "LetterTemplate.ddlLetterType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindDataField()
    {
        try
        {
            objCommon.FillDropDownList(ddlDataField, "ACD_ADMP_LETTER_TEMPLATE_DATAFIELD_CONFIGURATION", "DISTINCT LETTER_DATAFIELD_CONFIGID", "DATAFIELD_DISPLAY", "LETTER_TYPE_ID=" + ddlLetterType.SelectedValue, "LETTER_DATAFIELD_CONFIGID");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "LetterTemplate.BindDataField() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtLetterText.Text.Trim()))
            {
                string existmsg = "Please Enter Letter Body Content";
                objCommon.DisplayMessage(existmsg, this.Page);
                return;
            }

            if (ViewState["action"].ToString() == "add")
            {
                objLT.LetterTemplateId = 0;
            }
            else
            {
                objLT.LetterTemplateId = Convert.ToInt32(ViewState["LetterTempId"].ToString());
            }
            objLT.LetterTypeId=Convert.ToInt32(ddlLetterType.SelectedValue);
            objLT.LetterTemplateName = txtLetterTemplateName.Text;
            objLT.ShortDesc = txtShortDesc.Text.Trim();
            objLT.LetterText = txtLetterText.Text.Trim();
            bool ActiveStatus = false;
            if (hdnActive.Value == "true")
            {
                ActiveStatus = true;
            }


            objLT.ActiveStatus = ActiveStatus;
            objLT.UaNo = Convert.ToInt32(Session["userno"].ToString());
            int status = objLC.InsUpdLetterTemplate(objLT);
            if (status == 1)
            {
                clearcontrols();
                objCommon.DisplayMessage(this.Page, "Record added successfully.", Page);
                return;
            }
            else if (status == 2)
            {
                clearcontrols();
                objCommon.DisplayMessage(this.Page, "Record updated successfully.", Page);
                return;
            }
            else if (status == -1001)
            {
                objCommon.DisplayMessage(this.Page, "Record already exists.", Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error.", Page);
                return;
            }
        
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "LetterTemplate.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
        }

    }




    protected void btnFieldSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            

            if (ViewState["action"].ToString() == "add")
            {

                Obj_ltfd.LetterTemplateId = 0;
            }
            else
            {
                Obj_ltfd.LetterTemplateId = Convert.ToInt32(ViewState["LetterTempId"].ToString());
            }
            Obj_ltfd.LetterTypeId = Convert.ToInt32(ddllettertypeid.SelectedValue);
            Obj_ltfd.DisplayDataField = txtdisplayfield.Text;
            Obj_ltfd.DataField = txtdatafield.Text;

            Obj_ltfd.UaNo = Convert.ToInt32(Session["userno"].ToString());
            int status = objLC.InsUpdLetterFieldTemplateData(Obj_ltfd);
            if (status == 1)
            {
                CleaDatField();
                objCommon.DisplayMessage(this.Page, "Record added successfully.", Page);
                return;
            }
            else if (status == 2)
            {
                CleaDatField();
                objCommon.DisplayMessage(this.Page, "Record updated successfully.", Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                return;
            }
            else if (status == -1001)
            {
                objCommon.DisplayMessage(this.Page, "Record already exists.", Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error.", Page);
                return;
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "LetterTemplate.btnFieldSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
        }

    }

  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearcontrols();
    }
    private void clearcontrols()
    {
        ddlLetterType.SelectedIndex = 0;
        ddlDataField.Items.Clear();
        ddlDataField.Items.Insert(0, new ListItem("Please Select", "0"));
        txtLetterTemplateName.Text = string.Empty;
        txtShortDesc.Text = string.Empty;
        txtLetterText.Text = string.Empty;
        ViewState["action"] = "add";
        BindListView(); 
    }

    protected void btnDatafieldCancel_Click(object sender, EventArgs e)
    {
        try
        {
            CleaDatField();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void CleaDatField()
    {
        ddllettertypeid.ClearSelection();
        txtdisplayfield.Text = string.Empty;
        txtdatafield.Text = string.Empty;        
        ViewState["action"] = "add";
        BindLstDataField();
    }

    protected void btnEditLetterTemplate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearcontrols();
            ImageButton btnEdit = sender as ImageButton;
            string lettertemplateid = btnEdit.CommandArgument.ToString();
            objLT.LetterTemplateId = Convert.ToInt32(lettertemplateid.ToString());
            DataSet dsGetTemplate = objLC.GetLetterTemplate(objLT);
            ViewState["action"] = "edit";
            if (dsGetTemplate.Tables[0].Rows.Count > 0)
            {
                ViewState["LetterTempId"] = lettertemplateid;
                txtLetterTemplateName.Text = dsGetTemplate.Tables[0].Rows[0]["LETTER_TEMPLATE_NAME"].ToString();
                txtShortDesc.Text = dsGetTemplate.Tables[0].Rows[0]["SHORT_DESC"].ToString();
                ddlLetterType.SelectedValue = dsGetTemplate.Tables[0].Rows[0]["LETTER_TYPE_ID"].ToString();
                if (ddlLetterType.SelectedIndex > 0)
                {
                    BindDataField();
                }
                txtLetterText.Text = dsGetTemplate.Tables[0].Rows[0]["LETTER_TEXT"].ToString();
                if (dsGetTemplate.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "True")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatus(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatus(false);", true);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.btnEditTemplate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        hdnSampleText.Value = string.Empty;
        SampleName.Text = string.Empty;
        string sampletext = objCommon.LookUp("ACD_ADMP_LETTER_TEMPLATE_TYPE", "SAMPLE_LETTER_TEXT", "LETTER_TYPE_ID=" + Convert.ToInt32(ddlLetterType.SelectedValue) + " AND ACTIVE_STATUS=1 AND MODULE_TYPE='ADMP'");
        if (sampletext != string.Empty)
        {
            SampleName.Text = ddlLetterType.SelectedItem.Text;
            hdnSampleText.Value = sampletext.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ModalPopupShowSample();</script>", false);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Sample Not Found.", Page);
            return;
        }
    }

    protected void BindLstDataField()
    {
        try 
        {
            Obj_ltfd.LetterTemplateId = 0;
            DataSet ds = objLC.GetLetterTemplate_DATA(Obj_ltfd);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlfieldtemplate.Visible = true;
                datafield_listview.DataSource = ds;
                datafield_listview.DataBind();
            }
            else
            {
                pnlfieldtemplate.Visible = false;
                datafield_listview.DataSource = null;
                datafield_listview.DataBind();
            }
        }
        catch(Exception ex) 
        {
            throw ex;
        }
    }

    protected void ddltemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Obj_ltfd.LetterTemplateId = Convert.ToInt32(ddllettertypeid.SelectedValue);
            DataSet ds = objLC.GetLetterTemplate_BYTEMPID(Obj_ltfd);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlfieldtemplate.Visible = true;
                datafield_listview.DataSource = ds;
                datafield_listview.DataBind();
            }
            else
            {

                pnlfieldtemplate.Visible = false;
                datafield_listview.DataSource = null;
                datafield_listview.DataBind();

            }
        }
        catch(Exception ex) 
        {
            objCommon.ShowError(Page, "LetterTemplate.ddltemplate_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }


    protected void datafield_listview_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["LetterTempId"] = id;
            ShowDataFieldDetail(id);

            ViewState["action"] = "edit";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LetterTemplate.ShowDataFieldDetail() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDataFieldDetail(int ID)
    {
        Obj_ltfd.LetterTemplateId = ID;
        DataSet ds = objLC.GetLetterTemplate_DATA(Obj_ltfd);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddllettertypeid.SelectedValue = ds.Tables[0].Rows[0]["LETTER_TYPE_ID"].ToString();
            txtdatafield.Text = ds.Tables[0].Rows[0]["DATAFIELD"].ToString();
            txtdisplayfield.Text = ds.Tables[0].Rows[0]["DATAFIELD_DISPLAY"].ToString();
        }
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
            // btnmodal.Attributes.Add("class", "nav-link active");
            //  etemplate.Attributes.Add("class", "nav-link");           
            HiddenField1.Value = "1";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
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