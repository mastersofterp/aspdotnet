//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[lockUlockSalaryController]                                  
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
            public class lockUlockSalaryController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetStaffSalFile(int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_RET_STAFF_SALFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.lockUlockSalaryController.GetStaffSalFile-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStaffSalFileBulk(int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_RET_STAFF_SALFILE_BULK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.lockUlockSalaryController.GetStaffSalFile-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateUnlockSalary(int salNo, string yesNo)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SALNO",salNo);
                        objParams[1] = new SqlParameter("@P_YN",yesNo);                     
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UNLOCK_SALARY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.lockUlockSalaryController.UpdateUnlockSalary-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdatelockSalary(int staffNo, string mDate, string fDATE, int collegeNo)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STAFFNO",staffNo);
                        objParams[1] = new SqlParameter("@P_DATE",mDate);
                        objParams[2] = new SqlParameter("@P_FDT",fDATE);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_LOCK_SALARY_PERMANENTLY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.lockUlockSalaryController.UpdatelockSalary-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatelockSalaryBulk(int staffNo, string mDate, string fDATE, int collegeNo,int salno)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_DATE", mDate);
                        objParams[2] = new SqlParameter("@P_FDT", fDATE);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[4] = new SqlParameter("@P_SALNO", salno);
                        
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_LOCK_SALARY_PERMANENTLY_BULK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.lockUlockSalaryController.UpdatelockSalary-> " + ex.ToString());
                    }
                    return retStatus;
                }


            }
        }
    }
}
