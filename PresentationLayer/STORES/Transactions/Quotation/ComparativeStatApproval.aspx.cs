//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : ComparativeStatApproval.aspx                                      
// CREATION DATE : 16-march-2019                                                    
// CREATED BY    : Mrunal Singh                                                 
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Drawing.Printing;

public partial class STORES_Transactions_Quotation_ComparativeStatApproval : System.Web.UI.Page
{  
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Vendor_Quotation_Entry_Controller objVQtEntry = new Str_Vendor_Quotation_Entry_Controller();
    Str_Quotation_Tender objQT = new Str_Quotation_Tender();
    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }

    protected void Page_Init(object sender, EventArgs e)
    {

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
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                string CompAuth = objCommon.LookUp("STORE_REFERENCE","COMPARATIVE_STAT_AUTHORITY_UANO","");
                if (Session["userno"].ToString() == CompAuth)
                {
                    this.BindQuotation();
                }
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

    private void BindQuotation()
    {
        DataSet ds = objVQtEntry.GetQuotationForApproval(Convert.ToInt32(Session["strdeptcode"].ToString()));        
        lstQuot.DataSource = ds.Tables[0];
        lstQuot.DataTextField = "REFNO";
        lstQuot.DataValueField = "QUOTNO";
        lstQuot.DataBind();
    }       

    protected void BindItemForCmpStmtByQuot(string Quotno)
    {
        DataSet dsItems = objVQtEntry.GetItemsByQuotNo(Quotno,0);
        lstItem.DataSource = dsItems.Tables[0];
        lstItem.DataTextField = "ITEM_NAME";
        lstItem.DataValueField = "ITEM_NO";
        lstItem.DataBind();
    } 

    protected void lstQuot_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemForCmpStmtByQuot(lstQuot.SelectedValue);
        divRemark.Visible = true;
    }

    
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Stores," + rptFileName;
            url += "&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_QUOTNO=" + Convert.ToString(lstQuot.SelectedValue) + "," + "@P_ITEM_NO=" + Convert.ToInt32(lstItem.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btncmpall_Click(object sender, EventArgs e)
    {        

        if (lstQuot.SelectedIndex >= 0)
        {
            HtmlTable Table = new HtmlTable();
            HtmlTableRow Title = new HtmlTableRow();
            HtmlTableCell TitleCell = new HtmlTableCell();
            TitleCell.Align = "center";
            TitleCell.NoWrap = true;
            TitleCell.InnerHtml = "<h4>Comparative Statement</h4><br>";

            // Table.Width = "100%";
            HtmlTableRow NetTotRow = new HtmlTableRow();
            HtmlTableRow BlankRow = new HtmlTableRow();
            HtmlTableRow RowHead1 = new HtmlTableRow();
            HtmlTableCell RowHeadItemCell1 = new HtmlTableCell();
            RowHead1.Cells.Add(RowHeadItemCell1);
            RowHead1.BorderColor = "Black";

            //Table.Rows.Add(BlankRow);
            //Table.Attributes.Add("class", "vista-grid");

            HtmlTableRow RowHeadQuot = new HtmlTableRow();
            HtmlTableCell RowHeadItemCellQuot = new HtmlTableCell();

            Table.Border = 1;
            Table.BorderColor = "Black";
            Table.Align = "Left";
            Table.CellSpacing = 0;

            RowHeadQuot.BorderColor = "Black";
            RowHeadItemCellQuot.InnerHtml = "<h4>Quot Ref No:" + lstQuot.SelectedItem.Text + "</h4><br>";
            RowHeadItemCellQuot.BgColor = "white";
            RowHeadQuot.Cells.Add(RowHeadItemCellQuot);
            Table.Rows.Add(RowHeadQuot);

            HtmlTableRow RowHead = new HtmlTableRow();
            DataTable VendorDt = objVQtEntry.GetVendorForCmpRpt(lstQuot.SelectedValue).Tables[0];
            HtmlTableCell RowHeadItemCell = new HtmlTableCell();
            RowHead.Cells.Add(RowHeadItemCell);
            RowHead.BorderColor = "Black";
            //RowHead.BgColor = "Orange";

            RowHeadItemCell.InnerHtml = "ItemName";
            RowHeadItemCell.BgColor = "Yellow";
            //RowHeadItemCell.BgColor="
            HtmlTableCell RowHeadQtyCell = new HtmlTableCell();

            RowHeadQtyCell.InnerHtml = "QTY";
            RowHeadQtyCell.BgColor = "Yellow";
            RowHead.Cells.Add(RowHeadQtyCell);
            foreach (DataRow dr1 in VendorDt.Rows)
            {
                HtmlTableCell RowHeadCell = new HtmlTableCell();
                RowHeadCell.InnerHtml = dr1["PNAME"].ToString();
                RowHeadCell.BgColor = "Yellow";
                RowHead.Cells.Add(RowHeadCell);
                Table.Rows.Add(RowHead);
            }
            DataTable QuotItemDt = objVQtEntry.GetItemsByQuotNo(lstQuot.SelectedValue,0).Tables[0];
            foreach (DataRow dr in QuotItemDt.Rows)
            {

                //ITEM NAME
                HtmlTableRow ItemRow = new HtmlTableRow();
                HtmlTableCell cellItemName = new HtmlTableCell();
                cellItemName.Align = "Left";
                cellItemName.InnerHtml = dr["ITEM_NAME"].ToString();
                ItemRow.Cells.Add(cellItemName);
                //QTY
                HtmlTableCell cellqty = new HtmlTableCell();
                cellqty.InnerHtml = dr["QTY"].ToString();
                cellqty.Align = "Left";
                ItemRow.Cells.Add(cellqty);
                //RATE
                foreach (DataRow drV in VendorDt.Rows)
                {
                    DataTable RateByVendorandItemDt = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), Convert.ToInt32(dr["ITEM_NO"].ToString())).Tables[0];
                    HtmlTableCell cellRate = new HtmlTableCell();
                    cellRate.Align = "Left";
                    cellRate.InnerHtml = RateByVendorandItemDt.Rows[0]["PRICE"].ToString();
                    ItemRow.Cells.Add(cellRate);
                    // DataTable RateByVendorandItemDt1 = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), Convert.ToInt32(dr["ITEM_NO"].ToString())).Tables[0];
                }
                Table.Rows.Add(ItemRow);

            }

            HtmlTableCell blank1 = new HtmlTableCell();
            blank1.BgColor = "yellow";
            HtmlTableCell blank2 = new HtmlTableCell();
            blank2.InnerText = "Total Incl. All Taxes / Discount";
            blank2.BgColor = "yellow";
            NetTotRow.Cells.Add(blank1);
            NetTotRow.Cells.Add(blank2);

            HtmlTableRow NetFieldTot = new HtmlTableRow();
            HtmlTableRow BlankRow1 = new HtmlTableRow();


            HtmlTableCell blankF1 = new HtmlTableCell();
            // blankF1.BgColor = "yellow";
            HtmlTableCell blankF2 = new HtmlTableCell();
            blankF2.InnerText = "Extra Charges";
            blankF2.BgColor = "yellow";
            NetFieldTot.Cells.Add(blankF1);
            NetFieldTot.Cells.Add(blankF2);


            HtmlTableRow NetAmtRow = new HtmlTableRow();
            HtmlTableRow BlankRow2 = new HtmlTableRow();


            HtmlTableCell blankNA1 = new HtmlTableCell();
            blankF1.BgColor = "yellow";
            HtmlTableCell blankNA2 = new HtmlTableCell();
            blankNA2.InnerText = "Net Amount";
            blankNA2.BgColor = "yellow";
            NetAmtRow.Cells.Add(blankNA1);
            NetAmtRow.Cells.Add(blankNA2);


            foreach (DataRow drV in VendorDt.Rows)
            {
                DataTable NetTotByVendorDt = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), 0).Tables[0];

                DataTable NetExtraCharge = objVQtEntry.GetExtraCharge(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString())).Tables[0];

                HtmlTableCell NetTotRowcell = new HtmlTableCell();
                NetTotRowcell.Align = "Left";
                NetTotRowcell.InnerHtml = NetTotByVendorDt.Rows[0][0].ToString();
                NetTotRow.Cells.Add(NetTotRowcell);
                NetTotRowcell.BgColor = "Yellow";

                HtmlTableCell NetRowCell1 = new HtmlTableCell();
                NetRowCell1.Align = "left";
                if (NetExtraCharge.Rows.Count > 0)
                {
                    NetRowCell1.InnerHtml = NetExtraCharge.Rows[0][0].ToString();
                }
                else
                {
                    NetRowCell1.InnerHtml = "-";
                }
                NetFieldTot.Cells.Add(NetRowCell1);
                NetRowCell1.BgColor = "Yellow";
                Double NetAmt;
                if (NetExtraCharge.Rows.Count > 0)
                {
                    NetAmt = Convert.ToDouble(NetExtraCharge.Rows[0][0].ToString()) + Convert.ToDouble(NetTotByVendorDt.Rows[0][0].ToString());
                }
                else
                {
                    NetAmt = Convert.ToDouble(NetTotByVendorDt.Rows[0][0].ToString());
                }
                HtmlTableCell NetRowCell2 = new HtmlTableCell();
                NetRowCell2.Align = "left";
                NetRowCell2.InnerHtml = NetAmt.ToString();
                NetAmtRow.Cells.Add(NetRowCell2);
                NetRowCell2.BgColor = "Yellow";
            }
            Table.Rows.Add(BlankRow);
            Table.Rows.Add(NetTotRow);
            Title.Cells.Insert(0, TitleCell);
            Table.Rows.Insert(0, Title);

            Table.Rows.Add(BlankRow);
            Table.Rows.Add(NetTotRow);

            Table.Rows.Add(BlankRow1);
            Table.Rows.Add(BlankRow1);
            Table.Rows.Add(NetFieldTot);

            Table.Rows.Add(BlankRow2);
            Table.Rows.Add(BlankRow2);
            Table.Rows.Add(NetAmtRow);

            pnlCmpst.Controls.Add(Table);
            Table.Visible = true;


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", "cpm.xls"));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Table.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        else
        {
            Showmessage("Select Quotation Ref No");           
        }
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    // Code for get Comparative statement for all items in pdf format
    protected void btnCmpAllPdf_Click(object sender, EventArgs e)
    {
        

        string Script = string.Empty;

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
        url += "Reports/STORES/ComparativeReport.aspx?";

        url += "quotno=" + lstQuot.SelectedValue.ToString();
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        //Script += " window.open('" + url + "','Comparative Statement','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //ScriptManager.RegisterClientScriptBlock(this.updVendorentry, updVendorentry.GetType(), "Report", Script, true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objQT.QUOTNO = lstQuot.SelectedValue;
            objQT.REFNO = lstQuot.SelectedItem.Text;
            objQT.ISAPPROVE = Convert.ToChar(ddlSelect.SelectedValue);
            objQT.APPROVAL_REMARK = txtRemarks.Text;
            CustomStatus cs = (CustomStatus)objVQtEntry.UpdateComparativeStatApproval(objQT);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Showmessage("Approval Status Updated Successfully.");
                BindQuotation();
                clear();
                return;
            }            
        }
        catch (Exception ex)
        {
            objCommon.ShowError(this, ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        divRemark.Visible = false;
        lstItem.Items.Clear();
        txtRemarks.Text = string.Empty;
    }
    protected void btncmpallNew_Click(object sender, EventArgs e)
    {
        if (lstQuot.SelectedIndex >= 0)
        {
             HtmlTable Table = new HtmlTable();
            HtmlTableRow Title = new HtmlTableRow();
            HtmlTableCell TitleCell = new HtmlTableCell();
            TitleCell.Align = "center";
            TitleCell.NoWrap = true;
            TitleCell.InnerHtml = "<h4>Comparative Statement</h4><br>";

            // Table.Width = "100%";
            HtmlTableRow NetTotRow = new HtmlTableRow();
            HtmlTableRow BlankRow = new HtmlTableRow();
            HtmlTableRow RowHead1 = new HtmlTableRow();
            HtmlTableCell RowHeadItemCell1 = new HtmlTableCell();
            RowHead1.Cells.Add(RowHeadItemCell1);
            RowHead1.BorderColor = "Black";

            //Table.Rows.Add(BlankRow);
            //Table.Attributes.Add("class", "vista-grid");

            HtmlTableRow RowHeadQuot = new HtmlTableRow();
            HtmlTableCell RowHeadItemCellQuot = new HtmlTableCell();

            Table.Border = 1;
            Table.BorderColor = "Black";
            Table.Align = "Left";
            Table.CellSpacing = 0;

            RowHeadQuot.BorderColor = "Black";
            RowHeadItemCellQuot.InnerHtml = "<h4>Quot Ref No:" + lstQuot.SelectedItem.Text + "</h4><br>";
            RowHeadItemCellQuot.BgColor = "white";
            RowHeadQuot.Cells.Add(RowHeadItemCellQuot);
            Table.Rows.Add(RowHeadQuot);
            Table.Border = 1;
            Table.BorderColor = "Black";
            Table.Align = "Left";
            Table.CellSpacing = 2;

            RowHeadQuot.BorderColor = "Black";
            RowHeadItemCellQuot.InnerHtml = "<h4>Quot Ref No:" + lstQuot.SelectedItem.Text + "</h4><br>";
            RowHeadItemCellQuot.BgColor = "white";
            RowHeadQuot.Cells.Add(RowHeadItemCellQuot);
            Table.Rows.Add(RowHeadQuot);

            HtmlTableRow RowHead = new HtmlTableRow();
            DataTable VendorDt = objVQtEntry.GetVendorForCmpRpt(lstQuot.SelectedValue).Tables[0];
            HtmlTableCell RowHeadItemCell = new HtmlTableCell();
            RowHead.Cells.Add(RowHeadItemCell);
            RowHead.BorderColor = "Black";
            //RowHead.BgColor = "Orange";

            RowHeadItemCell.InnerHtml = "ItemName";
            RowHeadItemCell.BgColor = "Yellow";
            //RowHeadItemCell.BgColor="
            HtmlTableCell RowHeadQtyCell = new HtmlTableCell();

            RowHeadQtyCell.InnerHtml = "QTY";
            RowHeadQtyCell.BgColor = "Yellow";
            RowHead.Cells.Add(RowHeadQtyCell);
            foreach (DataRow dr1 in VendorDt.Rows)
            {
                HtmlTableCell RowHeadCell = new HtmlTableCell();
                RowHeadCell.InnerHtml = dr1["PNAME"].ToString();
                RowHeadCell.BgColor = "Yellow";
                RowHead.Cells.Add(RowHeadCell);
                Table.Rows.Add(RowHead);
            }
            DataTable QuotItemDt = objVQtEntry.GetItemsByQuotNo(lstQuot.SelectedValue,0).Tables[0];
            foreach (DataRow dr in QuotItemDt.Rows)
            {

                //ITEM NAME
                HtmlTableRow ItemRow = new HtmlTableRow();
                HtmlTableCell cellItemName = new HtmlTableCell();
                cellItemName.Align = "Left";
                cellItemName.InnerHtml = dr["ITEM_NAME"].ToString();
                ItemRow.Cells.Add(cellItemName);
                //QTY
                HtmlTableCell cellqty = new HtmlTableCell();
                cellqty.InnerHtml = dr["QTY"].ToString();
                cellqty.Align = "Left";
                ItemRow.Cells.Add(cellqty);
                //RATE
                foreach (DataRow drV in VendorDt.Rows)
                {
                    DataTable RateByVendorandItemDt = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), Convert.ToInt32(dr["ITEM_NO"].ToString())).Tables[0];
                    HtmlTableCell cellRate = new HtmlTableCell();
                    cellRate.Align = "Left";
                    cellRate.InnerHtml = RateByVendorandItemDt.Rows[0]["PRICE"].ToString();
                    ItemRow.Cells.Add(cellRate);
                    // DataTable RateByVendorandItemDt1 = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), Convert.ToInt32(dr["ITEM_NO"].ToString())).Tables[0];
                }
                Table.Rows.Add(ItemRow);

            }

            HtmlTableCell blank1 = new HtmlTableCell();
            blank1.BgColor = "yellow";
            HtmlTableCell blank2 = new HtmlTableCell();
            blank2.InnerText = "Total Incl. All Taxes / Discount";
            blank2.BgColor = "yellow";
            NetTotRow.Cells.Add(blank1);
            NetTotRow.Cells.Add(blank2);

            HtmlTableRow NetFieldTot = new HtmlTableRow();
            HtmlTableRow BlankRow1 = new HtmlTableRow();


            HtmlTableCell blankF1 = new HtmlTableCell();
            // blankF1.BgColor = "yellow";
            HtmlTableCell blankF2 = new HtmlTableCell();
            blankF2.InnerText = "Extra Charges";
            blankF2.BgColor = "yellow";
            NetFieldTot.Cells.Add(blankF1);
            NetFieldTot.Cells.Add(blankF2);


            HtmlTableRow NetAmtRow = new HtmlTableRow();
            HtmlTableRow BlankRow2 = new HtmlTableRow();


            HtmlTableCell blankNA1 = new HtmlTableCell();
            blankF1.BgColor = "yellow";
            HtmlTableCell blankNA2 = new HtmlTableCell();
            blankNA2.InnerText = "Net Amount";
            blankNA2.BgColor = "yellow";
            NetAmtRow.Cells.Add(blankNA1);
            NetAmtRow.Cells.Add(blankNA2);


            foreach (DataRow drV in VendorDt.Rows)
            {
                DataTable NetTotByVendorDt = objVQtEntry.GetItemsForVendor(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString()), 0).Tables[0];

                DataTable NetExtraCharge = objVQtEntry.GetExtraCharge(lstQuot.SelectedValue, Convert.ToInt32(drV["PNO"].ToString())).Tables[0];

                HtmlTableCell NetTotRowcell = new HtmlTableCell();
                NetTotRowcell.Align = "Left";
                NetTotRowcell.InnerHtml = NetTotByVendorDt.Rows[0][0].ToString();
                NetTotRow.Cells.Add(NetTotRowcell);
                NetTotRowcell.BgColor = "Yellow";

                HtmlTableCell NetRowCell1 = new HtmlTableCell();
                NetRowCell1.Align = "left";
                if (NetExtraCharge.Rows.Count > 0)
                {
                    NetRowCell1.InnerHtml = NetExtraCharge.Rows[0][0].ToString();
                }
                else
                {
                    NetRowCell1.InnerHtml = "-";
                }
                NetFieldTot.Cells.Add(NetRowCell1);
                NetRowCell1.BgColor = "Yellow";
                Double NetAmt;
                if (NetExtraCharge.Rows.Count > 0)
                {
                    NetAmt = Convert.ToDouble(NetExtraCharge.Rows[0][0].ToString()) + Convert.ToDouble(NetTotByVendorDt.Rows[0][0].ToString());
                }
                else
                {
                    NetAmt = Convert.ToDouble(NetTotByVendorDt.Rows[0][0].ToString());
                }
                HtmlTableCell NetRowCell2 = new HtmlTableCell();
                NetRowCell2.Align = "left";
                NetRowCell2.InnerHtml = NetAmt.ToString();
                NetAmtRow.Cells.Add(NetRowCell2);
                NetRowCell2.BgColor = "Yellow";
            }
            Table.Rows.Add(BlankRow);
            Table.Rows.Add(NetTotRow);
            Title.Cells.Insert(0, TitleCell);
            Table.Rows.Insert(0, Title);

            Table.Rows.Add(BlankRow);
            Table.Rows.Add(NetTotRow);

            Table.Rows.Add(BlankRow1);
            Table.Rows.Add(BlankRow1);
            Table.Rows.Add(NetFieldTot);

            Table.Rows.Add(BlankRow2);
            Table.Rows.Add(BlankRow2);
            Table.Rows.Add(NetAmtRow);

            pnlCmpst.Controls.Add(Table);
            Table.Visible = true;


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", "cpm.xls"));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Table.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        else
        {
            Showmessage("Select Quotation Ref No");
        }

    }
}