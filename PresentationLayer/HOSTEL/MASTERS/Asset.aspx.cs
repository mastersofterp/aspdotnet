//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ASSET MASTER
// CREATION DATE : 24-NOV-2010
// CREATED BY    : GAURAV SONI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_Masters_Asset : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AssetController assetController = new AssetController();


    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlAssetType, "ACD_HOSTEL_ASSET_TYPE", "ASSET_TYPE_NO", "ASSET_TYPE_NAME", "ASSET_TYPE_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "ASSET_TYPE_NO");
                    this.ShowAllAssets();
                    this.ShowAllAssetType();
                    ViewState["action"] = "add";
                }
            }
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Asset.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Asset.aspx");
        }
    }
    #endregion

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string assetTypeNo = objCommon.LookUp("ACD_HOSTEL_ASSET", "ASSET_TYPE_NO", "ASSET_TYPE_NO = '" + ddlAssetType.SelectedValue + "' AND ASSET_NAME= '" + txtAssetName.Text + "'");
            if (assetTypeNo != null && assetTypeNo != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    private bool CheckDuplicateEntryUpdate(int assetno)
    {
        bool flag = false;
        try
        {
            string assetTypeNo = objCommon.LookUp("ACD_HOSTEL_ASSET", "ASSET_TYPE_NO"," AND ASSET_TYPE_NO = '" + ddlAssetType.SelectedValue + "' AND ASSET_NAME= '" + txtAssetName.Text + "'");
            if (assetTypeNo != null && assetTypeNo != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    #region Actions

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //int organizationid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            int check = 0;
            int assetNo = 0;
            int assetTypeNo = int.Parse(ddlAssetType.SelectedValue);
            string assetName = txtAssetName.Text.Trim();
            int assetQuantity = Convert.ToInt32(txtQuantity.Text.Trim());
            string collegeCode = Session["colcode"].ToString();
            char isavail;
            if (chkAvail.Checked == true)
            {
                isavail = 'Y';
            }
            else { isavail = 'N'; }

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Asset
                if (ViewState["action"].ToString().Equals("add"))
                {
                    check = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_ASSET", "count(*)", " ASSET_NAME= '" + txtAssetName.Text + "'"));
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage(this.updBlock, "Entry for this Selection Already Exist.", this.Page);
                        return;
                    }


                    else if (check != null && check != 0)
                    {
                        objCommon.DisplayMessage(this.updBlock, "Sorry, this Asset Name Already Done in Some one Asset Type.", this);
                        return;
                    }
                    else
                    {
                        cs = (CustomStatus)assetController.AddAsset(isavail, assetTypeNo, assetName, assetQuantity, collegeCode);
                        //cs = (CustomStatus)this.AddAsset(isavail, assetTypeNo, assetName, assetQuantity, collegeCode);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.updBlock, "Asset Saved Successfully.!", this);
                            ShowAllAssets();
                            ClearControlContents();
                        }
                    }
                }

                /// Update Asset
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    assetNo = (GetViewStateItem("AssetNo") != string.Empty ? int.Parse(GetViewStateItem("AssetNo")) : 0);
                    check = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_ASSET", "count(*)", "ASSET_NO !=" + assetNo + " AND ASSET_NAME= '" + txtAssetName.Text + "'"));

                    if (CheckDuplicateEntryUpdate(assetNo) == true)
                    {
                        objCommon.DisplayMessage(this.updBlock, "Entry for this Selection Already Exist.", this.Page);
                        return;
                    }
                    else if (check != null && check != 0)
                    {
                        objCommon.DisplayMessage(this.updBlock, "Sorry, this Asset Name Already Done in Some one Asset Type.", this);
                        return;
                    }
                    else
                    {
                        cs = (CustomStatus)assetController.UpdateAsset(isavail, assetNo, assetTypeNo, assetName, assetQuantity);
                        //cs = (CustomStatus)this.UpdateAsset(isavail, assetNo, assetTypeNo, assetName, assetQuantity);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.updBlock, "Asset Updated Successfully.!", this);
                            this.ClearControlContents();
                        }
                    }
                    //    else
                    //    {
                    //        objCommon.DisplayMessage(updBlock, "Entry for this Selection Already Done.", this.Page);//added By sonali bhor

                    //    }
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    objCommon.DisplayMessage(this.updBlock, "Unable to complete the operation.", this);
                else
                    this.ShowAllAssets();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnAssetSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int organizationid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            int assetTypeNo = 0;
            string assetName = txtAssetType.Text.Trim();
            string assetCode = txtAssetCode.Text;
            string collegeCode = Session["colcode"].ToString();

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Asset
                if (ViewState["action"].ToString().Equals("add"))
                {

                    cs = (CustomStatus)assetController.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);

                    //cs = (CustomStatus)this.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ShowAllAssets();
                        // ShowMessage("Record Saved Successfully!!!");
                        objCommon.DisplayMessage(this.updatePanel2, "Asset Type Saved Successfully.!", this);
                        ShowAllAssetType();
                        ClearControl();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updatePanel2, "Entry for this Selection Already Done.", this.Page);//added By sonali bhor
                        return;
                    }

                }


                /// Update Asset
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    assetTypeNo = Convert.ToInt32(ViewState["ASSETTYPENO"].ToString());
                    cs = (CustomStatus)assetController.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
                    //cs = (CustomStatus)this.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowAllAssets();
                        //ShowMessage("Record Updated Successfully!!!");
                        objCommon.DisplayMessage(this.updatePanel2, "Asset Type Updated Successfully.!", this);
                        ShowAllAssetType();
                        ClearControl();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updatePanel2, "Entry for this Selection Already Done.", this.Page);
                        return;

                    }
                }
            }
            //this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControlContents();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            ImageButton editButton = sender as ImageButton;
            int assetNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = assetController.GetAssetByNo(assetNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                if (ddlAssetType.Items.FindByValue(dr["ASSET_TYPE_NO"].ToString()) != null)
                    ddlAssetType.SelectedValue = dr["ASSET_TYPE_NO"].ToString();

                if (dr["ASSET_NAME"] != null && dr["ASSET_NAME"].ToString() != null)
                    txtAssetName.Text = dr["ASSET_NAME"].ToString();

                if (dr["asset_quantity"] != null && dr["asset_quantity"].ToString() != null)
                    txtQuantity.Text = dr["asset_quantity"].ToString();

                if (dr["IS_AVAILABLE"].ToString() == "Y")  //&& dr["IS_AVAILABLE"].ToString() != null
                {
                    chkAvail.Checked = true;
                }
                else              //else condition added by Saurabh L on 30/09/2022
                {
                    chkAvail.Checked = false; 
                }

                ViewState["action"] = "edit";
                ViewState["AssetNo"] = dr["ASSET_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowAllAssets()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlAssetType, "ACD_HOSTEL_ASSET_TYPE", "ASSET_TYPE_NO", "ASSET_TYPE_NAME", "ASSET_TYPE_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "ASSET_TYPE_NO");
            DataSet ds = assetController.GetAllAssets();
            lvAssets.DataSource = ds;
            lvAssets.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.ShowAllAssets() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void dpAssets_PreRender(object sender, EventArgs e)
    //{
    //    ShowAllAssets();
    //}

    private void ClearControlContents()
    {
        btnSubmit.Text = "Submit";
        txtAssetName.Text = string.Empty;
        ddlAssetType.SelectedIndex = 0;
        txtQuantity.Text = string.Empty;
        chkAvail.Checked = false;
        ViewState["action"] = "add";
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    #endregion

    //protected void btnAssetSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int assetTypeNo = 0;
    //        string assetName = txtAssetType.Text.Trim();
    //        string assetCode = txtAssetCode.Text;
    //        string collegeCode = Session["colcode"].ToString();

    //        /// check form action whether add or update
    //        if (ViewState["action"] != null)
    //        {
    //            CustomStatus cs = new CustomStatus();

    //            /// Add Asset
    //            if (ViewState["action"].ToString().Equals("add"))
    //            {

    //                cs = (CustomStatus)assetController.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);

    //                //cs = (CustomStatus)this.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
    //                if (cs.Equals(CustomStatus.RecordSaved))
    //                {
    //                    ShowAllAssets();
    //                    // ShowMessage("Record Saved Successfully!!!");
    //                    objCommon.DisplayMessage(this.updatePanel2, "Asset Type Saved Successfully.!", this);
    //                    ShowAllAssetType();
    //                    ClearControl();
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updatePanel2, "Entry for this Selection Already Done.", this.Page);//added By sonali bhor
    //                    return;
    //                }

    //            }


    //            /// Update Asset
    //            if (ViewState["action"].ToString().Equals("edit"))
    //            {
    //                assetTypeNo = Convert.ToInt32(ViewState["ASSETTYPENO"].ToString());
    //                cs = (CustomStatus)assetController.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
    //                //cs = (CustomStatus)this.AddUpdateAssetType(assetTypeNo, assetName, assetCode, collegeCode);
    //                if (cs.Equals(CustomStatus.RecordUpdated))
    //                {
    //                    ShowAllAssets();
    //                    //ShowMessage("Record Updated Successfully!!!");
    //                    objCommon.DisplayMessage(this.updatePanel2, "Asset Type Updated Successfully.!", this);
    //                    ShowAllAssetType();
    //                    ClearControl();
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updatePanel2, "Entry for this Selection Already Done.", this.Page);
    //                    return;

    //                }
    //            }
    //        }
    //        //this.ClearControlContents();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}

    private void ClearControl()
    {
        btnAssetSubmit.Text = "Submit";
        ViewState["action"] = "add";
        txtAssetType.Text = string.Empty;
        txtAssetCode.Text = string.Empty;
    }

    protected void btnAssetcancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    protected void btnAssetEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnAssetSubmit.Text = "Update";
            ImageButton editButton = sender as ImageButton;
            int assetTypeNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = assetController.GetAssetTypeByNo(assetTypeNo);
            //DataSet ds = this.GetAssetTypeByNo(assetTypeNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["action"] = "edit";
                DataRow dr = ds.Tables[0].Rows[0];

                //if (ddlAssetType.Items.FindByValue(dr["ASSET_TYPE_NO"].ToString()) != null)
                //    txtAssetName.Text = dr["ASSET_TYPE_NO"].ToString();
                ViewState["ASSETTYPENO"] = ds.Tables[0].Rows[0]["ASSET_TYPE_NO"].ToString();

                if (dr["ASSET_TYPE_NAME"] != null && dr["ASSET_TYPE_NAME"].ToString() != null)
                    txtAssetType.Text = dr["ASSET_TYPE_NAME"].ToString();

                if (dr["asset_type_code"] != null && dr["asset_type_code"].ToString() != null)
                    txtAssetCode.Text = dr["asset_type_code"].ToString();


                ViewState["AssetNo"] = dr["ASSET_TYPE_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowAllAssetType()
    {
        try
        {
            DataSet ds = assetController.GetAllAssetsType();
            //DataSet ds = this.GetAllAssetsType();
            lvItemAssetMaster.DataSource = ds;
            lvItemAssetMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.ShowAllAssets() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // CONTROLLER METHODS CALL. //
    //public int AddAsset(char isavail, int assetTypeNo, string assetName, int assQuan, string collegeCode)
    //{
    //    int status = 0;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] sqlParams = new SqlParameter[]
    //            {
    //                new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
    //                new SqlParameter("@P_ASSET_NAME", assetName),
    //                new SqlParameter("@P_ASSET_QUANTITY", assQuan),
    //                new SqlParameter("@P_COLLEGE_CODE", collegeCode),
    //                 new SqlParameter("@P_ISAVAIL", isavail),
    //                new SqlParameter("@P_ASSET_NO", status)
    //            };
    //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

    //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_INSERT", sqlParams, true);

    //        if (obj != null && obj.ToString() != "-99")
    //            status = Convert.ToInt32(CustomStatus.RecordSaved);
    //        else
    //            status = Convert.ToInt32(CustomStatus.Error);
    //    }
    //    catch (Exception ex)
    //    {
    //        status = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.AddAsset() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //    return status;
    //}

    //public int UpdateAsset(char isavail, int assetNo, int assetTypeNo, string assetName, int assQuan)
    //{
    //    int status;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] sqlParams = new SqlParameter[]
    //            {                    
    //                new SqlParameter("@P_ASSET_NAME", assetName),
    //                new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
    //                new SqlParameter("@P_ASSET_QUANTITY", assQuan),
    //                 new SqlParameter("@P_ISAVAIL", isavail),
    //                new SqlParameter("@P_ASSET_NO", assetNo)
    //            };
    //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

    //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_UPDATE", sqlParams, true);

    //        if (obj != null && obj.ToString() != "-99")
    //            status = Convert.ToInt32(CustomStatus.RecordSaved);
    //        else
    //            status = Convert.ToInt32(CustomStatus.Error);
    //    }
    //    catch (Exception ex)
    //    {
    //        status = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //    return status;
    //}

    //public DataSet GetAssetTypeByNo(int assetTypeNo)
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo) };

    //        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_TYPE_GET_BY_NO", objParams);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.GetAssetByNo() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //    return ds;
    //}

    //public int AddUpdateAssetType(int assetTypeNo, string assetName, string assCode, string collegeCode)
    //{
    //    int status = 0;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] sqlParams = new SqlParameter[]
    //            {
    //                new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
    //                new SqlParameter("@P_ASSET_NAME", assetName),
    //                new SqlParameter("@P_ASSET_CODE", assCode),
    //                new SqlParameter("@P_COLLEGE_CODE", collegeCode),
    //                new SqlParameter("@P_STATUS", status)
    //            };
    //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

    //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_ASSET_TYPE", sqlParams, true);

    //        if (obj != null && obj.ToString() == "1")
    //            status = Convert.ToInt32(CustomStatus.RecordSaved);
    //        else if (obj != null && obj.ToString() == "2")
    //            status = Convert.ToInt32(CustomStatus.RecordUpdated);
    //        else
    //            status = Convert.ToInt32(CustomStatus.Error);
    //    }
    //    catch (Exception ex)
    //    {
    //        status = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.AddAsset() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //    return status;
    //}

    //public DataSet GetAllAssetsType()
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] objParams = new SqlParameter[0];

    //        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_TYPE_GET_ALL", objParams);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.GetAllAssets() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //    return ds;
    //}
}