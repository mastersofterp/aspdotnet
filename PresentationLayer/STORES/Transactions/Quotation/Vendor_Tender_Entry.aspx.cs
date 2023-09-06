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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using IITMS.NITPRM;

using System.Globalization;
using System.Collections;
using System.Collections.Generic;


public partial class STORES_Transactions_Quotation_Vendor_Tender_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Vendor_Tender_Entry_Controller objVenTnd = new Str_Vendor_Tender_Entry_Controller();
    Str_Vendor_Quotation_Entry_Controller objVQtEntry = new Str_Vendor_Quotation_Entry_Controller();
    DataTable tmpCalc;
    DataTable tmpPer;
    DataTable tmpInfo;

    GridView gvBudgetReport = new GridView();
    ArrayList arrlist = new ArrayList();
    ArrayList alPno = new ArrayList();
    ArrayList TotalPrice = new ArrayList();
    ArrayList TotalVale = new ArrayList();
    ArrayList GrandTotal = new ArrayList();
    ArrayList SubTotalPrice = new ArrayList();
    ArrayList SubTotalVale = new ArrayList();
    ArrayList GrandTotalPrice = new ArrayList();
    ArrayList GrandTotalVale = new ArrayList();
    int GridColCount = 3;
    ArrayList Tax = new ArrayList();
    ArrayList Discount = new ArrayList();

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

            Tabs.ActiveTabIndex = 0;
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["TaxTable"] = null;
                this.bindTendor();
            }
        }
        divMsg.InnerHtml = string.Empty;
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

    void bindTendor()
    {
        DataSet ds = objVenTnd.GetAllTendorCommercial();
        lstTender.DataSource = ds.Tables[0];
        lstTender.DataValueField = "TNO";
        lstTender.DataTextField = "TDRNO";
        lstTender.DataBind();
        DataSet ds1 = objVenTnd.GetAllTendorComp();
        lstTender1.DataSource = ds1.Tables[0];
        lstTender1.DataValueField = "TENDERNO";
        lstTender1.DataTextField = "TENDER";
        lstTender1.DataBind();

    }

    void bindParty()
    {
        lstVendor.Items.Clear();
        lstVendor.Items.Insert(0, new ListItem("Please Select", "0"));

        DataSet ds = new DataSet();
        ds = objVenTnd.getVendors(Convert.ToInt32(lstTender.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {            
            lstVendor.DataSource = ds.Tables[0];
            lstVendor.DataValueField = "TVNO";
            lstVendor.DataTextField = "VENDORNAME";
            lstVendor.DataBind();
        }
    }
    void bindPartyforMod()
    {
        lstVendor.Items.Clear();
        lstVendor.Items.Insert(0, new ListItem("Please Select", "0"));
        DataSet ds1 = objVenTnd.getVendorsMod(Convert.ToInt32(lstTender.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lstVendor.DataSource = ds1.Tables[0];
            lstVendor.DataValueField = "TVNO";
            lstVendor.DataTextField = "VENDORNAME";
            lstVendor.DataBind();
        }
    }
    protected void lstTender_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstVendor.Enabled = true;
        ClearItem();
        this.filldetails();
        //this.BindFieldMaster(Convert.ToInt32(lstTender.SelectedValue));
        this.bindPartyforMod();

    }

    protected void imgFieldNext_Click(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 1;
    }
    protected void imgVendorPrevious_Click(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 0;
    }
    protected void imgVendorNext_Click(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 2;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        STR_TENDER_ITEM_ENTRY objvientry = new STR_TENDER_ITEM_ENTRY();
        STR_TENDER_VENDOR objvendor = new STR_TENDER_VENDOR();
        try
        {
            for (int i = 0; i < grdItemList.Rows.Count; i++)
            {
                TextBox txtRate = (TextBox)grdItemList.Rows[i].FindControl("txtRate");
                if (txtRate.Text == "" || Convert.ToDouble(txtRate.Text) < 1)
                {
                    Showmessage("Please Enter Valid Rate For Each Item");
                    return;
                }
            }

            //if (ViewState["action"].Equals("add"))
            //{
            int TRVNO = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_PARTY", "MAX(TVNO)", "").ToString());
            objvendor.TVNO = TRVNO;
            objvendor.TVNO = Convert.ToInt32(lstVendor.SelectedValue);
            for (int i = 0; i < grdItemList.Rows.Count; i++)
            {

                objvientry.ITEM_NO = (int)grdItemList.DataKeys[i].Value;
                int itemno = (int)grdItemList.DataKeys[i].Value;

                Label lblqty = (Label)grdItemList.Rows[i].FindControl("lblQty");
                TextBox txtRate = (TextBox)grdItemList.Rows[i].FindControl("txtRate");
                TextBox txtDiscPercent = (TextBox)grdItemList.Rows[i].FindControl("txtDisc");
                TextBox txtDiscAmount = (TextBox)grdItemList.Rows[i].FindControl("txtDiscAmt");
                TextBox txtTaxableAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxableAmt");
                TextBox txtTaxAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxAmt");
                TextBox txtAmt = (TextBox)grdItemList.Rows[i].FindControl("txtAmt");
                HiddenField hdnTINO = (HiddenField)grdItemList.Rows[i].FindControl("hdnTINO");

                HiddenField hdnOthItemRemark = (HiddenField)grdItemList.Rows[i].FindControl("hdnOthItemRemark");
                HiddenField hdnTechSpec = (HiddenField)grdItemList.Rows[i].FindControl("hdnTechSpec");
                HiddenField hdnQualityQtySpec = (HiddenField)grdItemList.Rows[i].FindControl("hdnQualityQtySpec");


                objvientry.ITEM_REMARK = hdnOthItemRemark.Value;
                objvientry.TECHSPEC = hdnTechSpec.Value;
                objvientry.QUALITY_QTY_SPEC = hdnQualityQtySpec.Value;
                objvientry.TINO = Convert.ToInt32(hdnTINO.Value);
                objvientry.QTY = Convert.ToInt32(lblqty.Text);
                objvientry.TAXABLE_AMT = Convert.ToDecimal(txtTaxableAmt.Text);
                objvientry.TAXAMT = Convert.ToDecimal(txtTaxAmt.Text);
                objvientry.PRICE = Convert.ToDecimal(txtRate.Text);
                objvientry.DISCOUNT = txtDiscPercent.Text == "" ? 0 : Convert.ToDecimal(txtDiscPercent.Text);
                objvientry.DISCOUNTAMOUNT = txtDiscAmount.Text == "" ? 0 : Convert.ToDecimal(txtDiscAmount.Text);
                objvientry.TOTALAMT = Convert.ToDecimal(txtAmt.Text);

                ViewState["TotAmount"] = objvientry.TOTALAMT;

                objvientry.FLAG = "S";
                objvientry.EDATE = DateTime.Now.Date;
                //objvientry.TENDERNO = lstTender.SelectedValue;
                objvientry.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());

                objvientry.TENDERNO = objCommon.LookUp("STORE_TENDER", "TENDERNO", "TNO = " + Convert.ToInt32(lstTender.SelectedValue)).ToString();
                objvientry.COLLEGE_CODE = (Session["colcode"].ToString());
                objvientry.TVNO = Convert.ToInt32(lstVendor.SelectedValue);
                string colcode = (Session["colcode"].ToString());
                int ret = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_ITEM", "count(*)", "item_no =" + objvientry.ITEM_NO + "and TENDERNO='" + objvientry.TENDERNO + "' and TVNO=" + objvientry.TVNO));
                if (ret == 0)
                {
                    objVenTnd.SavePartyItemsEntry(objvientry, colcode);
                    Showmessage("Vendor Entry Save Successfully");
                }
                else
                {
                    objVenTnd.UpdatePartyItemsEntry(objvientry, colcode);
                    Showmessage("Vendor Entry Updated Successfully");
                }
            }
            STR_TENDER_FIELD_ENTRY objPFEntry = new STR_TENDER_FIELD_ENTRY();
            objPFEntry.TENDERNO = lstTender.SelectedValue;
            objPFEntry.TVNO = Convert.ToInt32(lstVendor.SelectedValue);
            objPFEntry.VENDOR_TAX_TBL = ViewState["TaxTable"] as DataTable;
            objVenTnd.SavePartyFieldEntry(objPFEntry, Session["colcode"].ToString());

            this.BindItemList(Convert.ToInt32(lstTender.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));
            //BindQuotforComp(); 

            //}

            //else
            //{
            //    this.updatePartyEntry(Session["colcode"].ToString());
            //    this.SavePartyFields(Session["colcode"].ToString(), Convert.ToInt32(lstVendor.SelectedValue));
            //    Showmessage("Vendor Entry Update  Succesfully");
            //}
            ClearItem();

        }

        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "edit";
        Tabs.ActiveTabIndex = 0;
        bindTendor();
        lstVendor.Enabled = false;
        lstVendor.Items.Clear();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearItem();
        ViewState["action"] = "add";
        bindTendor();
    }


    private void BindItemListForModification(int tendorno, int tvno)
    {
        DataSet dsItems = objVenTnd.GetVendorEntryItemsByParty(tendorno, tvno);
        grdItemList.DataSource = dsItems.Tables[0];
        string[] DataKeyNames = { "TINO" };
        grdItemList.DataKeyNames = DataKeyNames;
        grdItemList.DataBind();
        this.ShowPartyItems();

    }

    private void BindItemList(int tenderno, int Tvno)
    {
        DataSet dsItems = objVenTnd.GetTenderItemByTenderNo(tenderno, Tvno);
        grdItemList.DataSource = dsItems.Tables[0];
        string[] DataKeyNames = { "ITEM_NO" };
        grdItemList.DataKeyNames = DataKeyNames;
        grdItemList.DataBind();
    }

    private void filldetails()
    {
        DataSet ds1 = objVenTnd.GetTenderDetailsByTenderNo(Convert.ToInt32(lstTender.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblEMD.Text = ds1.Tables[0].Rows[0]["EMD"].ToString();
            lblSalesTax.Text = ds1.Tables[0].Rows[0]["STAX"].ToString();
            lblTAMT.Text = ds1.Tables[0].Rows[0]["TDAMT"].ToString();
            lblTOpen.Text = ds1.Tables[0].Rows[0]["TDODATE"].ToString();
            lblTotal.Text = ds1.Tables[0].Rows[0]["TOTAMT"].ToString();
            lblTREFNO.Text = ds1.Tables[0].Rows[0]["TENDERNO"].ToString();
        }


    }
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    void DisplayMessage(string Message)
    {

        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    protected void imgVendorPrevious_Click1(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 0;
    }
    protected void imgVendorNext_Click1(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 2;
    }

    protected void ClearItem()
    {

        Tabs.ActiveTabIndex = 0;
        grdItemList.DataSource = null;
        grdItemList.DataBind();
        lblvndadd.Text = string.Empty;
        lblvnd.Text = string.Empty;
        lblremark.Text = string.Empty;
        lblContact.Text = string.Empty;
        lblTotal.Text = string.Empty;
        lblTREFNO.Text = string.Empty;
        lblVCode.Text = string.Empty;
        lblcst.Text = string.Empty;
        lblbst.Text = string.Empty;
        lblEmail.Text = string.Empty;
        lblTOpen.Text = string.Empty;
        lblTAMT.Text = string.Empty;
        lblSalesTax.Text = string.Empty;
        lblEMD.Text = string.Empty;

        Session["tmpInfo"] = null;
        Session["tmpCalc"] = null;
        Session["tmpPer"] = null;
        bindParty();
    }

    void bindVendoDetails(int tenderno, int tvno)
    {
        DataSet ds = new DataSet();
        ds = objVenTnd.getVendor(tenderno, tvno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblContact.Text = ds.Tables[0].Rows[0]["VENDOR_CONTACT"].ToString();
            lblremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            lblvnd.Text = ds.Tables[0].Rows[0]["VENDORNAME"].ToString();
            lblvndadd.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
            lblcst.Text = ds.Tables[0].Rows[0]["CST"].ToString();
            lblbst.Text = ds.Tables[0].Rows[0]["BST"].ToString();
            lblEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            lblVCode.Text = ds.Tables[0].Rows[0]["VENDORCODE"].ToString();
        }
        else
        {
            lblContact.Text = string.Empty;
            lblremark.Text = string.Empty;
            lblvnd.Text = string.Empty;
            lblvndadd.Text = string.Empty;
            lblcst.Text = string.Empty;
            lblbst.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblVCode.Text = string.Empty;
        }
    }

    void ShowPartyItems()
    {
        foreach (GridViewRow row in grdItemList.Rows)
        {
            int TINO = (int)grdItemList.DataKeys[row.DataItemIndex].Value;
            DataTable dtPIEntry = objVenTnd.GetPartyEnrtyByNO(TINO).Tables[0];
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            txtRate.Text = dtPIEntry.Rows[0]["RATE"].ToString();
            TextBox txtcurr = (TextBox)row.FindControl("txtcurr");
            txtcurr.Text = dtPIEntry.Rows[0]["CURRENCY"].ToString();
            TextBox txtPerticular = (TextBox)row.FindControl("txtPerticular");
            txtPerticular.Text = dtPIEntry.Rows[0]["PERTICULAR"].ToString();
            TextBox txtModelno = (TextBox)row.FindControl("txtModelno");
            txtModelno.Text = dtPIEntry.Rows[0]["MODELNO"].ToString();
            TextBox txtManufacturer = (TextBox)row.FindControl("txtManufacturer");
            txtManufacturer.Text = dtPIEntry.Rows[0]["MANUFACTURE"].ToString();//
            TextBox txtTaxPer = (TextBox)row.FindControl("txtTaxPer");
            txtTaxPer.Text = dtPIEntry.Rows[0]["TAX_PER"].ToString();
            TextBox txtTaxAmt = (TextBox)row.FindControl("txtTaxAmt");
            txtTaxAmt.Text = dtPIEntry.Rows[0]["TAX_AMT"].ToString();
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
            txtTotal.Text = dtPIEntry.Rows[0]["TOTAL_AMT"].ToString();
        }
    }


    protected void lstVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

        // this.BindFieldMaster(Convert.ToInt32(lstTender.SelectedValue));
        bindVendoDetails(Convert.ToInt32(lstTender.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));
        Tabs.ActiveTabIndex = 1;
        //if (ViewState["action"].Equals("add"))
        //{
        lblTREFNO.Text = lstTender.SelectedItem.Text;
        this.BindItemList(Convert.ToInt32(lstTender.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));
        // }
        //else
        //{
        //    BindItemListForModification(Convert.ToInt32(lstTender.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));

        //}
    }


    //comparative statement control
    protected void btncmpitem_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Comparative_Statements", "Single_Item_Cmp_Report_Tender.rpt");

        }
        catch (Exception ex)
        {
            objCommon.ShowError(this, ex.Message);
        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            objCommon.ShowError(this, ex.Message);
        }
    }

    #region  Comparative

    protected void btncmpall_Click(object sender, EventArgs e)
    {
        if (lstTender1.SelectedIndex >= 0)
        {
            DataTable VendorDt = objVenTnd.GetVendorForFinancialCmpRpt(Convert.ToInt32(lstTender1.SelectedValue)).Tables[0];
            if (VendorDt.Rows.Count == 0)
            {
                Showmessage("Please Save All The Items For At Least One Vendor");
                return;
            }
            //Added by vijay andoju For getting Proper Structure for Showing Report
            DataTable dtBind = Datateble(VendorDt);

            gvBudgetReport.RowDataBound += new GridViewRowEventHandler(gvBudgetReport_RowDataBound);

            gvBudgetReport.DataSource = dtBind;
            gvBudgetReport.DataBind();

            int ColumnCount = dtBind.Columns.Count;
            int rowCount = dtBind.Rows.Count;

            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=ComparativeStatement.xls";

            AddHeader(ColumnCount + 1, rowCount);



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", attachment);
            Response.AppendHeader("Refresh", ".5; BudgetReportNew.aspx");
            Response.Charset = "";
            Response.ContentType = "application/" + ContentType;
            StringWriter sw1 =

                new StringWriter();
            HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
            gvBudgetReport.HeaderRow.Visible = false;
            gvBudgetReport.RenderControl(htw1);
            Response.Output.Write(sw1.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            Showmessage("Select Quotation Ref No.");
            //Tabs.ActiveTabIndex = 3;
        }
    }

    private DataTable Datateble(DataTable dtvendor)
    {


        //Added by vijay andoju for Creating Table 
        DataTable dt = new DataTable();
        dt.Columns.Add("Sl.No", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        //---------------------------------------------


        //Creating Column names for Vendor wise
        for (int i = 0; i < dtvendor.Rows.Count; i++)
        {
            dt.Columns.Add("Price/Unit" + i, typeof(string));
            dt.Columns.Add("Value" + i, typeof(string));
            arrlist.Add(dtvendor.Rows[i]["VENDORNAME"].ToString());
            alPno.Add(dtvendor.Rows[i]["TVNO"].ToString());
        }
        //---------------------------------------------------


        //Inserting the Itemname Qty in table

        DataTable TenderItemDt = objVenTnd.GetItemsByTnoForFinancialCmpRpt(Convert.ToInt32(lstTender1.SelectedValue)).Tables[0];
        for (int j = 0; j < TenderItemDt.Rows.Count; j++)
        {
            DataRow Row1;
            Row1 = dt.NewRow();
            Row1["Sl.No"] = j + 1;
            Row1["Description"] = TenderItemDt.Rows[j]["Item_name"].ToString();
            Row1["Quantity"] = TenderItemDt.Rows[j]["Qty"].ToString();
            dt.Rows.Add(Row1);


            for (int k = 0; k < dtvendor.Rows.Count; k++)
            {
                DataTable RateByVendorandItemDt = objVenTnd.GetItemsForTenderVendorFinancialCmpRpt(Convert.ToInt32(lstTender1.SelectedValue), Convert.ToInt32(dtvendor.Rows[k]["TVNO"].ToString()), Convert.ToInt32(TenderItemDt.Rows[j]["ITEM_NO"])).Tables[0];
                if (RateByVendorandItemDt.Rows.Count > 0)
                {
                    dt.Rows[j]["Price/Unit" + k] = RateByVendorandItemDt.Rows[0]["PRICE"].ToString();
                    dt.Rows[j]["Value" + k] = Convert.ToDouble(RateByVendorandItemDt.Rows[0]["Qty"]) * Convert.ToDouble(RateByVendorandItemDt.Rows[0]["PRICE"]);
                    // dt.Rows[j]["Value" + k] = RateByVendorandItemDt.Rows[0]["Grand"].ToString();
                    dt.Rows[j].AcceptChanges();
                }
                else
                {
                    dt.Rows[j]["Price/Unit" + k] = "0";
                    dt.Rows[j]["Value" + k] = "0";
                    dt.Rows[j].AcceptChanges();
                }
            }
        }
        //--------------------------------------------------------------------------------------------------
        int RoCount1 = dt.Rows.Count - 1;


        //added by vijay andoju 21102020 for calculation perpose like TotalAmount,SubTotalAmount,TaxAmount,DiscountAmount,GrandTotal
        for (int n = 0; n < arrlist.Count; n++)
        {

            Discount.Add("0");
            Discount.Add(Convert.ToDecimal(objCommon.LookUp("STORE_TENDER_ITEM", "ISNULL(SUM(DISCOUNT_AMOUNT),0)DISAMT", " TVNO=" + Convert.ToInt32(alPno[n].ToString()) + " AND TENDERNO='" + lstTender1.SelectedItem.Text.Trim() + "'")).ToString());

            TotalPrice.Add(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n])));
            TotalPrice.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n])))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()));


            DataTable NetExtraCharge = null;//objVQtEntry.GetExtraCharge(lstQuot1.SelectedValue, Convert.ToInt32(alPno[n].ToString())).Tables[0];
            if (NetExtraCharge != null && NetExtraCharge.Rows.Count > 0)
            {
                Tax.Add("0");
                //Tax.Add(Convert.ToDecimal(Convert.ToDecimal(TotalPrice[TotalPrice.Count - 1]) * Convert.ToDecimal(NetExtraCharge.Rows[0]["TOTVAT"])) / 100);
                Tax.Add(NetExtraCharge.Rows[0]["TOTVAT"].ToString());
            }
            else
            {
                Tax.Add("0");
                Tax.Add("0");
            }
            GrandTotalVale.Add(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n]))));
            GrandTotalVale.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

            GrandTotal.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

        }

        //--------------------------------------------------------------------------------------------------------

        return dt;

    }

    protected void gvBudgetReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvBudgetReport.HeaderRow.Cells.Count; i++)
                {
                    string Header = gvBudgetReport.HeaderRow.Cells[i].Text;
                    if (Header.ToString().Contains("/") || Header.ToString().Contains("Price/Unit") || Header.ToString().Contains("Value"))
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            //e.Row.Cells[i].Text = String.Format("{0:N2}", e.Row.Cells[i].Text == "&nbsp;" ? 0.00 : Convert.ToDouble(e.Row.Cells[i].Text));
                            e.Row.Cells[i].Text = IndianCurrency(e.Row.Cells[i].Text);
                        }
                        else
                        {
                            e.Row.Cells[i].Text = IndianCurrency("0");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string IndianCurrency(string AMOUNT)
    {

        decimal Amount = decimal.Parse(AMOUNT, CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("en-IN");
        string text = Amount.ToString("N2", hindi);
        return text;
    }

    private void AddHeader(int colspan, int Row)
    {
        int rows = Row + 9;


        string[] Col = { "Price/Unit", "Value" };

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell.Text = "Maulana Abul Kalam Azad University of Technology, West Bengal";
        HeaderCell.ColumnSpan = colspan;
        HeaderCell.Font.Size = 14;
        HeaderCell.Font.Bold = true;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gvBudgetReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "SIMHAT, HARINGHATA, NADIA, WEST  BENGAL, INDIA - 741249.";
        HeaderCell1.ColumnSpan = colspan;
        HeaderCell1.Font.Size = 9;
        HeaderCell1.Font.Bold = true;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gvBudgetReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "";//For FT/GN/20/08/01.04.19
        HeaderCell2.ColumnSpan = colspan;
        HeaderCell2.Font.Size = 10;
        HeaderCell2.Font.Bold = true;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gvBudgetReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "COMPARATIVE STATEMENT";
        HeaderCell3.ColumnSpan = colspan;
        HeaderCell3.Font.Size = 14;
        HeaderCell3.Font.Bold = true;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gvBudgetReport.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell4 = new TableCell();
        HeaderCell4 = new TableCell();
        HeaderCell4.Text = "Guidelines for preparation of Comparative Statement";
        HeaderCell4.ColumnSpan = colspan;
        HeaderCell4.Font.Size = 9;
        HeaderCell4.Font.Bold = true;
        HeaderCell4.BackColor = System.Drawing.Color.White;
        HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow4.Cells.Add(HeaderCell4);
        gvBudgetReport.Controls[0].Controls.AddAt(4, HeaderGridRow4);

        GridViewRow HeaderGridRow5 = new GridViewRow(5, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell5 = new TableCell();
        HeaderCell5 = new TableCell();
        HeaderCell5.Text = "* The final approval form should be prepared supplier wise";
        HeaderCell5.ColumnSpan = colspan;
        HeaderCell5.Font.Size = 10;
        HeaderCell5.Font.Bold = true;
        HeaderCell5.BackColor = System.Drawing.Color.White;
        HeaderCell5.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow5.Cells.Add(HeaderCell5);
        gvBudgetReport.Controls[0].Controls.AddAt(5, HeaderGridRow5);

        #region Headder

        GridViewRow HeaderGridRow6 = new GridViewRow(6, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header6Cell = new TableCell();
        Header6Cell.Text = "Sr.No.";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        Header6Cell = new TableCell();
        Header6Cell.Text = "Description";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        //----Added by vijay andoju 21-07-2020 for showing opening balance

        Header6Cell = new TableCell();
        Header6Cell.Text = "Quantity";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        TableCell HeaderN = new TableCell();

        for (int i = 0; i < arrlist.Count; i++)
        {
            for (int j = i; j == i; j++)
            {
                HeaderN = new TableCell();

                HeaderN.Text = arrlist[j].ToString();


                HeaderN.ColumnSpan = 2;
                HeaderN.RowSpan = 1;
                HeaderN.Font.Size = 10;
                HeaderN.Font.Bold = true;
                HeaderN.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow6.Cells.Add(HeaderN);
                gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);
                GridColCount++;

            }
        }

        GridViewRow HeaderGridRow7 = new GridViewRow(7, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header7Cell = new TableCell();

        for (int i = 1; i <= arrlist.Count; i++)
        {
            for (int j = 0; j < Col.Length; j++)
            {
                TableCell HeaderN1 = new TableCell();
                HeaderN1.Text = Col[j].ToString();
                HeaderN1.ColumnSpan = 1;
                //if (HeaderN1.Text == "Price/Unit")
                //    
                //else
                //    HeaderN1.ColumnSpan = 2;
                HeaderN1.RowSpan = 1;
                HeaderN1.Font.Size = 10;
                HeaderN1.Font.Bold = true;
                HeaderN1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow7.Cells.Add(HeaderN1);
                gvBudgetReport.Controls[0].Controls.AddAt(7, HeaderGridRow7);
                GridColCount++;
            }
        }

        #endregion

        //----Added by Gopal Anthati 10-12-2020 to get Calculative Taxes Dynamically
        #region Dynamic Total
        string NetAmount = string.Empty;
        DataTable TaxDt = objVenTnd.GetTaxHeadsForFinancialCmpRpt(Convert.ToInt32(lstTender1.SelectedValue)).Tables[0];

        List<string> TaxHead = new List<string>();
        List<string> TaxHeadNo = new List<string>();
        TaxHead.Add("Net Amount");
        TaxHeadNo.Add("0");
        for (int i = 0; i < TaxDt.Rows.Count; i++)
        {
            TaxHead.Add(TaxDt.Rows[i]["TAX_NAME"].ToString());
            TaxHeadNo.Add(TaxDt.Rows[i]["TAXID"].ToString());
        }

        for (int i = 1; i <= TaxHead.Count; i++)
        {
            GridViewRow HeaderGridRow10 = new GridViewRow(11, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell10 = new TableCell();
            HeaderCell10 = new TableCell();

            HeaderCell10.Text = TaxHead[i - 1];
            HeaderCell10.ColumnSpan = 3;
            HeaderCell10.RowSpan = 1;
            HeaderCell10.Font.Size = 11;

            HeaderCell10.Font.Bold = true;

            HeaderCell10.HorizontalAlign = HorizontalAlign.Left;
            HeaderGridRow10.Cells.Add(HeaderCell10);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow10);


            //for (int j = 1; j <= arrlist.Count * 2; j++)
            for (int j = 1; j <= arrlist.Count; j++)
            {
                for (int k = 1; k <= 2; k++)
                {
                    TableCell HeaderN1 = new TableCell();
                    DataSet TaxAmountDt = objVenTnd.GetTaxesForFinancialCmpRpt(Convert.ToInt32(lstTender1.SelectedValue), Convert.ToInt32(alPno[j - 1].ToString()), Convert.ToInt32(TaxHeadNo[i - 1]));
                    if (TaxHeadNo[i - 1] == "0")
                    {
                        if (k == 1)
                        {
                            HeaderN1.Text = "0";
                        }
                        else
                        {
                            String GrandTotalAmount = "0";
                            string CalAmt = objCommon.LookUp("STORE_TENDER_FIELDS B INNER JOIN  STORE_TAX_MASTER C ON (B.TAXID=C.TAXID)", "SUM(ISNULL(TAX_AMOUNT,0))TAX_AMOUNT ", "B.TENDERNO='" + lstTender1.SelectedValue + "' AND B.TVNO =" + Convert.ToInt32(alPno[j - 1].ToString()));
                            if (CalAmt == "") CalAmt = "0";
                            //for (int l = 1; l <= GrandTotal.Count; l++)
                            //{
                            GrandTotalAmount = GrandTotal[j - 1].ToString();
                            //}
                            NetAmount = IndianCurrency((Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount)).ToString());

                            string[] array = NetAmount.Split('.');
                            if (array[1] == "50")
                            {
                                NetAmount = (Convert.ToDouble(NetAmount) + 0.01).ToString();
                            }
                            //HeaderN1.Text = IndianCurrency((Math.Round(Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount))).ToString());
                            HeaderN1.Text = IndianCurrency(Math.Round(Convert.ToDouble(NetAmount)).ToString());
                        }
                    }
                    else
                    {
                        if (k == 1)
                        {
                            HeaderN1.Text = "0";
                        }
                        else
                        {
                            if (TaxAmountDt.Tables[0].Rows.Count > 0)
                                HeaderN1.Text = IndianCurrency(TaxAmountDt.Tables[0].Rows[0]["AMT"].ToString());
                            else
                                HeaderN1.Text = "0";
                        }
                    }


                    HeaderN1.ColumnSpan = 1;
                    HeaderN1.RowSpan = 1;
                    HeaderN1.Font.Size = 11;
                    if (i % 2 == 0)
                    {
                        HeaderN1.Font.Bold = false;
                    }
                    else
                    {
                        HeaderN1.Font.Bold = true;
                    }

                    HeaderN1.HorizontalAlign = HorizontalAlign.Right;
                    HeaderGridRow10.Cells.Add(HeaderN1);
                    gvBudgetReport.Controls[0].Controls.AddAt(rows + 2, HeaderGridRow10);
                }
            }
        }
        int count = TaxDt.Rows.Count;
        #endregion

        #region Total

        //string[] Head = { "Grand Total Amount", "Add - GST", "Total Amount", "Less - Discount" };
        string[] Head = { "Total Amount", "Less - Discount" };
        for (int i = 1; i <= Head.Length; i++)
        {
            GridViewRow HeaderGridRow9 = new GridViewRow(10, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell9 = new TableCell();
            HeaderCell9 = new TableCell();

            HeaderCell9.Text = Head[i - 1];
            HeaderCell9.ColumnSpan = 3;
            HeaderCell9.RowSpan = 1;
            HeaderCell9.Font.Size = 11;

            HeaderCell9.Font.Bold = true;

            HeaderCell9.HorizontalAlign = HorizontalAlign.Left;
            HeaderGridRow9.Cells.Add(HeaderCell9);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + 0, HeaderGridRow9);
            for (int j = 1; j <= arrlist.Count * 2; j++)
            {

                TableCell HeaderN1 = new TableCell();
                if (i == 1)
                {
                    HeaderN1.Text = IndianCurrency(TotalPrice[j - 1].ToString());
                }
                if (i == 2)
                {
                    HeaderN1.Text = IndianCurrency(Discount[j - 1].ToString());
                }
                if (i == 3)
                {
                    HeaderN1.Text = IndianCurrency(GrandTotalVale[j - 1].ToString());
                }
                if (i == 4)
                {
                    HeaderN1.Text = IndianCurrency(Tax[j - 1].ToString());
                }

                HeaderN1.ColumnSpan = 1;
                HeaderN1.RowSpan = 1;
                HeaderN1.Font.Size = 11;
                if (i % 2 == 0)
                {
                    HeaderN1.Font.Bold = false;
                }
                else
                {
                    HeaderN1.Font.Bold = true;
                }

                HeaderN1.HorizontalAlign = HorizontalAlign.Right;
                HeaderGridRow9.Cells.Add(HeaderN1);
                gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow9);
            }

        }
        #endregion

        #region SignatureHead
        int GridColumnCount = GridColCount;
        //string[] FooterHead = { "Prepared by", "Head of the Department", "Accounts Section", "Principal", "Secretary", "Treasurer" };
        string[] FooterHead = { "Prepared by" };
        //GridViewRow HeaderGridRow12 = new GridViewRow(12, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //for (int i = 0; i < 6; i++)
        //{
        //    TableCell HeaderCell11 = new TableCell();
        //    HeaderCell11.Text = "";
        //    HeaderCell11.ColumnSpan = 1;
        //    HeaderCell11.RowSpan = 1;
        //    HeaderCell11.Font.Size = 10;
        //    HeaderCell11.Font.Bold = true;
        //    HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow12.Cells.Add(HeaderCell11);
        //    gvBudgetReport.Controls[0].Controls.AddAt(rows + (4 + count), HeaderGridRow12);
        //}
        GridViewRow HeaderGridRow11 = new GridViewRow(13, 0, DataControlRowType.Header, DataControlRowState.Insert);
        for (int i = 0; i < FooterHead.Length; i++)
        {
            TableCell HeaderCell11 = new TableCell();
            HeaderCell11.Text = FooterHead[i].ToString();
            HeaderCell11.ColumnSpan = 2;
            HeaderCell11.RowSpan = 3;
            HeaderCell11.Font.Size = 10;
            HeaderCell11.Font.Bold = true;
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow11.Cells.Add(HeaderCell11);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + (4 + count), HeaderGridRow11);
        }
        #endregion

        gvBudgetReport.FooterStyle.Font.Bold = true;
        gvBudgetReport.FooterStyle.Font.Size = 19;
    }

    #endregion

    private static void PrepareControlForExport(Control control)
    {
    }

    protected void BindItemForCmpStmtByTender(int tenderno, int Tvno)
    {
        DataSet dsItems = objVenTnd.GetTenderItemByTenderNo(tenderno, Tvno);
        lstItem.DataSource = dsItems.Tables[0];
        lstItem.DataTextField = "ITEM_NAME";
        lstItem.DataValueField = "ITEM_NO";
        lstItem.DataBind();
    }
    protected void lstTender1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemForCmpStmtByTender(Convert.ToInt32(lstTender1.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Store")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Store," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + "," + "@P_TENDERNO=" + Convert.ToString(lstTender1.SelectedValue) + "," + "@P_ITEM_NO=" + Convert.ToInt32(lstItem.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,STORE," + rptFileName;
            ////url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@P_TENDERNO=" + lstTender.SelectedValue  + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_TENDERNO=" + lstTender.SelectedValue;

            ////divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            ////divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ////divMsg.InnerHtml += " </script>";
            ////To open new window from Updatepanel

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //protected void txtTaxPer_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateTax();
    //}
    //protected void txtRate_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateTax();
    //}
    //protected void CalculateTax()
    //{
    //    for (int i = 0; i < grdItemList.Rows.Count; i++)
    //    {
    //        TextBox txtRate = (TextBox)grdItemList.Rows[i].FindControl("txtRate");
    //        txtRate.Text = txtRate.Text;
    //        // Label lblQty = (Label)grdItemList.FindControl("lblQty");
    //        Label lblQty = (Label)grdItemList.Rows[i].FindControl("lblQty");

    //        TextBox txtPer = (TextBox)grdItemList.Rows[i].FindControl("txtTaxPer");
    //        if (txtPer.Text != "" && txtRate.Text.Length > 0)
    //        {
    //            Double GrsAmt = Convert.ToDouble(txtRate.Text) * Convert.ToDouble(lblQty.Text);
    //            Double TotTax = (GrsAmt) * Convert.ToDouble(txtPer.Text) / 100;
    //            TextBox txtTaxAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxAmt");
    //            txtTaxAmt.Text = TotTax.ToString();//(GrsAmt + TotTax).ToString();
    //            TextBox txtTotAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTotal");
    //            txtTotAmt.Text = (GrsAmt + TotTax).ToString();// TotTax.ToString();
    //        }
    //    }
    //}


    private void BindTaxes()
    {
        DataSet ds = null;
        int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO IN (SELECT PNO FROM STORE_TENDER_PARTY WHERE TVNO=" + lstVendor.SelectedValue + ")"));
        int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
        if (VendorState == CollegeState)
        {
            ds = objVenTnd.GetTaxesForTender(lstTender.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            ds = objVenTnd.GetTaxesForTender(lstTender.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
        }
        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxAdd = (DataTable)ViewState["TaxTable"];
            if (ds.Tables[0].Rows.Count > 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    int maxVal = 0;

                    DataRow datarow = null;
                    datarow = dtTaxAdd.NewRow();
                    if (datarow != null)
                    {
                        maxVal = Convert.ToInt32(dtTaxAdd.AsEnumerable().Max(row => row["TAX_SRNO"]));
                    }
                    datarow["TAX_SRNO"] = ds.Tables[0].Rows[i]["TAX_SRNO"].ToString();//maxVal + 1;
                    datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                    datarow["TAXID"] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                    datarow["TAX_NAME"] = ds.Tables[0].Rows[i]["TAX_NAME"].ToString();
                    datarow["TAX_AMOUNT"] = ds.Tables[0].Rows[i]["TAX_AMOUNT"].ToString();
                    ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                    dtTaxAdd.Rows.Add(datarow);
                }
                ViewState["TaxTable"] = dtTaxAdd;
                DataRow[] foundRow = dtTaxAdd.Select("ITEM_NO=" + ViewState["ItemNo"]);
                if (foundRow.Length > 0)
                {
                    DataTable dtTax = foundRow.CopyToDataTable();
                    lvTax.DataSource = dtTax;
                    lvTax.DataBind();
                    hdnListCount.Value = dtTax.Rows.Count.ToString();
                    ViewState["action"] = "edit";
                    hdnOthEdit.Value = "1";

                    this.MdlTax.Show();
                    divOthPopup.Visible = false;
                    divTaxPopup.Visible = true;
                    CalTotTax();
                }
            }
            else
            {
                lvTax.DataSource = null;
                lvTax.DataBind();
                this.MdlTax.Hide();
                Showmessage("No Taxes Are Applicable For This Item.");
                return;
            }
            // AddTaxTable(dtTaxdup);

        }
        else if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtTax = (DataTable)ds.Tables[0];
            DataRow[] foundRow = dtTax.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dtTaxAdd = foundRow.CopyToDataTable();
                lvTax.DataSource = dtTaxAdd;
                lvTax.DataBind();
                hdnListCount.Value = dtTaxAdd.Rows.Count.ToString();
                ViewState["action"] = "edit";
                hdnOthEdit.Value = "1";
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                CalTotTax();
            }
        }
        else
        {
            lvTax.DataSource = null;
            lvTax.DataBind();
            this.MdlTax.Hide();
            Showmessage("No Taxes Are Applicable For This Item.");
            return;
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

    protected void btnAddTax_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["ItemNo"] = null;
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);

        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dt = foundRow.CopyToDataTable();
                lvTax.DataSource = dt;
                lvTax.DataBind();
                hdnListCount.Value = dt.Rows.Count.ToString();
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                //ViewState["TaxEdit"]="edit";
                CalTotTax();
            }
            else
            {
                BindTaxes();
            }

        }
        else
        {
            BindTaxes();
        }

    }

    protected void btnAddOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        this.MdlTax.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        //if (ViewState["action"].ToString() == "edit" && hdnOthEdit.Value == "1")
        //{
        ImageButton btn = sender as ImageButton;
        int ItemNo = Convert.ToInt32(btn.CommandArgument);
        string Tenderno = objCommon.LookUp("STORE_TENDER", "TENDERNO", "TNO=" + lstTender.SelectedValue);
        DataSet ds = objCommon.FillDropDown("STORE_TENDER_ITEM", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "TENDERNO='" + Tenderno + "' AND TVNO=" + Convert.ToInt32(lstVendor.SelectedValue) + " AND ITEM_NO=" + ItemNo, "");
        if (ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
            txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
            txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();
        }
        ///}
    }

    protected void btnSaveTax_Click(object sender, EventArgs e)
    {
        //if (ViewState["TaxEdit"] == null)
        // {
        if (ViewState["TaxTable"] != null && ((DataTable)ViewState["TaxTable"]) != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                foreach (DataRow drow in foundRow)
                    dtTaxdup.Rows.Remove(drow);
            }
            DataTable dtTax = (DataTable)ViewState["TaxTable"];
            DataTable dtCount = (DataTable)ViewState["TaxTable"];
            int SrnoCount = dtCount.Rows.Count;
            int count = 0;
            int maxVal = 0;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;

                DataRow datarow = null;
                datarow = dtTax.NewRow();

                if (SrnoCount > 0)
                {
                    datarow["TAX_SRNO"] = maxVal + 1; //dtTax.Rows[count]["TAX_SRNO"].ToString();
                    maxVal++;
                }
                else
                {
                    if (datarow != null)
                    {
                        maxVal = Convert.ToInt32(dtTax.AsEnumerable().Max(row => row["TAX_SRNO"]));
                    }
                    datarow["TAX_SRNO"] = maxVal + 1;
                }

                datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                datarow["TAXID"] = hdnTaxId.Value;
                datarow["TAX_NAME"] = lblTaxName.Text;
                datarow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(datarow);
                count++;
            }
            ViewState["TaxTable"] = dtTax;
        }
        else
        {
            DataTable dtTax = this.CreateTaxTable();
            DataRow datarow = null;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                datarow = dtTax.NewRow();

                datarow["TAX_SRNO"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                datarow["TAXID"] = hdnTaxId.Value;
                datarow["TAX_NAME"] = lblTaxName.Text;
                datarow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(datarow);
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

    protected void btnSaveOthInfo_Click(object sender, EventArgs e)
    {
        this.MdlTax.Hide();
        txtItemRemarkOth.Text = string.Empty;
        txtQualityQtySpec.Text = string.Empty;
        txtTechSpec.Text = string.Empty;

    }

}

