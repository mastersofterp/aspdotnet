using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_DocumentMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhdC = new PhdController();

    protected void Page_Load(object sender, EventArgs e)
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

                BindListView();

            }
            ViewState["action"] = "add";

        }
    }

    protected void BindListView()
    {
        try
        {
            int id = 0;
            int mode = 2;
            DataSet ds = objPhdC.GetEditDocumentMappingList(id, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelDocument.Visible = true;
                lvDocumentMapping.DataSource = ds.Tables[0];
                lvDocumentMapping.DataBind();
            }
            else
            {
                PanelDocument.Visible = true;
                lvDocumentMapping.DataSource = null;
                lvDocumentMapping.DataBind();
                objCommon.DisplayMessage(this, "No record found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHD_DocumentMapping.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Clear()
    {
        txtDocumentName.Text = "";
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int status;
            string DocumentName = txtDocumentName.Text.Trim();
            int Mandatory;
            if (hfdActive.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (hfdStart.Value == "true")
            {
                Mandatory = 1;
            }
            else
            {
                Mandatory = 0;
            }
            int mode;
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                int id = Convert.ToInt32(ViewState["id"]);
                mode = 2;
                CustomStatus cs = (CustomStatus)objPhdC.UpdateDocumentMappingData(id, DocumentName, status, Mandatory, mode);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear();
                    BindListView();
                    objCommon.DisplayMessage(this, "Record updated successfully.", this.Page);
                    ViewState["action"] = null;
                }
            }
            else
            {
                mode = 1;
                CustomStatus cs = (CustomStatus)objPhdC.InsertDocumentMappingData(0, DocumentName, status, Mandatory, mode);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Record saved successfully.", this.Page);
                    BindListView();
                    Clear();
                }
                else
                {
                    objCommon.DisplayMessage(this, "Record already exist.", this.Page);
                    Clear();
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
   
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            int mode = 1;
            ShowDetail(ID, mode);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHD_DocumentMapping.btnEdit() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetail(int ID, int mode)
    {
        DataSet ds = objPhdC.GetEditDocumentMappingList(ID, mode);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtDocumentName.Text = ds.Tables[0].Rows[0]["DOCUMENTNAME"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
            }
            if (ds.Tables[0].Rows[0]["MANDATORY"].ToString() == "True")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatStart(true);", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatStart(false);", true);
            }
        }
    }
}