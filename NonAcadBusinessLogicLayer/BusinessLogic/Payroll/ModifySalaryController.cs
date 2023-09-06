//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ModifySalaryController]                                  
// CREATION DATE : 26-JULY-2009                                                        
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
            public class ModifySalaryController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetEmpMonthFile(string payHead,int collegeNo, int staff, string monYear,int EmpType, string orderby)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", payHead);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF", staff);
                        objParams[3] = new SqlParameter("@P_MONYEAR", monYear);
                        objParams[4] = new SqlParameter("@P_EMPTYPENO", EmpType);
                        objParams[5] = new SqlParameter("@P_ORDERBY", orderby);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_MONFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModifySalaryController.GetEmpMonthFile-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateSalaryMonFile(string payHead, decimal amount,int idNo,string mDate)
                {
                    int retStatus = 0;
                    try
                    {   

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PayHead",payHead);
                        objParams[1] = new SqlParameter("@P_PayHead_val",amount);
                        objParams[2] = new SqlParameter("@P_IDNO",idNo);
                        objParams[3] = new SqlParameter("@P_DATE", mDate);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SALARY_MONFILE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ModifySalaryController.UpdateSalaryMonFile-> " + ex.ToString());
                    }
                    return retStatus;
                }

            }
        }
    }
}