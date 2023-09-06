using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class STR_BudgetController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region STORE_BUDGETHEAD
                    public int AddBudgetHead(Str_Budget objBudget)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            //Add New BUDGETHEAD
                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_HEADNAME",objBudget.HEADNAME);
                            objParams[1] = new SqlParameter("@P_AMOUNT", objBudget.AMOUNT);
                            objParams[2] = new SqlParameter("@P_SDATE", objBudget.SDATE);
                            objParams[3] = new SqlParameter("@P_ENDDATE", objBudget.ENDDATE);
                            objParams[4] = new SqlParameter("@P_NATURE", objBudget.NATURE);
                            objParams[5] = new SqlParameter("@P_SCHEME", objBudget.SCHEME);
                            objParams[6] = new SqlParameter("@P_COORDINATOR", objBudget.COORDINATOR);
                            objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objBudget.COLLEGE_CODE);
                            objParams[8] = new SqlParameter("@P_HNO", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDHEAD_INSERT", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.AddBudgetHead-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                    public int UpdateBudgetHead(Str_Budget objBudget)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            //Add New UPDATEBUDGETHEAD
                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_HNO", objBudget.HNO);
                            objParams[1] = new SqlParameter("@P_HEADNAME", objBudget.HEADNAME);
                            objParams[2] = new SqlParameter("@P_AMOUNT", objBudget.AMOUNT);
                            objParams[3] = new SqlParameter("@P_SDATE", objBudget.SDATE);
                            objParams[4] = new SqlParameter("@P_ENDDATE", objBudget.ENDDATE);
                            objParams[5] = new SqlParameter("@P_NATURE", objBudget.NATURE);
                            objParams[6] = new SqlParameter("@P_SCHEME", objBudget.SCHEME);
                            objParams[7] = new SqlParameter("@P_COORDINATOR", objBudget.COORDINATOR);
                            objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objBudget.COLLEGE_CODE);                       
                            if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDHEAD_UPDATE", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.UpdateBudgetHead-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                    public DataSet GetAllBudgetHead()
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[0];
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDHEAD_GET_ALL", objParams);

                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.GetAllBudgetHead-> " + ex.ToString());
                        }
                        return ds;
                    }

                    public DataSet GetSingleRecordBudgetHead(int hNo)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null; ;
                            objParams = new SqlParameter[1];
                            objParams[0] = new SqlParameter("@P_HNO", hNo);
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDHEAD_GET_BY_NO", objParams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.GetSingleRecordBudgetHead-> " + ex.ToString());
                        }
                        return ds;
                    }

                #endregion

                #region STORE_BUDHEAD
                public int AddBudget(Str_Budget objBudget)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_HNO", objBudget.HNO);
                        objParams[1] = new SqlParameter("@P_DCODE", objBudget.DCODE);
                        objParams[2] = new SqlParameter("@P_AMT", objBudget.AMT);
                        objParams[3] = new SqlParameter("@P_SDATE", objBudget.SDATE);
                        objParams[4] = new SqlParameter("@P_EDATE", objBudget.EDATE);
                        objParams[5] = new SqlParameter("@P_BALAMT", objBudget.BALAMT);                      
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE",objBudget.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_BUDNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGET_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.AddBudget-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateBudget(Str_Budget objBudget)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New UpdateBudget
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_BUDNO", objBudget.BUDNO);
                        objParams[1] = new SqlParameter("@P_HNO", objBudget.HNO);
                        objParams[2] = new SqlParameter("@P_DCODE", objBudget.DCODE);
                        objParams[3] = new SqlParameter("@P_AMT", objBudget.AMT);
                        objParams[4] = new SqlParameter("@P_SDATE", objBudget.SDATE);
                        objParams[5] = new SqlParameter("@P_EDATE", objBudget.EDATE);
                        objParams[6] = new SqlParameter("@P_BALAMT", objBudget.BALAMT);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objBudget.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGET_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.UpdateBudget-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllBudget()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDGET_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.GetAllBudget-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordBudget(int bugNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BUDNO", bugNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDGET_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_BudgetController.GetSingleRecordBudget-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

            }
        }
    }
}
