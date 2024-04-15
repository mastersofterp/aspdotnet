//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : 
// CREATION DATE : 06-Oct-2012                                               
// CREATED BY    : KAPIL BUDHLANI                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the bankbook Report
//=================================================================================
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

using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;
using System.IO;
using IITMS.NITPRM;

public partial class Account_BankBookReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExportExcelController objExport = new ExportExcelController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common();
            //objCcbc = new CombinedCashBankBookController();
            //objrpc = new ReceiptPaymentController();

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";

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
                    objCommon.DisplayUserMessage(UPDLedger, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    //txtFrmDate.Text = DateTime.Now.ToShortDateString();
                    SetFinancialYear();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    objCommon.FillDropDownList(ddlbank, "ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=2", "PARTY_NAME");
                }
            }
            rdbWithNarration.Checked = true;
            rdbWithoutNarration.Checked = false;
        }
    }

    private void SetFinancialYear()
    {
        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");

            string Fdate = DateTime.Now.ToString();

            if (Convert.ToDateTime(Session["fin_date_to"]) > Convert.ToDateTime(DateTime.Now.ToString()))
            {
                //added by tanu 24/02/2022

                Fdate = "01/" + DateTime.Parse(Fdate).Month.ToString() + "/" + DateTime.Parse(Fdate).Year.ToString();
                txtFrmDate.Text = Convert.ToDateTime(Fdate).ToString("dd/MM/yyyy");

                if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
                {
                    txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                    txtFrmDate.Focus();
                    Fdate = txtFrmDate.Text;

                }
            }
            else
            {
                string Fdate1 = string.Empty;
                Fdate = "01/" + Convert.ToDateTime(Session["fin_date_to"]).Month.ToString() + "/" + Convert.ToDateTime(Session["fin_date_to"]).Year.ToString();
                txtFrmDate.Text = Convert.ToDateTime(Fdate).ToString("dd/MM/yyyy");

                //if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
                //{
                //    txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                //    txtFrmDate.Focus();

                //}
            }

            string Todate = DateTime.Parse(Fdate).AddMonths(1).ToString();
            Todate = DateTime.Parse(Todate).AddDays(-1).ToString();
            txtUptoDate.Text = Convert.ToDateTime(Todate).ToString("dd/MM/yyyy");
            ViewState["Todate"] = txtUptoDate.Text;

            if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtUptoDate.Focus();
                ViewState["Todate"] = txtUptoDate.Text;
            }
        }
        dtr.Close();


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlbank.SelectedIndex <= 0)
        {
            objCommon.DisplayUserMessage(UPDLedger, "PLease Select Bank", this);

            return;
        }
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
        //if (RdbMonthWise.Checked)
        //{
        if (rdbMonthWise.Checked)
        {
            if (chkRunning.Checked)
                ShowReport("CashBook", "BankBook_running.rpt");
            else
                ShowReport("CashBook", "BankBook.rpt");
        }
        if (rdbDayWise1.Checked)
        {
            if (chkRunning.Checked)
                ShowReport("CashBook", "BankBookDayWise_running.rpt");
            else
                ShowReport("CashBook", "BankBookDayWise.rpt");
        }
        //}
        //else
        //{
        //    ShowReport("BANK BOOK REPORT", "BankBookMonthWise.rpt");
        //}
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string ClMode = string.Empty;
            //ClMode = txtmd.Text.ToString().Trim();

            string LedgerName = string.Empty;
            LedgerName = ddlbank.SelectedItem.Text;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbWithNarration.Checked)
            {
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + ddlbank.SelectedValue.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + "0";
            }
            else
            {
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + ddlbank.SelectedValue.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + "1";

            }
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

        }
    }

    protected void rdbWithNarration_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbWithNarration.Checked)
        {
            rdbWithoutNarration.Checked = false;

        }
    }
    protected void rdbWithoutNarration_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbWithoutNarration.Checked)
        {
            rdbWithNarration.Checked = false;
        }
    }
    protected void RdbYearly_CheckedChanged(object sender, EventArgs e)
    {
        divCalender.Visible = false;
        SetFinancialYear();
    }
    protected void RdbMonthWise_CheckedChanged(object sender, EventArgs e)
    {
        divCalender.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlbank.SelectedIndex<=0)
        {
            objCommon.DisplayUserMessage(UPDLedger, "PLease Select Bank", this);
          
            return;
        }
        DataSet dsBankData = objExport.GetDataForExcelBankBook(Session["comp_code"].ToString(), ddlbank.SelectedValue.ToString(), Convert.ToDateTime(txtFrmDate.Text.ToString().Trim()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text.ToString().Trim()).ToString("dd-MMM-yyyy"));
        string[] columns = new string[2];
        columns[0] = "VOUCHER_NO";
        columns[1] = "Vch_Type";
        DataTable dtVouchers = dsBankData.Tables[0].DefaultView.ToTable(true, columns);
        DataTable dtExcelData = new DataTable();
        for (int i = 0; i < dtVouchers.Rows.Count; i++)
        {
            if (!dtExcelData.Columns.Contains("TRANSACTION_DATE"))
                dtExcelData.Columns.Add("TRANSACTION_DATE");

            if (!dtExcelData.Columns.Contains("PARTY_NAME"))
                dtExcelData.Columns.Add("PARTY_NAME");

            if (!dtExcelData.Columns.Contains("CLUB_AMT"))
                dtExcelData.Columns.Add("CLUB_AMT");

            if (!dtExcelData.Columns.Contains("Vch_Type"))
                dtExcelData.Columns.Add("Vch_Type");

            if (!dtExcelData.Columns.Contains("VOUCHER_NO"))
                dtExcelData.Columns.Add("VOUCHER_NO");

            if (!dtExcelData.Columns.Contains("TOTCREDIT"))
                dtExcelData.Columns.Add("TOTCREDIT");

            if (!dtExcelData.Columns.Contains("TOTDEBIT"))
                dtExcelData.Columns.Add("TOTDEBIT");

            if (!dtExcelData.Columns.Contains("CHQ_NO"))
                dtExcelData.Columns.Add("CHQ_NO");


            DataView dvData = new DataView(dsBankData.Tables[0]);
            dvData.RowFilter = "VOUCHER_NO=" + dtVouchers.Rows[i]["VOUCHER_NO"].ToString() + " and Vch_Type='" + dtVouchers.Rows[i]["Vch_Type"].ToString() + "'";
            DataTable drVoucherData = dvData.ToTable();

            if (i == 0)
            {
                DataRow drOpeningRows = dtExcelData.NewRow();
                drOpeningRows["TRANSACTION_DATE"] = Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy");
                if ((Convert.ToDecimal(objCommon.FillDropDown("temp_bank_book_excel", "top 1 openingamt as total", "", "", "").Tables[0].Rows[0][0].ToString())) < 0)
                {
                    drOpeningRows["TOTCREDIT"] = objCommon.FillDropDown("temp_bank_book_excel", "top 1 -openingamt as total", "", "", "").Tables[0].Rows[0][0].ToString();
                    drOpeningRows["TOTDEBIT"] = "";
                }
                else
                {
                    drOpeningRows["TOTCREDIT"] = "";
                    drOpeningRows["TOTDEBIT"] = objCommon.FillDropDown("temp_bank_book_excel", "top 1 openingamt as total", "", "", "").Tables[0].Rows[0][0].ToString();
                }

                drOpeningRows["PARTY_NAME"] = "Opening Balance";
                drOpeningRows["CLUB_AMT"] = "";
                drOpeningRows["Vch_Type"] = "";
                drOpeningRows["VOUCHER_NO"] = "";
                drOpeningRows["CHQ_NO"] = "";
                dtExcelData.Rows.Add(drOpeningRows);
            }
            for (int j = 0; j < drVoucherData.Rows.Count; j++)
            {
                DataRow drRows = dtExcelData.NewRow();
                if (j == 0)
                {
                    drRows["TRANSACTION_DATE"] = "";
                    drRows["TOTCREDIT"] = "";
                    drRows["TOTDEBIT"] = "";
                    drRows["PARTY_NAME"] = drVoucherData.Rows[j]["PARTY_NAME"];
                    drRows["CLUB_AMT"] = drVoucherData.Rows[j]["CLUB_AMT"].ToString() + drVoucherData.Rows[j]["TRAN"].ToString();
                    drRows["Vch_Type"] = drVoucherData.Rows[j]["Vch_Type"];
                    drRows["VOUCHER_NO"] = drVoucherData.Rows[j]["VOUCHER_NO"];
                    drRows["TRANSACTION_DATE"] = Convert.ToDateTime(drVoucherData.Rows[j]["TRANSACTION_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    if ((Convert.ToDecimal(objCommon.FillDropDown("temp_bank_book_excel", "sum(isnull(CREDIT,0))-sum(isnull(DEBIT,0)) as total", "", "VOUCHER_NO=" + drVoucherData.Rows[j]["VOUCHER_NO"].ToString() + " and Vch_Type='" + drVoucherData.Rows[j]["Vch_Type"].ToString() + "'", "").Tables[0].Rows[0][0].ToString())) < 0)
                    {
                        drRows["TOTCREDIT"] = objCommon.FillDropDown("temp_bank_book_excel", "sum(isnull(DEBIT,0))-sum(isnull(CREDIT,0)) as total", "", "VOUCHER_NO=" + drVoucherData.Rows[j]["VOUCHER_NO"].ToString() + " and Vch_Type='" + drVoucherData.Rows[j]["Vch_Type"].ToString() + "'", "").Tables[0].Rows[0][0].ToString();
                        drRows["TOTDEBIT"] = "";
                    }
                    else
                    {
                        drRows["TOTCREDIT"] = "";
                        drRows["TOTDEBIT"] = objCommon.FillDropDown("temp_bank_book_excel", "sum(isnull(CREDIT,0))-sum(isnull(DEBIT,0)) as total", "", "VOUCHER_NO=" + drVoucherData.Rows[j]["VOUCHER_NO"].ToString() + " and Vch_Type='" + drVoucherData.Rows[j]["Vch_Type"].ToString() + "'", "").Tables[0].Rows[0][0].ToString();
                    }
                    drRows["CHQ_NO"] = drVoucherData.Rows[j]["CHQ_NO"];
                    dtExcelData.Rows.Add(drRows);
                }
                else
                {
                    drRows["TRANSACTION_DATE"] = "";
                    drRows["PARTY_NAME"] = drVoucherData.Rows[j]["PARTY_NAME"];
                    drRows["CLUB_AMT"] = drVoucherData.Rows[j]["CLUB_AMT"].ToString() + drVoucherData.Rows[j]["TRAN"].ToString();
                    drRows["Vch_Type"] = drVoucherData.Rows[j]["Vch_Type"];
                    drRows["VOUCHER_NO"] = drVoucherData.Rows[j]["VOUCHER_NO"];
                    drRows["TOTCREDIT"] = "";
                    drRows["TOTDEBIT"] = "";
                    drRows["CHQ_NO"] = "";
                    dtExcelData.Rows.Add(drRows);
                }
            }
            if (i == dtVouchers.Rows.Count - 1)
            {
                //For total credit and debit
                DataRow drTotalRows = dtExcelData.NewRow();
                drTotalRows["TRANSACTION_DATE"] = "";
                if ((Convert.ToDecimal(objCommon.FillDropDown("temp_bank_book_excel", "top 1 openingamt as total", "", "", "").Tables[0].Rows[0][0].ToString())) < 0)
                {
                    drTotalRows["TOTCREDIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "-total+sum(debit)", "", "", "").Tables[0].Rows[0][0].ToString();
                    drTotalRows["TOTDEBIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "sum(credit)", "", "", "").Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    drTotalRows["TOTCREDIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "sum(debit)", "", "", "").Tables[0].Rows[0][0].ToString();
                    drTotalRows["TOTDEBIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "total+sum(credit)", "", "", "").Tables[0].Rows[0][0].ToString();
                }

                drTotalRows["PARTY_NAME"] = "Total";
                drTotalRows["CLUB_AMT"] = "";
                drTotalRows["Vch_Type"] = "";
                drTotalRows["VOUCHER_NO"] = "";
                drTotalRows["CHQ_NO"] = "";
                dtExcelData.Rows.Add(drTotalRows);

                //For Closing Balance
                DataRow drClbalRows = dtExcelData.NewRow();
                drTotalRows["TRANSACTION_DATE"] = "";
                if ((Convert.ToDecimal(objCommon.FillDropDown("temp_bank_book_excel", "top 1 openingamt as total", "", "", "").Tables[0].Rows[0][0].ToString())) < 0)
                {
                    drClbalRows["TOTCREDIT"] = "";
                    drClbalRows["TOTDEBIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "((-total+sum(DEBIT))-sum(credit))", "", "", "").Tables[0].Rows[0][0].ToString();

                    if (Convert.ToDecimal(drClbalRows["TOTDEBIT"]) < 0)
                    {
                        drClbalRows["TOTCREDIT"] = Convert.ToDecimal(drClbalRows["TOTDEBIT"]) * -1;
                        drClbalRows["TOTDEBIT"] = "";
                    }
                }
                else
                {
                    drClbalRows["TOTCREDIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(total+sum(credit))-sum(DEBIT)", "", "", "").Tables[0].Rows[0][0].ToString();
                    drClbalRows["TOTDEBIT"] = "";

                    if (Convert.ToDecimal(drClbalRows["TOTCREDIT"]) < 0)
                    {
                        drClbalRows["TOTDEBIT"] = Convert.ToDecimal(drClbalRows["TOTCREDIT"]) * -1;
                        drClbalRows["TOTCREDIT"] = "";
                    }
                }   

                drClbalRows["PARTY_NAME"] = "Closing Balance";
                drClbalRows["CLUB_AMT"] = "";
                drClbalRows["Vch_Type"] = "";
                drClbalRows["VOUCHER_NO"] = "";
                drClbalRows["CHQ_NO"] = "";
                dtExcelData.Rows.Add(drClbalRows);

                //For Grand Total
                DataRow drGrdTotRows = dtExcelData.NewRow();
                drGrdTotRows["TRANSACTION_DATE"] = "";
                string totcredit = dtExcelData.Rows[dtExcelData.Rows.Count - 1]["TOTCREDIT"].ToString();
                if (totcredit != string.Empty)
                    drGrdTotRows["TOTCREDIT"] = Convert.ToDecimal(dtExcelData.Rows[dtExcelData.Rows.Count - 2]["TOTCREDIT"].ToString()) + Convert.ToDecimal(totcredit);
                else
                    drGrdTotRows["TOTCREDIT"] = Convert.ToDecimal(dtExcelData.Rows[dtExcelData.Rows.Count - 2]["TOTCREDIT"].ToString()) + Convert.ToDecimal(0);

                string totDebit = dtExcelData.Rows[dtExcelData.Rows.Count - 1]["TOTDEBIT"].ToString();
                if (totDebit != string.Empty)
                    drGrdTotRows["TOTDEBIT"] = Convert.ToDecimal(dtExcelData.Rows[dtExcelData.Rows.Count - 2]["TOTDEBIT"].ToString()) + Convert.ToDecimal(totDebit);
                else
                    drGrdTotRows["TOTDEBIT"] = Convert.ToDecimal(dtExcelData.Rows[dtExcelData.Rows.Count - 2]["TOTDEBIT"].ToString()) + Convert.ToDecimal(0);

                drGrdTotRows["PARTY_NAME"] = "Grand Total";
                drGrdTotRows["CLUB_AMT"] = "";
                drGrdTotRows["Vch_Type"] = "";
                drGrdTotRows["VOUCHER_NO"] = "";
                drGrdTotRows["CHQ_NO"] = "";
                dtExcelData.Rows.Add(drGrdTotRows);
            }
        }

        gvTrialBalance.DataSource = dtExcelData;
        gvTrialBalance.DataBind();

        if (dsBankData.Tables[0].Rows.Count>0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "Bank Book";
            HeaderCell1.ColumnSpan = 8;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            gvTrialBalance.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddlbank.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            gvTrialBalance.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow3.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(3, HeaderGridRow3);


            GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderGridRow4.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(4, HeaderGridRow4);

            //gvTrialBalance.Controls[0].Controls.AddAt(0, HeaderGridRow);

            string attachment = "attachment; filename=" + ddlbank.SelectedItem.Text.Replace(" ", "_") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvTrialBalance.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        rdbMonthWise.Checked = true;
        SetFinancialYear();
        ddlbank.SelectedIndex = 0;
        rdbWithNarration.Checked = true;
        chkRunning.Checked = false;
    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date should be in financial year", this.Page);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }
        string fDate = txtFrmDate.Text;
        string ToDate = DateTime.Parse(fDate).AddMonths(1).ToString();
        ToDate = DateTime.Parse(ToDate).AddDays(-1).ToString();

        txtUptoDate.Text = Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");
        ViewState["Todate"] = txtUptoDate.Text;
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            ViewState["Todate"] = txtUptoDate.Text;
            return;
        }


        txtUptoDate.Focus();
    }
    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(ViewState["Todate"])) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "The period of From Date and UpTo Date should not be more than one month", this.Page);
            txtUptoDate.Text = Convert.ToDateTime(ViewState["Todate"]).ToString("dd/MM/yyyy");
            ViewState["Todate"] = txtUptoDate.Text;
            txtUptoDate.Focus();
            return;
        }
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            ViewState["Todate"] = txtUptoDate.Text;
            return;
        }
        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date.", this);
            SetFinancialYear();
            txtUptoDate.Focus();
            return;
        }
    }
}
