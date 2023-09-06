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


public partial class STORES_Transactions_Str_CondemnItemSale : System.Web.UI.Page
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
                ObjComman.FillDropDownList(ddlVendorName, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");



                ViewState["Action"] = "Add";
                //txtVPDate.Text = DateTime.Now.ToString();
                BindListViewCond_SALEDetail();
                Session["dtItem"] = null;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Transactions_Str_CondemnItemSale.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubCategory.Items.Clear();
        ddlSubCategory.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlItem.Items.Clear();
        ddlItem.Items.Insert(0, new ListItem("Please Select", "0"));
        string itemType = "SCRAP";
        string itemType1 = "NONSCRAP";
        int MISGNO = 0;
        //DataSet ds = objJvStkCon.GetItemType(ItemNo, txtToDate.Text);

        if (ddlItemType.SelectedItem.Value == "1")
        {
            DataSet ds = objJvStkCon.GetItemType(itemType, MISGNO);
            ddlSubCategory.DataSource = ds.Tables[0];
            ddlSubCategory.DataTextField = "MISGNAME";
            ddlSubCategory.DataValueField = "MISGNO";
            ddlSubCategory.DataBind();

        }
        else if (ddlItemType.SelectedItem.Value == "2")
        {
            DataSet ds = objJvStkCon.GetItemType(itemType1, MISGNO);
            ddlSubCategory.DataSource = ds.Tables[0];
            ddlSubCategory.DataTextField = "MISGNAME";
            ddlSubCategory.DataValueField = "MISGNO";
            //ddlSubCategory.DataTextField = "MISGNAME";
            //ddlSubCategory.DataValueField = "MISGNO";
            ddlSubCategory.DataBind();
        }
        else if (ddlItemType.SelectedItem.Value == "3")
        {
            ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");

        }
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itemType = "";
        ddlItem.Items.Clear();
        ddlItem.Items.Insert(0, new ListItem("Please Select", "0"));
        int MISGNO = 0;

        MISGNO = Convert.ToInt32(ddlSubCategory.SelectedValue);


        if (ddlItemType.SelectedItem.Value == "1")
        {
            itemType = "SCRAP";
            DataSet ds = objJvStkCon.GetItemType(itemType, MISGNO);
            ddlItem.DataSource = ds.Tables[0];
            ddlItem.DataTextField = "ITEM_NAME";
            ddlItem.DataValueField = "ITEM_NO";

            //ddlItem.DataTextField = "DSR_NUMBER";
            //ddlItem.DataValueField = "MISGNO";
            ddlItem.DataBind();
        }
        if (ddlItemType.SelectedItem.Value == "2")
        {
            itemType = "NONSCRAP";
            DataSet ds = objJvStkCon.GetItemType(itemType, MISGNO);
            ddlItem.DataSource = ds.Tables[0];
            ddlItem.DataTextField = "ITEM_NAME";
            ddlItem.DataValueField = "ITEM_NO";
            ddlItem.DataBind();
        }
        if (ddlItemType.SelectedItem.Value == "3")
        {
            ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
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
            //DataSet ds = ObjComman.FillDropDown("STORE_INVOICE_DSR_ITEM", "INVDINO as DSR_ID", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DEPR_CAL_START_DATE IS NOT NULL AND DSR_NUMBER IS NOT NULL", "");
            //DataSet ds = ObjComman.FillDropDown("STORE_INVOICE_DSR_ITEM", "INVDINO as DSR_ID", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DSR_NUMBER IS NOT NULL", "");
            //DataSet ds = ObjComman.FillDropDown("STORE_INVOICE_DSR_ITEM", "INVDINO as DSR_ID", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DSR_NUMBER IS NOT NULL AND DSR_NUMBER not in(Select DSR_NUMBER from STORE_CONDEMNED_ITEM_TRAN)", "");


            DataSet ds = objJvStkCon.GetItemsForSale(ddlItemType.SelectedItem.Text, Convert.ToInt32(ddlItem.SelectedValue));
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
           

            //lvItem.Visible = true;
            //DataTable dtItemDup = (DataTable)Session["dtItem"];
            string ItemNo1 = string.Empty;
            string TotDsr = string.Empty;
            int count = 0;
            foreach (ListViewItem lv in lvShowItem.Items)
            {
                CheckBox chkInvdiNo = lv.FindControl("chkInvdiNo") as CheckBox;
                Label lblDSRNumber = lv.FindControl("lblDSRNumber") as Label;
               
                if (chkInvdiNo.Checked)
                {
                    count++;
                    string invidno = lblDSRNumber.Text;


                    string DsrNumber = string.Empty;
                    foreach (ListViewItem lv1 in lvItemSale.Items)
                    {
                        Label lblDSRNumber1 = lv1.FindControl("lblDSR_NUMBER") as Label;

                        string dsrnumber = lblDSRNumber1.Text;
                        //DsrNumber += dsrnumber + ',';

                        if (invidno == dsrnumber)
                        {
                            MessageBox("Item Already axist");
                            return;
                        }
                    }
                    //ItemNo1 += invidno + ',';
                }
            }
            if (count == 0)
            {
                MessageBox("Please Select Atleast One Item");
                return;

            }


            string ItemNo = string.Empty;
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


            AddItemTable();
            //Session["dtItem"] = ItemRTbl;

            DataSet ds = objJvStkCon.GetCondemnItemSale(ItemNo, txtToDate.Text);

            dtRow = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtRow = ItemRTbl.NewRow();

                    dtRow["DSR_NUMBER"] = ds.Tables[0].Rows[i]["DSR_NUMBER"];
                    dtRow["DSR_ID"] = ds.Tables[0].Rows[i]["DSR_ID"];
                    dtRow["STOCK_VALUE"] = ds.Tables[0].Rows[i]["STOCK_VALUE"];
                    dtRow["DEPRECIATED_AMOUNT"] = ds.Tables[0].Rows[i]["DEPRECIATED_AMOUNT"];
                    dtRow["VALUE"] = ds.Tables[0].Rows[i]["VALUE"];
                    dtRow["SALE_VALUE"] = ds.Tables[0].Rows[i]["SALE_VALUE"];
                    dtRow["PROFLOSS"] = ds.Tables[0].Rows[i]["PROFLOSS"];
                    //dtRow["REMARK"] = "";

                    ItemRTbl.Rows.Add(dtRow);


                    lvItemSale.DataSource = ItemRTbl;
                    lvItemSale.DataBind();


                    //btnSave.Visible = true;
                    lvItemSale.Visible = true;
                    btnSave.Enabled = true;
                }
            }
            else
            {
                if (ItemRTbl.Rows.Count > 0)
                {
                    lvItemSale.DataSource = ItemRTbl;
                    lvItemSale.DataBind();
                    //btnSave.Visible = true;
                    lvItemSale.Visible = true;
                    btnSave.Enabled = true;

                }
                else
                {
                    lvItemSale.DataSource = null;
                    lvItemSale.DataBind();
                    //btnSave.Visible = false;
                    lvItemSale.Visible = false;
                    MessageBox("No Record Found");
                }


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Str_CondemnItemSale.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }
    }

    private void AddItemTable()
    {
        ItemRTbl = this.CreateCondemnedSaleTable();

        dtRow = null;
        foreach (ListViewItem lv in lvItemSale.Items)
        {
            Label lblDSR_NUMBER = lv.FindControl("lblDSR_NUMBER") as Label;
            HiddenField hdnDSR_ID = lv.FindControl("hdnDSR_ID") as HiddenField;
            Label lblSTOCK_VALUE = lv.FindControl("lblSTOCK_VALUE") as Label;
            Label lblDEPRECIATED_AMOUNT = lv.FindControl("lblDEPRECIATED_AMOUNT") as Label;
            Label lblVALUE = lv.FindControl("lblVALUE") as Label;

            TextBox lblSaleValue = lv.FindControl("lblSaleValue") as TextBox;
            Label lblProfitLoss = lv.FindControl("lblProfitLoss") as Label;
            HiddenField hdnProfitLoss = lv.FindControl("hdnProfitLoss") as HiddenField;
            TextBox lblREMARK = lv.FindControl("lblREMARK") as TextBox;

            dtRow = ItemRTbl.NewRow();
            dtRow["DSR_NUMBER"] = lblDSR_NUMBER.Text;
            dtRow["DSR_ID"] = Convert.ToInt32(hdnDSR_ID.Value);
            dtRow["STOCK_VALUE"] = Convert.ToDouble(lblSTOCK_VALUE.Text);
            dtRow["DEPRECIATED_AMOUNT"] = Convert.ToDouble(lblDEPRECIATED_AMOUNT.Text);
            dtRow["VALUE"] = Convert.ToDouble(lblVALUE.Text);
            dtRow["SALE_VALUE"] = Convert.ToDouble(lblSaleValue.Text);
            dtRow["PROFLOSS"] = Convert.ToDouble(hdnProfitLoss.Value);
            dtRow["REMARK"] = lblREMARK.Text;

            ItemRTbl.Rows.Add(dtRow);

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
            objJvStockEnt.COND_SALE_TRNO = txtCOND_SALE_TRNO.Text;
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
        btnSave.Enabled = false;
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
        ddlItemType.SelectedIndex = 0;                 //vishwas 
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


        ViewState["Action"] = "Add";
    }
    protected void btnAddN_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = true;
        lvCondemnedSaleEntry.Visible = false;
        btnAddN.Visible = false;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        btnSave.Enabled = false;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = false;
        lvCondemnedSaleEntry.Visible = true;
        btnAddN.Visible = true;
        BindListViewCond_SALEDetail();

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        pnlDSRDetails.Visible = true;
        lvCondemnedSaleEntry.Visible = false;
        btnAddN.Visible = false;
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ClearAll();
        ImageButton btnEdit = sender as ImageButton;
        int CIS_ID = Convert.ToInt32(btnEdit.CommandArgument);
        ViewState["CIS_ID"] = CIS_ID;
        ViewState["Action"] = "Edit";

        DataSet dsEdit = objJvStkCon.GetCondSaleDetailsById(CIS_ID);
        if (dsEdit.Tables[0].Rows[0]["COND_STATUS"].ToString() == "A")
        {
            MessageBox("Approved Record Can Not Be Modify.");
            return;
        }
        else if (dsEdit.Tables[0].Rows[0]["COND_STATUS"].ToString() == "R")
        {
            MessageBox("Rejected Record Can Not Be Modify.");
            return;
        }

        txtCOND_SALE_TRNO.Text = dsEdit.Tables[0].Rows[0]["COND_SALE_TRNO"].ToString();
        divTRNumber.Visible = true;
        txtToDate.Text = dsEdit.Tables[0].Rows[0]["SALE_DATE"].ToString();
        ddlVendorName.SelectedValue = dsEdit.Tables[0].Rows[0]["PNO"].ToString();


        //ddlItem.SelectedValue = dsEdit.Tables[0].Rows[0]["ITEM_NO"].ToString();
        //ddlSubCategory.SelectedValue = dsEdit.Tables[0].Rows[0]["MISGNO"].ToString();
        //ddlItemType.SelectedValue == dsEdit.Tables[0].Rows[0]["ITEM_NO"].ToString();



        if (dsEdit.Tables[1].Rows.Count > 0)
        {
            lvItemSale.DataSource = dsEdit.Tables[1];
            lvItemSale.DataBind();
            lvItemSale.Visible = true;
            //hdnRowCount.Value = dsEdit.Tables[1].Rows.Count.ToString();
            //divList.Visible = true;
        }
        btnSave.Enabled = true;

        lvCondemnedSaleEntry.Visible = false;
        btnAddN.Visible = false;
        pnlDSRDetails.Visible = true;

    }

    protected void ddlSubCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //DateTime date;
        //if (!DateTime.TryParse(txtToDate.Text, out date))
        //{
        //    MessageBox(this.Min, "The Date you entered is in invalid format");
        //}
    }
}