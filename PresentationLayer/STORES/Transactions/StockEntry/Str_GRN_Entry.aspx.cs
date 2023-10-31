
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                       
// CREATION DATE : 22-June-2021                                                 
// CREATED BY    : GOPAL ANTHATI                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class STORES_Transactions_StockEntry_Str_GRN_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StrGRNCon objGRNCon = new StrGRNCon();
    StrGRNEnt objGRNEnt = new StrGRNEnt();

    DataTable dtItemTable = null;
    DataRow datarow = null;
    int dupItemFlag = 0;
    int SaveItemFlag = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }
    protected void Page_Load(object sender, EventArgs e)
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {

                }

            }
            ViewState["Items"] = null;
            Session["dtItem"] = null;
            FillDropDownList();
            ViewState["Action"] = "Add";
            ViewState["TaxTable"] = null;
            ViewState["SRNO_TAX"] = null;
            BindListView();
            txtGRNDate.Text = DateTime.Now.ToString();
        }
        //
        divMsg.InnerText = string.Empty;
    }
    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");

        //Modified by Shabina 28-09-2021
        //ddlSecNumber.Items.Clear();
        //DataSet ds1 = objGRNCon.GetddlSPlist();
        //if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
        //{
        //    ddlSecNumber.DataSource = ds1.Tables[0];
        //    ddlSecNumber.DataTextField = ds1.Tables[0].Columns["SP_NUMBER"].ToString();
        //    ddlSecNumber.DataValueField = ds1.Tables[0].Columns["SPID"].ToString();

        //    ddlSecNumber.DataBind();

        //}


        objCommon.FillDropDownList(ddlSecNumber, "STORE_SEC_PASS_MAIN", "SPID", "SP_NUMBER", "SPID NOT IN (SELECT SPID FROM STORE_GRN_MAIN)", "SP_NUMBER DESC");
        ddlPO.Items.Clear();

        DataSet ds = objGRNCon.GetPODropdown();

        // DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", " STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE_ITEM) AND PORDNO NOT IN (SELECT PORDNO FROM STORE_SEC_PASS_MAIN)", "PORDNO DESC");
        // DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A'", "PORDNO DESC");//AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE)
        if (ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
        }
    }
    private void BindListView()
    {
        DataSet ds = objCommon.FillDropDown("STORE_GRN_MAIN A INNER JOIN STORE_PARTY B ON (A.PNO=B.PNO)", "GRN_NUMBER,SPDATE,GRNDATE", "GRNID,A.REMARK,B.PNAME", "", "GRNID DESC");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvGRNEntry.DataSource = ds.Tables[0];
            lvGRNEntry.DataBind();
            lvGRNEntry.Visible = true;
        }
        else
        {
            lvGRNEntry.DataSource = null;
            lvGRNEntry.DataBind();
            lvGRNEntry.Visible = false;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSPNum.Visible = false;
        divAddItem.Visible = false;
        string PoNumbers = GetPONumbers();
        DataSet ds1 = objGRNCon.GetItemsByPO(PoNumbers);
        dtItemTable = ds1.Tables[0];
        //ds1.Merge(ds2);
        lvItem.DataSource = ds1.Tables[0];
        lvItem.DataBind();
        lvItem.Visible = true;
        if (ds1.Tables[1].Rows.Count > 0 && ds1.Tables[1].Rows[0]["REFNO"].ToString() != "")
        {
            txtPONum.Text = ds1.Tables[1].Rows[0]["REFNO"].ToString();
            divPONum.Visible = false;
        }
        else
        {
            divPONum.Visible = false;
        }
        Session["dtItem"] = dtItemTable;
        if (Session["dtItem"] != null || (DataTable)Session["dtItem"] != null)
        {
            AddItemTable();
            for (int i = 0; i < dtItemTable.Rows.Count; i++)
            {
                dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
            }
            Session["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
        }

        CalItemCount();
        if (ddlPO.SelectedValue == "")
            divSPNum.Visible = true;
        else
            divSPNum.Visible = false;

        AddPONumber();

    }
    private string GetPONumbers()
    {
        string PONumber = "";
        string PoValues = string.Empty;
        int count = 0;
        // pnlFeeTable.Update();
        foreach (ListItem item in ddlPO.Items)
        {
            if (item.Selected == true)
            {
                PONumber += item.Value + '$';
                count = 1;
            }
        }
        if (count > 0)
        {
            PoValues = PONumber.Substring(0, PONumber.Length - 1);
            if (PoValues != "")
            {
                string[] degValue = PoValues.Split('$');

            }
        }
        return PoValues;

    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        divSPNum.Visible = false;
        divPO.Visible = false;
        PnlItem.Visible = true;
        divAddItem.Visible = false;
        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
    }
    #region AddItem

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        try
        {
            lvItem.Visible = true;
            SaveItemFlag = 1;
            //DataTable dtItemDup = (DataTable)Session["dtItem"];

            //if (CheckDuplicateVehRow(dtItemDup, ddlVehicle.SelectedItem.Text.Trim()))
            //{
            //    lvItem.DataSource = dtItemDup;
            //    lvItem.DataBind();
            //    ddlVehicle.SelectedIndex = 0;
            //    MessageBox("This Vehicle Is Already Exist.");
            //    return;
            //}


            int maxVal = 0;
            AddItemTable();


            if (dupItemFlag == 1)
            {
                return;
            }

            Session["dtItem"] = dtItemTable;
            datarow = dtItemTable.NewRow();

            if (datarow != null)
            {
                maxVal = Convert.ToInt32(dtItemTable.AsEnumerable().Max(row => row["ITEM_SRNO"]));
            }
            datarow["ITEM_SRNO"] = maxVal + 1;
            datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
            datarow["ITEM_NAME"] = ddlItem.SelectedItem.Text;
            datarow["PO_QTY"] = 0;
            datarow["RECEIVED_QTY"] = 0;
            datarow["GRN_QTY"] = txtItemQty.Text == "" ? "0" : txtItemQty.Text;
            datarow["BAL_QTY"] = 0;
            datarow["RATE"] = 0;
            datarow["DISC_PER"] = 0;
            datarow["DISC_AMT"] = 0;
            datarow["TAXABLE_AMT"] = 0;
            datarow["TAX_AMT"] = 0;
            datarow["BILL_AMT"] = 0;
            datarow["ITEM_REMARK"] = txtItemRemark.Text;
            datarow["PORDNO"] = 0;
            datarow["IS_TAX"] = 0;
            datarow["TECH_SPEC"] = "";
            datarow["QUALITY_QTY_SPEC"] = "";

            dtItemTable.Rows.Add(datarow);

            Session["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
            ClearItem();
            CalItemCount();
            AddPONumber();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void AddPONumber()
    {
        foreach (ListViewItem lv in lvItem.Items)
        {
            HiddenField hdnPordno = lv.FindControl("hdnPordno") as HiddenField;
            Label lblRefno = lv.FindControl("lblRefno") as Label;
            lblRefno.Text = hdnPordno.Value == "0" ? "" : objCommon.LookUp("STORE_PORDER", "REFNO", "PORDNO=" + hdnPordno.Value);

        }
    }

    private void CalItemCount()
    {
        double ItemQtyCount = 0.0;
        for (int i = 0; i < dtItemTable.Rows.Count; i++)
        {
            ItemQtyCount += Convert.ToDouble(dtItemTable.Rows[i]["GRN_QTY"].ToString());
        }
       // divItemCount.Visible = true;
        lblItemCount.Text = dtItemTable.Rows.Count.ToString();
        lblItemQtyCount.Text = ItemQtyCount.ToString();
        hdnrowcount.Value = dtItemTable.Rows.Count.ToString();
    }

    private DataTable CreateItemTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PO_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RECEIVED_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GRN_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BAL_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_PER", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAXABLE_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAX_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BILL_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ITEM_REMARK", typeof(string)));
        dt.Columns.Add(new DataColumn("PORDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("IS_TAX", typeof(int)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        return dt;
    }
    private void ClearItem()
    {
        ddlSecNumber.Items.Clear();
        ddlItem.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.GetEditableDatarowFromTOG -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    private void AddItemTable()
    {
        dtItemTable = this.CreateItemTable();

        datarow = null;
        foreach (ListViewItem i in lvItem.Items)
        {
            HiddenField hdnItemSrNo = i.FindControl("hdnItemSrNo") as HiddenField;
            HiddenField hdnItemno = i.FindControl("hdnItemno") as HiddenField;
            Label lblItemName = i.FindControl("lblItemName") as Label; 
            TextBox lblGRNQty = i.FindControl("lblGRNQty") as TextBox;            
            TextBox lblRate = i.FindControl("lblRate") as TextBox;    
        
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;
            HiddenField hdnIsTax = i.FindControl("hdnIsTax") as HiddenField;
            HiddenField hdnTechSpec = i.FindControl("hdnTechSpec") as HiddenField;
            HiddenField hdnQualityQtySpec = i.FindControl("hdnQualityQtySpec") as HiddenField;
            HiddenField hdnOthItemRemark = i.FindControl("hdnOthItemRemark") as HiddenField;

            //Adde by gopal anthati on 18/05/2022 to maintain the disable field values

            TextBox lblDiscPer     =  i.FindControl("lblDiscPer") as TextBox;
            TextBox lblDiscAmt     =  i.FindControl("lblDiscAmt") as TextBox;
            TextBox lblTaxableAmt  =  i.FindControl("lblTaxableAmt") as TextBox;
            TextBox lblTaxAmount   =  i.FindControl("lblTaxAmount") as TextBox;
            TextBox lblBillAmt     =  i.FindControl("lblBillAmt") as TextBox;
            TextBox lblPOQty       =  i.FindControl("lblPOQty") as TextBox;
            TextBox lblReceivedQty =  i.FindControl("lblReceivedQty") as TextBox;
            TextBox lblBalQty      =  i.FindControl("lblBalQty") as TextBox;

            HiddenField hdnItemPOQty = i.FindControl("hdnItemPOQty") as HiddenField;
            HiddenField hdnItemRecQty = i.FindControl("hdnItemRecQty") as HiddenField;
            HiddenField hdnItemBalQty = i.FindControl("hdnItemBalQty") as HiddenField;
            HiddenField hdnItemDiscPer = i.FindControl("hdnItemDiscPer") as HiddenField;
            HiddenField hdnItemDiscAmt = i.FindControl("hdnItemDiscAmt") as HiddenField;
            HiddenField hdnItemTaxableAmt = i.FindControl("hdnItemTaxableAmt") as HiddenField;
            HiddenField hdnItemTaxAmt = i.FindControl("hdnItemTaxAmt") as HiddenField;
            HiddenField hdnItemBillAmt = i.FindControl("hdnItemBillAmt") as HiddenField;

            //End////



            if (SaveItemFlag == 1 && lblItemName.Text == ddlItem.SelectedItem.Text)
            {
                MessageBox("This Item Name Already Exist.");
                dupItemFlag = 1;
                return;
            }
            else
            {

                datarow = dtItemTable.NewRow();
                datarow["ITEM_SRNO"] = hdnItemSrNo.Value;
                datarow["ITEM_NO"] = hdnItemno.Value;
                datarow["ITEM_NAME"] = lblItemName.Text;
                datarow["PO_QTY"] = hdnItemPOQty.Value == "" ? "0" : hdnItemPOQty.Value;
                datarow["RECEIVED_QTY"] = hdnItemRecQty.Value == "" ? "0" : hdnItemRecQty.Value;
                datarow["GRN_QTY"] = lblGRNQty.Text == "" ? "0" : lblGRNQty.Text;
                datarow["BAL_QTY"] = hdnItemBalQty.Value == "" ? "0" : hdnItemBalQty.Value;
                datarow["RATE"] = lblRate.Text == "" ? "0" : lblRate.Text;
                datarow["DISC_PER"] = hdnItemDiscPer.Value == "" ? "0" : hdnItemDiscPer.Value;
                datarow["DISC_AMT"] = hdnItemDiscAmt.Value == "" ? "0" : hdnItemDiscAmt.Value;
                datarow["TAXABLE_AMT"] = hdnItemTaxableAmt.Value == "" ? "0" : hdnItemTaxableAmt.Value;
                datarow["TAX_AMT"] = hdnItemTaxAmt.Value == "" ? "0" : hdnItemTaxAmt.Value;
                datarow["BILL_AMT"] = hdnItemBillAmt.Value == "" ? "0" : hdnItemBillAmt.Value;
                datarow["ITEM_REMARK"] = hdnOthItemRemark.Value;
                datarow["PORDNO"] = hdnPordno.Value;
                datarow["IS_TAX"] = hdnIsTax.Value;
                datarow["TECH_SPEC"] = hdnTechSpec.Value;
                datarow["QUALITY_QTY_SPEC"] = hdnOthItemRemark.Value;

                dtItemTable.Rows.Add(datarow);

                lblPOQty.Text = hdnItemPOQty.Value;  
                lblBalQty.Text = hdnItemBalQty.Value;
                lblReceivedQty.Text = hdnItemRecQty.Value;
                lblDiscPer.Text = hdnItemDiscPer.Value;
                lblDiscAmt.Text = hdnItemDiscAmt.Value;
                lblTaxableAmt.Text = hdnItemTaxableAmt.Value;
                lblTaxAmount.Text = hdnItemTaxAmt.Value;
                lblBillAmt.Text = hdnItemBillAmt.Value;   
            }

        }
    }

    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
            {
                AddItemTable();
                dtItemTable.Rows.Remove(this.GetEditableDatarow(dtItemTable, btnDelete.CommandArgument));
                for (int i = 0; i < dtItemTable.Rows.Count; i++)
                {
                    dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
                }
                Session["dtItem"] = dtItemTable;
                lvItem.DataSource = dtItemTable;
                lvItem.DataBind();
                lvItem.Visible = true;
                CalItemCount();
            }



            //ImageButton btnDelete = sender as ImageButton;
            //if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
            //{
            //    AddItemTable();
            //    dtItemTable.Rows.Remove(this.GetEditableDatarow(dtItemTable, btnDelete.CommandArgument));
            //    for (int i = 0; i < dtItemTable.Rows.Count; i++)
            //    {
            //        dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
            //    }
            //    Session["dtItem"] = dtItemTable;
            //    lvItem.DataSource = dtItemTable;
            //    lvItem.DataBind();
            //    lvItem.Visible = true;
            //    CalItemCount();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnDeleteRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion
    private bool CheckDuplicateTaxRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["VNAME"].ToString().ToLower() == value.ToLower())
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateAdministrationRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void btnAddTax_Click(object sender, ImageClickEventArgs e)
    {
        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;
        lblTaxableAmt.Text = hdnTaxableAmt.Value;
        lblTaxAmt.Text = hdnTaxAmt.Value;
        lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnTaxableAmt.Value) + Convert.ToDecimal(hdnTaxAmt.Value));
        lblDiscAmt.Text = hdnDiscAmt.Value;

       
        //DataTable dtTaxdup = null;
        ViewState["ItemNo"] = null;
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);

        if (ViewState["TaxTable"] != null)
        {
            //dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                //BindTaxes();
                DataSet ds = null;
                int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + ddlVendor.SelectedValue));
                int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
                if (VendorState == CollegeState)
                {
                    ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
                }
                else
                {
                    ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
                }
                lvTax.DataSource = ds.Tables[0];
                lvTax.DataBind();
                hdnListCount.Value = ds.Tables[0].Rows.Count.ToString();
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                CalTotTax();
                //if (ViewState["Action"].ToString() == "edit")
                //{
                //    DataTable dt = foundRow.CopyToDataTable();
                //    lvTax.DataSource = dt;
                //    lvTax.DataBind();
                //    hdnListCount.Value = dtTaxdup.Rows.Count.ToString();
                //    this.MdlTax.Show();
                //    divOthPopup.Visible = false;
                //    divTaxPopup.Visible = true;
                //    //ViewState["TaxEdit"]="edit";
                //    CalTotTax();
                //}
                //else
                //{
                //    BindTaxes();
                //}

            }
            else
            {
                GetDefaultTaxes();
            }

        }
        else
        {
            GetDefaultTaxes();
        }

    }

    private void CalTotTax()
    {
        decimal TotTaxAmt = 0;
        foreach (ListViewItem i in lvTax.Items)
        {
            TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            TotTaxAmt += Convert.ToDecimal(lblTaxAmount.Text);
        }
        txtTotTaxAmt.Text = TotTaxAmt.ToString("00.00");
    }
    private void GetDefaultTaxes()
    {
        DataSet ds = null;
        int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + ddlVendor.SelectedValue));
        int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
        if (VendorState == CollegeState)
        {
            ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtTaxAdd = (DataTable)ds.Tables[0];
            DataRow[] foundRow = dtTaxAdd.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dtTaxNew = foundRow.CopyToDataTable();
                lvTax.DataSource = dtTaxNew;
                lvTax.DataBind();
                hdnListCount.Value = dtTaxNew.Rows.Count.ToString();
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                CalTotTax();
                //txtTotTaxAmt.Text = ds.Tables[1].Rows[0]["TOT_TAX_AMT"].ToString();
            }
        }
        else
        {
            lvTax.DataSource = null;
            lvTax.DataBind();
            this.MdlTax.Hide();
            MessageBox("No Taxes Are Applicable For This Item.");
        }
    }
    protected void btnTaxSubmit_Click(object sender, EventArgs e)
    {
        CalTotTax();
        //if (ViewState["TaxEdit"] == null)
        // {
        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;
        lblTaxableAmt.Text = hdnTaxableAmt.Value;
        lblTaxAmt.Text = txtTotTaxAmt.Text;
        lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnTaxableAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));
        lblDiscAmt.Text = hdnDiscAmt.Value;


        if (ViewState["TaxTable"] != null && ((DataTable)ViewState["TaxTable"]) != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                foreach (DataRow drow in foundRow)
                    dtTaxdup.Rows.Remove(drow);
            }
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                int maxVal = 0;
                DataTable dtTax = (DataTable)ViewState["TaxTable"];
                DataRow dtRow = null;
                dtRow = dtTax.NewRow();
                if (dtRow != null)
                {
                    maxVal = Convert.ToInt32(dtTax.AsEnumerable().Max(row => row["TAX_SRNO"]));
                }
                dtRow["TAX_SRNO"] = maxVal + 1;
                dtRow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                dtRow["TAXID"] = hdnTaxId.Value;
                dtRow["TAX_NAME"] = lblTaxName.Text;
                dtRow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(dtRow);
                ViewState["TaxTable"] = dtTax;
            }
        }
        else
        {
            DataTable dtTax = this.CreateTaxTable();
            DataRow dtRow = null;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                dtRow = dtTax.NewRow();

                dtRow["TAX_SRNO"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtRow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                dtRow["TAXID"] = hdnTaxId.Value;
                dtRow["TAX_NAME"] = lblTaxName.Text;
                dtRow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(dtRow);
                ViewState["TaxTable"] = dtTax;
            }
        }
        // }
        // else
        // {
        // }
        txtTotTaxAmt.Text = string.Empty;
    }

    private DataTable CreateTaxTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("TAX_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(int)));
        dt.Columns.Add(new DataColumn("TAX_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAX_AMOUNT", typeof(decimal)));
        return dt;
    }


    private void GenerateGRNNumber()
    {
        DataSet ds = objGRNCon.GetGRNNumber();
        txtGRNNumber.Text = ds.Tables[0].Rows[0]["GRN_NUMBER"].ToString();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddItemTable();
        string PoNumber = GetPONumbers();

        if (dtItemTable.Rows.Count == 0)
        {
            MessageBox("Please Add Item Details");
            return;
        }
       

        objGRNEnt.SPID = ddlSecNumber.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSecNumber.SelectedValue);
    
        if (txtSpDate.Text != string.Empty)
        {
            objGRNEnt.SPDATE = Convert.ToDateTime(txtSpDate.Text);
        }
        else
        {
            objGRNEnt.SPDATE = DateTime.MinValue;
        }
        objGRNEnt.GRNDATE = Convert.ToDateTime(txtGRNDate.Text);
        objGRNEnt.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
        objGRNEnt.REMARK = txtRemark.Text;
        objGRNEnt.DMDATE = Convert.ToDateTime(txtDMDate.Text);
        objGRNEnt.DMNO = txtDMNo.Text;
        objGRNEnt.PORDNO = PoNumber == "" ? "0" : PoNumber;
        objGRNEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objGRNEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);
        objGRNEnt.GRN_ITEM_TBL = dtItemTable;
        objGRNEnt.GRN_TAX_TBL = ViewState["TaxTable"] as DataTable;
        
        objGRNEnt.MDNO = Convert.ToInt32(Session["strdeptcode"]);

        //foreach (ListViewItem lv in lvItem.Items)
        //{
        //    TextBox lblRate = lv.FindControl("lblRate") as TextBox;
        //    TextBox lblGRNQty = lv.FindControl("lblGRNQty") as TextBox;
        //    if (lblRate.Text == "" || Convert.ToInt32(lblRate.Text) == 0)
        //    {
        //        MessageBox("Please Enter in Rate Amount..");
        //        return;
        //    }
        //    if (lblGRNQty.Text == "" || lblGRNQty.Text == "0")
        //    {
        //        MessageBox("Please Enter GRN Qty..");
        //        return;
        //    }
        //}


        if (ViewState["Action"].ToString() == "Add")
        {
            GenerateGRNNumber();
            objGRNEnt.GRN_NUMBER = txtGRNNumber.Text;
            objGRNEnt.GRNID = 0;
            CustomStatus cs = (CustomStatus)objGRNCon.InsUpdateGRNEntry(objGRNEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved & GRN Number Generated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        else
        {
            objGRNEnt.GRNID = Convert.ToInt32(ViewState["GRNID"]);
            objGRNEnt.GRN_NUMBER = txtGRNNumber.Text;
            CustomStatus cs = (CustomStatus)objGRNCon.InsUpdateGRNEntry(objGRNEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Updated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        //ClearAll(); 
        divGRNNumber.Visible = true;
        btnSubmit.Enabled = false;
        btnCancel.Visible = false;
        btnAddNew2.Visible = true;
    }
    protected void btnAdNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        divGRNEtry.Visible = true;
        lvGRNEntry.Visible = false;
        btnSubmit.Enabled = true;
        btnCancel.Visible = true;
        btnAddNew2.Visible = false;
    }

    private void ClearAll()
    {
        txtSpDate.Text = string.Empty;
        txtGRNDate.Text = DateTime.Now.ToString();
        ddlVendor.SelectedIndex = 0;
        //ddlSecNumber.SelectedIndex = 0;        
        txtRemark.Text = string.Empty;
        lvItem.DataSource = null;
        lvItem.DataBind();
        lvItem.Visible = false;
        lvTax.DataSource = null;
        lvTax.DataBind();
        txtGRNNumber.Text = string.Empty;
        divGRNNumber.Visible = false;
        lblItemCount.Text = string.Empty;
        lblItemQtyCount.Text = string.Empty;
        txtPONum.Text = string.Empty;
        divPONum.Visible = false;
        divPO.Visible = true;
        divSPNum.Visible = true;
        btnAddNew.Visible = false;
        txtDMNo.Text = string.Empty;
        txtDMDate.Text = string.Empty;
        hdnOthEdit.Value = "0";
        PnlItem.Visible = false;
        divAddItem.Visible = true;
        //divItemCount.Visible = false;
        txtItemQty.Text = "";   //modified by shabina 17/09/2021

        ddlPO.Visible = true;
        FillDropDownList();
        ViewState["Items"] = null;
        Session["dtItem"] = null;
        ViewState["Action"] = "Add";
        ViewState["TaxTable"] = null;
        ViewState["SRNO_TAX"] = null;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        divGRNEtry.Visible = false;
        lvGRNEntry.Visible = true;
        btnAddNew.Visible = true;
        BindListView();
    }
    protected void ddlSecNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAddItem.Visible = false;
        divPO.Visible = false;
        DataSet ds = objGRNCon.GetItemsBySPID(Convert.ToInt32(ddlSecNumber.SelectedValue));
        dtItemTable = ds.Tables[0];

        lvItem.DataSource = ds.Tables[0];
        lvItem.DataBind();
        lvItem.Visible = true;
        if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["REFNO"].ToString() != "")
        {
            txtPONum.Text = ds.Tables[1].Rows[0]["REFNO"].ToString();
            divPONum.Visible = true;
            string[] SecurityPOs = txtPONum.Text.Split('$');
        }
        else
        {
            divPONum.Visible = false;
        }
        Session["dtItem"] = dtItemTable;
        if (Session["dtItem"] != null || (DataTable)Session["dtItem"] != null)
        {
            AddItemTable();
            for (int i = 0; i < dtItemTable.Rows.Count; i++)
            {
                dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
            }
            Session["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
        }
        CalItemCount();
        ddlPO.Items.Clear();
        DataSet dsPO = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE_ITEM)", "PORDNO DESC");
        for (int i = 0; i < dsPO.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(dsPO.Tables[0].Rows[i][1]), Convert.ToString(dsPO.Tables[0].Rows[i][0])));
        }
        if (ddlSecNumber.SelectedIndex == 0)
            divPO.Visible = true;
        else
            divPO.Visible = false;

        AddPONumber();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int GRNID = Convert.ToInt32(btnEdit.CommandArgument);
        if (Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_ITEM", "COUNT(*)", "GRNID=" + GRNID)) > 0)        
        {
            MessageBox("Invoice Entry Already Submitted For This GRN Number.So,You Can not Modify.");
            return;
        }
        ClearAll();
        ViewState["GRNID"] = GRNID;
        ViewState["Action"] = "edit";
        divGRNEtry.Visible = true;
        lvGRNEntry.Visible = false;
        btnSubmit.Enabled = true;
        ShowDetals(GRNID);
        btnCancel.Visible = true;
        btnAddNew2.Visible = true;
    }

    private void ShowDetals(int GRNID)
    {
        objCommon.FillDropDownList(ddlSecNumber, "STORE_SEC_PASS_MAIN", "SPID", "SP_NUMBER", "", "SP_NUMBER");
        DataSet ds = objGRNCon.GetGRNEntryDetailsForEdit(GRNID);
        txtGRNNumber.Text = ds.Tables[0].Rows[0]["GRN_NUMBER"].ToString();
        divGRNNumber.Visible = true;
        txtGRNDate.Text = ds.Tables[0].Rows[0]["GRNDATE"].ToString();
        txtSpDate.Text = ds.Tables[0].Rows[0]["SPDATE"].ToString();
        ddlSecNumber.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
        ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["PNO"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        txtDMDate.Text = ds.Tables[0].Rows[0]["DMDATE"].ToString();
        txtDMNo.Text = ds.Tables[0].Rows[0]["DMNO"].ToString();
        string[] PONum = ds.Tables[0].Rows[0]["PORDNO"].ToString().Split("$".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SPID"]) > 0)
        {
            divPO.Visible = false;
            txtPONum.Text = ds.Tables[3].Rows[0]["REFNO"].ToString();
            divPONum.Visible = false;
            divAddItem.Visible = false;
        }
        else if (ds.Tables[0].Rows[0]["PORDNO"].ToString() != "0" && ds.Tables[0].Rows[0]["PORDNO"].ToString() != "")
        {
            divSPNum.Visible = false;
            txtPONum.Text = ds.Tables[3].Rows[0]["REFNO"].ToString();
            divPONum.Visible = true;
            divAddItem.Visible = false;
        }
        else
        {
            divPO.Visible = false;
            divPONum.Visible = false;
            divSPNum.Visible = false;
        }
        foreach (string PO in PONum)
        {
            if (ddlPO.Items.FindByValue(PO) != null)
            {
                ddlPO.Items.FindByValue(PO).Selected = true;
            }
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            lvItem.DataSource = ds.Tables[1];
            lvItem.DataBind();
            lvItem.Visible = true;
            dtItemTable = ds.Tables[1];
            Session["dtItem"] = dtItemTable;
            CalItemCount();
            AddPONumber();
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            //lvTax.DataSource = ds.Tables[2];
            //lvTax.DataBind();
            hdnListCount.Value = ds.Tables[2].Rows.Count.ToString();
            ViewState["TaxTable"] = ds.Tables[2];
        }
    }
    protected void btnAddOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;
        lblDiscAmt.Text = hdnDiscAmt.Value;

        this.MdlTax.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        if (ViewState["Action"].ToString() == "edit" && hdnOthEdit.Value == "0")
        {
            ImageButton btn = sender as ImageButton;
            int ItemNo = Convert.ToInt32(btn.CommandArgument);
            DataSet ds = objCommon.FillDropDown("STORE_GRN_ITEM", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "GRNID=" + Convert.ToInt32(ViewState["GRNID"]) + " AND ITEM_NO=" + ItemNo, "");
            txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
            txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
            txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();
        }
    }

    protected void btnSaveOthInfo_Click(object sender, EventArgs e)
    {
        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmount = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;
       
        lblTaxableAmt.Text = hdnTaxableAmt.Value;
        lblBillAmt.Text = (Convert.ToDouble(lblTaxableAmt.Text) + Convert.ToDouble(lblTaxAmount.Text)).ToString();
        //lblTaxAmount.Text = hdnTaxAmt.Value;//txtTotTaxAmt.Text;        
        //lblBillAmt.Text = hdnBillAmt.Value;
        this.MdlTax.Hide();
        txtItemRemarkOth.Text = string.Empty;
        txtQualityQtySpec.Text = string.Empty;
        txtTechSpec.Text = string.Empty;

    }
}