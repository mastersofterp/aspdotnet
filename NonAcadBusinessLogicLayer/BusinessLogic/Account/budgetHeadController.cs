using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class budgetHeadController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                // private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public budgetHeadController()
                {
                }
                public budgetHeadController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddUpdateBudgetName(budgetHead objbud, string code_year, int DeptNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_BUDG_NO", objbud.BUDG_NO);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_BUDG_CODE", objbud.BUDG_CODE);
                        objParams[3] = new SqlParameter("@P_BUDG_NAME", objbud.BUDG_NAME);
                        objParams[4] = new SqlParameter("@P_BUDG_PRNO", objbud.BUDG_PRNO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objbud.COLLEGE_CODE);
                        //objParams[6] = new SqlParameter("@P_BUD_DEPT", DeptNo);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_BUDGET", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.budgetHeadController.AddUpdateBudgetName-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet FetchBudgetHead(string code_year)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_FECTH_BUDGET_HEIRARCHICAL", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                public DataSet FetchDepartment(string Connection)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(Connection);
                        ds = objSQLHelper.ExecuteDataSet("SELECT SUBDEPTNO, SUBDEPT FROM PAYROLL_SUBDEPT");
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                public int BudgetAllocation(DataTable dt, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACC_BUDGET_ALLOCATION", dt);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_OUT", 0);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BUDGET_ALLOCATION", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.budgetHeadController.AddUpdateBudgetName-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet FetchBudgetHeadAmount(string code_year, int bud_amt)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COMP_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_BUDG_PRNO", bud_amt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BUDGET_AMT_DATA", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }
            }
        }
    }
}
