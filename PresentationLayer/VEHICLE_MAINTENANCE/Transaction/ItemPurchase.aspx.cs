//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 03-OCT-2015
// DESCRIPTION   : USE TO ENTER THE ITEMS(FUEL/INDENTS) WHICH ARE PURCHASED.
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

public partial class VEHICLE_MAINTENANCE_Transaction_ItemPurchase : System.Web.UI.Page
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
                    ViewState["action"] = "add";
                    //BindlistView();                 
                    txtAmt.Attributes.Add("readonly", "true");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    // This method is used to bind the entry list of purchased items.
    private void BindlistView(int ItemType)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ITEM_PURCHASE P INNER JOIN  VEHICLE_ITEMMASTER IM ON (P.ITEM_ID = IM.ITEM_ID)", "P.PURCHASE_ID, P.PURCHASE_DATE, IM.ITEM_NAME", " P.QUANTITY, P.RATE,CAST(P.TOTAL_AMT as DECIMAL(15,2)) TOTAL_AMT, P.ITEM_TYPE", "P.ITEM_TYPE=" + ddlItemType.SelectedValue, "P.PURCHASE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvPurItem.DataSource = ds;
                lvPurItem.DataBind();
            }
            else
            {
                lvPurItem.DataSource = null;
                lvPurItem.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to show details fetch from DataBase.
    private void ShowDetails(int purchase_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ITEM_PURCHASE", "*,CAST(TOTAL_AMT as DECIMAL(15,2)) as t_amt", "", "PURCHASE_ID=" + purchase_id, "PURCHASE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemType.SelectedValue = ds.Tables[0].Rows[0]["ITEM_TYPE"].ToString();
                objCommon.FillDropDownList(ddlItemName, "VEHICLE_ITEMMASTER", "ITEM_ID", "ITEM_NAME", "ITEM_TYPE=" + Convert.ToInt32(ddlItemType.SelectedValue), "ITEM_ID");
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["ITEM_ID"].ToString();
                txtPDate.Text = ds.Tables[0].Rows[0]["PURCHASE_DATE"].ToString();
                txtQuantity.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();
                txtAmt.Text = ds.Tables[0].Rows[0]["t_amt"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["RATE"].ToString();
                ddlVendorName.SelectedValue = ds.Tables[0].Rows[0]["FUEL_SUPPILER_ID"].ToString();
                txtCRN.Text = ds.Tables[0].Rows[0]["CRN"].ToString();
                ddlPurchaseFor.SelectedValue = ds.Tables[0].Rows[0]["PURCHASE_FOR"].ToString();
                txtPurchaseCouponNumber.Text = ds.Tables[0].Rows[0]["PURCHASE_COUPON_NUMBER"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controls.
    private void Clear()
    {
        //ddlItemType.SelectedIndex = 0;
        ddlItemName.SelectedIndex = 0;
        txtPDate.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        ViewState["action"] = "add";
        txtAmt.Text = string.Empty;
        ViewState["purchase_id"] = null;
        txtRate.Text = string.Empty;
        lblUnit.Text = string.Empty;
        //-----start----28-03-2023----
       
        divvendor.Visible = false;
        divcrn.Visible = false;
        divpurchasefor.Visible = false;
        divPurchasecoupnnumber.Visible = false;

        txtCRN.Text = string.Empty;
        txtPurchaseCouponNumber.Text = string.Empty;
        ddlVendorName.SelectedValue = "0";
        ddlPurchaseFor.SelectedValue = "0";

        lvPurItem.DataSource = null;
        lvPurItem.DataBind();
        pnlList.Visible = false;
        //------end-----28-03-2023

    }
    // This method is used to generate the report for item list.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            int itemType = 0;
            if (RdoItemType.SelectedValue == "Fuel")
            {
                itemType = 1;
            }
            else if (RdoItemType.SelectedValue == "Indent")
            {
                itemType = 2;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ItemPurchaseDetailReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            if (itemType == 1)
            {
                url += "&param=@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else
            {
                url += "&param=@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text + ",@P_ITEM_TYPE=" + itemType;
            }

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
            DateTime currenttime = Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"));
            if (Convert.ToDateTime(txtPDate.Text) > currenttime)
            {
                objCommon.DisplayMessage(updActivity, "Future Date Is Not Allowed..", this.Page);
                return;

            }

            objVM.ITEM_TYPE = Convert.ToInt32(ddlItemType.SelectedValue);
            objVM.ITEM_ID = Convert.ToInt32(ddlItemName.SelectedValue);
            objVM.PURCHASE_DATE = Convert.ToDateTime(txtPDate.Text.Trim());
            objVM.QUANTITY = Convert.ToDecimal(txtQuantity.Text.Trim());
            objVM.TOTAL_AMT = Convert.ToDecimal(txtAmt.Text.Trim());
            objVM.RATE = Convert.ToDecimal(txtRate.Text);
            objVM.FUEL_SUPPILER_ID = Convert.ToInt32(ddlVendorName.SelectedValue);
            objVM.CRN = txtCRN.Text;
            objVM.PURCHASE_FOR =Convert.ToInt32( ddlPurchaseFor.SelectedValue);
            objVM.PURCHASE_COUPON_NUMBER = txtPurchaseCouponNumber.Text;
            objVM.USERNO = Convert.ToInt32(Session["userno"]); 
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateItemsPurchase(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        ddlItemType.SelectedIndex = 0;
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView(Convert.ToInt32(ddlItemType.SelectedValue));
                        Clear();
                        ddlItemType.SelectedIndex = 0;
                        objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["purchase_id"] != null)
                    {
                        objVM.PURCHASE_ID = Convert.ToInt32(ViewState["purchase_id"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateItemsPurchase(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                            ddlItemType.SelectedIndex = 0;
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView(Convert.ToInt32(ddlItemType.SelectedValue));
                            Clear();
                            ddlItemType.SelectedIndex = 0;
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedIndex = 0;
        Clear();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int purchase_id = int.Parse(btnEdit.CommandArgument);
            ViewState["purchase_id"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(purchase_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int itemType = 0;
       // ShowReport("Item Purchase Details", "rptItemPurchaseDetails.rpt");
        if (RdoItemType.SelectedValue == "Fuel")
        {
            itemType = 1;
        }
        else if (RdoItemType.SelectedValue == "Indent")
        {
            itemType = 2;
        }

        if (RdoItemType.SelectedValue == "Fuel")
        {
            if (rdoReportType.SelectedValue == "pdf")
            {
                ShowReport("Item Purchase Details", "rptItemPurchaseReport.rpt");
            }
            else if (rdoReportType.SelectedValue == "xls")
            {
                DataSet ds = objVMC.GetItemPurchaseReportForExcel(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(itemType));
                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold;  }</style>";
                string attachment = "attachment; filename=Purchase_Entry_Report_Template.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        else if (RdoItemType.SelectedValue == "Indent")
        {
            if (rdoReportType.SelectedValue == "pdf")
            {
                ShowReport("Item Purchase Details", "rptItemPurchaseDetails.rpt");
            }
            else if (rdoReportType.SelectedValue == "xls")
            {
                DataSet ds = objVMC.GetItemPurchaseReportForExcel(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(itemType));
                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold;  }</style>";
                string attachment = "attachment; filename=STUDENT_PLACEMENT_DATA.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }
    #endregion
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtRate.Text = string.Empty;
            lblUnit.Text = "";
            if (ddlItemType.SelectedIndex == 1)
            {
                Clear();
                objCommon.FillDropDownList(ddlItemName, "VEHICLE_ITEMMASTER", "ITEM_ID", "ITEM_NAME", "ITEM_TYPE=" + Convert.ToInt32(ddlItemType.SelectedValue), "ITEM_ID");
                objCommon.FillDropDownList(ddlVendorName, "VEHICLE_FUEL_SUPPILER_MASTER", "FUEL_SUPPILER_ID", "FUEL_SUPPILER_NAME", "", "FUEL_SUPPILER_ID");
                divvendor.Visible=true;
                divcrn.Visible=true;
                divpurchasefor.Visible=true;
                divPurchasecoupnnumber.Visible = true;

            }
            else if (ddlItemType.SelectedIndex == 2)
            {
                Clear();
                  objCommon.FillDropDownList(ddlItemName, "VEHICLE_ITEMMASTER", "ITEM_ID", "ITEM_NAME", "ITEM_TYPE=" + Convert.ToInt32(ddlItemType.SelectedValue), "ITEM_ID");

                  divvendor.Visible = false;
                  divcrn.Visible = false;
                  divpurchasefor.Visible = false;
                  divPurchasecoupnnumber.Visible = false;
            }
            else
            {
                Clear();

                divvendor.Visible = false;
                divcrn.Visible = false;
                divpurchasefor.Visible = false;
                divPurchasecoupnnumber.Visible = false;
                objCommon.DisplayMessage(this.updActivity, "Please Select Item Type.", this.Page);
                return;
            }
            pnlList.Visible = true;
            BindlistView(Convert.ToInt32(ddlItemType.SelectedValue));
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.ddlItemType_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                hdnRate.Value = "0";
                txtRate.Text = string.Empty;
                lblUnit.Text = "";
                DataSet ds = null;
                ds = objCommon.FillDropDown("VEHICLE_ITEMMASTER IM INNER JOIN VEHICLE_UNIT U ON (IM.UNIT = U.UNIT_ID) ", "RATE", "U.UNIT_NAME", "ITEM_ID=" + Convert.ToInt32(ddlItemName.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnRate.Value = ds.Tables[0].Rows[0]["RATE"].ToString();
                    txtRate.Text = hdnRate.Value;
                    lblUnit.Text = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                }
                else
                {
                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Please Define Item Name.", this.Page);
                    return;
                }
            }
            else
            {
                //Clear();
                objCommon.DisplayMessage(this.updActivity, "Please Select Item Name.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_ItemPurchase.ddlItemName_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnreport_show_Click(object sender, EventArgs e)
    {
        btnReport.Visible = true;
        divfrom.Visible = true;
        divTo.Visible = true;
        btnback.Visible = true;
        divreportin.Visible = true;
        btnSubmit.Visible = false;
        btnreport_show.Visible = false;
        btnCancel.Visible = false;
        //ddlItemType.Visible = false;
        //ddlItemName.Visible = false;
        //txtPDate.Visible = false;
        //txtQuantity.Visible = false;
        //txtRate.Visible = false;
        //txtAmt.Visible = false;
        divitemtype.Visible = false;
        divitemname.Visible = false;
        divpurchasedate.Visible = false;
        divquantitypur.Visible = false;
        divrate.Visible = false;
        divtotalamt.Visible = false;
        divrdoitemtype.Visible = true;
        pnlList.Visible = false;
        divvendor.Visible = false;
        divcrn.Visible = false;
        divpurchasefor.Visible = false;
        divPurchasecoupnnumber.Visible = false;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        btnReport.Visible = false;
        divfrom.Visible = false;
        divTo.Visible = false;
        btnback.Visible = false;

        divitemtype.Visible = true;
        divitemname.Visible = true;
        divpurchasedate.Visible = true;
        divquantitypur.Visible = true;
        divrate.Visible = true;
        divtotalamt.Visible = true;

        btnSubmit.Visible = true;
        btnreport_show.Visible = true;
        btnCancel.Visible = true;
        divreportin.Visible = false;
        divrdoitemtype.Visible = false;
        ddlItemType.SelectedValue = "0";
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        rdoReportType.SelectedValue = "pdf";
        RdoItemType.SelectedValue = "Fuel";
    }
}