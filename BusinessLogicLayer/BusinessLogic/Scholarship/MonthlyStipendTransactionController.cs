//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SCHOLARSHIP                                                           
// PAGE NAME     : SCHOLAERSHIP CONTROLLER                                              
// CREATION DATE : 14-07-2015                                                         
// CREATED BY    : DIGEHS PATEL                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================



using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using BusinessLogicLayer.BusinessEntities.SCHOLARSHIP;



namespace BusinessLogicLayer.BusinessEntities
{

    public class MonthlyStipendTransactionController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region  ScholarshipEntry

        public int AddMonthlyStipendTransactionEntry(MonthlyStipendTransaction addobj)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNO", addobj.IDNO);
                        objParams[1] = new SqlParameter("@P_TRANDT", addobj.TranDate);
                        objParams[2] = new SqlParameter("@P_TRANMONTHFROM", addobj.TranmonthFrom);
                        objParams[3] = new SqlParameter("@P_MONNAME", addobj.MonthName);
                        objParams[4] = new SqlParameter("@P_TRANAMT", addobj.TranAmt);
                        objParams[5] = new SqlParameter("@P_TRANST", addobj.TranST);
                        objParams[6] = new SqlParameter("@P_REMARK", addobj.Remark);
                        objParams[7] = new SqlParameter("@P_TRANMONTHTO", addobj.TranmonthTo);
                        objParams[8] = new SqlParameter("@P_ARREARAMT", "");
                        objParams[9] = new SqlParameter("@P_ARREARDATE_FROM", "");
                        objParams[10] = new SqlParameter("@p_ARREARDATE_TO", "");
                        objParams[11] = new SqlParameter("@P_ARREAR_REMARK", "");
                        objParams[12] = new SqlParameter("@P_QUERYTYPE", 1);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("ACD_STIPEND_TRAN_INS_UPDATE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipEntry-> " + ex.ToString());
                }

                return retStatus;

            }
        }

        public int AddMonthlyStipendArrTransactionEntry(MonthlyStipendTransaction addobj)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TRANNO",addobj.TRANNO );
                        objParams[1] = new SqlParameter("@P_ARREARAMT",addobj.ArrearAmt );
                        objParams[2] = new SqlParameter("@P_ARREARDATE_FROM",addobj.ArrearFromDate );
                        objParams[3] = new SqlParameter("@p_ARREARDATE_TO",addobj.ArrearToDate );
                        objParams[4] = new SqlParameter("@P_ARREAR_REMARK","");
                        objParams[5] = new SqlParameter("@P_QUERYTYPE",addobj.QueryType);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("ACD_STIPEND_ARR_TRAN_UPDATE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipEntry-> " + ex.ToString());
                }

                return retStatus;

            }
        }

        public int DeleteMonthlyStipendTransactionEntry(MonthlyStipendTransaction addobj)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TRANNOS", addobj.TRANNOSTR);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STIPEND_TRAN_DELETE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipEntry-> " + ex.ToString());
                }

                return retStatus;

            }
        }

        public DataSet GetStipendStudent(int branchno, int degreeno, int admbatch, string todate, string fromdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                {
                    objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                    objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                    objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);
                    objParams[3] = new SqlParameter("@P_TODATE", todate);
                    objParams[4] = new SqlParameter("@P_FROMDATE", fromdate);
                }
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHOLAERSHIP_MONTHLYSTIPEND", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MonthlyStipendTransactionController.GetStipendStudent-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetStipendArrStudent(int branchno, int degreeno, int admbatch, string todate, string fromdate, string trandate, string arrtodate, string arrfromdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                {
                    objParams = new SqlParameter[8];
                    objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                    objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                    objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);
                    objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                    objParams[4] = new SqlParameter("@P_TODATE", todate);
                    objParams[5] = new SqlParameter("@P_TRANDATE", trandate);
                    objParams[6] = new SqlParameter("@P_ARREARSFROMDATE", arrfromdate);
                    objParams[7] = new SqlParameter("@P_ARREARSTODATE", arrtodate);
            
                }
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STIPEND_ARREARS_DETAIL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MonthlyStipendTransactionController.GetStipendStudent-> " + ex.ToString());
            }
            return ds;
        }
        # endregion

        public CustomStatus AddMonthlyStipendTransactionentry(StudentScholarshipEntry objFN)
        {
            throw new NotImplementedException();
        }
    }
}
