using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class Acc_ledgerReportGrid : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Grid_Entity objGrid = new Grid_Entity();
    Grid_Controller objGridController = new Grid_Controller();
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
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                }
                //Page Authorization
                //CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                SetFinancialYear();
                if (Request.QueryString["ISBACK"] == null)
                {
                    FillGrid(Request.QueryString["ledger"].ToString(), Request.QueryString["party_no"].ToString());

                    lblLedger.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO=" + Request.QueryString["party_no"].ToString());
                }
                if (Request.QueryString["ISBACK"] != null)
                {
                    string Script = "CloseWindow()";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
                    Session["ISBACK"] = "True";
                }
            }
        }
    }


    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();

        }
        dtr.Close();
    }

    private void FillGrid(string ledger, string party_no)
    {
        string PaymentTypeNo = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PAYMENT_TYPE_NO", "PARTY_NO=" + party_no);
        DataSet LedgerReport = new DataSet();
        DataSet dsVoucher = new DataSet();
        DataSet dsBalances = new DataSet();

        objGrid.CompCode = Session["comp_code"].ToString();
        //objGrid.FromDate = Convert.ToDateTime(Session["fin_date_from"].ToString());
        //objGrid.ToDate = Convert.ToDateTime(Session["fin_date_to"].ToString());

        objGrid.FromDate = Convert.ToDateTime(Request.QueryString["fromDate"].ToString());
        objGrid.ToDate = Convert.ToDateTime(Request.QueryString["Todate"].ToString());

        objGrid.Ledger = party_no;
        string from = objGrid.FromDate.ToString("dd-MMM-yyyy");

        lblFrm.Text = objGrid.FromDate.ToString("dd-MMM-yyyy");
        lblTo.Text = objGrid.ToDate.ToString("dd-MMM-yyyy");


        if (PaymentTypeNo == "1" || PaymentTypeNo == "2")
        {
            dsVoucher = objGridController.LedgerReportVoucherNoCashBank(objGrid);
            LedgerReport = objGridController.LedgerReportForCashBank(objGrid);
            dsBalances = objGridController.LedgerReportBalnces(PaymentTypeNo, Session["comp_code"].ToString(), party_no, from);
        }
        else
        {
            dsVoucher = objGridController.LedgerReportvOUCHERNO(objGrid);
            LedgerReport = objGridController.LedgerReport(objGrid);
            dsBalances = objGridController.LedgerReportBalnces(PaymentTypeNo, Session["comp_code"].ToString(), party_no, from);
        }

        DataSet dsLedger = LedgerReport;

        if (dsVoucher != null)
        {
            if (dsVoucher.Tables[0].Rows.Count > 0)
            {
                DataView dvLedger = dsVoucher.Tables[0].DefaultView;
                dvLedger.RowFilter = "VOUCHER_NO is not null";
                DataTable dtLedger = dvLedger.ToTable();
                RptData.DataSource = dtLedger;
                RptData.DataBind(); string Trnsfer_Entry = string.Empty;
                for (int i = 0; i < RptData.Items.Count; i++)
                {
                    ListView lvGrp = RptData.Items[i].FindControl("lvGrp") as ListView;
                    ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
                    Label lblVchType = RptData.Items[i].FindControl("lblvchtype") as Label;
                    HiddenField hdnTransferEntry = RptData.Items[i].FindControl("hdnTransferEntry") as HiddenField;
                    if (hdnTransferEntry.Value == "1")
                    { Trnsfer_Entry = "1"; }
                    if (hdnTransferEntry.Value == "True")
                    { Trnsfer_Entry = "1"; }
                    if (hdnTransferEntry.Value == "False")
                    { Trnsfer_Entry = "0"; }
                    if (hdnTransferEntry.Value == "0")
                    { Trnsfer_Entry = "0"; }
                    if (Trnsfer_Entry == "1")
                    {
                        btnEdit.Attributes.Add("onClick", "alert('Transfer Entry Can not Edit')");
                    }
                    else
                    {
                        btnEdit.Attributes.Add("onClick", "VoucherModification('" + btnEdit.CommandArgument + "," + lblVchType.Text + "," + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MM-yyyy")) + "," + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MM-yyyy")) + "','" + Request.QueryString["ledger"].ToString() + "','" + Request.QueryString["party_no"].ToString() + "')");

                    }

                    DataView dvLedger1 = LedgerReport.Tables[0].DefaultView;
                    dvLedger1.RowFilter = "VOUCHER_NO='" + btnEdit.CommandArgument + "' and Vch_Type='" + lblVchType.Text + "'";
                    DataTable dtLedger1 = dvLedger1.ToTable();
                    lvGrp.DataSource = dtLedger1;
                    lvGrp.DataBind();
                    for (int j = 0; j < lvGrp.Items.Count; j++)
                    {
                        HiddenField hdnTransferEntry1 = lvGrp.Items[j].FindControl("hdnTransferEntry") as HiddenField;
                        Label TransactionDate = lvGrp.Items[j].FindControl("TransactionDate") as Label;
                        DateTime date = Convert.ToDateTime(dtLedger1.Rows[j]["TRANSACTION_DATE"].ToString());
                        Label lblParty = lvGrp.Items[j].FindControl("lblPartyName") as Label;
                        TransactionDate.Text = Convert.ToString(date.ToString("dd-MMM-yyyy"));
                        if (hdnTransferEntry1.Value == "True")
                        {
                            lblParty.Attributes.Add("onClick", "alert('Transfer Entry Can not Edit')");
                        }
                        else
                        {
                            string trantype = string.Empty;
                            if (lblVchType.Text == "Payment")
                                trantype = "P";
                            else if (lblVchType.Text == "Receipt")
                                trantype = "R";
                            else if (lblVchType.Text == "Contra")
                                trantype = "C";
                            else
                                trantype = "J";
                            //string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
                            string voucher_seq = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "VOUCHER_SQN", "VOUCHER_NO='" + btnEdit.CommandArgument + "' and TRANSACTION_TYPE='" + trantype + "' and TRANSACTION_DATE between '" + Convert.ToDateTime(Request.QueryString["fromDate"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["Todate"].ToString()).ToString("dd-MMM-yyyy") + "'");
                         //   lblParty.Attributes.Add("onClick", "VoucherModification('" + voucher_seq + "," + lblVchType.Text + "," + Request.QueryString["fromDate"].ToString() + "," + Request.QueryString["Todate"].ToString() + "','" + Request.QueryString["ledger"].ToString() + "','" + Request.QueryString["party_no"].ToString() + "','" + Request.QueryString["fromDate"].ToString() + "','" + Request.QueryString["Todate"].ToString() + "')");
                        }
                    }
                }

                Label lblopDebit = RptData.Controls[0].FindControl("lblopDebit") as Label;
                Label lblopCredit = RptData.Controls[0].FindControl("lblopCredit") as Label;
                Label lblclDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblclDebit") as Label;
                Label lblClCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblClCredit") as Label;
                Label lbltotDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lbltotDebit") as Label;
                Label lblTotCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotCredit") as Label;
                lblopDebit.Text = dsBalances.Tables[0].Rows[0]["OpDebit"].ToString();
                lblopCredit.Text = dsBalances.Tables[0].Rows[0]["opCredit"].ToString();
                lblclDebit.Text = dsBalances.Tables[0].Rows[0]["clBalDebit"].ToString();
                lblClCredit.Text = dsBalances.Tables[0].Rows[0]["clBalCredit"].ToString();
                lbltotDebit.Text = dsBalances.Tables[0].Rows[0]["totDebit"].ToString();
                lblTotCredit.Text = dsBalances.Tables[0].Rows[0]["totCredit"].ToString();
            }
        }
    }

    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {

            string VCH_TYPE = string.Empty;

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));


            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_VCH_NO=" + VchNo.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDMainGroup, UPDMainGroup.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label lblVchType = e.Item.FindControl("lblvchtype") as Label;
        if (lblVchType != null)
        {
            string Voucher_No = e.CommandArgument.ToString();
            if (e.CommandName.ToString().Trim() == "VoucherPrint")
            {
                string isFourSign = objCommon.LookUp("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
                if (isFourSign == "N")
                {
                    if (lblVchType.Text.ToString().Trim() == "Payment" || lblVchType.Text.ToString().Trim() == "Receipt")
                    {

                        ShowVoucherPrintReport("Voucher", "PmtRcptCashVoucherRpt.rpt", lblVchType.Text.ToString().Trim(), Voucher_No);

                    }
                    else
                    {
                        ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", lblVchType.Text.ToString().Trim(), Voucher_No);
                    }
                }
                else if (isFourSign == "Y")
                {
                    if (lblVchType.Text.ToString().Trim() == "Payment" || lblVchType.Text.ToString().Trim() == "Receipt")
                    {
                        ShowVoucherPrintReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", lblVchType.Text.ToString().Trim(), Voucher_No);
                    }
                    else
                    {
                        ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Format2.rpt", lblVchType.Text.ToString().Trim(), Voucher_No);
                    }
                }

            }
        }
    }

    private void ShowReportForBankAndCash(string reportTitle, string rptFileName, string LedgerName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string ClMode = string.Empty;
            //ClMode = txtmd.Text.ToString().Trim();



            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + Request.QueryString["fromDate"].ToString() + " to " + Request.QueryString["Todate"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(Request.QueryString["fromDate"].ToString()).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(Request.QueryString["Todate"].ToString()).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + "1";


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDMainGroup, UPDMainGroup.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReportForGrid(string reportTitle, string rptFileName, string LedgerName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string ClMode;
            ClMode = "";



            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + Request.QueryString["fromDate"].ToString() + " to " + Request.QueryString["Todate"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(Request.QueryString["fromDate"].ToString()).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(Request.QueryString["Todate"].ToString()).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + 1;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UPDMainGroup, UPDMainGroup.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string PaymentTypeNo = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PAYMENT_TYPE_NO", "PARTY_NO=" + Request.QueryString["party_no"].ToString());
        string LedgerName = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PARTY_NAME", "PARTY_NO=" + Request.QueryString["party_no"].ToString());
        if (PaymentTypeNo == "1" || PaymentTypeNo == "2")
        {
            ShowReportForBankAndCash("Ledger Report", "BankBook.rpt", Request.QueryString["party_no"].ToString());
        }
        else
        {
            ShowReportForGrid("Ledger Report", "LedgerBook.rpt", Request.QueryString["party_no"].ToString());
        }
    }
}
