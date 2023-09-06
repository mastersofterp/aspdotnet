//======================================================================================
// PROJECT NAME  : RFC_common                                                                
// MODULE NAME   : MAPPING CREATION                        
// CREATION DATE : 14-10-2021                                                       
// CREATED BY    : S.Patil
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class RFC_CONFIG_Masters_OwnerStatusMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OwnershipStatusController objBC = new OwnershipStatusController();
    OwnershipStatus objOwn = new OwnershipStatus();
    ConfigAffilationTypeController objAffil = new ConfigAffilationTypeController();
    ConfigAffilationType objAff = new ConfigAffilationType();
    UniversityController objUC = new UniversityController();
    University objUNI = new University();
    InstituteTypeController objInsCntr = new InstituteTypeController();
    InstituteType objInsti = new InstituteType();
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
                //Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }
            BindListView();
            BindListViewAffilation();
            BindListViewUniversity();
            BindListViewInstitute();
            BindListViewConfgRef();
            FillDropDown();
            //EDIT
            ViewState["action"] = "add";
            ViewState["actionAffilation"] = "add";
            ViewState["actionUniv"] = "add";
            ViewState["actionInstitute"] = "add";
            ViewState["actionCongif"] = "add";
            //trQualLevel.Visible = false;
            //lblQuaLevel.Visible = false;
            //ddlQualification.Visible = false;
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion Page Events

    #region Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_OwnerStatusMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_OwnerStatusMaster.aspx");
        }
    }
    #endregion Check Authorization


    #region Ownership Status Master
    #region Button Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objOwn.OwnershipStatusName = txtOwnershipStatusName.Text.Trim();
            if (hfdStat.Value == "true")
            {
                objOwn.IsActive = true;
            }
            else
            {
                objOwn.IsActive = false;
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["ownid"] != null)
                {
                    objOwn.OwnershipStatusId = Convert.ToInt32(ViewState["ownid"]);
                }
                CustomStatus cs = (CustomStatus)objBC.SaveOwnershipStatusData(objOwn);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updBatch, "Record Already exist", this.Page);
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OwnerStatusMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int qualifyexamno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(qualifyexamno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OwnerStatusMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Button Click Events

    #region Methods

    private void ShowDetail(int id)
    {
        DataSet ds = null;
        ds = objBC.GetOwnershipStatusDataById(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ownid"] = id.ToString();
                txtOwnershipStatusName.Text = ds.Tables[0].Rows[0]["OwnershipStatusName"] == null ? string.Empty : ds.Tables[0].Rows[0]["OwnershipStatusName"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatOwner(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatOwner(false);", true);
                }
            }
        }
    }

    private void Clear()
    {
        txtOwnershipStatusName.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["ownid"] = null;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetOwnershipStatusDataById(0);
            lvOwnership.DataSource = ds;
            lvOwnership.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OwnerStatusMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Methods

    #endregion Ownership Status Master


    #region Affilation Type

    #region Button Click Events
    //To Save Data
    protected void btnSaveAffilation_Click(object sender, EventArgs e)
    {
        try
        {
            objAff.AffilationName = txtAffilationName.Text.Trim();
            if (hfdStatAff.Value == "true")
            {
                objAff.ActiveStatus = true;
            }
            else
            {
                objAff.ActiveStatus = false;
            }

            //Check whether to add or update
            if (ViewState["actionAffilation"] != null)
            {
                if (ViewState["AffilationTypeId"] != null)
                {
                    objAff.AffilationTypeId = Convert.ToInt32(ViewState["AffilationTypeId"]);
                }
                CustomStatus cs = (CustomStatus)objAffil.SaveAffilationTypeData(objAff);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ClearAffilation();
                    objCommon.DisplayMessage(this.updAffilation, "Record Saved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearAffilation();
                    objCommon.DisplayMessage(this.updAffilation, "Record Updated Successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updAffilation, "Record Already exist", this.Page);
                }
                BindListViewAffilation();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.btnSaveAffilation_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //To Clear Fields
    protected void btnCancelAffilation_Click(object sender, EventArgs e)
    {
        ClearAffilation();
    }
    //To Edit Data
    protected void btnEditAffilation_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetailAffilation(editno);
            ViewState["actionAffilation"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.btnEditAffilation_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Button Click Events

    #region Methods
    //To Show Edit Record Details
    private void ShowDetailAffilation(int id)
    {
        DataSet ds = null;
        ds = objAffil.GetAffilationTypeData(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["AffilationTypeId"] = id.ToString();
                txtAffilationName.Text = ds.Tables[0].Rows[0]["AffilationName"] == null ? string.Empty : ds.Tables[0].Rows[0]["AffilationName"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatAffilation(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatAffilation(false);", true);
                }
            }
        }
    }
    //To Clear Fields
    private void ClearAffilation()
    {
        txtAffilationName.Text = string.Empty;
        ViewState["actionAffilation"] = "add";
        ViewState["AffilationTypeId"] = null;
    }
    //To Bind listview
    private void BindListViewAffilation()
    {
        try
        {
            DataSet ds = objAffil.GetAffilationTypeData(0);
            lvAffilation.DataSource = ds;
            lvAffilation.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.BindListViewAffilation-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Methods

    #endregion Affilation Type


    #region Define University

    #region Button Click Events
    protected void btnSaveUniversity_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUniversityName.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(updUniversity, "Please Enter University Name", this.Page);
            }
            else if (ddlState.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updUniversity, "Please Select State", this.Page);
            }
            else
            {

                objUNI.UniversityName = txtUniversityName.Text.Trim();
                objUNI.Stateid = Convert.ToInt32(ddlState.SelectedValue);
                if (hfdStatUniv.Value == "true")
                {
                    objUNI.Status = true;
                }
                else
                {
                    objUNI.Status = false;
                }
                //Check whether to add or update
                if (ViewState["actionUniv"] != null)
                {

                    if (ViewState["uniid"] != null)
                    {
                        objUNI.Universityid = Convert.ToInt32(ViewState["uniid"]);
                    }
                    CustomStatus cs = (CustomStatus)objUC.SaveUniversityMasterData(objUNI);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearUniversity();
                        objCommon.DisplayMessage(this.updUniversity, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearUniversity();
                        objCommon.DisplayMessage(this.updUniversity, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updUniversity, "Record Already exist", this.Page);
                    }

                    BindListViewUniversity();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.btnSaveUniversity_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelUniv_Click(object sender, EventArgs e)
    {
        ClearUniversity();
    }
    protected void btnEditUniv_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int universityno = int.Parse(btnEdit.CommandArgument);
            ShowDetailUniv(universityno);
            ViewState["actionUniv"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.btnEditUniv_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Button Click Events

    #region Methods

    private void ShowDetailUniv(int id)
    {
        DataSet ds = null;
        ds = objUC.GetUniversityMasterDateByid(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["uniid"] = id.ToString();
                txtUniversityName.Text = ds.Tables[0].Rows[0]["UNIVERSITYNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["UNIVERSITYNAME"].ToString();
                //objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatUniv(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatUniv(false);", true);
                }
            }
        }
    }
    private void BindListViewUniversity()
    {
        try
        {
            DataSet ds = objUC.GetUniversityMasterDateByid(0);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvUniversity.DataSource = ds.Tables[0];
                lvUniversity.DataBind();
            }
            else
            {
                lvUniversity.DataSource = null;
                lvUniversity.DataBind();
            }

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlState.DataSource = ds.Tables[1];
                ddlState.DataTextField = ds.Tables[1].Columns["STATENAME"].ToString();
                ddlState.DataValueField = ds.Tables[1].Columns["STATENO"].ToString();
                ddlState.DataBind();
            }
            else
            {
                ddlState.DataSource = null;
                ddlState.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.BindListViewUniversity-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ClearUniversity()
    {
        txtUniversityName.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ViewState["actionUniv"] = "add";
        ViewState["uniid"] = null;
    }
    #endregion Methods

    #endregion Define University


    #region College Type / Institute Type
    #region Button Click Events
    protected void btnSaveInstitute_Click(object sender, EventArgs e)
    {
        try
        {
            objInsti.InstituteTypeName = txtInstituteTypeName.Text.Trim();
            if (hfdStatInsti.Value == "true")
            {
                objInsti.IsActive = true;
            }
            else
            {
                objInsti.IsActive = false;
            }

            //Check whether to add or update
            if (ViewState["actionInstitute"] != null)
            {
                if (ViewState["InstitutionTypeId"] != null)
                {
                    objInsti.InstituteTypeNo = Convert.ToInt32(ViewState["InstitutionTypeId"]);
                }
                CustomStatus cs = (CustomStatus)objInsCntr.SaveInstituteTypeData(objInsti);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ClearInstitute();
                    objCommon.DisplayMessage(this.updInstitute, "Record Saved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearInstitute();
                    objCommon.DisplayMessage(this.updInstitute, "Record Updated Successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updInstitute, "Record already exist", this.Page);
                }
                BindListViewInstitute();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.btnSaveInstitute_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelInstitute_Click(object sender, EventArgs e)
    {
        ClearInstitute();
    }

    protected void btnEditInstitute_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetailInstitute(editno);
            ViewState["actionInstitute"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.btnEditInstitute_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Button Click Events

    #region Methods
    private void ShowDetailInstitute(int id)
    {
        DataSet ds = null;
        ds = objInsCntr.GetInstituteTypeDataById(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["InstitutionTypeId"] = id.ToString();
                txtInstituteTypeName.Text = ds.Tables[0].Rows[0]["InstitutionTypeName"] == null ? string.Empty : ds.Tables[0].Rows[0]["InstitutionTypeName"].ToString();

                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatInstitute(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatInstitute(false);", true);
                }
            }
        }
    }

    private void ClearInstitute()
    {
        txtInstituteTypeName.Text = string.Empty;
        //IsActive.Checked = true;
        ViewState["actionInstitute"] = "add";
        ViewState["InstitutionTypeId"] = null;
    }

    private void BindListViewInstitute()
    {
        try
        {
            DataSet ds = objInsCntr.GetInstituteTypeDataById(0);
            lvInstituteType.DataSource = ds;
            lvInstituteType.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvInstituteType);//Set label - 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.BindListViewInstitute-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Methods

    #endregion College Type


    #region ConfigReferenceDetails
    #region Button Click Events
    protected void btnSubmitConfgRef_Click(object sender, EventArgs e)
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
                    BindListViewConfgRef();
                    ClearAllConfgRef();
                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Record Added sucessfully", this.Page);
                    BindListViewConfgRef();
                    ClearAllConfgRef();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                    BindListViewConfgRef();
                    ClearAllConfgRef();
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancelConfgRef_Click(object sender, EventArgs e)
    {
        ClearAllConfgRef();
    }

    protected void btnEditConfgRef_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["id"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionCongif"] = "edit";
            this.ShowDetailsConfgRef(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.btnEditConfgRef_Click-> " + ex.Message + "" + ex.StackTrace);
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

    private void BindListViewConfgRef()
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
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.BindListViewConfgRef-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ClearAllConfgRef()
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

    private void ShowDetailsConfgRef(int id)
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
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_ConfigRefDetails.ShowDetailsConfgRef-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Methods

    #endregion ConfigReferenceDetails
}