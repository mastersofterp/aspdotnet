//======================================================================================
// PROJECT NAME  : RFC_CODE                                                                
// MODULE NAME   : PBI Configuration                 
// CREATION DATE : 11-03-2022                                                        
// CREATED BY    : DIKSHA NANDURKAR  
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic;
using BusinessLogicLayer.BusinessEntities.Academic;

public partial class PBIConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PBIConfigurationController objPCC = new PBIConfigurationController();
    PBIConfigurationEntity objPCE = new PBIConfigurationEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindListView();
            BindSubWorkspaceListView();
            PbiConfigurationListView();
            FillDropDown();
        }

    }

    public void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlWorkspaceName, "ACD_PBI_CONFIGURATION_WORKSPACE_MASTER", "WORKSPACE_ID", "WORKSPACE_NAME", "WORKSPACE_ID>0", "WORKSPACE_ID");

            objCommon.FillDropDownList(ddlworkspace, "ACD_PBI_CONFIGURATION_WORKSPACE_MASTER", "WORKSPACE_ID", "WORKSPACE_NAME", "WORKSPACE_ID>0", "WORKSPACE_ID");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PBIConfiguration.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }





    protected void btnSubmitWorkspace_Click(object sender, EventArgs e)
    {
        objPCE.workspace_name = txtWorkspaceName.Text.Trim();


        if (hfdActive.Value == "true")
        {
            objPCE.status = true;
        }
        else
        {
            objPCE.status = false;
        }

        objPCE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {


            objPCE.Workspace_id = Convert.ToInt32(ViewState["wpid"]);
            CustomStatus cs = (CustomStatus)objPCC.UpdateWorkspaceData(objPCE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearData();
                BindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["action"] = null;
            }
        }
        else
        {
            //Edit 
            CustomStatus cs = (CustomStatus)objPCC.InsertWorkspaceData(objPCE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                ClearData();


            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearData();

            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearData();


            }
            BindListView();
        }
    }
    protected void lvConfiguration_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void BindListView()
    {
        try
        {

            DataSet ds = objPCC.GetListview();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelWorkapce.Visible = true;
                lvWorkapce.DataSource = ds.Tables[0];
                lvWorkapce.DataBind();
            }
            else
            {
                PanelWorkapce.Visible = true;
                lvWorkapce.DataSource = null;
                lvWorkapce.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvWorkapce.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "INACTIVE")
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
                objUCommon.ShowError(Page, "PBIConfiguration.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnEditWorkspace_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditWorkspace = sender as LinkButton;
            int WORKSPACE_ID = Convert.ToInt32(btnEditWorkspace.CommandArgument);
            ViewState["wpid"] = Convert.ToInt32(btnEditWorkspace.CommandArgument);
            ShowDetail(WORKSPACE_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PbiConfiguration.btnEditWorkspace_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetail(int WORKSPACE_ID)
    {

        DataSet ds = objPCC.EditWorkspaceData(WORKSPACE_ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtWorkspaceName.Text = ds.Tables[0].Rows[0]["WORKSPACE_NAME"].ToString();

            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "ACTIVE")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWorkspace(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWorkspace(false);", true);
            }


        }
    }
    protected void btnCancelWorkspace_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    public void ClearData()
    {
        txtWorkspaceName.Text = "";
    }


    protected void btnSubmiteSubWorkspace_Click(object sender, EventArgs e)
    {
        objPCE.sub_workspace_name = txtSubWorkspace.Text.Trim();
        objPCE.Workspace_id = Convert.ToInt32(ddlWorkspaceName.SelectedValue);


        if (hfStatusubworkspace.Value.ToLower() == "true")
        {
            objPCE.status = true;
        }
        else
        {
            objPCE.status = false;
        }
        objPCE.OrganizationId = Convert.ToInt32(Session["OrgId"]);
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            objPCE.sub_Workspace_id = Convert.ToInt32(ViewState["sid"]);
            CustomStatus cs = (CustomStatus)objPCC.UpdateSubWorkspaceData(objPCE);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearSubworkspaceData();
                BindSubWorkspaceListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["action"] = null;
            }

        }

        else
        {
            CustomStatus cs = (CustomStatus)objPCC.InsertSubWorkspaceData(objPCE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ClearSubworkspaceData();
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                ClearSubworkspaceData();
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);

            }
            else
            {
                ClearSubworkspaceData();
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);

            }
            BindSubWorkspaceListView();

        }
    }

    protected void lvEvent_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void btnEditSubworkspace_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditSubworkspace = sender as LinkButton;
            int SUB_WORKSPACE_ID = Convert.ToInt32(btnEditSubworkspace.CommandArgument);
            ViewState["sid"] = Convert.ToInt32(btnEditSubworkspace.CommandArgument);
            ShowSubworkspaceDetail(SUB_WORKSPACE_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PBIConfiguration.btnEditPbi_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowSubworkspaceDetail(int SUB_WORKSPACE_ID)
    {
        DataSet ds = objPCC.EditSubWorkspaceData(SUB_WORKSPACE_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtSubWorkspace.Text = ds.Tables[0].Rows[0]["SUB_WORKSPACE_NAME"].ToString();
            ddlWorkspaceName.SelectedValue = ds.Tables[0].Rows[0]["WORKSPACE_ID"].ToString();

            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "ACTIVE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSubWorkspace(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSubWorkspace(false);", true);
            }

        }
    }

    protected void BindSubWorkspaceListView()
    {
        try
        {

            DataSet ds = objPCC.GetSubWorkspaceListview();

            if (ds.Tables[0].Rows.Count > 0)
            {
                panelSubWorkspace.Visible = true;
                lvSubWorkspace.DataSource = ds.Tables[0];
                lvSubWorkspace.DataBind();
            }
            else
            {
                panelSubWorkspace.Visible = true;
                lvSubWorkspace.DataSource = null;
                lvSubWorkspace.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvSubWorkspace.Items)
            {
                Label Status = dataitem.FindControl("lblWStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "INACTIVE")
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
                objUCommon.ShowError(Page, "PBIConfiguration.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancelSubWorkspace_Click(object sender, EventArgs e)
    {
        ClearSubworkspaceData();
    }
    public void ClearSubworkspaceData()
    {
        txtSubWorkspace.Text = "";
        ddlWorkspaceName.SelectedIndex = 0;
    }

    protected void btnSubmitPbilink_Click(object sender, EventArgs e)
    {
        objPCE.pbi_link_name = txtPbiLink.Text.Trim();
        objPCE.Workspace_id = Convert.ToInt32(ddlworkspace.SelectedValue);

        objPCE.sub_Workspace_id = Convert.ToInt32(ddlSubWorkspace.SelectedValue);

        if (hfStatuspbi.Value.ToLower() == "true")
        {
            objPCE.status = true;
        }
        else
        {
            objPCE.status = false;
        }
        objPCE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            objPCE.pbi_link_configuration = Convert.ToInt32(ViewState["pid"]);
            int WORKSPACE_ID = Convert.ToInt32(ddlworkspace.SelectedValue);
            objCommon.FillDropDownList(ddlSubWorkspace, "ACD_PBI_CONFIGURATION_SUB_WORKSPACE", "SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", " WORKSPACE_ID =" + Convert.ToInt32(ddlworkspace.SelectedValue), "SUB_WORKSPACE_ID");
            CustomStatus cs = (CustomStatus)objPCC.UpdatePbiData(objPCE);

            if (cs.Equals(CustomStatus.RecordUpdated))

                PbiConfigurationListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ViewState["action"] = null;
            ClearpbiLinkData();

        }

        else
        {
            CustomStatus cs = (CustomStatus)objPCC.InsertPbiLinkData(objPCE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearpbiLinkData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearpbiLinkData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearpbiLinkData();
            }
            PbiConfigurationListView();
        }

    }

    protected void lvCategory_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void btnEditPbi_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditPbi = sender as LinkButton;
            int PBI_LINK_CONFIGRATION_ID = Convert.ToInt32(btnEditPbi.CommandArgument);
            ViewState["pid"] = Convert.ToInt32(btnEditPbi.CommandArgument);
            ShowPbiConfigurationDetail(PBI_LINK_CONFIGRATION_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PBIConfiguration.btnEditPbi_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowPbiConfigurationDetail(int PBI_LINK_CONFIGRATION_ID)
    {
        DataSet ds = objPCC.EditPbiLinkData(PBI_LINK_CONFIGRATION_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtPbiLink.Text = ds.Tables[0].Rows[0]["PBI_LINK_NAME"].ToString();
            ddlworkspace.SelectedValue = ds.Tables[0].Rows[0]["WORKSPACE_ID"].ToString();
            int WORKSPACE_ID = Convert.ToInt32(ddlworkspace.SelectedValue);
            objCommon.FillDropDownList(ddlSubWorkspace, "ACD_PBI_CONFIGURATION_SUB_WORKSPACE", "SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", " WORKSPACE_ID =" + Convert.ToInt32(ddlworkspace.SelectedValue), "SUB_WORKSPACE_ID");
            ddlSubWorkspace.SelectedValue = ds.Tables[0].Rows[0]["SUB_WORKSPACE_ID"].ToString();



            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "ACTIVE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPbi(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPbi(false);", true);
            }
        }
    }
    protected void ddlworkspace_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlSubWorkspace, "ACD_PBI_CONFIGURATION_SUB_WORKSPACE", "SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", " WORKSPACE_ID =" + Convert.ToInt32(ddlworkspace.SelectedValue), "SUB_WORKSPACE_ID");

        }
        catch (Exception ex)
        {

        }
    }




    protected void PbiConfigurationListView()
    {
        try
        {

            DataSet ds = objPCC.GetPbiLinkListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelPbiLink.Visible = true;
                lvPbiLink.DataSource = ds.Tables[0];
                lvPbiLink.DataBind();
            }
            else
            {
                PanelPbiLink.Visible = true;
                lvPbiLink.DataSource = null;
                lvPbiLink.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvPbiLink.Items)
            {
                Label Status = dataitem.FindControl("lblPStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "INACTIVE")
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
                objUCommon.ShowError(Page, "PBIConfiguration.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnCancelpbiLink_Click(object sender, EventArgs e)
    {
        ClearpbiLinkData();
    }

    public void ClearpbiLinkData()
    {
        ddlworkspace.SelectedIndex = 0;
        ddlSubWorkspace.SelectedIndex = 0;
        txtPbiLink.Text = "";



    }
}





