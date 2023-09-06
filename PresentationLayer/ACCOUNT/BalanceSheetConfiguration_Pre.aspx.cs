//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : BALANCE SHEET CONFIGURATION
// CREATION DATE : 08-MARCH-2010                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;
using IITMS.NITPRM;

public partial class BalanceSheetConfiguration_Pre : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    static string fromdate = string.Empty;
    static string todate = string.Empty;
    static string isNetSurplus = string.Empty;
    public bool IsShowMsg = true;
    string space1 = "     ".ToString();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((fromdate == "" || fromdate == string.Empty) && (todate == "" || todate == string.Empty))
        {
            //fromdate = Request.QueryString["obj"].Split(',')[0].ToString().Trim();
           // todate = Request.QueryString["obj"].Split(',')[1].ToString().Trim();
            SetFinancialYear();
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
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else { Session["comp_set"] = ""; }

                //Page Authorization
                //CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                TrialBalanceReportController od = new TrialBalanceReportController();
                od.DeleteBalanceSheetReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
                od.GenerateBalanceSheet_Asstes(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                //GenerateTrialBalanceFormatNew_Assets();
                od.GenerateBalanceSheet_Liability(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                //GenerateTrialBalanceFormatNew_Liability();
                //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
                //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));

                //od.DeleteBalanceSheetZEROAmount();
                DataSet dsIncome = new DataSet();
                dsIncome = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "SUM(CL_balance)", "", " PARTY_NO=0 AND fa_no<>0 and (CL_BALANCE<>0)", "");
                DataSet dsExpense = new DataSet();
                dsExpense = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "SUM(CL_balance)", "", " PARTY_NO=0 AND fa_no<>0 and (CL_BALANCE<>0)", "");
                decimal _IncomeSum = 0, _NetIncome_Expense = 0;
                decimal _ExpenseSum = 0;
                if (dsIncome != null)
                {
                    if (dsIncome.Tables[0] != null)
                    {
                        if (dsIncome.Tables[0].Rows.Count > 0)
                        {
                            if (dsIncome.Tables[0].Rows[0][0].ToString() != "")
                            {
                                _IncomeSum = Convert.ToDecimal(dsIncome.Tables[0].Rows[0][0].ToString());
                            }
                        }
                    }
                }
                if (dsExpense != null)
                {
                    if (dsExpense.Tables[0] != null)
                    {
                        if (dsExpense.Tables[0].Rows.Count > 0)
                        {
                            if (dsExpense.Tables[0].Rows[0][0].ToString() != "")
                            {
                                _ExpenseSum = Convert.ToDecimal(dsExpense.Tables[0].Rows[0][0].ToString());
                            }
                        }
                    }
                }
                _NetIncome_Expense = _ExpenseSum - _IncomeSum;
                ViewState["NetIncome_Expense"] = _NetIncome_Expense;

                BindScheduleDefination();
                CreateBalanceSheetFormat();
                GetDeficitSurplus();
            }

        }
        //divMsg.InnerHtml = string.Empty;
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
            fromdate = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            todate = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }

    private void GetDeficitSurplus()
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        od.DeletePLReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
        od.GeneratePL_Income(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
        //GenerateTrialBalanceFormatNew_Income();
        od.GeneratePL_Expenses(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
        DataSet dsExpense = new DataSet();
        dsExpense = objCommon.FillDropDown("TEMP_SHEDULE_EXPENSES1", "SUM(CL_balance)", "", " PARTY_NO=0 AND fa_no<>0 and (CL_BALANCE<>0)", "");
        DataSet dsIncome = new DataSet();
        dsIncome = objCommon.FillDropDown("TEMP_SHEDULE_INCOME1", "SUM(CL_balance)", "", " PARTY_NO=0 AND fa_no<>0 and (CL_BALANCE<>0)", "");
        decimal _IncomeSum = 0, _NetIncome_Expense = 0;
        decimal _ExpenseSum = 0;
        if (dsIncome != null)
        {
            if (dsIncome.Tables[0] != null)
            {
                if (dsIncome.Tables[0].Rows.Count > 0)
                {
                    if (dsIncome.Tables[0].Rows[0][0].ToString() != "")
                    {
                        _IncomeSum = -Convert.ToDecimal(dsIncome.Tables[0].Rows[0][0].ToString());
                    }
                }
            }
        }
        if (dsExpense != null)
        {
            if (dsExpense.Tables[0] != null)
            {
                if (dsExpense.Tables[0].Rows.Count > 0)
                {
                    if (dsExpense.Tables[0].Rows[0][0].ToString() != "")
                    {
                        _ExpenseSum = Convert.ToDecimal(dsExpense.Tables[0].Rows[0][0].ToString());
                    }
                }
            }
        }
        _NetIncome_Expense = _IncomeSum - _ExpenseSum;
        //ViewState["NetIncome_Expense"] = _NetIncome_Expense;
    }

    protected void GenerateTrialBalanceFormatNew_Assets()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "*", "", "PARTYNAME <> '' AND PARTY_NO =0 AND PRNO = 0", string.Empty);
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
                            oTran.AddBalanceSheetAssetsFormat(oEntity);
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
    protected void GenerateTrialBalanceFormatNew_Liability()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "*", "", "PARTYNAME <> '' AND PARTY_NO =0 AND PRNO = 0", string.Empty);
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
                            oTran.AddBalanceSheetLiabilityFormat(oEntity);
                            InsertChild1(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
                            AddSubGroup1(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
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
        DataSet dsLdg = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "*", "", "PARTYNAME <> '' AND PRNO ='" + MGRP_NO + "' AND PARTY_NO =0", string.Empty);
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
                    oTran1.AddBalanceSheetAssetsFormat(oEntity);

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
                    oTran2.AddBalanceSheetAssetsFormat(oEntity1);

                }
            }
        }
    }

    private void AddSubGroup1(string MGRP_NO, string spacegroup, int POSITION)
    {
        DataSet dsLdg = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "*", "", "PARTYNAME <> '' AND PRNO ='" + MGRP_NO + "' AND PARTY_NO =0", string.Empty);
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
                    oTran1.AddBalanceSheetLiabilityFormat(oEntity);

                    InsertChild1(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    AddSubGroup1(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    //InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));

                }
            }
        }
    }

    private void InsertChild1(string MGRP_NO, string spacechild, int POSITION)
    {

        short MGRP_NO1 = Convert.ToInt16(MGRP_NO);
        DataSet dsC = GetChildRecord1(MGRP_NO1);
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
                    oTran2.AddBalanceSheetLiabilityFormat(oEntity1);

                }
            }
        }
    }
    protected DataSet GetChildRecord(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected DataSet GetChildRecord1(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected DataSet GetParentChildRecord(Int16 mgrp, Int16 prno)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "*", "", "MGRP_NO=" + mgrp.ToString().Trim() + " and  prno=" + prno.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }

    protected DataSet GetParentChildRecord1(Int16 mgrp, Int16 prno)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "*", "", "MGRP_NO=" + mgrp.ToString().Trim() + " and  prno=" + prno.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }

    protected void CreateBalanceSheetFormat()
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        obj.CallBalanceSheetFormatProc(isNetSurplus, "Y");

    }
    protected void BindScheduleDefination()
    {
        string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();
        rptSchDef.DataSource = null;
        rptSchDef.DataBind();
        rptSchDef1.DataSource = null;
        rptSchDef1.DataBind();
        BindLiabilitySide();
        BindAssetSide();
    }

    protected DataSet SetProfitLossNetDeficit(DataSet dsOp)
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));

        if (dsOp != null)
        {

            if (dsOp.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDouble(dsPLOC.Tables[0].Rows[0][1]) < 0)
                {
                    isNetSurplus = "N";

                    DataRow row;
                    row = dsOp.Tables[0].NewRow();
                    row["partyname"] = "NET DEFICIT";
                    row["mgrp_no"] = "17654";
                    row["prno"] = "0";
                    row["party_no"] = "0";

                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                    {
                        row["op_balance"] = "0";
                    }
                    else
                    {
                        row["op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                        // row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2;
                        row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]));
                    }
                    row["is_party"] = "0";


                    DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                    if (dsPLDC != null)
                    {
                        if (dsPLDC.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                            {
                                row["Debit"] = 0;
                            }
                            else
                            {
                                row["Debit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                            }

                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                            {
                                row["Credit"] = 0;
                            }
                            else
                            {
                                row["Credit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                            }
                            row["fano"] = "2";
                            row["CounterId"] = "100000";
                            row["lindex"] = "0";
                            //row["position"] = "";
                            //row["Schedule"] = "";
                            //row["CCode"] = Session["comp_code"].ToString().Trim();
                        }
                    }

                    dsOp.Tables[0].Rows.Add(row);
                }

            }
        }

        return dsOp;

    }
    protected DataSet SetProfitLossNetSurplus(DataSet dsOp)
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));

        if (dsOp != null)
        {

            if (dsOp.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToDouble(dsPLOC.Tables[0].Rows[0][1]) > 0)
                {
                    isNetSurplus = "Y";

                    DataRow row;
                    row = dsOp.Tables[0].NewRow();
                    row["partyname"] = "NET SURPLUS";
                    row["mgrp_no"] = "17655";
                    row["prno"] = "0";
                    row["party_no"] = "0";

                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                    {
                        row["op_balance"] = "0";
                    }
                    else
                    {
                        row["op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                        //row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2;
                        row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]));
                    }
                    row["is_party"] = "0";


                    DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                    if (dsPLDC != null)
                    {
                        if (dsPLDC.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                            {
                                row["Debit"] = 0;
                            }
                            else
                            {

                                row["Debit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                            }

                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                            {
                                row["Credit"] = 0;
                            }
                            else
                            {
                                row["Credit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                            }
                            row["fano"] = "1";
                            row["CounterId"] = "100000";
                            row["lindex"] = "0";
                            //row["position"] = "";
                            //row["Schedule"] = "";
                            //row["CCode"] = Session["comp_code"].ToString().Trim();
                        }
                    }

                    dsOp.Tables[0].Rows.Add(row);

                }
            }
        }

        return dsOp;

    }

    protected void BindLiabilitySide()
    {

        //for liability side============
        DataSet dsOp;
        string present = string.Empty;

        dsOp = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY1", "MGRP_NO,prno,party_NO,partyname,TB_POSITION,HEADNO,cl_balance", "", " PARTY_NO=0 AND fa_no=1 and (CL_BALANCE<>0 or CREDIT<>0 or DEBIT<>0)", "counterid");
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                rptSchDef.DataSource = dsOp.Tables[0];
                rptSchDef.DataBind();
                if (rptSchDef.Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < rptSchDef.Rows.Count; i++)
                    {
                        HiddenField hidmgrpno = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                        hidmgrpno.Value = dsOp.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();

                        HiddenField hidprno = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
                        hidprno.Value = dsOp.Tables[0].Rows[i]["prno"].ToString().Trim();

                        HiddenField hidpartyno = rptSchDef.Rows[i].FindControl("hid_partyno") as HiddenField;
                        hidpartyno.Value = dsOp.Tables[0].Rows[i]["party_NO"].ToString().Trim();

                        Label lblprt = rptSchDef.Rows[i].FindControl("lblparty") as Label;
                        lblprt.Text = dsOp.Tables[0].Rows[i]["partyname"].ToString();

                        //if (present == "Y")
                        //{
                        TextBox txtp = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                        txtp.Text = dsOp.Tables[0].Rows[i]["TB_POSITION"].ToString();

                        //TextBox txtS = rptSchDef.Rows[i].FindControl("txt_sch") as TextBox;
                        //txtS.Text = dsOp.Tables[0].Rows[i]["PARTYNAME"].ToString();

                        HiddenField headno = rptSchDef.Rows[i].FindControl("hid_headno") as HiddenField;
                        headno.Value = dsOp.Tables[0].Rows[i]["HEADNO"].ToString().Trim();

                        lblprt.Attributes.Add("style", "cursor:pointer;");
                        lblprt.Attributes.Add("onclick", "ShowledgerReport('" + hidmgrpno.Value + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy") + "');");

                        //}
                    }
                }
            }
        }
    }
    protected void BindAssetSide()
    {

        //for asset side============

        DataSet dsOp;
        string present = string.Empty;

        dsOp = objCommon.FillDropDown("TEMP_SHEDULE_ASSET1", "MGRP_NO,prno,party_NO,partyname,TB_POSITION,HEADNO,cl_balance", "", " PARTY_NO=0 AND fa_no=2 and (CL_BALANCE<>0 or CREDIT<>0 or DEBIT<>0)", "counterid");
        //if (dsOp != null)
        //{
        //    if (dsOp.Tables[0].Rows.Count > 0)
        //    {
        //        present = "Y";
        //    }
        //    else
        //    {
        //        dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=2", "counterid");
        //        dsOp = SetProfitLossNetDeficit(dsOp);
        //    }
        //}
        //else
        //{
        //    dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=2", "counterid");
        //    dsOp = SetProfitLossNetDeficit(dsOp);
        //}
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                rptSchDef1.DataSource = dsOp.Tables[0];
                rptSchDef1.DataBind();
                if (rptSchDef1.Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < rptSchDef1.Rows.Count; i++)
                    {
                        HiddenField hidmgrpno = rptSchDef1.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                        hidmgrpno.Value = dsOp.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();

                        HiddenField hidprno = rptSchDef1.Rows[i].FindControl("hid_prno") as HiddenField;
                        hidprno.Value = dsOp.Tables[0].Rows[i]["prno"].ToString().Trim();

                        HiddenField hidpartyno = rptSchDef1.Rows[i].FindControl("hid_partyno") as HiddenField;
                        hidpartyno.Value = dsOp.Tables[0].Rows[i]["party_NO"].ToString().Trim();

                        Label lblprt = rptSchDef1.Rows[i].FindControl("lblparty") as Label;
                        lblprt.Text = dsOp.Tables[0].Rows[i]["partyname"].ToString();


                        //if (present == "Y")
                        //{
                        TextBox txtp = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;
                        txtp.Text = dsOp.Tables[0].Rows[i]["TB_POSITION"].ToString();

                        //TextBox txtS = rptSchDef1.Rows[i].FindControl("txt_sch") as TextBox;
                        //txtS.Text = dsOp.Tables[0].Rows[i]["Schedule"].ToString();

                        HiddenField headno = rptSchDef1.Rows[i].FindControl("hid_headno") as HiddenField;
                        headno.Value = dsOp.Tables[0].Rows[i]["HEADNO"].ToString().Trim();

                        lblprt.Attributes.Add("style", "cursor:pointer;");
                        lblprt.Attributes.Add("onclick", "ShowledgerReport('" + hidmgrpno.Value + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy") + "');");

                        //}

                    }
                }
            }
        }
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}
    protected void rptSchDef_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptSchDef.PageIndex = e.NewPageIndex;
        rptSchDef.DataBind();
    }
    protected void rptSchDef1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptSchDef1.PageIndex = e.NewPageIndex;
        rptSchDef1.DataBind();
    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SchedularDefining.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SchedularDefining.aspx");
        }
    }
    #endregion

    //protected void rptSchDef_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    int i = 0;
    //    for (i = 0; i < rptSchDef.Rows.Count; i++)
    //    {
    //      TextBox txtpos=  rptSchDef.Rows[i].FindControl("txt_position") as TextBox;

    //      HiddenField hidmgrp = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
    //      HiddenField hidpr = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
    //      txtpos.Attributes.Add("onblur", "LoadPosition('" + hidmgrp.Value.ToString() + "','" + hidpr.Value.ToString() + "');");

    //    }

    // }
    protected void SetPosition(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("txt_position") as TextBox;
                TextBox textValue3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPosition(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        if (k == rptSchDef.Rows.Count)
        {
            if (setpos != "Y")
            {
                TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue4.Enabled = false;

            }

        }

    }
    protected void SetPositionAsset(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef1.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("txt_position") as TextBox;
                TextBox textValue3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPositionAsset(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        if (k == rptSchDef1.Rows.Count)
        {
            if (setpos != "Y")
            {
                TextBox textValue4 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue4.Enabled = false;

            }

        }

    }
    protected void SetSchedule(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("txt_sch") as TextBox;
                TextBox textValue3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPosition(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        //if (k == rptSchDef.Rows.Count)
        //{
        //    if (setpos != "Y")
        //    {
        //        TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
        //        textValue4.Enabled = false;

        //    }

        //}

    }
    protected void SetScheduleAsset(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef1.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("txt_sch") as TextBox;
                TextBox textValue3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetScheduleAsset(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        //if (k == rptSchDef.Rows.Count)
        //{
        //    if (setpos != "Y")
        //    {
        //        TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
        //        textValue4.Enabled = false;
        //    }
        //}

    }
    protected void txt_position_TextChanged(object sender, EventArgs e)
    {
        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
            TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
            HiddenField headno = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;

            //string idstr = ids[4].ToString();
            //HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr)].FindControl("hid_mgrpno") as HiddenField;
            //TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr)].FindControl("txt_position") as TextBox;
            //HiddenField headno = rptSchDef.Rows[Convert.ToInt16(idstr)].FindControl("hid_headno") as HiddenField;

            headno.Value = "10";
            int l = 0;
            for (l = 0; l < rptSchDef.Rows.Count; l++)
            {

                //if (Convert.ToInt16(idstr) != l)
                if (Convert.ToInt16(idstr) - 2 != l)
                {

                    TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(l)].FindControl("txt_position") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Position No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }



            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                    TextBox textValue1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetPosition(hid3.Value.ToString().Trim(), i);
                }
            }
        }

    }
    private Boolean CheckPosition(string EnteredPosition, string PresentPosition)
    {
        if (EnteredPosition == PresentPosition)
        {
            return false;

        }
        else
        {
            return true;
        }

    }
    protected void txt_sch_TextChanged(object sender, EventArgs e)
    {

        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;

            TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_Sch") as TextBox;
            int l = 0;
            for (l = 0; l < rptSchDef.Rows.Count; l++)
            {

                if (Convert.ToInt16(idstr) - 2 != l)
                {

                    TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(l)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }

            int x = 0;
            for (x = 0; x < rptSchDef1.Rows.Count; x++)
            {

                if (Convert.ToInt16(idstr) - 2 != x)
                {

                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(x)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                    TextBox textValue1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_sch") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetSchedule(hid3.Value.ToString().Trim(), i);
                }
            }
        }
    }

    protected void txt_position_TextChanged1(object sender, EventArgs e)
    {
        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
            TextBox textValueChk = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
            HiddenField headno = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;

            //string idstr = ids[4].ToString();
            //HiddenField hid1 = rptSchDef1.Rows[Convert.ToInt16(idstr)].FindControl("hid_mgrpno") as HiddenField;
            //TextBox textValueChk = rptSchDef1.Rows[Convert.ToInt16(idstr)].FindControl("txt_position") as TextBox;
            //HiddenField headno = rptSchDef1.Rows[Convert.ToInt16(idstr)].FindControl("hid_headno") as HiddenField;
            
            headno.Value = "10";
            int l = 0;
            for (l = 0; l < rptSchDef1.Rows.Count; l++)
            {
                if (Convert.ToInt16(idstr) - 2 != l)
                //if (Convert.ToInt16(idstr) != l)
                {
                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(l)].FindControl("txt_position") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Position No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;
                    }
                }
            }

            int i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                    TextBox textValue1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
                    //TextBox textValue1 = rptSchDef1.Rows[Convert.ToInt16(idstr)].FindControl("txt_position") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetPositionAsset(hid3.Value.ToString().Trim(), i);
                }
            }
        }

    }
    protected void txt_sch_TextChanged1(object sender, EventArgs e)
    {
        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;

            TextBox textValueChk = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_Sch") as TextBox;
            int l = 0;
            for (l = 0; l < rptSchDef1.Rows.Count; l++)
            {

                if (Convert.ToInt16(idstr) - 2 != l)
                {
                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(l)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }

            int x = 0;
            for (x = 0; x < rptSchDef.Rows.Count; x++)
            {
                if (Convert.ToInt16(idstr) - 2 != x)
                {
                    TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(x)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;
                    }
                }

            }

            int i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                    TextBox textValue1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_sch") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetScheduleAsset(hid3.Value.ToString().Trim(), i);
                }
            }
        }

    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        if (rptSchDef.Rows.Count > 0 || rptSchDef1.Rows.Count > 0)
        {
            //Set Liability Side
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                TextBox txtsch = rptSchDef.Rows[i].FindControl("txt_sch") as TextBox;
                HiddenField hdnmgrp = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                HiddenField hdnprno = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
                HiddenField hdHeadNo = rptSchDef.Rows[i].FindControl("hid_headno") as HiddenField;
                if (Convert.ToString(txtpos.Text).Trim() == "")
                {
                    txtpos.Text = "0";
                }
                if (Convert.ToString(hdHeadNo.Value).Trim() == "")
                {
                    hdHeadNo.Value = "0";
                }
                //if (Convert.ToString(txtsch.Text).Trim() == "")
                //{
                //    txtsch.Text = "0";
                //}

                //obj1.SetBalanceSheetFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), Convert.ToInt16(hdHeadNo.Value));
                TrialBalanceReportController obj1 = new TrialBalanceReportController();
                //if (hdnmgrp.Value.ToString().Trim() == "17655")
                //{
                //    //pl account
                //    DataSet dstemp = new DataSet();
                //    TrialBalanceReportController obj = new TrialBalanceReportController();
                //    DataSet dsplac = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                //    if (dsplac != null)
                //    {
                //        if (dsplac.Tables[0].Rows.Count > 0)
                //        {
                //            double op = 0;
                //            double cl = 0;
                //            double cr = 0;
                //            double dr = 0;
                //            if (Convert.ToString(dsplac.Tables[0].Rows[0]["Opening"]).Trim() == "")
                //            {
                //                op = 0;
                //            }
                //            else
                //            {
                //                op = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Opening"]));
                //            }
                //            if (Convert.ToString(dsplac.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                //            {
                //                cl = 0;
                //            }
                //            else
                //            {
                //                //cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Cl"])) / 2;
                //                cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["ClosingTransaction"]));
                //            }

                //            DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                //            if (dsPLDC != null)
                //            {
                //                if (dsPLDC.Tables[0].Rows.Count > 0)
                //                {
                //                    if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                //                    {
                //                        dr = 0;
                //                    }
                //                    else
                //                    {
                //                        dr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                //                    }
                //                    if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                //                    {
                //                        cr = 0;
                //                    }
                //                    else
                //                    {
                //                        cr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                //                    }
                //                }
                //            }
                //            obj1.SetBalanceSheetFormatForPLAccount(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), "Y", Convert.ToDouble(op), Convert.ToDouble(cl), Convert.ToDouble(dr), Convert.ToDouble(cr));
                //        }

                //    }
                //}
                //else
                //{
                obj1.SetBalanceSheetLiabilityFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text));
                //}
            }

            //Set Asset Side
            i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;
                TextBox txtsch = rptSchDef1.Rows[i].FindControl("txt_sch") as TextBox;
                HiddenField hdnmgrp = rptSchDef1.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                HiddenField hdnprno = rptSchDef1.Rows[i].FindControl("hid_prno") as HiddenField;
                HiddenField hdHeadNo = rptSchDef1.Rows[i].FindControl("hid_headno") as HiddenField;
                if (Convert.ToString(txtpos.Text).Trim() == "")
                {
                    txtpos.Text = "0";

                }
                if (Convert.ToString(hdHeadNo.Value).Trim() == "")
                {
                    hdHeadNo.Value = "0";

                }
                //if (Convert.ToString(txtsch.Text).Trim() == "")
                //{
                //    txtsch.Text = "0";

                //}
                TrialBalanceReportController obj1 = new TrialBalanceReportController();
                //if (hdnmgrp.Value.ToString().Trim() == "17654")
                //{
                //    //pl account
                //    DataSet dstemp=new DataSet();
                //    TrialBalanceReportController obj = new TrialBalanceReportController();
                //    DataSet dsplac = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                //    if (dsplac != null)
                //    {
                //        if (dsplac.Tables[0].Rows.Count > 0)
                //        {
                //            double op = 0;
                //            double cl = 0;
                //            double cr = 0;
                //            double dr = 0;
                //            if (Convert.ToString(dsplac.Tables[0].Rows[0]["Opening"]).Trim() == "")
                //            {
                //                op = 0;
                //            }
                //            else
                //            {
                //                op = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Opening"]));
                //            }
                //            if (Convert.ToString(dsplac.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                //            {
                //                cl = 0;
                //            }
                //            else
                //            {
                //                //cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Cl"]) )/2;
                //                cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["ClosingTransaction"]));
                //            }

                //             DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                //             if (dsPLDC != null)
                //             {
                //                 if (dsPLDC.Tables[0].Rows.Count > 0)
                //                 {
                //                     if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                //                     {
                //                        dr= 0;
                //                     }
                //                     else
                //                     {

                //                        dr= (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]) );
                //                     }

                //                     if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                //                     {
                //                         cr = 0;
                //                     }
                //                     else
                //                     {
                //                         cr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]) );
                //                     }
                //                 }
                //             }

                //            obj1.SetBalanceSheetFormatForPLAccount(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), "Y", Convert.ToDouble(op), Convert.ToDouble(cl), Convert.ToDouble(dr), Convert.ToDouble(cr));

                //        }

                //    }

                //}
                //else
                //{
                obj1.SetBalanceSheetAssetsFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text));
                //}

            }
            TrialBalanceReportController obj2 = new TrialBalanceReportController();
            //obj2.ArrangeBalanceSheet(Session["comp_code"].ToString().Trim());
            //ArrangeExactBalanceSheet();
            obj2.DeleteBalanceSheetReportFormat(Session["comp_code"].ToString());
            GenerateTrialBalanceFormatNew_Assets();
            GenerateTrialBalanceFormatNew_Liability();

            obj2.DeleteBalanceSheetZEROAmount();
            obj2.OrderBalanceSheetReport();
            if (IsShowMsg != false)
                objCommon.DisplayMessage(upd, "Balance Sheet Format Set Successfully, To View Report Please Click On Show Button.", this);


        }


    }

    protected void ArrangeExactBalanceSheet()
    {
        //for Liability side
        DataSet dsOp = objCommon.FillDropDown("TEMP_BALANACESHEET_LIABILITY", "*", "", string.Empty, "counterid");
        DeleteExtraShedules(dsOp);
        //for Asset side
        DataSet dsOp1 = objCommon.FillDropDown("TEMP_BALANACESHEET_ASSET", "*", "", string.Empty, "counterid");
        DeleteExtraShedules(dsOp1);
    }

    protected void DeleteExtraShedules(DataSet dsext)
    {
        if (dsext != null)
        {
            if (dsext.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i < dsext.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim() != "0")
                    {
                        int j = 0;
                        int x = 0;
                        if (Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim() != "")
                        {
                            x = Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim());
                        }

                        for (j = 0; j < dsext.Tables[0].Rows.Count; j++)
                        {
                            int y = 0;
                            if (Convert.ToString(dsext.Tables[0].Rows[j]["Schedule"]).Trim() != "")
                            {
                                y = Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[j]["Schedule"]).Trim());
                            }

                            if (Convert.ToInt16(x) == Convert.ToInt16(y))
                            {
                                int k = 0;
                                for (k = 0; k < dsext.Tables[0].Rows.Count; k++)
                                {
                                    int z = 0;
                                    if (Convert.ToString(dsext.Tables[0].Rows[k]["Schedule"]).Trim() != "")
                                    {
                                        z = Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[k]["Schedule"]).Trim());
                                    }

                                    if (Convert.ToInt16(x) == Convert.ToInt16(z))
                                    {
                                        if (Convert.ToInt16(dsext.Tables[0].Rows[i]["MGRP_NO"]) == Convert.ToInt16(dsext.Tables[0].Rows[k]["PRNO"]))
                                        {
                                            //delete query
                                            TrialBalanceReportController o = new TrialBalanceReportController();
                                            o.DeleteSchedules(Convert.ToInt16(dsext.Tables[0].Rows[i]["MGRP_NO"]), Convert.ToInt16(dsext.Tables[0].Rows[k]["Schedule"]));
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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/ACCOUNT/AccountingVouchersModifications.aspx?pageno=349");  // Commnented By Akshay Dixit On 04-07-2022

        Response.Redirect("~/Account/BalanceSheetReport.aspx?pageno=1927");  // Added By Akshay Dixit On 04-07-2022

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        //ShowBalanceSheetRpt("BALANCESHEET", "MainBalanceSheet.rpt");

        IsShowMsg = false;

        //To set balancesheet
        btnSet_Click(sender, e);
        ShowBalanceSheetRpt1("BALANCESHEET", "BalanceSheetReportFormat1.rpt", "N");
    }

    private void ShowBalanceSheetRpt1(string reportTitle, string rptFileName, string consolidate)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim() + " to " + todate.ToString().Trim() + "," + "@DeficitSurplus=" + ViewState["NetIncome_Expense"].ToString().Trim() + "," + "@P_CODE=" + Session["comp_code"].ToString().Trim() + ",@P_CONSOLIDATE=" + consolidate;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //notinuse
    private void ShowBalanceSheetRpt(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string CollegeName = "M.T.E. SOCITEY'S WALCHAND COLLEGE OF ENGINEERING SANGLI";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@@CollegeName=" + CollegeName.ToString() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate="+ todate.ToString().Trim();

            //Script1 += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script1, true);

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@@CollegeName=" + CollegeName.ToString() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();

            // Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //string newWin = "window.open('" + url + "');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

            //ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");

            //string fullURL = "window.open('" + url + "', '_blank', 'height=500,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true); 


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

            //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
            //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Report", Script1, true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Report", "Script1", true);

            Response.Redirect(url);

            //string newWin = "window.open('" + url + "');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        //IsShowMsg = false;
        //To set balancesheet
        //btnSet_Click(sender, e);
        //ShowBalanceSheetScheduleRpt("ScheduleReport","ScheduleReport.rpt");
        //TrialBalanceReportController obj2 = new TrialBalanceReportController();
        //obj2.SetBalanceSheetGroupFormat(Session["comp_code"].ToString());
        Panel1.Visible = true;
        DataSet dsResult = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_GROUPBALANCESHEET", "MGRP_NO", "MGRP_NAME", "", "MGRP_NAME");
        if (dsResult != null)
        {
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dsResult.Tables[0];
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        HiddenField hdnSch_IDEx = GridView1.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                        hdnSch_IDEx.Value = dsResult.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();
                        Label lblprt = GridView1.Rows[i].FindControl("lblparty") as Label;
                        lblprt.Text = dsResult.Tables[0].Rows[i]["MGRP_NAME"].ToString();

                        string position = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_GROUPBALANCESHEET", "isnull(ISGROUP,0)", "MGRP_NO=" + hdnSch_IDEx.Value.ToString());
                        TextBox txtposition = GridView1.Rows[i].FindControl("txt_position1") as TextBox;
                        txtposition.Text = position;
                    }
                }
            }
        }
        updp_ModalPopupExtender1.Show();
    }

    private void ShowBalanceSheetScheduleRpt(string reportTitle, string rptFileName)
    {
        try
        {
            string sch = string.Empty;

            string Script1 = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            if (txtschedule.Text.ToString().Trim() == "")
            {
                sch = "0";
            }
            else
            {
                sch = txtschedule.Text.ToString().Trim();
            }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@V_SCHNO=" + sch.ToString();

            Script1 += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Reports", Script1, true);
            //Response.Redirect(url);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheetScheduleRpt -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController oref = new TrialBalanceReportController();
        oref.RefreshBalanceSheet(Session["comp_code"].ToString().Trim());
        BindScheduleDefination();
        CreateBalanceSheetFormat();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        if (GridView1.Rows.Count > 0)
        {
            //Set Expence Side
            int i = 0;
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox txtpos = GridView1.Rows[i].FindControl("txt_position1") as TextBox;


                if (Convert.ToString(txtpos.Text).Trim() == "")
                {
                    objCommon.DisplayUserMessage(upd, "Position Must Not be Blank", this.Page);
                    return;
                }
                else
                {
                    // objIEBS.Position = Convert.ToInt32(txtpos.Text);
                }
                string comp_code = Session["comp_code"].ToString().Trim();
                HiddenField hdnAmount = GridView1.Rows[i].FindControl("hdnAmount") as HiddenField;
                HiddenField hdnSchID = GridView1.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                // objIEBS.Sch_id = Convert.ToInt32(hdnSchID.Value);
                int ret = od.setGroupingBalanceSheet(txtpos.Text, hdnSchID.Value.ToString(), comp_code);
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
        Panel1.Visible = false;
    }
    //protected void txt_position1_Click(object sender, EventArgs e)
    //{
    //    TextBox txtPosition = rptSchDef.FindControl("txt_position1") as TextBox;
    //    for (int i = 0; i < rptSchDef.Rows.Count; i++)
    //    {
    //        TextBox a = rptSchDef.Rows[i].FindControl("txt_position1") as TextBox;
    //        if (txtPosition.Text == a.Text)
    //        {
    //            objCommon.DisplayUserMessage(upd, "Position of two Heads must be different", this.Page);
    //            return;
    //        }
    //    }
    //}
    protected void btnReset_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController obj2 = new TrialBalanceReportController();
        obj2.SetBalanceSheetGroupFormat(Session["comp_code"].ToString());
        btnSchedule_Click(sender, e);

    }
    protected void rptSchDef_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GroupReport")
        {
           SetTrialBalance();
            ShowGroupReport("Group Trial Balance", "TrialBalanceDR.rpt", e.CommandArgument.ToString());
        }
    }

    private void ShowGroupReport(string reportTitle, string rptFileName, string MGRPNO)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_COMPCODE=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy").Trim() + "," + "@P_MGRP_NO=" + MGRPNO;


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

    private void SetTrialBalance()
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
        od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"]).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"]).ToString("dd-MMM-yyyy"), 1);
        ////GenerateTrialBalanceFormat("N");
        //GenerateTrialBalanceFormatNew2();
        //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(Request.QueryString["obj"].Split(',')[0]).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Request.QueryString["obj"].Split(',')[1]).ToString("dd-MMM-yyyy"));
        //od.OrderTrialBalanceReport();
        ////UpdateTotalForLedgerNew();
        ////UpdateTotalForMainLedger();
        //od.DeleteTrialBalanceZEROAmount();
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
                            InsertChildTrialBal(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
                            AddSubGroupTrialBal(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1, Convert.ToInt16(dsLdg.Tables[0].Rows[i]["TB_POSITION"].ToString().Trim()));
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

    private void InsertChildTrialBal(string MGRP_NO, string spacechild, int POSITION)
    {

        short MGRP_NO1 = Convert.ToInt16(MGRP_NO);
        DataSet dsC = GetChildRecordTran(MGRP_NO1);
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

    protected DataSet GetChildRecordTran(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }

    private void AddSubGroupTrialBal(string MGRP_NO, string spacegroup, int POSITION)
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

                    InsertChildTrialBal(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    AddSubGroupTrialBal(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup, POSITION);
                    //InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));

                }
            }
        }
    }
    protected void rptSchDef1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GroupReport")
        {
            SetTrialBalance();
            ShowGroupReport("Group Trial Balance", "TrialBalanceDR.rpt", e.CommandArgument.ToString());
        }
    }
    protected void btnShowConsolidateBalance_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;

        //To set balancesheet
        btnSet_Click(sender, e);
        ShowBalanceSheetRpt1("BALANCESHEET", "BalanceSheetReportFormat1.rpt", "Y");
    }

    //Generate Export to Excel Report
    protected void btnExport_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;
        btnSet_Click(sender, e);

        TrialBalanceReportController objTrialBal = new TrialBalanceReportController();

        DataSet dsExpense = objTrialBal.GetLiabilitiesExcell(Session["comp_code"].ToString());
        string fromto = "FROM " + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + " TO " + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy");

        if (dsExpense.Tables[0].Rows.Count > 0)
        {
            DataRow rowTotalExp = dsExpense.Tables[0].NewRow();
            rowTotalExp["PARTYNAME"] = "<strong>" + "Net Surplus" + "</strong>";
            if (Convert.ToDouble(ViewState["NetIncome_Expense"]) > 0)
                rowTotalExp["FINALBAL"] = Convert.ToDouble(ViewState["NetIncome_Expense"].ToString());
            else
                rowTotalExp["FINALBAL"] = 0;
            dsExpense.Tables[0].Rows.Add(rowTotalExp);

            DataRow rowExp = dsExpense.Tables[0].NewRow();
            rowExp["PARTYNAME"] = "<strong>" + "Total" + "</strong>";
            if (Convert.ToDouble(ViewState["NetIncome_Expense"]) > 0)
                rowExp["FINALBAL"] = Convert.ToDouble(ViewState["NetIncome_Expense"]) + Convert.ToDouble(dsExpense.Tables[0].Rows[0]["LIABBBAL"]);
            else
                rowExp["FINALBAL"] = 0 + Convert.ToDouble(dsExpense.Tables[0].Rows[0]["LIABBBAL"]);
            dsExpense.Tables[0].Rows.Add(rowExp);

            gvLiability.DataSource = dsExpense;
            gvLiability.DataBind();

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = Session["comp_name"].ToString() + "<br/>" + fromto + "<br/> Liabilities";
            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            gvLiability.Controls[0].Controls.AddAt(0, HeaderGridRow1);
        }
        DataSet dsIncome = objTrialBal.GetAssetsExcell(Session["comp_code"].ToString());

        if (dsIncome.Tables[0].Rows.Count > 0)
        {
            DataRow rowTotalIncome = dsIncome.Tables[0].NewRow();
            rowTotalIncome["ASSETPARTYNAME"] = "<strong>" + "Net Deficit" + "</strong>";
            if (Convert.ToDouble(ViewState["NetIncome_Expense"]) < 0)
                rowTotalIncome["FINALBAL"] = -(Convert.ToDouble(ViewState["NetIncome_Expense"].ToString()));
            else
                rowTotalIncome["FINALBAL"] = 0;
            dsIncome.Tables[0].Rows.Add(rowTotalIncome);

            DataRow rowInc = dsIncome.Tables[0].NewRow();
            rowInc["ASSETPARTYNAME"] = "<strong>" + "Total" + "</strong>";
            if (Convert.ToDouble(ViewState["NetIncome_Expense"]) < 0)
                rowInc["FINALBAL"] = -(Convert.ToDouble(ViewState["NetIncome_Expense"].ToString())) + Convert.ToDouble(dsIncome.Tables[0].Rows[0]["ASSETBBAL"]);
            else
                rowInc["FINALBAL"] = 0 + Convert.ToDouble(dsIncome.Tables[0].Rows[0]["ASSETBBAL"]);
            dsIncome.Tables[0].Rows.Add(rowInc);

            gvAssets.DataSource = dsIncome;
            gvAssets.DataBind();

            GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = Session["comp_name"].ToString() + "<br/>" + fromto + "<br/> Assets";
            HeaderCell2.ColumnSpan = 3;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            gvAssets.Controls[0].Controls.AddAt(0, HeaderGridRow2);
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Balancesheet.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        Table tb = new Table();
        TableRow tr1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.Controls.Add(gvLiability);
        tr1.Cells.Add(cell1);
        TableCell cell3 = new TableCell();
        cell3.Controls.Add(gvAssets);
        tr1.Cells.Add(cell3);
        tb.Rows.Add(tr1);
        tb.RenderControl(hw);
        //style to format numbers to string
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

    }


    /* For Binding Assets and Libilities Grid */

    //private void BindLibility()
    //{
    //    string MGRPNO = string.Empty;

    //    foreach(GridViewRow RowLiability in rptSchDef.Rows)
    //    {
    //        HiddenField hid_mgrpno = RowLiability.FindControl("hid_mgrpno") as HiddenField;
    //        MGRPNO = MGRPNO + hid_mgrpno.Value + ",";
    //    }

    //    MGRPNO = MGRPNO.TrimEnd().Substring(0, MGRPNO.Length - 1);

    //    trPanel.Visible = true;

    //    TrialBalanceReportController od = new TrialBalanceReportController();
    //   // od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());

    //    od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"), 1);
    //   // GenerateTrialBalanceFormatNew2();
    //    //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
    //    //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
    //    //od.OrderTrialBalanceReport();

    //    DataSet dsFinalHead = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME as Party_name,(case  when OP_BALANCE<0 then 'Cr' else 'Dr' end) as OpbalMode,(case  when OP_BALANCE<0 then -OP_BALANCE else OP_BALANCE end) as OP_BALANCE1,(case  when CL_BALANCE<0 then 'Cr' else 'Dr' end) as clBalMode,(case  when CL_BALANCE<0 then -CL_BALANCE else CL_BALANCE end) as CL_BALANCE1", "*", "FANO = 1", "TB_POSITION,ID,PARTY_NO");
    //    for (int i = 0; i < dsFinalHead.Tables[0].Rows.Count; i++)
    //    {
    //        dsFinalHead.Tables[0].Rows[i]["PARTYNAME"] = dsFinalHead.Tables[0].Rows[i]["PARTYNAME"].ToString().Replace(" ", "&nbsp;");
    //    }
    //    RptData.DataSource = dsFinalHead;
    //    RptData.DataBind();

    //    for (int i = 0; i < RptData.Items.Count; i++)
    //    {
    //        ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
    //        HtmlTableCell trPartyName = RptData.Items[i].FindControl("trPartyName") as HtmlTableCell;
    //        Label lblParty = RptData.Items[i].FindControl("lblParty") as Label;
    //        if (btnEdit.CommandArgument == "0" || btnEdit.ToolTip == "PROFIT & LOSS A/c")
    //        {
    //            btnEdit.Visible = false;
    //            btnEdit.Attributes.Add("class", "altitem");

    //        }
    //        else
    //        {
    //            //lnkLedgerReport.Attributes.Add("onclick", "return ShowledgerReport('" + lnkLedgerReport.ToolTip.Trim() + "','" + lnkLedgerReport.CommandArgument + "');");
    //            trPartyName.Attributes.Add("onclick", "ShowledgerReport('" + btnEdit.ToolTip.Trim() + "','" + btnEdit.CommandArgument + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy") + "');");
    //            trPartyName.Attributes.Add("onmouseout", "this.style.backgroundColor='ThreeDFace'");
    //            trPartyName.Attributes.Add("onmouseover", "this.style.backgroundColor='#81BEF7'");
    //            trPartyName.Attributes.Add("style", "width: 33%;cursor:pointer;");

    //        }
    //    }

    //    //DataSet dsTotal = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

    //    //Label lblTotalDebit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotalDebit") as Label;
    //    //Label lblTotalCredit = RptData.Controls[RptData.Controls.Count - 1].FindControl("lblTotalCredit") as Label;
    //    //lblTotalCredit.Text = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
    //    //lblTotalDebit.Text = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
    //}



    //private void BindAssests()
    //{
    //    string MGRPNO = string.Empty;

    //    foreach (GridViewRow RowAssests in rptSchDef1.Rows)
    //    {
    //        HiddenField hid_mgrpno = RowAssests.FindControl("hid_mgrpno") as HiddenField;
    //        MGRPNO = MGRPNO + hid_mgrpno.Value + ",";
    //    }

    //    MGRPNO = MGRPNO.TrimEnd().Substring(0, MGRPNO.Length - 1);

    //    trPanel.Visible = true;

    //    TrialBalanceReportController od = new TrialBalanceReportController();
    //    //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());

    //    od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"), 1);
    //    //GenerateTrialBalanceFormatNew2();
    //    //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
    //    //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
    //    //od.OrderTrialBalanceReport();

    //    DataSet dsFinalHead = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "PARTYNAME as Party_name,(case  when OP_BALANCE<0 then 'Cr' else 'Dr' end) as OpbalMode,(case  when OP_BALANCE<0 then -OP_BALANCE else OP_BALANCE end) as OP_BALANCE1,(case  when CL_BALANCE<0 then 'Cr' else 'Dr' end) as clBalMode,(case  when CL_BALANCE<0 then -CL_BALANCE else CL_BALANCE end) as CL_BALANCE1", "*", "FANO = 2", "TB_POSITION,ID,PARTY_NO");
    //    for (int i = 0; i < dsFinalHead.Tables[0].Rows.Count; i++)
    //    {
    //        dsFinalHead.Tables[0].Rows[i]["PARTYNAME"] = dsFinalHead.Tables[0].Rows[i]["PARTYNAME"].ToString().Replace(" ", "&nbsp;");
    //    }
    //    RptAssets.DataSource = dsFinalHead;
    //    RptAssets.DataBind();

    //    for (int i = 0; i < RptAssets.Items.Count; i++)
    //    {
    //        ImageButton btnEdit = RptAssets.Items[i].FindControl("btnEdit") as ImageButton;
    //        HtmlTableCell trPartyName = RptAssets.Items[i].FindControl("trPartyName") as HtmlTableCell;
    //        Label lblParty = RptAssets.Items[i].FindControl("lblParty") as Label;
    //        if (btnEdit.CommandArgument == "0" || btnEdit.ToolTip == "PROFIT & LOSS A/c")
    //        {
    //            btnEdit.Visible = false;
    //            btnEdit.Attributes.Add("class", "altitem");

    //        }
    //        else
    //        {
    //            //lnkLedgerReport.Attributes.Add("onclick", "return ShowledgerReport('" + lnkLedgerReport.ToolTip.Trim() + "','" + lnkLedgerReport.CommandArgument + "');");
    //            trPartyName.Attributes.Add("onclick", "ShowledgerReport('" + btnEdit.ToolTip.Trim() + "','" + btnEdit.CommandArgument + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy") + "');");
    //            trPartyName.Attributes.Add("onmouseout", "this.style.backgroundColor='ThreeDFace'");
    //            trPartyName.Attributes.Add("onmouseover", "this.style.backgroundColor='#81BEF7'");
    //            trPartyName.Attributes.Add("style", "width: 33%;cursor:pointer;");

    //        }
    //    }

    //    //DataSet dsTotal = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");

    //    //Label lblTotalDebit = RptAssets.Controls[RptData.Controls.Count - 1].FindControl("lblTotalDebit") as Label;
    //    //Label lblTotalCredit = RptAssets.Controls[RptData.Controls.Count - 1].FindControl("lblTotalCredit") as Label;
    //    //lblTotalCredit.Text = dsTotal.Tables[0].Rows[0]["totCredit"].ToString();
    //    //lblTotalDebit.Text = dsTotal.Tables[0].Rows[0]["totDebit"].ToString();
    //}






    //protected void btnShowGrid_Click(object sender, EventArgs e)
    //{
    //    BindLibility();
    //    BindAssests();
    //}
}
