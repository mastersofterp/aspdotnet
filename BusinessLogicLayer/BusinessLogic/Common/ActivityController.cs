//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// CLASS NAME    : ACTIVITY CONTROLLER CLASS                                            
// CREATION DATE : 18-JUN-2009                                                          
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI                                    
// MODIFIED DATE : 07-OCT-2009 (NIRAJ D. PHALKE)                                        
// MODIFIED DESC : 
//======================================================================================

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

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ActivityController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetActivities()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_GET_ALL_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //public DataSet GetFeeItemAmounntRevaluation(int sessionno)
        //{

        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        //SqlParameter[] sqlParams = new SqlParameter[0];
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_AMOUNTFORFEEITEMREVALUATION", objParams);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
        //    }

        //    return ds;

        //}

        public DataSet GetFeeItemAmounntRevaluation(int sessionno)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //SqlParameter[] sqlParams = new SqlParameter[0];
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_AMOUNTFORFEEITEMREVALUATION", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
            }

            return ds;

        }
        public DataSet GetActivities(int actiityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ACTIVITY_NO", actiityNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_GET_ACTIVITY_BY_ID", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //modified on 06-04-2020 by Vaishali for Notification
        public int AddSessionActivity(Activity actiity)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ACTIVITY_CODE", actiity.ActivityCode),
                    new SqlParameter("@P_ACTIVITY_NAME", actiity.ActivityName ),
                    new SqlParameter("@P_USERTYPEID", actiity.UserTypes),
                    new SqlParameter("@P_EXAMNO", actiity.Exam_No),
                    new SqlParameter("@P_SUBEXAMNO", actiity.SubExam_No),
                    new SqlParameter("@P_PAGELINKS", actiity.Page_links),
                    new SqlParameter("@P_PREREQ_ACT_NO", actiity.Prereq_Act_No),
                     new SqlParameter("@ActivityTemplate", actiity.ActivityTemplate),
                     new SqlParameter("@ACTIVESTATUS", actiity.ActiveStatus),//Added By Rishabh
                     new SqlParameter("@P_ASSIGN",actiity.AssignFlag),//Added by Injamam 28-2-23
                    new SqlParameter("@P_ACTIVITY_NO", SqlDbType.Int),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_ACTIVITY", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.AddActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //modified on 06-04-2020 by Vaishali for Notification
        public int UpdateSessionActivity(Activity actiity)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ACTIVITY_NO", actiity.ActivityNo),
                    new SqlParameter("@P_ACTIVITY_CODE", actiity.ActivityCode),
                    new SqlParameter("@P_ACTIVITY_NAME", actiity.ActivityName),
                    new SqlParameter("@P_USERTYPEID", actiity.UserTypes),
                    new SqlParameter("@P_EXAMNO", actiity.Exam_No),
                    new SqlParameter("@P_SUBEXAMNO", actiity.SubExam_No),
                    new SqlParameter("@P_PAGELINKS", actiity.Page_links),
                    new SqlParameter("@P_PREREQ_ACT_NO", actiity.Prereq_Act_No),       
                    new SqlParameter("@ActivityTemplate", actiity.ActivityTemplate), 
                    new SqlParameter("@ACTIVESTATUS", actiity.ActiveStatus), //Added By Rishabh
                    new SqlParameter("@P_ASSIGN",actiity.AssignFlag),    //Added by Injamam 28-2-23
                };
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_UPD_ACTIVITY", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.UpdateActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);

                DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY", objParams);
                if (ds.Tables.Count > 0)
                    dtr = ds.Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.ActivityController.CheckActivity-> " + ex.ToString());
            }
            return dtr;
        }

        public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink, string degreeNo, string branchNo, string semesterNo)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY_BY_DEBRASE", objParams);
                if (ds.Tables.Count > 0)
                    dtr = ds.Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.ActivityController.CheckActivity-> " + ex.ToString());
            }
            return dtr;
        }
        public DataSet GetFeeItemAmounntENDSEM(int sessionno,int feeitemid)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                //SqlParameter[] sqlParams = new SqlParameter[0];
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_FEEITEMID", feeitemid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_AMOUNTFORFEEITEMENDSEM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
            }

            return ds;

        }
        //public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink)
        //{
        //    DataTableReader dtr = null;

        //    try
        //    {
        //        SQLHelper objsqlhelper = new SQLHelper(_connectionString);
        //        SqlParameter[] objParams = new SqlParameter[3];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
        //        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
        //        objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);

        //        DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY", objParams);
        //        if (ds.Tables.Count > 0)
        //            dtr = ds.Tables[0].CreateDataReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.ActivityController.CheckActivity-> " + ex.ToString());
        //    }
        //    return dtr;
        //}


        // added by safal gupta on 26022021

        //public DataSet getReportCourseTeacher(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_COURSE_TEACHER_ALLOTMENT_BY_HOD", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        // added by safal gupta on 26022021



        public DataSet getReportCourseTeacher(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_COURSE_TEACHER_ALLOTMENT_BY_HOD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021
        //public DataSet getExcelReportCourseTeacher(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_COURSE_TEACHER_ALLOTMENT_BY_HOD_EXCELS", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        public DataSet getExcelReportCourseTeacher(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_COURSE_TEACHER_ALLOTMENT_BY_HOD_EXCELS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added by safal gupta on 26022021

        //public DataSet getReportOfferedCourse(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_OFFERED_COURSES_BY_HOD ", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        public DataSet getReportOfferedCourse(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_OFFERED_COURSES_BY_HOD ", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added by safal gupta on 26022021

        //public DataSet getExcelReportOfferedCourse(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
        //        //[PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD_EXCEL
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_OFFERED_COURSES_BY_HOD_EXCEL", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportOfferedCourse(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                //[PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD_EXCEL
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_OFFERED_COURSES_BY_HOD_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021

        //public DataSet getReportTimeTableAllot(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_TIME_TABLE_ACTIVITY_BY_HOD", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        public DataSet getReportTimeTableAllot(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_TIME_TABLE_ACTIVITY_BY_HOD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021
        //public DataSet getExcelReportTimeTableAllot(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_TIME_TABLE_ACTIVITY_BY_HOD_EXCEL", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportTimeTableAllot(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_TIME_TABLE_ACTIVITY_BY_HOD_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021


        //public DataSet getReportTeacherStudAllot(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_TEACHER_STUDENT_ALLOTMENT_BY_HOD", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getReportTeacherStudAllot(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_TEACHER_STUDENT_ALLOTMENT_BY_HOD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021
        //public DataSet getExcelReportTeacherStudAllot(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_TEACHER_STUDENT_ALLOTMENT_BY_HOD_EXCELS", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportTeacherStudAllot(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_TEACHER_STUDENT_ALLOTMENT_BY_HOD_EXCELS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added by safal gupta on 26022021
        //PKG_ACD_GET_TRACK_ACTIVITY_FOR_TEACHER_STUDENT_ALLOTMENT_BY_HOD_EXCELS
        //public DataSet getReportBulKCourseReg(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getReportBulKCourseReg(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021
        //public DataSet getExcelReportBulKCourseReg(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD_EXCELS", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportBulKCourseReg(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACK_ACTIVITY_FOR_BULK_COURSE_REG_BY_HOD_EXCELS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021

        public DataSet getReportTeachingPlan(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_TEACHING_PLAN_ACTIVITYLOG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //PKG_ACD_GET_FACULTY_FOR_TEACHING_PLAN_ACTIVITYLOG_EXCELS
        // added by safal gupta on 26022021
        //public DataSet getExcelReportTeachingPlan(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_TEACHING_PLAN_ACTIVITYLOG_EXCELS", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportTeachingPlan(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_TEACHING_PLAN_ACTIVITYLOG_EXCELS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021

        //public DataSet getReportAttendance(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_ATTENDANCE__ACTIVITYLOG", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getReportAttendance(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_ATTENDANCE__ACTIVITYLOG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021

        public DataSet getExcelReportAttendance(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_ATTENDANCE__ACTIVITYLOG_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // added by safal gupta on 26022021


        //public DataSet getReportMarkEntry(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_MARK_ENTRY_ACTIVITY", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.getReportMarkEntry() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getReportMarkEntry(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_MARK_ENTRY_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.getReportMarkEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
      


        // added by safal gupta on 26022021
        //public DataSet getExcelReportMarkEntry(int Session)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_MARK_ENTRY_ACTIVITY_EXCEL", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.getReportMarkEntry() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet getExcelReportMarkEntry(int Session, int scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_MARK_ENTRY_ACTIVITY_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.getReportMarkEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021



        public DataSet getReportCourseRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_STUDENT_COURSE_REGISTRATION_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021

        public DataSet getExcelReportCourseRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_STUDENT_COURSE_REGISTRATION_ACTIVITY_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by safal gupta on 26022021
        public DataSet getReportExamRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_EXAM_REGISTRATION_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added by safal gupta on 26022021
        public DataSet getExcelReportExamRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_EXAM_REGISTRATION_ACTIVITY_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        // added by safal gupta on 26022021
        public DataSet getReportBackLogRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_STUDENT_BACKLOG_REGISTRATION_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added by safal gupta on 26022021
        public DataSet getExcelReportBackLogRegistration(int Session, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                sqlParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_TRACKING_STUDENT_BACKLOG_REGISTRATION_ACTIVITY_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //--------------Added by Mahesh On Dated 30-03-3031--------------
        public DataSet GetExamName()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACTIVITY_GET_EXAM_NAME", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetExamName() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetSubExam(int ExamNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];

                sqlParams[0] = new SqlParameter("@P_EXAMNO", ExamNo);
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACTIVITY_GET_SUBEXAM_NAME", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ActivityController.GetSubExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //--------------END 30-03-3031--------------



        //MODIFIED  BY SAFAL GUPTA ON 14042021

        public int AddNameOfActivity(string activityName, int points, int permissiblepoints, int colcode)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            //new SqlParameter("@P_ACADEMIC_YEAR",acdyr),
                             new SqlParameter("@P_ACTIVITY_NAME", activityName),
                             new SqlParameter("@P_POINTS", points),
                             new SqlParameter("@P_PERMISSIBLE_POINTS",permissiblepoints),
                             new SqlParameter("@P_COLLEGE_CODE",colcode),
                          

                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_MASTER_INSERT_ACTIVTY_NAME", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.AddNameOfActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //1****************************************************************************************

        public int UpdateNameOfActivity(int actno, string activityName, int points, int PermissiblePoint)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ACTIVITYNO", actno),
                             new SqlParameter("@P_ACTIVITY_NAME", activityName),
                             new SqlParameter("@P_POINTS", points),
                             new SqlParameter("@P_PERMISSIBLE_POINTS",PermissiblePoint),
                            //new SqlParameter("@P_ACADEMIC_YEAR", acdyr),

                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_MASTER_UPDATE_ACTIVTY_NAME", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.UpdateNameOfActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //2***********************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public SqlDataReader GetActivityDetails(int actno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", actno) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_MAR_MASTER_GET_BY_ACTIVITY_NO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetActivityDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }
        //3*****************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public DataSet GetAllActivity(int version)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_VERSON", version);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_MASTER_ACTIVITY_NAME", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllSlots() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //4******************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public int AddNameOfSubActivity(int actno, string activityName, int points, int permissible_point, int colcode, string srno)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ACTIVITYNO", actno),
                             new SqlParameter("@P_SUB_ACTIVITY_NAME", activityName),
                             new SqlParameter("@P_POINTS", points),
                             new SqlParameter("@P_PERMISSIBLE_POINTS", permissible_point),
                             new SqlParameter("@P_COLLEGE_CODE",colcode),
                             new SqlParameter ("@P_SR_NO",srno),

                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_SUB_ACTIVTY_NAME", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.AddNameOfSubActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //5***************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021
        public int UpdateNameOfSubActivity(int actno, string subactivityname, int points, int Permissiblepoint, string srno)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SUBACTIVITYNO", actno),
                             new SqlParameter("@P_SUB_ACTIVITY_NAME", subactivityname),
                             new SqlParameter("@P_POINTS", points),
                             new SqlParameter("@P_PERMISSIBLE_POINTS",Permissiblepoint),
                                new SqlParameter ("@P_SR_NO",srno),
                           

                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_SUB_ACTIVTY_NAME", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.UpdateNameOfSubActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //6********************************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public SqlDataReader GetSubActivityDetails(int actno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", actno) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_MAR_GET_BY_SUB_ACTIVITY_NO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetSubActivityDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }
        //7**************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public DataSet GetAllSubActivity(int actno)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_ACTIVITYNO", actno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_ALL_SUB_ACTIVITY_NAME", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllSubActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //8***************************************************************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public DataSet GetStudentDetails(int acdyr, int college, int Degree, int Branch, int facAdvisor, int activityno)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@P_ACADEMIC_YEAR", acdyr);
                sqlParams[1] = new SqlParameter("@P_COLLGE_ID", college);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", Degree);
                sqlParams[3] = new SqlParameter("@P_BRANCHNO", Branch);
                sqlParams[4] = new SqlParameter("@P_FAC_ADVISOR", facAdvisor);
                sqlParams[5] = new SqlParameter("@P_ACTIVITYNO", activityno);

                ds = objDataAccess.ExecuteDataSetSP("ACD_PKG_GET_STUDENT_FOR_MAR_ENTRY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //9********************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public DataSet GetSubActivity(int batchno, int college, int BranchDegree, int actno, string regno)
        {
            DataSet ds = null;
            try
            {


                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@P_ADMBATCH", batchno);
                sqlParams[1] = new SqlParameter("@P_COLLGE_ID", college);
                sqlParams[2] = new SqlParameter("@P_BRANCHNO", BranchDegree);
                sqlParams[3] = new SqlParameter("@P_ACTIVITYNO", actno);
                sqlParams[4] = new SqlParameter("@P_REGNO", regno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_ALL_SUB_ACTIVITY_NAME", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllSubActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //10*********************************************************************************************************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021


        public int AddMarActivityPoints(string regno, int idno, string studentname, int cuAcq, int branch, int college, int ua_no, int actno, int acdyr, int degree, DataTable dt)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_REGNO", regno),
                             new SqlParameter("@P_IDNO", idno),
                             new SqlParameter("@P_STUDENT_NAME", studentname),
                             new SqlParameter("@P_CURRENT_ACQ_POINTS",cuAcq),
                            // new SqlParameter ("@P_CURRENT_PER_POINTS",cuPer),
                             new SqlParameter("@P_BRANCHNO", branch),
                             new SqlParameter("@P_COLLEGE_ID", college),
                             new SqlParameter("@P_UA_NO",ua_no),
                             new SqlParameter ("@P_ACTIVITYNO",actno),
                             new SqlParameter("@P_ACADEMIC_YEAR", acdyr),
                               new SqlParameter("@P_DEGREENO", degree),
                             //new SqlParameter("@P_SUB_ACTIVITY_NO",subactno),
                            new SqlParameter("@P_UT_DT", dt),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("ACD_PKG_MAR_INSERT_STUDENT_ENTRY", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.AddMarActivityPoints() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //11*******************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021

        public DataSet GetAllSubActivityForMarEntry(int actno)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_ACTIVITYNO", actno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_ALL_SUB_ACTIVITY_FOR_MAR_ENTRY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllSubActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //12*******************************************************************************
        //ADDED BY SAFAL GUPTA ON 09042021
        public DataSet GetStudentDataSubActivityWise(int ActivityNo, int idno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_ACTIVITYNO", ActivityNo);
                sqlParams[1] = new SqlParameter("@P_IDNO", idno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_DATA_SUBACTIVITY_WISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetStudentDataSubActivityWise() --> " + ex.Message + " " + ex.StackTrace);

            }

            return ds;


        }

        public DataSet GetMarEntryStudentReport(int acdyr, int college, int Degree, int Branch, int facAdvisor)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@P_ACADEMIC_YEAR", acdyr);
                sqlParams[1] = new SqlParameter("@P_COLLEGE_ID", college);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", Degree);
                sqlParams[3] = new SqlParameter("@P_BRANCHNO", Branch);
                sqlParams[4] = new SqlParameter("@P_FAC_ADVISOR", facAdvisor);
                //sqlParams[5] = new SqlParameter("@P_ACTIVITYNO", activityno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_MAR_ACTIVTTY_STUDENT_WISE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // Added By Nikhil Lambe to done changes on Fa mark entry page.
        public DataSet GetStudentDetailsNew(int acdyr, int college, int Degree, int Branch, int facAdvisor)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@P_ACADEMIC_YEAR", acdyr);
                sqlParams[1] = new SqlParameter("@P_COLLGE_ID", college);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", Degree);
                sqlParams[3] = new SqlParameter("@P_BRANCHNO", Branch);
                sqlParams[4] = new SqlParameter("@P_FAC_ADVISOR", facAdvisor);
                // sqlParams[5] = new SqlParameter("@P_ACTIVITYNO", activityno);

                ds = objDataAccess.ExecuteDataSetSP("ACD_PKG_GET_STUDENT_FOR_MAR_ENTRY_NEW", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetStudentDetailsNew() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // Added By Nikhil Lambe to done changes on Fa mark entry page.
        public DataSet GetAllActivityNew(int version, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_VERSON", version);
                sqlParams[1] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_MASTER_ACTIVITY_NAME_NEW", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllActivityNew() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // Added By Swapnil P dated on 29-07-2022 for get activated sessionno as per student college, degree and branch
        public DataSet GetSessionNoForActivity(int idno, int ua_type, int pageno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_IDNO", idno);
                sqlParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                sqlParams[2] = new SqlParameter("@P_PAGE_LINK", pageno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SESSIONNO_FOR_ACTIVITY_BY_IDNO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.GetAllActivityNew() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet FillFeedbackSession(int degreeno, int branchno, int semesterno,int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                sqlParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                sqlParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FILL_FEEDBACK_SESSION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.FillFeedbackSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet FillFeedbackType(int degreeno, int branchno, int semesterno, int college_id,int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                sqlParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                sqlParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                sqlParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FILL_FEEDBACK_FEEDBACK_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityController.FillFeedbackSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by pooja for activity master
        public DataSet GetDefinedPhDStudentActivities(int activityno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];

                sqlParams[0] = new SqlParameter("@P_ACTIVITYNO", activityno);

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_PHD_STUDENT_REGISTRATION_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedPhDStudentActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Pooja for Activity Master

        public int InsertPhD_Students_RegistrationForm_Activity(int activityno, int AdmBatch, DateTime FromDate, DateTime ToDate, int Activity_Status, decimal regFees)
        {
            int ret = Convert.ToInt32(CustomStatus.Error);
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ACTIVITYNO", activityno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                objParams[2] = new SqlParameter("@P_FROM_DATE", FromDate);
                objParams[3] = new SqlParameter("@P_TODATE", ToDate);
                objParams[4] = new SqlParameter("@P_ACTIVITY_STATUS", Activity_Status);//Added by Dileep Kare on 16/09/2021
                objParams[5] = new SqlParameter("@P_FEES", regFees);//Added by Nikhil L. on 19/12/2022 to add registration fees.
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object obj = objHelper.ExecuteNonQuerySP("PKG_ACD_INS_PHD_STUDENT_REG_FORM_ACTIVITY", objParams, true);
                if (Convert.ToInt32(obj) != -99 && Convert.ToInt32(obj) != 0 && Convert.ToInt32(obj) == 1)
                {
                    ret = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                if (Convert.ToInt32(obj) != -99 && Convert.ToInt32(obj) != 0 && Convert.ToInt32(obj) == 2)
                {
                    ret = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                //else
                //{
                //    ret = Convert.ToInt32(CustomStatus.RecordExist);
                //}
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.InsertPhD_Students_RegistrationForm_Activity-> " + ex.ToString());
            }
            return ret;
        }
        public DataSet GetPhDStudents_Registration_Form_Activity_Details()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objHelper.ExecuteDataSetSP("PKG_GET_PHD_REG_ACTIVITY_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetForeignStudents_Registration_Form_Activity_Details-> " + ex.ToString());
            }
            return ds;
        }
    }
}