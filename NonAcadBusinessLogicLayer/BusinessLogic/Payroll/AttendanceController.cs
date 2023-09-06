//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ATTENDANCE CONTROLLER]                                  
// CREATION DATE : 23-JULY-2009                                                        
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
            public class AttendanceController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetAttendanceOfEmployee(int staffNo, int ShowAbsent, int orderByIdno, int Dept, int EmpType,int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_SHOW_ABSENT", ShowAbsent);
                        objParams[2] = new SqlParameter("@P_ORDERBY_IDNO", orderByIdno);
                        objParams[3] = new SqlParameter("@P_DEPT", Dept);
                        objParams[4] = new SqlParameter("@P_EMPTYPENO", EmpType);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_PAY_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int UpdateAbsentDays(decimal absentDays, int idNo, decimal overtime)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ABSENTDAYS", absentDays);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        objParams[2] = new SqlParameter("@P_OVERTIME", overtime);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_ABSENTDAYS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.AttendanceController.UpdateAbsentDays-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Publish Attendance
                //public int AddUnPublishAttendance(int sessionno, int schemeno, int semesterno, int absorption_status, int sectionno, string idnos, int status)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[8];

                //        {
                //            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //            objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                //            objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //            objParams[3] = new SqlParameter("@P_ABSORPTION_STATUS", absorption_status);
                //            objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                //            objParams[5] = new SqlParameter("@P_IDNOS", idnos);
                //            objParams[6] = new SqlParameter("@P_STATUS", status);
                //            objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //            objParams[7].Direction = ParameterDirection.Output;
                //        }

                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_REPORT_PUBLISH_STU_ATTENDANCE", objParams, true) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResultProcessing.AddUnPublishAttendance-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddUnPublishAttendance(int sessionno, int schemeno, int semesterno, int absorption_status, int sectionno, string idnos, int status)
                {
                    //DataSet ds = null;   
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_ABSORPTION_STATUS", absorption_status),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_STATUS", status),
                            new SqlParameter("@P_OUTPUT", status)

                        };
                        objParams[objParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                         if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_REPORT_PUBLISH_STU_ATTENDANCE", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResultProcessing.AddUnPublishAttendance-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to delete record from Attendance table.
                /// </summary>
                /// <param name="newsid">Delete record as per this attno.</param>
                /// <returns>Integer CustomStatus - Record Deleted or Error</returns>
                public int Delete(int attno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ATTNO", attno);

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SP_DEL_ATTENDANCE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.Delete-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                #region SalaryHold
                public DataSet GetEmployeeForSalaryHold(int staffNo, string salaryMonth, int holdEmployees, int orderByIdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_MONTH", salaryMonth);
                        objParams[2] = new SqlParameter("@P_SHOWHOLDEMPLOYEES", holdEmployees);
                        objParams[3] = new SqlParameter("@P_ORDERBY_IDNO", orderByIdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_PAY_EMPLOYEE_FOR_HOLD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetEmployeeForSalaryHold-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateEmployeeHoldStatus(string month, bool holdSalary, int idNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MONTH", month);
                        objParams[1] = new SqlParameter("@P_HOLDSALARY", holdSalary);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_HOLD_SALARY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.AttendanceController.UpdateEmployeeHoldStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

               
            }
        }
    }
}
