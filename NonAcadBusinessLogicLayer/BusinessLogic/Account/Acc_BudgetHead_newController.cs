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
            public class Acc_BudgetHead_newController
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            
                public Acc_BudgetHead_newController()
                {
                }
                public Acc_BudgetHead_newController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddUpd_BudgetHead(BudgetHeadEntity objEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams=new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_BUDGET_NO", objEnt.BUDGET_NO);
                        objParams[1]=new SqlParameter("@P_BUDGET_CODE",objEnt.BUDGET_CODE);
                        objParams[2]=new SqlParameter("@P_BUDGET_HEAD",objEnt.BUDGET_HEAD);
                        objParams[3]=new SqlParameter("@P_PARENT_ID",objEnt.PARENT_ID);
                        objParams[4]=new SqlParameter("@P_SERIAL_NO",objEnt.SERIAL_NO);
                        objParams[5]=new SqlParameter("@P_BUDGET_PRAPOSAL",objEnt.BUDGET_PRAPOSAL);
                        objParams[6]=new SqlParameter("@P_COLLEGE_CODE",objEnt.COLLEGE_CODE);
                        objParams[7]=new SqlParameter("@P_CREATED_BY",objEnt.CREATED_BY);
                        objParams[8] = new SqlParameter("@P_RECURRING_NONREC", objEnt.RECURRING);
                        objParams[9] = new SqlParameter("@P_PARTYNO", objEnt.PARTYNO);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BUDGET_HEAD_INSERT_UPDATE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("3"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                             else if(ret.ToString().Equals("1"))
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.AddMainGroup-> " + ex.ToString());
                    }
                    return retStatus;
                  
                }
                public DataSet GET_BUDGETHEAD_BY_BUDGET_NO(BudgetHeadEntity objEnt , string Company_Code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_BUDGET_NO", objEnt.BUDGET_NO);
                        objParams[1] = new SqlParameter("@P_COMPANY_CODE", Company_Code);   // Added by Akshay on 09-05-2022
                   

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_BUDGETHEAD_BY_BUDGETNO", objParams);
                    }
                    catch (Exception ex)
                    {
                     
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.AddMainGroup-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet BIND_DROPDOWN()
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_DROPDOWN_FRM_BUDGETHEAD", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.AddMainGroup-> " + ex.ToString());
                    }
                    return ds;
                }
        }
    }
}
}


