//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 02-OCT-2015
// DESCRIPTION   : USE TO CREATE DIFFERENT ITEM NAMES.
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Master_ItemMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    #region Page Load
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlunit, "VEHICLE_UNIT", "UNIT_ID", "UNIT_NAME", "ACTIVESTATUS=1", "UNIT_ID");     
                    ViewState["action"] = "add";
                    BindlistView();
                   // txtAmt.Attributes.Add("readonly", "true");
                }               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
    #region User Defined Methods
    // This method is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    // This method is used to bind the entry list of items.
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ITEMMASTER IM INNER JOIN VEHICLE_UNIT U ON (IM.UNIT = U.UNIT_ID)", "IM.ITEM_ID, IM.ITEM_NAME, IM.RATE", " IM.ITEM_TYPE, U.UNIT_NAME", "", "ITEM_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvItem.DataSource = ds;
                lvItem.DataBind();
            }
            else
            {
                lvItem.DataSource = null;
                lvItem.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to show details fetch from DataBase.
    private void ShowDetails(int item_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ITEMMASTER", "*", "", "ITEM_ID=" + item_id, "ITEM_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemName.Text = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                ddlunit.SelectedValue = ds.Tables[0].Rows[0]["UNIT"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["RATE"].ToString();
              //  txtQuantity.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();
                ddlItemType.SelectedValue = ds.Tables[0].Rows[0]["ITEM_TYPE"].ToString();               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controls.
    private void Clear()
    {
        txtItemName.Text = string.Empty;
        ddlunit.SelectedIndex = 0;
        txtRate.Text = string.Empty;
      //  txtQuantity.Text = string.Empty;
        ddlItemType.SelectedIndex = 0;
        ViewState["action"] = "add";
      //  txtAmt.Text = string.Empty;
        ViewState["item_id"] = null;

    }
    // This method is used to generate the report for item list.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
           // string Script = string.Empty;
           // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));
           // url += "Reports/CommonReport.aspx?";
           // url += "exporttype=" + exporttype;
           // url += "&filename=RCValidityExpiry" + ".pdf";
           // url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

           //// url += "&param=@P_EXPIRY_FOR=DRIVING_LICENCE,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value + ",@P_VEHICLE_TYPE=1";

           // // To open new window from Updatepanel
           // System.Text.StringBuilder sb = new System.Text.StringBuilder();
           // string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
           // sb.Append(@"window.open('" + url + "','','" + features + "');");
           // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Form Actions
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.ITEM_NAME = txtItemName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtItemName.Text.Trim());
            objVM.UNIT = Convert.ToInt32(ddlunit.SelectedValue);
            objVM.RATE = Convert.ToDecimal(txtRate.Text.Trim());
         //   objVM.QUANTITY = Convert.ToInt32(txtQuantity.Text.Trim());
            objVM.ITEM_TYPE = Convert.ToInt32(ddlItemType.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateItemMaster(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                       // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView();                      
                        Clear();
                     //   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Save Successfully.');", true);
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["item_id"] != null)
                    {
                        objVM.ITEM_ID = Convert.ToInt32(ViewState["item_id"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateItemMaster(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                           // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            Clear();
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                           objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                          
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int item_id = int.Parse(btnEdit.CommandArgument);
            ViewState["item_id"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(item_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowItemDetails("Item Details", "rptItemDetails.rpt");
    }

    private void ShowItemDetails(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ItemDetailReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ITEM_TYPE=" + ddlItemType.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_ItemMaster.ShowItemDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }      






    #endregion
}