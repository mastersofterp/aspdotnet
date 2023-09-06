﻿using System;
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class Health_StockMaintenance_InvoiceEntry : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StockMaster objStock = new StockMaster();
    StockMaintnance objSController = new StockMaintnance();
    //Temporary Datatable For Items
    DataTable DtItems;
    static double taxAmt;
    static double GrossAmt;
    static double NetAmt;
    static double taxAmount;
    static double ItmQty;
    List<Health_Invoice_Item> ListInvItem;
    Health_Invoice_Item EditItem;
    int ITEMNO;
    int INVNO;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
                Session["usertype"] == null || Session["userfullname"] == null )
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               // this.bindInvoiceNo();                   
                this.BindCurrentDate();
                this.bindlistview();
                objCommon.FillDropDownList(ddlVendor, "HEALTH_PARTY P,HEALTH_PARTY_CATEGORY PC ,ACD_CITY C", "PNO", "PNAME+'['+PCODE+'] '", "P.PCNO=PC.PCNO AND C.CITYNO=P.CITYNO", "");
               // objCommon.FillDropDownList(ddlItem, "HEALTH_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
               
                InitailizeTmpTable();              
                GrossAmt = 0;
                NetAmt = 0;
                ItmQty = 0;
                ListInvItem = new List<Health_Invoice_Item>();               
                ViewState["GrossAmt"] = GrossAmt;
                Session["NetAmt"] = NetAmt;
                ViewState["calaction"] = "False";             
                Session["listitem"] = ListInvItem;
                ViewState["Itemaction"] = "add";

                Session["RecTbl"] = null;
                ViewState["SRNO"] = null;
                ViewState["actionADD"] = "add";
            }
            ViewState["action"] = "add";
        }
        txtAMT.Attributes.Add("readonly", "readonly");
        divMsg.InnerHtml = string.Empty;
    }

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


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select ITEM_NO, ITEM_NAME AS ITEM_NAME from HEALTH_ITEM where ITEM_NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "---------*" + sdr["ITEM_NAME"].ToString());
                    }
                }
                conn.Close();
                return ItemsName;
            }
        }
    }        


    void bindInvoiceNo()
    {
        DataSet ds = null;
        //int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objSController.GenrateInvoiceNo(0);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"].ToString());
            txtInvoiceNo.ReadOnly = true;
            string inv_no = objCommon.LookUp("HEALTH_INVOICE", "COUNT(1)+1", "");
            HdfInvoicNo.Value = inv_no.ToString();
        }
        objCommon.FillDropDownList(ddlInv, "HEALTH_INVOICE", "INVTRNO", "INVNO", "", "INVTRNO DESC");
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        bindlistview();
    }
   
    void bindlistview()
    {
        DataSet ds = null;
        ds = objSController.GetAllInvoice();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvGazette.DataSource = ds;
            lvGazette.DataBind();
        }
        else
        {
            lvGazette.DataSource = null;
            lvGazette.DataBind();
        }
       
    }    
    //Initilize Date Fields
    void BindCurrentDate()
    {
        txtDMDt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtInvDt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtRecDt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtPODt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
      
    }      
   
    //Calcultae Total Amount When Rate Changed.
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtAMT.Text = (Convert.ToInt32(txtTotQty.Text) * Convert.ToDouble(txtRate.Text)).ToString();
        }
        catch (Exception ex)
        {
            DisplayMessage("Enter Valid Values For Rate Or Quantity");
        }
    }
    //Initialize Temporary Table
    void InitailizeTmpTable()
    {
        DataSet ds = objSController.GetAllItemsByInvoice(0);
        DtItems = ds.Tables[0];
        DataColumn[] PrimaryColumns = { DtItems.Columns["ITEM_NO"] };
        DtItems.PrimaryKey = PrimaryColumns;
        Session["dtitems"] = DtItems;
    }

    //Add Item To Temporory Item Table.
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    // ModifyItemInvTable(Convert.ToInt32(hfItemName.Value));
    //    //ModifyItemInvTable(Convert.ToInt32(ViewState["ddlInvoice"].ToString()));
    //    ModifyItemInvTable(Convert.ToInt32(ViewState["INVNO"]));
    //    if (!CheckItemInListWhileEdit(Convert.ToInt32(hfItemName.Value)))
    //    {
    //        ViewState["calaction"] = "False";
    //        this.GetItemsForInvoive();

    //        DtItems = (DataTable)Session["dtitems"];
    //        DataRow dr = DtItems.NewRow();
    //        dr["ITEM_NO"] = hfItemName.Value;
    //        dr["QTY"] = txtQty.Text;
    //        dr["TOTQTY"] = txtTotQty.Text;
    //        dr["RATE"] = txtRate.Text;
    //        dr["AMT"] = txtAMT.Text;
    //        dr["ITEM_NAME"] = txtItemName.Text;
    //        dr["BATCH_NO"] = txtBatchNo.Text;
    //        dr["EXPIRY_DATE"] = txtExpiryDate.Text;
    //        dr["MFG_DATE"] = txtMFGDate.Text;
    //        dr["UNIT"] = txtUnit.Text;
    //        if (!DtItems.Rows.Contains(Convert.ToInt32(hfItemName.Value)))
    //        {
    //            DtItems.Rows.Add(dr);
    //            CalculateGross();
    //            CalculateNet();
    //            CalculateTotQty();
    //        }
    //        else
    //        {
    //            this.DisplayMessage("Item Already Selected For Invoice");
    //        }
    //        grdItemDet.DataSource = DtItems;
    //        grdItemDet.DataBind();
    //        txtAMT.Text = string.Empty;
    //        txtRate.Text = "0";
    //        txtQty.Text = "0";
    //        txtTotQty.Text = "0";
    //        txtItemName.Text = string.Empty;
    //        ViewState["Itemaction"] = "add";
    //        txtBatchNo.Text = string.Empty;
    //        txtExpiryDate.Text = string.Empty;
    //        txtMFGDate.Text = string.Empty;
    //        txtUnit.Text = "0";
    //    }
    //    else
    //    {
    //        this.DisplayMessage("Same Item Is Present In List ! Try Another");
    //    }

    //}

    void DisplayMessage(string Message)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    //Calculate Button Click
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        CalculateExtraCharges();
    }
    //Calculate ExtraChrges Or Discount.
    void CalculateExtraCharges()
    {       
      
        GrossAmt = (double)ViewState["GrossAmt"];      
        NetAmt = GrossAmt;
        if (Convert.ToDouble(txtEPer.Text) > 0)
        {
            txtEAmt.Text = (NetAmt * (Convert.ToDouble(txtEPer.Text) / 100)).ToString();
        }
        if (Convert.ToDouble(txtEPer1.Text) > 0)
        {
            txtEAmt1.Text = (NetAmt * (Convert.ToDouble(txtEPer1.Text) / 100)).ToString();
        }
        if (Convert.ToDouble(txtEPer2.Text) > 0)
        {
            txtEAmt2.Text = (NetAmt * (Convert.ToDouble(txtEPer2.Text) / 100)).ToString();
        }
        if (Convert.ToDouble(txtEPer3.Text) > 0)
        {
            txtEAmt3.Text = (NetAmt * (Convert.ToDouble(txtEPer3.Text) / 100)).ToString();
        }
        double tmpNetAmt = NetAmt;
        if (txtEcharge.Text.Trim() != string.Empty)
        {
            if (chkDiscount.Checked == true)
            {
                tmpNetAmt = tmpNetAmt - Convert.ToDouble(txtEAmt.Text);
            }
            else
            {
                tmpNetAmt = tmpNetAmt + Convert.ToDouble(txtEAmt.Text);
            }
        }
        else
        {
            txtEPer.Text = "0";
            txtEAmt.Text = "0";
        }
        if (txtEcharge1.Text.Trim() != string.Empty)
        {
            if (chkDiscount1.Checked == true)
            {
                tmpNetAmt = tmpNetAmt - Convert.ToDouble(txtEAmt1.Text);
            }
            else
            {
                tmpNetAmt = tmpNetAmt + Convert.ToDouble(txtEAmt1.Text);
            }
        }
        else
        {
            txtEPer1.Text = "0";
            txtEAmt1.Text = "0";
        }
        if (txtEcharge2.Text.Trim() != string.Empty)
        {
            if (chkDiscount2.Checked == true)
            {
                tmpNetAmt = tmpNetAmt - Convert.ToDouble(txtEAmt2.Text);
            }
            else
            {
                tmpNetAmt = tmpNetAmt + Convert.ToDouble(txtEAmt2.Text);
            }
        }
        else
        {
            txtEPer2.Text = "0";
            txtEAmt2.Text = "0";
        }
        if (txtEcharge3.Text.Trim() != string.Empty)
        {
            if (chkDiscount3.Checked == true)
                tmpNetAmt = tmpNetAmt - Convert.ToDouble(txtEAmt3.Text);
            else
                tmpNetAmt = tmpNetAmt + Convert.ToDouble(txtEAmt3.Text);
        }
        else
        {
            txtEPer3.Text = "0";
            txtEAmt3.Text = "0";
        }
        NetAmt = tmpNetAmt;
        Session["NetAmt"] = NetAmt;
        lblNetAmt.Text = tmpNetAmt.ToString();
        ViewState["calaction"] = "True";
    }

    //Save All Transaction.

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CalculateGross();       
            CalculateNet();
            CalculateExtraCharges();
            if (ViewState["calaction"].Equals("True"))
            {
                CustomStatus cs = (CustomStatus)SaveInvoice();
                if (cs == CustomStatus.RecordSaved)
                {
                  objCommon.DisplayMessage("Invoice Saved Successfully.",this.Page);
                  this.bindlistview();
                }
                if (cs == CustomStatus.RecordUpdated)
                {
                  objCommon.DisplayMessage("Invoice Updated Successfully.",this.Page);
                  this.bindlistview();
                }
                this.Clear();
            }
            else
            {
                DisplayMessage("Please Press Calculate Button Before Saving.");
            }
        }
        catch (Exception Ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_StockMaintenance_InvoiceEntry->btnSave_Click" + Ex.Message);
        }
    }

    //Save Invoice
    int SaveInvoice()
    {
        StockMaster objStock = new StockMaster();

        objStock.INVOICE_DATE = Convert.ToDateTime(txtInvDt.Text);
        objStock.D_M_NO = txtDMNo.Text;
        objStock.D_M_DATE = Convert.ToDateTime(txtDMDt.Text);
        objStock.VENDOR_NO = Convert.ToInt32(ddlVendor.SelectedValue);      
        objStock.RECEIVE_DATE = Convert.ToDateTime(txtRecDt.Text);
        objStock.PURCHASE_ORDER_NO = txtPONo.Text;
        objStock.PURCHASE_ORDER_DATE = Convert.ToDateTime(txtPODt.Text); 
     
        objStock.GRAMT = Convert.ToDouble(ViewState["GrossAmt"].ToString());

        objStock.FLAG1 = (chkDiscount.Checked) ? true: false;
        objStock.FLAG2 = (chkDiscount1.Checked) ? true : false;
        objStock.FLAG3 = (chkDiscount2.Checked) ? true : false;
        objStock.FLAG4 = (chkDiscount3.Checked) ? true : false;
        
        objStock.ECHG1 = txtEcharge.Text;
        objStock.ECHG2 = txtEcharge1.Text;
        objStock.ECHG3 = txtEcharge2.Text;
        objStock.ECHG4 = txtEcharge3.Text;
        objStock.EP1 = Convert.ToDouble(txtEPer.Text);
        objStock.EP2 = Convert.ToDouble(txtEPer1.Text);
        objStock.EP3 = Convert.ToDouble(txtEPer2.Text);
        objStock.EP4 = Convert.ToDouble(txtEPer3.Text);
        objStock.EAMT1 = Convert.ToDouble(txtEAmt.Text);
        objStock.EAMT2 = Convert.ToDouble(txtEAmt1.Text);
        objStock.EAMT3 = Convert.ToDouble(txtEAmt2.Text);
        objStock.EAMT4 = Convert.ToDouble(txtEAmt3.Text);
        objStock.ITEMTOTQTY = Convert.ToInt32(ViewState["TOTQTY"].ToString());
        objStock.NETAMT = Convert.ToDouble(Session["NetAmt"].ToString());
        objStock.REMARK = txtRemark.Text;


        DataTable dt;
        dt = (DataTable)Session["RecTbl"];
        objStock.ITEMLIST = dt;
   
      //  ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
        if (ViewState["action"].Equals("add"))
        {
            objStock.INVOICE_NO = txtInvoiceNo.Text;
            return objSController.SaveInovoiceEntry(objStock,  Session["colcode"].ToString(), "N");
        }
        else
        {
            //objStock.INVTRNO = Convert.ToInt32(ddlInvoice.SelectedValue);
            objStock.INVOICE_NO = txtInvoiceNo.Text;
            objStock.INVTRNO = Convert.ToInt32(ViewState["INVNO"]);
            return objSController.SaveInovoiceEntry(objStock, Session["colcode"].ToString(), "N");
        }
    }  

   
    //Add Items To ListItem.
    void AddItemToList(Health_Invoice_Item InvItem)
    {
        ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];      
                
        ListInvItem.Add(InvItem);
        Session["listitem"] = ListInvItem;
    }
    //Calculate total item quantity
    void CalculateTotQty() 
    {
        double tmpitmQty = 0;
        //ListInvItem = (List<Health_Invoice_Item>)Session["listItem"];
        //foreach (Health_Invoice_Item _Item in ListInvItem)
        //{
        //    tmpitmQty += _Item.TOTQTY;
        //}
        DataTable dt = (DataTable)Session["RecTbl"];
        foreach (DataRow dr in dt.Rows)
        {
            tmpitmQty += Convert.ToDouble(dr["TOT_QTY"].ToString());
        }
        ItmQty = tmpitmQty;
        ViewState["TOTQTY"] = ItmQty;
        lblItemQty.Text = ItmQty.ToString();
    }    
    //Calculate Gross Amount
    void CalculateGross()
    {
        //double tmpgrsAmt = 0;
        //ListInvItem = (List<Health_Invoice_Item>)Session["listItem"];

        //foreach (Health_Invoice_Item _Item in ListInvItem)
        //{
        //    tmpgrsAmt += _Item.AMT;
        //}
        //GrossAmt = tmpgrsAmt;
        //ViewState["GrossAmt"] = GrossAmt;
        //lblGrossAmt.Text = GrossAmt.ToString();


        double tmpgrsAmt = 0;
        DataTable dt = (DataTable)Session["RecTbl"];      
        foreach (DataRow dr in dt.Rows)
        {
            tmpgrsAmt +=  Convert.ToDouble(dr["AMOUNT"].ToString());
        }
        GrossAmt = tmpgrsAmt;
        ViewState["GrossAmt"] = GrossAmt;
        lblGrossAmt.Text = GrossAmt.ToString();

    }

    //Calculate NetAmount
    void CalculateNet()
    {
        NetAmt = (double)Session["NetAmt"];
        GrossAmt = (double)ViewState["GrossAmt"];       
        NetAmt = GrossAmt;
        lblNetAmt.Text = NetAmt.ToString();
        Session["NetAmt"] = NetAmt;
    }

    //Get Selected Items From List
    void GetItemsForInvoive()
    {
        if (ViewState["Itemaction"].Equals("edit"))
        {
            EditItem = (Health_Invoice_Item)Session["edititem"];
            int itemno = EditItem.ITEM_NO;          
            ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
            if (ListInvItem.Contains(EditItem))
                ListInvItem.Remove(EditItem);
        }
        Health_Invoice_Item InvItem = new Health_Invoice_Item();
        InvItem.ITEM_NO = Convert.ToInt32(hfItemName.Value);
        InvItem.QTY = Convert.ToDouble(txtQty.Text);
        InvItem.TOTQTY = Convert.ToDouble(txtTotQty.Text);
        InvItem.RATE = Convert.ToDouble(txtRate.Text);
        InvItem.AMT = Convert.ToDouble(txtAMT.Text);
        InvItem.BATCH_NO = txtBatchNo.Text;       
        InvItem.EXPIRY_DATE = txtExpiryDate.Text.Trim();     
        InvItem.MFG_DATE = txtExpiryDate.Text.Trim();
        InvItem.UNIT = Convert.ToDouble(txtUnit.Text);
        if (ViewState["action"].Equals("add"))
        {
            InvItem.INVTRNO = objSController.GetInVoiceTRNO();
        }
        else
        {           
            InvItem.INVTRNO = Convert.ToInt32(ViewState["INVNO"]);
        }
        this.AddItemToList(InvItem);
    }

    //Clear All Items.
    void Clear()
    {
        InitailizeTmpTable();
        txtInvoiceNo.Visible = true;
        ddlInvoice.Visible = false;
        lvItem.DataSource = null;
        lvItem.DataBind();      
        txtInvoiceNo.Text = string.Empty;
        BindCurrentDate();
        txtPONo.Text = string.Empty;
        lblGrossAmt.Text = string.Empty;
        lblItemQty.Text = string.Empty;
        lblNetAmt.Text = string.Empty;
        //lblTaxAmt.Text = string.Empty;
        txtTotQty.Text = "0";
        txtQty.Text = "0";
        txtRate.Text = "0";
        txtUnit.Text = "0";
        txtTotQty.Text = "0";
        txtBatchNo.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        txtMFGDate.Text = string.Empty;

        txtRemark.Text = string.Empty;
        txtAMT.Text = string.Empty;
        txtEcharge.Text = string.Empty;
        txtEcharge1.Text = string.Empty;
        txtEcharge2.Text = string.Empty;
        txtEcharge3.Text = string.Empty;
        txtDMNo.Text = string.Empty;

        ddlVendor.SelectedValue = "0";
        txtEPer.Text = "0";
        txtEPer1.Text = "0";
        txtEPer2.Text = "0";
        txtEPer3.Text = "0";
        txtEAmt.Text = "0";
        txtEAmt1.Text = "0";
        txtEAmt2.Text = "0";
        txtEAmt3.Text = "0";

        chkDiscount.Checked = false;
        chkDiscount1.Checked = false;
        chkDiscount2.Checked = false;
        chkDiscount3.Checked = false;


        txtItemName.Text = string.Empty;
        hfItemName.Value = "0";

        this.ClearList();

        taxAmt = 0;
        GrossAmt = 0;
        NetAmt = 0;
     
        ViewState["GrossAmt"] = GrossAmt; 
        Session["NetAmt"] = NetAmt;
        ViewState["calaction"] = "False";      
        this.InitailizeTmpTable();
    
        ViewState["action"] = "add";
        //pnlPo.Visible = false;
        //this.bindInvoiceNo();       
        txtPONo.Enabled = true;

        ViewState["INVNO"] = null;
        Session["RecTbl"] = null;
        lvItem.Visible = false;
        ViewState["actionADD"] = "add";
        ViewState["SRNO"] = null;
       
    }

    //Edit Item.

    //protected void grdItemDet_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    ViewState["Itemaction"] = "edit";
    //    ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
    //    ITEMNO = (int)grdItemDet.DataKeys[e.NewSelectedIndex].Values[0];

    //    ViewState["ITEM"] = ITEMNO.ToString();

     
    //    INVNO = Convert.ToInt32(ViewState["ddlInvoice"].ToString());
    //    //Select Item From List.
    //    var items = from Sitem in ListInvItem where Sitem.ITEM_NO == ITEMNO select Sitem;
    //    foreach (var item in items)
    //    {
    //        hfItemName.Value = item.ITEM_NO.ToString();
    //        txtItemName.Text = item.ITEM_NAME.ToString();
    //        txtUnit.Text = item.UNIT.ToString();
    //        txtQty.Text = item.QTY.ToString();
    //        txtTotQty.Text = item.TOTQTY.ToString();
    //        txtRate.Text = item.RATE.ToString();
    //        txtAMT.Text = item.AMT.ToString();
    //        txtRate.Enabled = true;
    //        txtBatchNo.Text = item.BATCH_NO.ToString();
    //        txtExpiryDate.Text = item.EXPIRY_DATE.ToString();
    //        txtMFGDate.Text = item.MFG_DATE.ToString();
    //        Session["edititem"] = item;
    //    }
        
    //    ModifyItemInvTable(INVNO);

    //}

    protected void bnCancel_Click(object sender, EventArgs e)
    {
        ViewState["Itemaction"] = "add";
        txtAMT.Text = "0";
        txtQty.Text = "0";
        txtRate.Text = "0";
        txtUnit.Text = "0";
        txtItemName.Text = string.Empty;
        hfItemName.Value = "0";
        txtBatchNo.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        txtMFGDate.Text = string.Empty;
        txtTotQty.Text = "0";     
    }

    //Modify InvItem
    void ModifyItemInvTable(int ITEM_NO)
    {
        if (ViewState["Itemaction"].Equals("edit"))
        {
            DataSet ds = objSController.GetAllItemsByInvoice(ITEM_NO);
            DtItems = ds.Tables[0];
            DataColumn[] PrimaryColumns = { DtItems.Columns["ITEM_NO"] };
            DtItems.PrimaryKey = PrimaryColumns;
            Session["dtitems"] = DtItems;
            
            //int ITEM = Convert.ToInt32(ds.Tables[0].Rows[0]["ITEM_NO"]); ViewState["ITEM"]
            int ITEM = Convert.ToInt32(ViewState["ITEM"].ToString());
            DtItems = (DataTable)Session["dtitems"];
            //DataRow dr = DtItems.Rows.Find(ITEM_NO);
            DataRow dr = DtItems.Rows.Find(ITEM);
            if (dr != null)
            {
                DtItems.Rows.Remove(dr);
            }
            lvItem.DataSource = DtItems;
            lvItem.DataBind();
            Session["dtitems"] = DtItems;
        }
       
    }  

    //Modify Button Click.
    protected void btnModify_Click(object sender, EventArgs e)
    {
        this.Clear();
        this.ClearList();
        ddlInvoice.Visible = true;
        txtInvoiceNo.Visible = false;
        objCommon.FillDropDownList(ddlInvoice, "HEALTH_INVOICE", "INVTRNO", "INVNO", "", "INVTRNO DESC");  
        ViewState["action"] = "edit";
    }

    //Show Invoice Details.
    void ShowInvoiceDetails(int INVTRNO)
    {
        DtItems = (DataTable)Session["dtitems"];        
        Session["dtitems"] = DtItems;      
        ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
        ListInvItem.Clear();       
        DataTable dtInvoice = objSController.GetInvoiceByNo(INVTRNO).Tables[0];
        DataRow dr = dtInvoice.Rows[0];
        //txtInvoiceNo.Text = Convert.ToString(ddlInvoice.SelectedItem);
        txtInvoiceNo.Text = dr["INVNO"].ToString();
        txtPODt.Text = Convert.ToDateTime(dr["PODT"]).ToString("dd/MM/yyyy");
        txtPONo.Enabled = false;
        //txtInvoiceNo.Enabled = false;
        txtInvDt.Text = Convert.ToDateTime(dr["INVDT"]).ToString("dd/MM/yyyy");
        txtRecDt.Text = Convert.ToDateTime(dr["RECDT"]).ToString("dd/MM/yyyy");
        txtDMDt.Text = Convert.ToDateTime(dr["DMDT"]).ToString("dd/MM/yyyy");       
       
       ddlVendor.SelectedValue = dr["PNO"].ToString();
     
        if (dr["DMNO"] != null)
        {
            txtDMNo.Text = dr["DMNO"].ToString();
        }
        txtPONo.Text = dr["PORDNO"].ToString();

        txtEcharge.Text = dr["ECHG1"].ToString();
        txtEcharge1.Text = dr["ECHG2"].ToString();
        txtEcharge2.Text = dr["ECHG3"].ToString();
        txtEcharge3.Text = dr["ECHG4"].ToString();

        if (dr["FLAG1"].ToString() == "True")
        {
            chkDiscount.Checked = true;
        }
        else
        {
            chkDiscount.Checked = false;
        }
        if (dr["FLAG2"].ToString() == "True")
        {
            chkDiscount1.Checked = true;
        }
        else
        {
            chkDiscount1.Checked = false;
        }
        if (dr["FLAG3"].ToString() == "True")
        {
            chkDiscount2.Checked = true;
        }
        else
        {
            chkDiscount2.Checked = false;
        }
        if (dr["FLAG4"].ToString() == "True")
        {
            chkDiscount3.Checked = true;
        }
        else
        {
            chkDiscount3.Checked = false;
        }

        txtEPer.Text = dr["EP1"].ToString();
        txtEPer1.Text = dr["EP2"].ToString();
        txtEPer2.Text = dr["EP3"].ToString();
        txtEPer3.Text = dr["EP4"].ToString();

        txtEAmt.Text = string.Format("{0:0.00}", dr["EAMT1"]);
        txtEAmt1.Text = string.Format("{0:0.00}", dr["EAMT2"]);
        txtEAmt2.Text = string.Format("{0:0.00}", dr["EAMT3"]);
        txtEAmt3.Text = string.Format("{0:0.00}", dr["EAMT4"]);

        GrossAmt = Convert.ToDouble(dr["GRAMT"]);
        NetAmt = Convert.ToDouble(dr["NETAMT"]);
        ItmQty = (dr["ITEM_TOT_QTY"].ToString() == string.Empty) ? 0 : Convert.ToDouble(dr["ITEM_TOT_QTY"]);
        txtRemark.Text = dr["REMARK"].ToString();
        ViewState["GrossAmt"] = GrossAmt;
        ViewState["NetAmt"] = NetAmt;
        ViewState["TOTQTY"] = ItmQty;
        lblGrossAmt.Text = GrossAmt.ToString();
        lblNetAmt.Text = NetAmt.ToString();
        lblItemQty.Text = ItmQty.ToString();
       
        DataSet dtItem = objSController.GetAllItemsByInvoice(Convert.ToInt32(ViewState["INVNO"]));

        if (Convert.ToInt32(dtItem.Tables[0].Rows.Count) > 0)
        {

            lvItem.DataSource = dtItem.Tables[0];
            lvItem.DataBind();
            lvItem.Visible = true;
            Session["RecTbl"] = dtItem.Tables[0];
            ViewState["SRNO"] = Convert.ToInt32(dtItem.Tables[0].Rows.Count);
            ViewState["ddlInvoice"] = ViewState["INVNO"].ToString();
        }        
      
      //  ConvertDtItemToListItem(dtItem.Tables[0]);            


    }

    //Show Detail For Selected Invoice.
    protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitailizeTmpTable();
        ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
        ListInvItem.Clear();     
        ShowInvoiceDetails(Convert.ToInt32(ddlInvoice.SelectedValue));    
    }

    //Convert Datatable Item TO ListItem.
    void ConvertDtItemToListItem(DataTable dt)
    {
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                Health_Invoice_Item Item = new Health_Invoice_Item();
                Item.ITEM_NO = (int)dr["ITEM_NO"];
                Item.UNIT = Convert.ToDouble(dr["UNIT"]);
                Item.QTY = Convert.ToDouble(dr["QTY"]);
                Item.TOTQTY = Convert.ToDouble(dr["TOTQTY"]);
                Item.RATE = Convert.ToDouble(dr["RATE"]);
                Item.AMT = Convert.ToDouble(dr["AMT"]);
                Item.BATCH_NO = dr["BATCH_NO"].ToString();
                Item.EXPIRY_DATE = Convert.ToString(dr["EXPIRY_DATE"]);
                Item.MFG_DATE = Convert.ToString(dr["MFG_DATE"]);
                Item.INVTRNO = Convert.ToInt32(dr["INVTRNO"]);
                Item.ITEM_NAME = dr["ITEM_NAME"].ToString(); 
                AddItemToList(Item);
            }
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "IITMS.UAIMS->PesentationLayer->Health_StockMaintenance_InvoiceEntry->ConvertDtItemToListItem" + ex.Message);
        }
    }
   
    bool CheckItemInListWhileEdit(int ItemNo)
    {
        if (ViewState["Itemaction"].Equals("edit"))
        {
            EditItem = (Health_Invoice_Item)Session["edititem"];
            DtItems = (DataTable)Session["dtitems"];
            if (DtItems.Rows.Contains(ItemNo) && (ItemNo != EditItem.ITEM_NO))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    //cancel Changes
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
    }    
    void ClearList()
    {
        ListInvItem = (List<Health_Invoice_Item>)Session["listitem"];
        ListInvItem.Clear();
    }    
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (ddlInv.SelectedIndex > 0)
        {

            ShowReport("INVOICE_REPORT", "Str_Invoice.rpt");
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlMain,"Select invoice number first", this.Page);
            return;
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        pnlReport.Visible = false;
        btnSave.Enabled = true;
        btnModify.Enabled = true;
    }
    protected void ddlInv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Invoice.rpt&param=@P_INVTRNO=" + Convert.ToInt32(ddlInv.SelectedValue) + ",@username=" + Session["userfullname"].ToString(), "UAIMS");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {        
        ShowReport_consolidate("DETAIL_INVOICE_REPORT", "InvoiceConsolidateReport.rpt");
    }
    private void ShowReport_consolidate(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_INVTRNO=0";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_InvoiceEntry.ShowReport_consolidate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    //To Show INVOICE report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_INVTRNO=" + Convert.ToInt32(ViewState["invno"]) ;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ShowReport_consolidate.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["INVNO"] = int.Parse(btnEdit.CommandArgument);
        ViewState["action"] = "edit";
        ShowInvoiceDetails(Convert.ToInt32(ViewState["INVNO"]));    
    }
    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        //if (ddlInv.SelectedIndex > 0)
        //{
        Button btnReport = sender as Button;
        ViewState["invno"] = int.Parse(btnReport.CommandArgument);
            
            // ShowReport("INVOICE_REPORT", "Str_Invoice.rpt");
        ShowReport("INVOICE_REPORT", "Str_Invoice_details.rpt");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.updpnlMain, "Select invoice number first", this.Page);
        //    return;  Str_Invoice_details
        //}
    }


    private DataTable CreateTabel()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("UNIT", typeof(decimal)));
        dtRe.Columns.Add(new DataColumn("QUANTITY", typeof(decimal)));
        dtRe.Columns.Add(new DataColumn("TOT_QTY", typeof(decimal)));
        dtRe.Columns.Add(new DataColumn("RATE", typeof(decimal)));
        dtRe.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
        dtRe.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dtRe.Columns.Add(new DataColumn("EXPIRY_DATE", typeof(DateTime)));
        dtRe.Columns.Add(new DataColumn("MFG_DATE", typeof(DateTime)));
        dtRe.Columns.Add(new DataColumn("INVTRNO", typeof(int)));
        
        return dtRe;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lvItem.Visible = true;
            
          if (ViewState["actionADD"].ToString().Equals("add"))
            {
                if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
                {
                    int maxVal = 0;
                    DataTable dt = (DataTable)Session["RecTbl"];
                    DataRow dr = dt.NewRow();
                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                    }
                    dr["SRNO"] = maxVal + 1;
                    // dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["ITEM_NAME"] = txtItemName.Text.Trim() == null ? string.Empty : Convert.ToString(txtItemName.Text.Trim());
                    dr["ITEM_NO"] = Convert.ToInt32(hfItemName.Value);
                    dr["UNIT"] = Convert.ToDecimal(txtUnit.Text);
                    dr["QUANTITY"] = Convert.ToDecimal(txtQty.Text.Trim());
                    dr["TOT_QTY"] = Convert.ToDecimal(txtTotQty.Text.Trim());
                    dr["RATE"] = Convert.ToDecimal(txtRate.Text.Trim());
                    dr["AMOUNT"] = Convert.ToDecimal(txtAMT.Text.Trim());
                    dr["BATCHNO"] = txtBatchNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtBatchNo.Text.Trim());
                    dr["EXPIRY_DATE"] = Convert.ToDateTime(txtExpiryDate.Text);
                    dr["MFG_DATE"] = Convert.ToDateTime(txtMFGDate.Text);
                    if (ViewState["INVNO"] == null)
                    {
                        dr["INVTRNO"] = 0;
                    }
                    else
                    {
                        dr["INVTRNO"] = Convert.ToInt32(ViewState["INVNO"]);
                    }

                    dt.Rows.Add(dr);
                    Session["RecTbl"] = dt;
                    CalculateGross();
                    CalculateNet();
                    CalculateTotQty();
                    lvItem.DataSource = dt;
                    lvItem.DataBind();
                    ClearRec();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                }
                else
                {

                    DataTable dt = this.CreateTabel();
                    DataRow dr = dt.NewRow();                  

                    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["ITEM_NAME"] = txtItemName.Text.Trim() == null ? string.Empty : Convert.ToString(txtItemName.Text.Trim());
                    dr["ITEM_NO"] = Convert.ToInt32(hfItemName.Value);
                    dr["UNIT"] = Convert.ToDecimal(txtUnit.Text);
                    dr["QUANTITY"] = Convert.ToDecimal(txtQty.Text.Trim());
                    dr["TOT_QTY"] = Convert.ToDecimal(txtTotQty.Text.Trim());
                    dr["RATE"] = Convert.ToDecimal(txtRate.Text.Trim());
                    dr["AMOUNT"] = Convert.ToDecimal(txtAMT.Text.Trim());
                    dr["BATCHNO"] = txtBatchNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtBatchNo.Text.Trim());
                    dr["EXPIRY_DATE"] = Convert.ToDateTime(txtExpiryDate.Text);
                    dr["MFG_DATE"] = Convert.ToDateTime(txtMFGDate.Text);
                    dr["INVTRNO"] = 0;

                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dt.Rows.Add(dr);
                    ClearRec();
                    Session["RecTbl"] = dt;
                    CalculateGross();
                    CalculateNet();
                    CalculateTotQty();
                    lvItem.DataSource = dt;
                    lvItem.DataBind();
                }
            }
            else
            {
                if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
                {
                    DataTable dt = (DataTable)Session["RecTbl"]; 
                    DataRow dr = dt.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO"]);
                    dr["ITEM_NAME"] = txtItemName.Text.Trim() == null ? string.Empty : Convert.ToString(txtItemName.Text.Trim());
                    dr["ITEM_NO"] = Convert.ToInt32(hfItemName.Value);
                    dr["UNIT"] = Convert.ToDecimal(txtUnit.Text);
                    dr["QUANTITY"] = Convert.ToDecimal(txtQty.Text.Trim());
                    dr["TOT_QTY"] = Convert.ToDecimal(txtTotQty.Text.Trim());
                    dr["RATE"] = Convert.ToDecimal(txtRate.Text.Trim());
                    dr["AMOUNT"] = Convert.ToDecimal(txtAMT.Text.Trim());
                    dr["BATCHNO"] = txtBatchNo.Text.Trim() == null ? string.Empty : Convert.ToString(txtBatchNo.Text.Trim());
                    dr["EXPIRY_DATE"] = Convert.ToDateTime(txtExpiryDate.Text);
                    dr["MFG_DATE"] = Convert.ToDateTime(txtMFGDate.Text);
                    if (ViewState["INVNO"] == null)
                    {
                        dr["INVTRNO"] = 0;
                    }
                    else
                    {
                        dr["INVTRNO"] = Convert.ToInt32(ViewState["INVNO"]);
                    }



                    dt.Rows.Add(dr);
                    Session["RecTbl"] = dt;
                    CalculateGross();
                    CalculateNet();
                    CalculateTotQty();
                    lvItem.DataSource = dt;
                    lvItem.DataBind();
                    ClearRec();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_InvoiceEntry.btnAddTo_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEditRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = ((DataTable)Session["RecTbl"]);

                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
                ViewState["EDIT_SRNO"] = btnEditRec.CommandArgument;
                txtItemName.Text  = dr["ITEM_NAME"].ToString();
                hfItemName.Value = dr["ITEM_NO"].ToString();
                txtUnit.Text     =  dr["UNIT"].ToString();
                txtQty.Text      =  dr["QUANTITY"].ToString();
                txtTotQty.Text =  dr["TOT_QTY"].ToString();
                txtRate.Text = dr["RATE"].ToString();
                txtAMT.Text = dr["AMOUNT"].ToString();
                txtBatchNo.Text = dr["BATCHNO"].ToString(); 
                txtExpiryDate.Text = dr["EXPIRY_DATE"].ToString();
                txtMFGDate.Text = dr["MFG_DATE"].ToString();

                dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvItem.DataSource = dt;
                lvItem.DataBind();
                ViewState["actionADD"] = "edit";

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_InvoiceEntry.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvItem.DataSource = dt;
                lvItem.DataBind();
               
            }
            MessageBox("Record Deleted Successfully");
            CalculateGross();
            CalculateNet();
            CalculateTotQty();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_InvoiceEntry.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }

          

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_InvoiceEntry.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void ClearRec()
    {
        txtItemName.Text = string.Empty;       
        hfItemName.Value = string.Empty;  
        txtUnit.Text = string.Empty;    
        txtQty.Text = string.Empty;    
        txtTotQty.Text = string.Empty;    
        txtRate.Text = string.Empty;    
        txtAMT.Text = string.Empty;    
        txtBatchNo.Text = string.Empty;    
        txtExpiryDate.Text = string.Empty;
        txtMFGDate.Text = string.Empty;
        ViewState["actionADD"] = "add";
        ViewState["EDIT_SRNO"] = null;
    }


}