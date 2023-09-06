using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class STORES_Transactions_Str_CondemnedItemSaleApproval : System.Web.UI.Page
{
    Common ObjComman = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Str_JvStockCon objJvStkCon = new Str_JvStockCon();
    Str_JvStockEnt objJvStockEnt = new Str_JvStockEnt();
    Str_VendorPaymentCon objVpCon = new Str_VendorPaymentCon();
    DataTable ItemRTbl = null;
    DataRow dtRow = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                }
                //if (rblAssestType.SelectedValue == "1")
                //{
                //    divFromDate.Visible = true;
                //    divToDate.Visible = true;
                //    divToFields.Visible = false;
                //}
               
                ObjComman.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO = 1", "MIGNAME");
                ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
                ObjComman.FillDropDownList(ddlVendorName, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
                ViewState["Action"] = "Add";
                //txtVPDate.Text = DateTime.Now.ToString();
                BindListViewCond_SALEDetail();
            }
            btnApprove.Enabled = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Transactions_Str_CondemnItemSale.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewCond_SALEDetail()
    {
        DataSet ds = objJvStkCon.GetCONDEMNED_SALEDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvCondemnedSaleEntry.DataSource = ds.Tables[0];
            lvCondemnedSaleEntry.DataBind();
            lvCondemnedSaleEntry.Visible = true;
        }
        else
        {
            lvCondemnedSaleEntry.DataSource = null;
            lvCondemnedSaleEntry.DataBind();
            lvCondemnedSaleEntry.Visible = false;
        }
    }

    private void BindListViewItemList()
    {
        try
        {
            //DataSet ds = ObjComman.FillDropDown("STORE_INVOICE_DSR_ITEM", "INVDINO as DSR_ID", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DEPR_CAL_START_DATE IS NOT NULL", "");  
            DataSet ds = ObjComman.FillDropDown("STORE_INVOICE_DSR_ITEM", "INVDINO as DSR_ID", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DSR_NUMBER IS NOT NULL", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvShowItem.DataSource = ds.Tables[0];
                lvShowItem.DataBind();
                divShowItem.Visible = true;
                divAddItem.Visible = true;

            }
            else
            {
                lvShowItem.DataSource = null;
                lvShowItem.DataBind();
                divShowItem.Visible = false;
                divAddItem.Visible = false;
                MessageBox("No Record Found");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Str_DepriciationCalculation.BindListViewItemList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void BindListViewCondemnItemSale(string ItemNo)
    //{
    //    try
    //    {
    //        DataSet ds = objJvStkCon.GetCondemnItemSale(ItemNo);
    //        lvItemSale.DataSource = ds;
    //        lvItemSale.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Str_CondemnItemSale.BindListViewCondemnItemSale-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}



    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewItemList();

    }

    protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
    }
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //    string ItemNo = string.Empty;
    //    foreach (ListViewItem lv in lvShowItem.Items)
    //    {
    //        CheckBox chkInvdiNo = lv.FindControl("chkInvdiNo") as CheckBox;
    //        HiddenField lblDSRNumber = lv.FindControl("lblDSRNumber") as HiddenField;
    //        if (chkInvdiNo.Checked)
    //        {
    //            string invidno = lblDSRNumber.Value;
    //            ItemNo += invidno + ',';
    //        }
    //    }
    //    if (ItemNo != string.Empty)
    //        ItemNo = ItemNo.Substring(0, ItemNo.Length - 1);
    //    //objJVEnt.INVIDNO = ItemNo;

    //    DataSet ds = objJvStkCon.GetCondemnItemSale(ItemNo, txtToDate.Text);
    //    lvItemSale.DataSource = ds;
    //    lvItemSale.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Str_CondemnItemSale.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string ItemNo = string.Empty;
            string TotDsr = string.Empty;
            foreach (ListViewItem lv in lvShowItem.Items)
            {
                CheckBox chkInvdiNo = lv.FindControl("chkInvdiNo") as CheckBox;
                Label lblDSRNumber = lv.FindControl("lblDSRNumber") as Label;
                if (chkInvdiNo.Checked)
                {
                    string invidno = lblDSRNumber.Text;
                    ItemNo += invidno + ',';
                }
            }
            if (ItemNo != string.Empty)
                ItemNo = ItemNo.Substring(0, ItemNo.Length - 1);

            string DsrNumber = string.Empty;
            foreach (ListViewItem lv in lvItemSale.Items)
            {
                Label lblDSRNumber = lv.FindControl("lblDSR_NUMBER") as Label;

                string dsrnumber = lblDSRNumber.Text;
                DsrNumber += dsrnumber + ',';

            }
            if (DsrNumber != string.Empty)
                DsrNumber = DsrNumber.Substring(0, DsrNumber.Length - 1);

            TotDsr = ItemNo + ',' + DsrNumber;
            //objJVEnt.INVIDNO = ItemNo;



            DataSet ds = objJvStkCon.GetCondemnItemSale(TotDsr, txtToDate.Text);
            if (ds.Tables[0].Rows.Count > 0 || ds != null)
            {
                lvItemSale.DataSource = ds;
                lvItemSale.DataBind();
                //btnSave.Visible = true;
                lvItemSale.Visible = true;
            }
            else
            {
                lvItemSale.DataSource = null;
                lvItemSale.DataBind();
                //btnSave.Visible = false;
                lvItemSale.Visible = false;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "Str_CondemnItemSale.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);

            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        objJvStockEnt.ITEMNO = Convert.ToInt32(ddlItem.SelectedValue);
        objJvStockEnt.PNO = Convert.ToInt32(ddlVendorName.SelectedValue);
     //   objJvStockEnt.DSR_ID = Convert.ToInt32(ddlSubCategory.SelectedValue);
        objJvStockEnt.SALE_DATE = Convert.ToDateTime(txtToDate.Text);

        objJvStockEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objJvStockEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);

        objJvStockEnt.SALE_SAVE_TABLE = this.AddCondemnedSaleTable();

        if (ViewState["Action"].ToString() == "Add")
        {
            objJvStockEnt.COND_SALE_TRNO = GenerateCondSaleTRNO();
            objJvStockEnt.CIS_ID = 0;
            CustomStatus cs = (CustomStatus)objJvStkCon.InsUpdateCondemnedSaleItem(objJvStockEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved  Successfully.");
                
            }
            else
            {
                MessageBox("Save Failed.");
            }
        }
        else
        {

            objJvStockEnt.CIS_ID = Convert.ToInt32(ViewState["CIS_ID"]);
            CustomStatus cs = (CustomStatus)objJvStkCon.InsUpdateCondemnedSaleItem(objJvStockEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Updated Successfully.");
                
            }
            else
            {
                MessageBox("Save Failed.");
            }
        }
       
        divTRNumber.Visible = true;
    }

    private string GenerateCondSaleTRNO()
    {
        DataSet ds = objJvStkCon.GetCondSaleTRNumber();
        txtCOND_SALE_TRNO.Text = ds.Tables[0].Rows[0]["COND_SALE_TRNO"].ToString();
        return txtCOND_SALE_TRNO.Text;
    }



    private DataTable AddCondemnedSaleTable()
    {
        DataTable dtSav = this.CreateCondemnedSaleTable();
        int MaxSrnoVal = 0;
        foreach (ListViewItem lv in lvItemSale.Items)
        {
            MaxSrnoVal++;
            Label lblDSR_NUMBER = lv.FindControl("lblDSR_NUMBER") as Label;
            HiddenField hdnDSR_ID = lv.FindControl("hdnDSR_ID") as HiddenField;
            Label lblSTOCK_VALUE = lv.FindControl("lblSTOCK_VALUE") as Label;
            Label lblDEPRECIATED_AMOUNT = lv.FindControl("lblDEPRECIATED_AMOUNT") as Label;
            Label lblVALUE = lv.FindControl("lblVALUE") as Label;

            TextBox lblSaleValue = lv.FindControl("lblSaleValue") as TextBox;            
            Label lblProfitLoss = lv.FindControl("lblProfitLoss") as Label;
            Label lblStatus = lv.FindControl("lblStatus") as Label;
            HiddenField hdnProfitLoss = lv.FindControl("hdnProfitLoss") as HiddenField;
            TextBox lblREMARK = lv.FindControl("lblREMARK") as TextBox;


            DataRow dr = null;
            dr = dtSav.NewRow();

            dr["DSR_NUMBER"] = lblDSR_NUMBER.Text;
            dr["DSR_ID"] = Convert.ToInt32(hdnDSR_ID.Value);
            dr["STOCK_VALUE"] = Convert.ToDouble(lblSTOCK_VALUE.Text);

            dr["DEPRECIATED_AMOUNT"] = Convert.ToDouble(lblDEPRECIATED_AMOUNT.Text);
            //dr["DEPR_TO_DATE"] = Convert.ToDateTime(lblDEPR_TO_DATE.Text);
            dr["VALUE"] = Convert.ToDouble(lblVALUE.Text);
            dr["SALE_VALUE"] = Convert.ToDouble(lblSaleValue.Text);
            dr["PROFLOSS"] = Convert.ToDouble(hdnProfitLoss.Value);
            dr["COND_STATUS"] = lblStatus.Text;
            dr["REMARK"] = lblREMARK.Text;




            dtSav.Rows.Add(dr);
        }
        return dtSav;
    }

    //private DataTable CreateSaveTable()
    //{
    //    throw new NotImplementedException();
    //}

    private DataTable CreateCondemnedSaleTable()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("DSR_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("STOCK_VALUE", typeof(double)));
        dt.Columns.Add(new DataColumn("DEPRECIATED_AMOUNT", typeof(double)));
        //dt.Columns.Add(new DataColumn("DEPR_TO_DATE", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("VALUE", typeof(double)));
        dt.Columns.Add(new DataColumn("SALE_VALUE", typeof(double)));
        dt.Columns.Add(new DataColumn("PROFLOSS", typeof(double)));
        dt.Columns.Add(new DataColumn("COND_STATUS", typeof(string)));
        dt.Columns.Add(new DataColumn("REMARK", typeof(string)));

        return dt;
    }



    protected void lblSaleValue_TextChanged(object sender, EventArgs e)
    {

        //Label lblProfitLoss = lvItemSale.FindControl("lblSaleValue") as Label - lvItemSale.FindControl("lblVALUE") as Label;

        //if (lblSaleValue > lblVALUE)
        //{
        //    lblREMARK = "PROFIT";

        //}
        //else
        //{
        //    lblREMARK = "LOSS";
        //}


    }
    private void ClearAll()
    {
        ddlSubCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlVendorName.SelectedIndex = 0;
        txtCOND_SALE_TRNO.Text = string.Empty;
        txtToDate.Text = "";
        lvShowItem.DataSource = null;
        lvShowItem.DataBind();
        divShowItem.Visible = false;

        lvItemSale.DataSource = null;
        lvItemSale.DataBind();
        lvItemSale.Visible = false;


        divAddItem.Visible = false;
        divTRNumber.Visible = false;
       // rdlApprove.SelectedItem.cle
        rdlApprove.Items[0].Selected = true;
             rdlApprove.Items[1].Selected = false;
                                                 

        ViewState["Action"] = "Add";
    }
    protected void btnAddN_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = true;
        lvCondemnedSaleEntry.Visible = false;
  
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = false;
        lvCondemnedSaleEntry.Visible = true;
        pnlApprove.Visible = false;
        BindListViewCond_SALEDetail();
        pnlApprove.Visible = false;

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = true;
        lvCondemnedSaleEntry.Visible = false;
       
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //ClearAll();
        Button btnEdit = sender as Button;
        int CIS_ID = Convert.ToInt32(btnEdit.CommandArgument);
        ViewState["CIS_ID"] = CIS_ID;
        ViewState["Action"] = "Edit";

        DataSet dsEdit = objJvStkCon.GetCondSaleDetailsById(CIS_ID);

        txtCOND_SALE_TRNO.Text = dsEdit.Tables[0].Rows[0]["COND_SALE_TRNO"].ToString();
        divTRNumber.Visible = true;
        txtToDate.Text = dsEdit.Tables[0].Rows[0]["SALE_DATE"].ToString();
        ddlVendorName.SelectedValue = dsEdit.Tables[0].Rows[0]["PNO"].ToString();
       // ddlSubCategory.SelectedValue=dsEdit.Tables[0].Rows[0]["DSR_ID"].ToString();
       // ddlItem.SelectedValue = dsEdit.Tables[0].Rows[0]["ITEM_NO"].ToString();
        if (dsEdit.Tables[1].Rows.Count > 0)
        {
            lvItemSale.DataSource = dsEdit.Tables[1];
            lvItemSale.DataBind();
            lvItemSale.Visible = true;
            pnlApprove.Visible = true;
            //hdnRowCount.Value = dsEdit.Tables[1].Rows.Count.ToString();
            //divList.Visible = true;
            ddlCategory.Visible = false;
            ddlSubCategory.Visible = false;
            ddlItem.Visible = false;
            updpnlMain.Visible = true;
            pnlApprove.Visible = true;
        }
        
        lvCondemnedSaleEntry.Visible = false;

        pnlDSRDetails.Visible = true;
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        CustomStatus cs = (CustomStatus)objJvStkCon.UpdateStatus(Convert.ToInt32(ViewState["CIS_ID"]), Convert.ToChar(rdlApprove.SelectedValue));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            if (rdlApprove.SelectedValue == "A")
            {
                MessageBox("Condemned Item Sale Approved Successfully.");                
                btnApprove.Enabled = false;               
            }
            else
            {
                MessageBox("Condemned Item Sale Rejected Successfully.");
                btnApprove.Enabled = false;               
            }
        }
        else
        {
            MessageBox("Transaction Failed.");
        }
        pnlDSRDetails.Visible = false;
        lvCondemnedSaleEntry.Visible = true;
        pnlApprove.Visible = false;
        BindListViewCond_SALEDetail();
        pnlApprove.Visible = false;
    }
    
    //protected void 
}




































//if(Status=='A')
//{
//btnselect_Click.enabled=false;
//}
//if(status='R')
//{
//btnselect_click.enabled=false;
//}
//else
//{
//btnSelect_click.enabled=true;
//}