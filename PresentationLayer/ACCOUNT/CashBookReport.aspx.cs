//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : 
// CREATION DATE : 06-APR-2012                                               
// CREATED BY    : KAPIL BUDHLANI                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the Cash Book report.
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Transactions;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using IITMS.NITPRM;

public partial class CashBookReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    //CashBook objCash=new CashBook ();
    FinCashBook objCash = new FinCashBook();
    FinanceCashBookController objCashCon;
    CombinedCashBankBookController objCcbc;
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
            objCashCon = new FinanceCashBookController();
            objCcbc = new CombinedCashBankBookController();

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

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

                    //objCommon.DisplayUserMessage(UPDLedger,"Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    // SetFinancialYear();    
                    txtFrmDate.Text = DateTime.Now.ToShortDateString();
                    txtUptoDate.Text = DateTime.Now.ToShortDateString();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    lstCash.Items.Clear();
                    DataSet dsCash = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "payment_type_no=1", "PARTY_NO");
                    lstCash.DataTextField = "PARTY_NAME";
                    lstCash.DataValueField = "PARTY_NO";
                    lstCash.DataSource = dsCash.Tables[0];
                    lstCash.DataBind();
                }
            }
        }
        SetFinancialYear();
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
            if (!Page.IsPostBack)
            {
                txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
                txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
            }
        }
        dtr.Close();
    }

    private void SetFinancialYear1()
    {
        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            //Session["comp_code"] = dtr["COMPANY_CODE"];
            //Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            //Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");

        }
        dtr.Close();
    }
    private void CheckPageAuthorization()
    {
        //if (Request.QueryString["obj"] != null)
        //{
        //    if (Request.QueryString["obj"].ToString().Trim() != "config")
        //    {
        //        if (Request.QueryString["pageno"] != null)
        //        {
        //            //Check for Authorization of Page
        //            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
        //            {
        //                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
        //            }
        //        }
        //        else
        //        {
        //            //Even if PageNo is Null then, don't show the page
        //            Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
        //        }

        //    }

        //}
        //else
        //{


        //    if (Request.QueryString["pageno"] != null)
        //    {
        //        //Check for Authorization of Page
        //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
        //        {
        //            Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
        //        }
        //    }
        //    else
        //    {
        //        //Even if PageNo is Null then, don't show the page
        //        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
        //    }

        //}




    }

    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (lstCash.SelectedIndex != -1)
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

            //if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            //{
            //    objCommon.DisplayUserMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            //    txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            //    txtUptoDate.Focus();
            //    return;
            //}

            //if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            //{
            //    objCommon.DisplayUserMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            //    txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            //    txtFrmDate.Focus();
            //    return;
            //}
            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
                txtUptoDate.Focus();
                return;
            }
            //GenerateReport();
            //if (rdbWithNarration.Checked)
            //{
            //    ShowReport("CashBook", "CashbookReportWithNarration.rpt");
            //}
            //else
            //{
            //    ShowReport("CashBook", "CashbookReport.rpt");
            //}
            //if (RdbYearly.Checked)
            //{
            //    SetFinancialYear1();
            //    ShowReport("CashBook", "BankBook.rpt");
            //}
            //else
            //{
            //if (rdbMonthWise.Checked)
            //ShowReport("CashBook", "BankBook.rpt");
            //if (rdbDayWise1.Checked)
            //    ShowReport("CashBook", "BankBookDayWise.rpt");
            //}
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
        }
        else
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select CashBook.", this);
        }
    }

    protected void btnCondensed_Click(object sender, EventArgs e)
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

        ShowConReport("Condensed CashBook Book", "Ledger_Book_con.rpt");
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
            LedgerName = "CASH IN HAND";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_LEDGERNAME=" + LedgerName;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
            objCommon.DisplayMessage("Error Occured!", this.Page);
        }
    }

    public void GenerateReport()
    {
        double OBALANCE = 0.0;
        double CBALANCE = 0.0;
        double OPAMOUNT = 0.0;
        double amt1 = 0.0;
        double amt2 = 0.0;
        string pcode = string.Empty;
        for (int i = 0; i < lstCash.Items.Count; i++)
        {
            if (i == lstCash.SelectedIndex)
                pcode = Convert.ToString(lstCash.Items[i].Value);
        }

        //prepare data for report
        //if (RdbYearly.Checked)
        //{
        objCashCon.PrepareData(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), pcode, 0);
        //}
        //else
        //{
        //    objCashCon.PrepareData(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), pcode,1);
        //}

        //string PNAME = string.Empty;
        //DataSet rscbook1 = objCashCon.GetReceiptSide();

        //for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        //{
        //    PNAME = Convert.ToString(rscbook1.Tables[0].Rows[i]["PARTY_NAME"]);
        //    DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy2", "TR_NO", "RECIEPT", "date1=" + Convert.ToDateTime(txtFrmDate.Text).ToShortDateString() + " AND (RECIEPT=0 OR RECIEPT IS NULL)", "TR_NO");

        //    if (rscbook2.Tables[0].Rows.Count < 1)
        //    {
        //        //INSERT
        //        objCash.Date = Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["Date1"]);
        //        objCash.Particular = Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]);
        //        objCash.Amt = Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]);

        //        objCash.VNo = Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]);
        //        objCash.PartyName = Convert.ToString(rscbook1.Tables[0].Rows[i]["PARTY_NAME"]);
        //        objCash.TrNo = Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tr_no"]);
        //        objCash.TrType = Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]);
        //        objCash.SubTrNo = Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]);
        //        objCash.MgrpNo = Convert.ToInt32(rscbook1.Tables[0].Rows[i]["MGRP_NO"]);
        //        objCash.TEntry = Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentryr"]);

        //        objCashCon.AddRecieptSideEntry(objCash, 0.0, 0.0, 0.0, 1, 0);//no opening closing bal insert UPDATE

        //    }
        //    else
        //    {
        //        //objCash.Date = Convert.ToDateTime(rscbook2.Tables[0].Rows[0]["Date1"]);
        //        objCash.Particular = Convert.ToString(rscbook1.Tables[0].Rows[0]["particular"]);
        //        objCash.Amt = Convert.ToDouble(rscbook1.Tables[0].Rows[0]["AMOUNT"]);
        //        objCash.VNo = Convert.ToInt32(rscbook1.Tables[0].Rows[0]["vno"]);
        //        objCash.PartyName = Convert.ToString(rscbook1.Tables[0].Rows[0]["PNAME"]);
        //        objCash.TrNo = Convert.ToInt32(rscbook2.Tables[0].Rows[0]["tr_no"]);
        //        objCash.TrType = Convert.ToString(rscbook1.Tables[0].Rows[0]["tr_type"]);
        //        objCash.SubTrNo = Convert.ToInt32(rscbook1.Tables[0].Rows[0]["subtr_no"]);
        //        objCash.MgrpNo = Convert.ToInt32(rscbook1.Tables[0].Rows[0]["MGRP_NO"]);
        //        objCash.TEntry = Convert.ToInt32(rscbook1.Tables[0].Rows[0]["tentryr"]);

        //        objCashCon.AddRecieptSideEntry(objCash, 0.0, 0.0, 0.0, 2, 0);//no opening closing bal insert UPDATE
        //    }
        //}

        ////opening balance
        //OBALANCE = 0;
        //CBALANCE = 0;
        //OPAMOUNT = 0;

        //DataSet rscom = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[0].Value), 0, 0);

        //if (rscom.Tables[0].Rows.Count > 0)
        //{
        //    OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
        //}

        ////DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[0].Value), Convert.ToInt32(lstCash.Items[0].Value), 22);
        ////OBALANCE = OPAMOUNT + (rscom1.Tables[0].Rows[0][1] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0][1])) - (rscom.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0][0]));

        //OBALANCE = OPAMOUNT;

        //rscbook1 = objCommon.FillDropDown("TEMP_ACC_CWDUMMY2", "*", "", "", "");

        //if (rscbook1.Tables[0].Rows.Count < 1)
        //{
        //    objCash.Date = Convert.ToDateTime(txtFrmDate.Text.Trim());
        //    objCashCon.AddRecieptSideEntry(objCash, OBALANCE, OBALANCE, OBALANCE, 1, 1);//opening closing bal insert
        //}
        ////end of opening balance

        //rscbook1 = objCommon.FillDropDown("TEMP_ACC_cwdummy1", "DISTINCT DATE1", "null as a", "", "");


        //for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        //{
        //    amt1 = 0;
        //    amt2 = 0;
        //    //DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy1", "*", "", "date1=convert(datetime,'" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]).ToShortDateString() + "',103)", "");
        //    DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy1", "*", "", "date1=convert(datetime,'" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]).ToShortDateString() + "',103) AND PARTY NOT LIKE '%,%'", "");
        //    for (int j = 0; j < rscbook2.Tables[0].Rows.Count; j++)
        //    {
        //        if (Convert.ToString(rscbook2.Tables[0].Rows[j]["Status"]).ToLower() == "dr")
        //            amt1 = amt1 + Convert.ToDouble(rscbook2.Tables[0].Rows[j]["AMOUNT"]);
        //        else
        //            amt2 = amt2 + Convert.ToDouble(rscbook2.Tables[0].Rows[j]["AMOUNT"]);

        //    }
        //    CBALANCE = OBALANCE + amt1 - amt2;

        //    //objCash.Date = Convert.ToDateTime(txtFrmDate.Text.Trim());
        //    objCash.Date = Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["DATE1"]);
        //    objCashCon.AddRecieptSideEntry(objCash, OBALANCE, CBALANCE, OBALANCE + amt1, 2, 1);//opening closing bal UPDATE

        //    OBALANCE = CBALANCE;
        //}
        //DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy1", "*", "", txtFrmDate.ToString(), txtUptoDate.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbWithNarration.Checked)
            {
                // url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@P_NV=" + "0" + "," + "@Report_Heading=" + lstCash.SelectedItem.Text + "," + "@P_CODE_YEAR=" + Session["comp_code"].ToString();
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + lstCash.SelectedValue.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + string.Empty + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + "0";
            }
            else
            {
                //url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@P_NV=" + "1" + "," + "@Report_Heading=" + lstCash.SelectedItem.Text + "," + "@P_CODE_YEAR=" + Session["comp_code"].ToString(); 
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + lstCash.SelectedValue.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + string.Empty + "," + "@P_ClosBalMode=" + string.Empty + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_NV=" + "1";
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


    protected void RdbMonthWise_CheckedChanged(object sender, EventArgs e)
    {
        divCalender.Visible = true;
        SetFinancialYear1();
    }
    protected void RdbYearly_CheckedChanged(object sender, EventArgs e)
    {
        divCalender.Visible = false;
        SetFinancialYear1();
    }
    protected void btnOldFormat_Click(object sender, EventArgs e)
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

        //if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
        //    txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
        //    txtUptoDate.Focus();
        //    return;
        //}

        //if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
        //    txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
        //    txtFrmDate.Focus();
        //    return;
        //}

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
        GenerateReport();
        //if (rdbWithNarration.Checked)
        //{
        //    ShowReport("CashBook", "CashbookReportWithNarration.rpt");
        //}
        //else
        //{
        //    ShowReport("CashBook", "CashbookReport.rpt");
        //}
        //if (RdbYearly.Checked)
        //{
        //SetFinancialYear1();
        ShowReport("CashBook", "CashbookReportNew.rpt");
        //}
        //else
        //{
        //    ShowReport("CashBook", "CashbookReportWithNarration.rpt");
        //}
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (lstCash.SelectedIndex == -1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select CashBook.", this);
            return;
        }
        DataSet dsBankData = objExport.GetDataForExcelBankBook(Session["comp_code"].ToString(), lstCash.SelectedValue.ToString(), Convert.ToDateTime(txtFrmDate.Text.ToString().Trim()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text.ToString().Trim()).ToString("dd-MMM-yyyy"));
        string[] columns = new string[2];
        columns[0] = "VOUCHER_NO";
        columns[1] = "Vch_Type";
        DataTable dtVouchers = dsBankData.Tables[0].DefaultView.ToTable(true, columns);//"VOUCHER_NO,Vch_Type"
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
                //DataRow[] drVoucherData = dsBankData.Tables[0].Select("VOUCHER_NO=" + dtVouchers.Rows[i]["VOUCHER_NO"].ToString() + " and Vch_Type='" + dtVouchers.Rows[i]["Vch_Type"].ToString()+"'");
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

                    //drRows["PARTY_NAME"] = "(As per Detail)";
                    //drRows["CLUB_AMT"] = "";
                    //drRows["Vch_Type"] = "";
                    //drRows["VOUCHER_NO"] = "";
                    //dtExcelData.Rows.Add(drRows);
                    //DataRow drRowsfirst = dtExcelData.NewRow();
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
                    if ((Convert.ToDecimal(objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(-total+sum(DEBIT))-sum(credit)", "", "", "").Tables[0].Rows[0][0].ToString())) < 0)
                    {
                        drClbalRows["TOTCREDIT"] = "";
                        drClbalRows["TOTDEBIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(-total+sum(DEBIT))-sum(credit)", "", "", "").Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        drClbalRows["TOTCREDIT"] = "";
                        drClbalRows["TOTDEBIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(-total+sum(DEBIT))-sum(credit)", "", "", "").Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    //if ((Convert.ToDecimal(objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(total+sum(credit))-sum(DEBIT)", "", "", "").Tables[0].Rows[0][0].ToString())) < 0)
                    if ((Convert.ToDecimal(objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(total+sum(credit))-sum(DEBIT)", "", "", "").Tables[0].Rows[0][0].ToString())) > 0)
                    {
                        drClbalRows["TOTCREDIT"] = objCommon.FillDropDown("(select top 1 openingamt as total from temp_bank_book_excel) as a,temp_bank_book_excel as b group by b.PARTY_NO,a.total", "(total+sum(credit))-sum(DEBIT)", "", "", "").Tables[0].Rows[0][0].ToString();
                        drClbalRows["TOTDEBIT"] = "";
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

        if (dsBankData.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "Cash Book";
            HeaderCell1.ColumnSpan = 8;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            gvTrialBalance.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = lstCash.SelectedItem.Text;
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
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow4.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(4, HeaderGridRow4);


            //gvTrialBalance.Controls[0].Controls.AddAt(0, HeaderGridRow);

            string attachment = "attachment; filename=" + lstCash.SelectedItem.Text.Replace(" ", "_") + ".xls";
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
        SetFinancialYear();
        chkRunning.Checked = false;
        lstCash.SelectedIndex = -1;
        rdbWithNarration.Checked = true;
        rdbWithoutNarration.Checked = false;
        rdbMonthWise.Checked = true;
        rdbDayWise1.Checked = false;
    }
}

