using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mastersofterp_MAKAUAT;

public partial class ACADEMIC_CreateEmailTemplate : System.Web.UI.Page
{
    Common objCommon = new Common();
    ConfigController objConfigC = new ConfigController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();
                    CheckPageAuthorization();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    ViewState["action"] = "add";
                    Session["TempID"] = "0";
                    HiddenField1.Value = "0";
                    FillDropDown();
                    BindListView();
                    BindDatFieldListView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CreateEmailTemplate.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #region Selection Type,DataField
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateEmailTemplate.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateEmailTemplate.aspx");
        }
    }
    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlTemplateType, "ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE", "TEMPTYPEID > 0", "TEMPTYPEID");
            objCommon.FillDropDownList(ddltemplate, "ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE", "TEMPTYPEID > 0", "TEMPTYPEID");
            objCommon.FillDropDownList(ddlUserType, "User_Rights", "USERTYPEID", "USERDESC", "USERTYPEID > 0", "USERTYPEID");
            objCommon.FillDropDownList(ddlPageForTemplate, "ACCESS_LINK", "AL_No", "AL_Link", "AL_No > 0", "AL_No");

        }
        catch (Exception ex)
        {
            //throw;
            objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindDataField()
    {
        try
        {
            //TEMPDATAFIELDCONFIGID,TEMPTYPEID,	DATAFIELDDISPLAY, DATAFIELD
            objCommon.FillDropDownList(ddlDataField, "ACD_ADMP_EMAILTEMPLATE_DATAFIELD_CONFIGURATION", "DISTINCT TEMPDATAFIELDCONFIGID", "DATAFIELDDISPLAY", "TEMPTYPEID=" + ddlTemplateType.SelectedValue, "DATAFIELDDISPLAY");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "CreateEmailTemplate.BindDataField() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindDatFieldListView()
    {
        try
        {

            DataSet ds = objConfigC.GetRetAll_DatafieldsDetails(0);

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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CreateEmailTemplate.BindDatFieldListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListView()
    {
        try
        {

            DataSet ds = objConfigC.GetRetAll_CreateEmailTemplate(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlTemplate.Visible = true;
                lvTemplateDetails.DataSource = ds;
                lvTemplateDetails.DataBind();
            }
            else
            {

                pnlTemplate.Visible = false;
                lvTemplateDetails.DataSource = null;
                lvTemplateDetails.DataBind();

            }

            //foreach (ListViewDataItem dataitem in lvTemplateDetails.Items)
            //{
            //    Label Status = dataitem.FindControl("lblStatus") as Label;

            //    string Statuss = (Status.Text);

            //    if (Statuss == "InActive")
            //    {
            //        Status.CssClass = "badge badge-danger";
            //    }
            //    else
            //    {
            //        Status.CssClass = "badge badge-success";
            //    }

            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CreateEmailTemplate.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ClearTemplate()
    {
        ViewState["action"] = "add";
        Session["TempID"] = "0";
        ddlUserType.SelectedIndex = 0;
        ddlPageForTemplate.SelectedIndex = 0;
        txtTemplateName.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtTemplateBody.Text = "";

        ddlTemplateType.SelectedIndex = 0;
        ddlDataField.Items.Clear();
        BindListView();

    }
    private void ClearTemplateDataField()
    {
        ViewState["action"] = "add";
        Session["TempID"] = "0";
        txtdatafield.Text = string.Empty;
        txtdisplayfield.Text = string.Empty;
        ddltemplate.ClearSelection();
        BindDatFieldListView();

    }
    private void ShowDetail(int ID)
    {

        DataSet ds = objConfigC.GetRetAll_CreateEmailTemplate(ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtTemplateName.Text = ds.Tables[0].Rows[0]["TEMP_NAME"].ToString();
            txtSubject.Text = ds.Tables[0].Rows[0]["TEMP_SUBJECT"].ToString();
            ddlUserType.SelectedValue = ds.Tables[0].Rows[0]["USERTYPEID"].ToString();
            ddlPageForTemplate.SelectedValue = ds.Tables[0].Rows[0]["AL_No"].ToString();
            ddlTemplateType.SelectedValue = ds.Tables[0].Rows[0]["TEMPTYPEID"].ToString();
            objCommon.FillDropDownList(ddlDataField, "ACD_ADMP_EMAILTEMPLATE_DATAFIELD_CONFIGURATION", "DISTINCT TEMPDATAFIELDCONFIGID", "DATAFIELDDISPLAY", "TEMPTYPEID=" + ddlTemplateType.SelectedValue, "DATAFIELDDISPLAY");

            ddlDataField.SelectedValue = ds.Tables[0].Rows[0]["TEMPDATAFIELDCONFIGID"].ToString();
            txtTemplateBody.Text = ds.Tables[0].Rows[0]["TEMP_BODY"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(false);", true);
            }


        }

    }
    #endregion
    protected void ddlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDataField.Items.Clear();
            if (ddlTemplateType.SelectedIndex > 0)
            {
                BindDataField();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "LoadTinyMCE();", true);
            //txtCourseOutline.Text = txtCourseOutline.Text;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "CreateEmailTemplate.ddlTemplateType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(ID);

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CreateEmailTemplate.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTemplate();
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnDatafieldCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTemplateDataField();
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void btnDfieldSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int TemplateType = Convert.ToInt32(ddltemplate.SelectedValue);
            string TemplatName = ddltemplate.SelectedItem.Text;
            string DisplayDatafname = txtdisplayfield.Text;
            string Datafieldname = txtdatafield.Text;


            // var plainText = HtmlUtilities.ConvertToPlainText(string html);
            int mode = 0;


            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Update
                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;
                CustomStatus cs = (CustomStatus)objConfigC.UpdateDataFiledEmailTemplate(id, TemplateType, TemplatName, DisplayDatafname, Datafieldname, mode);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearTemplateDataField();
                    BindDatFieldListView();
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {
                //Insert
                mode = 1;
                CustomStatus cs = (CustomStatus)objConfigC.InsertDataFiledEmailTemplate(0, TemplateType, TemplatName, DisplayDatafname, Datafieldname, mode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindDatFieldListView();
                    ClearTemplateDataField();

                }

                else
                {

                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearTemplate();
                }
                BindDatFieldListView();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Receipt_Code.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string tempName = txtTemplateName.Text;
            string subject = txtSubject.Text;
            int UserType = Convert.ToInt32(ddlUserType.SelectedValue);
            int PageForTemp = Convert.ToInt32(ddlPageForTemplate.SelectedValue);
            int tempType = Convert.ToInt32(ddlTemplateType.SelectedValue);
            int DataField = Convert.ToInt32(ddlDataField.SelectedValue);
            string TempBody = txtTemplateBody.Text;
            // var plainText = HtmlUtilities.ConvertToPlainText(string html);
            int mode = 0;
            int status;
            if (hfdActive.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Update
                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;
                CustomStatus cs = (CustomStatus)objConfigC.UpdateCreateEmailTemplate(id, tempName, subject, UserType, PageForTemp, tempType, DataField, TempBody, status, mode);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearTemplate();
                    BindListView();
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {
                //Insert
                mode = 1;
                CustomStatus cs = (CustomStatus)objConfigC.InsertCreateEmailTemplate(0, tempName, subject, UserType, PageForTemp, tempType, DataField, TempBody, status, mode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindListView();
                    ClearTemplate();

                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearTemplate();
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Receipt_Code.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void datafield_listview_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["id"] = id;
            ShowDataFieldDetail(id);

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CreateEmailTemplate.ShowDataFieldDetail() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDataFieldDetail(int ID)
    {

        DataSet ds = objConfigC.Bind_DatafieldsDetails(ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddltemplate.SelectedValue = ds.Tables[0].Rows[0]["TEMPTYPEID"].ToString();
            txtdatafield.Text = ds.Tables[0].Rows[0]["DATAFIELD"].ToString();
            txtdisplayfield.Text = ds.Tables[0].Rows[0]["DATAFIELDDISPLAY"].ToString();
        }
    }
    protected void ddltemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(ddltemplate.SelectedValue);
        DataSet ds = objConfigC.GetRetAll_DatafieldsDetails(ID);
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