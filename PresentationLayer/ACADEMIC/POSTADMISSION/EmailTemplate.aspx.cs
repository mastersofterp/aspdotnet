using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using System.Data;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_EmailTemplate : System.Web.UI.Page
{
    Common objCommon = new Common();
    EmailTemplateController objEmailTemp = new EmailTemplateController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            Session["TempID"] = "0";
            FillDropDown();
            BindListView();
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "LoadTinyMCE();", true);
        }
    }

    #region Selection Type,DataField
    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlTemplateType, "ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE", "TEMPTYPEID > 0", "TEMPTYPEID");
        }
        catch (Exception ex)
        {
            //throw;
            objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

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
            objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.ddlTemplateType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
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
            objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.BindDataField() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region ListView Edit Button
    private void BindListView()
    {
        try
        {

            DataSet ds = objEmailTemp.GetRetAll_EmailTemplate(0);

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

            foreach (ListViewDataItem dataitem in lvTemplateDetails.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
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
    protected void btnEditTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            string id = btnEdit.CommandArgument.ToString();
            dt = objEmailTemp.GetRetAll_EmailTemplate(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                //TEMPLATEID,TEMPTYPEID,TEMPLATENAME,EMAILSUBJECT,TEMPLATETEXT,ACTIVESTATUS
                Session["TempID"] = id;

                txtTemplateName.Text = dt.Rows[0]["TEMPLATENAME"].ToString();
                txtEmailSubject.Text = dt.Rows[0]["EMAILSUBJECT"].ToString();
                ddlTemplateType.SelectedValue = dt.Rows[0]["TEMPTYPEID"].ToString();
                if (ddlTemplateType.SelectedIndex > 0)
                {
                    BindDataField();
                }
                txtCourseOutline.Text = dt.Rows[0]["TEMPLATETEXT"].ToString();
                ////ScriptManager.RegisterStartupScript(this, GetType(), "Src", "LoadTinyMCE();", true);

                if (dt.Rows[0]["ACTIVESTATUS"].ToString() == "True")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(false);", true);
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
    private void ClearEmailTemplate()
    {
        ViewState["action"] = "add";
        Session["TempID"] = "0";

        txtTemplateName.Text = string.Empty;
        txtEmailSubject.Text = string.Empty;
        txtCourseOutline.Text = "";

        ddlTemplateType.ClearSelection();
        ddlDataField.Items.Clear();
        BindListView();

    }
    #endregion

    #region Button Add DataField, Submit and Cancel
    //myTextBox.Text = myTextBox.Text.Insert(myTextBox.SelectionStart, "Hello world");
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            //string cursorPos = hfdCursorPos.Value;
            //return;
            //var insertText = "Text";
            //var selectionIndex = txtCourseOutline.SelectionStart;
            //txtCourseOutline.Text = txtCourseOutline.Text.Insert(selectionIndex, insertText);
            //txtCourseOutline.SelectionStart = selectionIndex + insertText.Length;
            //txtCourseOutline.Text = txtCourseOutline.Text.Insert(txtCourseOutline.SelectionStart, "Hello world");

            /*if (txtCourseOutline.Text.ToString().Length > 2)
            {
                txtCourseOutline.Text = txtCourseOutline.Text.ToString().Substring(0, txtCourseOutline.Text.ToString().Length - 4) + "[" + ddlDataField.SelectedItem.Text + "]</p>";
            }
            else
            {
                txtCourseOutline.Text = txtCourseOutline.Text + "[" + ddlDataField.SelectedItem.Text + "]";
            }*/
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACADEMIC_EmailTemplate.btnTemplate_Click() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["TempID"] = "0";
        ViewState["action"] = "add";
        ClearEmailTemplate();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCourseOutline.Text.Trim()))
        {
            string existmsg = "Please Enter Email Template Pattern.";
            objCommon.DisplayMessage(existmsg, this.Page);
            return;
        }

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            ////TEMPLATEID,TEMPTYPEID,TEMPLATENAME,EMAILSUBJECT,TEMPLATETEXT,ACTIVESTATUS
            int TEMPLATEID = 0;
            int TEMPTYPEID = Convert.ToInt32(ddlTemplateType.SelectedValue);
            string TEMPLATENAME = txtTemplateName.Text.Trim();
            string EMAILSUBJECT = txtEmailSubject.Text.Trim();
            string logo_path = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString().Trim();
            ////string TEMPLATETEXT = "<img id=\"ctl00_Img1\" src=\"https://admissionsrajagiri.mastersofterp.in/IMAGES/email_template_header_logo.png\" alt=\"Rajagiri-logo\" width=\"250px\" height=\"105px\"/>" + txtCourseOutline.Text.Trim();
           // string TEMPLATETEXT = "<img id=\"ctl00_Img1\" src=\"" + logo_path + "/IMAGES/email_template_header_logo.png\" alt=\"Rajagiri-logo\" width=\"250px\" height=\"105px\"/>" + txtCourseOutline.Text.Trim();
            string TEMPLATETEXT = txtCourseOutline.Text.Trim();

            bool ACTIVESTATUS = false;
            if (hfdActive.Value == "true")
            {
                ACTIVESTATUS = true;
            }

            /*if (string.IsNullOrEmpty(TEMPLATETEXT))
            {
                string existmsg = "Please Enter Email Template Pattern.";
                objCommon.DisplayMessage(existmsg, this.Page);
                return;
            }*/

            int ret = 0;
            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["TempID"].ToString()))
                {
                    TEMPLATEID = Convert.ToInt32(Session["TempID"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objEmailTemp.InsertUpdate_EmailTemplate(TEMPLATEID, TEMPTYPEID, TEMPLATENAME, EMAILSUBJECT, TEMPLATETEXT, ACTIVESTATUS));
            if (ret == 2)
            {
                displaymsg = "Record alreday exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearEmailTemplate();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
        }
        else
            Response.Redirect("~/default.aspx");

    }
    #endregion

}