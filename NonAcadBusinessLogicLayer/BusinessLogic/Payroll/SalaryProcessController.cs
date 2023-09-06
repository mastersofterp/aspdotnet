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
            public class SalaryProcessController
            {
             private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

             public string PayRollCalculation(string idNos,int userIdno,int staffNo,string monYear,string CollegeCode, int collegeNo)
             {
                 string retStatus = string.Empty;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                     SqlParameter[] objParams = null;
                     //Add New File
                     objParams = new SqlParameter[8];
                     objParams[0] = new SqlParameter("@P_IDNOS",idNos);
                     objParams[1] = new SqlParameter("@P_USER_IDNO",userIdno);
                     objParams[2] = new SqlParameter("@P_STAFFNO",staffNo);
                     objParams[3] = new SqlParameter("@P_DATE",monYear);
                     objParams[4] = new SqlParameter("@P_COLLEGECODE",CollegeCode);
                     objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                     objParams[6] = new SqlParameter("@P_MESSAGE",SqlDbType.NVarChar,1000);                     
                     objParams[6].Direction = ParameterDirection.Output;
                     objParams[7] = new SqlParameter("@P_ERROR", SqlDbType.NVarChar, 1000);
                     objParams[7].Direction = ParameterDirection.Output;
                     retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PAYROLL_PAY_CALCULATION", objParams, true));
                     
                 }
                 catch (Exception ex)
                 {
                     retStatus = Convert.ToString(CustomStatus.Error);
                     throw new IITMSException("IITMS.UAIMS.BusinessLayer.SalaryProcessController.PayRollCalculation-> " + ex.ToString());
                 }
                 return retStatus;
             }

             public string PayRollBulkCalculation(string idNos, int userIdno, int staffNo, string monYear, string CollegeCode, int collegeNo)
             {
                 string retStatus = string.Empty;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                     SqlParameter[] objParams = null;
                     //Add New File
                     objParams = new SqlParameter[8];
                     objParams[0] = new SqlParameter("@P_IDNOS", idNos);
                     objParams[1] = new SqlParameter("@P_USER_IDNO", userIdno);
                     objParams[2] = new SqlParameter("@P_STAFFNO", staffNo);
                     objParams[3] = new SqlParameter("@P_DATE", monYear);
                     objParams[4] = new SqlParameter("@P_COLLEGECODE", CollegeCode);
                     objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                     objParams[6] = new SqlParameter("@P_MESSAGE", SqlDbType.NVarChar, 1000);
                     objParams[6].Direction = ParameterDirection.Output;
                     objParams[7] = new SqlParameter("@P_ERROR", SqlDbType.NVarChar, 1000);
                     objParams[7].Direction = ParameterDirection.Output;
                     retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PAYROLL_PAY_CALCULATION", objParams, true));
                 }
                 catch (Exception ex)
                 {
                     retStatus = Convert.ToString(CustomStatus.Error);
                     throw new IITMSException("IITMS.UAIMS.BusinessLayer.SalaryProcessController.PayRollBulkCalculation-> " + ex.ToString());

                 }
                 finally
                 {
                    
                 }
                 return retStatus;
                 
             }

             public string PayRollCalculationProvisonal(string idNos, int userIdno, int staffNo, string monYear, string CollegeCode, int collegeNo)
             {
                 string retStatus = string.Empty;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                     SqlParameter[] objParams = null;
                     //Add New File
                     objParams = new SqlParameter[8];
                     objParams[0] = new SqlParameter("@P_IDNOS", idNos);
                     objParams[1] = new SqlParameter("@P_USER_IDNO", userIdno);
                     objParams[2] = new SqlParameter("@P_STAFFNO", staffNo);
                     objParams[3] = new SqlParameter("@P_DATE", monYear);
                     objParams[4] = new SqlParameter("@P_COLLEGECODE", CollegeCode);
                     objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                     objParams[6] = new SqlParameter("@P_MESSAGE", SqlDbType.NVarChar, 1000);
                     objParams[6].Direction = ParameterDirection.Output;
                     objParams[7] = new SqlParameter("@P_ERROR", SqlDbType.NVarChar, 1000);
                     objParams[7].Direction = ParameterDirection.Output;
                     retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PAYROLL_PAY_CALCULATION_PROVISIONAL", objParams, true));

                 }
                 catch (Exception ex)
                 {
                     retStatus = Convert.ToString(CustomStatus.Error);
                     throw new IITMSException("IITMS.UAIMS.BusinessLayer.SalaryProcessController.PayRollCalculation-> " + ex.ToString());
                 }
                 return retStatus;
             }

             // Bulk Employee Salary Process
             public DataSet GetPayEmployeeBulkSalaryProcessData(int Collegeno, string Monthyear, int Staffno)
             {
                 DataSet ds = null;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                     SqlParameter[] objParams = new SqlParameter[3];
                     objParams[0] = new SqlParameter("@P_COLLEGENO", Collegeno);
                     objParams[1] = new SqlParameter("@P_MONYEAR", Monthyear);
                     objParams[2] = new SqlParameter("@P_STAFFNO", Staffno);
                     ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_SAL_PROCESS_STATUS_STAFFCOLLEGE_WISE", objParams);
                 }
                 catch (Exception ex)
                 {
                     return ds;
                     throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
                 }

                 return ds;

             }
            }
        }
    }
}
