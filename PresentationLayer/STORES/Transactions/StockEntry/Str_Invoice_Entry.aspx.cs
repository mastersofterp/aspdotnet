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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class Stores_Transactions_Stock_Entry_Str_Invoice_Entry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    //StrGRNCon objGRNCon = new StrGRNCon();
    //StrGRNEnt objGRNEnt = new StrGRNEnt();
    Str_Invoice_Entry_Controller objINVCon = new Str_Invoice_Entry_Controller();
    Str_Invoice objINVEnt = new Str_Invoice();

    DataTable dtItemTable = null;
    DataRow datarow = null;
    BlobController objBlob = new BlobController();

    int dupItemFlag = 0;
    int SaveItemFlag = 0;

    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";

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
            txtInvoiceDate.Text = DateTime.Now.ToString();
            BlobDetails();
            ViewState["INV1"] = null;
            ViewState["FILE1"] = null;

            objCommon.FillDropDownList(ddlInv, "STORE_INVOICE", "INVTRNO", "INVNO", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "INVTRNO DESC");
        }
        //
        divMsg.InnerText = string.Empty;
      //  NetAmountCalculation();

    }
    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
        //objCommon.FillDropDownList(ddlGRNNumber, "STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "GRNID NOT IN (SELECT GRNID FROM STORE_INVOICE)", "GRN_NUMBER");
        ddlPO.Items.Clear();


        DataSet ds = objINVCon.GetPODropdown();



        //DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_GRN_ITEM) AND PORDNO NOT IN (SELECT PORDNO FROM STORE_SEC_PASS_MAIN)", "PORDNO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }

        ddlGRNNumber.Items.Clear();
        DataSet dsgrn = objCommon.FillDropDown("STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "", "GRN_NUMBER DESC");
        for (int i = 0; i < dsgrn.Tables[0].Rows.Count; i++)
        {
            ddlGRNNumber.Items.Add(new ListItem(Convert.ToString(dsgrn.Tables[0].Rows[i][1]), Convert.ToString(dsgrn.Tables[0].Rows[i][0])));
        }
    }
    private void BindListView()
    {
        DataSet ds = objCommon.FillDropDown("STORE_INVOICE A INNER JOIN STORE_PARTY B ON (A.PNO=B.PNO)", "INVNO,INVDATE,GRNDATE", "INVTRNO,A.REMARK,B.PNAME", "", "INVTRNO DESC");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvInvoiceEntry.DataSource = ds.Tables[0];
            lvInvoiceEntry.DataBind();
            lvInvoiceEntry.Visible = true;
        }
        else
        {
            lvInvoiceEntry.DataSource = null;
            lvInvoiceEntry.DataBind();
            lvInvoiceEntry.Visible = false;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNamestoresdoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
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
    private void AddPONumber()
    {
        foreach (ListViewItem lv in lvItem.Items)
        {
            HiddenField hdnPordno = lv.FindControl("hdnPordno") as HiddenField;
            Label lblRefno = lv.FindControl("lblRefno") as Label;
            lblRefno.Text = hdnPordno.Value == "0" ? "" : objCommon.LookUp("STORE_PORDER", "REFNO", "PORDNO=" + hdnPordno.Value);

        }
    }
    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs edivPONum)
    {
        PnlItem.Visible = false;  //012/05/2022

        divGRNNum.Visible = false;
        divAddItem.Visible = false;
        string PoNumbers = GetPONumbers();
        DataSet ds1 = objINVCon.GetItemsByPO(PoNumbers);
        //DataSet ds1 = objINVCon.GetItemsByPO(PoNumbers);
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
        {
            divGRNNum.Visible = true;
            divAddItem.Visible = true;
        }
        else
        {
            divGRNNum.Visible = false;
            divAddItem.Visible = false;
        }

        AddPONumber();
    }
    private string GetPONumbers()
    {
        string PONumber = "";
        string PoValues = string.Empty;
        int count = 0;
        //pnlFeeTable.Update();
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

    private string GetGRNNumbers()
    {
        string GRNNumber = "";
        string GRNValues = string.Empty;
        int count = 0;
        // pnlFeeTable.Update();
        foreach (ListItem item in ddlGRNNumber.Items)
        {
            if (item.Selected == true)
            {
                GRNNumber += item.Value + '$';
                count = 1;
            }
        }
        if (count > 0)
        {
            GRNValues = GRNNumber.Substring(0, GRNNumber.Length - 1);
            if (GRNValues != "")
            {
                string[] degValue = GRNValues.Split('$');

            }
        }
        return GRNValues;

    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        PnlItem.Visible = true;
        divAddItem.Visible = false;
        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
        divPO.Visible = false;
        divPONum.Visible = false;
        divGRNNum.Visible = false;
        divGrnNumtxt.Visible = false;
        pnlAttachmentList.Visible = false;
    }
    #region AddItem

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        try
        {
            SaveItemFlag = 1;
            lvItem.Visible = true;
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
            AddItemTableSavItem();
            Session["dtItem"] = dtItemTable;
            datarow = dtItemTable.NewRow();

            if (datarow != null)
            {
                maxVal = Convert.ToInt32(dtItemTable.AsEnumerable().Max(row => row["ITEM_SRNO"]));
            }
            datarow["ITEM_SRNO"] = maxVal + 1;
            datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);

            //if (IsItemExist(dtItemTable, ddlItem.SelectedItem.Text.Trim()))
            //{
            //    MessageBox("This Item Name Already Exist.");
            //    return;
            //}
            datarow["ITEM_NAME"] = ddlItem.SelectedItem.Text;
            datarow["PO_QTY"] = 0;
            datarow["RECEIVED_QTY"] = 0;
            datarow["INV_QTY"] = txtItemQty.Text == "" ? "0" : txtItemQty.Text;
            datarow["BAL_QTY"] = 0;
            datarow["RATE"] = 0;
            datarow["DISC_PER"] = 0;
            datarow["DISC_AMT"] = 0;
            datarow["TAXABLE_AMT"] = 0;
            datarow["TAX_AMT"] = 0;
            datarow["BILL_AMT"] = 0;
            datarow["ITEM_REMARK"] = txtItemRemark.Text;
            datarow["PORDNO"] = 0;
            datarow["GRNID"] = 0;
            datarow["IS_TAX"] = 0;
            datarow["IsTaxInclusive"] = 0;                 //30/12/2023
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


    // It is used to check duplicate Item name. 
    private bool IsItemExist(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_NAME"].ToString() == value)
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
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_GRN_Entry.IsItemExist()  -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }


    private void CalItemCount()
    {
        double ItemQtyCount = 0.0;
        for (int i = 0; i < dtItemTable.Rows.Count; i++)
        {
            ItemQtyCount += Convert.ToDouble(dtItemTable.Rows[i]["INV_QTY"].ToString());
        }
        divItemCount.Visible = true; //sHAIKH jUNED 11-11-2022
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
        dt.Columns.Add(new DataColumn("INV_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BAL_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_PER", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DISC_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAXABLE_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TAX_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BILL_AMT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ITEM_REMARK", typeof(string)));
        dt.Columns.Add(new DataColumn("PORDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("GRNID", typeof(int)));
        dt.Columns.Add(new DataColumn("IS_TAX", typeof(int)));
        dt.Columns.Add(new DataColumn("IsTaxInclusive", typeof(int)));          //30/12/2023
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        return dt;
    }
    private void ClearItem()
    {
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



    private void AddItemTableSavItem()
    {
        dtItemTable = this.CreateItemTable();

        datarow = null;
        foreach (ListViewItem i in lvItem.Items)
        {
            HiddenField hdnItemSrNo = i.FindControl("hdnItemSrNo") as HiddenField;
            HiddenField hdnItemno = i.FindControl("hdnItemno") as HiddenField;
            Label lblItemName = i.FindControl("lblItemName") as Label;
            TextBox lblInvoiceQty = i.FindControl("lblInvoiceQty") as TextBox;
            TextBox lblRate = i.FindControl("lblRate") as TextBox;
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;
            HiddenField hdnGrnId = i.FindControl("hdnGrnId") as HiddenField;
            HiddenField hdnIsTax = i.FindControl("hdnIsTax") as HiddenField;
            HiddenField hdnIsTaxInclusive = i.FindControl("hdnIsTaxInclusive") as HiddenField;          //30/12/2023
            HiddenField hdnTechSpec = i.FindControl("hdnTechSpec") as HiddenField;
            HiddenField hdnQualityQtySpec = i.FindControl("hdnQualityQtySpec") as HiddenField;
            HiddenField hdnOthItemRemark = i.FindControl("hdnOthItemRemark") as HiddenField;

            //Adde by gopal anthati to maintain the disable field values

            // TextBox lblDiscPer = i.FindControl("lblDiscPer") as TextBox;
            // TextBox lblDiscAmt = i.FindControl("lblDiscAmt") as TextBox;
            //TextBox lblTaxableAmt = i.FindControl("lblTaxableAmt") as TextBox;
            // TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            //TextBox lblBillAmt = i.FindControl("lblBillAmt") as TextBox;      
            // TextBox lblPOQty = i.FindControl("lblPOQty") as TextBox;
            // TextBox lblReceivedQty = i.FindControl("lblReceivedQty") as TextBox;
            // TextBox lblBalQty = i.FindControl("lblBalQty") as TextBox;
            HiddenField hdnItemPOQty = i.FindControl("hdnItemPOQty") as HiddenField;
            HiddenField hdnItemRecQty = i.FindControl("hdnItemRecQty") as HiddenField;
            HiddenField hdnItemBalQty = i.FindControl("hdnItemBalQty") as HiddenField;
            HiddenField hdnItemDiscPer = i.FindControl("hdnItemDiscPer") as HiddenField;
            HiddenField hdnItemDiscAmt = i.FindControl("hdnItemDiscAmt") as HiddenField;
            HiddenField hdnItemTaxableAmt = i.FindControl("hdnItemTaxableAmt") as HiddenField;
            HiddenField hdnItemTaxAmt = i.FindControl("hdnItemTaxAmt") as HiddenField;
            HiddenField hdnItemBillAmt = i.FindControl("hdnItemBillAmt") as HiddenField;

            //End////


            // modified by shabina 10-09-2021
            if (lblItemName.Text == ddlItem.SelectedItem.Text)
            {

                MessageBox("This Item Name Already Exist.");
                return;
            }


            datarow = dtItemTable.NewRow();
            datarow["ITEM_SRNO"] = hdnItemSrNo.Value;
            datarow["ITEM_NO"] = hdnItemno.Value;
            datarow["ITEM_NAME"] = lblItemName.Text;
            datarow["PO_QTY"] = hdnItemPOQty.Value == "" ? "0" : hdnItemPOQty.Value;
            datarow["RECEIVED_QTY"] = hdnItemRecQty.Value == "" ? "0" : hdnItemRecQty.Value;
            datarow["INV_QTY"] = lblInvoiceQty.Text == "" ? "0" : lblInvoiceQty.Text;
            datarow["BAL_QTY"] = hdnItemBalQty.Value == "" ? "0" : hdnItemBalQty.Value;
            datarow["RATE"] = lblRate.Text == "" ? "0" : lblRate.Text;
            datarow["DISC_PER"] = hdnItemDiscPer.Value == "" ? "0" : hdnItemDiscPer.Value;
            datarow["DISC_AMT"] = hdnItemDiscAmt.Value == "" ? "0" : hdnItemDiscAmt.Value;
            datarow["TAXABLE_AMT"] = hdnItemTaxableAmt.Value == "" ? "0" : hdnItemTaxableAmt.Value;
            datarow["TAX_AMT"] = hdnItemTaxAmt.Value == "" ? "0" : hdnItemTaxAmt.Value;
            datarow["BILL_AMT"] = hdnItemBillAmt.Value == "" ? "0" : hdnItemBillAmt.Value;
            datarow["ITEM_REMARK"] = hdnOthItemRemark.Value;
            datarow["PORDNO"] = hdnPordno.Value;
            datarow["GRNID"] = hdnGrnId.Value;
            datarow["IS_TAX"] = hdnIsTax.Value;
            
            datarow["IsTaxInclusive"] = hdnIsTaxInclusive.Value;                 // //30/12/2023
            datarow["TECH_SPEC"] = hdnTechSpec.Value;
            datarow["QUALITY_QTY_SPEC"] = hdnOthItemRemark.Value;

            dtItemTable.Rows.Add(datarow);

        }
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
            TextBox lblInvoiceQty = i.FindControl("lblInvoiceQty") as TextBox;
            TextBox lblRate = i.FindControl("lblRate") as TextBox;
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;
            HiddenField hdnGrnId = i.FindControl("hdnGrnId") as HiddenField;
            HiddenField hdnIsTax = i.FindControl("hdnIsTax") as HiddenField;
            HiddenField hdnIsTaxInclusive = i.FindControl("hdnIsTaxInclusive") as HiddenField;          //30/12/2023
            HiddenField hdnTechSpec = i.FindControl("hdnTechSpec") as HiddenField;
            HiddenField hdnQualityQtySpec = i.FindControl("hdnQualityQtySpec") as HiddenField;
            HiddenField hdnOthItemRemark = i.FindControl("hdnOthItemRemark") as HiddenField;


            //Adde by gopal anthati on 18/05/2022 to maintain the disable field values

            TextBox lblDiscPer = i.FindControl("lblDiscPer") as TextBox;
            TextBox lblDiscAmt = i.FindControl("lblDiscAmt") as TextBox;
            TextBox lblTaxableAmt = i.FindControl("lblTaxableAmt") as TextBox;
            TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            TextBox lblBillAmt = i.FindControl("lblBillAmt") as TextBox;
            TextBox lblPOQty = i.FindControl("lblPOQty") as TextBox;
            TextBox lblReceivedQty = i.FindControl("lblReceivedQty") as TextBox;
            TextBox lblBalQty = i.FindControl("lblBalQty") as TextBox;

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
                datarow["INV_QTY"] = lblInvoiceQty.Text == "" ? "0" : lblInvoiceQty.Text;
                datarow["BAL_QTY"] = hdnItemBalQty.Value == "" ? "0" : hdnItemBalQty.Value;
                datarow["RATE"] = lblRate.Text == "" ? "0" : lblRate.Text;
                datarow["DISC_PER"] = hdnItemDiscPer.Value == "" ? "0" : hdnItemDiscPer.Value;
                datarow["DISC_AMT"] = hdnItemDiscAmt.Value == "" ? "0" : hdnItemDiscAmt.Value;
                datarow["TAXABLE_AMT"] = hdnItemTaxableAmt.Value == "" ? "0" : hdnItemTaxableAmt.Value;
                datarow["TAX_AMT"] = hdnItemTaxAmt.Value == "" ? "0" : hdnItemTaxAmt.Value;
                datarow["BILL_AMT"] = hdnItemBillAmt.Value == "" ? "0" : hdnItemBillAmt.Value;
                // datarow["BILL_AMT"] = hdnBillAmt.Value == "" ? "0" : hdnBillAmt.Value; //(23/03/2022)
                datarow["ITEM_REMARK"] = hdnOthItemRemark.Value;
                datarow["PORDNO"] = hdnPordno.Value;
                datarow["GRNID"] = hdnGrnId.Value;
                datarow["IS_TAX"] = hdnIsTax.Value;
                datarow["IsTaxInclusive"] = hdnIsTaxInclusive.Value;                 // //30/12/2023
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

    protected void btnAddTax_Click(object sender, EventArgs e)
    {
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;

        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmount = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;

        lblTaxableAmt.Text = hdnTaxableAmt.Value;
        lblDiscAmt.Text = hdnDiscAmt.Value;
        lblTaxAmount.Text = hdnTaxAmt.Value;
        lblBillAmt.Text = hdnBillAmt.Value;

        if (Convert.ToString(ViewState["Action"]) == "edit")   //30/12/2023
        {
            string basiamt = hdnBasicAmt.Value;
            // lblTaxableAmt.Text = Convert.ToString(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value));
        }
    

        ViewState["ItemNo"] = null;
        if (ddlVendor.SelectedIndex == 0)
        {
            //MessageBox("Please Select Vendor.");
            // return;
        }
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);
        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
               // DataTable dtTaxNew = foundRow.CopyToDataTable();

                //----------------------------------------30/12/2023--added-----------------------//
                DataSet ds = null;
                int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + ddlVendor.SelectedValue));
                int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
                if (VendorState == CollegeState)
                {
                    //ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);     //30/12/2023
                    ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
                }
                else
                {
                    // ds = objGRNCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);  //30/12/2023
                    ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
                }
                //---------------------------------------------------------------------------//

               // lvTax.DataSource = dtTaxNew; 30/12/2023
                lvTax.DataSource = ds.Tables[0];
                lvTax.DataBind();
               // hdnListCount.Value = dtTaxNew.Rows.Count.ToString(); //30/12/2023
                 hdnListCount.Value= ds.Tables[0].Rows.Count.ToString();
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                //ViewState["TaxEdit"]="edit";
                CalTotTax();
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
            //ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
            // ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1); //30/12/2023
            ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            //ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);  // modified by shabina 10-09-2021
            //  ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0); //30/12/2023
            ds = objINVCon.GetTaxes(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
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
        //if (ViewState["TaxEdit"] == null)
        // {
 
        CalTotTax();
        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;
        HiddenField hdnItemTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("hdnItemTaxableAmt") as HiddenField;    //20/12/2023
        HiddenField hdnIsTaxInclusive = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("hdnIsTaxInclusive") as HiddenField;
        lblTaxableAmt.Text = hdnTaxableAmt.Value;
        lblTaxAmt.Text = txtTotTaxAmt.Text;
      //  lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnTaxableAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));
        lblDiscAmt.Text = hdnDiscAmt.Value;

        lblTaxableAmt.Text = hdnItemTaxableAmt.Value;        //30/12/2023
        hdnIsTaxInclusive.Value = hdnIsTaxInclusive.Value;     //30/12/2023

        if (hdnIsTaxInclusive.Value == "0")
        {
            lblTaxableAmt.Text = Convert.ToString(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value));

            // lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnTaxableAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));
            lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));

        }
        else
        {
            lblTaxableAmt.Text = Convert.ToString(Convert.ToDecimal(hdnBasicAmt.Value) - Convert.ToDecimal(hdnDiscAmt.Value) - Convert.ToDecimal(txtTotTaxAmt.Text));
          //  lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnItemTaxableAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));  //30/12/2023
            lblBillAmt.Text = Convert.ToString(Convert.ToDecimal(hdnTaxableAmt.Value) + Convert.ToDecimal(txtTotTaxAmt.Text));  //30/12/2023

        }

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
    private void GenerateInvoiceNumber()
    {
        DataSet ds = objINVCon.GetInvoiceNumber();
        txtInvoiceNumber.Text = ds.Tables[0].Rows[0]["INVNO"].ToString();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //DateTime.Now.ToString()
        DateTime getdate=Convert.ToDateTime(DateTime.Now.ToString());
        if (getdate  < Convert.ToDateTime(txtInvoiceDate.Text))
        {
            MessageBox("You can not create invoice for future date.");
            return;
        }

        AddItemTable();
        string PoNumber = GetPONumbers();
        string GRNNumber = GetGRNNumbers();

        if (dtItemTable == null || dtItemTable.Rows.Count == 0)
        {
            MessageBox("Please Add Item Details");
            return;
        }
         objINVEnt.GRN_NUM = GRNNumber == "" ? "0" : GRNNumber;
        
        objINVEnt.INVDATE = Convert.ToDateTime(txtInvoiceDate.Text);     
        if (txtGRNDate.Text != string.Empty && txtGRNDate.Text!="99/99/9999")     //Shaikh Juned (31/03/2022)
        {
            objINVEnt.GRNDATE = Convert.ToDateTime(txtGRNDate.Text);
        }
        else
        {
            objINVEnt.GRNDATE = DateTime.MinValue;
        }

        //------------------------------------------------//
        if (txtItemExpiryDate.Text != string.Empty && txtItemExpiryDate.Text!="99/99/9999")
        {
            objINVEnt.EXPIRYDATE = Convert.ToDateTime(txtItemExpiryDate.Text);
        }
        else
        {
            objINVEnt.EXPIRYDATE = DateTime.MinValue;
        }
        if (txtItemWarrentyDate.Text != string.Empty && txtItemWarrentyDate.Text!="99/99/9999")
        {
            objINVEnt.WARRANTYDATE = Convert.ToDateTime(txtItemWarrentyDate.Text);
        }
        else
        {
            objINVEnt.WARRANTYDATE = DateTime.MinValue;
        }

        if (txtItemWarrentyDate.Text != string.Empty && txtItemWarrentyDate.Text != "99/99/9999")
        {
            if (Convert.ToDateTime(txtItemWarrentyDate.Text) > Convert.ToDateTime(txtItemExpiryDate.Text))
            {
                MessageBox("Warranty Date Should not Be Greater Than  Expiry From Date ");
                return;
            }
        }

        //----------------------------------------------------//
        objINVEnt.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
        objINVEnt.MDNO = Convert.ToInt32(Session["strdeptcode"]);
        objINVEnt.REMARK = txtRemark.Text;
        objINVEnt.DMDATE = Convert.ToDateTime(txtDMDate.Text);
        objINVEnt.DMNO = txtDMNo.Text;
        objINVEnt.PORDNO = PoNumber == "" ? "0" : PoNumber;
        objINVEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objINVEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);
        objINVEnt.INVOICE_ITEM_TBL = dtItemTable;
        objINVEnt.INVOICE_TAX_TBL = ViewState["TaxTable"] as DataTable;
        NetAmountCalculation();  //--Shaikh Juned --11-11-2022  ---Add This method for Calculate Items Net Amount
        objINVEnt.NETAMOUNT = Convert.ToDecimal(ViewState["NetAmount"]);//--Shaikh Juned --11-11-2022 

        objINVEnt.INVOICE_UPLOAD_FILE_TBL = ViewState["INV1"] as DataTable;
        

        if (ViewState["Action"].ToString() == "Add")
        {
            GenerateInvoiceNumber();
            objINVEnt.INVNO = txtInvoiceNumber.Text;
            objINVEnt.INVTRNO = 0;
            CustomStatus cs = (CustomStatus)objINVCon.InsUpdateInvoiceEntry(objINVEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved & Invoice Number Generated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        else
        {
            objINVEnt.INVTRNO = Convert.ToInt32(ViewState["INVTRNO"]);
            objINVEnt.INVNO = txtInvoiceNumber.Text;
            CustomStatus cs = (CustomStatus)objINVCon.InsUpdateInvoiceEntry(objINVEnt);
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
        divInvNumber.Visible = true;
        btnSubmit.Enabled = false;
        btnAddNew2.Visible = true;
        btnCancel.Visible = false;
        ViewState["NetAmount"] = null;
    }
    protected void btnAdNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        divInvoiceEtry.Visible = true;
        lvInvoiceEntry.Visible = false;
        btnSubmit.Enabled = true;
        divAddItem.Visible = true;
        divGRNNum.Visible = true;
        divPO.Visible = true;
        btnAddNew2.Visible = false;
        btnCancel.Visible = true;
        lblNetAmtCount.Text = string.Empty;
        divItemCount.Visible = false;
    }

    private void ClearAll()
    {
        txtInvoiceDate.Text = DateTime.Now.ToString();
        txtGRNDate.Text = string.Empty;
        ddlVendor.SelectedIndex = 0;
        //ddlGRNNumber.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        lvItem.DataSource = null;
        lvItem.DataBind();
        lvItem.Visible = false;
        lvTax.DataSource = null;
        lvTax.DataBind();
        txtInvoiceNumber.Text = string.Empty;
        divInvNumber.Visible = false;
        lblItemCount.Text = string.Empty;
        lblItemQtyCount.Text = string.Empty;
        txtPONum.Text = string.Empty;
        divPONum.Visible = false;
        divPO.Visible = true;
        divGRNNum.Visible = true;
        btnAddNew.Visible = false;
        txtDMNo.Text = string.Empty;
        txtDMDate.Text = string.Empty;
        hdnOthEdit.Value = "0";
        PnlItem.Visible = false;
        divAddItem.Visible = true;
        //divItemCount.Visible = false;
        divGrnNumtxt.Visible = false;
        txtGrnNumbers.Text = string.Empty;
        divItemCount.Visible = false;


        ddlPO.Visible = true;
        FillDropDownList();
        ViewState["Items"] = null;
        Session["dtItem"] = null;
        ViewState["Action"] = "Add";
        ViewState["TaxTable"] = null;
        ViewState["SRNO_TAX"] = null;

        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        ViewState["INV1"] = null;
        ViewState["FILE1"] = null;
        txtItemExpiryDate.Text = string.Empty;
        txtItemWarrentyDate.Text = string.Empty;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        divInvoiceEtry.Visible = false;
        lvInvoiceEntry.Visible = true;
        btnAddNew.Visible = true;
        BindListView();
    }
    protected void ddlGRNNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlItem.Visible = false;  //012/05/2022
        divGRNNum.Visible = true;
        divAddItem.Visible = false;
        string GRNNumbers = GetGRNNumbers();
        if (ddlGRNNumber.SelectedValue == "")
        {
            ViewState["dtItem"] = null;
            lvItem.DataSource = null;
            lvItem.DataBind();
            divPO.Visible = true;
            divAddItem.Visible = true;
            divPONum.Visible = false;
            divGrnNumtxt.Visible = false;
            txtPONum.Text = string.Empty;
            txtGrnNumbers.Text = string.Empty;
            return;
        }
        DataSet ds = objINVCon.GetItemsByGRNID(GRNNumbers);
        if (ds.Tables[0].Rows.Count > 0)
        {
            dtItemTable = ds.Tables[0];

            lvItem.DataSource = ds.Tables[0];
            lvItem.DataBind();
            lvItem.Visible = true;
            if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["REFNO"].ToString() != "")
            {
                txtPONum.Text = ds.Tables[1].Rows[0]["REFNO"].ToString();
                divPONum.Visible = true;
            }
            else
            {
                divPONum.Visible = false;
            }
            if (ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows[0]["GRN_NUMBER"].ToString() != "")
            {
                txtGrnNumbers.Text = ds.Tables[2].Rows[0]["GRN_NUMBER"].ToString();
                divGrnNumtxt.Visible = true;
            }
            else
            {
                divGrnNumtxt.Visible = false;
            }
            ViewState["dtItem"] = dtItemTable;
            if (ViewState["dtItem"] != null || (DataTable)ViewState["dtItem"] != null)
            {
                AddItemTable();
                for (int i = 0; i < dtItemTable.Rows.Count; i++)
                {
                    dtItemTable.Rows[i]["ITEM_SRNO"] = i + 1;
                }
                ViewState["dtItem"] = dtItemTable;
                lvItem.DataSource = dtItemTable;
                lvItem.DataBind();
            }
            CalItemCount();
            ddlPO.Items.Clear();
            DataSet dsPO = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE_ITEM)", "PORDNO");
            for (int i = 0; i < dsPO.Tables[0].Rows.Count; i++)
            {
                ddlPO.Items.Add(new ListItem(Convert.ToString(dsPO.Tables[0].Rows[i][1]), Convert.ToString(dsPO.Tables[0].Rows[i][0])));
            }
            AddPONumber();
            divPO.Visible = false;
            divAddItem.Visible = false;
        }
        else
        {
            ViewState["dtItem"] = null;
            lvItem.DataSource = null;
            lvItem.DataBind();
            divPO.Visible = false;
            divAddItem.Visible = false;
        }
        //if (ddlGRNNumber.SelectedIndex == 0)
        //    divPO.Visible = true;
        //else
        //    divPO.Visible = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int INVTRNO = Convert.ToInt32(btnEdit.CommandArgument);
        ClearAll();
        ViewState["INVTRNO"] = INVTRNO;
        ViewState["Action"] = "edit";
        divInvoiceEtry.Visible = true;
        lvInvoiceEntry.Visible = false;
        btnSubmit.Enabled = true;
        ShowDetals(INVTRNO);
        btnAddNew2.Visible = true;
        btnCancel.Visible = true;
    }

    private void ShowDetals(int INVTRNO)
    {
        //objCommon.FillDropDownList(ddlGRNNumber, "STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "", "GRN_NUMBER");
        DataSet ds = objINVCon.GetInvoiceEntryDetailsForEdit(INVTRNO);
        txtInvoiceNumber.Text = ds.Tables[0].Rows[0]["INVNO"].ToString();
        divInvNumber.Visible = true;
        txtGRNDate.Text = ds.Tables[0].Rows[0]["GRNDATE"].ToString();
        txtInvoiceDate.Text = ds.Tables[0].Rows[0]["INVDATE"].ToString();
        //ddlGRNNumber.SelectedValue = ds.Tables[0].Rows[0]["GRNID"].ToString();
        ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["PNO"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        txtDMDate.Text = ds.Tables[0].Rows[0]["DMDATE"].ToString();
        txtDMNo.Text = ds.Tables[0].Rows[0]["DMNO"].ToString();
        lblNetAmtCount.Text = ds.Tables[0].Rows[0]["NETAMT"].ToString();

        txtItemExpiryDate.Text = ds.Tables[0].Rows[0]["EXPIRYDATE"].ToString();
        txtItemWarrentyDate.Text = ds.Tables[0].Rows[0]["WARRANTYDATE"].ToString();

        if (ds.Tables[0].Rows[0]["PORDNO"].ToString() != "0")
        {
            string[] PONum = ds.Tables[0].Rows[0]["PORDNO"].ToString().Split("$".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string PO in PONum)
            {
                if (ddlPO.Items.FindByValue(PO) != null)
                {
                    ddlPO.Items.FindByValue(PO).Selected = true;
                    divPO.Visible = true;
                    divPONum.Visible = true;
                    divGrnNumtxt.Visible = false;
                    divGRNNum.Visible = false;
                    divAddItem.Visible = false;
                }
            }
        }
        else if (ds.Tables[0].Rows[0]["GRNID"].ToString() != "0") // || ds.Tables[0].Rows[0]["GRNID"].ToString() != null)
        {
            string[] GRNNum = ds.Tables[0].Rows[0]["GRNID"].ToString().Split("$".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string GRN in GRNNum)
            {
                if (ddlGRNNumber.Items.FindByValue(GRN) != null)
                {
                    ddlGRNNumber.Items.FindByValue(GRN).Selected = true;
                    divPO.Visible = false;
                    divPONum.Visible = false;
                    divGrnNumtxt.Visible = true;
                    divGRNNum.Visible = true;
                    divAddItem.Visible = false;



                }
            }
        }
        if (ds.Tables[1].Rows[0]["GRNID"].ToString() == "0" || ds.Tables[0].Rows[0]["GRNID"].ToString() == null)
        {
            divGRNNum.Visible = false;
            divPO.Visible = false;

        }
        else if (ds.Tables[1].Rows[0]["PORDNO"].ToString() == "0" || ds.Tables[0].Rows[0]["PORDNO"].ToString() == null)
        {
            divGRNNum.Visible = false;
            divPO.Visible = false;
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
            if (Convert.ToString(ds.Tables[1].Rows[0]["IsTaxInclusive"]) == "1")   //30/12/2023
            {
                chkTaxInclusive.Checked = true;
            }
            else
            {
                chkTaxInclusive.Checked = false;
            }
        }
        else
        {
            lvItem.DataSource = null;
            lvItem.DataBind();
           // divlvItem.Visible = false;
            dtItemTable = null;
            ViewState["dtItem"] = null;
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            hdnListCount.Value = ds.Tables[2].Rows.Count.ToString();
            ViewState["TaxTable"] = ds.Tables[2];
        }
       // if (ds.Tables[3].Rows[0]["GRN_NUMBER"].ToString() == "")
        if (ds.Tables[1].Rows[0]["GRNID"].ToString() == "0")
        {
            divGrnNumtxt.Visible = false;

        }
        else
        {
            txtGrnNumbers.Text = ds.Tables[3].Rows[0]["GRN_NUMBER"].ToString();
            divGrnNumtxt.Visible = true;

        }
        if (ds.Tables[4].Rows.Count > 0)
        {
            txtPONum.Text = ds.Tables[4].Rows[0]["REFNO"].ToString();
            if (txtPONum.Text == "")
            {
                divPONum.Visible = false;
            }
            else
            {
                divPONum.Visible = true;
            }
        }
        else
        {
            divPONum.Visible = false;
        }

        if (ds.Tables[5].Rows.Count > 0)
        {
            lvCompAttach.DataSource = ds.Tables[5];
            lvCompAttach.DataBind();
            ViewState["INV1"] = ds.Tables[5];
            pnlAttachmentList.Visible = true;
        }
        else
        {
            pnlAttachmentList.Visible = false;
        }

    }
    protected void btnAddOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        TextBox lblDiscAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblDiscAmt") as TextBox;
        lblDiscAmt.Text = hdnDiscAmt.Value;

        TextBox lblTaxableAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxableAmt") as TextBox;
        TextBox lblTaxAmount = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblTaxAmount") as TextBox;
        TextBox lblBillAmt = lvItem.Items[Convert.ToInt32(hdnIndex.Value)].FindControl("lblBillAmt") as TextBox;

        lblTaxableAmt.Text = hdnTaxableAmt.Value;       
        lblTaxAmount.Text = hdnTaxAmt.Value;
        lblBillAmt.Text = hdnBillAmt.Value;

        this.MdlTax.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        if (ViewState["Action"].ToString() == "edit" && hdnOthEdit.Value == "0")
        {
            ImageButton btn = sender as ImageButton;
            int ItemNo = Convert.ToInt32(btn.CommandArgument);
            DataSet ds = objCommon.FillDropDown("STORE_INVOICE_ITEM", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "INVTRNO=" + Convert.ToInt32(ViewState["INVTRNO"]) + " AND ITEM_NO=" + ItemNo, "");
            txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
            txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
            txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();
        }
    }

    protected void btnSaveOthInfo_Click(object sender, EventArgs e)
    {
        this.MdlTax.Hide();
        txtItemRemarkOth.Text = string.Empty;
        txtQualityQtySpec.Text = string.Empty;
        txtTechSpec.Text = string.Empty;

    }

    //--Shaikh Juned --11-11-2022  ---Add This method for Calculate Items Net Amount ---Start
    private void NetAmountCalculation()
    {
        decimal netAmount = 0;

        foreach (ListViewItem item in lvItem.Items)
        {
            TextBox txtbillamount = item.FindControl("lblBillAmt") as TextBox;

            netAmount += Convert.ToDecimal(txtbillamount.Text);
            
        }

        lblNetAmtCount.Text = String.Format("{0:0}", netAmount);

        ViewState["NetAmount"] = lblNetAmtCount.Text;
    }
    //--Shaikh Juned --11-11-2022  ---Add This method for Calculate Items Net Amount ---end
    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            // add for multiple document attachment
          //  int idno = _idnoEmp;
            ServiceBook objSevBook = new ServiceBook();
            if (Uploadinvoice.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(Uploadinvoice.FileName)))
                {
                    if (Uploadinvoice.HasFile)
                    {
                        if (Uploadinvoice.FileContent.Length >= 1024 * 10000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 10 Mb");
                            Uploadinvoice.Dispose();
                            Uploadinvoice.Focus();
                            return;
                        }
                    }

                    string FileName = Uploadinvoice.FileName;
                    if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["FILE1"];
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (dtM.Rows[i]["DisplayFileName"].ToString() == FileName)
                            {
                                MessageBox("File Already Exist!");
                                return;
                            }
                        }
                    }
                    int inv_id = 0;
                    if (ViewState["INVTRNO"] == null)
                    {
                        inv_id = Convert.ToInt32(objCommon.LookUp("STORE_INVOICE", "isnull(MAX(INVTRNO)+1,0) INVTRNO", ""));
                    }
                    else
                    {
                        inv_id = Convert.ToInt32(ViewState["INVTRNO"]);
                    }
                    string file = Docpath + "TEMP_CONDUCTTRAINING_FILES\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "TRAINING_CONDUCTED\\";
                    ViewState["DESTINATION_PATH"] = PATH;
                    if (lblBlobConnectiontring.Text == "")
                    {
                        objSevBook.ISBLOB = 0;
                    }
                    else
                    {
                        objSevBook.ISBLOB = 1;
                    }
                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                       // string IdNo = _idnoEmp.ToString();
                        if (Uploadinvoice.HasFile)
                        {
                            string contentType = contentType = Uploadinvoice.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(Uploadinvoice.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            //string name = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename =   inv_id+"_INVOICE_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;

                            if (Uploadinvoice.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, inv_id + "_INVOICE_" + time, Uploadinvoice);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    int tano = Addfieldstotbl(filename);
                                    //BindListView_Attachments();
                                }
                            }
                        }
                    }
                    else
                    {
                        string filename = Uploadinvoice.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                int tano = Addfieldstotbl(filename);
                                path = file + "\\TC_" + tano + System.IO.Path.GetExtension(Uploadinvoice.PostedFile.FileName);
                                Uploadinvoice.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    Uploadinvoice.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        //dc = new DataColumn("SRNO", typeof(int));
        //dt.Columns.Add(dc);

        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);


        dc = new DataColumn("FILENAME", typeof(string));
        dt.Columns.Add(dc);

        ViewState["INV1"] = dt;
    }

    private int Addfieldstotbl(string filename)
    {
        if (ViewState["INV1"] != null && ((DataTable)ViewState["INV1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["INV1"];
            DataRow dr = dt.NewRow();
            //int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            //dr["SRNO"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["DisplayFileName"] = Uploadinvoice.FileName;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            this.BindListView_Attachments(dt);
           // ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["INV1"];
            DataRow dr = dt.NewRow();
            //int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            //dr["SRNO"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["DisplayFileName"] = Uploadinvoice.FileName;
            dr["FILENAME"] = filename;
           // ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            pnlAttachmentList.Visible = true;
            this.BindListView_Attachments(dt);
        }
        return Convert.ToInt32(ViewState["FUID"]);
    }
    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();
            pnlAttachmentList.Visible = true;

            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {

                Control ctrHeader = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["FILENAME"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return datRow;
    }

    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
  
            if (ViewState["INV1"] != null && ((DataTable)ViewState["INV1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["INV1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                ViewState["INV1"] = dt;
                lvCompAttach.DataSource = dt;
                lvCompAttach.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        pnlReport.Visible = true;
        InvPanel.Visible = false;
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("INVOICE_REPORT", "Str_InvoiceFormat2.rpt");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pnlReport.Visible = false;
        InvPanel.Visible = true;
    }
    //To Show INVOICE report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_INVTRNO=" + Convert.ToInt32(ddlInv.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

