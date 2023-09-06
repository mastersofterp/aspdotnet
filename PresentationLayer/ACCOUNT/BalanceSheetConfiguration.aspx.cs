//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : BALANCE SHEET CONFIGURATION
// CREATION DATE : 08-MARCH-2010                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : Nitin Meshram
// MODIFIED DESC : To Show Balance Sheet
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

public partial class BalanceSheetConfiguration : System.Web.UI.Page
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
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                btnShowWithLedger.Enabled = false;
                btnshow.Enabled = false;
                BindScheduleDefination();
                GenerateTrialBalnce();
                //CreateBalanceSheetFormat();                            
            }
          
        }
        //divMsg.InnerHtml = string.Empty;
    }

    private void GenerateTrialBalnce()
    {
        TrialBalanceReportController od = new TrialBalanceReportController();
       // od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
        //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"));
        //GenerateTrialBalanceFormatNew2();
        //od.OrderTrialBalanceReport();
        //UpdateTotalForLedgerNew();
        //UpdateTotalForMainLedger();
        //od.DeleteTrialBalanceZEROAmount();


        //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
        od.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"), 1);
        //GenerateTrialBalanceFormatNew2();
        //od.INSERT_PROFIT_LOSS(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"));
        //od.OrderTrialBalanceReport();
        objIEBSController.SetAmount(Session["comp_code"].ToString());

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
                objUCommon.ShowError(Page, "TrialBalanceReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
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
        obj.CallBalanceSheetFormatProc(isNetSurplus,"Y");
    
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
        DataSet dsExpenceSchedule = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_schmaster SchMa inner join ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger SchLedegr on (SchMa.Sch_ID=SchLedegr.Sch_ID)", "SchMa.Sch_ID,SchMa.Sch_name", "(sum(Amount_Dr)-sum(Amount_CR)) as cl_balance", "SchMa.Sch_Type='L' group by SchMa.sch_ID,SchMa.Sch_name", "");

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
    protected void BindAssetSide()
    {

        //for asset side============

        DataSet dsIncomeSchedule = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_schmaster SchMa inner join ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger SchLedegr on (SchMa.Sch_ID=SchLedegr.Sch_ID)", "SchMa.Sch_ID,SchMa.Sch_name", "sum(Amount_CR)-(sum(Amount_Dr)) as cl_balance", "SchMa.Sch_Type='A' group by SchMa.sch_ID,SchMa.Sch_name", "");

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
    protected void SetPosition(string value3,int i)
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
    string  id= t.ClientID;
        
      string[] ids =  id.Split('_');
      if (ids.Length > 0)
      {
          string idstr = ids[3].Remove(0, 3).ToString();
          HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
          TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
          HiddenField headno = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;
          headno.Value = "10";
           int l = 0;
           for (l = 0; l < rptSchDef.Rows.Count; l++)
           {

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
    private Boolean CheckPosition(string EnteredPosition,string PresentPosition )
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
            headno.Value = "10";
            int l = 0;
            for (l = 0; l < rptSchDef1.Rows.Count; l++)
            {
                if (Convert.ToInt16(idstr) - 2 != l)
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
        if (rptSchDef.Rows.Count > 0 && rptSchDef1.Rows.Count > 0)
        {
            //Set Expence Side
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;


                if (Convert.ToString(txtpos.Text).Trim() == "" || Convert.ToString(txtpos.Text).Trim()=="0")
                {
                    objCommon.DisplayUserMessage(upd, "Position should not Blank or 0", this.Page);
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
                    objCommon.DisplayUserMessage(upd, "Position should not Blank or 0", this.Page);
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
                btnShowWithLedger.Enabled = true;
                btnExport.Enabled = true;
            }


        }


    }

    protected void ArrangeExactBalanceSheet()
    { 
        //for Liability side
        DataSet dsOp = objCommon.FillDropDown("TEMP_BALANACESHEET_LIABILITY", "*", "",string.Empty, "counterid");
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
                for(i=0;i<dsext.Tables[0].Rows.Count;i++)
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
                                    z =Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[k]["Schedule"]).Trim());
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
        Response.Redirect("~/ACCOUNT/AccountingVouchersModifications.aspx?pageno=349");
       
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        //ShowBalanceSheetRpt("BALANCESHEET", "MainBalanceSheet.rpt");

        IsShowMsg = false;

        ShowBalanceSheetRpt("Balance Sheet", "BalanceSheetNew.rpt");
    }

    

    
    private void ShowBalanceSheetRpt(string reportTitle, string rptFileName)
    {
        try
        {
            

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
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

    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;
        //To set balancesheet
        btnSet_Click(sender, e);
        
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController oref = new TrialBalanceReportController();
        oref.RefreshBalanceSheet(Session["comp_code"].ToString().Trim());
        BindScheduleDefination();
        CreateBalanceSheetFormat();    
    }
    protected void rptSchDef_RowCommand(object sender, GridViewCommandEventArgs e)
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
                url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWiseForBalanceSheet.rpt";

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
                url += "&path=~,Reports,ACCOUNT," + "LedgerScheduleWiseForBalanceSheet.rpt";

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

    protected void btnShowWithLedger_Click(object sender, EventArgs e)
    {
        ShowBalanceSheetRpt("Balance Sheet", "BalnceSheetWithLedger.rpt");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ShowBalanceSheetExcel("BalanceSheet", "BalnceSheetWithLedger.rpt");
    }

    
}