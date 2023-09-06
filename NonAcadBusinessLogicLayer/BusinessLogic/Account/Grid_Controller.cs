using System;
using System.Text;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Grid_Controller
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public Grid_Controller()
                {
                }
                public Grid_Controller(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public DataSet LedgerReport(Grid_Entity gridEntity)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", gridEntity.CompCode);
                        objParams[1] = new SqlParameter("@P_LEDGER", gridEntity.Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", gridEntity.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[3] = new SqlParameter("@P_TODATE", gridEntity.ToDate.ToString("dd-MMM-yyyy"));
                        objParams[4] = new SqlParameter("@P_College_Code", "15");
                        dsReport = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK", objParams);
                        

                    }
                    catch (Exception ex)
                    {
                       
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }

                public DataSet LedgerReportvOUCHERNO(Grid_Entity gridEntity)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", gridEntity.CompCode);
                        objParams[1] = new SqlParameter("@P_LEDGER", gridEntity.Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", gridEntity.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[3] = new SqlParameter("@P_TODATE", gridEntity.ToDate.ToString("dd-MMM-yyyy"));
                        objParams[4] = new SqlParameter("@P_College_Code", "15");
                        dsReport = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK_FOR_GRID", objParams);


                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }

                /// <summary>
                /// Added Mahesh Dt.18/08/2017
                /// </summary>
                /// <param name="gridEntity"></param>
                /// <returns></returns>

                public DataSet NarrationWithLedger(Grid_Entity gridEntity)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@CollegeCode", gridEntity.CompCode);
                        objParams[1] = new SqlParameter("@Particulars", gridEntity.Ledger);
                        objParams[2] = new SqlParameter("@FromDate", gridEntity.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[3] = new SqlParameter("@ToDate", gridEntity.ToDate.ToString("dd-MMM-yyyy"));
                        dsReport = objSQLHelper.ExecuteDataSetSP("ACC_GET_LEDGER_DATA_BY_NARATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }




                public DataSet LedgerReportVoucherNoCashBank(Grid_Entity gridEntity)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", gridEntity.CompCode);
                        objParams[1] = new SqlParameter("@P_LEDGER", gridEntity.Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", gridEntity.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[3] = new SqlParameter("@P_TODATE", gridEntity.ToDate.ToString("dd-MMM-yyyy"));
                        objParams[4] = new SqlParameter("@P_College_Code", "15");
                        dsReport = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK_CONDENSED_NEW_FOR_GRID", objParams);


                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }




                public DataSet LedgerReportForCashBank(Grid_Entity gridEntity)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", gridEntity.CompCode);
                        objParams[1] = new SqlParameter("@P_LEDGER", gridEntity.Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", gridEntity.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[3] = new SqlParameter("@P_TODATE", gridEntity.ToDate.ToString("dd-MMM-yyyy"));
                        objParams[4] = new SqlParameter("@P_College_Code", "15");
                        dsReport = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK_CONDENSED_NEW", objParams);


                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }


                /// Purpose : Auto-complete Method for Item. 
                /// FUNCTION NAME: fillItem
                /// </summary>
                /// <returns></returns>
                public DataSet fillItem(string Narration)
                {
                    DataSet ds = null;
                    string UserName = string.Empty;
                    int i;
                    int j;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR",HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@Narration", Narration);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_ACC_NARRATION_AUTOCOMPLETE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet LedgerReportBalnces(string PaymentTypeNo,string codeyear,string partyno,string from)
                {
                    DataSet dsReport = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PAYMENT_TYPE_NO", PaymentTypeNo);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", codeyear);
                        objParams[2] = new SqlParameter("@party_no", partyno);
                        objParams[3] = new SqlParameter("@P_FROMDATE", from);
                        dsReport = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BALANCE_FOR_GRID", objParams);


                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return dsReport;
                }
            }
        }
    }
}
