//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNTING VOUCHERS MODIFICATIONS                                                     
// CREATION DATE : 12-NOV-2009                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class LedgerReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Grid_Entity objGrid = new Grid_Entity();
    Grid_Controller objGridController = new Grid_Controller();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    public static DataTable dt1 = new DataTable();
    string back = string.Empty;
    string space1 = "     ".ToString();
    string space2 = "          ".ToString();
    string space3 = string.Empty;
    DataTable dt = new DataTable();
    public static int RowIndex = -1;
    string[] para;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["WithoutCashBank"] = "Y";
        if (!Page.IsPostBack)
        {
            SetDataColumn();
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
                    objCommon.DisplayMessage("Select company/cash book.", this);
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["action"] = "add";
                }
            }
            SetFinancialYear();
        }
        divMsg.InnerHtml = string.Empty;
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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }
    private void SetDataColumn()
    {
        DataColumn dc = new DataColumn();
        dc.ColumnName = "Date";
        dt.Columns.Add(dc);

        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "Particulars";
        dt.Columns.Add(dc1);

        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "VchType";
        dt.Columns.Add(dc2);

        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "VchNo";
        dt.Columns.Add(dc3);

        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "Debit";
        dt.Columns.Add(dc4);

        DataColumn dc5 = new DataColumn();
        dc5.ColumnName = "Credit";
        dt.Columns.Add(dc5);
        Session["DatatableMod"] = dt;


    }

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        //BindGrid();

    }


    //private void BindGrid()
    //{
    //    string LedgerName = txtAcc.Text.Split('*')[0].ToString();
    //    //string PARTY_NO = Request.QueryString["party_no"].ToString();
    //    string PARTY_NO = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME='" + LedgerName + "'");
    //    lblLedger.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO=" + PARTY_NO);
    //    objGrid.CompCode = Session["comp_code"].ToString();
    //    string PaymentTypeNo = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PAYMENT_TYPE_NO", "PARTY_NO=" + PARTY_NO);
    //    DataSet LedgerReport = new DataSet();
    //    DataSet dsVoucher = new DataSet();
    //    DataSet dsBalances = new DataSet();
    //    grid.Visible = true;
    //    // string LedgerName = txtAcc.Text.Split('*')[0].ToString();
    //    //lblFrm.Text = objGrid.FromDate.ToString("dd-MMM-yyyy");
    //    //lblTo.Text = objGrid.ToDate.ToString("dd-MMM-yyyy");
    //    lblFrm.Text = txtFrmDate.Text;
    //    lblTo.Text = txtUptoDate.Text;
    //    objGrid.FromDate = Convert.ToDateTime(lblFrm.Text);
    //    objGrid.ToDate = Convert.ToDateTime(lblTo.Text);
    //    objGrid.Ledger = PARTY_NO;
    //    string from = objGrid.FromDate.ToString("dd-MMM-yyyy");
    //    string to = objGrid.ToDate.ToString("dd-MMM-yyyy");
    //    dsVoucher = objGridController.LedgerReportvOUCHERNO(objGrid);
    //    LedgerReport = objGridController.LedgerReport(objGrid);
    //    dsBalances = objGridController.LedgerReportBalnces(PaymentTypeNo, Session["comp_code"].ToString(), PARTY_NO, from);

    //    if (dsVoucher.Tables[0].Rows.Count > 0)
    //    {
    //        DataView dvLedger = dsVoucher.Tables[0].DefaultView;
    //        dvLedger.RowFilter = "VOUCHER_NO is not null";
    //        DataTable dtLedger = dvLedger.ToTable();
    //        RptData.DataSource = dtLedger;
    //        RptData.DataBind(); string Trnsfer_Entry = string.Empty;
    //        for (int i = 0; i < RptData.Items.Count; i++)
    //        {
    //            ListView lvGrp = RptData.Items[i].FindControl("lvGrp") as ListView;
    //            ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
    //            Label lblVchType = RptData.Items[i].FindControl("lblvchtype") as Label;
    //            HiddenField hdnTransferEntry = RptData.Items[i].FindControl("hdnTransferEntry") as HiddenField;
    //            if (hdnTransferEntry.Value == "1")
    //            { Trnsfer_Entry = "1"; }
    //            if (hdnTransferEntry.Value == "True")
    //            { Trnsfer_Entry = "1"; }
    //            if (hdnTransferEntry.Value == "False")
    //            { Trnsfer_Entry = "0"; }
    //            if (hdnTransferEntry.Value == "0")
    //            { Trnsfer_Entry = "0"; }
    //            if (Trnsfer_Entry == "1")
    //            {
    //                btnEdit.Attributes.Add("onClick", "alert('Transfer Entry Can not Edit')");
    //            }
    //            else
    //            {
    //                btnEdit.Attributes.Add("onClick", "VoucherModification('" + btnEdit.CommandArgument + "," + lblVchType.Text + "," + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MM-yyyy")) + "," + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MM-yyyy")) + "','" + LedgerName + "','" + PARTY_NO + "')");

    //            }

    //            DataView dvLedger1 = LedgerReport.Tables[0].DefaultView;
    //            dvLedger1.RowFilter = "VOUCHER_NO='" + btnEdit.CommandArgument + "' and Vch_Type='" + lblVchType.Text + "'";
    //            DataTable dtLedger1 = dvLedger1.ToTable();
    //            lvGrp.DataSource = dtLedger1;
    //            lvGrp.DataBind();
    //            for (int j = 0; j < lvGrp.Items.Count; j++)
    //            {
    //                HiddenField hdnTransferEntry1 = lvGrp.Items[j].FindControl("hdnTransferEntry") as HiddenField;
    //                Label TransactionDate = lvGrp.Items[j].FindControl("TransactionDate") as Label;
    //                DateTime date = Convert.ToDateTime(dtLedger1.Rows[j]["TRANSACTION_DATE"].ToString());
    //                Label lblParty = lvGrp.Items[j].FindControl("lblPartyName") as Label;
    //                TransactionDate.Text = Convert.ToString(date.ToString("dd-MMM-yyyy"));
    //                if (hdnTransferEntry1.Value == "True")
    //                {
    //                    lblParty.Attributes.Add("onClick", "alert('Transfer Entry Can not Edit')");
    //                }
    //                else
    //                {
    //                    string trantype = string.Empty;
    //                    if (lblVchType.Text == "Payment")
    //                        trantype = "P";
    //                    else if (lblVchType.Text == "Receipt")
    //                        trantype = "R";
    //                    else if (lblVchType.Text == "Contra")
    //                        trantype = "C";
    //                    else
    //                        trantype = "J";
    //                    //string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
    //                    string voucher_seq = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "VOUCHER_SQN", "VOUCHER_NO='" + btnEdit.CommandArgument + "' and TRANSACTION_TYPE='" + trantype + "' and TRANSACTION_DATE between '" + objGrid.FromDate.ToString("yyyy/MMM/dd") + "' and '" + objGrid.ToDate.ToString("yyyy/MMM/dd") + "'");
    //                    //lblParty.Attributes.Add("onClick", "VoucherModification('" + voucher_seq + "','" + lblVchType.Text + "','" + objGrid.FromDate + "','" + objGrid.ToDate + "','" + LedgerName + "','" + PARTY_NO + "','" + objGrid.FromDate + "','" + objGrid.ToDate + "')");
    //                }
    //            }
    //        }

    //        Label lblopDebit = RptData.Controls[0].FindControl("lblopDebit") as Label;
    //        Label lblopCredit = RptData.Controls[0].FindControl("lblopCredit") as Label;
    //        Label lblclDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblclDebit") as Label;
    //        Label lblClCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblClCredit") as Label;
    //        Label lbltotDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lbltotDebit") as Label;
    //        Label lblTotCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotCredit") as Label;
    //        lblopDebit.Text = dsBalances.Tables[0].Rows[0]["OpDebit"].ToString();
    //        lblopCredit.Text = dsBalances.Tables[0].Rows[0]["opCredit"].ToString();
    //        lblclDebit.Text = dsBalances.Tables[0].Rows[0]["clBalDebit"].ToString();
    //        lblClCredit.Text = dsBalances.Tables[0].Rows[0]["clBalCredit"].ToString();
    //        lbltotDebit.Text = dsBalances.Tables[0].Rows[0]["totDebit"].ToString();
    //        lblTotCredit.Text = dsBalances.Tables[0].Rows[0]["totCredit"].ToString();
    //    }

    //}


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }

    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }

    private void ClearRecord()
    {
        Session["VchDatatable"] = null;
        txtAcc.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAcc.Text = "";
        SetDataColumn();
        Session["VchDatatable"] = null;
        SetFinancialYear();
        txtAcc.Focus();

        rdbWithoutNarration.Checked = true;    
        chkRunningTot.Checked = true;
        rdbLedgerList.SelectedValue = "1";
        lblCurBal.Text = "";
    }

    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;

            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_VCH_NO=" + VchNo.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + VchNo;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
            LedgerName = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE = '" + txtAcc.Text.ToString().Trim().Split('*')[1].ToString() + "'");
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbWithNarration.Checked)
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + 0;
            else
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + 1;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReportAllLedgerReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode = string.Empty;
            string LedgerName = string.Empty;
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbWithNarration.Checked)
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_College_Code=" + Session["colcode"].ToString() + "," + "@P_Partyno=" + 0 + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_NV=" + 0;
            else
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_College_Code=" + Session["colcode"].ToString() + "," + "@P_Partyno=" + 0 + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_NV=" + 1;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

 
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcc.Text == "")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Enter Ledger Name", this);
                txtAcc.Focus();
                return;
            }
           
            if (txtFrmDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
                txtFrmDate.Focus();
                return;
            }
            if (txtUptoDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
                txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
                txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                txtFrmDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
                txtUptoDate.Focus();
                return;
            }

            if (rdbLedgerList.SelectedIndex == 0)
            {
                if (chkAdjustment.Checked)
                {
                    ShowReport("LedgerBook", "AdjustmentLedgerBook.rpt");
                }
                else
                {
                    if (chkRunningTot.Checked)
                        ShowReport("LedgerBook", "LedgerBookWithRunning.rpt");
                    else
                        ShowReport("LedgerBook", "LedgerBook.rpt");
                }
            }
            else
            {
                if (chkAdjustment.Checked)
                {
                    ShowReport("LedgerBook", "AdjustmentLedgerBook.rpt");
                }
                else
                {
                    if (chkRunningTot.Checked)
                        ShowReport("LedgerBook", "LedgerBookDetailedWithRunningTot.rpt");
                    else
                        ShowReport("LedgerBook", "LedgerBookDetailed.rpt");
                }
            }
         

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCondensed_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcc.Text == "")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Enter Ledger Name", this);
                txtAcc.Focus();
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

            if (txtAcc.Text.ToString().Trim() == "")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Enter Ledger Name.", this);
                txtAcc.Focus();
                return;
            }
            ShowConReport("Condensed Ledger Book", "Ledger_Book_con.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.btnCondensed_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        try
        {
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
            ShowConReport("Ledger Book Summary", "LedgerBookSummary.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.btnSummary_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnLedgerList_Click(object sender, EventArgs e)
    {
        try
        {
            TrialBalanceReportController od = new TrialBalanceReportController();
            od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
            od.GenerateLedgerList(Session["comp_code"].ToString());
            GenerateLedgerListFormat();

            ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowList_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ShowConReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string ClMode = string.Empty;
            //ClMode = txtmd.Text.ToString().Trim();
            string LedgerName = string.Empty;

            LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();
            LedgerName = LedgerName.Replace("¯", "");

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + lblCurBal.Text.ToString().Trim() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_LEDGERNAME=" + LedgerName;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.ShowConReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowLedgerListReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (rptFileName.ToString().Trim() == "TrialBalanceReport.rpt")
            {

                TrialBalanceReportController obj = new TrialBalanceReportController();
                DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
                double op = 0;
                double cl = 0;
                double plDr = 0;
                double plCr = 0;

                if (dsPLOC != null)
                {
                    if (dsPLOC.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                        {
                            op = 0;
                        }
                        else
                        {
                            op = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                        }
                        if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                        {
                            cl = 0;
                        }
                        else
                        {
                            cl = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]);
                        }
                    }
                }


                DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                if (dsPLDC != null)
                {
                    if (dsPLDC.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                        {
                            plDr = 0;
                        }
                        else
                        {
                            plDr = Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]);
                        }

                        if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                        {
                            plCr = 0;
                        }
                        else
                        {
                            plCr = Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]);
                        }

                    }
                }


                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                string LedgerName = string.Empty;

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ACCOUNT," + rptFileName;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@PLOpening=" + op.ToString() + "," + "@PLClosing=" + cl.ToString() + "," + "@PLDebit=" + plDr.ToString() + "," + "@PLCredit=" + plCr.ToString();
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            }
            else
            {

                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

                string LedgerName = string.Empty;

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ACCOUNT," + rptFileName;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString();

                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowBalanceSheet(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_CODE_YEAR=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_UpToDate=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnShowList_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
    //        od.GenerateLedgerList(Session["comp_code"].ToString());
    //        GenerateLedgerListFormat();

    //        ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnShowList_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
    protected void GenerateLedgerListFormat()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    TrialBalanceReport oEntity = new TrialBalanceReport();
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {

                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }


                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }



                            }


                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //RECENTLY ADDED===============
    protected void GenerateTrialBalanceFormatNew(string IsBalanceSheet)
    {
        try
        {
            int pName = 0;
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    double TotalDr5 = 0;
                    double TotalCr5 = 0;
                    int LedgerIndex = 0;
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {

                        double TotalDr = 0;
                        double TotalCr = 0;
                        TrialBalanceReport oEntity = new TrialBalanceReport();
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            oEntity.LEDGERINDEX = LedgerIndex;
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);
                            double TotalDr1 = 0;
                            double TotalCr1 = 0;
                            DataSet dsLdg1 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PRNO=" + dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() + " and party_no = 0", string.Empty);
                            if (dsLdg1 != null)
                            {
                                if (dsLdg1.Tables[0].Rows.Count > 0)
                                {

                                    int j = 0;

                                    for (j = 0; j < dsLdg1.Tables[0].Rows.Count; j++)
                                    {
                                        TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                        oEntity1.PartyName = "     " + dsLdg1.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim().Trim();
                                        oEntity1.MGRPNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                        oEntity1.PRNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                        oEntity1.PARTYNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                        oEntity1.OPBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                        oEntity1.CLBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                        oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                        oEntity1.DEBIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                        oEntity1.CREDIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                        oEntity1.FANO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["FA_NO"].ToString().Trim());
                                        oEntity1.LEDGERINDEX = LedgerIndex;
                                        TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                        oTran1.AddTrialBalanceReportFormat(oEntity1);

                                        DataSet dsLdg2 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim() + " and prno=" + dsLdg1.Tables[0].Rows[j]["prno"].ToString().Trim() + " and party_no <> 0", string.Empty);
                                        if (dsLdg2 != null)
                                        {
                                            int k = 0;
                                            for (k = 0; k < dsLdg2.Tables[0].Rows.Count; k++)
                                            {

                                                TrialBalanceReport oEntity2 = new TrialBalanceReport();
                                                oEntity2.PartyName = "          " + dsLdg2.Tables[0].Rows[k]["PARTYNAME"].ToString().Trim().Trim();
                                                oEntity2.MGRPNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim());
                                                oEntity2.PRNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["PRNO"].ToString().Trim());
                                                oEntity2.PARTYNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["PARTY_NO"].ToString().Trim());
                                                oEntity2.OPBALANCE = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["OP_BALANCE"].ToString().Trim());
                                                oEntity2.CLBALANCE = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["CL_BALANCE"].ToString().Trim());
                                                oEntity2.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                                oEntity2.DEBIT = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["DEBIT"].ToString().Trim());
                                                oEntity2.CREDIT = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["CREDIT"].ToString().Trim());
                                                oEntity2.FANO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["FA_NO"].ToString().Trim());
                                                oEntity2.LEDGERINDEX = LedgerIndex;
                                                TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                                oTran2.AddTrialBalanceReportFormat(oEntity2);


                                            }


                                        }




                                    }



                                }


                            }


                        }


                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.GenerateTrialBalanceFormatNew -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //END RECENTLY ADDED======================================


    //SECOND RECENTLY ADDED===========
    protected void GenerateTrialBalanceFormatNew1()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    TrialBalanceReport oEntity = new TrialBalanceReport();
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {

                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }


                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }



                            }


                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    //END SECOND RECENTLY ADDED=======


    //add on date 02/03/2010
    protected void InsertSubParentEntry(Int16 mgrpno, Int16 prono, Int16 partyno, DataSet dsSp, Int16 fano)
    {

        if (dsSp != null)
        {
            if (dsSp.Tables[0].Rows.Count > 0)
            {


                int k = 0;
                TrialBalanceReport oEntity = new TrialBalanceReport();
                //space1 = space1.ToString() + "  ";
                for (k = 0; k < dsSp.Tables[0].Rows.Count; k++)
                {
                    if (mgrpno == Convert.ToInt16(dsSp.Tables[0].Rows[k]["prno"]) && Convert.ToInt16(dsSp.Tables[0].Rows[k]["party_no"]) == 0)
                    {
                        oEntity.PartyName = space1.ToString() + dsSp.Tables[0].Rows[k]["PARTYNAME"].ToString().Trim();
                        oEntity.MGRPNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim());
                        oEntity.PRNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["PRNO"].ToString().Trim());
                        oEntity.PARTYNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["PARTY_NO"].ToString().Trim());
                        oEntity.OPBALANCE = Convert.ToDouble(dsSp.Tables[0].Rows[k]["OP_BALANCE"].ToString().Trim());
                        oEntity.CLBALANCE = Convert.ToDouble(dsSp.Tables[0].Rows[k]["CL_BALANCE"].ToString().Trim());
                        oEntity.DEBIT = Convert.ToDouble(dsSp.Tables[0].Rows[k]["DEBIT"].ToString().Trim());
                        oEntity.CREDIT = Convert.ToDouble(dsSp.Tables[0].Rows[k]["CREDIT"].ToString().Trim());
                        oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                        oEntity.FANO = fano;// Convert.ToInt16(dsSp.Tables[0].Rows[k]["FA_NO"].ToString().Trim());
                        TrialBalanceReportController oTran = new TrialBalanceReportController();
                        oTran.AddTrialBalanceReportFormat(oEntity);

                        DataSet dsC = GetChildRecord(Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim()));
                        if (dsC != null)
                        {
                            if (dsC.Tables[0].Rows.Count > 0)
                            {
                                int x = 0;

                                TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                space2 = space2.ToString() + "  ";
                                for (x = 0; x < dsC.Tables[0].Rows.Count; x++)
                                {


                                    oEntity1.PartyName = space2.ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                    oEntity1.MGRPNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["MGRP_NO"].ToString().Trim());
                                    oEntity1.PRNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PRNO"].ToString().Trim());
                                    oEntity1.PARTYNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PARTY_NO"].ToString().Trim());
                                    oEntity1.OPBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["OP_BALANCE"].ToString().Trim());
                                    oEntity1.CLBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["CL_BALANCE"].ToString().Trim());
                                    oEntity1.DEBIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["DEBIT"].ToString().Trim());
                                    oEntity1.CREDIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["CREDIT"].ToString().Trim());
                                    oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                    oEntity1.FANO = fano;// Convert.ToInt16(dsC.Tables[0].Rows[x]["FA_NO"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity1);


                                }

                                space2 = "          ".ToString();

                                //    space2 = space2.ToString() + "  ";

                            }

                        }
                        //InsertSubParentEntry(Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsSp.Tables[0].Rows[k]["prno"].ToString().Trim()), Convert.ToInt16(dsSp.Tables[0].Rows[k]["party_no"].ToString().Trim()), dsSp, fano);

                    }


                }
                //space1 = space1.ToString() + "  ";
                space1 = "      ".ToString();
                // space1 = "  ".ToString();
            }
        }

    }
    protected DataSet GetChildRecord(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected DataSet GetParentChildRecord(Int16 mgrp, Int16 prno)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + mgrp.ToString().Trim() + " and  prno=" + prno.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected void ArrangeFormat(DataSet dsLdg1, DataSet dsLdg)
    {
        try
        {


            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    ArrayList t = new ArrayList();
                    int i = 0;
                    DataView dv = dsLdg.Tables[0].DefaultView;
                    dt1 = dv.ToTable();
                    for (i = 0; i < dt1.Rows.Count; i++)
                    {
                        TrialBalanceReport oEntity = new TrialBalanceReport();


                        int j = 0;
                        if (t.Contains(dt1.Rows[i]["MGRP_NO"].ToString().Trim()) == false)
                        {
                            for (j = 0; j < dsLdg1.Tables[0].Rows.Count; j++)
                            {

                                if (dt1.Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                {
                                    oEntity.PartyName = dsLdg1.Tables[0].Rows[j]["PARTYNAME"].ToString();
                                    //oEntity.PartyName = space1.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["fano"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);
                                }
                            }
                        }
                        t.Add(dt1.Rows[i]["MGRP_NO"].ToString().Trim());


                    }

                }


            }

        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.ArrangeFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void GenerateTrialBalanceFormatNew2()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PARTYNAME <> ''", string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    TrialBalanceReport oEntity = new TrialBalanceReport();
                    int i = 0;

                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            space1 = space1.ToString() + "  ";
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {
                                    //oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.PartyName = space1.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);
                                    space1 = space1.ToString() + "  ";
                                    InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));
                                    DataSet dsC = GetChildRecord(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()));
                                    if (dsC != null)
                                    {
                                        if (dsC.Tables[0].Rows.Count > 0)
                                        {
                                            int x = 0;
                                            TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                            space2 = space2.ToString() + "  ";
                                            for (x = 0; x < dsC.Tables[0].Rows.Count; x++)
                                            {

                                                // oEntity1.PartyName = "          ".ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                                oEntity1.PartyName = space2.ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                                oEntity1.MGRPNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["MGRP_NO"].ToString().Trim());
                                                oEntity1.PRNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PRNO"].ToString().Trim());
                                                oEntity1.PARTYNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PARTY_NO"].ToString().Trim());
                                                oEntity1.OPBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["OP_BALANCE"].ToString().Trim());
                                                oEntity1.CLBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["CL_BALANCE"].ToString().Trim());
                                                oEntity1.DEBIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["DEBIT"].ToString().Trim());
                                                oEntity1.CREDIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["CREDIT"].ToString().Trim());
                                                oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                                oEntity1.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                                TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                                oTran2.AddTrialBalanceReportFormat(oEntity1);


                                            }
                                            space2 = "          ".ToString();

                                        }

                                    }

                                }
                                else
                                {
                                    if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                    {
                                        TrialBalanceReport oEntity3 = new TrialBalanceReport();
                                        oEntity3.PartyName = space2.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                        //oEntity3.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                        oEntity3.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                        oEntity3.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                        oEntity3.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                        oEntity3.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                        oEntity3.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                        oEntity3.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                        oEntity3.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                        oEntity3.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                        oEntity3.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                        TrialBalanceReportController oTran3 = new TrialBalanceReportController();
                                        oTran3.AddTrialBalanceReportFormat(oEntity3);

                                    }

                                }


                            }
                            space1 = "     ".ToString();

                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    // end of 02/03/2010
    protected void GenerateTrialBalanceFormat(string IsBalanceSheet)
    {
        try
        {
            int pName = 0;
            TrialBalanceReport oEntity = new TrialBalanceReport();
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    double TotalDr5 = 0;
                    double TotalCr5 = 0;
                    int LedgerIndex = 0;
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {

                        double TotalDr = 0;
                        double TotalCr = 0;


                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CAPITAL ACCOUNT")
                            {
                                LedgerIndex = 1;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "FIXED ASSETS")
                            {
                                LedgerIndex = 2;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "LOANS (LIABILITY)")
                            {
                                LedgerIndex = 3;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "INVESTMENTS")
                            {
                                LedgerIndex = 4;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CURRENT LIABLITIES")
                            {
                                LedgerIndex = 5;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CURRENT ASSETS")
                            {
                                LedgerIndex = 6;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "DIRECT EXPENSES")
                            {
                                LedgerIndex = 7;
                            }




                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            oEntity.LEDGERINDEX = LedgerIndex;
                            double tot = oEntity.CREDIT - oEntity.DEBIT;
                            if (tot > 0)
                            {
                                oEntity.CREDIT = tot;
                                oEntity.DEBIT = 0;
                            }
                            else
                            {
                                oEntity.CREDIT = 0;
                                oEntity.DEBIT = Math.Abs(tot);

                            }

                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            double TotalDr1 = 0;
                            double TotalCr1 = 0;





                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++) //2 for loop
                            {


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    if (pName != 0)
                                    {
                                        TrialBalanceReportController oUpd5 = new TrialBalanceReportController();
                                        TrialBalanceReport oEnUpd5 = new TrialBalanceReport();
                                        oEnUpd5.MGRPNO = pName;
                                        double tot11 = TotalCr5 - TotalDr5;
                                        if (tot11 > 0)
                                        {
                                            oEnUpd5.CREDIT = tot11;
                                            oEnUpd5.DEBIT = 0.00;
                                        }
                                        else
                                        {
                                            oEnUpd5.DEBIT = Math.Abs(tot11);
                                            oEnUpd5.CREDIT = 0.00;
                                        }



                                        oUpd5.UpdateTrialBalanceAmount(oEnUpd5);

                                        TotalDr5 = 0;
                                        TotalCr5 = 0;
                                    }



                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    pName = oEntity.MGRPNO;
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    oEntity.LEDGERINDEX = LedgerIndex;
                                    double tot1 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot1 > 0)
                                    {
                                        oEntity.CREDIT = tot1;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot1);

                                    }

                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                            oEntity.LEDGERINDEX = LedgerIndex;
                                            double tot2 = oEntity.CREDIT - oEntity.DEBIT;
                                            if (tot2 > 0)
                                            {
                                                oEntity.CREDIT = tot2;
                                                oEntity.DEBIT = 0;
                                            }
                                            else
                                            {
                                                oEntity.CREDIT = 0;
                                                oEntity.DEBIT = Math.Abs(tot2);

                                            }


                                            TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                            TotalCr1 = TotalCr1 + oEntity.CREDIT;


                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    double tot3 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot3 > 0)
                                    {
                                        oEntity.CREDIT = tot3;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot3);

                                    }

                                    TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                    TotalCr1 = TotalCr1 + oEntity.CREDIT;

                                    TotalDr5 = TotalDr5 + oEntity.DEBIT;
                                    TotalCr5 = TotalCr5 + oEntity.CREDIT;                                                //TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    //oTran2.AddTrialBalanceReportFormat(oEntity);
                                }


                            }// End of 2 for loop
                            TrialBalanceReportController oUpd = new TrialBalanceReportController();
                            TrialBalanceReport oEnUpd = new TrialBalanceReport();
                            oEnUpd.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]);

                            double tot12 = TotalCr1 - TotalDr1;
                            if (tot12 > 0)
                            {
                                oEnUpd.CREDIT = tot12;
                                oEnUpd.DEBIT = 0.00;
                            }
                            else
                            {
                                oEnUpd.DEBIT = Math.Abs(tot12);
                                oEnUpd.CREDIT = 0.00;
                            }


                            oUpd.UpdateTrialBalanceAmount(oEnUpd);

                            if (pName != 0)
                            {
                                TrialBalanceReportController oUpd7 = new TrialBalanceReportController();
                                TrialBalanceReport oEnUpd7 = new TrialBalanceReport();
                                oEnUpd7.MGRPNO = pName;
                                double tot13 = TotalCr5 - TotalDr5;
                                if (tot13 > 0)
                                {
                                    oEnUpd7.CREDIT = tot13;
                                    oEnUpd7.DEBIT = 0.00;
                                }
                                else
                                {
                                    oEnUpd7.DEBIT = Math.Abs(tot13);
                                    oEnUpd7.CREDIT = 0.00;
                                }


                                oUpd7.UpdateTrialBalanceAmount(oEnUpd7);

                                //TotalDr5 = 0;
                                //TotalCr5 = 0;
                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            double TotalDr2 = 0;
                            double TotalCr2 = 0;
                            double TotalDr6 = 0;
                            double TotalCr6 = 0;

                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++) // for loop 3
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {

                                    oEntity.PartyName = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    pName = oEntity.MGRPNO;
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;//Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    double tot4 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot4 > 0)
                                    {
                                        oEntity.CREDIT = tot4;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot4);

                                    }
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    oEntity.LEDGERINDEX = LedgerIndex;
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {

                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    double tot5 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot5 > 0)
                                    {
                                        oEntity.CREDIT = tot5;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot5);

                                    }
                                    TotalDr2 = TotalDr2 + oEntity.DEBIT;
                                    TotalCr2 = TotalCr2 + oEntity.CREDIT;
                                    TotalDr6 = TotalDr6 + oEntity.DEBIT;
                                    TotalCr6 = TotalCr6 + oEntity.CREDIT;


                                }



                            }// end of for loop 3


                        }




                    }


                }



                oEntity.PartyName = "Diff. in Opening Balances".ToString().Trim().Trim();
                oEntity.MGRPNO = 0;
                oEntity.PRNO = 0;
                oEntity.PARTYNO = 0;
                oEntity.OPBALANCE = 0;
                oEntity.CLBALANCE = 0;
                double TotalOpeningBalance = 0;
                PartyController op = new PartyController();
                DataSet dsOp = op.GetTotalOpeningBalances(Session["comp_code"].ToString());
                if (dsOp != null)
                {
                    if (dsOp.Tables[0].Rows.Count > 0)
                    {

                        TotalOpeningBalance = Convert.ToDouble(dsOp.Tables[0].Rows[0]["CREDIT"]) - Convert.ToDouble(dsOp.Tables[0].Rows[0]["DEBIT"]);

                    }

                }
                if (TotalOpeningBalance < 0)
                {
                    oEntity.DEBIT = Math.Abs(TotalOpeningBalance);

                }
                else
                {
                    oEntity.CREDIT = Math.Abs(TotalOpeningBalance);

                }
                oEntity.ISPARTY = 1;
                oEntity.FANO = 0;
                oEntity.LEDGERINDEX = 10;
                TrialBalanceReportController oTran4 = new TrialBalanceReportController();
                oTran4.AddTrialBalanceReportFormat(oEntity);

                //For Income-Expenses Transaction Amount=========================
                if (IsBalanceSheet == "Y")
                {

                    oEntity.PartyName = "Income Expenses A/c".ToString();
                    oEntity.MGRPNO = 0;
                    oEntity.PRNO = 0;
                    oEntity.PARTYNO = 0;
                    oEntity.OPBALANCE = 0;
                    oEntity.CLBALANCE = 0;
                    double TotalDrExpensesAmount = 0;
                    double TotalCrExpensesAmount = 0;
                    double NetExpensAmount = 0;
                    DataSet dsOp1 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "Debit", "Credit", "IS_PARTY=0 AND FA_NO=4", "FA_NO");
                    if (dsOp1 != null)
                    {
                        if (dsOp1.Tables[0].Rows.Count > 0)
                        {
                            int K = 0;
                            for (K = 0; K < dsOp1.Tables[0].Rows.Count; K++)
                            {
                                TotalDrExpensesAmount = TotalDrExpensesAmount + Convert.ToDouble(dsOp1.Tables[0].Rows[K]["Debit"].ToString().Trim());
                                TotalCrExpensesAmount = TotalCrExpensesAmount + Convert.ToDouble(dsOp1.Tables[0].Rows[K]["Credit"].ToString().Trim());

                            }

                            NetExpensAmount = Convert.ToDouble(TotalCrExpensesAmount) - Convert.ToDouble(TotalDrExpensesAmount);

                        }

                    }

                    double TotalDrIncomeAmount = 0;
                    double TotalCrIncomeAmount = 0;
                    double NetIncomeAmount = 0;
                    DataSet dsOp2 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "Debit", "Credit", "IS_PARTY=0 AND FA_NO=3", "FA_NO");
                    if (dsOp2 != null)
                    {
                        if (dsOp2.Tables[0].Rows.Count > 0)
                        {
                            int l = 0;
                            for (l = 0; l < dsOp2.Tables[0].Rows.Count; l++)
                            {
                                TotalDrIncomeAmount = TotalDrIncomeAmount + Convert.ToDouble(dsOp2.Tables[0].Rows[l]["Debit"].ToString().Trim());
                                TotalCrIncomeAmount = TotalCrIncomeAmount + Convert.ToDouble(dsOp2.Tables[0].Rows[l]["Credit"].ToString().Trim());

                            }

                            NetIncomeAmount = Convert.ToDouble(TotalCrIncomeAmount) - Convert.ToDouble(TotalDrIncomeAmount);

                        }

                    }

                    double NetDeficitAmount = NetIncomeAmount - NetExpensAmount;






                    if (NetDeficitAmount < 0)
                    {
                        oEntity.DEBIT = Math.Abs(NetDeficitAmount);

                    }
                    else
                    {
                        oEntity.CREDIT = Math.Abs(NetDeficitAmount);

                    }



                    oEntity.ISPARTY = 1;
                    oEntity.FANO = 0;
                    oEntity.LEDGERINDEX = 11;
                    TrialBalanceReportController oTran5 = new TrialBalanceReportController();
                    oTran5.AddTrialBalanceReportFormat(oEntity);
                }
                //===============================================================

                TrialBalanceReportController oDelete = new TrialBalanceReportController();
                oDelete.DeleteTrialBalanceAmount();

            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.GenerateTrialBalanceFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //protected void btnShowTrialBalance_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (txtFrmDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            txtFrmDate.Focus();
    //            return;
    //        }
    //        if (txtUptoDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            txtFrmDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //        od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //        //GenerateTrialBalanceFormat("N");
    //        GenerateTrialBalanceFormatNew2();
    //        od.OrderTrialBalanceReport();
    //        UpdateTotalForLedgerNew();
    //        UpdateTotalForMainLedger();
    //        od.DeleteTrialBalanceZEROAmount();
    //        //DataSet dsLdg = od.GetDistinctMGRPNO();
    //        //DataSet dst = od.ArrangeTrialBalanceReport();
    //        //ArrangeFormat(dst, dsLdg);


    //        ShowLedgerListReport("TrialBalance", "TrialBalanceReport.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnShowTrialBalance_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }




    //}
    protected void UpdateTotalForLedger()
    {
        try
        {
            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    double Cr1 = 0;
                    double Dr1 = 0;
                    double Cr2 = 0;
                    double Dr2 = 0;
                    double Op1 = 0;
                    double Cl1 = 0;
                    double Op2 = 0;
                    double Cl2 = 0;
                    int mgrpno1 = 0;
                    int mgrpno2 = 0;
                    int pno1 = 0;
                    int pno2 = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {

                            if (Math.Abs(Cr1) > 0 || Math.Abs(Dr1) > 0 || Math.Abs(Op1) > 0 || Math.Abs(Cl1) > 0)
                            {
                                //update total
                                TrialBalanceReportController tbc = new TrialBalanceReportController();
                                tbc.UpdateAllTrialBalanceAmount(mgrpno1, Cr1, Dr1, Op1, Cl1);

                                Cr1 = 0;
                                Dr1 = 0;
                                Op1 = 0;
                                Cl1 = 0;

                            }



                            mgrpno1 = Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]);


                        }
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == mgrpno1 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            if (Math.Abs(Cr2) > 0 || Math.Abs(Dr2) > 0 || Math.Abs(Op2) > 0 || Math.Abs(Cl2) > 0)
                            {
                                //update total
                                TrialBalanceReportController tbc1 = new TrialBalanceReportController();
                                tbc1.UpdateAllTrialBalanceAmount(mgrpno2, Cr2, Dr2, Op2, Cl2);

                                Cr1 = Cr1 + Cr2;
                                Dr1 = Dr1 + Dr2;
                                Op1 = Op1 + Op2;
                                Cl1 = Cl1 + Cl2;
                                Cr2 = 0;
                                Dr2 = 0;
                                Op2 = 0;
                                Cl2 = 0;

                            }

                            mgrpno2 = Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]);
                            pno2 = Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]);


                        }
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]) == mgrpno2 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == pno2 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) != 0)
                        {
                            Cr2 = Cr2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["CREDIT"]);
                            Dr2 = Dr2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["DEBIT"]);
                            Op2 = Op2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["OP_BALANCE"]);
                            Cl2 = Cl2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["CL_BALANCE"]);



                        }





                    }








                }



            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.UpdateTotalForLedger -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void UpdateTotalForLedgerNew()
    {
        try
        {

            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {

                    int i = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim()) == 0)
                        {
                            //call 

                            TrialBalanceReportController tbrc = new TrialBalanceReportController();
                            DataSet dsres = tbrc.GetLedgerTotal(Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()));
                            if (dsres != null)
                            {
                                int CID = 0;
                                if (dsres.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]) > 0)
                                    {

                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[0]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[0]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[0]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[0]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }
                                    else
                                    {
                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[1]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[1]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[1]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[1]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }


                                }


                            }



                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.UpdateTotalForLedgerNew -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void UpdateTotalForMainLedger()
    {
        try
        {

            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {

                    int i = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()) == 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim()) == 0)
                        {
                            //call 

                            TrialBalanceReportController tbrc = new TrialBalanceReportController();
                            DataSet dsres = tbrc.GetMainLedgerTotal(Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()));
                            if (dsres != null)
                            {
                                int CID = 0;
                                if (dsres.Tables[0].Rows.Count > 1)
                                {
                                    if (Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]) > 0)
                                    {


                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[1]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[1]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[1]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[1]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }
                                    else
                                    {
                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[0]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[0]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[0]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[0]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);

                                    }


                                }


                            }



                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.UpdateTotalForMainLedger -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //protected void btnShowBalanceSheet_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //       // btnShowTrialBalance_Click(sender, e);


    //        if (txtFrmDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            txtFrmDate.Focus();
    //            return;
    //        }
    //        if (txtUptoDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            txtFrmDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //        od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //        //GenerateTrialBalanceFormat("N");
    //        GenerateTrialBalanceFormatNew2();
    //        od.OrderTrialBalanceReport();
    //        UpdateTotalForLedgerNew();
    //        UpdateTotalForMainLedger();
    //        od.DeleteTrialBalanceZEROAmount();

    //        Response.Redirect("BalanceSheetConfiguration.aspx?obj=" + txtFrmDate.Text.ToString().Trim() + ","+ txtUptoDate.Text.ToString().Trim() + "");

    //        //TrialBalanceReportController od = new TrialBalanceReportController();
    //        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //        //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //        //GenerateTrialBalanceFormat("Y");
    //        //od.OrderTrialBalanceReport();
    //        //ShowBalanceSheet("BalanceSheet", "BalanceSheet1.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }

    //}
    //protected void btnPL_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    if (txtFrmDate.Text.ToString().Trim() == "")
    //    //    {
    //    //        objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //    //        txtFrmDate.Focus();
    //    //        return;
    //    //    }
    //    //    if (txtUptoDate.Text.ToString().Trim() == "")
    //    //    {
    //    //        objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //    //        txtUptoDate.Focus();
    //    //        return;
    //    //    }


    //    //    if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //    //    {
    //    //        objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //    //        txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //    //        txtUptoDate.Focus();
    //    //        return;
    //    //    }

    //    //    if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //    //    {
    //    //        objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //    //        txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //    //        txtFrmDate.Focus();
    //    //        return;
    //    //    }

    //    //    if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //    //    {
    //    //        objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //    //        txtUptoDate.Focus();
    //    //        return;
    //    //    }
    //    //    TrialBalanceReportController od = new TrialBalanceReportController();
    //    //    od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //    //    od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //    //    GenerateTrialBalanceFormat("Y");
    //    //    od.OrderTrialBalanceReport();
    //    //    ShowBalanceSheet("ProfitLoss", "IncomeExpensesMainReport.rpt");
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objUCommon.ShowError(Page, "LedgerReport.btnPL_Click -> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objUCommon.ShowError(Page, "Server UnAvailable");

    //    //}


    //    try
    //    {

    //        // btnShowTrialBalance_Click(sender, e);


    //        if (txtFrmDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            txtFrmDate.Focus();
    //            return;
    //        }
    //        if (txtUptoDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            txtFrmDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //        od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //        //GenerateTrialBalanceFormat("N");
    //        GenerateTrialBalanceFormatNew2();
    //        od.OrderTrialBalanceReport();
    //        UpdateTotalForLedgerNew();
    //        UpdateTotalForMainLedger();
    //        od.DeleteTrialBalanceZEROAmount();

    //        Response.Redirect("IncomeExpenditureConfiguration.aspx?obj=" + txtFrmDate.Text.ToString().Trim() + "," + txtUptoDate.Text.ToString().Trim() + "");

    //        //TrialBalanceReportController od = new TrialBalanceReportController();
    //        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
    //        //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
    //        //GenerateTrialBalanceFormat("Y");
    //        //od.OrderTrialBalanceReport();
    //        //ShowBalanceSheet("BalanceSheet", "BalanceSheet1.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnPL_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }


    //}
    //protected void btnRP_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (txtAcc.Text.ToString().Trim() == "")
    //        //{
    //        //    objCommon.DisplayMessage("Enter Ledger Name.", this);
    //        //    txtAcc.Focus();
    //        //    return;

    //        //}
    //        //if (RptData.Items.Count == 0)
    //        //{
    //        //    objCommon.DisplayMessage("Record Not Available.", this);
    //        //    txtAcc.Focus();
    //        //    return;

    //        //}
    //        DeleteReceiptPaymentFormat();
    //        GenerateReceiptPaymentFormat();
    //        ShowReportReceiptPayment("ReceiptPayment", "ReceiptPayment.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnRP_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
    private void ShowReportReceiptPayment(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            //ClMode = txtmd.Text.ToString().Trim();
            string LedgerName = string.Empty;

            // LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReportReceiptPayment -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void DeleteReceiptPaymentFormat()
    {

        AccountTransactionController objtrn1 = new AccountTransactionController();
        objtrn1.DeleteReceiptPaymentData(Session["comp_code"].ToString().Trim());

    }


    public void GenerateReceiptPaymentFormat()
    {
        DataSet dso = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "*", "", "payment_type_no in ('1','2') ", "party_no");
        if (dso.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            int id = 1;
            double DrOpBalance = 0;
            double CrOpBalance = 0;
            for (i = 0; i < dso.Tables[0].Rows.Count; i++)
            {
                if (dso.Tables[0].Rows[i]["STATUS"].ToString().Trim() == "D")
                {
                    DrOpBalance = DrOpBalance + Convert.ToDouble(dso.Tables[0].Rows[i]["OPBALANCE"]);

                }
                else
                {
                    CrOpBalance = CrOpBalance + Convert.ToDouble(dso.Tables[0].Rows[i]["OPBALANCE"]);

                }

            }

            //insert for ope5ning balances
            if (DrOpBalance > 0)
            {
                InsertReceiptPayment(id, "R", DrOpBalance, "OPENING BALANCE");
            }
            if (CrOpBalance > 0)
            {
                if (DrOpBalance > 0)
                {
                    id = id + 1;
                }
                else
                {
                    id = 1;
                }
                InsertReceiptPayment(id, "P", CrOpBalance, "OPENING BALANCE");
            }
            //End of insert for opening balances

            int j = 0;
            for (j = 0; j < dso.Tables[0].Rows.Count; j++)
            {

                //call proccedure
                AccountTransactionController objtrn = new AccountTransactionController();
                DataSet dsResult = objtrn.GetReceiptPaymentResult(Session["comp_code"].ToString().Trim(), dso.Tables[0].Rows[j]["PARTY_NAME"].ToString().Trim(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    int k = 0;
                    for (k = 0; k < dsResult.Tables[0].Rows.Count; k++)
                    {
                        if (DrOpBalance > 0)
                        {
                            id = id + 1;

                        }
                        else if (CrOpBalance > 0)
                        {
                            id = id + 1;

                        }

                        if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Contra")
                        {
                            if (Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]) > 0)
                            {
                                InsertReceiptPayment(id, "R", Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]), Convert.ToString(dso.Tables[0].Rows[j]["Party_Name"]).Trim());


                            }
                            if (Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]) > 0)
                            {
                                InsertReceiptPayment(id, "P", Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]), Convert.ToString(dso.Tables[0].Rows[j]["Party_Name"]).Trim());


                            }


                        }
                        else if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Reciept")
                        {
                            InsertReceiptPayment(id, "R", Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]), Convert.ToString(dsResult.Tables[0].Rows[k]["LEDGER"]).Trim());
                        }
                        else if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Payment")
                        {
                            InsertReceiptPayment(id, "P", Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]), Convert.ToString(dsResult.Tables[0].Rows[k]["LEDGER"]).Trim());
                        }




                    }


                }


            }





        }


    }


    public void InsertReceiptPayment(int index, string RPFlag, double Amount, string Ledger)
    {
        AccountTransactionController objAdd = new AccountTransactionController();
        objAdd.AddReceiptPaymentFormat(index, Ledger, Amount, RPFlag);

    }
    //protected void btndb_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (txtFrmDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            txtFrmDate.Focus();
    //            return;
    //        }
    //        if (txtUptoDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            txtFrmDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteDayBookRecord();
    //        GetDayBookReportFormat();
    //        OrderDaybook();
    //        ShowDayBook("DayBook", "DayBook.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "LedgerReport.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
    private void ShowDayBook(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (txtFrmDate.Text.ToString().Trim() == txtUptoDate.Text.ToString().Trim())
            {
                url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();
            }
            else
            {
                url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();

            }


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void OrderDaybook()
    {
        TrialBalanceReportController ot = new TrialBalanceReportController();
        ot.OrderDaybookReportFormat();

    }

    //Check for the Dates
    private void Checkdates()
    {
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
    }
    //public void GetDayBookReportFormat()
    //{
    //    int id = 0;
    //    int i = 0;
    //    TrialBalanceReportController od1 = new TrialBalanceReportController();
    //    DataSet ds = od1.GetDistinctVoucherNo(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                TrialBalanceReportController od2 = new TrialBalanceReportController();
    //                DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(),Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]));
    //                if (ds1.Tables[0].Rows.Count > 0)
    //                { 
    //                    //int k=0;
    //                    //for(k=0;k< ds1.Tables[0].Rows.Count;k++)
    //                    //{
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "J")
    //                        {
    //                           id= AddJournalVoucherEntry(ds1.Tables[0],id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "P")
    //                        {
    //                            id = AddPaymentVoucherEntry(ds1.Tables[0], id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "C")
    //                        {
    //                            id = AddContraVoucherEntry(ds1.Tables[0], id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "R")
    //                        {
    //                            id = AddReceiptVoucherEntry(ds1.Tables[0], id);

    //                        }

    //                   // }



    //                }

    //            }


    //        }

    //    }


    //}
    //    private int AddPaymentVoucherEntry(DataTable dtp,int id) //function for Payment
    //{
    //    int l = 0;
    //    for (l = 0; l < dtp.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
    //        {
    //           AccountTransactionController od7 = new AccountTransactionController();
    //           od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[l]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1);
    //           id = id + 1;        
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtp.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "", dtp.Rows[m]["str_voucher_no"].ToString().Trim(),0 , Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtp.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }



    //    return id;

    //}// End Of function for Payment
    //    private int AddContraVoucherEntry(DataTable dtp, int id) //function for Contra
    //{
    //    int l = 0;
    //    for (l = 0; l < dtp.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
    //        {
    //            AccountTransactionController od7 = new AccountTransactionController();
    //            od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Contra", dtp.Rows[l]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1);
    //            id = id + 1;
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtp.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "", dtp.Rows[m]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtp.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }





    //    return id;

    //}// End Of function for contra
    //    private int AddReceiptVoucherEntry(DataTable dtr, int id) //function for Receipt
    //{
    //    int l = 0;
    //    for (l = 0; l < dtr.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtr.Rows[l]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od7 = new AccountTransactionController();
    //            od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[l]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[l]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtr.Rows[l]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtr.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtr.Rows[m]["Tran"]).Trim() == "Dr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "", dtr.Rows[m]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtr.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtr.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() + " DT. " +Convert.ToDateTime(dtr.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "", dtr.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "", dtr.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }




    //    return id;

    //}// End Of function for Receipt
    //private int AddJournalVoucherEntry(DataTable dtj, int id)//function for JV
    //{

    //    int u = 0;
    //    double Amt = 0;
    //    string vtype = string.Empty;
    //    string vno = string.Empty;
    //    string name = string.Empty;
    //    string TrnDate = string.Empty;
    //    string trntype = string.Empty;
    //    int ind = 0;
    //    int bold = 0;

    //    DataTable dtTemp;
    //    for (u = 0; u < dtj.Rows.Count; u++)
    //    {
    //        if (Convert.ToDouble(dtj.Rows[u]["Amount"]) > Amt)
    //        {
    //            Amt = Convert.ToDouble(dtj.Rows[u]["Amount"]);
    //            vtype = "Journal";
    //            vno = Convert.ToString(dtj.Rows[u]["str_voucher_no"]).Trim();
    //            name = Convert.ToString(dtj.Rows[u]["party_name"]).Trim();
    //            trntype = Convert.ToString(dtj.Rows[u]["tran"]).Trim();
    //            TrnDate = Convert.ToString(Convert.ToDateTime(dtj.Rows[u]["transaction_date"]).ToString("dd/MMM/yyyy"));
    //            bold = 1;
    //            ind = u;

    //        }


    //    }


    //    AccountTransactionController od4 = new AccountTransactionController();
    //    if (trntype == "Dr")
    //    {
    //        od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, Amt, 0, 1);
    //        id = id + 1;
    //    }
    //    else
    //    {
    //        od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, 0, Amt, 1);
    //        id = id + 1;
    //    }
    //    dtTemp = dtj;
    //    dtj.Rows[ind].Delete();
    //    dtj.AcceptChanges();

    //    int y = 0;
    //    for (y = 0; y < dtj.Rows.Count; y++)
    //    {
    //        AccountTransactionController od5 = new AccountTransactionController();

    //        if (Convert.ToString(dtj.Rows[y]["tran"]).Trim() == "Dr")
    //        {
    //            od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "", dtj.Rows[y]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 0, 1);
    //            id = id + 1;
    //        }
    //        else
    //        {
    //            od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "", dtj.Rows[y]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;

    //        }
    //    }


    //    int o = 0;
    //    for (o = 0; o < dtTemp.Rows.Count; o++)
    //    {
    //        if (Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od10 = new AccountTransactionController();
    //            od10.AddDayBookReportFormat(id, Convert.ToDateTime(dtTemp.Rows[o]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim(), "", dtTemp.Rows[o]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            id = id + 1;
    //        }

    //    }

    //    return id;



    //}// End Of function for JV
    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            //btnGo_Click(sender, e);
        }
        txtAcc.Focus();

    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            //btnGo_Click(sender, e);
        }
        txtUptoDate.Focus();
        txtUptoDate.Text = Convert.ToDateTime(txtFrmDate.Text).AddMonths(1).ToString();
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

    protected void btnAllLedgerRpt_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtFrmDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
                txtFrmDate.Focus();
                return;
            }
            if (txtUptoDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
                txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
                txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                txtFrmDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
                txtUptoDate.Focus();
                return;
            }

            //if (txtAcc.Text.ToString().Trim() == "")
            //{
            //    objCommon.DisplayMessage("Enter Ledger Name.", this);
            //    txtAcc.Focus();
            //    return;
            //}

            ShowReportAllLedgerReport("LedgerBook", "ALLLedgerBook.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
           
            double debittotal = 0;
            double credittotal = 0;

            double debitClosing = 0;
            double creditClosing = 0;

            Checkdates();

            double debitOpening = 0;
            double creditOpening = 0;

            TrialBalanceReportController objTrialBalance = new TrialBalanceReportController();
            string PartyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME='" + txtAcc.Text.Split('*')[0] + "'");
            DataSet dsLedgerData = null;
            DataSet dsAllLedgerData = null;
            if (txtAcc.Text != "")
            {
                {
                    dsLedgerData = objTrialBalance.GetLedgerData(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), PartyNo, Session["colcode"].ToString());
                }
                DataTable dtLedgerData = dsLedgerData.Tables[0];
                DataTable dtLedger = new DataTable();

                for (int i = 0; i < dtLedgerData.Rows.Count; i++)
                {
                    if (!dtLedger.Columns.Contains("TRANSACTION_DATE"))
                        dtLedger.Columns.Add("TRANSACTION_DATE");

                    if (!dtLedger.Columns.Contains("PARTY_NAME"))
                        dtLedger.Columns.Add("PARTY_NAME");

                    if (!dtLedger.Columns.Contains("Mode"))
                        dtLedger.Columns.Add("Mode");

                    if (!dtLedger.Columns.Contains("Vch_Type"))
                        dtLedger.Columns.Add("Vch_Type");

                    if (!dtLedger.Columns.Contains("VOUCHER_NO"))
                        dtLedger.Columns.Add("VOUCHER_NO");

                    if (!dtLedger.Columns.Contains("CREDIT"))
                        dtLedger.Columns.Add("CREDIT");

                    if (!dtLedger.Columns.Contains("DEBIT"))
                        dtLedger.Columns.Add("DEBIT");

                    if (!dtLedger.Columns.Contains("PARTICULARS"))
                        dtLedger.Columns.Add("PARTICULARS");

                    //if (!dtLedger.Columns.Contains("curbal"))
                    //    dtLedger.Columns.Add("curbal");

                    if (i == 0)
                    {

                        DataView dvData = new DataView(dsLedgerData.Tables[0]);
                        dvData.RowFilter = "party_name='OPENING BALANCE'";
                        // + dtLedger.Rows[i]["party_name"].ToString() + " and OPBALANCE='" + dtLedger.Rows[i]["OPBALANCE"].ToString() + "'"
                        DataTable drVoucherData = dvData.ToTable();

                        DataRow drOpeningRows = dtLedger.NewRow();
                        drOpeningRows["TRANSACTION_DATE"] = Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy");

                        DataSet drt = GetOpeningLedgerData(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), PartyNo, Session["comp_code"].ToString());
                        if (drt.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(drt.Tables[0].Rows[0]["closing"]) < 0)
                            {
                                drOpeningRows["CREDIT"] = "";
                                drOpeningRows["DEBIT"] = dtLedgerData.Rows[i]["OPBALANCE"];

                            }
                            else
                            {
                                drOpeningRows["CREDIT"] = dtLedgerData.Rows[i]["OPBALANCE"];
                                drOpeningRows["DEBIT"] = "";

                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(drVoucherData.Rows[i]["OPBALANCE"]) > 0)
                            {
                                drOpeningRows["CREDIT"] = dtLedgerData.Rows[i]["OPBALANCE"];
                                drOpeningRows["DEBIT"] = "";
                            }
                            else
                            {
                                drOpeningRows["CREDIT"] = "";
                                drOpeningRows["DEBIT"] = dtLedgerData.Rows[i]["OPBALANCE"];
                            }
                        }

                        drOpeningRows["PARTY_NAME"] = "Opening Balance";
                        drOpeningRows["Mode"] = "";
                        drOpeningRows["Vch_Type"] = "";
                        drOpeningRows["VOUCHER_NO"] = "";
                        drOpeningRows["PARTICULARS"] = "";
                        // drOpeningRows["curbal"] = "";
                        dtLedger.Rows.Add(drOpeningRows);
                    }
                    else
                    {
                        DataRow drLedgerData = dtLedger.NewRow();
                        drLedgerData["TRANSACTION_DATE"] = Convert.ToDateTime(dtLedgerData.Rows[i]["TRANSACTION_DATE"]).ToString("dd-MMM-yyyy");
                        if (Convert.ToDouble(dtLedgerData.Rows[i]["CREDIT"]) > 0)
                            drLedgerData["Mode"] = "Dr";
                        else if (Convert.ToDouble(dtLedgerData.Rows[i]["DEBIT"]) > 0)
                            drLedgerData["Mode"] = "Cr";
                        drLedgerData["party_name"] = dtLedgerData.Rows[i]["party_name"];
                        //drLedgerData["Mode"] = dtLedgerData.Rows[i]["Mode"];
                        drLedgerData["Vch_Type"] = dtLedgerData.Rows[i]["Vch_Type"];
                        drLedgerData["VOUCHER_NO"] = dtLedgerData.Rows[i]["VOUCHER_NO"];

                        drLedgerData["DEBIT"] = dtLedgerData.Rows[i]["CREDIT"];
                        drLedgerData["CREDIT"] = dtLedgerData.Rows[i]["DEBIT"];
                        drLedgerData["PARTICULARS"] = dtLedgerData.Rows[i]["PARTICULARS"];
                        //drLedgerData["curbal"] = dtLedgerData.Rows[i]["curbal"].ToString() + "" + dtLedgerData.Rows[i]["curbaltran"].ToString();
                        dtLedger.Rows.Add(drLedgerData);
                    }
                }


                DataSet drrt = GetOpeningLedgerData(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), PartyNo, Session["comp_code"].ToString());
                if (drrt.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(drrt.Tables[0].Rows[0]["closing"]) < 0)
                    {

                        debitOpening = -(Convert.ToDouble(drrt.Tables[0].Rows[0]["closing"]));
                    }


                    if (Convert.ToInt32(drrt.Tables[0].Rows[0]["closing"]) > 0)
                    {

                        creditOpening = (Convert.ToDouble(drrt.Tables[0].Rows[0]["closing"]));
                    }


                }
                //for showing total
                DataRow rowTotal = dtLedger.NewRow();
                rowTotal["TRANSACTION_DATE"] = "";
                rowTotal["VOUCHER_NO"] = "";
                rowTotal["Mode"] = "";
                rowTotal["party_name"] = "";
                rowTotal["Vch_Type"] = "";
                rowTotal["VOUCHER_NO"] = "";
                credittotal = Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]);
                debittotal = Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]);

                rowTotal["CREDIT"] = "<strong>" + (credittotal + creditOpening) + "</strong>";
                rowTotal["DEBIT"] = "<strong>" + (debittotal + debitOpening) + "</strong>";
                rowTotal["PARTICULARS"] = "";
                //rowTotal["curbal"] = "";
                //row[0] = "<strong>This is heading 1</strong>";
                //row[1] = "<strong>This is heading 2</strong>";
                //row[2] = "<strong>This is heading 3</strong>";
                //dt.Rows.Add(row);
                //dt.AcceptChanges();

                double debitClosOpening = 0;
                double creditClosOpening = 0;

                debitClosOpening = debittotal + debitOpening;
                creditClosOpening = credittotal + creditOpening;


                dtLedger.Rows.Add(rowTotal);
                dtLedger.AcceptChanges();

                //for showing Closing balance
                DataRow rowCloseBal = dtLedger.NewRow();
                rowCloseBal["TRANSACTION_DATE"] = "";
                rowCloseBal["Mode"] = "";
                rowCloseBal["party_name"] = "Closing Balance";
                rowCloseBal["VOUCHER_NO"] = "";
                rowCloseBal["Vch_Type"] = "";
                rowCloseBal["VOUCHER_NO"] = "";


                if (Convert.ToDouble(creditClosOpening) < Convert.ToDouble(debitClosOpening))
                {
                    debitClosing = 0;
                    creditClosing = Convert.ToDouble(debitClosOpening) - Convert.ToDouble(creditClosOpening);
                    rowCloseBal["CREDIT"] = "<strong>" + (Convert.ToDouble(debitClosOpening) - Convert.ToDouble(creditClosOpening)).ToString() + "</strong>";
                    //rowCloseBal["CREDIT"] = "<strong>" + (Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]) + creditOpening).ToString() + "</strong>";
                    rowCloseBal["DEBIT"] = "";
                }
                else
                {
                    creditClosing = 0;
                    debitClosing = Convert.ToDouble(creditClosOpening) - Convert.ToDouble(debitClosOpening);
                    rowCloseBal["CREDIT"] = "";
                    rowCloseBal["DEBIT"] = "<strong>" + (Convert.ToDouble(creditClosOpening) - Convert.ToDouble(debitClosOpening)).ToString() + "</strong>";
                }



                //if (Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) < Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]))
                //{
                //    debitClosing = 0;
                //    creditClosing = Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]);
                //    rowCloseBal["CREDIT"] = "<strong>" + (Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]) ).ToString() + "</strong>";
                //    //rowCloseBal["CREDIT"] = "<strong>" + (Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]) + creditOpening).ToString() + "</strong>";
                //    rowCloseBal["DEBIT"] = "";
                //}
                //else
                //{
                //    creditClosing = 0;
                //    debitClosing = Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]);
                //    rowCloseBal["CREDIT"] = "";
                //    rowCloseBal["DEBIT"] = "<strong>" + (Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["debit_sum"]) - Convert.ToDouble(dsLedgerData.Tables[0].Rows[0]["credit_sum"]) ).ToString() + "</strong>";
                //}


                rowCloseBal["PARTICULARS"] = "";
                //rowCloseBal["curbal"] = "";

                dtLedger.Rows.Add(rowCloseBal);
                dtLedger.AcceptChanges();

                //for showing grand total
                DataRow rowGrandTotal = dtLedger.NewRow();

                rowGrandTotal["VOUCHER_NO"] = "Total";
                rowGrandTotal["CREDIT"] = "<strong>" + (creditClosOpening + creditClosing).ToString() + "</strong>";
                rowGrandTotal["DEBIT"] = "<strong>" + (debitClosOpening + debitClosing).ToString() + "</strong>";
                dtLedger.Rows.Add(rowGrandTotal);
                dtLedger.AcceptChanges();

                GridExcel.DataSource = dtLedger;
                GridExcel.DataBind();

                if (rdbWithoutNarration.Checked == true)
                    GridExcel.Columns[7].Visible = false;
                //if (chkRunningTot.Checked == true)
                //    GridExcel.Columns[8].Visible = true;
                //else
                //    GridExcel.Columns[8].Visible = false;

                if (dsLedgerData.Tables[0].Rows.Count > 0)
                {
                    //To add heading in excel
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell = new TableCell();
                    HeaderCell = new TableCell();
                    HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
                    HeaderCell.ColumnSpan = 7;
                    HeaderCell.BackColor = System.Drawing.Color.White;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    GridExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

                    GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell1 = new TableCell();
                    HeaderCell1.Text = "Ledger Account";
                    HeaderCell1.ColumnSpan = 7;
                    HeaderCell1.BackColor = System.Drawing.Color.White;
                    HeaderCell1.ForeColor = System.Drawing.Color.Black;
                    HeaderGridRow1.Cells.Add(HeaderCell1);
                    GridExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

                    GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell2 = new TableCell();
                    HeaderCell2 = new TableCell();
                    HeaderCell2.Text = txtAcc.Text.Split('*')[0];
                    HeaderCell2.ColumnSpan = 7;
                    HeaderCell2.BackColor = System.Drawing.Color.White;
                    HeaderCell2.ForeColor = System.Drawing.Color.Black;
                    HeaderGridRow2.Cells.Add(HeaderCell2);
                    GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);

                    GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
                    HeaderCell.ColumnSpan = 7;
                    HeaderCell.BackColor = System.Drawing.Color.White;
                    HeaderCell.ForeColor = System.Drawing.Color.Black;
                    HeaderGridRow3.Cells.Add(HeaderCell);
                    GridExcel.Controls[0].Controls.AddAt(3, HeaderGridRow3);

                    GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.ColumnSpan = 7;
                    HeaderCell.BackColor = System.Drawing.Color.White;
                    HeaderGridRow4.Cells.Add(HeaderCell);
                    GridExcel.Controls[0].Controls.AddAt(4, HeaderGridRow4);

                    string attachment = "attachment; filename=LedgerBook.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GridExcel.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            else
            {
                {
                    dsAllLedgerData = objTrialBalance.GetDatawithoutLedger(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), "0", Session["colcode"].ToString(), "0");
                }
                if (dsAllLedgerData.Tables[0].Rows.Count > 0)
                {
                    GridView gvdata = new GridView();
                    gvdata.DataSource = dsAllLedgerData;
                    gvdata.DataBind();
                    string attachment = "attachment; filename=LedgerBook.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvdata.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LedgerReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    //protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "MyUpdate")
    //    {
    //        string vchType = string.Empty;
    //        string LedgerName = txtAcc.Text.Split('*')[0].ToString();
    //        string PARTY_NO = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME='" + LedgerName + "'");
    //        lblLedger.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO=" + PARTY_NO);
    //        LinkButton lnkBtn1 = e.Item.FindControl("lblPartyName") as LinkButton;
    //        int VoucherNo = Convert.ToInt32(lnkBtn1.CommandArgument);
    //        string VoucherType = lnkBtn1.ToolTip;
    //        int VchSeq = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "VOUCHER_SQN", "VOUCHER_NO='" + VoucherNo + "' AND TRANSACTION_TYPE='" + lnkBtn1.ToolTip + "'"));

    //        DateTime from = Convert.ToDateTime(txtFrmDate.Text);
    //        DateTime to = Convert.ToDateTime(txtUptoDate.Text);
    //        string pram = (VchSeq + ",'" + vchType + "'," + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MM-yyyy")) + "," + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MM-yyyy")) + "','" + LedgerName + "','" + PARTY_NO + "'," + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MM-yyyy")) + "," + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MM-yyyy")) + "");
    //        Response.Redirect("AccountingVouchers.aspx?obj=config," + pram);
    //    }
    //}

    protected void lblPartyName_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            int VoucherNo = Convert.ToInt32(lnkbtn.CommandArgument);
            string vchType = string.Empty;
            string VoucherType = lnkbtn.ToolTip;
            if (VoucherType == "Payment")
                vchType = "P";
            if (VoucherType == "Journal")
                vchType = "J";
            if (VoucherType == "Receipt")
                vchType = "R";
            if (VoucherType == "Contra")
                vchType = "C";

            string LedgerName = txtAcc.Text.Split('*')[0].ToString();
            string PARTY_NO = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME='" + LedgerName + "'");
            //lblLedger.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO=" + PARTY_NO);
            //LinkButton lnkBtn1 = RptData.FindControl("lblPartyName") as LinkButton;
            //HiddenField hdnTransferEntry = RptData.FindControl("hdnTransferEntry") as HiddenField;

            int VchSeq = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "DISTINCT VOUCHER_SQN", "VOUCHER_NO='" + VoucherNo + "' AND TRANSACTION_TYPE='" + vchType + "'"));
            int TransferEntry = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "DISTINCT TRANSFER_ENTRY", "VOUCHER_NO='" + VoucherNo + "' AND TRANSACTION_TYPE='" + vchType + "'"));
            if (Convert.ToInt32(TransferEntry) > 0)
            {
                //lblErrorMsg.Text = "Transfer entries cannot Edit";
                objCommon.DisplayMessage(UPDLedger, "Transfer entries cannot Edit", this);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transfer entries cannot Edit')", true);
                return;
                //lnkBtn1.Attributes.Add("onClick", "alert('Transfer Entry Can not Edit')");
            }
            else
            {
                DateTime from = Convert.ToDateTime(txtFrmDate.Text);
                DateTime to = Convert.ToDateTime(txtUptoDate.Text);
                string pram = (VchSeq + "," + VoucherType + "," + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd/MM/yyyy")) + "," + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd/MM/yyyy")) + "&ledger=" + LedgerName + "&party_no=" + PARTY_NO + "&fromDate=" + (Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd/MM/yyyy")) + "&Todate=" + (Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd/MM/yyyy")) + "");
                Response.Redirect("AccountingVouchers.aspx?obj=LedgerReport," + pram, true);
            }
        }
        catch
        {
            throw;
        }
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetLedgerkName(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetLedgerReport(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }
    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.Split('*').Length > 1)
        {
            int partyId = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE=" + txtAcc.Text.Split('*')[1].ToString()));
            double Balance = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "BALANCE", "PARTY_NO=" + partyId));
            if (Balance > 0)
                lblCurBal.Text = Balance.ToString() + " Dr";
            else
                lblCurBal.Text = Balance.ToString() + " Cr";
        }
        else
        {
            objCommon.DisplayMessage("Enter Ledger Name.", this);
            txtAcc.Focus();
        }
    }
    public DataSet GetOpeningLedgerData(string FromDate, string Ledger, string code_year)
    {
        DataSet ds = null;
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_FROMDATE", FromDate);
            objParams[1] = new SqlParameter("@P_LEDGER", Ledger);
            objParams[2] = new SqlParameter("@P_CODE_YEAR", code_year);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_Get_opbal_FOR_LEDGER", objParams);

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossDrCr-> " + ex.ToString());
        }
        return ds;
    }
}