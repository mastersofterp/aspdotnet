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
            public class MarksEntryDetailReportController
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

             public DataSet GetCollegeDetail(int SessionNo,int UA_NO,int College_ID,int SubType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_COLLEGE_ID", College_ID),
                            new SqlParameter("@P_SUBTYPE", SubType),
                            new SqlParameter("@_INPUTID", 1),
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_MARKS_ENTRY_COURSE_DETAIL1", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetCollegeDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

             public DataSet GetCourseDetail1(int SessionNo, int CollegeID, int UA_NO, int SubType, int semester, int course)
             {
                 DataSet ds = null;
                 try
                 {
                     SQLHelper objDataAccess = new SQLHelper(_connectionString);
                     SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                            new SqlParameter("@P_COLLEGE_ID", CollegeID),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_SUBTYPE", SubType),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_COURSENO", course),
                            new SqlParameter("@_INPUTID", 2),
                        };

                     ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_MARKS_ENTRY_COURSE_DETAIL", sqlParams);

                 }
                 catch (Exception ex)
                 {
                     throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetCourseDetail() --> " + ex.Message + " " + ex.StackTrace);
                 }

                 return ds;
             }

                public DataSet GetMarksEntryComponentDetail(int SessionNo,int Courseno, int SubType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_SUBTYPE", SubType),
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_MARKS_ENTRY_EXAM_DETAIL_REPORT", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetMarksEntryComponentDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                public DataSet GetMarksEntryStudentDetail(int SessionNo, int CollegeID, int UA_NO,int Courseno,int SubType,int UA_Type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                            new SqlParameter("@P_COLLEGE_ID", CollegeID),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_SUBTYPE", SubType),
                            new SqlParameter("@P_UA_TYPE", UA_Type),
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_MARKS_ENTRY_STUDENT_DETAIL_REPORT", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetMarksEntryStudentDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                //added by safal gupta 26042021
                public int InsertMarkEntryMilSentLog(int From_Uano, int To_Ua_No, string Email_Subject, string Email_Body,int CourseNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SENDER_UANO", From_Uano);
                        objParams[1] = new SqlParameter("@P_RECEIVER_UANO",To_Ua_No);
                        objParams[2] = new SqlParameter("@P_MAIL_SUBJECT", Email_Subject);
                        objParams[3] = new SqlParameter("@P_MAIL_BODY", Email_Body);
                        objParams[4] = new SqlParameter("@P_COURSENO", CourseNo);

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_MARKS_ENTRY_MAIL_SENDING_INSERT_LOG", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryDetailReportController.InsertAnswerSheetMark-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added Mahesh on Dated 05-05-2021 for -ResultPublicationReport.aspx page.
                public DataSet GetResultPublicationDetailInExcel(int SessionNo, int CollegeID, int Degreeno, int Branchno, int SchemeNo, int SemesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                            new SqlParameter("@P_COLLEGE_ID", CollegeID),
                            new SqlParameter("@P_DEGREENO",Degreeno),//Added By Dileep Kare on 07.06.2021
                            new SqlParameter("@P_BRANCHNO", Branchno),
                            new SqlParameter("@P_SCHEMENO", SchemeNo),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_RESULT_PUBLICATION_REPORT_EXCEL", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetResultPublicationDetailInExcel() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }


                //Added Mahesh on Dated 05-05-2021 for -ResultPublicationReport.aspx page.
                public DataSet GetResultPublicationPendingDetailInExcel(int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", SessionNo),
                       
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_GET_RESULT_DECLARATION_PENDING", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryDetailReportController.GetResultPublicationDetailInExcel() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }


            }
        }
    }
}
