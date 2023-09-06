using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_Acc_GST_TDS_Report : System.Web.UI.Page
{
    Common ObjComman = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();

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
                
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["ds"] = null;
            }
        }

        if (!(ViewState["idno"] == null || ViewState["idno"] == ""))
        {
            //OpenPopUp(Button2, "UploadDoc.aspx?action=pr&idno=" + Convert.ToInt32(ViewState["idno"].ToString()), "PRM", 1000, 1500);
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

    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (rblGroup.SelectedValue == "1")
        {
            ShowReport("GST ON TDS REPORT", "GST_TDS_Report.rpt");
        }
        if (rblGroup.SelectedValue == "2")
        {
            ShowReport("GST REPORT", "GSTReport.rpt");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {

        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString() + "," + "@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["ds"] = null;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        rblGroup.SelectedValue = "1";
        lvGSTONTDS.DataSource = null;
        lvGSTONTDS.DataBind();
        lvgst.DataSource = null;
        lvgst.DataBind();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (rblGroup.SelectedValue == "1")
        {
            DivGSTONTDS.Visible = true;
            DataSet DsExcel = objAVC.GetGSTONTDSRecordsExcel(Session["Comp_Code"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            if (DsExcel.Tables[0].Rows.Count > 0)
            {
                ViewState["ds"] = DsExcel;
                lvGSTONTDS.DataSource = DsExcel;
                lvGSTONTDS.DataBind();


            }
        }
        else
        {
            DivGSTONTDS.Visible = false;
        }
        if (rblGroup.SelectedValue == "2")
        {
            Divgst.Visible = true;
            DataSet Ds = objAVC.GetGSTDetailsExcel(Session["Comp_Code"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ds"] = Ds;
                lvgst.DataSource = Ds;
                lvgst.DataBind();


            }
        }
        else
        {
            Divgst.Visible = false;
        }


    }

    protected void imgbutExporttoexcel_Click(object sender, ImageClickEventArgs e)
    {
        //if (lvEmpStatus == null)
        //{
        //    objCommon.DisplayMessage(this.Page, "Please select Month and Staff", this.Page);
        //    return;
        //}
        if (rblGroup.SelectedValue == "1")
        {
         ExportToExcel();
        }
        if (rblGroup.SelectedValue == "2")
        {
            ExportGSTExcel();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //Verifies that the control is rendered
    }

 

    private void ExportToExcel()
    {

        DataSet DsExcel = objAVC.GetGSTONTDSRecordsExcel(Session["Comp_Code"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

        DataView dvData = new DataView(DsExcel.Tables[0]);
        DataTable dtExcelData = new DataTable();
        dtExcelData = dvData.ToTable();
        DataRow Row = dtExcelData.NewRow();
        double GBillAmtTotal = 0, CGST_AMOUNT = 0, SGST_AMOUNT = 0, IGST_AMOUNT = 0;
        Row["PARTY_NAME"] = "Grand Total";

        if (DsExcel != null)
        {
            if (DsExcel.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DsExcel.Tables[0].Rows.Count; i++)
                {
                    GBillAmtTotal = GBillAmtTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["TAXABLE_AMOUNT"].ToString());
                    CGST_AMOUNT = CGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["CGST_AMOUNT"].ToString());
                    SGST_AMOUNT = SGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["SGST_AMOUNT"].ToString());
                    IGST_AMOUNT = IGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["IGST_AMOUNT"].ToString());
                }
                Row["TAXABLE_AMOUNT"] = GBillAmtTotal.ToString();
                Row["CGST_AMOUNT"] = CGST_AMOUNT.ToString();
                Row["SGST_AMOUNT"] = SGST_AMOUNT.ToString();
                Row["IGST_AMOUNT"] = IGST_AMOUNT.ToString();
              

                dtExcelData.Rows.Add(Row);
                //grdTDSExcel.DataSource = DsExcel.Tables[0];


                Session["dtExcel"] = dtExcelData;
              
                DataTable dtExcel = Session["dtExcel"] as DataTable;
                grdTDSExcel.DataSource = dtExcel;
                grdTDSExcel.DataBind();

                //GridViewRow lastrow = grdTDSExcel.Rows[(grdTDSExcel.Rows.Count) - 1];
                //for (int i = 0; i < lastrow.Cells.Count; i++)
                //{
                //    lastrow.Cells[i].Font.Bold = true;
                //}

                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                {
                    if (dtExcelData.Rows[i]["PARTY_NAME"].ToString() == "0")
                    {
                        grdTDSExcel.Rows[i].Font.Bold = true;
                    }
                }
            }
            else
            {

               // objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
                grdTDSExcel.DataSource = null;
                grdTDSExcel.DataBind();
               // pnlTDSGrid.Visible = false;
                return;
            }
        }
        else
        {
           // objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
            grdTDSExcel.DataSource = null;
            grdTDSExcel.DataBind();
           // pnlTDSGrid.Visible = false;
            return;
        }

        if (DsExcel.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = "MAKAUT";
            HeaderCell.ColumnSpan = 8;
            // HeaderCell.BackColor = System.Drawing.Color.White;
            // HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "PERIOD " + txtFromDate.Text +" "+ "TO"+"  " + txtToDate.Text; ;
            HeaderCell1.ColumnSpan = 8;
            // HeaderCell1.BackColor = System.Drawing.Color.White;
            // HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            grdTDSExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "GST ON TDS REPORT";
            HeaderCell.ColumnSpan = 8;
            // HeaderCell.BackColor = System.Drawing.Color.White;
            //HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            string attachment = "attachment; filename=TDSONGST.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdTDSExcel.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    private void ExportGSTExcel()
    {

        DataSet DsExcel = objAVC.GetGSTDetailsExcel(Session["Comp_Code"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

        DataView dvData = new DataView(DsExcel.Tables[0]);
        DataTable dtExcelData = new DataTable();
        dtExcelData = dvData.ToTable();
        DataRow Row = dtExcelData.NewRow();
        double GBillAmtTotal = 0, CGST_AMOUNT = 0, SGST_AMOUNT = 0, IGST_AMOUNT = 0;
        Row["PARTY_NAME"] = "Grand Total";

        if (DsExcel != null)
        {
            if (DsExcel.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DsExcel.Tables[0].Rows.Count; i++)
                {
                    GBillAmtTotal = GBillAmtTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["BILL_AMT"].ToString());
                    CGST_AMOUNT = CGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["CGST_AMOUNT"].ToString());
                    SGST_AMOUNT = SGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["SGST_AMOUNT"].ToString());
                    IGST_AMOUNT = IGST_AMOUNT + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["IGST_AMOUNT"].ToString());
                }
                Row["BILL_AMT"] = GBillAmtTotal.ToString();
                Row["CGST_AMOUNT"] = CGST_AMOUNT.ToString();
                Row["SGST_AMOUNT"] = SGST_AMOUNT.ToString();
                Row["IGST_AMOUNT"] = IGST_AMOUNT.ToString();


                dtExcelData.Rows.Add(Row);
                //grdTDSExcel.DataSource = DsExcel.Tables[0];


                Session["dtExcel"] = dtExcelData;

                DataTable dtExcel = Session["dtExcel"] as DataTable;
                GRIDGST.DataSource = dtExcel;
                GRIDGST.DataBind();

                //GridViewRow lastrow = grdTDSExcel.Rows[(grdTDSExcel.Rows.Count) - 1];
                //for (int i = 0; i < lastrow.Cells.Count; i++)
                //{
                //    lastrow.Cells[i].Font.Bold = true;
                //}

                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                {
                    if (dtExcelData.Rows[i]["PARTY_NAME"].ToString() == "0")
                    {
                        GRIDGST.Rows[i].Font.Bold = true;
                    }
                }
            }
            else
            {

                // objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
                GRIDGST.DataSource = null;
                GRIDGST.DataBind();
                // pnlTDSGrid.Visible = false;
                return;
            }
        }
        else
        {
            // objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
            GRIDGST.DataSource = null;
            GRIDGST.DataBind();
            // pnlTDSGrid.Visible = false;
            return;
        }

        if (DsExcel.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = "MAKAUT";
            HeaderCell.ColumnSpan = 8;
             HeaderCell.BackColor = System.Drawing.Color.White;
             HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);
            GRIDGST.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "PERIOD "+txtFromDate.Text+"  "+"TO"+"  "+txtToDate.Text;
            HeaderCell1.ColumnSpan = 8;
             HeaderCell1.BackColor = System.Drawing.Color.White;
             HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GRIDGST.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "GST REPORT";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell);
            GRIDGST.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            string attachment = "attachment; filename=GST.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GRIDGST.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}