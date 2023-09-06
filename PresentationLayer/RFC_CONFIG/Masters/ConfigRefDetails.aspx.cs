//======================================================================================
// PROJECT NAME  : RFC_common                                                                
// MODULE NAME   : CONFIG REFERENCE DETAILS                       
// CREATION DATE : 20-10-2021                                                       
// CREATED BY    : RISHABH BAJIRAO   
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using System.Data;

public partial class RFC_CONFIG_Masters_ConfigRefDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigRefDetails objCon = new ConfigRefDetails();
    ConfigRefController objRefCntr = new ConfigRefController();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //if (Session["masterpage"] != null)
        //    objCommon.SetMasterPage(Page, "ConfigSiteMasterPage.master");
        //else
        //    objCommon.SetMasterPage(Page, "ConfigSiteMasterPage.master");


        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //FillDropDown();
            BindListView();
            //EDIT
            ViewState["actionCongif"] = "add";
        }
    }
    #endregion Page Events

    #region Check Authorization
    //Page Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_OrgStructure.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AffilationType.aspx");
        }
    }
    #endregion Check Authorization

    #region Button Click Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCon.OrganizationId = Convert.ToInt32(ddlOrganization.SelectedValue);
            objCon.ProjectName = txtProjectName.Text.Trim();
            objCon.ServerName = txtServerName.Text.Trim();
            objCon.UserId = txtUserID.Text.Trim();
            objCon.Password = txtPassword.Text.Trim();
            objCon.DBName = txtDatabaseName.Text.Trim();
            objCon.OrganizationUrl = txtOrgUrl.Text.Trim(); //Added By Rishabh on 04/12/2021
            objCon.DefaultPage = txtDefaultpage.Text.Trim();//Added By Rishabh on 07/12/2021

            if (ViewState["actionCongif"] != null)
            {
                if (ViewState["actionCongif"].ToString().Equals("edit"))
                {
                    objCon.ReferenceDetailsId = Convert.ToInt32(ViewState["id"]);
                }
                CustomStatus cs = (CustomStatus)objRefCntr.SaveConfigRefDetails(objCon);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Record Updated sucessfully", this.Page);
                    BindListView();
                    ClearAllFields();
                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Record Added sucessfully", this.Page);
                    BindListView();
                    ClearAllFields();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                    BindListView();
                    ClearAllFields();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["id"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionCongif"] = "edit";
            this.ShowDetails(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Button Click Events

    #region Methods
    private void FillDropDown()
    {
        
        objCommon.FillDropDownList(ddlOrganization, "tblConfigOrganizationMaster", "OrganizationId", "OrgName", "ActiveStatus=1", "OrganizationId");
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objRefCntr.GetConfigDetails();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlConfigRef.Visible = true;
                lvConfigRef.DataSource = ds;
                lvConfigRef.DataBind();
            }
            else
            {
                pnlConfigRef.Visible = false;
                lvConfigRef.DataSource = null;
                lvConfigRef.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlOrganization.DataTextField = ds.Tables[1].Columns["OrgName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlOrganization.DataValueField = ds.Tables[1].Columns["OrganizationId"].ToString();
                ddlOrganization.DataSource = ds.Tables[1];      //assigning datasource to the dropdownlist  
                ddlOrganization.DataBind();  //binding dropdownlist 
            }
            else
            {
                ddlOrganization.DataSource = null;
                ddlOrganization.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ClearAllFields()
    {
        ddlOrganization.SelectedIndex = 0;
        txtProjectName.Text = string.Empty;
        txtServerName.Text = string.Empty;
        txtUserID.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtDatabaseName.Text = string.Empty;
        txtOrgUrl.Text = string.Empty;
        txtDefaultpage.Text = string.Empty;
        //ViewState["actionCongif"] = null;
        ViewState["actionCongif"] = "add";
    }

    private void ShowDetails(int id)
    {
        try
        {
            DataSet ds = null;
            ds = objRefCntr.GetConfigDetailsById(id);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ReferenceDetailsId"] = id.ToString();
                    //  if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "false")

                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["ActiveStatus"]))
                    {
                        ddlOrganization.SelectedValue = ds.Tables[0].Rows[0]["OrganizationId"] == null ? string.Empty : ds.Tables[0].Rows[0]["OrganizationId"].ToString();
                        txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"] == null ? string.Empty : ds.Tables[0].Rows[0]["ProjectName"].ToString();
                        txtServerName.Text = ds.Tables[0].Rows[0]["ServerName"] == null ? string.Empty : ds.Tables[0].Rows[0]["ServerName"].ToString();
                        txtUserID.Text = ds.Tables[0].Rows[0]["UserId"] == null ? string.Empty : ds.Tables[0].Rows[0]["UserId"].ToString();
                        txtPassword.Text = ds.Tables[0].Rows[0]["Password"] == null ? string.Empty : ds.Tables[0].Rows[0]["Password"].ToString();
                        txtDatabaseName.Text = ds.Tables[0].Rows[0]["DBName"] == null ? string.Empty : ds.Tables[0].Rows[0]["DBName"].ToString();
                        txtOrgUrl.Text = ds.Tables[0].Rows[0]["URL_LINK"] == null ? string.Empty : ds.Tables[0].Rows[0]["URL_LINK"].ToString();
                        txtDefaultpage.Text = ds.Tables[0].Rows[0]["DEFAULT_PAGE"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEFAULT_PAGE"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, ds.Tables[0].Rows[0]["OrgName"].ToString() + " Organization is Inactive", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.ShowDetails-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Methods

    


}