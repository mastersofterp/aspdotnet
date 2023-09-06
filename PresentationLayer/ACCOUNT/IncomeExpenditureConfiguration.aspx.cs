//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : INCOME EXPENDITURE REPORT CONFIGURATION
// CREATION DATE : 08-APRIL-2010                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : NITIN MESHRAM
// MODIFIED DESC : MODIFY TO SHOW SHEDULE AND REPORT
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
using IITMS.NITPRM;


public partial class IncomeExpenditureConfiguration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    IncomeExpenBalanceSheetController objIEBSController = new IncomeExpenBalanceSheetController();
    IncomeExpenBalanceSheet objIEBS = new IncomeExpenBalanceSheet();

    string space1 = "     ".ToString();
    string space2 = "          ".ToString();
    string space3 = string.Empty;

    static string fromdate = string.Empty;
    static string todate = string.Empty;
    static string isNetSurplus = string.Empty;
    public bool IsShowMsg = true;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //fromdate = Request.QueryString["obj"].Split(',')[0].ToString().Trim();
        //todate = Request.QueryString["obj"].Split(',')[1].ToString().Trim();

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                GenerateTrialBalnce();
                BindScheduleDefination();
                CreateBalanceSheetFormat();
                btnExport.Enabled = false;
            }

        }         
    }

    private void GenerateTrialBalnce()
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
        //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"));
        od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"), 1);
        //GenerateTrialBalanceFormat("N");
        //GenerateTrialBalanceFormatNew2();
        //od.OrderTrialBalanceReport();
        ////UpdateTotalForLedgerNew();
        ////UpdateTotalForMainLedger();
        //od.DeleteTrialBalanceZEROAmount();
        objIEBSController.SetAmount(Session["comp_code"].ToString());

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
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);
                            InsertChild(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1);
                            AddSubGroup(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim(), space1);
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

    private void AddSubGroup(string MGRP_NO, string spacegroup)
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
                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                    oTran1.AddTrialBalanceReportFormat(oEntity);

                    InsertChild(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup);
                    AddSubGroup(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim(), spacegroup);
                    //InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));

                }
            }
        }
    }

    private void InsertChild(string MGRP_NO, string spacechild)
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



                            }

                        }


                    }


                }

                space1 = "      ".ToString();

            }
        }

    }

    protected DataSet GetChildRecord(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;
 
    }

    protected void CreateBalanceSheetFormat()
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        obj.CallBalanceSheetFormatProc(isNetSurplus, "N");
    }

    protected void BindScheduleDefination()
    {
        string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();
        rptSchDef.DataSource = null;
        rptSchDef.DataBind();
        rptSchDef1.DataSource = null;
        rptSchDef1.DataBind();
        
        BindExpenceSide();
        BindIncomeSide();
    }


    private void BindExpenceSide()
    {
        DataSet dsExpenceSchedule = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_schmaster SchMa inner join ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger SchLedegr on (SchMa.Sch_ID=SchLedegr.Sch_ID)", "SchMa.Sch_ID,SchMa.Sch_name", "(sum(Amount_CR)+sum(-Amount_Dr)) as cl_balance", "SchMa.Sch_Type='E' group by SchMa.sch_ID,SchMa.Sch_name", "");
        
        if (dsExpenceSchedule != null)
        {
            if (dsExpenceSchedule.Tables[0].Rows.Count > 0)
            {
                rptSchDef.DataSource = dsExpenceSchedule.Tables[0];
                rptSchDef.DataBind();
                if (rptSchDef.Rows.Count > 0)
                {
                    for (int i = 0; i < rptSchDef.Rows.Count; i++)
                    {
                        HiddenField hdnSch_IDEx = rptSchDef.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                        hdnSch_IDEx.Value = dsExpenceSchedule.Tables[0].Rows[i]["Sch_ID"].ToString().Trim();
                        TextBox lblprt = rptSchDef.Rows[i].FindControl("lblparty") as TextBox;
                        lblprt.Text = dsExpenceSchedule.Tables[0].Rows[i]["Sch_name"].ToString();

                        HiddenField hdnAmount = rptSchDef.Rows[i].FindControl("hdnAmount") as HiddenField;
                        hdnAmount.Value = dsExpenceSchedule.Tables[0].Rows[i]["cl_balance"].ToString().Trim();

                        string position = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_schMaster", "Position", "Sch_ID=" + hdnSch_IDEx.Value.ToString());
                        TextBox txtposition = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                        txtposition.Text = position;
                    }
                }
            }
        }
    }


    private void BindIncomeSide()
    {
        DataSet dsIncomeSchedule = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_schmaster SchMa inner join ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger SchLedegr on (SchMa.Sch_ID=SchLedegr.Sch_ID)", "SchMa.Sch_ID,SchMa.Sch_name", "sum(Amount_Dr)-(sum(Amount_CR)) as cl_balance", "SchMa.Sch_Type='I' group by SchMa.sch_ID,SchMa.Sch_name", "");

        if (dsIncomeSchedule != null)
        {
            if (dsIncomeSchedule.Tables[0].Rows.Count > 0)
            {
                rptSchDef1.DataSource = dsIncomeSchedule.Tables[0];
                rptSchDef1.DataBind();
                if (rptSchDef1.Rows.Count > 0)
                {
                    for (int i = 0; i < rptSchDef1.Rows.Count; i++)
                    {
                        HiddenField hdnSch_IDIncome = rptSchDef1.Rows[i].FindControl("hdnSchIdIncome") as HiddenField;
                        hdnSch_IDIncome.Value = dsIncomeSchedule.Tables[0].Rows[i]["Sch_ID"].ToString().Trim();
                        TextBox lblprt = rptSchDef1.Rows[i].FindControl("lblparty") as TextBox;
                        lblprt.Text = dsIncomeSchedule.Tables[0].Rows[i]["Sch_name"].ToString();

                        HiddenField hdnAmount = rptSchDef1.Rows[i].FindControl("hdnAmount") as HiddenField;
                        hdnAmount.Value = dsIncomeSchedule.Tables[0].Rows[i]["cl_balance"].ToString().Trim();

                        string position = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_schMaster", "Position", "Sch_ID=" + hdnSch_IDIncome.Value.ToString());
                        TextBox txtposition = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;
                        txtposition.Text = position;
                    }
                }
            }
        }
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
                        row["Op_balance"] = "0";
                    }
                    else
                    {
                        row["Op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                        //row["Cl_balance"] = ((Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2) - Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Op"]);
                        row["Cl_balance"] = ((Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]))) - Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
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
                        row["Op_balance"] = "0";
                    }
                    else
                    {
                        row["Op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                        //row["Cl_balance"] = ((Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2) - Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Op"]);
                        row["Cl_balance"] = ((Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]))) - Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
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
    }

    protected void txt_position_TextChanged(object sender, EventArgs e)
    {
        //TextBox txtPosition = rptSchDef.FindControl("txt_position") as TextBox;
        //string txtposition = txtPosition.Text;
        //for (int i = 0; i < rptSchDef.Rows.Count; i++)
        //{
        //    TextBox a = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
        //    string abc = a.Text;
            
        //    if (txtPosition.Text == a.Text)
        //    {
        //        objCommon.DisplayUserMessage(upd, "Position of two Schedule is must be different", this.Page);
        //        return;
        //    }
        //}
    }

    //protected void txt_position_TextChanged(object sender, EventArgs e)
    //{
    //    TextBox t = sender as TextBox;
    //    string id = t.ClientID;

    //    string[] ids = id.Split('_');
    //    if (ids.Length > 0)
    //    {
    //        string idstr = ids[3].Remove(0, 3).ToString();
    //        HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
    //        TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
    //        HiddenField headno = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;
    //        headno.Value = "10";
    //        int l = 0;
    //        for (l = 0; l < rptSchDef.Rows.Count; l++)
    //        {

    //            if (Convert.ToInt16(idstr) - 2 != l)
    //            {

    //                TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(l)].FindControl("txt_position") as TextBox;

    //                if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
    //                {
    //                    objCommon.DisplayMessage(upd, "Position No. Allready Available, Try Another. ", this);
    //                    textValueChk.Focus();
    //                    return;

    //                }
    //            }

    //        }



    //        int i = 0;
    //        for (i = 0; i < rptSchDef.Rows.Count; i++)
    //        {
    //            HiddenField hid2 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
    //            if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
    //            {
    //                TextBox textValue = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
    //                TextBox textValue1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
    //                textValue.Text = textValue1.Text;

    //                HiddenField hid3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
    //                SetPosition(hid3.Value.ToString().Trim(), i);
    //            }
    //        }
    //    }

    //}

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

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    //objCommon.DisplayMessage(upd, "Income Expenditure Report Format Set Successfully, To View Report Please Click On Show Button.", this);
    //    HiddenField hdnSchID = rptSchDef.FindControl("hdnSchIdEx") as HiddenField;
    //    //string abc = rptSchDef.Rows[e.NewSelectedIndex].Cells[4].Text;

    //    string abc = rptSchDef.Rows[e
    //    string Script = string.Empty;
    //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

    //    url += "Reports/CommonReport.aspx?";
    //    url += "pagetitle=" + "SCHEDULE WISE LEDGER";
    //    url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWise.rpt";

    //    url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();


    //    url += "&param=@P_SCH_ID=" + hdnSchID.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

    //    Script += " window.open('" + url + "','" + "SCHEDULE WISE LEDGER" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

    //    ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
    //}


    protected void btnSet_Click(object sender, EventArgs e)
    {
        if (rptSchDef.Rows.Count > 0 && rptSchDef1.Rows.Count > 0)
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
                    objIEBS.Position = Convert.ToInt32(txtpos.Text);
                }
                string comp_code = Session["comp_code"].ToString().Trim();
                HiddenField hdnAmount = rptSchDef.Rows[i].FindControl("hdnAmount") as HiddenField;
                HiddenField hdnSchID = rptSchDef.Rows[i].FindControl("hdnSchIdEx") as HiddenField;
                objIEBS.Sch_id = Convert.ToInt32(hdnSchID.Value);
                int ret = objIEBSController.setReport(objIEBS, comp_code, Convert.ToString(hdnAmount.Value));
                if (ret == 1)
                {
                    IsShowMsg = true;
                }
                else
                {
                    IsShowMsg = false;
                }
            }

            //Set Income Side
            i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;


                if (Convert.ToString(txtpos.Text).Trim() == "" || Convert.ToString(txtpos.Text).Trim()=="0")
                {
                    objCommon.DisplayUserMessage(upd, "Position Must Not be blank or 0", this.Page);
                    return;
                }
                else
                {
                    objIEBS.Position = Convert.ToInt32(txtpos.Text);
                }
                string comp_code = Session["comp_code"].ToString().Trim();
                HiddenField hdnAmount = rptSchDef1.Rows[i].FindControl("hdnAmount") as HiddenField;
                HiddenField hdnSchID = rptSchDef1.Rows[i].FindControl("hdnSchIdIncome") as HiddenField;
                objIEBS.Sch_id = Convert.ToInt32(hdnSchID.Value);
                int ret = objIEBSController.setReport(objIEBS, comp_code, Convert.ToString(hdnAmount.Value));
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
                objCommon.DisplayMessage(upd, "Income Expenditure Report Format Set Successfully, To View Report Please Click On Show Button.", this);
                btnshow.Enabled = true;
                btnshowWithLedger.Enabled = true;
                btnExport.Enabled = true;
            }


        }
    }






    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ACCOUNT/AccountingVouchersModifications.aspx?pageno=349");

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;
        //To set balancesheet
        //btnSet_Click(sender, e);
        //ShowBalanceSheetRpt("INCOMEEXPENDITURE", "MainIncomeExpenseNew.rpt");
        ShowBalanceSheetRpt("INCOMEEXPENDITURE", "Income_ExpenditureRPT.rpt");
    }

    private void ShowBalanceSheetRpt(string reportTitle, string rptFileName)
    {
        try
        {
            //string Script1 = string.Empty;
            //string CollegeName ="M.T.E. SOCITEY'S WALCHAND COLLEGE OF ENGINEERING SANGLI";
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            //string LedgerName = string.Empty;
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@@CollegeName=" + CollegeName.ToString() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate="+ todate.ToString().Trim();
            //Script1 += " window.Open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Reports", Script1, true);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString() ;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);

            //Response.Redirect(url);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

 

    private void ShowBalanceSheetScheduleRpt(string reportTitle, string rptFileName)
    {
        try
        {
            string sch = string.Empty;

            // string Script1 = string.Empty;
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            // if (txtschedule.Text.ToString().Trim() == "")
            // {
            //     sch = "0";
            // }
            // else
            // {
            //     sch = txtschedule.Text.ToString().Trim();
            // }

            // url += "Reports/CommonReport.aspx?";
            // url += "pagetitle=" + reportTitle;
            // url += "&path=~,Reports,ACCOUNT," + rptFileName;
            // url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@V_SCHNO=" + sch.ToString();
            //// url += "&param=@V_SCHNO=" + sch.ToString();

            // Script1 += " window.Open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            // ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Reports", Script1, true);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@V_SCHNO=" + sch.ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
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


    protected void txt_position_Click(object sender, EventArgs e)
    {
        TextBox txtPosition = rptSchDef.FindControl("txt_position") as TextBox;
        for (int i = 0; i < rptSchDef.Rows.Count; i++)
        {
            TextBox a = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
            if (txtPosition.Text == a.Text)
            {
                objCommon.DisplayUserMessage(upd, "Position of two Schedule is must be different", this.Page);
                return;
            }
        }
    }
    //protected void rptSchDef_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    ////HiddenField hdnSchId = FindControl["hdnSchIdEx"] as HiddenField;
    //    //HiddenField hdnSchID = rptSchDef.Rows[e.NewSelectedIndex].FindControl("hdnSchIdEx") as HiddenField;
    //    //string abc = rptSchDef.Rows[e.NewSelectedIndex].Cells[4].Text;
    //    //string Script = string.Empty;
    //    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

    //    //url += "Reports/CommonReport.aspx?";
    //    //url += "pagetitle=" + "SCHEDULE WISE LEDGER";
    //    //url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWise.rpt";

    //    //url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();


    //    //url += "&param=@P_SCH_ID=" + hdnSchID.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

    //    //Script += " window.open('" + url + "','" + "SCHEDULE WISE LEDGER" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

    //    //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);

    //}
    
    protected void rptSchDef_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

        string id = string.Empty;


        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Print")
            {
                 
                id = e.CommandArgument.ToString();
                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "SCHEDULE WISE LEDGER";
                url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWise.rpt";

                //url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();


                url += "&param=@P_SCH_ID=" + id.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

                Script += " window.open('" + url + "','" + "SCHEDULE WISE LEDGER" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);

                
            }
            
           
        }

        catch (Exception ee)
        {
            string message = ee.Message;
        }



        //if (e.CommandName == "Print")
        //{
        //    HiddenField hdnSchID = rptSchDef.FindControl("hdnSchIdEx") as HiddenField;
        //    //str abc = rptSchDef.Rows[e.CommandArgument].Cells[4].ToString();
        //    string Script = string.Empty;
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + "SCHEDULE WISE LEDGER";
        //    url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWise.rpt";

        //    url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();


        //    url += "&param=@P_SCH_ID=" + hdnSchID.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

        //    Script += " window.open('" + url + "','" + "SCHEDULE WISE LEDGER" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

        //    ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
        //}
    }
    protected void rptSchDef_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rptSchDef1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Empty;


        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Print")
            {

                id = e.CommandArgument.ToString();
                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "SCHEDULE WISE LEDGER";
                url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWise.rpt";

                //url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();


                url += "&param=@P_SCH_ID=" + id.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

                Script += " window.open('" + url + "','" + "SCHEDULE WISE LEDGER" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);


            }

        }

        catch (Exception ee)
        {
            string message = ee.Message;
        }
    }
    protected void btnshowWithLedger_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;

        ShowBalanceSheetRpt("INCOMEEXPENDITURE", "Income_ExpenditureWithLedger.rpt");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ShowBalanceSheetExcel("INCOMEEXPENDITURE", "Income_ExpenditureWithLedger.rpt");
    }

    private void ShowBalanceSheetExcel(string reportTitle, string rptFileName)
    {
        try
        { 
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "path=~,Reports,ACCOUNT," + rptFileName + "&exporttype=doc&filename=" + reportTitle;
            //url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);

            //Response.Redirect(url);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
