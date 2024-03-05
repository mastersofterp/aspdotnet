using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class MarksEntryController : IDisposable
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetControlSheetNoByCourse(int sessionno, int ua_no, int subid, int courseno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_UA_NO", ua_no),
                        new SqlParameter("@P_SUBID", subid),
                        new SqlParameter("@P_COURSENO", courseno),
                        new SqlParameter("@P_EXAMNAME", exam)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_COURSE_SP_GET_CONTROLSHEETNO", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                /// <summary>
                /// Added By Rita M.   17-12-2019
                /// </summary>
                /// <param name="sessiono"></param>
                /// <param name="ua_no"></param>
                /// <param name="ccode"></param>
                /// <param name="sectionno"></param>
                /// <param name="subid"></param>
                /// <param name="Exam"></param>
                /// <param name="schemeno"></param>
                /// <param name="ua_name"></param>
                /// <returns></returns>
                public DataSet GetStudentsForMarkEntryadmin_Operator(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string ua_name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[7] = new SqlParameter("@P_USERNAME", ua_name);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_OPERATOR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public string MarkEntryResultProc(Exam objExam, string ip, string idno)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objExam.Sessionno),
                            new SqlParameter("@P_SCHEMENO",objExam.SchemeNo),
                            new SqlParameter("@P_SEMESTER_NO",objExam.SemesterNo),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ip),
                            
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                        SqlConnection conn = new SqlConnection(_connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACD_RESULTPROCESSING_OLD_NITGOA", conn);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.MarkEntryResultProc() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
                public DataSet GetStudentsForMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                /// <summary>
                /// Added By S.Patil - 30012020
                /// </summary>
                /// <param name="sessiono"></param>
                /// <param name="ua_no"></param>
                /// <param name="ccode"></param>
                /// <param name="sectionno"></param>
                /// <param name="subid"></param>
                /// <param name="semesterno"></param>
                /// <param name="Exam"></param>
                /// <param name="COURSENO"></param>
                /// <param name="subexam"></param>
                /// <returns></returns>
                public DataSet GetStudentsForMarkEntrySubExam(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string subexam, int SubExamNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUBEXAM", subexam);
                        objParams[9] = new SqlParameter("@P_SUBEXAMNO", SubExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_SUBEXAM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                //public DataSet GetStudentsForMarkEntryadmin(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno) //Comment by Mahesh on Dated 24/06/2021
                public DataSet GetStudentsForMarkEntryadmin(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                //method to get marks entry status added on[14-sep-2016]
                /// <summary>
                /// Modified By S.Patil - 31012020
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="ua_no"></param>
                /// <param name="subid"></param>
                /// <returns></returns>
                //public DataSet GetCourse_MarksEntryStatus(int sessionno, int ua_no, int subid)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_SUBID", subid);
                //        if (subid != 2) // added by S.Patil
                //        {
                //            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);
                //        }
                //        else
                //        {
                //            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM", objParams);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                public DataSet GetCourse_MarksEntryStatus(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid == 2 || subid == 4) // added by S.Patil
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }


                //Added on 08022023
                public DataSet MarkEntryStatus(int schemeno, int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MARK_ENTRY_STATUS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.MarkEntryStatus-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryTA(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_TA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public DataSet GetStudentsForMarkEntry(int sessiono, int ua_no, int courseno, string ccode, int subid, string exam, int sectionno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[4] = new SqlParameter("@P_SUBID", subid);
                //        objParams[5] = new SqlParameter("@P_EXAM", exam);
                //        objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                //        //objParams[6] = new SqlParameter("@P_CONTROLSHEET_NO", controlsheetno);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int opNo)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        {
                //            //Parameters for MARKS
                //            new SqlParameter("@P_SESSIONNO", sessionno),
                //            new SqlParameter("@P_COURSENO", courseno),
                //            new SqlParameter("@P_CCODE", ccode),
                //            new SqlParameter("@P_STUDIDS", idnos),
                //            //Mark Fields
                //            new SqlParameter("@P_MARKS", marks),
                //            //Parameters for Final Lock 
                //            new SqlParameter("@P_LOCK", lock_status),
                //            new SqlParameter("@P_EXAM", exam),
                //            //Parameters for ACD_LOCKTRAC TABLE 
                //            new SqlParameter("@P_TH_PR", th_pr),
                //            new SqlParameter("@P_UA_NO", ua_no),
                //            new SqlParameter("@P_IPADDRESS", ipaddress),
                //            new SqlParameter("@P_EXAMTYPE", examtype),
                //            //new SqlParameter("@P_OP", SqlDbType.Int)
                //            new SqlParameter("@P_WIN_OPERATOR_NO",opNo)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                //        if (ret != null && ret.ToString() == "1")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        {
                //            //Parameters for MARKS
                //            new SqlParameter("@P_SESSIONNO", sessionno),
                //            new SqlParameter("@P_COURSENO", courseno),
                //            new SqlParameter("@P_CCODE", ccode),
                //            new SqlParameter("@P_STUDIDS", idnos),
                //            //Mark Fields
                //            new SqlParameter("@P_MARKS", marks),
                //            //Parameters for Final Lock 
                //            new SqlParameter("@P_LOCK", lock_status),
                //            new SqlParameter("@P_EXAM", exam),
                //            //Parameters for ACD_LOCKTRAC TABLE 
                //            new SqlParameter("@P_TH_PR", th_pr),
                //            new SqlParameter("@P_UA_NO", ua_no),
                //            new SqlParameter("@P_IPADDRESS", ipaddress),
                //            new SqlParameter("@P_EXAMTYPE", examtype),
                //            new SqlParameter("@P_TITLE", title),
                //            new SqlParameter("@P_OP", SqlDbType.Int)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                //        if (ret != null && ret.ToString() == "1")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UpdateMarkEntry(int sessionno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            //new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
         
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AdminUpdateMarkEntry(int sessionno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, string SubExam, int ModifyBy, int Courseno, int Examno, string SUBEXAMCOMPONETNAME)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            //new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            //added Mahesh on Dated 24/06/2021
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_MODIFYBY", ModifyBy),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", SUBEXAMCOMPONETNAME),

                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_STUD_INSERT_MARKS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Added by S.Patil - 29012020
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="ccode"></param>
                /// <param name="idnos"></param>
                /// <param name="marks"></param>
                /// <param name="semno"></param>
                /// <param name="lock_status"></param>
                /// <param name="exam"></param>
                /// <param name="examno"></param>
                /// <param name="sectionno"></param>
                /// <param name="th_pr"></param>
                /// <param name="ua_no"></param>
                /// <param name="ipaddress"></param>
                /// <param name="examtype"></param>
                /// <returns></returns>
                public int UpdateMarkEntryForSubExam(int sessionno, int courseno, string ccode, string idnos, string marks, int semno, int lock_status, string exam, int examno, int sectionno, int th_pr, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                             new SqlParameter("@P_SEMESTERNO", semno),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_SECTIONNO",sectionno ),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_SUBEXAM", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet CourseEligibilityCheck(int sessionno, string courseccode, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_CCODE", courseccode),
                            new SqlParameter("@P_COURSENO", courseno),                           
                           
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_COURSE_ELIGIBILITY_CHK_MARKENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return ds;
                }

                //for course update
                public string MarkEntryResultProcStudent(int session, int schemeno, int semester, string ip, int idno)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",session),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_SEMESTER_NO",semester),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                         new SqlParameter("@P_IPADDRSSS",ip),
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                        SqlConnection conn = new SqlConnection(_connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACD_RESULTPROCESSING_NITGOA", conn);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.MarkEntryResultProc() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public int GradeAllotment(int sessionno, int courseno, string examtype, string absoluterelative, int degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ABSOLUTERELATIVE",absoluterelative),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_NEW_RELATIVE_13012011", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GradeAllotment --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int CutOffALlotment(int sessionno, int courseno, string examtype, string absoluterelative, int degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ABSOLUTERELATIVE",absoluterelative),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_CUTTOFF_CAL_RELATIVE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GradeAllotment --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEndExamMarksDataExcel(int sessionno, int courseno, int sectionno, int prev_status, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[4] = new SqlParameter("@P_UA_NO ", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_GET_STUD_FOR_MARKENTRY_ENDSEM_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetEndExamMarksDataExcel-> " + ex.ToString());
                    }

                    return ds;
                }

                public int ProcessResult(int sessionno, int schemeno, int semesterno, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSION_NO", sessionno),
                            new SqlParameter("@P_SCHEME_NO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_GRADE_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCourseForTeacher(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //added on [10-sep-2016] for marks entry form.
                public DataSet GetONExams_NEW(int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_GET_EXAM_NAMES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams_NEW-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseDetailsForMarkEntry(int sessionno, int ua_type, int ua_dec, int ua_no, int page_link, int courseno, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_DEC", ua_dec);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[4] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[6] = new SqlParameter("@P_CCODE", ccode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_GET_COURSE_FOR_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseDetailsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                //public DataSet GetONExams(int sessionno, int ua_type, int ua_no, int page_link)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                //        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                //        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                //public DataSet GetCourses_By_ON_EXAMS(int sessiono, int schemeno, int semesterno, string exam)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[3] = new SqlParameter("@P_EXAM", exam);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForMarkEntry_Admin-> " + ex.ToString());
                //    }

                //    return ds;
                //}
                public DataSet GetONExams(int sessionno, int ua_type, int ua_no, int page_link)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added By S.Patil - 28 Jan 2020
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="ua_type"></param>
                /// <param name="ua_no"></param>
                /// <param name="page_link"></param>
                /// <param name="subid"></param>
                /// <returns></returns>
                public DataSet GetONExamsActivityBySubjectType(int sessionno, int ua_type, int ua_no, int page_link, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_BYSUBID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added Mahesh on Dated 10/02/2020
                public DataSet GetONPracticalExamsBySubjectType(int Patternno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PATTERNNO", Patternno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_MULTIPLE_EXAMS_BYSUBID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONPracticalExamsBySubjectType-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourses_By_ON_EXAMS(int sessiono, int schemeno, int semesterno, string exam, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetCourses_By_ON_EXAMS_MarkEntry(int sessiono, int schemeno, int semesterno, string exam, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS_MARKENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourses_By_ON_EXAMS_MarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }




                //public DataSet GetCourses_By_ON_EXAMS_MarkEntry(int sessiono, int schemeno, int semesterno, string exam, int uano)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[3] = new SqlParameter("@P_EXAM", exam);
                //        objParams[4] = new SqlParameter("@P_UA_NO", uano);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS_MARKENTRY", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourses_By_ON_EXAMS_MarkEntry-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //public DataSet GetCourses_By_ON_EXAMS_MarkEntry_Revaluation(int sessiono, int schemeno, int semesterno, int courseno)
                //{

                //}

                public DataSet GetTeacher_Unlock_Marks(int sessionno, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_TEACHER_FOR_UNLOCK_MARKS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetTeacher_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetScheme_Unlock_Marks(int sessionno, int subid, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_UNLOCK_MARKS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetScheme_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_Unlock_Marks(int sessionno, int subid, int ua_no, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSE_FOR_UNLOCK_MARKS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_LockDetails(int sessionno, int courseno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_FOR_COURSE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_LockDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public int LockUnLockMarkEntry(int sessionno, int schemeno, int courseno, int th_pr, int ua_no, bool lock_status, string exams, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAMS", exams),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_LOCK_UNLOCK_MARKENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetONExams(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_ONEXAMS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetExamCourses(int sessionno, int schemeno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETEXAM_COURSES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetExamCourses-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetMarksList(int sessionno, int schemeno, int courseno, int controlsheetno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_CONTROLSHEET_NO", controlsheetno);
                        objParams[4] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MARKSHEET_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarksList-> " + ex.ToString());
                    }

                    return ds;
                }

                #region Admin Mark Entry Methods
                //===============================


                public DataSet GetStudentsForMarkEntry_AdminNew(int sessiono, int schemeno, int semesterno, int sectionno, int courseno, string ccode, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", ccode);
                        objParams[6] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_NEW", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetStudentsForMarkEntry_Admin(int sessiono, int courseno, string ccode, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                //public DataSet GetSchemeForMarkEntry_Admin(int sessiono, int deptno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_MARKENTRY_ADMIN", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetSchemForMarkEntry_Admin-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //public DataSet GetSchemeForMarkEntry_Admin(int sessiono, int deptno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                //        //objParams[2] = new SqlParameter("@P_UA_NO", uano);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_MARKENTRY_ADMIN", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetSchemForMarkEntry_Admin-> " + ex.ToString());
                //    }

                //    return ds;
                //}
                public DataSet GetSchemeForMarkEntry_Admin(int sessiono, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        //objParams[2] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_MARKENTRY_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetSchemForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetCourseForTeacherEndSem(int sessionno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_END_SEM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacherEndSem-> " + ex.ToString());
                    }

                    return ds;
                }
                //Modify Grade
                public DataSet GetStudentsForModifyGradeEntry(int sessiono, int courseno, string ccode, string exam, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_OLDGRADE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                //UNLOCK CHANGE GRADE
                public int UnlockChangeGrade(int sessionno, int courseno, string idnos, string ccode, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int opNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS",idnos),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_WIN_OPERATOR_NO",opNo)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_UNLOCK_GRADE_ENTRY_CHANGE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //MODIFY SAVE PROC UPDATE GRADE ENTRY 

                public int UpdateNewGrade(int sessionno, int courseno, int idnos, string grade, string newgrade, int lock_status, int ua_no, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                           // new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_OLDGRADE", grade),
                            new SqlParameter("@P_NEWGRADE",newgrade),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OP", SqlDbType.Int)

                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_CHANGE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForMarkEntry_Admin(int sessiono, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETEXAM_COURSES_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetExamsForCourse_Admin(int sessiono, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_FOR_COURSE_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetExamsforCourse_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetControlSheetNoByCourse_Admin(int sessionno, int courseno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", courseno),
                        new SqlParameter("@P_EXAMNAME", exam)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_COURSE_SP_GET_CONTROLSHEETNO_ADMIN", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse_Admin() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                public DataSet GetMarkEntryIncompleteSubjOpr1(int firstOprNo, int secondOprNo, int sessionNo, int schemeNo, string examName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_OPERATOR1", firstOprNo),   
                            new SqlParameter("@P_OPERATOR2", secondOprNo),
                            new SqlParameter("@P_SESSION_NO", sessionNo),
                            new SqlParameter("@P_SCHEME_NO", schemeNo),
                            new SqlParameter("@P_EXAM_NAME", examName)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_WIN_MARK_ENTRY_GET_INCOMPLETE_SUBJ1", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetMarkEntryIncompleteSubjOpr1() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetMarkEntryIncompleteSubjOpr2(int firstOprNo, int secondOprNo, int sessionNo, int schemeNo, string examName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_OPERATOR1", firstOprNo),   
                            new SqlParameter("@P_OPERATOR2", secondOprNo),
                            new SqlParameter("@P_SESSION_NO", sessionNo),
                            new SqlParameter("@P_SCHEME_NO", schemeNo),
                            new SqlParameter("@P_EXAMNAME", examName)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_WIN_MARK_ENTRY_GET_INCOMPLETE_SUBJ2", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetMarkEntryIncompleteSubjOpr2() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int LockAB_CC(int sessionno, int courseno, int lck)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_LOCK", lck)
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LOCK_ABSENT_COPYCASE", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
                //public DataSet GetCourseForTeacherForAttendance(int sessionno, int ua_no, int subid, int sectionNo, int querytype)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_SUBID", subid);
                //        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                //        objParams[4] = new SqlParameter("@P_QUERYTYPE ", querytype);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                //    }

                //    return ds;
                //}
                //added by reena
                //public DataSet GetCourseForTeacherForAttendance(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_SUBID", subid);
                //        objParams[3] = new SqlParameter("@P_THPR", thpr);
                //        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                //    }

                //    return ds;
                //}


                /// <summary>
                /// Added by Pritish on Date 31-05-2019
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="ua_no"></param>
                /// <param name="subid"></param>
                /// <param name="thpr"></param>
                /// <param name="sectionNo"></param>
                /// <returns></returns>
                public DataSet GetCourseForTeacherForAttendance(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_THPR", thpr);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }


                //public DataSet GetCourseForTeacherForAttendancealt(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_SUBID", subid);
                //        objParams[3] = new SqlParameter("@P_THPR", thpr);
                //        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE_altcourses", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                //    }

                //    return ds;
                //}



                //LOCK UNLOCK MARKS --testmark

                /// <summary>
                /// Added by Pritish on Date 31-05-2019
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="ua_no"></param>
                /// <param name="subid"></param>
                /// <param name="thpr"></param>
                /// <param name="sectionNo"></param>
                /// <returns></returns>
                public DataSet GetCourseForTeacherForAttendancealt(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_THPR", thpr);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE_altcourses", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// This controller is used to Lock or Unlock Test marks entry.
                /// Page : LockMarksByScheme.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="exam"></param>
                /// <param name="test"></param>
                /// <param name="schemeno"></param>
                /// <param name="semesterno"></param>
                /// <param name="sectionno"></param>
                /// <param name="courseno"></param>
                /// <param name="subid"></param>
                /// <param name="un_no"></param>
                /// <param name="lock_status"></param>
                /// <param name="ipaddress"></param>
                /// <param name="ldate"></param>
                /// <returns></returns>

                //public int LockUnLockMarkEntryByAdmin(int sessionno, int semester, int schemeno, int examtype, int courseno, int section, int facultynotheory, int facultynopractical, int lockunlock, string ipaddress, int lock_by)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        { 
                //            //new SqlParameter("@P_COLLEGEID",college_id),
                //            new SqlParameter("@P_SESSIONNO",sessionno),
                //            new SqlParameter("@P_SEMESTER",semester),
                //            new SqlParameter("@P_SCHEMENO",schemeno),
                //            new SqlParameter("@P_EXAMTYPE",examtype),                     
                //            new SqlParameter("@P_COURSENO",courseno), 
                //            new SqlParameter("@P_SECTION",section),
                //            new SqlParameter("@P_UA_NO",facultynotheory),
                //            new SqlParameter("@P_UA_NO_PRAC",facultynopractical),
                //            new SqlParameter("@P_LOCK",lockunlock),
                //            new SqlParameter("@P_IPADDRESS",ipaddress),
                //           // new SqlParameter("@P_LDATE",ldate),
                //           // new SqlParameter("@P_EXAMNO",examno),
                //            //new SqlParameter("@P_OPID",opid),
                //            new SqlParameter("@P_LOCKBY",lock_by)
                //        };

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_UNLOCK", objParams, false);
                //        if (ret != null)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int LockUnLockMarkEntryByAdmin(int sessionno, int semester, int schemeno, int examtype, int courseno, int section, int facultynotheory, int facultynopractical, int lockunlock, string ipaddress, int lock_by, string subexamtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //new SqlParameter("@P_COLLEGEID",college_id),
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_SEMESTER",semester),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_EXAMTYPE",examtype),                     
                            new SqlParameter("@P_COURSENO",courseno), 
                            new SqlParameter("@P_SECTION",section),
                            new SqlParameter("@P_UA_NO",facultynotheory),
                            new SqlParameter("@P_UA_NO_PRAC",facultynopractical),
                            new SqlParameter("@P_LOCK",lockunlock),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                           // new SqlParameter("@P_LDATE",ldate),
                           // new SqlParameter("@P_EXAMNO",examno),
                            //new SqlParameter("@P_OPID",opid),
                            new SqlParameter("@P_LOCKBY",lock_by),
                            new SqlParameter("@P_SUBEXAMTYPE",subexamtype)     // added on 13-02-2020 by Vaishali
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_UNLOCK", objParams, false);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// This controller is used to Get Student for Marks entry.
                /// Page : MarkEntryByAdmin.aspx
                /// </summary>
                /// <param name="sessiono"></param>
                /// <param name="courseno"></param>
                /// <param name="ccode"></param>
                /// <param name="exam"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>

                public DataSet GetStudentsForMarkEntry_Admin(int sessiono, int courseno, string ccode, string exam, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllGrades(int sessionno, int subid, int courseno, int ua_no, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_UANO", ua_no);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_GRADES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }





                public DataSet GetCourses_By_ON_EXAMS_MarkEntry_Revaluation(int sessiono, int schemeno, int semesterno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS_MARKENTRY_REVALUATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourses_By_ON_EXAMS_MarkEntry_Revaluation-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_LockStatus(int sessionno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LOCKUNLOCK_STATUS_BY_SCHEME", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_LockDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseStatusByUano(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_FACULTY_MARK_ENTRY_CHECK_BY_UANO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //UPDATE MARK ENTRY FS ON 11 MAY 2013
                public int UpdateFsMarkEntry(int sessionno, int courseno, int idno, decimal s1mark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_IDNO", idno),
                            //Mark Fields
                            new SqlParameter("@P_S1MARK", s1mark),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_MARKS_FS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateFsMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //LOCK MARK ENTRY FS ON 11 MAY 2013
                public int UpdateLockMarkEntryFs(int session, int course, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", session),
                            new SqlParameter("@P_COURSENO", course),
                            new SqlParameter("@P_IDNO", idno),
                            //Mark Fields
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_MARKSLOCK_FS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateFsMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Check SPI CPI status
                public DataSet Get_SPI_CPI_Status(int sessionno, int degreeno, int branchno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_CPI_SPI_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.Get_SPI_CPI_Status-> " + ex.ToString());
                    }

                    return ds;
                }

                //Check SPI CPI Semesterwise status
                public DataSet Get_SPI_CPI_Sem_Status(int sessionno, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_CPI_SPI_REPORT_NEW", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.Get_SPI_CPI_Sem_Status-> " + ex.ToString());
                    }

                    return ds;
                }
                public int AddAuditCourse(MarkEntry objMarkEntry, string grade, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);
                        objParams[5] = new SqlParameter("@P_GRADES", grade);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_GRADE", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.AddAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetAllIncompleteMarkEntry(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                    new SqlParameter("@P_SEMESTERNO", semesterno)                    
                 };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_MARKS_ENTRY_NOT_DONE_RESULT", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetAllIncompleteMarkEntry --> " + ex.Message);
                    }
                    return ds;
                }
                public DataSet GetAllIncompleteLockStatus(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                    new SqlParameter("@P_SEMESTERNO", semesterno)                    
                 };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_ENDSEM_LOCK_ENTRY_NOT_DONE_RESULT", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetAllIncompleteLockStatus --> " + ex.Message);
                    }
                    return ds;
                }
                public DataSet GetStudentData(int sessionno, int degreeno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_DATA", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }
                ////public int ProcessResultAll(int sessionno, int schemeno, int semesterno, int exam, string idno, string ipAdddress, int colg)
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                ////        //SqlParameter[] objParams = new SqlParameter[] { };
                ////        SqlParameter[] objParams = new SqlParameter[] 
                ////        {
                ////            //Parameters for MARKS
                ////            new SqlParameter("@P_SESSIONNO", sessionno),
                ////            new SqlParameter("@P_SCHEMENO", schemeno),
                ////            new SqlParameter("@P_SEMESTER_NO", semesterno),
                ////            new SqlParameter("@P_EXAM", exam),
                ////            new SqlParameter("@P_IDNO",idno),
                ////             new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                ////             new SqlParameter("@P_IPADDRSSS",ipAdddress),
                ////             new SqlParameter("@P_COLLEGE_ID",colg),
                ////            new SqlParameter("@P_MSG", SqlDbType.Int)
                ////        };
                ////        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                ////        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MZU_UG", objParams, true);
                ////        if (ret != null && ret.ToString() == "1")
                ////        {
                ////            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                ////        }
                ////        else
                ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                ////    }
                ////    return retStatus;
                ////}

                /// <summary>
                /// Updated  BY: S.Patil ON 30-07-2020 | RESULT PROCESSING AJU
                /// </summary>
                public int ProcessResultAll(int sessionno, int schemeno, int semesterno, string idno, int exam, string ipAdddress, int colg, int STUDENTTYPE, int ua_no, int covidFlag)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ipAdddress),
                            new SqlParameter("@P_COLLEGE_ID",colg),   
                            //new SqlParameter("@P_RESULT_TYPE",STUDENTTYPE),  
                            //new SqlParameter("@P_TYPE",Type),  
                            new SqlParameter("@P_UA_NO",ua_no),  
                             new SqlParameter("@P_REG_COVID19",covidFlag),// added by s.patil - 28072020
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MAKAUT", objParams, true); //Added by roshan 28-12-2016

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResultAll --> " + ex.ToString());
                    }
                    return retStatus;
                }

                // FOR RESULT PROCESS OF PC
                public int ProcessResultPCAll(int sessionno, int schemeno, int semesterno, int exam, string idno, string ipAdddress, int colg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                             new SqlParameter("@P_IPADDRSSS",ipAdddress),
                             new SqlParameter("@P_COLLEGE_ID",colg),
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MZU_PC", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ProcessResultAllSem(int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                              new SqlParameter("@P_IDNO",idno),
                            
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_ALLSEM_NITGOA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int LockAuditCourse(MarkEntry objMarkEntry, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_LOCK", objParams, false);


                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException

                           ("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.LockAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UnlockAuditCourse(MarkEntry objMarkEntry, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_UNLOCK", objParams, false);


                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException

                           ("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.UnlockAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateMinnorMidLockMarkEntry(int courseno, int sessionno, int schemeno, int uano, int sectionno, string exam, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_UA_NO", uano),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IPADDRESS", ipAddress)
                        };
                        //objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOCK_MINNOR_MID_MARK_ENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMinnorMidLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentExtmarkDetails(int opid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_OPID", opid),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_EXTERNAL_STUD_MARKS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet GetStudentmarkDetails(int opid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_OPID", opid),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_STUD_MARKS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }
                public int MarkEntrybyOperator(int opid, string studname, string examname, string marks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_OPID", opid);
                        objParams[1] = new SqlParameter("@P_STUDIDS", studname);
                        objParams[2] = new SqlParameter("@P_EXAMNAME", examname);
                        objParams[3] = new SqlParameter("@P_MARKS", marks);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARK_ENTRY_BY_OPERATOR", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }
                public int LockEntrybyOperator(int opid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_OPID", opid);

                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_LOCK_ENTRY_BY_OPERATOR", objParams, true);
                        if (ret != null && ret.ToString() == "2")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }
                public DataSet GetMatchMarkCompare(int courseno, int status, int DEGREE, int branch, int colg, int sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {

                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_INT_EXT", status),
                        new SqlParameter("@P_DEGREENO", DEGREE),
	                    new SqlParameter("@P_BRANCHNO", branch),
	                    new SqlParameter("@P_COLLEGENO", colg),
	                    new SqlParameter("@P_SEMNO", sem),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MATCH_BY_OPERATOR", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }
                //public DataSet GetMarkCompareReport(int courseno, int status, int DEGREE, int branch, int colg, int sem)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                //        SqlParameter[] sqlParams = new SqlParameter[] {

                //        new SqlParameter("@P_courseno", courseno),
                //        new SqlParameter("@P_INT_EXT", status),
                //        new SqlParameter("@P_DEGREENO", DEGREE),
                //        new SqlParameter("@P_BRANCHNO", branch),
                //        new SqlParameter("@P_COLLEGENO", colg),
                //        new SqlParameter("@P_SEMNO", sem),
                //};
                //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR_report", sqlParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                //    }
                //    return ds;
                //}

                //modified on 25-03-2020 by Vaishali
                public DataSet GetMarkCompareReport(int colg, int sessionno, int schemeno, int semesterno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {

                        new SqlParameter("@P_COLLEGE_ID", colg),
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_SCHEMENO", schemeno),
	                    new SqlParameter("@P_SEMESTERNO", semesterno),
	                    new SqlParameter("@P_EXAMTYPE", examtype),
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR_report", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetMarkCompareReport() --> " + ex.Message);
                    }
                    return ds;
                }

                //public int UnLockOperatorMarkEntry(string OPID, string IDNOS)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        { 
                //            new SqlParameter("@P_OPID", OPID),
                //            new SqlParameter("@P_IDNOS", IDNOS)

                //        };
                //        //;objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARKS_UNLOCK", objParams, true);
                //        if (ret != null)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                //modified on 25-03-2020 by Vaishali
                public int UnLockOperatorMarkEntry(string OPID, string IDNOS, int COLGID, int sessionno, int schemeno, int semesterno, int examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_OPID", OPID),
                            new SqlParameter("@P_IDNOS", IDNOS),
                            new SqlParameter("@P_COLLEGE_ID", COLGID),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_EXAMTYPE", examtype),                        
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARKS_UNLOCK", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UnLockOperatorMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int MoveOperatorMarkEntry(int sessionno, int opid, int intext)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                          
                            new SqlParameter("@P_opid", opid),
                             new SqlParameter("@P_INT_EXT", intext),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                         };
                        ; objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_STUD_MARKS_RESULT", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public DataSet GetMarkCompare(int courseno, int COLGID, int status)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                //        SqlParameter[] sqlParams = new SqlParameter[] {

                //        new SqlParameter("@P_courseno", courseno),
                //        new SqlParameter("@P_COLLEGE_ID", COLGID),
                //        new SqlParameter("@P_INT_EXT", status),
                //        new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                //        };
                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR", sqlParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                //    }
                //    return ds;
                //}



                //modified by Vaishali on 25-03-2020
                public DataSet GetMarkCompare(int COLGID, int sessionno, int schemeno, int semesterno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {

                        new SqlParameter("@P_COLLEGE_ID", COLGID),
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_SCHEMENO", schemeno),
                        new SqlParameter("@P_SEMESTERNO", semesterno),
                        new SqlParameter("@P_EXAMTYPE", examtype),
                        new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetMarkCompare() --> " + ex.Message);
                    }
                    return ds;
                }
                public int InsertMarkEntryLock(int opid, int LOCKBY, string ipadress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_OPID", opid),
                            new SqlParameter("@P_LOCKBY", LOCKBY),
                            new SqlParameter("@P_IPADDRESS", ipadress),
                         
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARKS_ENTRY_LOCK_LOG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMarkComparewithlock(int courseno, int COLGID, int status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {

                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_COLLEGE_ID", COLGID),
                        new SqlParameter("@P_INT_EXT", status),
                       
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR_lock", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet Getopidstring(int sessiono, int status, int courseno, int colg)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", colg);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_INT_EXT", status);

                        //objParams[6] = new SqlParameter("@P_CONTROLSHEET_NO", controlsheetno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_acd_opid_string", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public int LockAB_CC(int sessionno, int courseno, string ExamNo, string idno, int lck)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_EXAMNO", ExamNo),
                            new SqlParameter("@P_IDNOS", idno),
                            new SqlParameter("@P_LOCK", lck),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }
                ////public DataSet GetresultprocessStudentData(int sessionno, int degreeno, int schemeno, int semesterno, int colgid)
                ////{
                ////    DataSet ds = null;
                ////    try
                ////    {
                ////        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                ////        SqlParameter[] sqlParams = new SqlParameter[] {
                ////    new SqlParameter("@P_SESSIONNO", sessionno),
                ////    new SqlParameter("@P_DEGREENO", degreeno),
                ////    new SqlParameter("@P_SCHEMENO", schemeno),
                ////    new SqlParameter("@P_SEMESTERNO", semesterno),
                ////    new SqlParameter("@P_collegeid", colgid),
                ////};
                ////        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_result_process_STUDENT_DATA", sqlParams);

                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                ////    }
                ////    return ds;
                ////}

                public DataSet GetresultprocessStudentData(int sessionno, int degreeno, int schemeno, int semesterno, int colgid, int StudentType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_collegeid", colgid),
                            new SqlParameter("@P_StudentType", StudentType),
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_RESULT_PROCESS_STUDENT_DATA", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }


                public int PGProcessResultAll(int sessionno, int schemeno, int semesterno, int exam, string idno, string ipAdddress, int colg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                             new SqlParameter("@P_IPADDRSSS",ipAdddress),
                             new SqlParameter("@P_COLLEGE_ID",colg),
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MZU_PG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForTeacherForAttendance1(int sessionno, int ua_no, int subid, int sectionNo, int querytype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                        objParams[4] = new SqlParameter("@P_QUERYTYPE ", querytype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }
                //public int UpdateExamRegStatus(string idnos, int sess, int scheme, int degree, int branch, int semester, int colgid)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        {
                //            //Parameters for MARKS
                //            new SqlParameter("@P_IDNOS", idnos),
                //            new SqlParameter("@P_SESSIONNO", sess),
                //            new SqlParameter("@P_SCHEMENO", scheme),
                //             new SqlParameter("@P_DEGREENO", degree),
                //            new SqlParameter("@P_BRANCHNO", branch),
                //            new SqlParameter("@P_SEMESTERNO", semester),
                //         new SqlParameter("@P_COLLEGE_ID", colgid),

                //            new SqlParameter("@P_OP", SqlDbType.Int)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_EXAM_REGIST_STATUS", objParams, true);
                //        if (ret != null)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                public int UpdateExamRegStatus(string idnos, int sess, int scheme, int degree, int branch, int semester, int colgid, int Ua_no, string Ip_Address)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_SESSIONNO", sess),
                            new SqlParameter("@P_SCHEMENO", scheme),
                            new SqlParameter("@P_DEGREENO", degree),
                            new SqlParameter("@P_BRANCHNO", branch),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_COLLEGE_ID", colgid),
                            new SqlParameter("@P_UA_NO",Ua_no),
                            new SqlParameter("@P_IP_ADDRESS",Ip_Address),
                         
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_EXAM_REGIST_STATUS", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateExamInchargeStatus(string idnos, int sess, int scheme, int degree, int branch, int semester, int colgid, int Ua_no, string Ip_Address, int prev_status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_SESSIONNO", sess),
                            new SqlParameter("@P_SCHEMENO", scheme),
                            new SqlParameter("@P_DEGREENO", degree),
                            new SqlParameter("@P_BRANCHNO", branch),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_COLLEGE_ID", colgid),
                            new SqlParameter("@P_UA_NO",Ua_no),
                            new SqlParameter("@P_IP_ADDRESS",Ip_Address),
                            new SqlParameter("@P_PREV_STATUS",prev_status),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_EXAM_INCHARGE_STATUS", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                ////public int UpdateResultlockStatus(string idno, int semester)
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);

                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                ////        SqlParameter[] objParams = new SqlParameter[] 
                ////        {
                ////            //Parameters for MARKS
                ////            new SqlParameter("@P_IDNO", idno),
                ////            new SqlParameter("@P_SEMESTERNO", semester),

                ////            new SqlParameter("@P_OP", SqlDbType.Int)
                ////        };
                ////        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                ////        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_EXAM_LOCK_STATUS", objParams, true);
                ////        if (ret != null)
                ////        {
                ////            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                ////        }
                ////        else
                ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                ////    }
                ////    return retStatus;
                ////}

                public int UpdateResultlockStatus(string idno, int schemeno, int semester, int sessionno, string username, string ipaddress, int flag, int STUDENTTYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_LOCK_UNLOCK_BY", username),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@FLAG", flag),
                            new SqlParameter("@P_STUDENTTYPE", STUDENTTYPE),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_EXAM_LOCK_STATUS", objParams, true);
                        if (ret != null)
                        {
                            //retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            retStatus = Convert.ToInt32(ret);
                        }

                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CheckMarksLocked(int sessionno, string idnos, int schemeno, int semesterno)
                {
                    int Status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        {
                            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objParams[1] = new SqlParameter("@P_IDNO", idnos);
                            objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                            objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                            objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;
                        };
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GET_STATUS_MARK_ENTRY_LOCK", objParams, true);
                        Status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return Status;
                }

                public int UpdateMoveStatus(int sess, int colg, int course, int intext)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sess),
                            new SqlParameter("@P_COLLEGE_ID", colg),
                             new SqlParameter("@P_COURSENO", course),
                            new SqlParameter("@P_INT_EXT", intext),
                         
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_MOVE_STATUS", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetStudentsForRevalMarksEntry(int sessiono, int courseno, string ccode, string exam, string Status, string valuer)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                //        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_EXAM", exam);
                //        //objParams[4] = new SqlParameter("@P_IDNO", idno);
                //        objParams[4] = new SqlParameter("@P_FLAG", Status);
                //        objParams[5] = new SqlParameter("@P_VALUER", valuer);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_REVAL_MARK_ENTRY", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                public DataSet GetStudentsForRevalMarksEntry(int sessiono, int courseno, string ccode, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        //objParams[4] = new SqlParameter("@P_IDNO", idno);

                        //COMMENTED BY SRIKANTH 

                        //objParams[4] = new SqlParameter("@P_FLAG", Status);
                        //objParams[5] = new SqlParameter("@P_VALUER", valuer);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_REVAL_MARK_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }
                //public int ChangeRevalMarks(int sessionno, int courseno, string idnos, string oldmarks, string v1Marks, int lock_status, int ua_no, string ipaddress, string Remarks, string Status, string valuer)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        {
                //            //Parameters for MARKS
                //            new SqlParameter("@P_SESSIONNO", sessionno),
                //            new SqlParameter("@P_COURSENO", courseno),
                //           // new SqlParameter("@P_CCODE", ccode),
                //            new SqlParameter("@P_STUDIDS", idnos),
                //            new SqlParameter("@P_OLDMARKS", oldmarks),
                //            new SqlParameter("@P_VMARKS",v1Marks),
                //            //new SqlParameter("@P_V2MARKS",v2Marks),
                //            new SqlParameter("@P_LOCK", lock_status),
                //            new SqlParameter("@P_UA_NO", ua_no),
                //            new SqlParameter("@P_IPADDRESS", ipaddress),
                //            new SqlParameter("@P_REMARKS", Remarks),
                //            new SqlParameter("@P_FLAG",Status),
                //            new SqlParameter("@P_VALUER", valuer),
                //            new SqlParameter("@P_OP", SqlDbType.Int)

                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_REVAL_MARK_UG", objParams, true);
                //        if (ret != null && ret.ToString() == "1")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                public int ChangeRevalMarks(int sessionno, int courseno, string idnos, string oldmarks, string v1Marks, int lock_status, int ua_no, string ipaddress, string Remarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                           // new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_OLDMARKS", oldmarks),
                            new SqlParameter("@P_VMARKS",v1Marks),
                            //new SqlParameter("@P_V2MARKS",v2Marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_REMARKS", Remarks),
                            //new SqlParameter("@P_FLAG",Status),
                            //new SqlParameter("@P_VALUER", valuer), commented by srikanth 14-03-2020
                            new SqlParameter("@P_OP", SqlDbType.Int)

                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_REVAL_MARK", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UnlockRevalMark(int sessionno, int courseno, string idno, int examno, string opno, string valuer)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        {
                //            //Parameters for AUDIT POINTS
                //            new SqlParameter("@P_SESSIONNO", sessionno),
                //            new SqlParameter("@P_COURSENO", courseno),
                //            new SqlParameter("@P_STUDIDS", idno),
                //            new SqlParameter("@P_EXAMNO", examno),
                //            new SqlParameter("@P_FLAG", opno),
                //            new SqlParameter("@P_VALUER", valuer),
                //            new SqlParameter("@P_OUT", SqlDbType.Int)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UNLOCK_REVAL_MARK", objParams, true);
                //        if (ret != null && ret.ToString() == "1")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateAuditPoint --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UnlockRevalMark(int sessionno, int courseno, string idno, int examno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for AUDIT POINTS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS", idno),
                            new SqlParameter("@P_EXAMNO", examno),
                            //new SqlParameter("@P_FLAG", opno),    MODIFIED BY SRIKANTH 14-03-2020
                            //new SqlParameter("@P_VALUER", valuer),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UNLOCK_REVAL_MARK", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateAuditPoint --> " + ex.ToString());
                    }
                    return retStatus;
                }


                //added on [21-10-2016] for marks entry form BY OPERATOR.
                public DataSet GetONExams_NEW_FOR_OPERATOR(int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);


                        ds = objSQLHelper.ExecuteDataSetSP("PR_GET_EXAM_NAMES_FOR_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams_NEW-> " + ex.ToString());
                    }
                    return ds;
                }

                //method to get marks entry status added on[21-10-2016]

                public DataSet GetCourse_MarksEntryStatus_FOR_OPERATOR(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_FOR_OPERATOR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForTeacher_FOR_OPERATOR(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_FOR_OPERATOR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added on [21-10-2016]

                public DataSet GetStudentsForMarkEntry_For_Operator(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_BY_OPERATOR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public int UpdateMarkEntryTA1(int sessionno, int courseno, string idnos, string t1marks, string t2marks, string t3marks, string t4marks, int lock_status, int th_pr, int ua_no, string exam, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@IDNOS", idnos),

                            new SqlParameter("@SESSIONNO", sessionno),
                            new SqlParameter("@COURSENO", courseno),
                            
                            //Mark Fields
                            new SqlParameter("@T1MARK", t1marks),
                            new SqlParameter("@T2MARK", t2marks),
                            new SqlParameter("@T3MARK", t3marks),
                            new SqlParameter("@T4MARK", t4marks),
                             
                          // new SqlParameter("@P_CCODE",ccode),
                             new SqlParameter("@P_EXAM",exam),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@UA_NO",ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                         
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_UPDATE_MARK_TA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStudentsForMarkEntryTA(int sessiono, int ua_no, int courseno, int sectionno, int subid, string Exam, string ipaddress)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_TA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// ADDED BY: M. REHBAR SHEIKH ON 09-04-2019
                /// </summary>
                public DataSet GetNoDuesStudentData(int sessionno, int degreeno, int schemeno, int semesterno, int colgid, int StudentType, int UA_NO, int UA_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_collegeid", colgid),
                    new SqlParameter("@P_StudentType", StudentType),
                    //new SqlParameter("@P_UA_NO", UA_NO),
                    new SqlParameter("@P_UA_TYPE", UA_TYPE)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_NO_DUES_STUDENT_DATA", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetNoDuesStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                /// <summary>
                /// ADDED BY: M. REHBAR SHEIKH ON 16-04-2019
                /// </summary>
                public DataSet GetNoDuesFeeDetails(int SESSIONO, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_SESSIONNO", SESSIONO),
                    new SqlParameter("@P_IDNO", idno)
                 
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_NO_DUES_FEE_DETAILS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetNoDuesStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                /// <summary>
                /// ADDED BY: M. REHBAR SHEIKH ON 27-05-2019
                /// </summary>
                public DataSet GetAllIncompleteMarkEntry_ForResultProcess(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),  
                            new SqlParameter("@P_COLLEGE_ID", College_Id) 
                            //new SqlParameter("@P_COURSENO", courseno) 
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_MARK_ENTRY_NOT_DONE_FOR_RESULTPROCESS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteMarkEntry_ForResultProcess --> " + ex.Message);
                    }
                    return ds;
                }




                /// <summary>
                /// ADDED BY: M. REHBAR SHEIKH ON 27-05-2019
                /// </summary>
                public DataSet GetAllIncompleteLockStatus_ForResultProcess(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                            new SqlParameter("@P_COLLEGE_ID", College_Id)   
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_LOCK_ENTRY_NOT_DONE_FOR_RESULTPROCESS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteLockStatus_ForResultProcess --> " + ex.Message);
                    }
                    return ds;
                }

                //ADDED ON 14-02-2020 BY VAISHALI to get count for mark entry done/not done
                public DataSet GetMarkEntryNotDonecount(int CollegeID, int SessionNo, int degreeno, int branchno, int schemeno, int semesterno, int studenttype, int Int_Ext, int subid, int courseno, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", studenttype);
                        objParams[7] = new SqlParameter("@P_EXAMNO", Int_Ext);
                        objParams[8] = new SqlParameter("@P_SUBID", subid);
                        objParams[9] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[10] = new SqlParameter("@P_SUBEXAMTYPE", subexam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CHK_MARK_ENTRY_DONE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarkEntryNotDonecount-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED ON 15-02-2020 BY VAISHALI to get RECORD for mark entry done/not done REPORT
                public DataSet GetMarkEntryNotDoneRecordReport(int SessionNo, int school, int schemeno, int semesterno, string exam, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", school); // added by safal gupta on 17042021
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_EXAMNO", exam);
                        objParams[5] = new SqlParameter("@P_SUBEXAMTYPE", subexam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEST_MARKS_ENTRY_NOTDONE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarkEntryNotDoneRecordReport-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED ON 15-02-2020 BY VAISHALI to get RECORD for mark lock entry done/not done REPORT
                public DataSet GetLockMarkEntryNotDoneRecordReport(int SessionNo, int school, int schemeno, int semesterno, string exam, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", school);  // added by safal gupta on 17042021
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_EXAMNO", exam);
                        objParams[5] = new SqlParameter("@P_SUBEXAMTYPE", subexam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEST_LOCK_ENTRY_NOTDONE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetLockMarkEntryNotDoneRecordReport-> " + ex.ToString());
                    }

                    return ds;
                }



                //ADDED ON 21-03-2020 BY VAISHALI to marks for marks entry by operator
                public DataSet GetLockMarkEntryForOperator(int SessionNo, int courseno, int op_id, int is_mids_ends)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_OP_ID", op_id);
                        objParams[3] = new SqlParameter("@P_IS_MIDS_ENDS", is_mids_ends);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_MARKS_FOR_OP", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetLockMarkEntryForOperator-> " + ex.ToString());
                    }

                    return ds;
                }

                //added on 26-03-2020 by Vaishali TO MAINTAIN LOG OF MATCHED MARKS ENTRY COMPARISON
                public int InsMatchMarksCompLog(int sessionno, int schemeno, int semesterno, int examtype, int ua_no, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IP_ADDRESS", ipaddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_COMP_LOG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.InsMatchMarksCompLog --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added on 26-03-2020 by Vaishali TO GET THE RECORDS OF MATCHED MARKS OF THE STUDENTS FOR COMPARISON FOR REPORT
                public DataSet GetMatchedMarkCompareReport(int colg, int sessionno, int schemeno, int semesterno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {

                        new SqlParameter("@P_COLLEGE_ID", colg),
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_SCHEMENO", schemeno),
	                    new SqlParameter("@P_SEMESTERNO", semesterno),
	                    new SqlParameter("@P_EXAMTYPE", examtype),
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MATCHED_BY_OPERATOR_REPORT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetMatchedMarkCompareReport() --> " + ex.Message);
                    }
                    return ds;
                }



                //ADDED ON 26-03-2020 BY VAISHALI TO GET THE COMPARE STATUS LOG EXCEL REPORT
                public DataSet GetCOMPSTATUSREPORT(int SessionNo, int schemeno, int semesterno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAMTYPE", examtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COMP_STATUS_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCOMPSTATUSREPORT-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED ON 28-03-2020 BY VAISHALI TO GET THE MARK DETAILS OF THE STUDENT FOR MARKS CORRECTION
                public DataSet GetMarksForCorrection(int SessionNo, int idno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_EXAMTYPE", examtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MARKS_BY_IDNO_FOR_MARKCORRECTION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarksForCorrection-> " + ex.ToString());
                    }

                    return ds;
                }


                //added on 28-03-2020 by Vaishali TO CORRECT THE MARKS/LOCK OF FAC/OPERATOR 2
                public int UpdCorrectMarksbyidno(int sessionno, int idno, int examtype, string courseno, string op1marks, string op2marks, int markslock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_OP1MARKS", op1marks);
                        objParams[5] = new SqlParameter("@P_OP2MARKS", op2marks);
                        objParams[6] = new SqlParameter("@P_LOCK", markslock);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CORRECT_MARKS_BY_IDNO", objParams, true);
                        if (ret != null && ret.ToString() == "2")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.UpdCorrectMarksbyidno() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }
                #region Practical Subject Marks Entry Added by Mahesh on Dated 31-03-2020

                /// <summary>
                /// Get Practical Course Marks Entry Status
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="ua_no"></param>
                /// <param name="subid"></param>
                /// <returns></returns>
                public DataSet GetPracticalCourse_MarksEntryStatus(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_PRACTICAL_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPracticalCourse_MarksEntryStatus-> " + ex.ToString());
                    }

                    return ds;
                }


                /// <summary>
                /// Get Practical Course Sub Exam Name with min and max marks
                /// </summary>
                /// <param name="SessionNo"></param>
                /// <param name="CourseNo"></param>
                /// <param name="UA_NO"></param>
                /// <param name="SubId"></param>
                /// <returns></returns>
                public DataSet GetPractical_Course_SubExamName(int SessionNo, int CourseNo, int UA_NO, int SubId, int ExamNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[3] = new SqlParameter("@P_SUBID", SubId);
                        objParams[4] = new SqlParameter("@P_EXAMNO", ExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_PRACTICAL_SUBEXAM_COURSES_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPractical_Course_SubExamName-> " + ex.ToString());
                    }

                    return ds;
                }



                /// <summary>
                /// Get Student Information for Marks Entry Purpose.
                /// </summary>
                /// <param name="SESSIONO"></param>
                /// <param name="UA_NO"></param>
                /// <param name="CCODE"></param>
                /// <param name="SECTIONNO"></param>
                /// <param name="SUBID"></param>
                /// <param name="SEMESTERNO"></param>
                /// <param name="EXAMNO"></param>
                /// <param name="COURSENO"></param>
                /// <returns></returns>
                public DataSet GetStudentsForPracticalCourseMarkEntry(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// Get Practical Sub Exam Name
                /// </summary>
                /// <param name="SessionNo"></param>
                /// <param name="Patternno"></param>
                /// <returns></returns>
                public DataSet GetPracticalSubExamsBySubjectType(int SessionNo, int Patternno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_PATTERNNO", Patternno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PRACTICAL_SUB_EXAMS_BYSUBID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPracticalSubExamsBySubjectType-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion Practical Subject Marks Entry

                #region Marks Entry Dashboard 18-04-2020

                /// <summary>
                /// Get Marks Entry Dashboard Data.
                /// </summary>
                /// <param name="SessionNo"></param>
                /// <param name="SchemeNo"></param>
                /// <param name="SemesterNo"></param>
                /// <param name="ExamName"></param>
                /// <param name="Query_Value"></param>
                /// <returns></returns>
                public DataSet GetMarksEntryDashboard(int SessionNo, int SchemeNo, int SemesterNo, string ExamName, int Query_Value)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[3] = new SqlParameter("@P_EXAM", ExamName);
                        objParams[4] = new SqlParameter("@P_QUERY_VALUE", Query_Value);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_MARKSENTRY_CHART_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarksEntryDashboard-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion Marks Entry Dashboard.

                //********ADDED BY: M. REHBAR SHEIKH ON 08-06-2018***********
                public DataSet GetExamDashboardDetails(int sessionid, string examtype, string subExam, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONID", sessionid),
                            new SqlParameter("@P_COLLEGE_ID", college_id),
                            //new SqlParameter("@P_DEGREENO", degree),
                            //new SqlParameter("@P_BRANCHNO", branch),
                            //new SqlParameter("@P_SEMESTERNO", sem),    //FOR MARK COMPARISON DEPENDS ON SESSIONNO
                            new SqlParameter("@P_EXAMNO", examtype),
                            new SqlParameter("@P_SUBEXAM",subExam)
                        };
                        // sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_MARKENTRY_STATUS_CONSOLIDATED_DASHBOARD", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetExamDashboardDetails() --> " + ex.Message);
                    }
                    return ds;
                }

                /// <summary>
                /// ADDED BY DILEEP
                /// ON 30-04-2020
                /// </summary>
                /// <param name="Sessionno"></param>
                /// <param name="ExamType"></param>
                /// <returns></returns>
                public DataSet GetAllExamMarkEntryStatusforExcel(int sessionid, string ExamType, string subExam, int College_Id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONID", sessionid),
                            new SqlParameter("@P_COLLEGE_ID",College_Id),
                            //new SqlParameter("@P_DEGREENO", degree),
                            //new SqlParameter("@P_BRANCHNO", branch),
                            //new SqlParameter("@P_SEMESTERNO", sem),    //FOR MARK COMPARISON DEPENDS ON SESSIONNO
                            new SqlParameter("@P_EXAMNO", ExamType),
                            new SqlParameter("@P_SUBEXAM",subExam),
                        };
                        // sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_MARKENTRY_STATUS_CONSOLIDATED_DASHBOARD_FOR_EXCEL", sqlParams);
                    }
                    catch (Exception EX)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogic.MarksEntryController.GetAllExamMarkEntryStatusforExcel()-->" + EX.Message);
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Patil - 25042020
                /// </summary>
                /// <param name="sessiono"></param>
                /// <param name="ua_no"></param>
                /// <param name="ccode"></param>
                /// <param name="sectionno"></param>
                /// <param name="subid"></param>
                /// <param name="semesterno"></param>
                /// <param name="Exam"></param>
                /// <param name="COURSENO"></param>
                /// <returns></returns>
                public DataSet GetStudentsForAttendanceMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_AUTOGENERATE_ATTENDANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForAttendanceMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                //added on 25-04-2020 by Vaishali to check mark entry by Op2 in Result Process
                public DataSet GetMarkEntryNotDoneByOP2(int SessionNo, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_ALL_MARK_ENTRY_NOT_DONE_BY_OP2_FOR_RESULTPROCESS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarkEntryNotDoneByOP2-> " + ex.ToString());
                    }

                    return ds;
                }

                //added on 25-04-2020 by Vaishali to check lock entry by Op2 in Result Process
                public DataSet GetLockEntryNotDoneByOP2(int SessionNo, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_ALL_LOCK_ENTRY_NOT_DONE_BY_OP2_FOR_RESULTPROCESS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetLockEntryNotDoneByOP2-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added Nikhil on Dated 15-02-2021
                public DataSet GetCourseForTeacher_IA(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_FOR_IA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher_IA-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetPracticalCourse_MarksEntryStatus_IA(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_IA_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPracticalCourse_MarksEntryStatus-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetONExamsActivityBySubjectType_IA(int sessionno, int ua_type, int ua_no, int page_link, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_BYSUBID_FOR_IA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetPracticalSubExamsBySubjectType_IA(int SessionNo, int Patternno, int Subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_PATTERNNO", Patternno);
                        objParams[2] = new SqlParameter("@P_SUBID", Subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PRACTICAL_SUB_EXAMS_BYSUBID_FOR_IA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPracticalSubExamsBySubjectType_IA-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet CourseEligibilityCheck_IA(int sessionno, string courseccode, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_CCODE", courseccode),
                            new SqlParameter("@P_COURSENO", courseno),                           
                           
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_COURSE_ELIGIBILITY_CHK_MARKENTRY_FOR_IA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.CourseEligibilityCheck_IA --> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForPracticalCourseMarkEntry_IA(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetIA_Course_SubExamName(int SessionNo, int CourseNo, int UA_NO, int SubId, int ExamNo)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[3] = new SqlParameter("@P_SUBID", SubId);
                        objParams[4] = new SqlParameter("@P_EXAMNO", ExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_IA_SUBEXAM_COURSES_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetPractical_Course_SubExamName-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatus_IA(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid != 2) // added by S.Patil
                        {
                            // ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);

                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_FOR_IA", objParams);

                        }
                        else
                        {
                            //ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM", objParams);
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM_FOR_IA", objParams);

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetONExamsActivityBySubjectType_IA_EXM(int sessionno, int ua_type, int ua_no, int page_link, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_BYSUBID_IA_EXM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_IA_EXM(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_IA_EXM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_IA_EXM-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntrySubExam_IA_EXM(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string subexam, int SubExamNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUBEXAM", subexam);
                        objParams[9] = new SqlParameter("@P_SUBEXAMNO", SubExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_SUBEXAM_IA_EXM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForAttendanceMarkEntry_IA_EXM(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_AUTOGENERATE_ATTENDANCE_IA_EXM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForAttendanceMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetONPracticalExamsBySubjectType_Prac(int Patternno, int sessiono, int usertype, int pageno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PATTERNNO", Patternno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[2] = new SqlParameter("@P_UA_TYPE", usertype);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", pageno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_MULTIPLE_EXAMS_BYSUBID_PRAC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONPracticalExamsBySubjectType-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_EXM(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", courseno);
                        //objParams[8] = new SqlParameter("@P_SUBEXAM", subexam);
                        //objParams[9] = new SqlParameter("@P_SUBEXAMNO", SubExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_EXM-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryForPracExam(int sessionno, int courseno, string ccode, string idnos, string marks, int semno, int lock_status, string exam, int examno, int sectionno, int th_pr, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                             new SqlParameter("@P_SEMESTERNO", semno),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_SECTIONNO",sectionno ),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_SUBEXAM", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_PRACEXAM", objParams, true);



                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntryForPracExam --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int Calculate_Consolidate_Marks(int Sessionno, int Schemeno, int Semesterno, int Sectionno, int SubId, int Courseno, string CCODE, int ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SECTIONNO", Sectionno),
                            new SqlParameter("@P_SUBID",SubId),
                            new SqlParameter("@P_COURSENO",Courseno),
                            new SqlParameter("@P_CCODE", CCODE),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_IA_BEST_OF_MARKS_CALCULATION", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.Calculate_Consolidate_Marks --> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// NEW METHOD ADDED BY DILEEP KARE
                /// DATE:16-03-2021
                /// </summary>
                /// <param name="SESSIONNO"></param>
                /// <param name="semesterno"></param>
                /// <param name="audit_date"></param>
                /// <param name="Remark"></param>
                /// <returns></returns> 
                public DataSet GetViewExamFormStatus(int sessiono, int College_id, int Degreeno, int branchno, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VIEW_EXAM_FORM_STATUS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetViewExamFormStatusForExcel-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// NEW METHOD ADDED BY DILEEP KARE
                /// DATE:16-03-2021
                /// </summary>
                /// <param name="SESSIONNO"></param>
                /// <param name="semesterno"></param>
                /// <param name="audit_date"></param>
                /// <param name="Remark"></param>
                /// <returns></returns>      
                public DataSet GetViewExamFormStatusForExcel(int sessiono, int College_id, int Degreeno, int branchno, int schemeno, int semesterno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VIEW_EXAM_FORM_STATUS_FOR_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetViewExamFormStatusForExcel-> " + ex.ToString());
                    }

                    return ds;
                }


                public int GradeCardNumberGeneration(int Sessionno, string idnos, int College_id, int Degreeno, int Branchno, int Semesterno, int ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                     {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_DEGREENO", Degreeno),
                            new SqlParameter("@P_BRANCHNO", Branchno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_COLLEGE_ID",College_id),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_CARD_NUMBER_GENERATION", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GradeCardNumberGeneration --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added Mahesh on Dt.03-05-2021-- ResultProcess.aspx
                public int UpdateMarksEntryLockStatusAfterMarksEntryCompleted(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),  
                            new SqlParameter("@P_COLLEGE_ID", College_Id),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_MARKSENTRYLOCKSTATUS_AFTERMARKSENTRY_COMPLETED", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarksEntryLockStatusAfterMarksEntryCompleted --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added Mahesh on Dated 23/06/2021

                public DataSet GetLevelMarksEntryCourseDetail(int CourseNo, int SchemeNo, int SubjectType)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objDataAccessLayer = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SCHEMENO",SchemeNo),
                            new SqlParameter("@P_SUBJECTTYPE",SubjectType),
                            new SqlParameter("@P_COURSENO",CourseNo)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GetLevelMarksEntryCourseDetail", objParams);
                        return ds;
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                }

                public DataSet GetLevelMarksEntryExamDetail(int SubId, int Pattern, int MainExamNo, int ExamType)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objDataAccessLayer = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SUBID",SubId),
                            new SqlParameter("@P_PATTERNNO",Pattern),
                            new SqlParameter("@P_MAINEXAMNO",MainExamNo),
                            new SqlParameter("@P_EXAMTYPE",ExamType)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GetLevelMarksEntryExamDetail", objParams);
                        return ds;
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                }

                #region Call Dispose Methode- Added Mahesh on Dated 06/23/2021

                private bool Disposed;

                ~MarksEntryController()
                {
                    this.Dispose(false);
                }

                public void Dispose()
                {
                    this.Dispose(true);

                    //using GC.Collect Namespace.
                    GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool Disposing)
                {
                    if (!Disposed)
                    {
                        if (Disposing)
                        {
                            //Release manage resourses here.
                        }
                    }

                    Disposed = true;
                }

                #endregion Call Dispose Methode- Added Mahesh on Dated 06/23/2021

                public int UpdateMarkEntryNew(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);

                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetMarkEntryNotDonecount(int CollegeID, int SessionNo, int degreeno, int branchno, int schemeno, int semesterno, int studenttype, int Int_Ext, int subid, int courseno, string subexam, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", studenttype);
                        objParams[7] = new SqlParameter("@P_EXAMNO", Int_Ext);
                        objParams[8] = new SqlParameter("@P_SUBID", subid);
                        objParams[9] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[10] = new SqlParameter("@P_SUBEXAMTYPE", subexam);
                        objParams[11] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CHK_MARK_ENTRY_DONE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarkEntryNotDonecount-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetStudentsForMarkEntryNew(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_New", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }


                #region End Sem Markentry
                public int InsertMarkEntrybyAdmin(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, string SubExam, int Examno, string subexamcomponetname, string ans)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            //added Mahesh on Dated 24/06/2021
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                            new SqlParameter("@P_ANSSHEET", ans),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_RCPIT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertMarkEntrybyAdmin(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            //added Mahesh on Dated 24/06/2021
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                          
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                public int GradeGenaerationNew(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Semesterno, int Schemeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
{
//Parameters for MARKS
new SqlParameter("@P_SESSIONNO", sessionno),
new SqlParameter("@P_COURSENO", Courseno),
new SqlParameter("@P_STUDIDS", idnos),
new SqlParameter("@P_TH_PR", th_pr),
new SqlParameter("@P_UA_NO", ua_no),
new SqlParameter("@P_IPADDRESS", ipaddress),
new SqlParameter("@P_SEMESTERNO", Semesterno),
new SqlParameter("@P_SCHEMENO", Schemeno),
new SqlParameter("@P_OP", SqlDbType.Int)
};
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #region ADDED BY NARESH BEERLA FOR ENDSEM MARK ENTRY NIT GOA PATCH ON DT 28022022

                public int UpdateMarkEntryAllNew(int sessionno, int courseno, string ccode, string idnos, string marks, string totmarks, string grade, string Gpoint, string totPercent, string lgrade, string max, string min, string point, string totStudent, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title, int degreeNo, int sectionno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos), 
                            //Mark Fields 
                            new SqlParameter("@P_MARKS", marks), 
                            new SqlParameter("@P_TOTMARKS", totmarks), 
                            new SqlParameter("@P_GRADE", grade),
                            new SqlParameter("@P_GRADEPOINT", Gpoint),
                            new SqlParameter("@P_PERCENT", totPercent),
                            new SqlParameter("@P_LVGRADE", lgrade), 
                            new SqlParameter("@P_MAX", max), 
                            new SqlParameter("@P_MIN", min), 
                            new SqlParameter("@P_POINT", point), 
                            new SqlParameter("@P_TOTALSTUD", totStudent), 
                            new SqlParameter("@P_DEGREENO", degreeNo), 
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_SEMESTERNO", semesterno ),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_All_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntryAll --> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetAllGradesSection(int sessionno, int subid, int courseno, int ua_no, int degreeno, int sectionno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_UANO", ua_no);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_GRADES_SECTION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetEndExamMarksDataExcel(int sessionno, int courseno, int sectionno, int semesterno, int prev_status, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_UA_NO ", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_GET_STUD_FOR_MARKENTRY_ENDSEM_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetEndExamMarksDataExcel-> " + ex.ToString());
                    }

                    return ds;
                }

                public int ProcessResultAll(int sessionno, int schemeno, int semesterno, string idno, int exam, string ipAdddress, int colg, int STUDENTTYPE, int ua_no, int covidFlag, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ipAdddress),
                            new SqlParameter("@P_COLLEGE_ID",colg),   
                            //new SqlParameter("@P_RESULT_TYPE",STUDENTTYPE),  
                            //new SqlParameter("@P_TYPE",Type),  
                            new SqlParameter("@P_UA_NO",ua_no),  
                             new SqlParameter("@P_REG_COVID19",covidFlag),// added by s.patil - 28072020
                             new SqlParameter("@P_ORGID",OrgID),
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING", objParams, true); //Added by roshan 28-12-2016

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResultAll --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetMarkEntryStatus(int Sessionno, int Schemeno, int Sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                            new SqlParameter("@P_SCHEMENO",Schemeno),
                            new SqlParameter("@P_SEMESTERNO", Sem),    //FOR MARK COMPARISON DEPENDS ON SESSIONNO
                           
                        };
                        // sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_TESTMARK_STATUS", sqlParams);
                    }
                    catch (Exception EX)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogic.MarksEntryController.GetAllExamMarkEntryStatusforExcel()-->" + EX.Message);
                    }
                    return ds;
                }


                public int UpdateMarkEntryAllNew(int sessionno, int courseno, string ccode, string idnos, string marks, string totmarks, string grade, string Gpoint, string totPercent, string lgrade, string max, string min, string point, string totStudent, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title, int degreeNo, int sectionno, int semesterno, string FinalConversion)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos), 
                            //Mark Fields 
                            new SqlParameter("@P_MARKS", marks), 
                            new SqlParameter("@P_TOTMARKS", totmarks), 
                            new SqlParameter("@P_GRADE", grade),
                            new SqlParameter("@P_GRADEPOINT", Gpoint),
                            new SqlParameter("@P_PERCENT", totPercent),
                            new SqlParameter("@P_LVGRADE", lgrade), 
                            new SqlParameter("@P_MAX", max), 
                            new SqlParameter("@P_MIN", min), 
                            new SqlParameter("@P_POINT", point), 
                            new SqlParameter("@P_TOTALSTUD", totStudent), 
                            new SqlParameter("@P_DEGREENO", degreeNo), 
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_SEMESTERNO", semesterno ),
                            new SqlParameter("@P_CONVERSION", FinalConversion ),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_All_NEW_CRESCENT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntryAll --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int flagReval,
                                    string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_FLAGREVAL", flagReval),

                             new SqlParameter("@P_TO_EMAIL", to_email),//below parameter added by raju bitode on dated 14.03.2019 for maintained security log..
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),

                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int ProcessResultAll(int sessionno, int schemeno, int semesterno, string idno, int exam, string ipAdddress, int colg, int ua_no, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
{
//Parameters for MARKS
new SqlParameter("@P_SESSIONNO", sessionno),
new SqlParameter("@P_SCHEMENO", schemeno),
new SqlParameter("@P_SEMESTER_NO", semesterno),
new SqlParameter("@P_IDNO",idno),
new SqlParameter("@P_EXAM", exam),
new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
new SqlParameter("@P_IPADDRSSS",ipAdddress),
new SqlParameter("@P_COLLEGE_ID",colg),
//new SqlParameter("@P_RESULT_TYPE",STUDENTTYPE),
//new SqlParameter("@P_TYPE",Type),
new SqlParameter("@P_UA_NO",ua_no),
// new SqlParameter("@P_REG_COVID19",covidFlag),// added by s.patil - 28072020
new SqlParameter("@P_ORGID",OrgID),
new SqlParameter("@P_MSG", SqlDbType.Int)
};
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_JSS", objParams, true); //Added by roshan 28-12-2016



                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResultAll --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForUnlock(int schemeNo, int sessionNo, int branchNo, int semesterNo, int studType, int subType, int courseNo, int facNo, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_STUD_TYPE", studType);
                        objParams[5] = new SqlParameter("@P_SUBID", subType);
                        objParams[6] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[7] = new SqlParameter("@P_UA_NO", facNo);
                        objParams[8] = new SqlParameter("@P_EXAM", exam);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_BY_SCHEME_FAC_COURSE_SUBTYPE_FOR_UNLOCK", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }

                //--------------------------------------------------------------------------------------
                public int ToggleMarkEntryLockByAdmin(string idnoS, string lockS, int sessionno, int degreeno, int branchno, int schemeno, int semno, int courseno, int prev_stat, int ua_no, string examType, string ipaddress)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                                   
                           /*01*/ new SqlParameter("@P_IDNOS",idnoS),             
                           /*02*/ new SqlParameter("@P_LOCK",lockS),              
                           /*03*/ new SqlParameter("@P_SESSIONNO",sessionno),     
                           /*04*/ new SqlParameter("@P_DEGREENO",degreeno),       
                           /*05*/ new SqlParameter("@P_BRANCHNO",branchno),       
                           /*06*/ new SqlParameter("@P_SCHEMENO",schemeno),       
                           /*07*/ new SqlParameter("@P_SEMESTERNO",semno),        
                           /*08*/ new SqlParameter("@P_COURSENO",courseno),
                           /*09*/ new SqlParameter("@P_PREV_STATUS",prev_stat),
                           /*10*/ new SqlParameter("@P_UA_NO",ua_no),             
                           /*11*/ new SqlParameter("@P_EXAM",examType),           
                           /*12*/ new SqlParameter("@P_IPADDRESS",ipaddress),     
                           /*13*/ new SqlParameter("@P_OUT",SqlDbType.Int)        
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_TOGGLE_FOR_SELECTED_STUD", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.ToggleMarkEntryLockByAdmin --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Backlog Mark Entry related Methods Added by Naresh Beerla on dt 02052022

                public DataSet GetCourseForTeacherEndSem_Backlog(int sessionno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_END_SEM_BACKLOG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacherEndSem_Backlog-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryAllNew_Backlog(int sessionno, int courseno, string ccode, string idnos, string marks, string totmarks, string grade, string Gpoint, string totPercent, string lgrade, string max, string min, string point, string totStudent, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title, int degreeNo, int semesterno, string FinalConversion)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos), 
                            //Mark Fields 
                            new SqlParameter("@P_MARKS", marks), 
                            new SqlParameter("@P_TOTMARKS", totmarks), 
                            new SqlParameter("@P_GRADE", grade),
                            new SqlParameter("@P_GRADEPOINT", Gpoint),
                            new SqlParameter("@P_PERCENT", totPercent),
                            new SqlParameter("@P_LVGRADE", lgrade), 
                            new SqlParameter("@P_MAX", max), 
                            new SqlParameter("@P_MIN", min), 
                            new SqlParameter("@P_POINT", point), 
                            new SqlParameter("@P_TOTALSTUD", totStudent), 
                            new SqlParameter("@P_DEGREENO", degreeNo), 
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                         //   new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_SEMESTERNO", semesterno ),
                            new SqlParameter("@P_CONVERSION", FinalConversion ),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_All_NEW_BACKLOG_ENDSEM_CRESCENT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntryAll --> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetAllGradesSection_Backlog(int sessionno, int subid, int courseno, int ua_no, int degreeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_UANO", ua_no);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                        //     objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_GRADES_SECTION_BACKLOG_ENDSEM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetEndExamMarksDataExcel_Backlog(int sessionno, int courseno, int semesterno, int prev_status, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        // objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[2] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_UA_NO ", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_GET_STUD_FOR_MARKENTRY_BACKLOG_ENDSEM_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetEndExamMarksDataExcel-> " + ex.ToString());
                    }

                    return ds;
                }
                #endregion

                #region Revaluation & Photocopy Markentry



                public int InsertRevaluationMarkEntrybyAdmin(int sessionno, int Courseno, int Schemeno, int Semesterno, int Degreeno, int Branchno, string idnos, string marks, int lock_status, int ua_no, string ipaddress, int Orgid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
{
//Parameters for MARKS
new SqlParameter("@P_SESSIONNO", sessionno),
new SqlParameter("@P_SCHEMENO", Schemeno),
new SqlParameter("@P_SEMESTERNO", Semesterno),
new SqlParameter("@P_COURSENO", Courseno),
new SqlParameter("@P_DEGREENO", Degreeno),
new SqlParameter("@P_BRANCHNO", Branchno),
new SqlParameter("@P_STUDIDS", idnos),
new SqlParameter("@P_MARKS", marks),
new SqlParameter("@P_LOCK", lock_status),
new SqlParameter("@P_UANO", ua_no),
new SqlParameter("@P_IPADDRESS", ipaddress),
new SqlParameter("@P_ORGID", Orgid),
new SqlParameter("@P_OUT", SqlDbType.Int)
};
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_NEW", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REVAL_MARK_ENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }



                #endregion

                //added by prafull for Re_exam on date 07/07/2022

                public DataSet GetStudentsForMarkEntryNew_for_backlog(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_New_FOR_RE_EXAM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetStudentsForUnlock(int schemeNo, int sessionNo, int branchNo, int semesterNo, int studType, int subType, int courseNo, int facNo, string exam, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_STUD_TYPE", studType);
                        objParams[5] = new SqlParameter("@P_SUBID", subType);
                        objParams[6] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[7] = new SqlParameter("@P_UA_NO", facNo);
                        objParams[8] = new SqlParameter("@P_EXAM", exam);
                        objParams[9] = new SqlParameter("@P_SUBEXAM", subexam);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_BY_SCHEME_FAC_COURSE_SUBTYPE_FOR_UNLOCK", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }
                //===========================================================================================


                public int ToggleMarkEntryLockByAdmin(string idnoS, string lockS, int sessionno, int degreeno, int branchno, int schemeno, int semno, int courseno, int prev_stat, int ua_no, string examType, string subexam_type, string ipaddress)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                                   
                           /*01*/ new SqlParameter("@P_IDNOS",idnoS),             
                           /*02*/ new SqlParameter("@P_LOCK",lockS),              
                           /*03*/ new SqlParameter("@P_SESSIONNO",sessionno),     
                           /*04*/ new SqlParameter("@P_DEGREENO",degreeno),       
                           /*05*/ new SqlParameter("@P_BRANCHNO",branchno),       
                           /*06*/ new SqlParameter("@P_SCHEMENO",schemeno),       
                           /*07*/ new SqlParameter("@P_SEMESTERNO",semno),        
                           /*08*/ new SqlParameter("@P_COURSENO",courseno),
                           /*09*/ new SqlParameter("@P_PREV_STATUS",prev_stat),
                           /*10*/ new SqlParameter("@P_UA_NO",ua_no),             
                           /*11*/ new SqlParameter("@P_EXAM",examType),  
                           /*12*/ new SqlParameter("@P_SUBEXAM",subexam_type),
                           /*13*/ new SqlParameter("@P_IPADDRESS",ipaddress),     
                           /*14*/ new SqlParameter("@P_OUT",SqlDbType.Int)        
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_TOGGLE_FOR_SELECTED_STUD", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.ToggleMarkEntryLockByAdmin --> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Added by Sachin A on 05/08/2022 for To Save Mark Entry blod file uplod log 
                /// 
                /// </summary>               
                public int InsertMarkEntryBlobLog(int sessionno, int courseno, string ccode, string idnos, string marks, int semno, int lock_status, string exam, int examno, int sectionno, int th_pr, int ua_no, string ipaddress, string examtype, string file_name)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_SEMESTERNO", semno),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_SECTIONNO",sectionno ),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_FILENAME",file_name),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_EXAM_MARK_ENTRY_BLOB", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public int ReGradeGenaerationNew_RCPIT(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Semesterno, int Schemeno)
                //                {
                //                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //                    try
                //                    {
                //                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //                        SqlParameter[] objParams = new SqlParameter[]
                //                        {
                //                        //Parameters for MARKS
                //                        new SqlParameter("@P_SESSIONNO", sessionno),
                //                        new SqlParameter("@P_COURSENO", Courseno),
                //                        new SqlParameter("@P_STUDIDS", idnos),
                //                        new SqlParameter("@P_TH_PR", th_pr),
                //                        new SqlParameter("@P_UA_NO", ua_no),
                //                        new SqlParameter("@P_IPADDRESS", ipaddress),
                //                        new SqlParameter("@P_SEMESTERNO", Semesterno),
                //                        new SqlParameter("@P_SCHEMENO", Schemeno),
                //                        new SqlParameter("@P_OP", SqlDbType.Int)
                //                        };
                //                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                //                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_REGRADE_ALLOTMENT_NEW_RCPIT", objParams, true);
                //                        if (ret != null && ret.ToString() == "1")
                //                        {
                //                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //                        }
                //                        else
                //                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                //                    }
                //                    catch (Exception ex)
                //                    {
                //                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //                    }
                //                    return retStatus;
                //                }

                public int ReGradeGenaerationNew_RCPIT(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Semesterno, int Schemeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", Courseno),
                        new SqlParameter("@P_STUDIDS", idnos),
                        new SqlParameter("@P_TH_PR", th_pr),
                        new SqlParameter("@P_UA_NO", ua_no),
                        new SqlParameter("@P_IPADDRESS", ipaddress),
                        new SqlParameter("@P_SEMESTERNO", Semesterno),
                        new SqlParameter("@P_SCHEMENO", Schemeno),
                        new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_REGRADE_ALLOTMENT_NEW_RCPIT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForPracticalCourseMarkEntry_IA(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO, string subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA_TEST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetONExams_cc(int sessionno, int ua_type, int ua_no, int page_link)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateMarkEntry_SupplyExamReg(int sessionno, int courseno, string ccode, string idnos, string marks, string totmarks, string grade, string Gpoint, string totPercent, string lgrade, string max, string min, string point, string totStudent, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title, int degreeNo, int semesterno, string FinalConversion)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos), 
                            //Mark Fields 
                            new SqlParameter("@P_MARKS", marks), 
                            new SqlParameter("@P_TOTMARKS", totmarks), 
                            new SqlParameter("@P_GRADE", grade),
                            new SqlParameter("@P_GRADEPOINT", Gpoint),
                            new SqlParameter("@P_PERCENT", totPercent),
                            new SqlParameter("@P_LVGRADE", lgrade), 
                            new SqlParameter("@P_MAX", max), 
                            new SqlParameter("@P_MIN", min), 
                            new SqlParameter("@P_POINT", point), 
                            new SqlParameter("@P_TOTALSTUD", totStudent), 
                            new SqlParameter("@P_DEGREENO", degreeNo), 
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                         //   new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_SEMESTERNO", semesterno ),
                            new SqlParameter("@P_CONVERSION", FinalConversion ),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_SUPPLY_CRESCENT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntryAll --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForMarkEntryNew_cc(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_NEW_CC", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetCourse_MarksEntryStatus_cc(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid == 2 || subid == 4) // added by S.Patil
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM_CC", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_CC", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatusForMArkEntry(int sessionno, int ua_no, int subid, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_Courseno", courseno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_FOR_END_SEM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //added by prafull on dt 28092022

                //public int UpdateMarkEntryNewAdmin(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                //{
                //    int retStatus;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //        SqlParameter[] objParams = new SqlParameter[] 
                //        { 
                //            //Parameters for MARKS
                //            new SqlParameter("@P_SESSIONNO", sessionno),

                //            //Added By Abhinay Lad [17-07-2019]
                //            new SqlParameter("@P_COURSENO", courseno),

                //            new SqlParameter("@P_CCODE", ccode),
                //            new SqlParameter("@P_STUDIDS", idnos),
                //            //Mark Fields
                //            new SqlParameter("@P_MARKS", marks),
                //            //Parameters for Final Lock 
                //            new SqlParameter("@P_LOCK", lock_status),
                //            new SqlParameter("@P_EXAM", exam),
                //            //Parameters for ACD_LOCKTRAC TABLE 
                //            new SqlParameter("@P_TH_PR", th_pr),
                //            new SqlParameter("@P_UA_NO", ua_no),
                //            new SqlParameter("@P_IPADDRESS", ipaddress),
                //            new SqlParameter("@P_EXAMTYPE", examtype),

                //            //Added By Abhinay Lad [17-07-2019]
                //            new SqlParameter("@P_FLAGREVAL", FlagReval),

                //            new SqlParameter("@P_TO_EMAIL", to_email),
                //            new SqlParameter("@P_FROM_EMAIL", from_email),
                //            new SqlParameter("@P_SMSMOB", smsmobile),
                //            new SqlParameter("@P_FLAG", flag),
                //            new SqlParameter("@P_SMS_TEXT", sms_text),
                //            new SqlParameter("@P_EMAIL_TEXT", email_text),
                //            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                //            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                //            new SqlParameter("@P_SECTIONNO", SectionNo),
                //            new SqlParameter("@P_OP", SqlDbType.Int)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;


                //        //PKG_STUD_INSERT_MARK_WITH_RULE_CC
                //        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_ADMIN_CC", objParams, true);  //added by prafull on dt 30092022

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                //    }
                //    return retStatus;
                //}




                public int UpdateMarkEntryNewAdmin(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_ADMIN_CC", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by prafull on dt 30092022

                public DataSet GetStudentsForMarkEntryadmin_new(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID, int examno, int subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                        objParams[10] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[11] = new SqlParameter("@P_SUBEXAMNO", subexamno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }


                public int ReGradeGenaerationNew(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Semesterno, int Schemeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_RE_GRADE_ALLOTMENT_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCourse_GradeEntryStatus_cc(int sessionno, int courseno, string ccode, int lock_status, string exam, int thpr, int ua_no, string ipaddress, string examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_LOCK", lock_status);
                        objParams[4] = new SqlParameter("@P_EXAM ", exam);
                        objParams[5] = new SqlParameter("@P_TH_PR ", thpr);
                        objParams[6] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", examtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADE_ENTRY_DATA", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }
                public int InsertGradeEntryBlobLog(int sessionno, int courseno, string ccode, string idnos, string grade, int lock_status, string exam, int examno, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_STUDIDS", idnos);
                        objParams[4] = new SqlParameter("@P_GRADES ", grade);
                        objParams[5] = new SqlParameter("@P_LOCK ", lock_status);
                        objParams[6] = new SqlParameter("@P_EXAM", exam);
                        objParams[7] = new SqlParameter("@P_TH_PR", examno);
                        objParams[8] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[10] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[11] = new SqlParameter("@P_OP", SqlDbType.Int);

                        objParams[11].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUD_INSERT_GRADES", objParams, true));
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomConfigController.AddRoomConfig()-> " + ex.ToString());
                    }
                }
                public DataSet GetCourseForTeacher_Admin_RCPIT(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_RCPIT_Admin", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetONExams_RCPIT_ADMIN(int sessionno, int ua_type, int ua_no, int page_link)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_RCPIT_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateMarkEntryNew1(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);

                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_RCPIT_ADMIN", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCourse_MarksEntryStatus_RECPIT_ADMIN(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid == 2 || subid == 4) // added by S.Patil
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM_RCPIT_ADMIN", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_RECPIT_ADMIN", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatus_RECPIT_ADMIN(int sessionno, int ua_no, int subid, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        if (subid == 2 || subid == 4) // added by S.Patil
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM_RCPIT_ADMIN", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_RECPIT_ADMIN", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntryNew_RCPIT_ADMIN(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_New_RECPIT_ADMIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetStudentsForPracticalCourseMarkEntry_IA_ADMIN_RCPIT(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO, string subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA_RCPIT_ADMIN", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA_TEST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public int UpdateMarkEntryForSubExam_RCPIT_ADMIN(int sessionno, int courseno, string ccode, string idnos, string marks, int semno, int lock_status, string exam, int examno, int sectionno, int th_pr, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                             new SqlParameter("@P_SEMESTERNO", semno),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_SECTIONNO",sectionno ),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_SUBEXAM_RCPIT_ADMIN", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //aaded by lalit for  added Attendance marks calculation
                public DataSet GetStudentsForMarkEntryNew_for_AttendanceMarks(int sessiono, int schemeno, int ua_no, string ccode, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_ATTENDANCE_NEW1", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetAllIncompleteGradeStatus_ForResultProcess(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                            new SqlParameter("@P_COLLEGE_ID", College_Id)   
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_GRADE_ENTRY_NOT_DONE_FOR_RESULTPROCESS", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteLockStatus_ForResultProcess --> " + ex.Message);
                    }
                    return ds;
                }

                // ADDED BY SHAHBAZ AHMAD FOR RAJAGIRI END SEM MARKS
                public int SaveScoreEntry(int userno, int schemeno, int sessionno, int semesterno, int courseno, string Mode, DataTable dt, string ipaddress)
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    int retStatus = 0;
                    try
                    {
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_MODE", Mode);
                        objParams[5] = new SqlParameter("@P_ACD_MARKENTRY_RAJAGIRI", dt);
                        objParams[6] = new SqlParameter("@P_USERNO", userno);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARK_ENTRY_RAJAGIRI", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.SaveScoreEntry --> " + ex.ToString());
                    }

                    return retStatus;
                }


                //added by prafull on dt 10012023  for Revaluation Grade Generation


                public int RevalGradeGeneration(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Schemeno, int Semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
{
//Parameters for MARKS
new SqlParameter("@P_SESSIONNO", sessionno),
new SqlParameter("@P_COURSENO", Courseno),
new SqlParameter("@P_STUDIDS", idnos),
new SqlParameter("@P_TH_PR", th_pr),
new SqlParameter("@P_UA_NO", ua_no),
new SqlParameter("@P_IPADDRESS", ipaddress),
new SqlParameter("@P_SEMESTERNO", Semesterno),
new SqlParameter("@P_SCHEMENO", Schemeno),
new SqlParameter("@P_OP", SqlDbType.Int)
};
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_NEW", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_FOR_REVAL", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForUnlock_for_reval(int schemeNo, int sessionNo, int branchNo, int semesterNo, int studType, int subType, int courseNo, int facNo, string exam, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_STUD_TYPE", studType);
                        objParams[5] = new SqlParameter("@P_SUBID", subType);
                        objParams[6] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[7] = new SqlParameter("@P_UA_NO", facNo);
                        objParams[8] = new SqlParameter("@P_EXAM", exam);
                        objParams[9] = new SqlParameter("@P_SUBEXAM", subexam);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_BY_SCHEME_FAC_COURSE_SUBTYPE_FOR_UNLOCK_REVALUATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }
                public int ToggleMarkEntryLockByAdmin_Reval(string idnoS, string lockS, int sessionno, int degreeno, int branchno, int schemeno, int semno, int courseno, int prev_stat, int ua_no, string examType, string subexam_type, string ipaddress)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
{
 new SqlParameter("@P_IDNOS",idnoS),
 new SqlParameter("@P_LOCK",lockS),
 new SqlParameter("@P_SESSIONNO",sessionno),
 new SqlParameter("@P_DEGREENO",degreeno),
 new SqlParameter("@P_BRANCHNO",branchno),
 new SqlParameter("@P_SCHEMENO",schemeno),
 new SqlParameter("@P_SEMESTERNO",semno),
 new SqlParameter("@P_COURSENO",courseno),
 new SqlParameter("@P_PREV_STATUS",prev_stat),
 new SqlParameter("@P_UA_NO",ua_no),
 new SqlParameter("@P_EXAM",examType),
 new SqlParameter("@P_SUBEXAM",subexam_type),
 new SqlParameter("@P_IPADDRESS",ipaddress),
 new SqlParameter("@P_OUT",SqlDbType.Int)
};
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_TOGGLE_FOR_SELECTED_STUD_REVALUATION", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.ToggleMarkEntryLockByAdmin --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //added by PRAFULL 24_01_2023
                #region
                //ADDED BY PRAFULL ON DT 23012023


                public int UpdateMarkEntryNew_RCPIT(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_RCPIT", objParams, true);


                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMarkEntryNew_CRESCENT(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_CRESCENT", objParams, true);


                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateMarkEntryNew_CC(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);

                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_CC", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetStudentsForMarkEntryadmin_cc(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                #region End Sem Markentry Admin_CC
                public int InsertMarkEntrybyAdmin_cc(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            //added Mahesh on Dated 24/06/2021
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_CC", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                //ADDED BY GAURAV 21_01_2023 FOR CPUK/H           
                #region End Sem Markentry CPUH/K

                public int InsertMarkEntrybyAdmin_CPU(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, int Sectionno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                           
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SECTIONNO", Sectionno),                              
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_COMMON_CODE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region Internal Markentry CPUH/K

                public int UpdateMarkEntryNew_CPU(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                           
                            new SqlParameter("@P_SESSIONNO", sessionno),                            
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                           
                            new SqlParameter("@P_FLAGREVAL", FlagReval),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_COMMON_CODE", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                #region SHOW STUD FOR MARKENTRY CPUH/K
                public DataSet GetStudentsForMarkEntryNew_CPU(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_NEW_COMMON_CODE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                #endregion



                //added by prafull on dt 02-02-2023 for grade allotment mit

                public int GradeGenaerationNew_MIT(string idnos, int sessionno, int Schemeno, int th_pr, int Semesterno, int Courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                             new SqlParameter("@P_IDNOS", idnos),
                             new SqlParameter("@P_SESSIONNO", sessionno),
                             new SqlParameter("@P_SCHEME", Schemeno),                             
                             new SqlParameter("@P_SUBID", th_pr),
                             new SqlParameter("@P_SEMESTERNO", Semesterno),
                             new SqlParameter("@P_COURSENO", Courseno),
                            
                            // new SqlParameter("@P_UA_NO", ua_no),
                            // new SqlParameter("@P_IPADDRESS", ipaddress),                            
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PASSING_CRITERIA_GRADE_ALLOTEMENT_AND_UPDATION_MIT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by prafull on dt 02-02-2023 for grade allotment mit
                public int GradeGenaerationNew_Regenerate_MIT(string idnos, int sessionno, int Schemeno, int th_pr, int Semesterno, int Courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                             new SqlParameter("@P_IDNOS", idnos),
                             new SqlParameter("@P_SESSIONNO", sessionno),
                             new SqlParameter("@P_SCHEME", Schemeno),                             
                             new SqlParameter("@P_SUBID", th_pr),
                             new SqlParameter("@P_SEMESTERNO", Semesterno),
                             new SqlParameter("@P_COURSENO", Courseno),
                            
                            // new SqlParameter("@P_UA_NO", ua_no),
                            // new SqlParameter("@P_IPADDRESS", ipaddress),                            
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PASSING_CRITERIA_GRADE_ALLOTEMENT_AND_UPDATION_REGENARTE_MIT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added By Sachin A dt on 04042023 for Revaluation Mark Entry Crescent
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="Courseno"></param>
                /// <param name="Schemeno"></param>
                /// <param name="Semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="idnos"></param>
                /// <param name="marks"></param>
                /// <param name="lock_status"></param>
                /// <param name="ua_no"></param>
                /// <param name="ipaddress"></param>
                /// <param name="Orgid"></param>
                /// <param name="grade"></param>
                /// <param name="Gdpoint"></param>
                /// <param name="FinalConversion"></param>
                /// <param name="section"></param>
                /// <param name="totPer"></param>
                /// <returns></returns>
                public int InsertRevaluationMarkEntryCrescent(int sessionno, int Courseno, int Schemeno, int Semesterno, int Degreeno, int Branchno, string idnos, string marks, int lock_status, int ua_no, string ipaddress, int Orgid, string grade, string Gdpoint, string FinalConversion, int section, string totPer)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_DEGREENO", Degreeno),
                            new SqlParameter("@P_BRANCHNO", Branchno),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UANO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_ORGID", Orgid),

                            new SqlParameter("@P_LVGRADE", grade),
                            new SqlParameter("@P_GDPOINT", Gdpoint),
                            new SqlParameter("@P_CONVERSION", FinalConversion ),
                            new SqlParameter("@P_PERCENT", totPer),
                            new SqlParameter("@P_SECTIONNO", section),

                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_NEW", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REVAL_MARK_ENTRY_CRESCENT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }


                #region SHOW STUD FOR MARKENTRY CPUH/K BY ADMIN END
                public DataSet GetStudentsForMarkEntryNew_CPU_admin(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_NEW_COMMON_CODE_Admin", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }


                #endregion
                #region  ALl Endsem MarkEntry  CPUH/K admin end
                public int InsertMarkEntrybyAdmin_CPU_ADMIN(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, int Sectionno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                           
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SECTIONNO", Sectionno),                              
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_COMMON_CODE_ADMIN", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
                #region  ALl internal MarkEntry  CPUH/K admin end
                public int UpdateMarkEntryNew_CPU_ADMIN(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                           
                            new SqlParameter("@P_SESSIONNO", sessionno),                            
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                           
                            new SqlParameter("@P_FLAGREVAL", FlagReval),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_COMMON_CODE_ADMIN", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
                //ADDED BY PRAFULL 13_04_2023
                public int LockUnLockMarkEntryByAdmin(int sessionno, int semester, int schemeno, int examtype, int courseno, int section, int facultynotheory, int facultynopractical, int lockunlock, string ipaddress, int lock_by, string subexamtype, string remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //new SqlParameter("@P_COLLEGEID",college_id),
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_SEMESTER",semester),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_EXAMTYPE",examtype),                     
                            new SqlParameter("@P_COURSENO",courseno), 
                            new SqlParameter("@P_SECTION",section),
                            new SqlParameter("@P_UA_NO",facultynotheory),
                            new SqlParameter("@P_UA_NO_PRAC",facultynopractical),
                            new SqlParameter("@P_LOCK",lockunlock),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                           // new SqlParameter("@P_LDATE",ldate),
                           // new SqlParameter("@P_EXAMNO",examno),
                            //new SqlParameter("@P_OPID",opid),
                            new SqlParameter("@P_LOCKBY",lock_by),
                           
                            new SqlParameter("@P_SUBEXAMTYPE",subexamtype),    // added on 13-02-2020 by Vaishali
                            new SqlParameter("@P_REMARK",remark)              // added by prafull on dt 13042023
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_UNLOCK", objParams, false);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ProcessResultAll(int sessionno, int schemeno, int semesterno, string idno, int exam, string ipAdddress, int colg, int STUDENTTYPE, int ua_no, int Type, string to_email, string from_email, string cc_emails, string smsmobile, int flag, string sms_text, string email_text)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ipAdddress),
                            new SqlParameter("@P_COLLEGE_ID",colg), 
                            //new SqlParameter("@P_COLLEGE_CODE",colg),  
                            new SqlParameter("@P_RESULT_TYPE",STUDENTTYPE),  
                            new SqlParameter("@P_TYPE",Type),  
                            new SqlParameter("@P_UA_NO",ua_no),  
                            //------added For ACD_RESULTPROCESSING_LOG by Hemanth on [2019-03-25]
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_CC_EMAILS", cc_emails),
                            new SqlParameter("@P_SMSMOBILE", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            //---------------------------------------------------
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_JSS", objParams, true); //Added by roshan 28-12-2016
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_JSS_Crescent", objParams, true); //Added by roshan 28-12-2016

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #region Added by gaurav 04_05_2023 For componant wise mark entry  cpuk/h

                public int UpdateMarkEntryNewAdmin_cpu(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),                           
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                          
                            new SqlParameter("@P_FLAGREVAL", FlagReval),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_COMMON_CODE_CPU", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertMarkEntrybyAdmin_CPU_ADMIN_COMPONANT(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, int Sectionno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {                                          
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //new SqlParameter("@P_SUB_EXAM", subexam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),                           
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SECTIONNO", Sectionno),                              
                            new SqlParameter("@P_SUBEXAM", SubExam),
                            new SqlParameter("@P_EXAMNO", Examno),
                            new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_COMMON_CODE_ADMIN_COMPONANT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                public int RevalGradeGeneration_Rcpiper(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Schemeno, int Semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", Courseno),
                        new SqlParameter("@P_STUDIDS", idnos),
                        new SqlParameter("@P_TH_PR", th_pr),
                        new SqlParameter("@P_UA_NO", ua_no),
                        new SqlParameter("@P_IPADDRESS", ipaddress),
                        new SqlParameter("@P_SEMESTERNO", Semesterno),
                        new SqlParameter("@P_SCHEMENO", Schemeno),
                        new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_NEW", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_REVAL_RCPIPER", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertRevaluationMarkEntrybyAdminRcpiper(int sessionno, int Courseno, int Schemeno, int Semesterno, int Degreeno, int Branchno, string idnos, string marks, int lock_status, int ua_no, string ipaddress, int Orgid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_DEGREENO", Degreeno),
                            new SqlParameter("@P_BRANCHNO", Branchno),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UANO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_ORGID", Orgid),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN_NEW", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REVAL_MARK_ENTRY_RCPIPER", objParams, true);

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Direct Grade Entry for external exam by faculty DAIICT Added By Injamam
                public DataSet GetCourseForTeacherGrade(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TEACHER_COURSES_FOR_GRADE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetONExams_Grade(int sessionno, int ua_type, int ua_no, int page_link)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[3] = new SqlParameter("@P_PAGE_LINK", page_link);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateMarkEntry_Grade_CC(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_FLAGREVAL", FlagReval),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_CC", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertGradeEntry_External(int sessionno, int courseno, string ccode, string idnos, string grade, int lock_status, string exam, int examno, int ua_no, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_STUDIDS", idnos);
                        objParams[4] = new SqlParameter("@P_GRADES ", grade);
                        objParams[5] = new SqlParameter("@P_LOCK ", lock_status);
                        objParams[6] = new SqlParameter("@P_EXAM", exam);
                        objParams[7] = new SqlParameter("@P_TH_PR", examno);
                        objParams[8] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[10] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[11] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUD_INSERT_GRADES_EXTERNAL", objParams, true));
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomConfigController.AddRoomConfig()-> " + ex.ToString());
                    }
                }

                public DataSet GetCourse_GradeEntryStatus_External(int sessionno, int courseno, string ccode, int lock_status, string exam, int thpr, int ua_no, string ipaddress, string examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_LOCK", lock_status);
                        objParams[4] = new SqlParameter("@P_EXAM ", exam);
                        objParams[5] = new SqlParameter("@P_TH_PR ", thpr);
                        objParams[6] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", examtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADE_ENTRY_DATA_EXTERNAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_Grade(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_GRADE_CC", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_Grade_for_backlog(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);
                        objParams[9] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_New_FOR_RE_EXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatus_External(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid == 2 || subid == 4)
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_GET_TEACHER_COURSES_GRADEMARKS_ENTRY_STATUS_SUBEXAM", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_GET_TEACHER_COURSES_GRADE_MARKS_ENTRY_STATUS", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForPracticalCourseMarkEntry_Grade(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO, string subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateMarkEntryNew_Grade(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_FLAGREVAL", FlagReval),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion



                #region new methods for Degree Completion criteria added by Injamam Ansari 26_08_2023
                public DataSet GetDegreeCompletionCriteria()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DEGREE_COMPLETION_CONFIG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetDegreeCompletionGradeCriteria(int dcccno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DCCC_NO", dcccno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DEGREE_COMPLETION_CREDIT_CONFIG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }

                public int InsertDegreeCriteriaConfig(int dcccno, int schemeno, decimal min_creadit, decimal min_garde, string prereqsub, string exemptsub, int college_id, int adconfig)
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    int retStatus = 0;
                    try
                    {
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_DCCC_NO", dcccno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_MIN_CREDIT", min_creadit);
                        objParams[3] = new SqlParameter("@P_MIN_GARDE", min_garde);
                        objParams[4] = new SqlParameter("@P_PREREQ_SUBJECT", prereqsub);
                        objParams[5] = new SqlParameter("@P_EXEMPT_SUBJECT", exemptsub);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[7] = new SqlParameter("@P_ADVANCE_CONFIG", adconfig);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_DEGREE_COMPLETION_CONFIG", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.SaveScoreEntry --> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertDegreeGradeCriteriaConfig(int dcccno, int schemeno, int course_cate_no, decimal min_grade, decimal max_garde)
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    int retStatus = 0;
                    try
                    {
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DCCC_NO", dcccno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSE_CATE_NO", course_cate_no);
                        objParams[3] = new SqlParameter("@P_MIN_GRADE", min_grade);
                        objParams[4] = new SqlParameter("@P_MAX_GARDE", max_garde);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_DEGREE_GRADE_CONFIG", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.SaveScoreEntry --> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int LockUnlockDegreeCritera(int idno, int schemeno, int degreeno, int status, int lock_status)
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    int retStatus = 0;
                    try
                    {
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_STATUS", status);
                        objParams[4] = new SqlParameter("@P_LOCK_STATUS", lock_status);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_DEGREE_LOCK_UNLOCK_STATUS", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.SaveScoreEntry --> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentEligibilityStatus(string idnos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@IDNO", idnos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_DEGREE_COMPLETION_CRITERIA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForUnlock-> " + ex.ToString());
                    }

                    return ds;
                }
                #endregion


                public DataSet GetStudentsForMarkEntryadmin_new(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID, int examno, int subexamno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                        objParams[10] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[11] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        objParams[12] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntryadmin_cc(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                        objParams[10] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                // ADDED BY PRAFULL ON DT 26042023  

                public DataSet GetStudentsForPracticalCourseMarkEntry_Admin_IA(int SESSIONO, int UA_NO, string CCODE, int SECTIONNO, int SUBID, int SEMESTERNO, int EXAMNO, int COURSENO, string subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[2] = new SqlParameter("@P_CCODE", CCODE);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_SUBID", SUBID);
                        objParams[5] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA_ADMIN_CC", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_STUD_FOR_PRACTICAL_COURSES_MARKS_ENTRY_FOR_IA_TEST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForPracticalCourseMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                // added by prafull for substitute mark entry on dt:10-10-2023

                public DataSet GetStudentsForMarkEntry_Substitute(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_CRESCENT_SUBSTITUTE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryNew_crescent_substitute(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_CRESCENT_SUBSTITUTE", objParams, true);


                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForTeacher_Substitute(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_SUBSTITUTE_CRESCENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatus_Substitute(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        if (subid == 2 || subid == 4) // added by S.Patil
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBEXAM_SUBSTITUTE_CRESCENT", objParams);
                        }
                        else
                        {
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_SUBSTITUTE_CRESCENT", objParams);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }


                #region---------------------------------Added by Rohit.D on 01-01-2024-----------------------------

                public DataSet GetStudentsForMarkEntry_Remajor(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno, string SubExam, string SubExamName, int College_ID, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        //Added Mahesh on Dated 24/06/2021
                        objParams[7] = new SqlParameter("@P_SUBEXAM", SubExam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNAME", SubExamName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                        objParams[10] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_MARKENTRY_REMAJOR_CC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                #endregion
                #region End Sem Markentry Admin_CC
                public int InsertMarkEntryRemajor(int sessionno, int Courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int Semesterno, int Schemeno, int Sectionno, string SubExam, int Examno, string subexamcomponetname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             //Parameters for MARKS
                             new SqlParameter("@P_SESSIONNO", sessionno),
                             new SqlParameter("@P_COURSENO", Courseno),
                             new SqlParameter("@P_CCODE", ccode),
                             new SqlParameter("@P_STUDIDS", idnos),
                             //Mark Fields
                             new SqlParameter("@P_MARKS", marks),
                             //Parameters for Final Lock
                             new SqlParameter("@P_LOCK", lock_status),
                             new SqlParameter("@P_EXAM", exam),
                             //new SqlParameter("@P_SUB_EXAM", subexam),
                             new SqlParameter("@P_TH_PR", th_pr),
                             new SqlParameter("@P_UA_NO", ua_no),
                             new SqlParameter("@P_IPADDRESS", ipaddress),
                             new SqlParameter("@P_EXAMTYPE", examtype),
                             //added Mahesh on Dated 24/06/2021
                             new SqlParameter("@P_SEMESTERNO", Semesterno),
                             new SqlParameter("@P_SCHEMENO", Schemeno),
                             new SqlParameter("@P_SUBEXAM", SubExam),
                             new SqlParameter("@P_SECTIONNO",Sectionno),
                             new SqlParameter("@P_EXAMNO", Examno),
                             new SqlParameter("@P_SUBEXAMNAME", subexamcomponetname),
                             new SqlParameter("@P_OP", SqlDbType.Int)
                              };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_BY_ADMIN", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXTERNAL_MARK_REMAJOR", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                #region Revaluation Mark Entry
                public int RevalGradeGeneration_CC(int sessionno, int Courseno, string idnos, int th_pr, int ua_no, string ipaddress, int Schemeno, int Semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        //Parameters for MARKS
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", Courseno),
                        new SqlParameter("@P_STUDIDS", idnos),
                        new SqlParameter("@P_TH_PR", th_pr),
                        new SqlParameter("@P_UA_NO", ua_no),
                        new SqlParameter("@P_IPADDRESS", ipaddress),
                        new SqlParameter("@P_SEMESTERNO", Semesterno),
                        new SqlParameter("@P_SCHEMENO", Schemeno),
                        new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_REVAL_CC", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertRevaluationMarkEntrybyAdmin_CC(int sessionno, int Courseno, int Schemeno, int Semesterno, int Degreeno, int Branchno, string idnos, string marks, int lock_status, int ua_no, string ipaddress, int Orgid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_DEGREENO", Degreeno),
                            new SqlParameter("@P_BRANCHNO", Branchno),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UANO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_ORGID", Orgid),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REVAL_MARK_ENTRY_CC", objParams, true);

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                //Added by Suraj.Y for Mark Entry Status on 27-02-2024
                public int AddMarkEntryStatus(string CodeDesc, int CodeValue, string ShortName, int OrgID, string FinalGrade, double GradePoint, int QueryType)
                {
                    int status1 = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                       {
                          new SqlParameter("@P_Code_Desc",CodeDesc),
                          new SqlParameter("@P_Code_Value", CodeValue),
                          new SqlParameter("@P_Short_Name", ShortName),
                          new SqlParameter("@P_OrgID", OrgID),
                          new SqlParameter("@P_Final_Grade", FinalGrade),
                          new SqlParameter("@P_Grade_Point",GradePoint),
                          new SqlParameter("@P_QueryType",QueryType),
               
                   
                       };

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_STATUS_CODES", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status1 = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status1 = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        status1 = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status1;

                }

                //Added by Suraj Y on 27-02-2024
                public DataSet GetAllMarkEntryStatus(int QueryType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_QueryType", QueryType) };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MARK_ENTRY_STATUS_CODES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Added by Suraj Y on 27-02-2024
                public DataSet GetMarkEntryStatusBYID(int ID, int QueryType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ID", ID), new SqlParameter("P_QueryType", QueryType) };


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MARK_ENTRY_STATUS_CODES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Added By Suraj Y On 27-02-2024
                public int UPDMarkEntryStatus(string CodeDesc, int CodeValue, string ShortName, int OrgID, string FinalGrade, double GradePoint, int QueryType)
                {
                    int status1 = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                        new SqlParameter("@P_Code_Desc",CodeDesc),
                        new SqlParameter("@P_Code_Value", CodeValue),
                        new SqlParameter("@P_Short_Name", ShortName),
                        new SqlParameter("@P_OrgID", OrgID),
               
                        new SqlParameter("@P_Final_Grade", FinalGrade),
                        new SqlParameter("@P_Grade_Point",GradePoint),
                        new SqlParameter("@P_QueryType",QueryType),
               
                   
                     };

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_STATUS_CODES", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status1 = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status1 = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        status1 = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status1;

                }

                //Added by Suraj Y on 28-02-2024
                public int DeleteMarkEntryStatus(int Id, int QueryType)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ID", Id), new SqlParameter("@P_QueryType", QueryType) };


                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_STATUS_CODES", objParams, true);

                        if (Convert.ToInt32(obj) == 5)
                        {
                            status = Convert.ToInt32(CustomStatus.RecordNotFound);
                        }
                        else
                        {
                            status = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }

                public DataSet CheckDuplicateCodeValue(int CodeValue)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_Code_Value", CodeValue) };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MARK_ENTRY_STATUS_CODES", objParams);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;

                }
                //Added End

            }

        }
    }

}
