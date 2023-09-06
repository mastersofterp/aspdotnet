using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BusinessLogicLayer.BusinessEntities.Academic;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class ODTrackerController
    {
        string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetFacultyEventList(int ReqStatus, int userType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USER_TYPE", userType);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_FACULTY_EVENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetFacultyEventList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertODTrackerEventsBYFaculty(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_EVENT_CATEGORY_NO", objODTracker.EventID);
                objParams[1] = new SqlParameter("@P_SUB_EVENT_CATEGORY_NO", objODTracker.Sub_EventID);
                objParams[2] = new SqlParameter("@P_EVENT_NAME", objODTracker.EventName);
                objParams[3] = new SqlParameter("@P_START_DATE", objODTracker.Event_Start_Date);
                objParams[4] = new SqlParameter("@P_END_DATE", objODTracker.Event_End_Date);
                objParams[5] = new SqlParameter("@P_IS_SPECIAL_EVENT", objODTracker.IsSpecialEvent);
                objParams[6] = new SqlParameter("@P_IS_PUBLISH", objODTracker.IsPublish);
                objParams[7] = new SqlParameter("@P_COMMENT", objODTracker.EventComment);
                objParams[8] = new SqlParameter("@P_UANO", objODTracker.UANO);
                objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", objODTracker.OrganizationID);
                objParams[10] = new SqlParameter("@P_SESSIONNO", objODTracker.SessionNo);
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_EVENT_LIST_BY_FACULTY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertODTrackerEventsBYFaculty-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODTrackerEventsBYFaculty(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_EVENT_CATEGORY_NO", objODTracker.EventID);
                objParams[1] = new SqlParameter("@P_SUB_EVENT_CATEGORY_NO", objODTracker.Sub_EventID);
                objParams[2] = new SqlParameter("@P_EVENT_NAME", objODTracker.EventName);
                objParams[3] = new SqlParameter("@P_START_DATE", objODTracker.Event_Start_Date);
                objParams[4] = new SqlParameter("@P_END_DATE", objODTracker.Event_End_Date);
                objParams[5] = new SqlParameter("@P_IS_SPECIAL_EVENT", objODTracker.IsSpecialEvent);
                objParams[6] = new SqlParameter("@P_IS_PUBLISH", objODTracker.IsPublish);
                objParams[7] = new SqlParameter("@P_COMMENT", objODTracker.EventComment);
                objParams[8] = new SqlParameter("@P_UANO", objODTracker.UANO);
                objParams[9] = new SqlParameter("@P_EVENT_SRNO", objODTracker.FacultyEvent_SRNO);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_EVENT_LIST_BY_FACULTY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerEventsBYFaculty-> " + ex.ToString());
            }
            return retStatus;
        }

        public int DeleteFacultyEvent(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_EVENT_SRNO", objODTracker.FacultyEvent_SRNO);                 
                objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_DELETE_FACULTY_EVENT", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.DeleteFacultyEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetMyEventListForPrincipal(int ReqStatus, int UserType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];                 
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                objParams[1] = new SqlParameter("@P_USER_TYPE", UserType);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_EVENT_LIST_FOR_PRINCIPAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetMyEventListForPrincipal --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetPlacementListForCoordinatorHead(int ReqStatus, int UserType,int UA_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                objParams[1] = new SqlParameter("@P_USER_TYPE", UserType);
                objParams[2] = new SqlParameter("@P_UA_NO", UA_NO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_LIST_FOR_COORDINATOR_HEAD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetPlacementListForCoordinatorHead --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateODTrackerEventsStatusByPrincipal(ODTracker objODTracker, int Organization_ID, int userType, int RequestStatus, DataTable dtEventSRNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];                 
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[1] = new SqlParameter("@P_FINAL_REQUEST_APPROVED_BY", objODTracker.Final_request_approved_by);
                objParams[2] = new SqlParameter("@P_FINAL_REQUEST_REJECTED_BY", objODTracker.Final_request_rejected_by);
                objParams[3] = new SqlParameter("@P_ACD_OD_TRACKER_FACULTY_EVENT_SRNO_LIST", dtEventSRNO);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_FACULTY_EVENTS_STATUS_BY_PRINCIPAL", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerEventsStatusByPrincipal-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODTrackerPlacementStatusByCoordinatorHead(ODTracker objODTracker, int Organization_ID, int userType, int RequestStatus, DataTable dtPlacementSRNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[1] = new SqlParameter("@P_FINAL_REQUEST_APPROVED_BY", objODTracker.Final_request_approved_by);
                objParams[2] = new SqlParameter("@P_FINAL_REQUEST_REJECTED_BY", objODTracker.Final_request_rejected_by);
                objParams[3] = new SqlParameter("@P_ACD_OD_TRACKER_COORDINATOR_PLACEMENT_SRNO_LIST", dtPlacementSRNO);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_COORDINATOR_PLACEMENT_STATUS_BY_COORDINATOR_HEAD", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerPlacementStatusByCoordinatorHead-> " + ex.ToString());
            }
            return retStatus;
        }

        public int Insert_Student_OD_Tracker_Events(ODTracker objODTracker,DataTable dtCourseTimeslot)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_FACULTY_EVENT_NO", objODTracker.FacultyEvent_SRNO);
                objParams[1] = new SqlParameter("@P_STUD_OD_DATE", objODTracker.Student_OD_date);
                objParams[2] = new SqlParameter("@P_STUDENT_COMMENT", objODTracker.Student_Comment);
                objParams[3] = new SqlParameter("@P_TIME_SLOT_NO", objODTracker.Time_Slot_No);
                objParams[4] = new SqlParameter("@P_COURSE_NO", objODTracker.CourseNo);                
                objParams[5] = new SqlParameter("@P_IDNO", objODTracker.Idno);
                objParams[6] = new SqlParameter("@P_INSERTED_BY", objODTracker.UANO);                 
                objParams[7] = new SqlParameter("@P_FACULTY_EVENT_UANO", objODTracker.Faculty_Event_Uano);
                objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", objODTracker.OrganizationID);
                objParams[9] = new SqlParameter("@P_SESSION_NO", objODTracker.SessionNo);
                objParams[10] = new SqlParameter("@P_OD_TRACKER_COURSE_LIST", dtCourseTimeslot);                
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_STUDENT_OD", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.Insert_Student_OD_Tracker_Events-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertODTrackerStudentPlacementEvent(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_PLACEMENT_NO", objODTracker.PlacementNo);
                objParams[1] = new SqlParameter("@P_PLACEMENT_DATE", objODTracker.PlacementDate);
                objParams[2] = new SqlParameter("@P_IDNO", objODTracker.Idno);
                objParams[3] = new SqlParameter("@P_REQUEST_STATUS",1 );
                objParams[4] = new SqlParameter("@P_STUDENT_COMMENT", objODTracker.StudPlacementComment);
                objParams[5] = new SqlParameter("@P_INSERTED_BY", objODTracker.UANO);
                objParams[6] = new SqlParameter("@P_COORDINATOR_ID", objODTracker.Coordinator_Id);
                objParams[7] = new SqlParameter("@P_ORGANIZATION_ID", objODTracker.OrganizationID);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_STUDENT_PLACEMENT_LIST", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertODTrackerStudentPlacementEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertODTrackerPlacementEvent(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_PLACEMENT_NAME", objODTracker.PlacementName);
                objParams[1] = new SqlParameter("@P_START_DATE", objODTracker.Event_Start_Date);
                objParams[2] = new SqlParameter("@P_END_DATE", objODTracker.Event_End_Date);
                objParams[3] = new SqlParameter("@P_PLACEMENT_HEAD", objODTracker.PlacementHeadNo);
                objParams[4] = new SqlParameter("@P_IS_PUBLISH", objODTracker.IsPublish);
                objParams[5] = new SqlParameter("@P_INSERTED_BY", objODTracker.UANO);
                objParams[6] = new SqlParameter("@P_COMMENT", objODTracker.EventComment);
                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_PLACEMENT_EVENT", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertODTrackerEventsBYFaculty-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODTrackerPlacementEvent(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_PLACEMENT_NAME", objODTracker.PlacementName);
                objParams[1] = new SqlParameter("@P_START_DATE", objODTracker.Event_Start_Date);
                objParams[2] = new SqlParameter("@P_END_DATE", objODTracker.Event_End_Date);
                objParams[3] = new SqlParameter("@P_PLACEMENT_HEAD", objODTracker.PlacementHeadNo);
                objParams[4] = new SqlParameter("@P_IS_PUBLISH", objODTracker.IsPublish);                 
                objParams[5] = new SqlParameter("@P_COMMENT", objODTracker.EventComment);
                objParams[6] = new SqlParameter("@P_PLACEMENT_SRNO", objODTracker.Placement_SRNO);
                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_PLACEMENT_EVENT", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerPlacementEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetPlacementEventList(int ReqStatus, int userType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USER_TYPE", userType);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_EVENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetFacultyEventList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int DeletePlacementEvent(ODTracker objODTracker)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PLACEMENT_SRNO", objODTracker.Placement_SRNO);

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_DELETE_PLACEMENT_EVENT", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.DeletePlacementEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetPlacementEventByID(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PLACEMENT_SRNO", objODTracker.Placement_SRNO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_EVENT_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetPlacementEventByID --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFacultyEventByID(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_EVENT_ID", objODTracker.FacultyEvent_SRNO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_FACULTY_EVENT_BYID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetFacultyEventByID --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentEventODListByStatus(int ReqStatus)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_STUDENT_EVENT_OD_LIST_BY_STATUS_FOR_FACULTY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetStudentEventODListByStatus --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int Update_Student_OD_Tracker_Events_Status(ODTracker objODTracker, int Organization_ID, int userType, int RequestStatus, DataTable dtCourses)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_STUD_OD_NO", objODTracker.Stud_OD_NO);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[2] = new SqlParameter("@P_APPROVED_BY", objODTracker.Approved_By);
                objParams[3] = new SqlParameter("@P_REJECTED_BY", objODTracker.Rejected_By);
                objParams[4] = new SqlParameter("@P_OD_TRACKER_COURSE_LIST", dtCourses);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_STUDENT_EVENTS_STATUS", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.Update_Student_OD_Tracker_Events_Status-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStudentPlacementEventByPlacementCoordinator(ODTracker objODTracker, int Organization_ID, int RequestStatus, DataTable dtStudPlacementNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COORDINATOR_ID", objODTracker.Coordinator_Id);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[2] = new SqlParameter("@P_APPROVED_BY", objODTracker.Approved_By);
                objParams[3] = new SqlParameter("@P_REJECTED_BY", objODTracker.Rejected_By);
                objParams[4] = new SqlParameter("@P_OD_TRACKER_STUD_PLACEMENT_NO_LIST", dtStudPlacementNo);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_STUD_PLACEMENT_EVENT_BY_PLACEMENT_COORDINATOR", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateStudentPlacementEventByPlacementCoordinator-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStudentPlacementEventByPlacementCoordinatorHead(ODTracker objODTracker, int Organization_ID, int RequestStatus, DataTable dtStudPlacementNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COORDINATOR_ID", objODTracker.Coordinator_Id);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[2] = new SqlParameter("@P_APPROVED_BY", objODTracker.Approved_By);
                objParams[3] = new SqlParameter("@P_REJECTED_BY", objODTracker.Rejected_By);
                objParams[4] = new SqlParameter("@P_OD_TRACKER_STUD_PLACEMENT_NO_LIST", dtStudPlacementNo);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_STUD_PLACEMENT_EVENT_BY_PLACEMENT_COORDINATOR_HEAD", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateStudentPlacementEventByPlacementCoordinatorHead-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetDashBoardDataForFaculty(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", objODTracker.UANO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_FACULTY_DASHBOARD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDashBoardDataForFaculty --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDashBoardDataForPrincipal(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", objODTracker.UANO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PRINCIPAL_DASHBOARD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDashBoardDataForPrincipal --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDashboardDataForCoordinatorHead(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", objODTracker.UANO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_COORDINATOR_HEAD_DASHBOARD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDashboardDataForCoordinatorHead --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDashboardDataForPlacementCoordinator(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", objODTracker.UANO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_COORDINATOR_DASHBOARD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDashboardDataForPlacementCoordinator --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentEventListForPrincipalApproval(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", objODTracker.RequestStatus);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_STUDENT_EVENT_LIST_FOR_PRINCIPAL_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetStudentEventListForPrincipalApproval --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateODTrackerStudentEventsStatusByPrincipal(ODTracker objODTracker, DataTable dtStudCourseNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", objODTracker.RequestStatus);
                objParams[1] = new SqlParameter("@P_FINAL_REQUEST_APPROVED_BY", objODTracker.Final_request_approved_by);
                objParams[2] = new SqlParameter("@P_FINAL_REQUEST_REJECTED_BY", objODTracker.Final_request_rejected_by);
                objParams[3] = new SqlParameter("@P_OD_TRACKER_COURSE_LIST", dtStudCourseNo);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_STUDENT_EVENTS_STATUS_BY_PRINCIPAL", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerStudentEventsStatusByPrincipal-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetPlacementCoordinatorStudentListForApproval(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", objODTracker.RequestStatus);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_COORDINATOR_STUDENT_LIST_FOR_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetPlacementCoordinatorStudentListForApproval --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetPlacementCoordinatorHeadStudentListForApproval(ODTracker objODTracker)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_REQUEST_STATUS", objODTracker.RequestStatus);
                objParams[1] = new SqlParameter("@P_UA_NO", objODTracker.UANO);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_PLACEMENT_COORDINATOR_HEAD_STUDENT_LIST_FOR_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetPlacementCoordinatorHeadStudentListForApproval --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
                 
        public int InsertODEvent(ODTracker objODTracker, int ActiveStatus,int Organization_ID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_EVENT_NAME", objODTracker.EventName);
                objParams[1] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", Organization_ID);                
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_EVENT_CATEGORY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertODEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODEvent(ODTracker objODTracker, int ActiveStatus, int Organization_ID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_EVENT_NAME", objODTracker.EventName);
                objParams[1] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);                 
                objParams[2] = new SqlParameter("@P_EVENT_ID", objODTracker.EventID);                 
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_EVENT_CATEGORY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertODSubEvent(ODTracker objODTracker, int ActiveStatus, int Organization_ID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SUB_EVENT_NAME", objODTracker.SubEventName);
                objParams[1] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", Organization_ID);
                objParams[3] = new SqlParameter("@P_EVENT_ID", objODTracker.EventID);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_SUB_EVENT_CATEGORY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertODSubEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODSubEvent(ODTracker objODTracker, int ActiveStatus, int Organization_ID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SUB_EVENT_NAME", objODTracker.SubEventName);
                objParams[1] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", Organization_ID);
                objParams[3] = new SqlParameter("@P_EVENT_ID", objODTracker.EventID);
                objParams[4] = new SqlParameter("@P_SUB_EVENT_ID", objODTracker.Sub_EventID);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_SUB_EVENT_CATEGORY", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODSubEvent-> " + ex.ToString());
            }
            return retStatus;
        }

        public int Insert_OD_Tracker_Events(ODTracker objODTracker, int Organization_ID,int userType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_EVENT_CATEGORY", objODTracker.EventID);
                objParams[1] = new SqlParameter("@P_SUB_EVENT_CATEGORY", objODTracker.Sub_EventID);
                objParams[2] = new SqlParameter("@P_EVENT_NAME", objODTracker.EventName);
                objParams[3] = new SqlParameter("@P_START_DATE", objODTracker.Event_Start_Date);
                objParams[4] = new SqlParameter("@P_END_DATE", objODTracker.Event_End_Date);
                objParams[5] = new SqlParameter("@P_IS_SPECIAL_EVENT", objODTracker.IsSpecialEvent);
                objParams[6] = new SqlParameter("@P_IS_PUBLISH", objODTracker.IsPublish);
                objParams[7] = new SqlParameter("@P_COMMENT", objODTracker.EventComment);
                objParams[8] = new SqlParameter("@P_USER_TYPE", userType);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_EVENT_LIST", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.Insert_OD_Tracker_Events-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetMyEventList(int Org_ID, int ReqStatus,int UserType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ORG_ID", Org_ID);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                objParams[2] = new SqlParameter("@P_USER_TYPE", UserType);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_EVENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetMyEventList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
                         
        public SqlDataReader GetSingleSubEvent(int SubEventNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SUB_EVENT_ID", SubEventNo);
                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_OD_TRACKER_GET_SUB_EVENT_BYID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSubEvent-> " + ex.ToString());
            }
            return dr;
        }

        public SqlDataReader GetSingleEvent(int EventNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_EVENT_ID", EventNo);
                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_OD_TRACKER_GET_EVENT_BYID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleEvent-> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetEventListForPrincipal(int Org_ID, int ReqStatus, int UserType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ORG_ID", Org_ID);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                objParams[2] = new SqlParameter("@P_USER_TYPE", UserType);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_EVENT_LIST_FOR_PRINCIPAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetEventListForPrincipal --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateODTrackerFacultyEventsStatusByPrincipal(ODTracker objODTracker, int Organization_ID, int userType, int RequestStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_EVENT_NO", objODTracker.EventID);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", RequestStatus);
                objParams[2] = new SqlParameter("@P_FINAL_REQUEST_APPROVED_BY", objODTracker.Final_request_approved_by);
                objParams[3] = new SqlParameter("@P_FINAL_REQUEST_REJECTED_BY", objODTracker.Final_request_rejected_by);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_FACULTY_EVENTS_STATUS_BY_PRINCIPAL", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerEventsStatusByPrincipal-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetFacultyEventListForPrincipal(int Org_ID, int ReqStatus, int UserType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ORG_ID", Org_ID);
                objParams[1] = new SqlParameter("@P_REQUEST_STATUS", ReqStatus);
                objParams[2] = new SqlParameter("@P_USER_TYPE", UserType);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_FACULTY_EVENT_LIST_FOR_PRINCIPAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetFacultyEventListForPrincipal --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentODEventData(int Idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);                 
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_STUDENT_EVENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetStudentODEventData --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentPlacementEventData(int Idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_STUDENT_PLACEMENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetStudentPlacementEventData --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetTimeSlotDetailsFromTimetable(string EventDate,int IDNO,int SemesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SDATE", EventDate);
                objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTER_NO", SemesterNo);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_TIME_SLOT_FROM_TIMETABLE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetTimeSlotDetailsFromTimetable --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertODTrackerEventConfig(int EventType, int MinimumDaysAllowed, int EventForFacultyStudent, ODTracker objODTracker,int ActiveStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_EVENT_TYPE", EventType);
                objParams[1] = new SqlParameter("@P_MINIMUM_DAYS_ALLOWED", MinimumDaysAllowed);
                objParams[2] = new SqlParameter("@P_EVENT_FOR_FACULTY_STUDENT", EventForFacultyStudent);
                objParams[3] = new SqlParameter("@P_INSERTED_BY", objODTracker.UANO);
                objParams[4] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);
                objParams[5] = new SqlParameter("@P_ORGANIZATION_ID", objODTracker.OrganizationID);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_EVENT_CONFIG", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.Insert_OD_Tracker_Events-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateODTrackerEventConfig(int ConfigSRNO, int MinimumDaysAllowed, int ActiveStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_CONFIG_SRNO", ConfigSRNO);
                objParams[1] = new SqlParameter("@P_MINIMUM_DAYS_ALLOWED", MinimumDaysAllowed);
                objParams[2] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_EVENT_CONFIG", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateODTrackerEventConfig-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertStudentODLimitConfig(ODTracker objODTracker, int ActiveStatus,int AdmBatchNo,int SchemeNo,int OdDays)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_ADM_BATCH_NO", AdmBatchNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                objParams[2] = new SqlParameter("@P_ALLOWED_OD_DAYS", OdDays);
                objParams[3] = new SqlParameter("@P_IS_ACTIVE", ActiveStatus);
                objParams[4] = new SqlParameter("@P_ORGANIZATIONID", objODTracker.OrganizationID);
                objParams[5] = new SqlParameter("@P_INSERTED_BY", objODTracker.UANO);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_INSERT_STUDENT_OD_LIMIT_CONFIG", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertStudentODLimitConfig-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStudentODLimitConfig(ODTracker objODTracker, int ActiveStatus, int AdmBatchNo, int SchemeNo, int OdDays)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_ADM_BATCH_NO", AdmBatchNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                objParams[2] = new SqlParameter("@P_ALLOWED_OD_DAYS", OdDays);
                objParams[3] = new SqlParameter("@P_IS_ACTIVE", ActiveStatus);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OD_TRACKER_UPDATE_STUDENT_OD_LIMIT_CONFIG", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateStudentODLimitConfig-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentODLimitConfig(int Admbatch,int SchemeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_OD_TRACKER_GET_STUDENT_OD_LIMIT_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetStudentODLimitConfig --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}