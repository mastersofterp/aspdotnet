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
            /// <summary>
            /// This Controller is use in Income Expenditure and Balance Sheet.
            /// </summary>
            public class IncomeExpenBalanceSheetController
            {
                IncomeExpenBalanceSheet IESController = new IncomeExpenBalanceSheet();
                public string _client_constr = string.Empty;
               string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               public IncomeExpenBalanceSheetController()
               {
               }


               public IncomeExpenBalanceSheetController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddShedule(IncomeExpenBalanceSheet objIES,string compCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[1] = new SqlParameter("@P_SCH_NAME", objIES.SchduleName);
                        objParams[2] = new SqlParameter("@P_SCH_TYPE", objIES.Schtype);
                        objParams[3] = new SqlParameter("@P_Sch_ID", objIES.Sch_id);

                        objParams[4] = new SqlParameter("@P_BS_IE", objIES.BS_IE);


                        objParams[5] = new SqlParameter("@P_USER", objIES.User_Id);


                        objParams[6] = new SqlParameter("@P_FIN_YR", objIES.FinancialYr);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SCH_MASTER_INSERT", objParams, true);
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
                    catch (Exception Ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + Ex.ToString());
                    }
                    return retStatus;
                }

                public void DeleteLedger(int Sch_id, string compCode)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        int i = objSQLHelper.ExecuteNonQuery("DELETE from ACC_" + compCode + "_Sheduled_Ledger where Sch_ID='" + Sch_id + "'");
                    }
                    catch(Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString()); 
                    }
                    
                }

                public int AddLedgerForSchedule(IncomeExpenBalanceSheet objIES, string compCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);

                        //int i = objSQLHelper.ExecuteNonQuery("DELETE from ACC_" + compCode + "_Sheduled_Ledger where Sch_ID='" + objIES.Sch_id + "'");

                        
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_COMPCODE", compCode);
                            objParams[1] = new SqlParameter("@P_SchID", objIES.Sch_id);
                            objParams[2] = new SqlParameter("@P_LedgerName", objIES.LedgerName);
                            objParams[3] = new SqlParameter("@P_Party_No", objIES.Party_NO);
                            objParams[4] = new SqlParameter("@P_FinYear", objIES.FinancialYr);
                            objParams[5] = new SqlParameter("@P_UserID", objIES.User_Id);

                            objParams[6] = new SqlParameter("@P_CollegeCode", objIES.College_Code);
                            objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SCH_LEDGER_INSERT", objParams, true);
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

                public int setReport(IncomeExpenBalanceSheet objIEBS,string CompCode,string Amount)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMPCODE", CompCode);
                        objParams[1] = new SqlParameter("@P_SchID", objIEBS.Sch_id);
                        objParams[2] = new SqlParameter("@P_Position", objIEBS.Position);
                        objParams[3] = new SqlParameter("@P_Amount", Amount);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SET_POSITION", objParams, true);
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

                public int SetAmount(string compcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPCODE", compcode);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_SET_AMOUNT_IEBS", objParams, true);
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
            }
        }
    }
}