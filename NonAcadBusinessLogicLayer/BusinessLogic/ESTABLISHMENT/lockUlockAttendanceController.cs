//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[lockUlockSalaryController]                                  
// CREATION DATE : 25-MAY-2011                                                        
// CREATED BY    : KUMAR PREMANKIT                                       
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
            public class lockUlockAttendanceController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetStaffAttendFile()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_RET_STAFF_ATTENDFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.lockUlockAttendController.GetStaffSalFile-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateUnlockAttendnace(int attendNo, string yesNo)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ATTENDNO", attendNo);
                        objParams[1] = new SqlParameter("@P_YN", yesNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UNLOCK_ATTENDANCE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.lockUlockAttendController.UpdateUnlockAttend-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //Modified by Mrunal Bansod.
                //Added one parameter for Deptno
                public int UpdatelockAttendance(string mDate, string fDATE, int STNO , int Collegeno)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DATE", mDate);
                        objParams[1] = new SqlParameter("@P_FDT", fDATE);
                        //objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[2] = new SqlParameter("@P_STNO", STNO);
                        objParams[3] = new SqlParameter("@P_Collegeno", Collegeno);
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_LOCK_ATTENDANCE_PERMANENTLY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.lockUlockAttendController.UpdatelockAttend-> " + ex.ToString());
                    }
                    return retStatus;
                }



            }
        }
    }
}
