//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[SupplimentaryBill_Controller]                                  
// CREATION DATE : 02-NOV-2009                                                        
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
            public class Pay_UpdatePayHeadEmpWiseController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllEarningHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_PAY_EARNING_HEADS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllPayHeads() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllDeductionHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_PAY_EARNING_DEDUCTION_HEADS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllDeductionHeads() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GetPayhead(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                         new SqlParameter("@P_IDNO",IDNO),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_HEAD", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                public int UpdatePayHeads(int idNo, string Payhead, string PayHeadValue)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateOverRule
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {  
                            new SqlParameter("@P_IDNO",idNo),
                            new SqlParameter("@P_PAYHEAD",Payhead),
                            new SqlParameter("@P_PAYHEADVALUES",PayHeadValue)                                                                                                                
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_PAYHEADS", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayOverRuleController.PAYROLL_UPDATE_PAYHEADS -> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}