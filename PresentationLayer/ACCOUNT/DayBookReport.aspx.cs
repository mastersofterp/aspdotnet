//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : DAY BOOK REPORT                                                    
// CREATION DATE : 24-May-2010                                               
// CREATED BY    : ASHISH THAKRE                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using IITMS.NITPRM;


public partial class DayBookReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");

        if (hdnBal.Value != "")
        {
            //lblCurBal.Text = hdnBal.Value;
            //lblmode.Text = hdnMode.Value;
        }

        Session["WithoutCashBank"] = "N";
        //btnGo.Attributes.Add("onClick", "return CheckFields();");
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
                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                  

                    //PopulateDropDown();
                    //PopulateListBox();

                    ViewState["action"] = "add";
                    pnlDatewise.Visible = false;
                    trDatewise.Visible = false;
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
            txtDate.Text = DateTime.Now.Date.ToShortDateString();
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
        // SetDataColumn();
        Session["VchDatatable"] = null;
        //RptData.DataSource = null;
        //RptData.DataBind();
        //txtAcc.Focus();

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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //int voucherNo = 0;
            //if (isVoucherAuto == "Y")
            //    voucherNo = Convert.ToInt16(txtVoucherNo.Text) - 1;
            //else
            //    voucherNo = Convert.ToInt16(txtVoucherNo.Text);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            // ClMode = lblmode.Text.ToString().Trim();
            string LedgerName = string.Empty;
            //if (txtAcc.Text.ToString().Trim().Split('*')[0].ToString() == "1")
            //{
            //    LedgerName = "Cash Book";

            //}
            //else if (txtAcc.Text.ToString().Trim().Split('*')[0].ToString() == "2")
            //{
            //    LedgerName = "Bank Book";

            //}
            //else
            //{
            //LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");

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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateTrialBalanceFormatNew -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.ArrangeFormat -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateTrialBalanceFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForLedger -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForLedgerNew -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForMainLedger -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }



    private void ShowReportReceiptPayment(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            // ClMode = lblmode.Text.ToString().Trim();
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
    protected void btndb_Click(object sender, EventArgs e)
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
            TrialBalanceReportController od = new TrialBalanceReportController();
            //od.DeleteDayBookRecord();
            //GetDayBookReportFormat();
            OrderDaybook();

            if (chkdept.Checked)
            {
               
                    ShowDayBookDeartment("DayBook", "DayBookDepartment.rpt");
                
            }
            else
            {
                ShowDayBook("DayBook", "DayBook.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowDayBook(string reportTitle, string rptFileName)
    {
        try
        {
            //string Script = string.Empty;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            //string LedgerName = string.Empty;

            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //if (txtFrmDate.Text.ToString().Trim() == txtUptoDate.Text.ToString().Trim())
            //{
            //    url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + "For "+ Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();
            //}
            //else
            //{
            //    url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();
            //}

            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

            objCommon = new Common();

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (txtFrmDate.Text.ToString().Trim() == txtUptoDate.Text.ToString().Trim())
            {
                if (rdbWithNarration.Checked == true)
                {
                    url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0";
                }
                else
                {
                    url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1";
                }
            }
            else
            {
                if (rdbWithNarration.Checked == true)
                {
                    if (radBetTwoDates.Checked)
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0";
                    else
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0";
                }
                else
                {
                    if (radBetTwoDates.Checked)
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1";
                    else
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1";
                }
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
        if (radBetTwoDates.Checked)
            ot.OrderDaybookReportFormat(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
        else
            ot.OrderDaybookReportFormat(Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
    }
    public void GetDayBookReportFormat()
    {
        try
        {
            int id = 0;
            int i = 0;
            DataSet ds = new DataSet();
            TrialBalanceReportController od1 = new TrialBalanceReportController();
            if (radBetTwoDates.Checked)
            {
                ds = od1.GetDistinctVoucherNo(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
            }
            if (radDay.Checked)
            {
                ds = od1.GetDistinctVoucherNo(Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
            }


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TrialBalanceReportController od2 = new TrialBalanceReportController();
                        if (ds.Tables[0].Rows[i][1].ToString() == "R")
                        {
                            DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]), ds.Tables[0].Rows[i]["TRANSACTION_TYPE"].ToString());
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                id = AddReceiptVoucherEntry(ds1.Tables[0], id);
                            }
                        }
                        if (ds.Tables[0].Rows[i][1].ToString() == "P")
                        {
                            DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]), ds.Tables[0].Rows[i]["TRANSACTION_TYPE"].ToString());
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                id = AddPaymentVoucherEntry(ds1.Tables[0], id);
                            }
                        }

                        if (ds.Tables[0].Rows[i][1].ToString() == "J")
                        {
                            DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]), ds.Tables[0].Rows[i]["TRANSACTION_TYPE"].ToString());
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                id = AddJournalVoucherEntry(ds1.Tables[0], id);
                            }
                        }
                        if (ds.Tables[0].Rows[i][1].ToString() == "C")
                        {
                            DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]), ds.Tables[0].Rows[i]["TRANSACTION_TYPE"].ToString());
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                id = AddContraVoucherEntry(ds1.Tables[0], id);
                            }
                        }
                    }


                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DayBookreport.GetDayBookReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private int AddPaymentVoucherEntry(DataTable dtp, int id) //function for Payment
    {
        try
        {
            objCommon = new Common();

            int m = 0;
            for (m = 0; m < dtp.Rows.Count; m++)
            {
                if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
                {
                    AccountTransactionController od8 = new AccountTransactionController();
                    //od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[m]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1, dtp.Rows[m]["Party_No"].ToString().Trim());
                    od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[m]["voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1, dtp.Rows[m]["Party_No"].ToString().Trim(), dtp.Rows[m]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[m]["Tran"]).Trim());
                    id = id + 1;
                }

            }

            int l = 0;
            for (l = 0; l < dtp.Rows.Count; l++)
            {
                if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
                {
                    AccountTransactionController od7 = new AccountTransactionController();
                    //od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[l]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1, dtp.Rows[l]["Party_No"].ToString().Trim());
                    od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[l]["VOUCHER_NO"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1, dtp.Rows[l]["Party_No"].ToString().Trim(), dtp.Rows[l]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[l]["Tran"]).Trim());
                    id = id + 1;
                }

            }

            int n = 0;
            for (n = 0; n < dtp.Rows.Count; n++)
            {
                if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
                {
                    AccountTransactionController od9 = new AccountTransactionController();
                    if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtp.Rows[n]["Party_No"].ToString().Trim(), dtp.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[n]["Tran"]).Trim());
                    }
                    else
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "Payment", dtp.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtp.Rows[n]["Party_No"].ToString().Trim(), dtp.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[n]["Tran"]).Trim());
                    }





                    id = id + 1;
                }

            }



            return id;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DayBookreport.AddPaymentVoucherEntry -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            return 0;
        }

    }// End Of function for Payment
    private int AddContraVoucherEntry(DataTable dtp, int id) //function for Contra
    {
        try
        {
            int l = 0;
            for (l = 0; l < dtp.Rows.Count; l++)
            {
                if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
                {
                    AccountTransactionController od7 = new AccountTransactionController();
                    od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Contra", dtp.Rows[l]["voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1, dtp.Rows[l]["Party_No"].ToString().Trim(), dtp.Rows[l]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[l]["Tran"]).Trim());
                    id = id + 1;
                }

            }
            int m = 0;
            for (m = 0; m < dtp.Rows.Count; m++)
            {
                if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
                {
                    AccountTransactionController od8 = new AccountTransactionController();
                    od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "Contra", dtp.Rows[m]["voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1, dtp.Rows[m]["Party_No"].ToString().Trim(), dtp.Rows[m]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[m]["Tran"]).Trim());
                    id = id + 1;
                }

            }


            int n = 0;
            for (n = 0; n < dtp.Rows.Count; n++)
            {
                if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
                {
                    AccountTransactionController od9 = new AccountTransactionController();
                    if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtp.Rows[n]["Party_No"].ToString().Trim(), dtp.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[n]["Tran"]).Trim());
                    }
                    else
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), dtp.Rows[l]["voucher_no"].ToString().Trim(), dtp.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtp.Rows[n]["Party_No"].ToString().Trim(), dtp.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), Convert.ToString(dtp.Rows[n]["Tran"]).Trim());
                    }





                    id = id + 1;
                }

            }





            return id;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DayBookreport.AddContraVoucherEntry -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            return 0;
        }

    }// End Of function for contra
    private int AddReceiptVoucherEntry(DataTable dtr, int id) //function for Receipt
    {
        try
        {
            int m = 0;
            for (m = 0; m < dtr.Rows.Count; m++)
            {
                if (Convert.ToString(dtr.Rows[m]["Tran"]).Trim() == "Dr")
                {
                    AccountTransactionController od8 = new AccountTransactionController();
                    //newly added by naresh warbhe
                    if (dtr.Rows[m]["TRANSFER_ENTRY"].ToString().Equals("True"))
                    {
                        //od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[m]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1, dtr.Rows[m]["Party_No"].ToString().Trim());
                        od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[m]["voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1, dtr.Rows[m]["Party_No"].ToString().Trim(), dtr.Rows[m]["Particulars"].ToString().Trim().Replace("'", "''"), dtr.Rows[m]["TRAN"].ToString().Trim());
                        id = id + 1;
                    }
                    else
                    {
                        //od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[m]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1, dtr.Rows[m]["Party_No"].ToString().Trim());
                        od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[m]["voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1, dtr.Rows[m]["Party_No"].ToString().Trim(), dtr.Rows[m]["Particulars"].ToString().Trim().Replace("'", "''"), dtr.Rows[m]["TRAN"].ToString().Trim());
                        id = id + 1;
                    }
                    //old code
                    //AccountTransactionController od8 = new AccountTransactionController();
                    //od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[m]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1, dtr.Rows[m]["Party_No"].ToString().Trim());
                    //id = id + 1;
                }

            }
            int l = 0;
            for (l = 0; l < dtr.Rows.Count; l++)
            {
                if (Convert.ToString(dtr.Rows[l]["Tran"]).Trim() == "Cr")
                {
                    AccountTransactionController od7 = new AccountTransactionController();
                    //od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[l]["Party_Name"].ToString().Trim(), "", dtr.Rows[l]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtr.Rows[l]["Amount"].ToString().Trim()), 1, dtr.Rows[l]["Party_No"].ToString().Trim());
                    od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[l]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[l]["VOUCHER_NO"].ToString(), 0, Convert.ToDouble(dtr.Rows[l]["Amount"].ToString().Trim()), 1, dtr.Rows[l]["Party_No"].ToString().Trim(), dtr.Rows[l]["Particulars"].ToString().Trim().Replace("'", "''"), dtr.Rows[l]["TRAN"].ToString().Trim());
                    id = id + 1;
                }

            }



            int n = 0;
            for (n = 0; n < dtr.Rows.Count; n++)
            {
                if (Convert.ToString(dtr.Rows[n]["Particulars"]).Trim() != "")
                {
                    AccountTransactionController od9 = new AccountTransactionController();
                    if (Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() != "")
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtr.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "", dtr.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtr.Rows[n]["Party_No"].ToString().Trim(), dtr.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), dtr.Rows[n]["TRAN"].ToString().Trim());
                    }
                    else
                    {
                        od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "Receipt", dtr.Rows[n]["voucher_no"].ToString().Trim(), 0, 0, 0, dtr.Rows[n]["Party_No"].ToString().Trim(), dtr.Rows[n]["Particulars"].ToString().Trim().Replace("'", "''"), dtr.Rows[n]["TRAN"].ToString().Trim());
                    }





                    id = id + 1;
                }

            }

            return id;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DayBookreport.AddReceiptVoucherEntry -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            return 0;
        }
    }// End Of function for Receipt
    private int AddJournalVoucherEntry(DataTable dtj, int id)//function for JV
    {
        try
        {
            int u = 0;
            double Amt = 0;
            string vtype = string.Empty;
            string vno = string.Empty;
            string name = string.Empty;
            string TrnDate = string.Empty;
            string trntype = string.Empty;
            int ind = 0;
            int bold = 0;
            string party_no = string.Empty;
            string narration = string.Empty;

            DataTable dtTemp;
            for (u = 0; u < dtj.Rows.Count; u++)
            {
                if (Convert.ToDouble(dtj.Rows[u]["Amount"]) > Amt)
                {
                    Amt = Convert.ToDouble(dtj.Rows[u]["Amount"]);
                    vtype = "Journal";
                    //vno = Convert.ToString(dtj.Rows[u]["str_voucher_no"]).Trim();
                    vno = Convert.ToString(dtj.Rows[u]["voucher_no"]).Trim();
                    name = Convert.ToString(dtj.Rows[u]["party_name"]).Trim();
                    trntype = Convert.ToString(dtj.Rows[u]["tran"]).Trim();
                    TrnDate = Convert.ToString(Convert.ToDateTime(dtj.Rows[u]["transaction_date"]).ToString("dd/MMM/yyyy"));
                    party_no = Convert.ToString(dtj.Rows[u]["party_no"]).Trim();
                    narration = Convert.ToString(dtj.Rows[u]["Particulars"]).Trim().Replace("'", "''");
                    bold = 1;
                    ind = u;


                }


            }


            AccountTransactionController od4 = new AccountTransactionController();
            if (trntype == "Dr")
            {
                od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, Amt, 0, 1, party_no, narration, trntype);
                id = id + 1;
            }
            else
            {
                od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, 0, Amt, 1, party_no, narration, trntype);
                id = id + 1;
            }
            dtTemp = dtj;
            dtj.Rows[ind].Delete();
            dtj.AcceptChanges();

            int y = 0;
            for (y = 0; y < dtj.Rows.Count; y++)
            {
                AccountTransactionController od5 = new AccountTransactionController();

                if (Convert.ToString(dtj.Rows[y]["tran"]).Trim() == "Dr")
                {
                    od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "Journal", Convert.ToString(dtj.Rows[y]["voucher_no"]).Trim(), Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 0, 1, dtj.Rows[y]["Party_No"].ToString().Trim(), dtj.Rows[y]["PARTICULARS"].ToString().Trim().Replace("'", "''"), trntype);
                    id = id + 1;
                }
                else
                {
                    od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "Journal", Convert.ToString(dtj.Rows[y]["voucher_no"]).Trim(), 0, Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 1, dtj.Rows[y]["Party_No"].ToString().Trim(), dtj.Rows[y]["PARTICULARS"].ToString().Trim().Replace("'", "''"), trntype);
                    id = id + 1;

                }
            }


            int o = 0;
            for (o = 0; o < dtTemp.Rows.Count; o++)
            {
                if (Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim() != "")
                {
                    AccountTransactionController od10 = new AccountTransactionController();
                    od10.AddDayBookReportFormat(id, Convert.ToDateTime(dtTemp.Rows[o]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim(), "Journal", dtTemp.Rows[o]["voucher_no"].ToString().Trim(), 0, 0, 0, dtTemp.Rows[o]["Party_No"].ToString().Trim(), dtTemp.Rows[o]["PARTICULARS"].ToString().Trim().Replace("'", "''"), trntype);
                    id = id + 1;
                }

            }

            return id;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DayBookreport.AddJournalVoucherEntry -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            return 0;
        }

    }// End Of function for JV

    //protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    //{
    //    //if (txtAcc.Text.ToString().Trim() != "")
    //    //{
    //    //    btnGo_Click(sender, e);
    //    //}
    //    //txtAcc.Focus();

    //}
    //protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    //{
    //    //if (txtAcc.Text.ToString().Trim() != "")
    //    //{
    //    //    btnGo_Click(sender, e);
    //    //}
    //    txtUptoDate.Focus();
    //}

    private void ShowReportDaywise(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@P_COLCODE=" + Session["comp_code"].ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_Company_Name=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@UserName=" + Session["userfullname"].ToString();
            url += "&param=@P_Company_Name=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_COLCODE=" + Session["COMP_CODE"].ToString().Trim().ToUpper() + "," + "@P_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();

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
    protected void radBetTwoDates_CheckedChanged(object sender, EventArgs e)
    {
        if (radBetTwoDates.Checked)
        {
            pnlDatewise.Visible = false;
            pnlBetTwoDates.Visible = true;
            trDatewise.Visible = false;
            trBtn.Visible = true;
        }
    }
    protected void radDay_CheckedChanged(object sender, EventArgs e)
    {
        if (radDay.Checked)
        {
            //pnlDatewise.Visible = true;
            //pnlBetTwoDates.Visible = false;
            pnlDatewise.Visible = true;
            pnlBetTwoDates.Visible = false;
            trDatewise.Visible = true;
            trBtn.Visible = false;
            //btndbOther.Visible = false;
        }
    }
    protected void btndbOther_Click(object sender, EventArgs e)
    {
        //ShowReportDaywise("DayBook_Report", "DaybookFormat2.rpt");
        //btndb_Click

        btndb_Click(sender, e);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
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
            ExporttoExcel();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDaywiseExport_Click(object sender, EventArgs e)
    {
        btnExportExcel_Click(sender, e);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    private void ExporttoExcel()
    {
        try
        {
            TrialBalanceReportController ot = new TrialBalanceReportController();

            if (radBetTwoDates.Checked)//Between Dates
            {
                if (rdbWithNarration.Checked == true)//With Narration
                {
                    ot.OrderDaybookReportFormat(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
                    DataSet ds = ot.GetDayBookRecordforExcell();
                    DataSet dsTotal = objCommon.FillDropDown("ACC_DAYBOOK_REPORT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();

                    GridExcel.Columns[2].Visible = false;
                    GridExcel.Columns[8].Visible = false;
                    GridExcel.Columns[9].Visible = false;
                    #region Add Total
                    DataRow rowTotal = ds.Tables[0].NewRow();
                    rowTotal["VchNo"] = "Total";
                    rowTotal["DEBIT"] = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
                    rowTotal["CREDIT"] = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
                    ds.Tables[0].Rows.Add(rowTotal);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                        GridExcel.DataSource = ds;
                        GridExcel.DataBind();
                        foreach (GridViewRow oItem in GridExcel.Rows)
                        {
                            if (oItem.Cells[5].Text == "Total")
                            {
                                oItem.Cells[5].Attributes.Add("class", "FinalHead");
                                oItem.Cells[6].Attributes.Add("class", "FinalHead");
                                oItem.Cells[7].Attributes.Add("class", "FinalHead");
                            }
                        }

                        AddHeader();

                        HttpContext.Current.Response.Clear();
                        string attachment = "attachment; filename=DayBook.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.MS-excel";
                        StringWriter sw = new StringWriter();

                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Response.Write(FinalHead);

                        GridExcel.RenderControl(htw);
                        Response.Write(sw.ToString());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.End();
                        //Response.Flush();
                    }
                    #endregion Add Total
                }
                else//Without Narration
                {
                    ot.OrderDaybookReportFormat(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
                    DataSet ds = ot.GetDayBookRecordforExcell();
                    DataSet dsTotal = objCommon.FillDropDown("ACC_DAYBOOK_REPORT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();

                    GridExcel.Columns[2].Visible = false;
                    GridExcel.Columns[7].Visible = false;
                    GridExcel.Columns[8].Visible = false;
                    GridExcel.Columns[9].Visible = false;
                    #region Add Total

                    DataRow rowTotal = ds.Tables[0].NewRow();
                    rowTotal["VchNo"] = "Total";
                    rowTotal["DEBIT"] = dsTotal.Tables[0].Rows[0]["totDebit"];
                    rowTotal["CREDIT"] = dsTotal.Tables[0].Rows[0]["totCredit"];
                    ds.Tables[0].Rows.Add(rowTotal);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                        GridExcel.DataSource = ds;
                        GridExcel.DataBind();
                        foreach (GridViewRow oItem in GridExcel.Rows)
                        {
                            if (oItem.Cells[5].Text == "Total")
                            {
                                oItem.Cells[5].Attributes.Add("class", "FinalHead");
                                oItem.Cells[6].Attributes.Add("class", "FinalHead");
                                oItem.Cells[7].Attributes.Add("class", "FinalHead");
                            }
                        }

                        AddHeaderWithout();

                        HttpContext.Current.Response.Clear();
                        string attachment = "attachment; filename=DayBook.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.MS-excel";
                        StringWriter sw = new StringWriter();

                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Response.Write(FinalHead);

                        GridExcel.RenderControl(htw);
                        Response.Write(sw.ToString());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        //Response.Flush();
                        Response.End();
                    }
                    #endregion Add Total
                }
            }
            else//Single Dates
            {
                if (rdbWithNarration.Checked == true)//With Narration
                {
                    ot.OrderDaybookReportFormat(Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
                    DataSet ds = ot.GetDayBookRecordforExcell();
                    DataSet dsTotal = objCommon.FillDropDown("ACC_DAYBOOK_REPORT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

                    //AddHeader();

                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();

                    GridExcel.Columns[2].Visible = false;
                    GridExcel.Columns[8].Visible = false;
                    GridExcel.Columns[9].Visible = false;
                    #region Add Total

                    DataRow rowTotal = ds.Tables[0].NewRow();
                    rowTotal["VchNo"] = "Total";
                    rowTotal["DEBIT"] = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
                    rowTotal["CREDIT"] = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
                    ds.Tables[0].Rows.Add(rowTotal);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                        GridExcel.DataSource = ds;
                        GridExcel.DataBind();
                        foreach (GridViewRow oItem in GridExcel.Rows)
                        {
                            if (oItem.Cells[5].Text == "Total")
                            {
                                oItem.Cells[5].Attributes.Add("class", "FinalHead");
                                oItem.Cells[6].Attributes.Add("class", "FinalHead");
                                oItem.Cells[7].Attributes.Add("class", "FinalHead");
                            }
                        }

                        AddHeader();

                        HttpContext.Current.Response.Clear();
                        string attachment = "attachment; filename=DayBook.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.MS-excel";
                        StringWriter sw = new StringWriter();

                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Response.Write(FinalHead);

                        GridExcel.RenderControl(htw);
                        Response.Write(sw.ToString());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        //Response.Flush();
                        Response.End();
                    }
                    #endregion Add Total
                }
                else//Without Narration
                {
                    ot.OrderDaybookReportFormat(Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
                    DataSet ds = ot.GetDayBookRecordforExcell();
                    DataSet dsTotal = objCommon.FillDropDown("ACC_DAYBOOK_REPORT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

                    //AddHeader();

                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();

                    GridExcel.Columns[2].Visible = false;
                    GridExcel.Columns[7].Visible = false;
                    GridExcel.Columns[8].Visible = false;
                    GridExcel.Columns[9].Visible = false;
                    #region Add Total

                    DataRow rowTotal = ds.Tables[0].NewRow();
                    rowTotal["VchNo"] = "Total";
                    rowTotal["DEBIT"] = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
                    rowTotal["CREDIT"] = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
                    ds.Tables[0].Rows.Add(rowTotal);
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();
                    foreach (GridViewRow oItem in GridExcel.Rows)
                    {
                        if (oItem.Cells[5].Text == "Total")
                        {
                            oItem.Cells[5].Attributes.Add("class", "FinalHead");
                            oItem.Cells[6].Attributes.Add("class", "FinalHead");
                            oItem.Cells[7].Attributes.Add("class", "FinalHead");
                        }
                    }

                    AddHeader();

                    HttpContext.Current.Response.Clear();
                    string attachment = "attachment; filename=DayBook.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();

                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Response.Write(FinalHead);

                    GridExcel.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                    //}
                    #endregion Add Total
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Add Header to GridView
    private void AddHeader()
    {
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
        HeaderCell.ColumnSpan = 7;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        GridExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "Day Book";
        HeaderCell1.ColumnSpan = 7;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        GridExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        if (radBetTwoDates.Checked == true)
        {
            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 7;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);
        }
        else
        {
            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 7;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);
        }

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "";
        HeaderCell3.ColumnSpan = 7;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        GridExcel.Controls[0].Controls.AddAt(3, HeaderGridRow3);
    }

    private void AddHeaderWithout()
    {
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
        HeaderCell.ColumnSpan = 6;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        GridExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "Day Book";
        HeaderCell1.ColumnSpan = 6;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        GridExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        if (radBetTwoDates.Checked == true)
        {
            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 6;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);
        }
        else
        {
            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 6;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);
        }

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "";
        HeaderCell3.ColumnSpan = 6;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        GridExcel.Controls[0].Controls.AddAt(3, HeaderGridRow3);
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SetFinancialYear();
        radBetTwoDates.Checked = true;
        rdbWithNarration.Checked = true;
    }
    protected void chkdept_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdept.Checked)
        {
            objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
            divDept.Visible = true;
            btnExportExcel.Visible = false;
        }
        else
        {
            divDept.Visible = false;
            btnExportExcel.Visible = true;
        }
    }

    private void ShowDayBookDeartment(string reportTitle, string rptFileName)
    {
        try
        {
            
            objCommon = new Common();

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (txtFrmDate.Text.ToString().Trim() == txtUptoDate.Text.ToString().Trim())
            {
                if (rdbWithNarration.Checked == true)
                {
                    url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0,@DEPTNO=" + ddldepartment.SelectedValue;
                }
                else
                {
                    url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1,@DEPTNO=" + ddldepartment.SelectedValue;
                }
            }
            else
            {
                if (rdbWithNarration.Checked == true)
                {
                    if (radBetTwoDates.Checked)
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0,@DEPTNO=" + ddldepartment.SelectedValue;
                    else
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=0,@DEPTNO=" + ddldepartment.SelectedValue;
                }
                else
                {
                    if (radBetTwoDates.Checked)
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1,@DEPTNO=" + ddldepartment.SelectedValue;
                    else
                        url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + ",@P_NV=1,@DEPTNO=" + ddldepartment.SelectedValue;
                }
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
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}



