using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class TrialBalanceReportController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public TrialBalanceReportController()
                {



                }
                public TrialBalanceReportController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }
                public int AddTrialBalanceReportFormat(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@V_PARTYNAME", objMG.PartyName);
                        objParams[1] = new SqlParameter("@V_MGRP_NO", objMG.MGRPNO);
                        objParams[2] = new SqlParameter("@V_PRNO", objMG.PRNO);
                        objParams[3] = new SqlParameter("@V_PARTY_NO", objMG.PARTYNO);
                        objParams[4] = new SqlParameter("@V_OP_BAL", objMG.OPBALANCE);
                        objParams[5] = new SqlParameter("@V_CL_BAL", objMG.CLBALANCE);
                        objParams[6] = new SqlParameter("@V_IS_PARTY", objMG.ISPARTY);
                        objParams[7] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[8] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objParams[9] = new SqlParameter("@V_FANO", objMG.FANO);
                        objParams[10] = new SqlParameter("@V_TB_POSITION", objMG.POSITION);
                        //objParams[10] = new SqlParameter("@V_LINDEX", 0);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST_INSERT", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public int AddBalanceSheetAssetsFormat(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@V_PARTYNAME", objMG.PartyName);
                        objParams[1] = new SqlParameter("@V_MGRP_NO", objMG.MGRPNO);
                        objParams[2] = new SqlParameter("@V_PRNO", objMG.PRNO);
                        objParams[3] = new SqlParameter("@V_PARTY_NO", objMG.PARTYNO);
                        objParams[4] = new SqlParameter("@V_OP_BAL", objMG.OPBALANCE);
                        objParams[5] = new SqlParameter("@V_CL_BAL", objMG.CLBALANCE);
                        objParams[6] = new SqlParameter("@V_IS_PARTY", objMG.ISPARTY);
                        objParams[7] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[8] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objParams[9] = new SqlParameter("@V_FANO", objMG.FANO);
                        objParams[10] = new SqlParameter("@V_TB_POSITION", objMG.POSITION);
                        //objParams[10] = new SqlParameter("@V_LINDEX", 0);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST_INSERT_INTO_ASSETS", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public int AddBalanceSheetLiabilityFormat(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@V_PARTYNAME", objMG.PartyName);
                        objParams[1] = new SqlParameter("@V_MGRP_NO", objMG.MGRPNO);
                        objParams[2] = new SqlParameter("@V_PRNO", objMG.PRNO);
                        objParams[3] = new SqlParameter("@V_PARTY_NO", objMG.PARTYNO);
                        objParams[4] = new SqlParameter("@V_OP_BAL", objMG.OPBALANCE);
                        objParams[5] = new SqlParameter("@V_CL_BAL", objMG.CLBALANCE);
                        objParams[6] = new SqlParameter("@V_IS_PARTY", objMG.ISPARTY);
                        objParams[7] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[8] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objParams[9] = new SqlParameter("@V_FANO", objMG.FANO);
                        objParams[10] = new SqlParameter("@V_TB_POSITION", objMG.POSITION);
                        //objParams[10] = new SqlParameter("@V_LINDEX", 0);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST_INSERT_INTO_LIABILITY", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public int AddPLIncomeFormat(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@V_PARTYNAME", objMG.PartyName);
                        objParams[1] = new SqlParameter("@V_MGRP_NO", objMG.MGRPNO);
                        objParams[2] = new SqlParameter("@V_PRNO", objMG.PRNO);
                        objParams[3] = new SqlParameter("@V_PARTY_NO", objMG.PARTYNO);
                        objParams[4] = new SqlParameter("@V_OP_BAL", objMG.OPBALANCE);
                        objParams[5] = new SqlParameter("@V_CL_BAL", objMG.CLBALANCE);
                        objParams[6] = new SqlParameter("@V_IS_PARTY", objMG.ISPARTY);
                        objParams[7] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[8] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objParams[9] = new SqlParameter("@V_FANO", objMG.FANO);
                        objParams[10] = new SqlParameter("@V_TB_POSITION", objMG.POSITION);
                        //objParams[10] = new SqlParameter("@V_LINDEX", 0);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST_INSERT_INTO_INCOME", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public int AddPLExpensesFormat(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@V_PARTYNAME", objMG.PartyName);
                        objParams[1] = new SqlParameter("@V_MGRP_NO", objMG.MGRPNO);
                        objParams[2] = new SqlParameter("@V_PRNO", objMG.PRNO);
                        objParams[3] = new SqlParameter("@V_PARTY_NO", objMG.PARTYNO);
                        objParams[4] = new SqlParameter("@V_OP_BAL", objMG.OPBALANCE);
                        objParams[5] = new SqlParameter("@V_CL_BAL", objMG.CLBALANCE);
                        objParams[6] = new SqlParameter("@V_IS_PARTY", objMG.ISPARTY);
                        objParams[7] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[8] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objParams[9] = new SqlParameter("@V_FANO", objMG.FANO);
                        objParams[10] = new SqlParameter("@V_TB_POSITION", objMG.POSITION);
                        //objParams[10] = new SqlParameter("@V_LINDEX", 0);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST_INSERT_INTO_EXPENSES", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }

                //Backup Storage Data For Transaction================
                public int AddTemporaryBackUp(BackupStorageClass obsc)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add 
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_TranId", obsc.TranId);
                        objParams[1] = new SqlParameter("@P_Particulars", obsc.Particulars);
                        objParams[2] = new SqlParameter("@P_Balance", obsc.Balance);
                        objParams[3] = new SqlParameter("@P_Narration", obsc.Narration);
                        objParams[4] = new SqlParameter("@P_Amount", obsc.Amount);
                        objParams[5] = new SqlParameter("@P_Debit", obsc.Debit);
                        objParams[6] = new SqlParameter("@P_Credit", obsc.Credit);
                        objParams[7] = new SqlParameter("@P_Mode", obsc.Mode);
                        objParams[8] = new SqlParameter("@P_PartyNo", obsc.PartyNo);
                        objParams[9] = new SqlParameter("@P_ChequeNo", obsc.ChequeNo);
                        objParams[10] = new SqlParameter("@P_ChequeDate", obsc.ChequeDate);
                        objParams[11] = new SqlParameter("@P_OPartyNo", obsc.OPartyNo);
                        objParams[12] = new SqlParameter("@P_TranMode", obsc.TranMode);
                        objParams[13] = new SqlParameter("@P_IpAddress", obsc.IpAddress);

                        objSQLHelper.ExecuteNonQuerySP("SP_TEMPORARY_TRANSACTION_STORAGE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.AddTemporaryBackUp-> " + ex.ToString());
                    }
                    return 1;
                }




                public int UpdateTemporaryStorageBack(double Balance, Int16 PartyNo, string IpAddress)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_Balance", Balance);
                        objParams[1] = new SqlParameter("@P_PartyNo", PartyNo);
                        objParams[2] = new SqlParameter("@P_IpAddress", IpAddress);
                        objSQLHelper.ExecuteNonQuerySP("SP_TEMPORARY_TRANSACTION_STORAGE_UPDATE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.UpdateTemporaryStorageBack-> " + ex.ToString());
                    }
                    return 1;
                }


                public int DeleteTemporaryStorageBack(Int16 PartyNo, string IpAddress)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TranId", PartyNo);
                        objParams[1] = new SqlParameter("@P_IpAddress", IpAddress);
                        objSQLHelper.ExecuteNonQuerySP("SP_TEMPORARY_TRANSACTION_STORAGE_DELETE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTemporaryStorageBack-> " + ex.ToString());
                    }
                    return 1;
                }


                public int RefreshBalanceSheet(string CompanyCode)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE", CompanyCode);
                        objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REFRESH_BALANCESHEET", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.RefreshBalanceSheet-> " + ex.ToString());
                    }
                    return 1;
                }





                //====================================================

                public int DeleteDayBookRecord()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_DAYBOOK_REPORT_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteDayBookRecord-> " + ex.ToString());
                    }
                    return 1;
                }

                public int UpdateTrialBalanceAmount(TrialBalanceReport objMG)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@V_MGRPNO", objMG.MGRPNO);
                        objParams[1] = new SqlParameter("@V_DEBIT", objMG.DEBIT);
                        objParams[2] = new SqlParameter("@V_CREDIT", objMG.CREDIT);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRIALBALANCE_AMOUNT_UPDATE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.UpdateTrialBalanceAmount-> " + ex.ToString());
                    }
                    return 1;
                }

                public int UpdateAllTrialBalanceAmount(int counterid, double cr, double dr, double op, double cl)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@V_CNTID", counterid);
                        objParams[1] = new SqlParameter("@V_DEBIT", dr);
                        objParams[2] = new SqlParameter("@V_CREDIT", cr);
                        objParams[3] = new SqlParameter("@V_CLBALANCE", cl);
                        objParams[4] = new SqlParameter("@V_OPBALANCE", op);
                        objParams[5] = new SqlParameter("@V_MGRP_FLAG", "N");

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRIALBALANCE_ALL_AMOUNT_UPDATE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.UpdateAllTrialBalanceAmount-> " + ex.ToString());
                    }
                    return 1;
                }


                public int UpdateAllTrialBalanceAmountByMGRPNO(int MGRPNO, double cr, double dr, double op, double cl)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@V_CNTID", MGRPNO);
                        objParams[1] = new SqlParameter("@V_DEBIT", dr);
                        objParams[2] = new SqlParameter("@V_CREDIT", cr);
                        objParams[3] = new SqlParameter("@V_CLBALANCE", cl);
                        objParams[4] = new SqlParameter("@V_OPBALANCE", op);
                        objParams[5] = new SqlParameter("@V_MGRP_FLAG", "Y");
                        objParams[6] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRIALBALANCE_ALL_AMOUNT_UPDATE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.UpdateAllTrialBalanceAmountByMGRPNO-> " + ex.ToString());
                    }
                    return 1;
                }

                public int DeleteTrialBalanceAmount()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRIALBALANCE_AMOUNT_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceAmount-> " + ex.ToString());
                    }
                    return 1;
                }
                public int DeleteTrialBalanceZEROAmount()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRIALBALANCE_ZEROAMOUNT_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceZEROAmount-> " + ex.ToString());
                    }
                    return 1;
                }
                public int DeleteBalanceSheetZEROAmount()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BALANCESHEET_ZEROAMOUNT_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceZEROAmount-> " + ex.ToString());
                    }
                    return 1;
                }
                public int DeleteIncomeExpensesZEROAmount()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PL_ZEROAMOUNT_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceZEROAmount-> " + ex.ToString());
                    }
                    return 1;
                }
                public void SetBalanceSheetGroupFormat(string Comp_Code)
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = null;

                    //Add Report Format 
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CODE", Comp_Code);
                    objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_TB_GROUP", objParams, true);
                }
                public int ResetBalanceSheet()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_RESET_BALANCESHEET", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.ResetBalanceSheet-> " + ex.ToString());
                    }
                    return 1;
                }

                public int DeleteSchedules(Int16 prno, Int16 schedule)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@V_PRNO", prno);
                        objParams[1] = new SqlParameter("@V_SCHEDULE", schedule);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BALANCESHEET_SCHEDULE_DELETE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteSchedules-> " + ex.ToString());
                    }
                    return 1;
                }

                public int OrderTrialBalanceReport()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE", "C");
                        objParams[1] = new SqlParameter("@P_College_Code", "");
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_TRIALBALANCE_ORDER", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.OrderTrialBalanceReport-> " + ex.ToString());
                    }
                    return 1;
                }
                public int OrderBalanceSheetReport()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE", "C");
                        objParams[1] = new SqlParameter("@P_College_Code", "");
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_BALANCESHEET_ORDER", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.OrderTrialBalanceReport-> " + ex.ToString());
                    }
                    return 1;
                }
                public int OrderIncomExpensesReport()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE", "C");
                        objParams[1] = new SqlParameter("@P_College_Code", "");
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_PL_ORDER", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.OrderTrialBalanceReport-> " + ex.ToString());
                    }
                    return 1;
                }
                // MODIFY ON 01-Nov-2014 BY DANISH
                public int OrderDaybookReportFormat()
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_YEAR", "C");
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_DAYBOOK_REPORT_ORDER", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.OrderDaybookReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }

                public int OrderDaybookReportFormat(string FromDate, string ToDate, string Compcode)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", ToDate);
                        objParams[2] = new SqlParameter("@P_YEAR", Compcode);

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_DAYBOOK_REPORT_ORDER", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.OrderDaybookReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }


                //public DataSet GetDayBookRecord(string FromDate, string UptoDate, string code_year, int VOUCHER_NO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[4];

                //        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                //        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                //        objParams[2] = new SqlParameter("@P_TO_DATE", UptoDate);
                //        objParams[3] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_DAYBOOK_RECORD", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetDayBookRecord(string FromDate, string UptoDate, string code_year, int VOUCHER_NO, string V_VCH_TYP)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", UptoDate);
                        objParams[3] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[4] = new SqlParameter("@P_VCH_TYPE", V_VCH_TYP);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_DAYBOOK_RECORD", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetBankRegisterRecord(string FromDate, string UptoDate, string code_year, int VOUCHER_NO, string voucherType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", UptoDate);
                        objParams[3] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[4] = new SqlParameter("@P_VCH_TYPE", voucherType);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_BANK_REGISTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetBankRegisterRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetLedgerTotal(Int16 MGRP_NO, Int16 PRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        //@P_College_Code
                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@P_College_Code", "");

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_RET_LEDGER_TOTAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetLedgerTotal-> " + ex.ToString());
                    }
                    return ds;
                }





                public DataSet GetOpeningBalance(string code, string fromdate, string todate, Int16 partyno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CODE", code);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[3] = new SqlParameter("@P_PARTY_NO", partyno);
                        //@P_COLLEGE_CODE
                        //objParams[4] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ret_closing_balance", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetOpeningBalance-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Nokhlal Kumar to get data for Bank Reconciliation fastly
                public DataSet GetBankReconciledData(string comp_code, int PartyNo, string FromDate, string ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@COMP_CODE", comp_code);
                        objParams[1] = new SqlParameter("@PARTY_NO", PartyNo);
                        objParams[2] = new SqlParameter("@FROM_DATE", FromDate);
                        objParams[3] = new SqlParameter("@TO_DATE", ToDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_BANK_RECONCILIATION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetBankReconciledData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetReconDataVoucherWise(string Comp_Code, int Vch_Sqn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@COMP_CODE", Comp_Code);
                        objParams[1] = new SqlParameter("@VOUCHER_SQN", Vch_Sqn);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_RECONCILIATION_DATA_VCHNO_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetReconDataVoucherWise-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetBankClosingBalance(string code, string todate, Int16 partyno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@fromDate", todate);
                        objParams[1] = new SqlParameter("@party_no", partyno);
                        objParams[2] = new SqlParameter("@P_CODE_YEAR", code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_Get_Bank_opbal", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetOpeningBalance-> " + ex.ToString());
                    }
                    return ds;
                }






                public DataSet SetBalanceSheetFormat(Int16 MGRP_NO, Int16 PRNO, Int16 Position, Int16 Schedule, Int16 HeadNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);
                        objParams[3] = new SqlParameter("@V_SCHEDULE", Schedule);
                        objParams[4] = new SqlParameter("@V_HEADNO", HeadNo);
                        //objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_BALANCESHEET_FORMAT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormat-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SetBalanceSheetAssetsFormat(Int16 MGRP_NO, Int16 PRNO, Int16 Position)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);

                        //objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_BALANCESHEET_FORMAT_ASSETS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormat-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SetBalanceSheetLiabilityFormat(Int16 MGRP_NO, Int16 PRNO, Int16 Position)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);
                        //objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_BALANCESHEET_FORMAT_LIABILITY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormat-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SetPLIncomeFormat(Int16 MGRP_NO, Int16 PRNO, Int16 Position)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);
                        objParams[3] = new SqlParameter("@V_COMPCODE", HttpContext.Current.Session["comp_code"].ToString());
                        //objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_PL_FORMAT_INCOME", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormat-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SetPLexpenseFormat(Int16 MGRP_NO, Int16 PRNO, Int16 Position)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);
                        objParams[3] = new SqlParameter("@V_COMPCODE", HttpContext.Current.Session["comp_code"].ToString());
                        //objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_PL_FORMAT_EXPENSE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormat-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet SetBalanceSheetFormatForPLAccount(Int16 MGRP_NO, Int16 PRNO, Int16 Position, Int16 Schedule, string isplac, double opening, double closing, double dr, double cr)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@V_PRNO", PRNO);
                        objParams[2] = new SqlParameter("@V_POSITION", Position);
                        objParams[3] = new SqlParameter("@V_SCHEDULE", Schedule);

                        objParams[4] = new SqlParameter("@V_OP", opening);
                        objParams[5] = new SqlParameter("@V_CL", closing);
                        objParams[6] = new SqlParameter("@V_DR", dr);
                        objParams[7] = new SqlParameter("@V_CR", cr);
                        //objParams[8] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["DataBase"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SET_BALANCESHEET_FORMAT_PL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.SetBalanceSheetFormatForPLAccount-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMainLedgerTotal(Int16 MGRP_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_RET_MAIN_LEDGER_TOTAL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetMainLedgerTotal-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMainLedgerTotalRemaining(Int16 MGRP_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@V_MGRPNO", MGRP_NO);
                        objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_RET_MAIN_LEDGER_TOTAL_REMAINING", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetMainLedgerTotal-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet ArrangeTrialBalanceReport()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_TRIAL_BALANCE_GROUP_ARRANGE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.ArrangeTrialBalanceReport-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet ArrangeBalanceSheet(string code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE", code);
                        //objParams[1] = new SqlParameter("@P_College_Code", Convert.ToString(HttpContext.Current.Session["DataBase"].ToString()));

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ARRANGE_BALANCESHEET_FORMAT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.ArrangeBalanceSheet-> " + ex.ToString());
                    }
                    return ds;

                }

                //public DataSet ArrangeBalanceSheet(string code)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_CODE", code);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ARRANGE_BALANCESHEET_FORMAT", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.ArrangeBalanceSheet-> " + ex.ToString());
                //    }
                //    return ds;

                //}


                public DataSet ArrangeIncomeExpenditureReport(string code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE", code);
                        // objParams[1] = new SqlParameter("@P_colcode", col_code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ARRANGE_INCOMEEXPENDITURE_FORMAT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.ArrangeIncomeExpenditureReport-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet CallBalanceSheetFormatProc(string isNetSurplus, string isBalanceSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IsNetSurplus", isNetSurplus);
                        //objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        if (isBalanceSheet == "Y")
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_CREATE_BALANCESHEET_FORMAT", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_CREATE_INCOMEEXPENDITURE_FORMAT", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.CallBalanceSheetFormatProc-> " + ex.ToString());
                    }
                    return ds;

                }


                public DataSet GetDistinctMGRPNO()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_TRIAL_BALANCE_DISTINCT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDistinctMGRPNO-> " + ex.ToString());
                    }
                    return ds;

                }

                public int DeleteTrialBalanceReportFormat(string CodeYear)
                {
                    try
                    {
                        //@P_College_Code
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_TRIALBALANCE_REPORTFORMAT_DELETE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }

                public int DeleteBalanceSheetReportFormat(string CodeYear)
                {
                    try
                    {
                        //@P_College_Code
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_BALANCESHEET_REPORTFORMAT_DELETE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public int DeletePLReportFormat(string CodeYear)
                {
                    try
                    {
                        //@P_College_Code
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add Report Format 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_PL_REPORTFORMAT_DELETE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.DeleteTrialBalanceReportFormat-> " + ex.ToString());
                    }
                    return 1;
                }
                public DataSet GetDistinctVoucherNo(string FromDate, string UptoDate, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", UptoDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_DISTINCT_VCHNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDistinctVoucherNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDistinctVoucherNoBankRegister(string FromDate, string UptoDate, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", UptoDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_DISTINCT_VCHNO_BANK_REGISTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDistinctVoucherNoBankRegister-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetProfitLossOpeningClosingBalance(string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_PROFITLOSS_OPCL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossOpeningClosingBalance-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetProfitLossOpeningClosingBalance(string code_year, string FromDate, string ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[2] = new SqlParameter("@P_UPTODATE", ToDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_RET_PROFIT_LOSS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossOpeningClosingBalance-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetProfitLossDrCr(string FromDate, string UptoDate, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TODATE", UptoDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_PROFITLOSS_DRCR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossDrCr-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GenerateLedgerList(string CodeYear)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_LEDGER_LIST", objParams, true);
                        //'@P_College_COde


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateLedgerList-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GenerateTrialBalance(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_TRIAL_BALANCE", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GenerateTrialBalance_DateWise(string CodeYear, string FromDate, string ToDate, int withZero)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);
                        objParams[3] = new SqlParameter("@P_WITHZERO", withZero);

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_TRIAL_BALANCE_V1", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GenerateBalanceSheet_Asstes(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_BALANCESHEET_ASSETS", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GenerateBalanceSheet_Liability(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_BALANCESHEET_LIABILITY", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GeneratePL_Income(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_PL_INCOME", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GeneratePL_Expenses(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_PL_EXPENSES", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }
                public int GenerateTrialBalance_DateWise_FinancialYR(string CodeYear, string FromDate, string ToDate)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_LEDGER_CLOSING_FINANCIAL_YEAR_END", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }

                public int INSERT_PROFIT_LOSS(string CodeYear, string FromDate, string ToDate)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REPORT_TRIAL_BALANCE_NEW_PROFIT_AND_LOSS", objParams, true);


                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GenerateTrialBalance-> " + ex.ToString());
                    }
                    return 1;
                }

                public int setTrialBalanceReport(string Position, string MGRP_NO, string CompCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPCODE", CompCode);
                        objParams[1] = new SqlParameter("@P_MGRP_NO", MGRP_NO);
                        objParams[2] = new SqlParameter("@P_Position", Position);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SET_POSITION_FOR_TRIAL_BALANCE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int setGroupingBalanceSheet(string Position, string MGRP_NO, string CompCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPCODE", CompCode);
                        objParams[1] = new SqlParameter("@P_MGRP_NO", MGRP_NO);
                        objParams[2] = new SqlParameter("@P_Position", Position);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SET_GROUPING_FOR_BALANCESHEET", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetDayBookRecordforExcell()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_ACC_DAY_BOOK_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                //Getting the Expense Data for Income Expense Report
                public DataSet GetExpenseExcell(string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_consolidate", "Y");

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_EXPENSE_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                //Getting the Income Data for Income Expense Report
                public DataSet GetIncomeExcell(string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_consolidate", "Y");

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_INCOME_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                //Getting the Liability Data for BalanceSheet Report
                public DataSet GetLiabilitiesExcell(string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_consolidate", "N");

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_LIABILITY_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                //Getting the Assets Data for BalanceSheet Report
                public DataSet GetAssetsExcell(string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_consolidate", "N");

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_ASSETS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }

                //Addded to get Data of Ledgers for Export to Excel
                public DataSet GetLedgerData(string FromDate, string UptoDate, string code_year, string Ledger, string CollegeCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_LEDGER", Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[3] = new SqlParameter("@P_TODATE", UptoDate);
                        objParams[4] = new SqlParameter("@P_College_Code", CollegeCode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossDrCr-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet BindIncExpBalsheetGrid(string code_year, string mgrpno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COMPCODE", code_year);
                        objParams[1] = new SqlParameter("@P_MGRP_NO", mgrpno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GROUP_REPORT_TRIAL_BALANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetDayBookRecord-> " + ex.ToString());
                    }
                    return ds;
                }


                //Addded to get Data of Ledgers for Export to Excel
                public DataSet GetDatawithoutLedger(string FromDate, string UptoDate, string code_year, string Ledger, string CollegeCode,string party_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_LEDGER", Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[3] = new SqlParameter("@P_TODATE", UptoDate);
                        objParams[4] = new SqlParameter("@P_College_Code", CollegeCode);
                        objParams[5] = new SqlParameter("@P_Partyno", party_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK_WITHOUT_PARTY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TrialBalanceReportController.GetProfitLossDrCr-> " + ex.ToString());
                    }
                    return ds;
                }

            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS