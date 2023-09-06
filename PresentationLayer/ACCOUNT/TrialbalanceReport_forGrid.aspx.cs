//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Trial Balance Reports                                                     
// CREATION DATE : 22-MAY-2010                                             
// CREATED BY    : Nitin Meshram                                               
// MODIFIED BY   : 
// MODIFIED DESC : Created to show trial balance in grid
//=================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using IITMS.NITPRM;

public partial class ACCOUNT_TrialbalanceReport_forGrid : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    public bool IsShowMsg = true;
   
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

        if (hdnBal.Value != "")
        {

            //lblCurBal.Text = hdnBal.Value;
            //lblmode.Text = hdnMode.Value;
        }

        Session["WithoutCashBank"] = "N";
        // btnGo.Attributes.Add("onClick", "return CheckFields();");
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
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SetDataColumn();
        Session["VchDatatable"] = null;
        SetFinancialYear();
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
            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
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
            //ClMode = lblmode.Text.ToString().Trim();
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
            // LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);


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


            if (rptFileName.ToString().Trim() == "TrialBalanceReport.rpt" || rptFileName.ToString().Trim() == "TrialBalanceReportWithoutClosing.rpt")
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

                if (rdbShortTrailBalanec.Checked)
                    url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@PLOpening=" + op.ToString() + "," + "@PLClosing=" + cl.ToString() + "," + "@PLDebit=" + plDr.ToString() + "," + "@PLCredit=" + plCr.ToString() + ",@P_Consolidate=0";
                else
                    url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@PLOpening=" + op.ToString() + "," + "@PLClosing=" + cl.ToString() + "," + "@PLDebit=" + plDr.ToString() + "," + "@PLCredit=" + plCr.ToString() + ",@P_Consolidate=1";
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
            }
            else
            {

                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

                string LedgerName = string.Empty;

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ACCOUNT," + rptFileName;
                if (rdbShortTrailBalanec.Checked)
                    url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + ",@P_Consolidate=0,@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_TO_DATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_WITHZERO=" + (rdbWithZero.Checked ? 1 : 0);
                else if (rdbDetail.Checked)
                    url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_TO_DATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_WITHZERO=" + (rdbWithZero.Checked ? 1 : 0) + ",@WithZero=" + (rdbWithZero.Checked ? 1 : 0);
                else
                    url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + ",@P_Consolidate=1,@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_TO_DATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy").Trim() + ",@P_WITHZERO=" + (rdbWithZero.Checked ? 1 : 0);


                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);


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
                objUCommon.ShowError(Page, "TrialBalanceReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "TrialBalanceReport.ArrangeFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void GenerateTrialBalanceFormatNew2()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PARTYNAME <> '' AND PARTY_NO =0 AND PRNO = 0", string.Empty);
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
                            oEntity.POSITION = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);
                            InsertChild(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
                            AddSubGroup(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
                        }
                    }
                }
            }
            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void AddSubGroup(string MGRP_NO, string spacegroup, int POSITION)
    {
        DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PARTYNAME <> '' AND PRNO ='" + MGRP_NO + "' AND PARTY_NO =0", string.Empty);
        if (dsLdg != null)
        {
            if (dsLdg.Tables[0].Rows.Count > 0)
            {

                int j = 0;
                spacegroup = spacegroup.ToString() + "  ";
                for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                {
                    TrialBalanceReport oEntity = new TrialBalanceReport();

                    //oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                    oEntity.PartyName = spacegroup.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["FA_NO"].ToString().Trim());
                    oEntity.POSITION = POSITION;
                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                    oTran1.AddTrialBalanceReportFormat(oEntity);

                    InsertChild(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    AddSubGroup(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    //InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));

                }
            }
        }
    }

    private void InsertChild(string MGRP_NO, string spacechild, int POSITION)
    {

        short MGRP_NO1 = Convert.ToInt16(MGRP_NO);
        DataSet dsC = GetChildRecord(MGRP_NO1);
        if (dsC != null)
        {
            if (dsC.Tables[0].Rows.Count > 0)
            {
                int x = 0;
                TrialBalanceReport oEntity1 = new TrialBalanceReport();
                spacechild = spacechild.ToString() + "  ";
                for (x = 0; x < dsC.Tables[0].Rows.Count; x++)
                {
                    // oEntity1.PartyName = "          ".ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                    oEntity1.PartyName = spacechild.ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                    oEntity1.MGRPNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["MGRP_NO"].ToString().Trim());
                    oEntity1.PRNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PRNO"].ToString().Trim());
                    oEntity1.PARTYNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PARTY_NO"].ToString().Trim());
                    oEntity1.OPBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["OP_BALANCE"].ToString().Trim());
                    oEntity1.CLBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["CL_BALANCE"].ToString().Trim());
                    oEntity1.DEBIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["DEBIT"].ToString().Trim());
                    oEntity1.CREDIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["CREDIT"].ToString().Trim());
                    oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                    oEntity1.FANO = Convert.ToInt16(dsC.Tables[0].Rows[x]["FA_NO"].ToString().Trim());
                    oEntity1.POSITION = POSITION;
                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                    oTran2.AddTrialBalanceReportFormat(oEntity1);

                }
            }
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
                objUCommon.ShowError(Page, "TrialBalanceReport.GenerateTrialBalanceFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void btnShowTrialBalance_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtFrmDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(upd, "Enter From Date", this);
                txtFrmDate.Focus();
                return;
            }
            if (txtUptoDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(upd, "Enter Upto Date", this);
                txtUptoDate.Focus();
                return;
            }


            if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                objCommon.DisplayMessage(upd, "Upto Date Should Be In The Financial Year Range. ", this);
                txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            {
                objCommon.DisplayMessage(upd, "From Date Should Be In The Financial Year Range. ", this);
                txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                txtFrmDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
            {
                objCommon.DisplayMessage(upd, "From Date Can Not Be Greater Than Upto Date Date. ", this);
                txtUptoDate.Focus();
                return;
            }
            TrialBalanceReportController od = new TrialBalanceReportController();
            // od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
            //  od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Convert.ToInt32(rdbWithZero.Checked ? 1 : 0));
            //GenerateTrialBalanceFormat("N");
            // GenerateTrialBalanceFormatNew2();

            //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            //od.OrderTrialBalanceReport();
            //UpdateTotalForLedgerNew();
            //UpdateTotalForMainLedger();
            //if (rdbWithZero.Checked)
            //    od.DeleteTrialBalanceZEROAmount("1");
            //else
            //    od.DeleteTrialBalanceZEROAmount("0");
            //DataSet dsLdg = od.GetDistinctMGRPNO();
            //DataSet dst = od.ArrangeTrialBalanceReport();
            //ArrangeFormat(dst, dsLdg);

            //if (rdbDetail.Checked)
            //{
            //    ShowLedgerListReport("TrialBalance", "TrialBalanceReport_DateWise.rpt");
            //}
            //else
            //{
            //    ShowLedgerListReport("TrialBalance", "SummaryTrialBalanceReport.rpt");
            //}


            if (rdbGeneral.Checked == true)
            {
                if (rdbDetail.Checked)
                {
                    ShowLedgerListReport("TrialBalance", "TrialBalanceReport_DateWise.rpt");
                }
                else
                {
                    ShowLedgerListReport("TrialBalance", "SummaryTrialBalanceReport.rpt");
                }
            }
            else
            {
                if (rdbGroup.Checked)
                    ShowGroupReport("Group Trial Balance", "TrialBalanceGroupReport.rpt");
                if (rdbDetailedGroup.Checked)
                    ShowGroupReport("Group Trial Balance", "TrialBalanceDetailedGroupReport.rpt");
                if (rdbHeadDeatils.Checked)
                    ShowGroupReport("Group Trial Balance", "TrialBalanceHeadGroupReport.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.btnShowTrialBalance_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    private void ShowGroupReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_COMPCODE=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@P_MGRP_NO=" + ddlFAGroup.SelectedValue.ToString();


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.ShowGroupReport -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "TrialBalanceReport.UpdateTotalForLedger -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "TrialBalanceReport.UpdateTotalForLedgerNew -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "TrialBalanceReport.UpdateTotalForMainLedger -> " + ex.Message + " " + ex.StackTrace);
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
            //ClMode = lblmode.Text.ToString().Trim();
            string LedgerName = string.Empty;

            // LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);


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

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);


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

    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtAcc.Text.ToString().Trim() != "")
        //{
        //   // btnGo_Click(sender, e);
        //}
        //txtAcc.Focus();

    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtAcc.Text.ToString().Trim() != "")
        //{
        //    //btnGo_Click(sender, e);
        //}
        txtUptoDate.Focus();
    }
    //protected void btnShowList_Click(object sender, EventArgs e)
    //{

    //}
    //protected void rdbDetail_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbDetail.Checked)
    //    {
    //        rdbShortTrailBalanec.Checked = false;
    //        pnlgrd.Visible = false;
    //    }
    //}
    //protected void rdbShortTrailBalanec_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbShortTrailBalanec.Checked)
    //    {
    //        rdbDetail.Checked = false;
    //        pnlgrd.Visible = false;
    //    }
    //}
    protected void btnShow_Click(object sender, EventArgs e)
    {
        trLinkNewVoucher.Visible = true; trPanel.Visible = true;
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(upd, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(upd, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }


        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayMessage(upd, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(upd, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(upd, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
        TrialBalanceReportController od = new TrialBalanceReportController();
        od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Convert.ToInt32(rdbWithZero.Checked ? 1 : 0));


        DataSet dsFinalHead = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME as Party_name,(case  when OP_BALANCE<0 then 'Cr' else 'Dr' end) as OpbalMode,(case  when OP_BALANCE<0 then -OP_BALANCE else OP_BALANCE end) as OP_BALANCE1,(case  when CL_BALANCE<0 then 'Cr' else 'Dr' end) as clBalMode,(case  when CL_BALANCE<0 then -CL_BALANCE else CL_BALANCE end) as CL_BALANCE1", "*", "", "TB_POSITION,ID,PARTY_NO");
        for (int i = 0; i < dsFinalHead.Tables[0].Rows.Count; i++)
        {
            dsFinalHead.Tables[0].Rows[i]["PARTYNAME"] = dsFinalHead.Tables[0].Rows[i]["PARTYNAME"].ToString().Replace(" ", "&nbsp;");
        }
        RptData.DataSource = dsFinalHead;
        RptData.DataBind();

        for (int i = 0; i < RptData.Items.Count; i++)
        {
            ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
            HtmlTableCell trPartyName = RptData.Items[i].FindControl("trPartyName") as HtmlTableCell;
            Label lblParty = RptData.Items[i].FindControl("lblParty") as Label;
            if (btnEdit.CommandArgument == "0" || btnEdit.ToolTip == "PROFIT & LOSS A/c")
            {
                btnEdit.Visible = false;
                btnEdit.Attributes.Add("class", "altitem");

            }
            else
            {
                //lnkLedgerReport.Attributes.Add("onclick", "return ShowledgerReport('" + lnkLedgerReport.ToolTip.Trim() + "','" + lnkLedgerReport.CommandArgument + "');");
                trPartyName.Attributes.Add("onclick", "ShowledgerReport('" + btnEdit.ToolTip.Trim() + "','" + btnEdit.CommandArgument + "','" + txtFrmDate.Text + "','" + txtUptoDate.Text + "');");
               // trPartyName.Attributes.Add("onmouseout", "this.style.backgroundColor='ThreeDFace'");
               // trPartyName.Attributes.Add("onmouseover", "this.style.backgroundColor='#81BEF7'");
               // trPartyName.Attributes.Add("style", "width: 33%;cursor:pointer;");

            }
        }

        DataSet dsTotal = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

        Label lblTotalDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotalDebit") as Label;
        Label lblTotalCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotalCredit") as Label;
        lblTotalCredit.Text = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
        lblTotalDebit.Text = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
        //Label lblPandLOP = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblPandLOP") as Label;
        //Label lblPandLCL = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblPandLCL") as Label;

        //lblPandLOP.Text = objCommon.LookUp("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "(case when OP_BALANCE<0 then (CAST(-OP_BALANCE as nvarchar(50))+' Cr') else (CAST(OP_BALANCE as nvarchar(50))+' Dr') end) as OPBAL", "mgrp_no=0 and PRNO=0");
        //lblPandLCL.Text = objCommon.LookUp("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "(case when CL_BALANCE<0 then (CAST(-CL_BALANCE as nvarchar(50))+' Cr') else (CAST(CL_BALANCE as nvarchar(50))+' Dr') end) as CLBAL", "mgrp_no=0 and PRNO=0");


    }

    protected void rdbSetConfiguration_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSetConfiguration.Checked)
            pnlgrd.Visible = true;
        DataSet dsResult = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", "FA_NO<>0", "MGRP_NAME");
        if (dsResult != null)
        {
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                rptSchDef.DataSource = dsResult.Tables[0];
                rptSchDef.DataBind();
                if (rptSchDef.Rows.Count > 0)
                {
                    for (int i = 0; i < rptSchDef.Rows.Count; i++)
                    {
                        HiddenField hdnSch_IDEx = rptSchDef.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                        hdnSch_IDEx.Value = dsResult.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();
                        TextBox lblprt = rptSchDef.Rows[i].FindControl("lblparty") as TextBox;
                        lblprt.Text = dsResult.Tables[0].Rows[i]["MGRP_NAME"].ToString();

                        string position = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "isnull(TB_POSITION,0)", "MGRP_NO=" + hdnSch_IDEx.Value.ToString());
                        TextBox txtposition = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                        txtposition.Text = position;
                    }
                }
            }
            upd_ModalPopupExtender1.Show();
        }

        //rdbSetConfiguration.Checked = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        if (rptSchDef.Rows.Count > 0)
        {
            //Set Expence Side
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;


                if (Convert.ToString(txtpos.Text).Trim() == "" || Convert.ToString(txtpos.Text).Trim() == "0")
                {
                    objCommon.DisplayUserMessage(upd, "Position Must Not be blank or 0", this.Page);
                    return;
                }
                else
                {
                    // objIEBS.Position = Convert.ToInt32(txtpos.Text);
                }
                string comp_code = Session["comp_code"].ToString().Trim();
                HiddenField hdnAmount = rptSchDef.Rows[i].FindControl("hdnAmount") as HiddenField;
                HiddenField hdnSchID = rptSchDef.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                // objIEBS.Sch_id = Convert.ToInt32(hdnSchID.Value);
                int ret = od.setTrialBalanceReport(txtpos.Text, hdnSchID.Value.ToString(), comp_code);
                if (ret == 1)
                {
                    IsShowMsg = true;
                }
                else
                {
                    IsShowMsg = false;
                }
            }

            if (IsShowMsg != false)
            {
                objCommon.DisplayMessage(upd, "Trial Balance Report Format Set Successfully, To View Report Please Click On Show Button.", this);
            }


        }
        pnlgrd.Visible = false;
    }

    protected void txt_position_Click(object sender, EventArgs e)
    {
        TextBox txtPosition = rptSchDef.FindControl("txt_position") as TextBox;
        for (int i = 0; i < rptSchDef.Rows.Count; i++)
        {
            TextBox a = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
            if (txtPosition.Text == a.Text)
            {
                objCommon.DisplayUserMessage(upd, "Position of two Heads must be different", this.Page);
                return;
            }
        }
    }
    protected void rdbDetail_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDetail.Checked)
        {
            rdbShortTrailBalanec.Checked = false;
            pnlgrd.Visible = false;
        }
    }
    protected void rdbShortTrailBalanec_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbShortTrailBalanec.Checked)
        {
            rdbDetail.Checked = false;
            pnlgrd.Visible = false;
        }
    }
    protected void rdbGroup_CheckedChanged(object sender, EventArgs e)
    {
        trDate.Visible = true;
        trReportType.Visible = false;
        trFAGroup.Visible = true;
        btnShow.Visible = false;
        divLevel.Visible = false;
        //objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO > 0", "MGRP_NAME");
        objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "FA_NO<>0", "MGRP_NAME");

    }

    protected void rdbGeneral_CheckedChanged(object sender, EventArgs e)
    {
        trFAGroup.Visible = false;
        trDate.Visible = true;
        trReportType.Visible = true;
        btnShow.Visible = true;
        divLevel.Visible = false;
        divonlyledger.Visible = true;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
        od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Convert.ToInt32(rdbWithZero.Checked ? 1 : 0));
        //GenerateTrialBalanceFormatNew2();
        //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
        //od.OrderTrialBalanceReport();
        //if (rdbWithZero.Checked)
        //    od.DeleteTrialBalanceZEROAmount("1");
        //else
        //    od.DeleteTrialBalanceZEROAmount("0");

        //Code to export in excel
        //GridView gvTrialBalance = new GridView();
        string ContentType = string.Empty;

        //Added by vijay andoju 20-08-2020 for trail balance only ledgers
        DataSet ds = new DataSet();
        if (rdbOnlyLedger.Checked == true)
        {
            ds = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME AS PARTY_NAME,(CASE WHEN OP_BALANCE>=0 THEN ' Dr' ELSE ' Cr' END) AS CrDr,OP_BALANCE AS OPENING_BALANCE,DEBIT,CREDIT", "CL_BALANCE AS CLOSING_BALANCE ,CASE WHEN CL_BALANCE>=0 THEN ' Dr' ELSE ' Cr' END AS CCrDr,IS_PARTY", " IS_PARTY<>0", "TB_POSITION,ID,PARTY_NO");
        }
        else if (rdbGroupwise.Checked == true)
        {
            ds = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME AS PARTY_NAME,(CASE WHEN OP_BALANCE>=0 THEN ' Dr' ELSE ' Cr' END) AS CrDr,OP_BALANCE AS OPENING_BALANCE,DEBIT,CREDIT", "CL_BALANCE AS CLOSING_BALANCE ,CASE WHEN CL_BALANCE>=0 THEN ' Dr' ELSE ' Cr' END AS CCrDr,IS_PARTY", "PARTY_NO=0 and level="+ddllevel.SelectedValue.ToString(), "TB_POSITION,ID,PARTY_NO");
        }
        else
        {
            ds = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME AS PARTY_NAME,CASE WHEN OP_BALANCE>=0 THEN ' Dr' ELSE ' Cr' END as CrDr,  OP_BALANCE AS OPENING_BALANCE", "DEBIT,CREDIT,CASE WHEN CL_BALANCE>=0 then ' Dr' ELSE ' Cr' END as CCrDr, CL_BALANCE AS CLOSING_BALANCE,IS_PARTY ", "", "TB_POSITION,ID,PARTY_NO");
        }
        DataSet dsTotal = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "SUM(OP_BALANCE) TOTALOPENBAL ,sum(DEBIT) as totDebit", "sum(CREDIT) totCredit,SUM(CL_BALANCE) TOTCLOSINGBAL", "party_no<>'0'", "");
        
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (rdbOnlyLedger.Checked == true || rdbGroupwise.Checked==true)
            {
                ds.Tables[0].Rows[i]["PARTY_NAME"] = ds.Tables[0].Rows[i]["PARTY_NAME"].ToString();//For Removing ________ before party name in that new only ledger report
            }
            else
            {
                ds.Tables[0].Rows[i]["PARTY_NAME"] = ds.Tables[0].Rows[i]["PARTY_NAME"].ToString().Replace(" ", "_");
            }

        }
        DataRow rowTotal = ds.Tables[0].NewRow();
        rowTotal["PARTY_NAME"] = "Grand Total";
        rowTotal["OPENING_BALANCE"] = dsTotal.Tables[0].Rows[0]["TOTALOPENBAL"].ToString();
        rowTotal["CrDr"] = Convert.ToDecimal(dsTotal.Tables[0].Rows[0]["TOTALOPENBAL"].ToString()) > 0 ? "Dr" : "Cr";
        rowTotal["DEBIT"] = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
        rowTotal["CREDIT"] = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
        rowTotal["CLOSING_BALANCE"] = dsTotal.Tables[0].Rows[0]["TOTCLOSINGBAL"].ToString();
        rowTotal["CCrDr"] = Convert.ToDecimal(dsTotal.Tables[0].Rows[0]["TOTCLOSINGBAL"].ToString()) > 0 ? "Dr" : "Cr";
        ds.Tables[0].Rows.Add(rowTotal);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.RemoveAt(3);

            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=TrialBalance.xls";
            if (rdbOnlyLedger.Checked == true || rdbGroupwise.Checked==true)
            {
                gv.DataSource = ds;
                gv.DataBind();
                AddHeaderOnyledger();
                gv.FooterStyle.Font.Bold = true;
                attachment = "attachment; filename=TrialBalance.xls";
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", attachment);
                Response.AppendHeader("Refresh", ".5; TrialbalanceReport_forGrid.aspx");
                Response.Charset = "";
                Response.ContentType = "application/" + ContentType;
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
                gv.RenderControl(htw1);
                Response.Output.Write(sw1.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                

             
                rdbOnlyLedger.Checked = false;
               
                return;
            }
            else
            {
                gvTrialBalance.DataSource = ds;
                gvTrialBalance.DataBind();
            }
            foreach (GridViewRow oItem in gvTrialBalance.Rows)
            {
                HiddenField hdnIsParty = oItem.FindControl("hdnIsParty") as HiddenField;
                if (hdnIsParty.Value == "0")
                {
                    oItem.Cells[0].Attributes.Add("class", "FinalHead");
                }
                if (oItem.Cells[0].Text == "Grand Total")
                {
                    oItem.Cells[0].Attributes.Add("class", "FinalHead");
                    oItem.Cells[2].Attributes.Add("class", "FinalHead");
                    oItem.Cells[3].Attributes.Add("class", "FinalHead");
                }

                // oItem.Cells[0].Text = oItem.Cells[0].Text.Replace("_", " ");

            }

            AddHeader();

            attachment = "attachment; filename=TrialBalance.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);

            gvTrialBalance.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
     
    }

    //Add Header to GridView
    private void AddHeader()
    {
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvTrialBalance.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "TRIAL BALANCE REPORT";
            HeaderCell1.ColumnSpan =8;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            gvTrialBalance.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            gvTrialBalance.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            HeaderCell3 = new TableCell();
            HeaderCell3.Text = "";
            HeaderCell3.ColumnSpan = 8;
            HeaderCell3.BackColor = System.Drawing.Color.White;
            HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow3.Cells.Add(HeaderCell3);
            gvTrialBalance.Controls[0].Controls.AddAt(3, HeaderGridRow3);
        
    }

    //Added by vijay andoju on 20-08-2020 for new excel 
    private void AddHeaderOnyledger()
    {


        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
        HeaderCell.ColumnSpan = 8;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gv.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "TRIAL BALANCE REPORT";
        HeaderCell1.ColumnSpan = 8;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gv.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
        HeaderCell2.ColumnSpan = 8;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gv.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "";
        HeaderCell3.ColumnSpan = 8;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gv.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        gv.FooterStyle.Font.Bold = true;
        gv.FooterStyle.Font.Size = 12;
    }

    protected void rdbDetailedGroup_CheckedChanged(object sender, EventArgs e)
    {
        trDate.Visible = true;
        trReportType.Visible = false;
        trFAGroup.Visible = true;
        divLevel.Visible = false;
        btnShow.Visible = false;
        //objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO > 0", "MGRP_NAME");
        objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "FA_NO<>0", "MGRP_NAME");

    }
    protected void rdbHeadDeatils_CheckedChanged(object sender, EventArgs e)
    {
        trDate.Visible = true;
        trReportType.Visible = false;
        trFAGroup.Visible = true;
        divLevel.Visible =false;
        btnShow.Visible = false;
        //objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO > 0", "MGRP_NAME");
        objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO in (select distinct(Mgrp_no) from acc_" + Session["comp_code"].ToString() + "_party where mgrp_no<>0) ", "FA_NO");
    }
    protected void rdbGroupwise_CheckedChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddllevel, "TEMP_TRIAL_BALANCE_REPORT_FORMAT  GROUP BY level", "level LEVELNO", "'Level '+cast(level as varchar(4))LEVEL", "", "");
        trFAGroup.Visible = false;    
        divLevel.Visible = true;
        trReportType.Visible = false;
        trDate.Visible = true;
        divonlyledger.Visible = false;
        rdbOnlyLedger.Checked = false;
    }
   
    protected void lnkNewVoucher_Click1(object sender, EventArgs e)
    {
        Response.Redirect(("~/ACCOUNT\\AccountingVouchers.aspx"));
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ddlFAGroup.SelectedIndex = 0;
        ddllevel.SelectedIndex = 0;

        trLinkNewVoucher.Visible = false; 
        trPanel.Visible = false;

        rdbGeneral.Checked = true;
        rdbGroup.Checked = false;
        rdbDetailedGroup.Checked = false;
        rdbHeadDeatils.Checked = false;
        rdbGroupwise.Checked = false;

        rdbWithZero.Checked = true;
        rdbWithoutZero.Checked = false;

        rdbDetail.Checked = true;
        rdbShortTrailBalanec.Checked = false;
        rdbConsolidateTrialBalance.Checked = false;
        rdbSetConfiguration.Checked = false;

        rdbOnlyLedger.Checked = false;

        trFAGroup.Visible = false;
        trDate.Visible = true;
        trReportType.Visible = true;
        btnShow.Visible = true;
        divLevel.Visible = false;
        divonlyledger.Visible = true;
    }
}
