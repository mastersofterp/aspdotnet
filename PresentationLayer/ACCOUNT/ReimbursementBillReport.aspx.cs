//=================================================================================
// PROJECT NAME  :UAIMS SVCE                                                 
// MODULE NAME   :Reimabursement Bill Report                                                 
// CREATION DATE :23-Sep-2020                                            
// CREATED BY    :Vijay Andoju                                       
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Web;


public partial class ACCOUNT_ReimbursementBillReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();
    GridView gvReibursement = new GridView();
    DataSet ds = new DataSet();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Check Page Authoriztion
            CheckPageAuthorization();
            BindCompany();
            BindDepartment();
            SetFinancialYear();
            if (Convert.ToInt32(Session["usertype"].ToString()) != 1)
            {
                ddlDept.SelectedValue = Session["UA_EmpDeptNo"].ToString();
                ddlDept.Enabled = false;
            }
            else
            {
                ddlDept.Enabled = true;
            }

        }
    }
    #endregion


    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {

            txtApprovalFormDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtApprovalToDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");

         
        }
        dtr.Close();
    }


    #region BindDropDown

    private void BindDepartment()
    {
        objCommon.FillDropDownList(ddlDept, "Payroll_subdept", "SubDeptNo", "SubDept", "SubDeptNo<>0", "SubDept");
    }

    private void BindCompany()
    {
        //   objCommon.FillDropDownList(ddlCompany, "Acc_Company", "Company_no", "Company_Name + ' - ' + Cast(Year(COMPANY_FINDATE_FROM) as nvarchar(10))+ ' - ' + Cast(Year(COMPANY_FINDATE_TO) As nvarchar(10))", "Drop_Flag='N'", "Company_Name");
    }

    #endregion

    #region CheckPageAuthorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    #endregion

    #region PrintReportPDF
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_CODE=" + Session["Comp_Code"].ToString() + ",@P_DEPT_NO=" + ddlDept.SelectedValue + ",@P_FROM_DATE=" + Convert.ToDateTime(txtApprovalFormDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtApprovalToDate.Text).ToString("yyyy-MM-dd");
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPT_NO=" + Session["UA_EmpDeptNo"].ToString() + ",@P_FROM_DATE='',@P_TO_DATE=''";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updreimabursementBillReport, updreimabursementBillReport.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        } 
    }
    #endregion
    protected void btnPdfReport_Click(object sender, EventArgs e)
    {
        ShowReport("Reimbursement Bill Report", "ReimbursementBillReport.rpt");
    }

    protected DataTable Addtable(DataSet dst)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SL.NO.", typeof(string));
        dt.Columns.Add("TRANSACTION NO", typeof(string));
        dt.Columns.Add("DATE OF ENTRY", typeof(string));
        dt.Columns.Add("INVOICE NO", typeof(string));
        dt.Columns.Add("INVOICE DATE", typeof(string));
     //   dt.Columns.Add("APPROVAL NO", typeof(string));
        dt.Columns.Add("ITEM DESCRIPTION", typeof(string));
        dt.Columns.Add("VENDOR NAME", typeof(string));
        dt.Columns.Add("AMOUNT PAID", typeof(string));
        dt.Columns.Add("PAID DATE", typeof(string));
        dt.Columns.Add("BUDGET HEAD", typeof(string));
        dt.Columns.Add("VOUCHER NO", typeof(string));
        dt.Columns.Add("LEDGER HEAD", typeof(string));
        dt.Columns.Add("EXPENSE HEAD", typeof(string));
        dt.Columns.Add("BANK", typeof(string));
        dt.Columns.Add("JOURNAL VOUCHER", typeof(string));
        dt.Columns.Add("PAYMENT VOUCHER", typeof(string));
        decimal Total = 0;
        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
        {
            DataRow row;
            row = dt.NewRow();
            row["SL.NO."] = i + 1;
            row["DATE OF ENTRY"] = dst.Tables[0].Rows[i]["DATE OF ENTRY"].ToString() == "-" ? "" : Convert.ToDateTime(dst.Tables[0].Rows[i]["DATE OF ENTRY"].ToString()).ToString("dd/MM/yyyy");

            row["TRANSACTION NO"] = dst.Tables[0].Rows[i]["TRANSACTION NO"].ToString();
            row["INVOICE NO"] = dst.Tables[0].Rows[i]["BILL INVOICE NO"].ToString();
            row["INVOICE DATE"] = dst.Tables[0].Rows[i]["BILL INVOICE DATE"].ToString() == "-" ? "" : Convert.ToDateTime(dst.Tables[0].Rows[i]["BILL INVOICE DATE"].ToString()).ToString("dd/MM/yyyy");
          //  row["APPROVAL NO"] = dst.Tables[0].Rows[i]["APPROVAL_NO"].ToString();
            row["ITEM DESCRIPTION"] = dst.Tables[0].Rows[i][7].ToString();
            row["VENDOR NAME"] = dst.Tables[0].Rows[i]["VENDOR NAME"].ToString();
            row["AMOUNT PAID"] = dst.Tables[0].Rows[i]["NET_AMT"].ToString();
            row["PAID DATE"] = dst.Tables[0].Rows[i]["APPROVAL DATE"].ToString() == "-" ? "" : Convert.ToDateTime(dst.Tables[0].Rows[i]["APPROVAL DATE"].ToString()).ToString("dd/MM/yyyy");
            row["BUDGET HEAD"] = dst.Tables[0].Rows[i]["BUDGET_HEAD"].ToString();
            row["VOUCHER NO"] = dst.Tables[0].Rows[i]["VOUCHER_NO"].ToString();
            row["LEDGER HEAD"] = dst.Tables[0].Rows[i]["LEDGERNAME"].ToString();
            row["EXPENSE HEAD"] = dst.Tables[0].Rows[i]["EXPENSENAME"].ToString();
            row["BANK"] = dst.Tables[0].Rows[i]["BANKNAME"].ToString();
            row["JOURNAL VOUCHER"] = dst.Tables[0].Rows[i]["JOURNALVOUCHER"].ToString();
            row["PAYMENT VOUCHER"] = dst.Tables[0].Rows[i]["PAYMENTVOUCHER"].ToString();
            Total+=Convert.ToDecimal(dst.Tables[0].Rows[i]["NET_AMT"].ToString());
            dt.Rows.Add(row);
        }
        DataRow row1;
        row1 = dt.NewRow();
        row1["SL.NO."] ="";
        row1["DATE OF ENTRY"] = "";

        row1["TRANSACTION NO"] ="";
        row1["INVOICE NO"] ="";
        row1["INVOICE DATE"] = "";
      //  row1["APPROVAL NO"] ="";
        row1["ITEM DESCRIPTION"] ="";
        row1["VENDOR NAME"] = "Total";
        row1["AMOUNT PAID"] = Total;
        row1["PAID DATE"] = "";
        row1["BUDGET HEAD"] = "";
        row1["VOUCHER NO"] = "";
        row1["LEDGER HEAD"] = "";
        row1["EXPENSE HEAD"] = "";
        row1["BANK"] = "";
        row1["JOURNAL VOUCHER"] = "";
        row1["PAYMENT VOUCHER"] = "";

        dt.Rows.Add(row1);

        return dt;
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        ds = objRPBController.GetReimbursementBillReport(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToDateTime(txtApprovalFormDate.Text), Convert.ToDateTime(txtApprovalToDate.Text), Session["Comp_Code"].ToString());

        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
        string attachment = "attachment; filename=ReimbursementBillReport.xls";
        DataTable dt = new DataTable();
        dt = Addtable(ds);
        gvReibursement.DataSource = dt;
        gvReibursement.DataBind();
        AddHeader();
      //  AddFooter(80);
        gvReibursement.FooterStyle.Font.Bold = true;
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", attachment);
        Response.AppendHeader("Refresh", ".5; ReimbursementBillReport.aspx");
        Response.Charset = "";
        Response.ContentType = "application/" + ContentType;
        StringWriter sw1 = new StringWriter();
        HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
        gvReibursement.RenderControl(htw1);
        Response.Output.Write(sw1.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();

    }


    private void AddHeader()
    {

        DataSet ds1 = objCommon.FillDropDown("reff", "CollegeName", "College_address", "College_code=" + Session["colcode"].ToString(), "");

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = ds1.Tables[0].Rows[0]["CollegeName"].ToString().ToUpper();
        HeaderCell.ColumnSpan = 16;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gvReibursement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = ds1.Tables[0].Rows[0]["College_address"].ToString().ToUpper();
        HeaderCell1.ColumnSpan = 16;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gvReibursement.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = " ";
        HeaderCell2.ColumnSpan = 16;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gvReibursement.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "BILL REIMBURSEMENT( FROM:  " + Convert.ToDateTime(txtApprovalFormDate.Text).ToString("yyyy.MM.dd") + "    TO:   " + Convert.ToDateTime(txtApprovalToDate.Text).ToString("yyyy.MM.dd") + ")";
        HeaderCell3.ColumnSpan = 16;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gvReibursement.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell4 = new TableCell();
        HeaderCell4 = new TableCell();
        HeaderCell4.Text = "DEPARTMENT: " + ds.Tables[0].Rows[0]["SUBDEPT"].ToString().ToUpper();
        HeaderCell4.ColumnSpan = 16;
        HeaderCell4.BackColor = System.Drawing.Color.White;
        HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow4.Cells.Add(HeaderCell4);
        gvReibursement.Controls[0].Controls.AddAt(4, HeaderGridRow4);

        gvReibursement.FooterStyle.Font.Bold = true;
        gvReibursement.FooterStyle.Font.Size = 12;
    }


    //private void AddFooter(int index)
    //{

    //    DataSet ds1 = objCommon.FillDropDown("reff", "CollegeName", "College_address", "College_code=" + Session["colcode"].ToString(), "");

    //    GridViewRow HeaderGridRow = new GridViewRow(0, index, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell = new TableCell();
    //    HeaderCell = new TableCell();
    //    HeaderCell.Text = ds1.Tables[0].Rows[0]["CollegeName"].ToString().ToUpper();
    //    HeaderCell.ColumnSpan = 11;
    //    HeaderCell.BackColor = System.Drawing.Color.White;
    //    HeaderCell.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow.Cells.Add(HeaderCell);
    //    gvReibursement.Controls[0].Controls.AddAt(index, HeaderGridRow);

    //    GridViewRow HeaderGridRow1 = new GridViewRow(1, index, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell1 = new TableCell();
    //    HeaderCell1.Text = ds1.Tables[0].Rows[0]["College_address"].ToString().ToUpper();
    //    HeaderCell1.ColumnSpan = 11;
    //    HeaderCell1.BackColor = System.Drawing.Color.White;
    //    HeaderCell1.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow1.Cells.Add(HeaderCell1);
    //    gvReibursement.Controls[0].Controls.AddAt(1, HeaderGridRow1);

    //    GridViewRow HeaderGridRow2 = new GridViewRow(2, index, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell2 = new TableCell();
    //    HeaderCell2 = new TableCell();
    //    HeaderCell2.Text = " ";
    //    HeaderCell2.ColumnSpan = 11;
    //    HeaderCell2.BackColor = System.Drawing.Color.White;
    //    HeaderCell2.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow2.Cells.Add(HeaderCell2);
    //    gvReibursement.Controls[0].Controls.AddAt(2, HeaderGridRow2);

    //    GridViewRow HeaderGridRow3 = new GridViewRow(3, index, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell3 = new TableCell();
    //    HeaderCell3 = new TableCell();
    //    HeaderCell3.Text = "BILL REIMBURSEMENT( FROM:  " + Convert.ToDateTime(txtApprovalFormDate.Text).ToString("yyyy.MM.dd") + "    TO:   " + Convert.ToDateTime(txtApprovalToDate.Text).ToString("yyyy.MM.dd") + ")";
    //    HeaderCell3.ColumnSpan = 11;
    //    HeaderCell3.BackColor = System.Drawing.Color.White;
    //    HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow3.Cells.Add(HeaderCell3);
    //    gvReibursement.Controls[0].Controls.AddAt(3, HeaderGridRow3);

    //    GridViewRow HeaderGridRow4 = new GridViewRow(4, index, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell4 = new TableCell();
    //    HeaderCell4 = new TableCell();
    //    HeaderCell4.Text = "DEPARTMENT: " + ds.Tables[0].Rows[0]["SUBDEPT"].ToString().ToUpper();
    //    HeaderCell4.ColumnSpan = 11;
    //    HeaderCell4.BackColor = System.Drawing.Color.White;
    //    HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow4.Cells.Add(HeaderCell4);
    //    gvReibursement.Controls[0].Controls.AddAt(4, HeaderGridRow4);

    //    gvReibursement.FooterStyle.Font.Bold = true;
    //    gvReibursement.FooterStyle.Font.Size = 12;
    //}
}

