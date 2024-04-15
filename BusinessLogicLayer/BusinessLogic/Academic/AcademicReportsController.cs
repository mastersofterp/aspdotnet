using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AcademicReportsController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet Offered_Course_Status_Excel(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACADEMIC_ACTIVITY_OFFERED_COURSE_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetPendingAttData-> " + ex.ToString());
                    }
                    return ds;
                }

                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet GetSessionwiseRegistrationCount(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_COUNT_ACADEMIC_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseRegistrationDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet GetCourseTeacherAllotmentStatus(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_TEACHER_ALLOTMENT_ACADEMIC_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseCourseRegisteredStudentList-> " + ex.ToString());
                    }
                    return ds;
                }
                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet GetTimeTableStatus(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIME_TABLE_STATUS_ACADEMIC_REPORTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseCourseRegisteredStudentList-> " + ex.ToString());
                    }
                    return ds;
                }
                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet GetTeachingPlanAttendanceStatus(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TEACHINGPLAN_ATTENDANCE_STATUS_ACADEMIC_REPORTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseCourseRegisteredStudentList-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Rishabh on 31/12/2021 to get Teacher not allot data.
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetTeachernotAllotStatus(string session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHER_ALLOTMENT_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session 
                public DataSet GetCancelTimeTableReport(int sessionno, string college)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", college);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CANCEL_TIME_TABLE_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseCourseRegisteredStudentList-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}