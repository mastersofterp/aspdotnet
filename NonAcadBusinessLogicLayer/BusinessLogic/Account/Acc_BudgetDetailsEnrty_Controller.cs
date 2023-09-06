//=================================================================================
// PROJECT NAME  : UAIMS - RFC-SVCEC                                                          
// MODULE NAME   :BUDGET_DETAILS_ENTRY                                                    
// CREATION DATE : 20-OCT-2019                                               
// CREATED BY    : ANDOJU VIJAY                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================



using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Acc_BudgetDetailsEnrty_Controller
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public Acc_BudgetDetailsEnrty_Controller()
                {
                }
                public Acc_BudgetDetailsEnrty_Controller(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }
                public DataSet Get_Data_By_Deptid(Acc_BudgetDetailsEnrty_Entity ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    DataSet ds = new DataSet();
                    try
                    {

                        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] Objparam = new SqlParameter[4];
                        Objparam[0] = new SqlParameter("@P_DEPT_ID", ObjEnt.Dept_id);
                        Objparam[1] = new SqlParameter("@P_PARENT_ID", ObjEnt.Parent_id);
                        Objparam[2] = new SqlParameter("@P_FROM_DATE", ObjEnt.FROM_DATE);
                        Objparam[3] = new SqlParameter("@P_TO_DATE", ObjEnt.TO_DATE);
                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_ACC_BIND_BUDGETDATA_BYDEPT_ID", Objparam);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_BudgetDetailsEnrty_Controller " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet Get_Data_By_Deptid_ForEnt(Acc_BudgetDetailsEnrty_Entity ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    DataSet ds = new DataSet();
                    try
                    {

                        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] Objparam = new SqlParameter[4];
                        Objparam[0] = new SqlParameter("@P_DEPT_ID", ObjEnt.Dept_id);
                        Objparam[1] = new SqlParameter("@P_PARENT_ID", ObjEnt.Parent_id);
                        Objparam[2] = new SqlParameter("@P_FROM_DATE", ObjEnt.FROM_DATE);
                        Objparam[3] = new SqlParameter("@P_TO_DATE", ObjEnt.TO_DATE);

                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_ACC_BUDGET_ETRY_BIND_BUDGETDATA_BYDEPT_ID", Objparam);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_BudgetDetailsEnrty_Controller " + ex.ToString());
                    }
                    return ds;
                }

                public int Ins_Upd_BudgetDetailsDep(Acc_BudgetDetailsEnrty_Entity ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] Objparam = new SqlParameter[8];
                        Objparam[0] = new SqlParameter("@P_BUDGETALLOC_ID", ObjEnt.BUDGETALLOCTION_ID);
                        Objparam[1] = new SqlParameter("@P_DEPT_ID", ObjEnt.Dept_id);
                        Objparam[2] = new SqlParameter("@P_FROM_DATE", ObjEnt.FROM_DATE);
                        Objparam[3] = new SqlParameter("@P_TO_DATE", ObjEnt.TO_DATE);
                        Objparam[4] = new SqlParameter("@P_CREATED_BY", ObjEnt.CREATED_BY);
                        Objparam[5] = new SqlParameter("@P_COLLEGE_ID", ObjEnt.COLLEGE_ID);
                        Objparam[6] = new SqlParameter("@P_ACC_BUDGET_ALLOC_DETAILS", ObjEnt.Budgettable);
                        Objparam[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        Objparam[7].Direction = ParameterDirection.Output;
                        //object ret = ObjSqlHelper.ExecuteNonQuerySP("PKG_INS_UPD_BUDGET_ALLOCATION_DETAILS_NEW", Objparam, true);
                        object ret = ObjSqlHelper.ExecuteNonQuerySP("PKG_ACC_INS_UPD_BUDGET_ALLOCATION_DETAILS_NEW", Objparam, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            if (ret.ToString().Equals("2"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            if (ret.ToString().Equals("3"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_BudgetDetailsEnrty_Controller " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ApproveBudgetAmount(Acc_BudgetDetailsEnrty_Entity ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] Objparams = new SqlParameter[4];
                        Objparams[0] = new SqlParameter("@P_BUDGETALLOC_ID", ObjEnt.BUDGETALLOCTION_ID);
                        Objparams[1] = new SqlParameter("@P_DEPT_ID", ObjEnt.Dept_id);
                        Objparams[2] = new SqlParameter("@P_ACC_BUDGET_APPROVE", ObjEnt.BudgetApproveTable);
                        Objparams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        Objparams[3].Direction = ParameterDirection.Output;
                        object ret = ObjSqlHelper.ExecuteNonQuerySP("PKG_ACC_INS_UPD_BUDGET_APPROVED_AMOUNT_NEW", Objparams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_BudgetDetailsEnrty_Controller " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
