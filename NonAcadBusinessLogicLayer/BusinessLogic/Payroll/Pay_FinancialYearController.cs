using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Pay_FinancialYearController
            {

                /// <SUMMARY>
                /// ConnectionStrings 
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                // To Insert New PKG_PAY_FINANCIAL_YEAR_INSERT
                public int AddFinancialYear(Pay_FinancialYear objfinancialYear)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FROMYEAR", objfinancialYear.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TOYEAR", objfinancialYear.TODATE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objfinancialYear.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_FINANCIAL_YEAR", objfinancialYear.FINANCIAL_YEAR);
                        objParams[4] = new SqlParameter("@P_SHORT_NAME", objfinancialYear.SHORT_NAME);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_FINANCIAL_YEAR_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FinancialYearController.AddFinancialYear->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To Update PKG_PAY_FINANCIAL_YEAR_UPDATE
                public int UpdateFinancialYear(Pay_FinancialYear objfinancialYear)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMYEAR", objfinancialYear.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TOYEAR", objfinancialYear.TODATE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objfinancialYear.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_FINANCIAL_YEAR", objfinancialYear.FINANCIAL_YEAR);
                        objParams[4] = new SqlParameter("@P_SHORT_NAME", objfinancialYear.SHORT_NAME);
                        objParams[5] = new SqlParameter("@P_FINYEARID", objfinancialYear.FINYEARID);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_FINANCIAL_YEAR_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FinancialYearController.AddFinancialYear->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To   SELECT ID wise Details
                public DataSet GetFanancialYearID(int FINYEARID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_FINYEARID", FINYEARID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_FINANCIAL_YEAR_SELECT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FinancialYearController.GetFanancialYearID->->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To   SELECT All Financial year details
                public DataSet GetAllFinancialYearDetails(string COLLEGE_CODE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_FINANCIAL_YEAR_SELECT_ALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Pay_FinanialYearController.GetAllFinancialYearDetails->->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
