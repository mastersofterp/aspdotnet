//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : SESSION ACTIVITY CONTROLLER CLASS                                    
// CREATION DATE : 15-JUN-2009                                                          
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI                                    
// MODIFIED DATE :
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
    public class SessionActivityController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetDefinedSessionActivities()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_ALL_SESSION_ACTIVITIES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDefinedSessionActivities(int sessionActiityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", sessionActiityNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_GET_SESSION_ACTIVITY_BY_ID", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //modified on 06-04-2020 by Vaishali for Notification
        //Modified by rishabh on 29/01/2022 for collegeids
        //public int AddSessionActivity(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, int College_Ids)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {
        //            new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
        //            new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
        //            new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
        //            new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos), // modified by SP
        //            new SqlParameter("@P_BRANCH", branch),
        //            new SqlParameter("@P_SEMESTER", semester),
        //            new SqlParameter("@P_DEGREE_NO", degreeno),
        //            new SqlParameter("@P_UserTypes", UserTypes),
        //            new SqlParameter("@P_SESSION_ACTIVITY_NO", status),
        //            new SqlParameter("@P_COLLEGE_IDS", College_Ids),
        //            new SqlParameter("@P_OUT", SqlDbType.Int)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        #region old method commented by Injamam
        //public DataSet AddSessionActivity(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, int College_Ids)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {
        //            new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
        //            new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
        //            new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
        //            new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos), // modified by SP
        //            new SqlParameter("@P_BRANCH", branch),
        //            new SqlParameter("@P_SEMESTER", semester),
        //            new SqlParameter("@P_DEGREE_NO", degreeno),
        //            new SqlParameter("@P_UserTypes", UserTypes),
        //            new SqlParameter("@P_SESSION_ACTIVITY_NO", status),
        //            new SqlParameter("@P_COLLEGE_IDS", College_Ids),
        //            new SqlParameter("@P_OUT", SqlDbType.Int)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        //status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams, true);
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        #endregion


        public DataSet GetDefinedSessionActivitiesFlag(int flag)  //flag parameter added by Injamam 28-2-23
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_FLAG", flag);

                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_ALL_SESSION_ACTIVITIES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet AddSessionActivity(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, int College_Ids, int flag) //flag parameter Added by Injamam 28-2-23
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            DataSet ds = new DataSet();
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
                    new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos), // modified by SP
                    new SqlParameter("@P_BRANCH", branch),
                    new SqlParameter("@P_SEMESTER", semester),
                    new SqlParameter("@P_DEGREE_NO", degreeno),
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", status),
                    new SqlParameter("@P_COLLEGE_IDS", College_Ids),
                    new SqlParameter("@P_FLAG", flag),   //Added by Injamam 28-2-23
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                //status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams, true);
                ds = objDataAccess.ExecuteDataSetSP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }




        //sandeep [29/01/2018]

        //public int AddSessionActivity(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, int College_Ids)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {
        //            new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
        //            new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
        //            new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
        //            new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
        //            new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos), // modified by SP
        //            new SqlParameter("@P_BRANCH", branch),
        //            new SqlParameter("@P_SEMESTER", semester),
        //            new SqlParameter("@P_DEGREE_NO", degreeno),
        //            new SqlParameter("@P_UserTypes", UserTypes),
        //            new SqlParameter("@P_SESSION_ACTIVITY_NO", status),
        //            new SqlParameter("@P_COLLEGE_IDS", College_Ids),
        //            new SqlParameter("@P_OUT", SqlDbType.Int)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_SESSION_ACTIVITY_DEGREE", sqlParams, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        //modified on 06-04-2020 by Vaishali for Notification
        //Modified by rishabh on 29/01/2022 for collegeids
        //Modified by Injamam for Flag
        public int UpdateSessionActivity(SessionActivity sessionActiity, string branch, string semster, string degreeno, string UserTypes, int College_Ids, int flag)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNo),
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MinValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MinValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),
                    new SqlParameter("@P_BRANCH", branch),
                    new SqlParameter("@P_SEMESTER", semster),
                    new SqlParameter("@P_DEGREE_NO", degreeno),
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_COLLEGE_IDS", College_Ids),
                    new SqlParameter("@P_FLAG",flag),       //added on 28-2-23 by Injamam
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", sessionActiity.SessionActivityNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_UPD_SESSION_ACTIVITY_DEGREE", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.UpdateSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //added on 06-04-2020 by Vaishali for Notification
        //Added College_ids on Dated 28-01-2021 by Mahesh Malve.
        //Added College_ids on Dated 28-01-2021 by Mahesh Malve.
        /// <summary>
        /// Mpodified by SP - 10/02/22
        /// </summary>
        /// <param name="sessionActiity"></param>
        /// <param name="branch"></param>
        /// <param name="semester"></param>
        /// <param name="degreeno"></param>
        /// <param name="UserTypes"></param>
        /// <param name="Notification_Template"></param>
        /// <param name="userno"></param>
        /// <param name="College_Ids"></param>
        /// <returns></returns>
        public int AddSessionActivity_FOR_NOTIFICATION(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, string Notification_Template, int userno, int College_Ids)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_Session_Name", sessionActiity.Session_Name),
                    new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos),
                    new SqlParameter("@P_Activity_Name", sessionActiity.Activity_Name),
                    new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_DEGREE_NO", degreeno), 
                    new SqlParameter("@P_BRANCH", branch),
                    new SqlParameter("@P_SEMESTER", semester),
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_Notification_Message", Notification_Template),                   
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),      
                    new SqlParameter("@P_CREATEDBY",userno), 
                    new SqlParameter("@P_COLLEGE_IDS",College_Ids), 
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_SESSION_ACTIVITY_FOR_NOTIFICATION_MSG", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        // added for android 08-04-2020 by Vaishali
        public DataSet GetFCMRegID(string UserTypes, string degreeno, string branchno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_DEGREE_NO", degreeno),
                    new SqlParameter("@P_BRANCH", branchno),
                    new SqlParameter("@P_SEMESTER", semesterno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_Android_Notification", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // added for android 08-04-2020 by Vaishali
        public DataSet GetFCM_ForAndroid_Details(string UserTypes, int ActivityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_ACTIVITY_NO", ActivityNo)    
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_Android_Notification_get_Msgdetails", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int AddFeedbackActivity(SessionActivity sessionActiity, string degreenos, string branchnos, string semesternos, int DATETIME_STATUS)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_START_DATE", sessionActiity.StartDate ),
                    new SqlParameter("@P_END_DATE", sessionActiity.EndDate),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
                    new SqlParameter("@P_FEEDBACK_TYPENO", sessionActiity.Feedbacktypeno),
                    new SqlParameter("@P_IP_ADDRESS", sessionActiity.Ipaddress),
                     new SqlParameter("@P_UA_NO", sessionActiity.User_no),
                     new SqlParameter("@P_DEGREENO", degreenos),
                     new SqlParameter("@P_BRANCHNO", branchnos),
                     new SqlParameter("@P_SEMESTERNO", semesternos),
                     new SqlParameter("@P_COLLEGE_ID", sessionActiity.College_Id),
                     new SqlParameter("@P_START_TIME", sessionActiity.Start_Time),
                     new SqlParameter("@P_END_TIME",sessionActiity.End_Time),
                      new SqlParameter("@P_STATUS",DATETIME_STATUS),
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEEDBACK_INSERT_FEEDBACK_ACTIVITY", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public int UpdateFeedbackActivity(SessionActivity sessionActiity, int DATETIME_STATUS)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_START_DATE", sessionActiity.StartDate ),
                    new SqlParameter("@P_END_DATE", sessionActiity.EndDate),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
                    new SqlParameter("@P_FEEDBACK_TYPENO", sessionActiity.Feedbacktypeno),
                    new SqlParameter("@P_IP_ADDRESS", sessionActiity.Ipaddress),
                     new SqlParameter("@P_UA_NO", sessionActiity.User_no),
                     new SqlParameter("@P_START_TIME", sessionActiity.Start_Time),
                     new SqlParameter("@P_END_TIME",sessionActiity.End_Time),
                         new SqlParameter("@P_STATUS",DATETIME_STATUS),
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", sessionActiity.SessionActivityNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEEDBACK_UPD_FEEDBACK_ACTIVITY", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.SessionActivityController.UpdateSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public DataSet GetDefinedFeedbackActivities(int sessionActiityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                 new SqlParameter("@P_SESSION_ACTIVITY_NO", sessionActiityNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEEDBACK_GET_FEEDBACK_ACTIVITY_BY_ID", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //FEEDABCK
        public DataSet GetDefinedFeedbackActivities()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_FEEDBACK_ALL_FEEDBACK_ACTIVITIES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCourseRegConfigActivityData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetCourseRegConfigActivityData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpSertCourseRegistraionActivity(int COURSE_REG_CONFIG_NO, string courseType, SessionActivity sessionActiity, string UserTypes, bool payment_applicable_Sem_Wise, int choiceFor)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[16];

                sqlParams[0] = new SqlParameter("@COURSE_REG_CONFIG_NO", COURSE_REG_CONFIG_NO);
                sqlParams[1] = new SqlParameter("@COURSE_TYPE", courseType);
                sqlParams[2] = new SqlParameter("@P_COLLEGE_ID", sessionActiity.College_Id);
                sqlParams[3] = new SqlParameter("@P_SCHEMENO", sessionActiity.Schemeno);
                sqlParams[4] = new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo);
                sqlParams[5] = new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MinValue) ? sessionActiity.StartDate as object : DBNull.Value as object));
                sqlParams[6] = new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MinValue) ? sessionActiity.EndDate as object : DBNull.Value as object));
                sqlParams[7] = new SqlParameter("@P_STARTED", sessionActiity.IsStarted);
                sqlParams[8] = new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus);
                sqlParams[9] = new SqlParameter("@P_DEGREENO", sessionActiity.Degree);
                sqlParams[10] = new SqlParameter("@P_BRANCH", sessionActiity.Branch);
                sqlParams[11] = new SqlParameter("@P_SEMESTER", sessionActiity.Semester);
                sqlParams[12] = new SqlParameter("@P_USERTYPES", UserTypes);
                sqlParams[13] = new SqlParameter("@P_PAYMENT_APPL_SEM_WISE", payment_applicable_Sem_Wise);
                sqlParams[14] = new SqlParameter("@P_CHOICEFOR", choiceFor);
                sqlParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[15].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPSERT_COURSE_REGISTRATION_ACTIVITY", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.UpSertCourseRegistraionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Added by Swapnil For Course Registration Activity Details section display Dated on 21-01-2023
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public DataSet GetCourseRegConfigActivityDataDetails(int groupid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_GROUPID", groupid);
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetCourseRegConfigActivityData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by Swapnil P dated on 21-01-2023 for Course Regstration Activity Insert and Update
        /// </summary>
        /// <param name="sessionActivity"></param>
        /// <param name="Sessionnos"></param>
        /// <param name="CollegeIds"></param>
        /// <param name="Degreenos"></param>
        /// <param name="Branchnos"></param>
        /// <param name="Semesternos"></param>
        /// <param name="UserRights"></param>
        /// <param name="CoursePattern"></param>
        /// <param name="choiceFor"></param>
        /// <param name="PAYMENT_APPLICABLE_FOR_SEM_WISE"></param>
        /// <param name="gropuid"></param>
        /// <returns></returns>
        //modified by nehal 22/03/23
        // MODIFY BELOW METHOD BY SHAILENDRA K. ON DATED 21.06.2023 AS PER T-44837 AND 44816 
        public int CourseRegistraionActivityInsert(SessionActivity sessionActivity, string Sessionnos, string CollegeIds, string Degreenos, string Branchnos,
            string Semesternos, string UserRights, string CoursePattern, int choiceFor, bool PAYMENT_APPLICABLE_FOR_SEM_WISE, int gropuid, string StartTime,
            string EndTime, int sessionid, int eligibilityForCrsReg, int StudIDType, int noOfPaperAllowed)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[22];

                sqlParams[0] = new SqlParameter("@P_COURSE_TYPE", CoursePattern);
                sqlParams[1] = new SqlParameter("@P_SESSION_NO", Sessionnos);
                sqlParams[2] = new SqlParameter("@P_START_DATE", ((sessionActivity.StartDate != DateTime.MinValue) ? sessionActivity.StartDate as object : DBNull.Value as object));
                sqlParams[3] = new SqlParameter("@P_END_DATE", ((sessionActivity.EndDate != DateTime.MinValue) ? sessionActivity.EndDate as object : DBNull.Value as object));
                sqlParams[4] = new SqlParameter("@P_STARTED", sessionActivity.IsStarted);
                sqlParams[5] = new SqlParameter("@P_SHOW_STATUS", sessionActivity.ShowStatus);
                sqlParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeIds);
                sqlParams[7] = new SqlParameter("@P_DEGREENO", Degreenos);
                sqlParams[8] = new SqlParameter("@P_BRANCH", Branchnos);
                sqlParams[9] = new SqlParameter("@P_SEMESTER", Semesternos);
                sqlParams[10] = new SqlParameter("@P_USERTYPES", UserRights);
                sqlParams[11] = new SqlParameter("@P_PAYMENT_APPL_SEM_WISE", PAYMENT_APPLICABLE_FOR_SEM_WISE);
                sqlParams[12] = new SqlParameter("@P_CHOICEFOR", choiceFor);
                sqlParams[13] = new SqlParameter("@P_GROUPID", gropuid);
                sqlParams[14] = new SqlParameter("@P_STARTTIME", StartTime);
                sqlParams[15] = new SqlParameter("@P_ENDTIME ", EndTime);
                sqlParams[16] = new SqlParameter("@P_ELIGIBILITY_FOR_CRS_REG", eligibilityForCrsReg); // added by Shailendra K. on dated 20.06.2023 as per T-44816
                sqlParams[17] = new SqlParameter("@P_SESSIONID", sessionid);
                sqlParams[18] = new SqlParameter("@P_ACTIVITYNO", sessionActivity.ActivityNo);               
                sqlParams[19] = new SqlParameter("@P_STUD_IDTYPE", StudIDType);
                sqlParams[20] = new SqlParameter("@P_NO_OF_PAPER_ALLOWED", noOfPaperAllowed); // added by Shailendra K. on dated 14.09.2023 as per T-48652

                sqlParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[21].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPSERT_COURSE_REGISTRATION_ACTIVITY_MODIFIED", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.UpSertCourseRegistraionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Added by Swapnil For Course Registration Activity Details section display Dated on 21-01-2023
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public DataSet GetCourseRegConfigActivityDataDetailsEdit(int groupid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_GROUPID", groupid);
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS_EDIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetCourseRegConfigActivityData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added By Vinay Mishra on 12/09/2023 - To Get Attendance Configuration Data By Group Id to Edit/Update Selected Record
        public DataSet GetAttendanceConfigDataDetailsEdit(int groupid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_GROUPID", groupid);
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_ATTENDANCE_CONFIG_ACTIVITY_DATA_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetAttendanceConfigDataDetailsEdit() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        #region Modification of Activity Master with group
        public int AddSessionActivity(SessionActivity sessionActiity, int sessionid, string branch, string semester, string degreeno, string UserTypes, string College_Ids, int flag, int groupid, string Exam_No, string SubExam_No)
        {
            int status = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONID", sessionid),
                    new SqlParameter("@P_COLLEGE_IDS", College_Ids),
                    new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),   
                    new SqlParameter("@P_DEGREE_NO", degreeno),
                    new SqlParameter("@P_BRANCH", branch),
                    new SqlParameter("@P_SEMESTER", semester),
                    new SqlParameter("@P_USERTYPES", UserTypes),
                    new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos), 
                    new SqlParameter("@P_FLAG", flag),
                    new SqlParameter("@P_GROUPID", groupid),
                    new SqlParameter("@P_EXAMNO", Exam_No),
                    new SqlParameter("@P_SUBEXAMNO", SubExam_No),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object stat = objDataAccess.ExecuteNonQuerySP("PKG_SESSION_INS_UPD_SESSION_ACTIVITY_DEGREE_GROUPID", sqlParams, true);
                status = Convert.ToInt32(stat);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        #endregion
        #region Exam Activity as per the Ticket 56283
        public int AddUpdateSessionActivity(SessionActivity sessionActiity, string branch, string semester, string degreeno, string UserTypes, int College_Ids, int flag, int exam_type, int session_activity_no)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", sessionActiity.SessionNo),
                    new SqlParameter("@P_START_DATE", ((sessionActiity.StartDate != DateTime.MaxValue)? sessionActiity.StartDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_END_DATE", ((sessionActiity.EndDate != DateTime.MaxValue)? sessionActiity.EndDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_STARTED", sessionActiity.IsStarted),
                    new SqlParameter("@P_SHOW_STATUS", sessionActiity.ShowStatus),                    
                    new SqlParameter("@P_ACTIVITY_NO", sessionActiity.ActivityNos),
                    new SqlParameter("@P_BRANCH", branch),
                    new SqlParameter("@P_SEMESTER", semester),
                    new SqlParameter("@P_DEGREE_NO", degreeno),
                    new SqlParameter("@P_UserTypes", UserTypes),
                    new SqlParameter("@P_SESSION_ACTIVITY_NO", session_activity_no),
                    new SqlParameter("@P_COLLEGE_IDS", College_Ids),
                    new SqlParameter("@P_FLAG", flag),
                    new SqlParameter("@P_EXAM_TYPE", exam_type),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_EXAM_INS_UPD_SESSION_ACTIVITY_DEGREE", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddUpdateSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        #endregion
    }
}