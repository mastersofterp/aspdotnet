using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
            public class ResultProcessing
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public string ResultProcess(int sessionno, int schemeno, int semesterno, string idno, int prev_status, DateTime resultdate, string ipaddress, string macaddress, string remarks, int ua_no, int resulttype)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSION_NO", sessionno),
                            new SqlParameter("@P_SCHEME_NO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                            new SqlParameter("@P_RESULTDATE", resultdate),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_MACADDRESS", macaddress),
                            new SqlParameter("@P_REMARKS", remarks),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_RESULTTYPE", resulttype),
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                        SqlConnection conn = new SqlConnection(connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_WIN_RESULT_PROCESSING_RTM", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = cmd.Parameters[4].Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.ResultProcess() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public string ConsolidateTAECAEMarks(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, int t4compulsary)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_T4COMPULSARY", t4compulsary),
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                        SqlConnection conn = new SqlConnection(connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_EXAM_CONSOLIDATE_TAE_CAE_MARKS", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        //status = cmd.Parameters[2].Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.ConsolidateTAECAEMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public string ConsolidateRTMInternalMarks(int sessionno, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno)
                        };

                        SqlConnection conn = new SqlConnection(connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_EXAM_CONSOLIDATE_RTM_INTERNAL", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.ConsolidateRTMInternalMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public string ConsolidateAttendance(int sessionno, int degreeno, int semesterno, string condition, double att_percentage_mark4, int att_mark4, double att_percentage1_mark3, double att_percentage2_mark3, int att_mark3, double att_percentage1_mark2, double att_percentage2_mark2, int att_mark2, string condition2, double att_percentage_mark0, int att_mark0, int schemetype, string ccode)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_CONDITION", condition),
                            new SqlParameter("@P_ATT_PERCENT_MARK4", att_percentage_mark4),
                            new SqlParameter("@P_ATT_MARK4", att_mark4),
                            new SqlParameter("@P_ATT_PERCENT1_MARK3", att_percentage1_mark3),
                            new SqlParameter("@P_ATT_PERCENT2_MARK3", att_percentage2_mark3),
                            new SqlParameter("@P_ATT_MARK3", att_mark3),
                            new SqlParameter("@P_ATT_PERCENT1_MARK2", att_percentage1_mark2),
                            new SqlParameter("@P_ATT_PERCENT2_MARK2", att_percentage2_mark2),
                            new SqlParameter("@P_ATT_MARK2", att_mark2),
                            new SqlParameter("@P_CONDITION2", condition2),
                            new SqlParameter("@P_ATT_PERCENT_MARK0", att_percentage_mark0),
                            new SqlParameter("@P_ATT_MARK0", att_mark0),
                            new SqlParameter("@P_SCHEMETYPE", schemetype),
                            new SqlParameter("@P_CCODE", ccode)
                        };

                        SqlConnection conn = new SqlConnection(connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACAD_EXAM_ATTENDANCE_MARKS_NEW", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = cmd.Parameters[4].Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.ConsolidateRTMInternalMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                /// <summary>
                /// This controller is used to Update Grade Point.
                /// Page : GradeAllotment_Common.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="coursesno"></param>
                /// <param name="ccode"></param>
                /// <param name="sectionno"></param>
                /// <param name="cutoffaa"></param>
                /// <param name="cutoffab"></param>
                /// <param name="cutoffbb"></param>
                /// <param name="cutoffbc"></param>
                /// <param name="cutoffcc"></param>
                /// <param name="cutoffcd"></param>
                /// <param name="cutoffdd"></param>
                /// <param name="cutoffff"></param>
                /// <returns></returns>

                public int UpdateGradePointCourseWise(int sessionno, int coursesno, string ccode, int sectionno, int cutoffaa, int cutoffab, int cutoffbb, int cutoffbc, int cutoffcc, int cutoffcd, int cutoffdd, int cutoffff, int ccStatus)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_COURSENO", coursesno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_CUTOFFAA", cutoffaa),
                            new SqlParameter("@P_CUTOFFAB", cutoffab),
                            new SqlParameter("@P_CUTOFFBB", cutoffbb),
                            new SqlParameter("@P_CUTOFFBC", cutoffbc),
                            new SqlParameter("@P_CUTOFFCC", cutoffcc),
                            new SqlParameter("@P_CUTOFFCD", cutoffcd),
                            new SqlParameter("@P_CUTOFFDD", cutoffdd),
                            new SqlParameter("@P_CUTOFFFF", cutoffff),
                            new SqlParameter("@P_CCSTATUS", ccStatus)
                        };

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_GRADEPOINT_UPDATE", sqlParams, false);

                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.UpdateGradePointCourseWise() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public string ResultProcessAutonomous(int sessionno, int schemeno, int semesterno, string idno,DateTime resultdate,string ipaddress)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSION_NO", sessionno),
                            new SqlParameter("@P_SCHEME_NO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_RESULTDATE", resultdate),
                            new SqlParameter("@P_IPADDRSSS", ipaddress),
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                        SqlConnection conn = new SqlConnection(connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACD_RESULTPROCESSING", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = cmd.Parameters[6].Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.ResultProcessAutonomous() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public string LockResultAutonomous(int sessionno, int schemeno, int semesterno, int ua_no, string ipaddress, int lockstatus, int category)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_LOCKSTATUS", lockstatus),
                            new SqlParameter("@P_CATEGORY", category)
                          };

                        SqlConnection conn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand("PKG_EXAM_RESULT_LOCK", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ResultProcessing.LockResultAutonomous() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetMarkEntryStatus_BeforeResultProcess(int sessionno, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);


                        ds = objSQLHelper.ExecuteDataSetSP("UAIMSACAD.PKG_ACAD_RESULT_PROCESSING_MARKSCHECK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// GRADE NOT ENTERED STUDENTS COUNT IS SHOWN HERE, SUBJECTWISE, WHICH IS USED IN AUTO-RESULT PROCESSING PAGE.
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="schemeno"></param>
                /// <param name="semesterno"></param>
                /// <param name="prev_status"></param>
                /// <returns></returns>
                public DataSet GetStudentsWithNullGrade(int sessionno, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_WITH_NULL_GRADE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddPublishResult(int sessionno, int degreeno, int branchno, int semesterno, string idnos, DateTime pdate, string ipAdd, int status, int examtype,int Prev_status,int schemeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_IDNO", idnos);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", ipAdd);
                        objParams[6] = new SqlParameter("@P_PUB_UNPUB_DATE", pdate);
                        objParams[7] = new SqlParameter("@P_PUB_UNPUB", status);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", examtype);   //added on 15-06-2020 by Vaishali
                        objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[10] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PROC_EXAM_PUBLISH_RESULT_MAKAUT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;


                }

                public DataSet GetStudentInfoByIdResultProcess(int studentId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_INFO FOR_PROCESS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ResultProcessing.GetStudentInfoByIdResultProcess() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                
                public int AddPublishResult(int sessionno, int degreeno, int branchno, int semesterno, string idnos, DateTime pdate, string ipAdd, int status, int Prev_status, int schemeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_IDNO", idnos);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", ipAdd);
                        objParams[6] = new SqlParameter("@P_PUB_UNPUB_DATE", pdate);
                        objParams[7] = new SqlParameter("@P_PUB_UNPUB", status);
                        // objParams[8] = new SqlParameter("@P_EXAMTYPE", examtype);   //added on 15-06-2020 by Vaishali
                        objParams[8] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[9] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PROC_EXAM_PUBLISH_RESULT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;


                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: NITPRM  

}//END: IITMS