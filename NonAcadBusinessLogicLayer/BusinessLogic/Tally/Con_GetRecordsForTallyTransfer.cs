using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Con_GetRecordsForTallyTransfer
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                //comment on 31052017 by nikhild
                //public DataSet GetAllDetails(Ent_GetRecordsForTallyTransfer ObjTTM, string fromdate, string todate)
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                //        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                //        objParams[2] = new SqlParameter("@FromDate", fromdate);
                //        objParams[3] = new SqlParameter("@TODate", todate);
                //        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTotalFeeForTallyIntegration]", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw;
                //    }
                //    return ds;
                //}

                // FOR PAYROLL TALLY TRANSFER
                //public DataSet GetAllDetails(Ent_GetRecordsForTallyTransfer ObjTTM, string fromdate, string todate)
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                //        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                //        objParams[2] = new SqlParameter("@FromDate", fromdate);
                //        objParams[3] = new SqlParameter("@TODate", todate);
                //        objParams[4] = new SqlParameter("@Degreeno", ObjTTM.Degreeno);
                //        objParams[5] = new SqlParameter("@Shiftno", ObjTTM.Shiftno);
                //        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTotalFeeForTallyIntegration]", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw;
                //    }
                //    return ds;
                //}
                public DataSet GetAllDetails(Ent_GetRecordsForTallyTransfer ObjTTM, string fromdate, string todate,string paytype)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[2] = new SqlParameter("@FromDate", fromdate);
                        objParams[3] = new SqlParameter("@TODate", todate);
                        objParams[4] = new SqlParameter("@Degreeno", ObjTTM.Degreeno);
                        objParams[5] = new SqlParameter("@Shiftno", ObjTTM.Shiftno);
                        objParams[6] = new SqlParameter("@Paytype",paytype);
                        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTotalFeeForTallyIntegration]", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }
                public DataSet GetAllPayDetails(Ent_GetRecordsForTallyTransfer ObjTTM, int StaffTypeId, string MonthYear)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@StaffTypeId", StaffTypeId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[2] = new SqlParameter("@MonthYear", MonthYear);
                        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTotalPayForTallyIntegration]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                //Payroll Transfer
                public long AddUpdatePayTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string Paydate, string StaffType, string PayMonth, DataTable dtPay)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@StaffTypeId", StaffType);
                        objParams[1] = new SqlParameter("@PayMonth", PayMonth);
                        objParams[2] = new SqlParameter("@PayDate", Paydate);
                        objParams[3] = new SqlParameter("@dtPay", dtPay);
                        objParams[4] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                        objParams[5] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[6] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                        objParams[7] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
                        objParams[8] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspInsertPayTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }

                //Payroll Transfer
                public long DeleteTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string Paydate, string StaffType, string PayMonth)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@StaffTypeId", StaffType);
                        objParams[1] = new SqlParameter("@PayMonth", PayMonth);
                        objParams[2] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[3] = new SqlParameter("@Rout", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspDeletePayTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }
                public long AddUpdateDCRTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string fromdate, string todate, string paytype)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                        objParams[1] = new SqlParameter("@FromDate", fromdate);
                        objParams[2] = new SqlParameter("@ToDate", todate);
                        objParams[3] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                        objParams[4] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[5] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                        objParams[6] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
                        objParams[7] = new SqlParameter("@Degreeno", ObjTTM.Degreeno);
                        objParams[8] = new SqlParameter("@Shiftno", ObjTTM.Shiftno);
                        objParams[9] = new SqlParameter("@Paytype", paytype);
                        objParams[10] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspInsertDCRTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }


                // FOR PAYROLL TALLY TRANSFER
                //added on 31052017
                //public long AddUpdateDCRTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string fromdate, string todate)
                //{
                //    long pkid = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[10];
                //        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                //        objParams[1] = new SqlParameter("@FromDate", fromdate);
                //        objParams[2] = new SqlParameter("@ToDate", todate);
                //        objParams[3] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                //        objParams[4] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                //        objParams[5] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                //        objParams[6] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
                //        objParams[7] = new SqlParameter("@Degreeno", ObjTTM.Degreeno);
                //        objParams[8] = new SqlParameter("@Shiftno", ObjTTM.Shiftno);
                //        objParams[9] = new SqlParameter("@R_Out", SqlDbType.Int);
                //        objParams[9].Direction = ParameterDirection.Output;


                //        object ret = objSQLHelper.ExecuteNonQuerySP("[UspInsertDCRTallyTransfer]", objParams, true);

                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //            {
                //                Message = "Transaction Failed!";
                //            }
                //            else
                //            {
                //                pkid = Convert.ToInt64(ret.ToString());
                //            }
                //        }
                //        else
                //        {
                //            pkid = -99;
                //            Message = "Transaction Failed!";
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        pkid = -99;
                //        throw;

                //    }
                //    return pkid;
                //}
                public DataSet GetAllSupplementrySalaryDetails(Ent_GetRecordsForTallyTransfer ObjTTM, int StaffTypeId, string MonthYear)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@StaffTypeId", StaffTypeId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[2] = new SqlParameter("@MonthYear", MonthYear);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetTotalPayForSuplementrySalaryTallyIntegration]", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public long AddUpdateSupplePayTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string Paydate, string StaffType, string PayMonth, DataTable dtPay)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@StaffTypeId", StaffType);
                        objParams[1] = new SqlParameter("@PayMonth", PayMonth);
                        objParams[2] = new SqlParameter("@PayDate", Paydate);
                        objParams[3] = new SqlParameter("@dtPay", dtPay);
                        objParams[4] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                        objParams[5] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[6] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                        objParams[7] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
                        objParams[8] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspInsertSupplePayTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }

                public DataSet GetAllDetailsForRefund(Ent_GetRecordsForTallyTransfer ObjTTM, string fromdate, string todate)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[2] = new SqlParameter("@FromDate", fromdate);
                        objParams[3] = new SqlParameter("@TODate", todate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TOTAL_FEES_FOR_REFUND_TO_TALLY_INTEGRATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet AddUpdateDCRTallyRecordsForRefund(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message, string fromdate, string todate)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                        objParams[1] = new SqlParameter("@FromDate", fromdate);
                        objParams[2] = new SqlParameter("@ToDate", todate);
                        objParams[3] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                        objParams[4] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                        objParams[5] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                        objParams[6] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_INSERT_DCR_TALLY_TRANSFER_FOR_REFUND", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw;

                    }
                    return ds;
                }


            }
        }
    }
}
