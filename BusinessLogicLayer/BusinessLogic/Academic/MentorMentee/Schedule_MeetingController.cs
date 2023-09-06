
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
            public class Schedule_MeetingController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

             
                public long AddUpdate_Meeting_Comittee_Details(MeetingScheduleMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ID", objMM.ID);
                        objParams[1] = new SqlParameter("@P_NAME", objMM.NAME);
                        objParams[2] = new SqlParameter("@P_CODE", objMM.CODE);
                        objParams[3] = new SqlParameter("@P_DEPTNO", objMM.DEPTNO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objMM.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COMMITTEE_TYPE", objMM.COMMITTEE_TYPE);
                        objParams[6] = new SqlParameter("@P_USERNO",objMM.UA_Userno);
                        // objParams[6] = new SqlParameter("@P_DEPARTMENT_ID", objMM.DEPTNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MEETING_COMMITEE_INSERT_UPDATE", objParams, true);   // PKG_MM_INSERTUPDATE_COMMITEE change the procedure name.
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_Comittee_Details->" + ex.ToString());
                    }
                    return retstatus;
                }
          

                public int DeleteMeetingCommittee(int committeeID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMMITTEEID", committeeID);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MEETING_DEL_COMMITTEE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.DeleteCommittee-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }

            

                public int AddUpdate_Meeting_Schedule_Details(MeetingScheduleMaster objMM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[27];

                        //@P_EDU_ID
                        objParams[0] = new SqlParameter("@P_PK_AGENDA", objMM.PK_AGENDA_ID);
                        objParams[1] = new SqlParameter("@P_FK_MEETING", objMM.FK_MEETING_ID);
                        if (!objMM.MEETING_DATE.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_MEETINGDATE", objMM.MEETING_DATE);
                        else
                            objParams[2] = new SqlParameter("@P_MEETINGDATE", DBNull.Value);
                        objParams[3] = new SqlParameter("@P_TIME", objMM.MEETING_TIME);
                        objParams[4] = new SqlParameter("@P_AGENDANO", objMM.AGENDA_NO);//CLAIM ID
                        objParams[5] = new SqlParameter("@P_TITLE", objMM.TITLE);
                        objParams[6] = new SqlParameter("@P_FILEPATH", objMM.FILEPATH);
                        objParams[7] = new SqlParameter("@P_USERID", objMM.USERID);

                        if (!objMM.AUDIT_DATE.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_AUDITDATE", objMM.AUDIT_DATE);
                        else
                            objParams[8] = new SqlParameter("@P_AUDITDATE", DBNull.Value);
                        objParams[9] = new SqlParameter("@P_VENUE", objMM.VENUE);
                        objParams[10] = new SqlParameter("@P_LOCK", objMM.LOCK);
                        objParams[11] = new SqlParameter("@P_TABLE_ITEM", objMM.TABLE_ITEM);
                        objParams[12] = new SqlParameter("@P_MEETING_CODE", objMM.CODE);
                        objParams[13] = new SqlParameter("@P_FILE_NAME", objMM.FILE_NAME);

                        objParams[14] = new SqlParameter("@P_ADDLINE", objMM.ADDLINE2);
                        objParams[15] = new SqlParameter("@P_CITY", objMM.CITY);
                        objParams[16] = new SqlParameter("@P_STATE", objMM.STATE);
                        objParams[17] = new SqlParameter("@P_ZIPCODE", objMM.ZIPCODE);
                        objParams[18] = new SqlParameter("@P_COUNTRY", objMM.COUNTRY);

                        if (!objMM.LAST_DATE.Equals(DateTime.MinValue))
                            objParams[19] = new SqlParameter("@P_LAST_DATE", objMM.LAST_DATE);
                        else
                            objParams[19] = new SqlParameter("@P_LAST_DATE", DBNull.Value);

                        objParams[20] = new SqlParameter("@P_DEPTNO", objMM.DEPTNO);
                        objParams[21] = new SqlParameter("@P_VENUEID", objMM.VENUEID);
                        objParams[22] = new SqlParameter("@P_MEETINGTOTIME", objMM.MEETING_TO_TIME);
                        objParams[23] = new SqlParameter("@P_USERNO", objMM.UA_Userno);
                        objParams[24] = new SqlParameter("@P_CONTENT_DETAILS", objMM.AGENDA_CONTAIN);

                        objParams[25] = new SqlParameter("@P_ACTIVE_STATUS", objMM.STATUS);
                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MEETING_INSERTUPDATE_SCHEDULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }



                public int AddUpdate_Meeting_Schedule_Documents(MeetingScheduleMaster objMM, string FILE_NAME)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];                      
                        objParams[0] = new SqlParameter("@P_DEC_ID", objMM.DEC_ID);
                        objParams[1] = new SqlParameter("@P_PK_AGENDA", objMM.PK_AGENDA_ID);
                        objParams[2] = new SqlParameter("@P_COMMITEEID", objMM.COMMITEEID);
                        objParams[3] = new SqlParameter("@P_USERNO", objMM.UA_Userno);
                        objParams[4] = new SqlParameter("@P_DESCRIPTION", objMM.DESCRIPTION);
                        objParams[5] = new SqlParameter("@P_FILENAME", FILE_NAME);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSUPD_MEETING_DESCRIPTION_DOCUMENTS_MM", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetDocumentList(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_DECID", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MEETING_DESCRIPTION_DOCUMENTS_LIST_MM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllTestSccoreList-> " + ex.ToString());
                    }
                    return ds;
                }



                // This method is used to get login credentailas for sending mail.
                public DataSet GetFromDataForEmail(int ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_MEETING_GET_LOGIN_DATA_FOR_EMAIL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

              
                public long AddUpdate_Meeting_Venue(MeetingScheduleMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PK_VENUEID", objMM.PK_COMMITEEDES);
                        objParams[1] = new SqlParameter("@P_USERNO", objMM.UA_Userno);
                        objParams[2] = new SqlParameter("@P_VENUE", objMM.VENUE);
                        objParams[3] = new SqlParameter("@P_STATUS", objMM.STATUS);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MEETING_VENUE_INSERTUPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdate_Designation->" + ex.ToString());
                    }
                    return retstatus;
                }

            
                public DataSet GetPlanScheduleMeetingExcelReport(int committeeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMMITTEEID", committeeid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SCHEDULE_MEETING_EXCLE_REPORTS_MM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Schedule_MeetingController.GetPlanScheduleMeetingExcelReport-> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }
    }
}