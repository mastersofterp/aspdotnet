//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[EmployeeLockUnlockController]                                  
// CREATION DATE : 30-MAY-2016                                                       
// CREATED BY    : SURAJ CHOUDHARI                         
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
            public class EmployeeLockUnlockController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetEmployeesForLockUnlock(int Staff, string order, string Dept, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_Staff", Staff);
                        objParams[1] = new SqlParameter("@P_ORDERBY", order);
                        objParams[2] = new SqlParameter("@P_Dept", Dept);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_LOCK_UNLOCK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateEmployeeLockUnlock(bool emplockunlock, int idNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPLOYEE_LOCK", emplockunlock);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_EMPLOYEE_LOCK_UNLOCK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
