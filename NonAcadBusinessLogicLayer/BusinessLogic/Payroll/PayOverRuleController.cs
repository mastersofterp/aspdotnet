//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [PAY ROLL]                                  
// CREATION DATE : 01-MAY-2009                                                        
// CREATED BY    : KIRAN GVS                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
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
            public class PayOverRuleController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddOverRule(int idNo, DateTime fromDt, DateTime toDt, char status, string remark, string collegeCode, string payHeadValues)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddOverRule
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO",idNo),
                            new SqlParameter("@P_FROMDT",fromDt),
                            new SqlParameter("@P_TODT",toDt),
                            new SqlParameter("@P_STATUS",status),
                            new SqlParameter("@P_REMARK",remark),
                            new SqlParameter("@P_COLLEGE_CODE",collegeCode),
                            new SqlParameter("@P_PAYHEADVALUES",payHeadValues)
                         
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_OVERRULE", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayOverRuleController.AddOverRule -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateOverRule(int idNo, DateTime fromDt, DateTime toDt,char status,string remark, string collegeCode, string payHeadValues )
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateOverRule
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {  
                            new SqlParameter("@P_IDNO",idNo),
                            new SqlParameter("@P_FROMDT",fromDt),
                            new SqlParameter("@P_TODT",toDt),
                            new SqlParameter("@P_STATUS",status),  
                            new SqlParameter("@P_REMARK",remark),
                            new SqlParameter("@P_COLLEGE_CODE",collegeCode),
                            new SqlParameter("@P_PAYHEADVALUES",payHeadValues)                                                                                      
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_OVERRULE", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayOverRuleController.UpdateOverRule -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllOverRule()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_SUPLIMENTARY_BILL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayOverRuleController.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSingleOverRule(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                         new SqlParameter("@P_IDNO",idNo),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_OVERRULE", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayOverRuleController.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DateTime GetMonthLastDate(DateTime date)
                {
                    DateTime dt;
                   
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParam = new SqlParameter[]
                        { 
                         new SqlParameter("@P_Date",date),
                        };
                        sqlParam[sqlParam.Length-1].Direction = ParameterDirection.InputOutput;

                        dt = Convert.ToDateTime(objSQLHelper.ExecuteNonQuerySP("PKG_GETLASTDATEOFMONTH", sqlParam, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayOverRuleController.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                  
                    return  dt;
                }

            }            
        }
    }
}
