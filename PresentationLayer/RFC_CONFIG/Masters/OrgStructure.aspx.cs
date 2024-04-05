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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
public partial class RFC_CONFIG_Masters_OrgStructure : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OrganizationController objOrg = new OrganizationController();
    Organization objOrgM = new Organization();
    ColgContoller objController = new ColgContoller();
    College objCollege = new College();
    Mapping objOrgMap = new Mapping();
    MappingCreationController objCont = new MappingCreationController();

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
            }

            FillDropDownColg();
            BindListViewColg();
            ViewState["editColg"] = "addcolg";
            BindCollegeListViewMap();
            BindMappingView(0);
            BindListViewOrg();
            ViewState["actionorg"] = "addorg";
            ViewState["PHOTOORG"] = null;
        }
        else
        {
            TabName.Value = Request.Form[TabName.UniqueID];

        }
    }
    #endregion Page Events
    #region Check Authorization
    //Page Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Session["rfcconfig"] = "rfcconfig";
            //if (Session["username"].ToString() == "superadmin")
            //{
            //    Session["userno"] = 1;
            //}
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
    #region Bind Dropdownlists
    private void FillDropDownColg()
    {
        //if (TabName.Value == "tab_1")
        //{
        DataSet ds = objOrg.GetDataToFillDropDownlist(0);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlInstTypeIdOrg.DataTextField = ds.Tables[0].Columns["InstitutionTypeName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlInstTypeIdOrg.DataValueField = ds.Tables[0].Columns["InstitutionTypeId"].ToString();
                ddlInstTypeIdOrg.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
                ddlInstTypeIdOrg.DataBind();  //binding dropdownlist 

                ddlInstituteTypeColg.DataTextField = ds.Tables[0].Columns["InstitutionTypeName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlInstituteTypeColg.DataValueField = ds.Tables[0].Columns["InstitutionTypeId"].ToString();
                ddlInstituteTypeColg.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
                ddlInstituteTypeColg.DataBind();  //binding dropdownlist 
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlOwnershipStatusIdOrg.DataTextField = ds.Tables[1].Columns["OwnershipStatusName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlOwnershipStatusIdOrg.DataValueField = ds.Tables[1].Columns["OwnershipStatusId"].ToString();
                ddlOwnershipStatusIdOrg.DataSource = ds.Tables[1];      //assigning datasource to the dropdownlist  
                ddlOwnershipStatusIdOrg.DataBind();  //binding dropdownlist 
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                ddlUniversityColg.DataTextField = ds.Tables[2].Columns["UniversityName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlUniversityColg.DataValueField = ds.Tables[2].Columns["UniversityId"].ToString();
                ddlUniversityColg.DataSource = ds.Tables[2];      //assigning datasource to the dropdownlist  
                ddlUniversityColg.DataBind();  //binding dropdownlist 
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                ddlOrgToMap.DataTextField = ds.Tables[3].Columns["OrgName"].ToString(); // text field name of table dispalyed in dropdown       
                ddlOrgToMap.DataValueField = ds.Tables[3].Columns["OrganizationId"].ToString();
                ddlOrgToMap.DataSource = ds.Tables[3];      //assigning datasource to the dropdownlist  
                ddlOrgToMap.DataBind();  //binding dropdownlist 
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                ddlStateColg.DataTextField = ds.Tables[4].Columns["STATENAME"].ToString(); // text field name of table dispalyed in dropdown       
                ddlStateColg.DataValueField = ds.Tables[4].Columns["STATENO"].ToString();
                ddlStateColg.DataSource = ds.Tables[4];      //assigning datasource to the dropdownlist  
                ddlStateColg.DataBind();  //binding dropdownlist 
            }
        }

        //objCommon.FillDropDownList(ddlInstTypeIdOrg, "tblConfigInstitutionTypeMaster", "InstitutionTypeId", "InstitutionTypeName", "ActiveStatus=1", "InstitutionTypeId");
        //objCommon.FillDropDownList(ddlOwnershipStatusIdOrg, "tblConfigOwnershipStatusMaster", "OwnershipStatusId", "OwnershipStatusName", "ActiveStatus=1", "OwnershipStatusId");
        ////}
        ////else if (TabName.Value == "tab_2")
        ////{
        //objCommon.FillDropDownList(ddlInstituteTypeColg, "tblConfigInstitutionTypeMaster", "InstitutionTypeId", "InstitutionTypeName", "ActiveStatus=1", "InstitutionTypeName DESC");
        //objCommon.FillDropDownList(ddlUniversityColg, "tblConfigUniversityMaster", "UniversityId", "UniversityName", "ActiveStatus=1", "UniversityName DESC");
        //objCommon.FillDropDownList(ddlStateColg, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME DESC");
        ////}
        ////else if (TabName.Value == "tab_3")
        ////{
        //objCommon.FillDropDownList(ddlOrgMap, "tblConfigOrganizationMaster", "OrganizationId", "OrgName", "ActiveStatus=1", "OrganizationId");
        ////}
    }
    #endregion Bind Dropdownlists

    #region org_master

    #region Dropdownlist Events
    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCollegeListView();
    }
    #endregion Dropdownlist Events

    #region Button Click Events
    protected void btnSubmitOrg_Click(object sender, EventArgs e)
    {
        byte[] ImageLogoByte;
        try
        {
            if (fuCollegeLogoOrg.PostedFile.FileName.Equals(string.Empty) && ViewState["PHOTOORG"] == null)
            {
                objCommon.DisplayMessage(updProg, "Please Select Logo", this);
                fuCollegeLogoOrg.Focus();
                return;
            }
            objOrgM.Name = txtorgName.Text.Trim();
            objOrgM.Address = txtAddress.Text.Trim();
            objOrgM.Email = txtemailadd.Text.Trim();
            objOrgM.Website = txtwebOrg.Text.Trim();
            objOrgM.ContactName = txtContactName.Text.Trim();
            objOrgM.ContactNo = txtContactNo.Text.Trim();
            objOrgM.ContactDesignation = txtContactDesign.Text.Trim();
            objOrgM.ContactEmail = txtContactEmail.Text.Trim();
            objOrgM.EstabishmentDate = txtEstdate.Text.Trim();
            objOrgM.InstitutionTypeId = Convert.ToInt32(ddlInstTypeIdOrg.SelectedValue);
            objOrgM.OwnershipStatusId = Convert.ToInt32(ddlOwnershipStatusIdOrg.SelectedValue);
            objOrgM.MISOrderDate = txtMISOrderDate.Text.Trim();
            if (hfdStatOrg.Value == "true")
            {
                objOrgM.ActiveStatus = true;
            }
            else
            {
                objOrgM.ActiveStatus = false;
            }
            //Added by Rishabh 11-10-2021
            if (hdnFlagOrg.Value == "true")
            {
                objOrgM.LogoFlag = true;
            }
            else
            {
                objOrgM.LogoFlag = false;
            }
            if (fuCollegeLogoOrg.HasFile)
            {
                //objOrgM.Logo = objCommon.GetImageData(fuCollegeLogo);
                if (fuCollegeLogoOrg != null)
                {
                    string[] validfiles = { "jpg", "jpeg", "png" };
                    string ext1 = System.IO.Path.GetExtension(fuCollegeLogoOrg.PostedFile.FileName);

                    bool isValidFile = false;
                    for (int i = 0; i < validfiles.Length; i++)
                    {
                        if (ext1 == "." + validfiles[i])
                        {
                            isValidFile = true;
                            //break;
                        }
                    }

                    if (!isValidFile)
                    {
                        objCommon.DisplayMessage(updProg, "Upload Logo only with following formats: .jpg, .png, .jpeg", this);
                        return;
                    }

                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuCollegeLogoOrg.PostedFile.InputStream);

                    decimal size = Math.Round(((decimal)fuCollegeLogoOrg.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 250)
                    {
                        Response.Write("<script>alert('Photo Size Must Not Exceed 250 KB')</script>");
                        objOrgM.Logo = null;
                        return;
                    }
                    else
                    {
                        using (BinaryReader br = new BinaryReader(fuCollegeLogoOrg.PostedFile.InputStream))
                        {
                            ImageLogoByte = fuCollegeLogoOrg.FileBytes;
                            objOrgM.Logo = ImageLogoByte;
                        }
                    }
                }
                hdnLogoOrg.Value = "0";
            }
            else
            {
                objOrgM.Logo = null;
                if (hdnLogoOrg.Value == "1")
                {

                }
                else
                {
                }

            }
            //end

            if (ViewState["actionorg"] != null)//&& Session["action"].ToString().Equals("edit"))
            {
                if (ViewState["actionorg"].ToString().Equals("edit"))
                {
                    objOrgM.OrganizationId = Convert.ToInt32(ViewState["id"]);
                }
                CustomStatus cs = (CustomStatus)objOrg.SaveUpdOrganizationDetails(objOrgM, Convert.ToInt32(hdnLogoOrg.Value));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControlsOrg();
                    BindListViewOrg();
                    objCommon.DisplayMessage(this.updOrg, "Record Updated Successfully.", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ClearControlsOrg();
                    BindListViewOrg();
                    objCommon.DisplayMessage(this.updOrg, "Record Added Successfully.", this.Page);
                }
                else
                {
                    //msgLbl.Text = "Record already exist";
                    objCommon.DisplayMessage(this.updOrg, "Record already exist", this.Page);
                    ViewState["PHOTOORG"] = null;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnSubmitOrg_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnCancelOrg_Click(object sender, EventArgs e)
    {
        ClearControlsOrg();
        //Refresh Page url
        //Response.Redirect(Request.Url.ToString());
    }
    protected void btnEditOrg_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["id"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionorg"] = "edit";
            this.ShowDetailsOrg(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Button Click Events

    #region Methods

    private void ClearControlsOrg()
    {
        ddlInstTypeIdOrg.SelectedIndex = 0;
        ddlOwnershipStatusIdOrg.SelectedIndex = 0;
        txtorgName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtemailadd.Text = string.Empty;
        txtwebOrg.Text = string.Empty;
        txtContactName.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtContactDesign.Text = string.Empty;
        txtContactEmail.Text = string.Empty;
        txtEstdate.Text = string.Empty;
        txtMISOrderDate.Text = string.Empty;
        ViewState["actionorg"] = "add";
        ViewState["id"] = null;
        ViewState["PHOTOORG"] = null;
        imgCollegeLogoOrg.ImageUrl = "~/Images/nophoto.jpg"; //Added By Rishabh on 11/11/2021
        objOrgM.Logo = null;
    }
    private void BindListViewOrg()
    {
        try
        {
            DataSet ds = objOrg.GetOrganizationById(Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvSessionOrg.DataSource = ds;
                lvSessionOrg.DataBind();
            }
            else
            {
                pnlSession.Visible = false;
                lvSessionOrg.DataSource = null;
                lvSessionOrg.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.BindListViewOrg-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void ShowDetailsOrg(int id)
    {
        byte[] imgData = null;
        try
        {
            DataSet ds = null;
            ds = objOrg.GetOrganizationById(id);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["AffilationTypeId"] = id.ToString();

                    txtorgName.Text = ds.Tables[0].Rows[0]["OrgName"] == null ? string.Empty : ds.Tables[0].Rows[0]["OrgName"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"] == null ? string.Empty : ds.Tables[0].Rows[0]["Address"].ToString();
                    txtemailadd.Text = ds.Tables[0].Rows[0]["Email"] == null ? string.Empty : ds.Tables[0].Rows[0]["Email"].ToString();
                    txtwebOrg.Text = ds.Tables[0].Rows[0]["Website"] == null ? string.Empty : ds.Tables[0].Rows[0]["Website"].ToString();
                    txtContactName.Text = ds.Tables[0].Rows[0]["ContactName"] == null ? string.Empty : ds.Tables[0].Rows[0]["ContactName"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"] == null ? string.Empty : ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtContactDesign.Text = ds.Tables[0].Rows[0]["ContactDesignation"] == null ? string.Empty : ds.Tables[0].Rows[0]["ContactDesignation"].ToString();
                    txtContactEmail.Text = ds.Tables[0].Rows[0]["ContactEmail"] == null ? string.Empty : ds.Tables[0].Rows[0]["ContactEmail"].ToString();
                    txtEstdate.Text = ds.Tables[0].Rows[0]["EstabishmentDate"] == null ? string.Empty : ds.Tables[0].Rows[0]["EstabishmentDate"].ToString();

                    if (ddlInstTypeIdOrg.Items.Contains(ddlInstTypeIdOrg.Items.FindByValue(ds.Tables[0].Rows[0]["InstitutionTypeId"].ToString())))
                    {
                        ddlInstTypeIdOrg.SelectedValue = ds.Tables[0].Rows[0]["InstitutionTypeId"] == null ? string.Empty : ds.Tables[0].Rows[0]["InstitutionTypeId"].ToString();
                    }

                    if (ddlOwnershipStatusIdOrg.Items.Contains(ddlOwnershipStatusIdOrg.Items.FindByValue(ds.Tables[0].Rows[0]["OwnershipStatusId"].ToString())))
                    {
                        ddlOwnershipStatusIdOrg.SelectedValue = ds.Tables[0].Rows[0]["OwnershipStatusId"] == null ? string.Empty : ds.Tables[0].Rows[0]["OwnershipStatusId"].ToString();
                    }

                    txtMISOrderDate.Text = ds.Tables[0].Rows[0]["MISOrderDate"] == null ? string.Empty : ds.Tables[0].Rows[0]["MISOrderDate"].ToString();
                    //imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=" + id + "&type=ORG_LOGO";
                    if (ds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
                    {
                        imgData = ds.Tables[0].Rows[0]["Logo"] as byte[];

                        imgCollegeLogoOrg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                        // imgPhotoNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                        //imgPhoto.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                        //imgPhotoNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                        ViewState["PHOTOORG"] = ds.Tables[0].Rows[0]["Logo"];
                        hdnLogoOrg.Value = "1";
                    }
                    else
                    {
                        hdnLogoOrg.Value = "0";
                    }

                    if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatOrg(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatOrg(false);", true);
                    }
                    if (ds.Tables[0].Rows[0]["Logo_On_Report"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Src", "SetFlagOrg(true)", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Src", "SetFlagOrg(false)", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.ShowDetailsOrg-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void BindCollegeListView()
    {
        try
        {
            DataSet ds = objOrg.GetOrganizationById(Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvSessionOrg.DataSource = ds;
                lvSessionOrg.DataBind();
            }
            else
            {
                pnlSession.Visible = false;
                lvSessionOrg.DataSource = null;
                lvSessionOrg.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.BindListViewOrg-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion Methods

    #endregion org_master

    #region college_master
    #region Methods
    private void ClearControlsColg()
    {
        ddlUniversityColg.SelectedIndex = 0;
        ddlStateColg.SelectedIndex = 0;
        ddlInstituteTypeColg.SelectedIndex = 0;
        //txtColgType.Text = string.Empty;
        txtColgNameColg.Text = string.Empty;
        txtCode.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtAddressColg.Text = string.Empty;
        txtLocation.Text = string.Empty;
        Session["COLLEGE_ID"] = null;
        btnImage.ImageUrl = "~/Images/nophoto.jpg"; //Added By Rishabh on 11/11/2021
        ViewState["PHOTO"] = null;
        ddlOrgToMap.SelectedIndex = 0;
        ViewState["COEsign"] = null;
        ImgSign.ImageUrl = "~/Images/default-fileupload.png";
       
    }
    private void BindListViewColg()
    {
        try
        {
            DataSet ds = objController.GetCollegeInfo();

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (DBNull.Value.Equals(ds.Tables[0].Rows[i]["COE_Sign"]))
                {
                    byte[] imgdata = System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Images/default-fileupload.png"));
                    ds.Tables[0].Rows[i]["COE_Sign"] = imgdata;
                }

                if (DBNull.Value.Equals(ds.Tables[0].Rows[i]["Logo"]))
                {
                    byte[] imgdata = System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Images/default-fileupload.png"));
                    ds.Tables[0].Rows[i]["Logo"] = imgdata;
                }
            }
           
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlColgMaster.Visible = true;
                lvColgMaster.DataSource = ds;
                lvColgMaster.DataBind();
            }
            else
            {
                pnlColgMaster.Visible = false;
                lvColgMaster.DataSource = null;
                lvColgMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.BindListViewColg-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    #endregion Methods

    #region Button Click Events
    protected void btnSubmitColg_Click(object sender, EventArgs e)
    {
        byte[] ImageLogoByte;
        try
        {
            if (fuLogoColg.PostedFile.FileName.Equals(string.Empty) && ViewState["PHOTO"] == null)
            {
                objCommon.DisplayMessage(updOrg, "Please Select Logo", this);
                fuLogoColg.Focus();
                return;
            }
            
            objCollege.University = Convert.ToInt32(ddlUniversityColg.SelectedValue);
            objCollege.InstituteType = Convert.ToInt32(ddlInstituteTypeColg.SelectedValue);
            //objCollege.CollegeType = txtColgType.Text.Trim();
            objCollege.Name = txtColgNameColg.Text.Trim();
            objCollege.CollegeCode = txtCode.Text.Trim();
            objCollege.Short_Name = txtShortName.Text.Trim();
            objCollege.Location = txtLocation.Text.Trim();
            objCollege.Address = txtAddressColg.Text.Trim();
            objCollege.State = Convert.ToInt32(ddlStateColg.SelectedValue);
            objCollege.COLLEGE_ID = Convert.ToInt32(Session["COLLEGE_ID"]);
            objCollege.OrgId = Convert.ToInt32(ddlOrgToMap.SelectedValue);
            if (hfdStatColg.Value == "true")
            {
                objCollege.ActiveStatus = true;
            }
            else
            {
                objCollege.ActiveStatus = false;
            }
            #region LogoUpload

            if (fuLogoColg.HasFile)
            {
                if (fuLogoColg != null)
                {
                    string[] validfiles = { "jpg", "jpeg", "png" };
                    string ext1 = System.IO.Path.GetExtension(fuLogoColg.PostedFile.FileName);

                    bool isValidFile = false;
                    for (int i = 0; i < validfiles.Length; i++)
                    {
                        if (ext1 == "." + validfiles[i])
                        {
                            isValidFile = true;
                            //break;
                        }
                    }


                    if (!isValidFile)
                    {
                        objCommon.DisplayMessage(updOrg, "Upload Logo only with following formats: .jpg, .png, .jpeg", this);
                        return;
                    }

                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuLogoColg.PostedFile.InputStream);

                    decimal size = Math.Round(((decimal)fuLogoColg.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 250)
                    {
                        Response.Write("<script>alert('Photo Size Must Not Exceed 250 KB')</script>");
                        return;
                    }
                    else
                    {
                        using (BinaryReader br = new BinaryReader(fuLogoColg.PostedFile.InputStream))
                        {
                            ImageLogoByte = fuLogoColg.FileBytes;
                            objCollege.UploadLogo = ImageLogoByte;
                        }
                    }
                }
                hdnLogoColg.Value = "0";
            }
            else
            {
                hdnLogoColg.Value = "1";
            }
            
            //else
            //{
            //    if (hdnLogoColg.Value == "1")
            //    {

            //    }
            //    else
            //    {
            //    }
                //objCommon.DisplayMessage(this.Page, "Please Select Logo.", this.Page);
                //return;}
            
            #endregion

            byte[] ImageSignByte;
            if (fuCoeSign.HasFile)
            {

                if (fuCoeSign != null)
                {
                    string[] validSign = { "jpg", "jpeg", "png" };
                    string ext1 = System.IO.Path.GetExtension(fuCoeSign.PostedFile.FileName);

                    bool isValidFile = false;
                    for (int i = 0; i < validSign.Length; i++)
                    {
                        if (ext1 == "." + validSign[i])
                        {
                            isValidFile = true;
                            //break;
                        }
                    }


                    if (!isValidFile)
                    {
                        objCommon.DisplayMessage(updOrg, "Upload Signature only with following formats: .jpg, .png, .jpeg", this);
                    }

                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuCoeSign.PostedFile.InputStream);

                    decimal size = Math.Round(((decimal)fuCoeSign.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 250)
                    {
                        Response.Write("<script>alert('Photo Size Must Not Exceed 250 KB')</script>");
                        return;
                    }
                    else
                    {
                        using (BinaryReader br = new BinaryReader(fuCoeSign.PostedFile.InputStream))
                        {
                            ImageSignByte = fuCoeSign.FileBytes;
                            objCollege.UploadSign = ImageSignByte;

                        }
                    }
                }
                //hdnLogoColg.Value = "0";
                hdnCoeSign.Value = "0";
            }
            else
            {
                hdnCoeSign.Value = "1";
            }

            
            if ( hdnCoeSign.Value =="0" && hdnLogoColg.Value == "1")
            {
                hdnLogoColg.Value = "2"; // to update data with COE sign only Logo remains same
            }
            else if (hdnCoeSign.Value == "0" && hdnLogoColg.Value == "0")
            {
                hdnLogoColg.Value = "3"; //to update data with both COE sign and Logo
            }
            //Check for update/insert
            //if (Convert.ToString(ViewState["editColg"]) == "edit")
            //{
            //    CustomStatus cs = (CustomStatus)objController.SaveCollegeInformation(objCollege, Convert.ToInt32(hdnLogoColg.Value));
            //    if (cs.Equals(CustomStatus.RecordUpdated))
            //    {
            //        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
            //    }
            //    BindListViewColg();
            //    ClearControlsColg();
            //}
            //else
            //{
           
            CustomStatus cs = (CustomStatus)objController.SaveCollegeInformation(objCollege, Convert.ToInt32(hdnLogoColg.Value));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Record Submited Successfully.", this.Page);
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
            }

            BindListViewColg();
            ClearControlsColg();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnSubmitColg_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
            BindListViewColg();
            ClearControlsColg();
        }
    }

   

    protected void btnCancelColg_Click(object sender, EventArgs e)
    {
        ClearControlsColg();
        //Refresh Page url
        //Response.Redirect(Request.Url.ToString());
    }
    protected void btnEditColg_Click(object sender, ImageClickEventArgs e)
    {
        byte[] imgData = null;
        byte[] signData = null;
        try
        {
            ImageButton btnEdit = (ImageButton)sender;
            int id = int.Parse(btnEdit.CommandArgument);
            ViewState["editColg"] = "edit";
            Session["COLLEGE_ID"] = id;
            DataSet ds = new DataSet();
            ds = objController.EditCollegeInfo(id);
            ddlUniversityColg.SelectedValue = ds.Tables[0].Rows[0]["UniversityId"].ToString();
            ddlOrgToMap.SelectedValue = ds.Tables[0].Rows[0]["OrganizationId"].ToString();
            //ddlUniversity_SelectedIndexChanged(sender, e);
            ddlUniversityColg.Focus();
            ddlInstituteTypeColg.SelectedValue = ds.Tables[0].Rows[0]["INSTITUTE_TYPE"].ToString();
            //txtColgType.Text = ds.Tables[0].Rows[0]["COLLEGE_TYPE"].ToString();
            txtColgNameColg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            txtCode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
            txtShortName.Text = ds.Tables[0].Rows[0]["SHORT_NAME"].ToString();
            txtLocation.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();
            txtAddressColg.Text = ds.Tables[0].Rows[0]["COLLEGE_ADDRESS"].ToString();
            if (ds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
            {
                imgData = ds.Tables[0].Rows[0]["Logo"] as byte[];

                btnImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                // imgPhotoNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                //imgPhoto.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                //imgPhotoNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                ViewState["PHOTO"] = ds.Tables[0].Rows[0]["Logo"];

                
                hdnLogoColg.Value = "1";
            }
             if (ds.Tables[0].Rows[0]["COE_Sign"] != DBNull.Value)
            {
                signData = ds.Tables[0].Rows[0]["COE_Sign"] as byte[];
                ImgSign.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(signData);
                ViewState["COEsign"] = ds.Tables[0].Rows[0]["COE_Sign"];
                hdnCoeSign.Value = "1";
            }
            else
            {
                hdnLogoColg.Value = "0";
                hdnCoeSign.Value = "0";
            }
            ddlStateColg.SelectedValue = ds.Tables[0].Rows[0]["StateId"].ToString();


            if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatColg(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStatColg(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatColg(false);", true);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnEditColg_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion Button Click Events
    #endregion college_master

    #region org_col_mapping
    #region Button Click Events
    protected void btnSaveOrgColMapping_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string colids = "";
            string colnames = "";
            objOrgMap.OrganizationId = Convert.ToInt32(ddlOrgMap.SelectedValue);
            string orgName = ddlOrgMap.SelectedItem.Text;

            foreach (ListViewDataItem dataitem in lvCollegeMap.Items)
            {
                CheckBox chkBox = (dataitem.FindControl("cbRow")) as CheckBox;
                Label lblColName = (dataitem.FindControl("lblColName")) as Label;
                if (chkBox.Checked)
                {
                    if (colids == "")
                    {
                        colids = chkBox.ToolTip;
                    }
                    else
                    {
                        colids = colids + "," + chkBox.ToolTip;
                    }
                    if (colnames == "")
                    {
                        colnames = orgName + " - " + lblColName.Text;
                    }
                    else
                    {
                        colnames = colnames + "," + orgName + " - " + lblColName.Text;
                    }
                    //objOrgMap.OrganizationName = orgName + " - " + lblColName.Text;
                }
            }
            if (colids != "")
            {
                //CustomStatus cs = (CustomStatus)objCont.SaveOrgColMapping(objOrgMap, colids, colnames);   //Commeted on date 2021 Nov 13 

                //Added on date 2021 Nov 13 
                Dictionary<int, string> RetResult = objCont.SaveOrgColMapping(objOrgMap, colids, colnames);

                int RetValue;
                string RetMessage = "";
                foreach (KeyValuePair<int, string> kvp in RetResult)
                {
                    RetValue = kvp.Key;
                    RetMessage = kvp.Value;
                }
                msg = RetMessage;
                //Added on date 2021 Nov 13 End

                //msg = "Record added sucessfully";  //Commeted on date 2021 Nov 13 

                objOrgMap.CollegeId = 0;
            }
            objCommon.DisplayUserMessage(this.updOrg, msg, this.Page);
            ClearCheckbox();
            ClearControlsMap();
            BindMappingView(0);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnSaveOrgColMapping_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnCancelMap_Click(object sender, EventArgs e)
    {
        ClearCheckbox();
        ClearControlsMap();
    }
    protected void btnEditOrgMap_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearCheckbox();
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ViewState["OrgColid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["actionOrgColid"] = "edit";
            this.ShowDetailsOrgMapping(editno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.btnEditOrgMap_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    #endregion Button Click Events
    #region Methods
    private void BindMappingView(int orgID)
    {
        try
        {
            DataSet ds = objCont.GetMappingData(orgID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvMapping.DataSource = ds;
                lvMapping.DataBind();
            }
            else
            {
                lvMapping.DataSource = null;
                lvMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.BindMappingView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void BindCollegeListViewMap()
    {
        try
        {
            DataSet ds = objCont.GetColList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvCollegeMap.DataSource = ds;
                lvCollegeMap.DataBind();
            }
            else
            {
                lvCollegeMap.DataSource = null;
                lvCollegeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.BindCollegeListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void ShowDetailsOrgMapping(int id)
    {
        try
        {
            DataSet ds = null;
            ds = objCont.GetMappingData(id);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ViewState["AffilationTypeId"] = id.ToString();
                    ddlOrgMap.SelectedValue = ds.Tables[0].Rows[0]["OrganizationId"] == null ? string.Empty : ds.Tables[0].Rows[0]["OrganizationId"].ToString();
                    int i = 0;
                    foreach (ListViewDataItem item in lvCollegeMap.Items)
                    {
                        CheckBox chkBox = (item.FindControl("cbRow")) as CheckBox;
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Rows[j]["CollegeId"].ToString() == chkBox.ToolTip)
                                chkBox.Checked = true;
                            //else
                            //    chkBox.Checked = false;
                        }


                        //if (i < lvCollegeMap.Items.Count )//ds.Tables[0].Rows.Count)
                        //{
                        //    if (i == lvCollegeMap.Items.Count - 1)
                        //    {
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        i++;
                        //    }
                        //}

                    }
                    //if (ds.Tables[0].Rows[0]["ActiveStatus"].ToString() == "Active")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMap(true);", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMap(false);", true);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_OrgStructure.ShowDetailsOrgMapping-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void ClearCheckbox()
    {
        CheckBox headchk = (lvCollegeMap.FindControl("cbHead") as CheckBox);

        if (headchk.Checked)
        {
            headchk.Checked = false;
        }
        foreach (ListViewItem item in lvCollegeMap.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                foreach (Control ctr in item.Controls)
                {
                    var tt = ctr;
                    CheckBox chk = (CheckBox)ctr.FindControl("cbRow");
                    CheckBox chkHead = (CheckBox)ctr.FindControl("cbHead");

                    if (chk.Checked)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
    }
    private void ClearControlsMap()
    {
        ddlOrgMap.SelectedIndex = 0;
    }
    #endregion Methods
    #region Dropdownlist Events
    protected void ddlOrgMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrgMap.SelectedIndex > 0)
        {
            BindMappingView(Convert.ToInt32(ddlOrgMap.SelectedValue));
        }
        else
        {
            BindMappingView(0);
        }
        ClearCheckbox();

        ShowDetailsOrgMapping(Convert.ToInt32(ddlOrgMap.SelectedValue));

        //con = new SqlConnection(cs);
        //SqlCommand cmd = new SqlCommand("select * from ACD_STATE WHERE STATENO=" + 1, con);
        //SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //sda.Fill(dt);
        //con.Open();
        //ddlCity.DataSource = dt;
        //ddlCity.DataTextField = "STATENAME";
        //ddlCity.DataValueField = "STATENO";
        //ddlCity.DataBind();
        //con.Close();
    }
    #endregion Dropdownlist Events
    #endregion org_col_mapping

}