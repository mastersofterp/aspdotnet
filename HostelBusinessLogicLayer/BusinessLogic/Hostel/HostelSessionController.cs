//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL SESSION CONTROLLER                                            
// CREATION DATE : 13-AUG-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class HostelSessionController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddHostelSession(string sessionName, DateTime sessionStart, DateTime sessionEnd, string collegeCode, int active,int isshow)
        {
            int status = 0;
           
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSION_NAME", sessionName),
                    new SqlParameter("@P_START_DATE", sessionStart),
                    new SqlParameter("@P_END_DATE", sessionEnd),
                    new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                    new SqlParameter("@P_FLOCK",active),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_ISSHOW",isshow),
                    new SqlParameter("@P_HOSTEL_SESSION_NO", status)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_SESSION_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.AddHostelSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateHostelSession(string sessionName, DateTime sessionStart, DateTime sessionEnd, int hostelSessionNo, int active, int isshow)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_SESSION_NAME", sessionName),
                    new SqlParameter("@P_START_DATE", sessionStart),
                    new SqlParameter("@P_END_DATE", sessionEnd),
                    new SqlParameter("@P_FLOCK",active),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_ISSHOW",isshow),
                    new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_SESSION_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.UpdateHostelSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllHostelSession()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_SESSION_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.GetAllHostelSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetHostelSessionByNo(int hostelSessionNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_SESSION_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.GetHostelSessionByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRoomAllotedStudent(int hostelSessionNo, int hostel_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo),
                    new SqlParameter("@P_HOSTEL_NO", hostel_no),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_STUDENTS_PROMOTE_TO_NEW_SESSION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.GetRoomAllotedStudent() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int PromoteStudentNewSession(int oldSession, int newSession, string idno, DateTime fromdate, DateTime todate, int Ua_No)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_OLD_SESSIONNO", oldSession);
                objParams[1] = new SqlParameter("@P_NEW_SESSIONNO", newSession);
                objParams[2] = new SqlParameter("@P_INDO", idno);
                objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[4] = new SqlParameter("@P_TODATE", todate);
                objParams[5] = new SqlParameter("@P_UA_NO", Ua_No);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                objParams[6].Direction = ParameterDirection.Output;
                // objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STUD_PROMOTE_NEWSESSION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Others);
                }

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.PromoteStudentNewSession()-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}
