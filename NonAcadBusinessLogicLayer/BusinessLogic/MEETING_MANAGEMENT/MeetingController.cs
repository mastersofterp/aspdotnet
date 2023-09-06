
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
            public class MeetingController
            {


                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetMemberDetailsByRange(MeetingMaster OBJMM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FK_COMMITEE", OBJMM.COMMITEE_NO);
                        objParams[1] = new SqlParameter("@P_FK_COMMITEE_DESIG", OBJMM.DESIGNATION_ID);
                        objParams[2] = new SqlParameter("@P_STARTDATE", OBJMM.STARTDATE);
                        objParams[3] = new SqlParameter("@P_ENDDATE", OBJMM.ENDDATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_MEMBERDETAIL_BY_RANGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Meeting_Controller.GetMemberDetailsByRange-> " + ex.ToString());
                    }
                    return ds;
                }
                public long Update_Agenda_Meeting_Lock(MeetingMaster objMM, char LOCK_MEETING)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMMITEE_NO", objMM.COMMITEE_NO);
                        objParams[1] = new SqlParameter("@P_CODE ", objMM.CODE);
                        objParams[2] = new SqlParameter("@P_LOCK_AGENDA ", objMM.LOCK);
                        objParams[3] = new SqlParameter("@P_LOCK_MEETING ", LOCK_MEETING);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_UPDATE_LOCK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Meeting_Controller.Update_Agenda_Meeting_Lock->" + ex.ToString());
                    }
                    return retstatus;
                }
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 16-FEB-2015
                /// DESCRIPTION   : ADD DEPTNO 
                /// </summary>
                /// <param name="objMM"></param>
                /// <returns></returns>
                public long AddUpdate_Comittee_Details(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ID", objMM.ID);
                        objParams[1] = new SqlParameter("@P_NAME", objMM.NAME);
                        objParams[2] = new SqlParameter("@P_CODE", objMM.CODE);
                        objParams[3] = new SqlParameter("@P_DEPTNO", objMM.DEPTNO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objMM.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COMMITTEE_TYPE", objMM.COMMITTEE_TYPE);
                        // objParams[6] = new SqlParameter("@P_DEPARTMENT_ID", objMM.DEPTNO);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_COMMITEE_INSERT_UPDATE", objParams, true);   // PKG_MM_INSERTUPDATE_COMMITEE change the procedure name.
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
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 29-DEC-2014
                /// DESCRIPTION  : TO INSERT/UPDATE DESIGNATION
                /// </summary>
                /// <param name="objMM"></param>
                /// <returns></returns>
                public long AddUpdate_Designation(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PK_COMMITEEDES", objMM.PK_COMMITEEDES);
                        objParams[1] = new SqlParameter("@P_DSIGNAME", objMM.DESIGNAME);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_DESIGNATION_INSERTUPDATE", objParams, true);
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
                public int DeleteDesignation(int DesigID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DESIGID", DesigID);
                        objSQLHelper.ExecuteNonQuerySP("PKG_MM_SP_DEL_DESIGNATION", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.DeleteDesignation-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }


                public int DeleteCommittee(int committeeID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMMITTEEID", committeeID);
                        objSQLHelper.ExecuteNonQuerySP("PKG_MM_SP_DEL_COMMITTEE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.DeleteCommittee-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }

                public int Delete_Commitee_Member(MeetingMaster objMM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        //objParams[0] = new SqlParameter("@P_MEMBER_NO", objMM.MEMBER_NO );
                        objParams[0] = new SqlParameter("@P_COMMITEE_NO", objMM.COMMITEE_NO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MM_DELETE_COMMITEE_MEMBER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.MEETING_Controller.Delete_Commitee_Member-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 17-FEB-2015
                /// DESCRIPTION   : ADD NEW PARAMERE AS DEPTNO
                /// </summary>
                /// <param name="objMM"></param>
                /// <returns></returns>
                public long AddUpdate_Comittee_Member(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_PKID", objMM.ID);
                        objParams[1] = new SqlParameter("@P_COMMITEE_NO ", objMM.COMMITEE_NO);
                        objParams[2] = new SqlParameter("@P_PK_CMEMBER ", objMM.MEMBER_NO);
                        objParams[3] = new SqlParameter("@P_ACTIVE", objMM.ACTIVE);
                        if (!objMM.AUDIT_DATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_AUDITDATE", objMM.AUDIT_DATE);
                        else
                            objParams[4] = new SqlParameter("@P_AUDITDATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_USERID ", objMM.USERID);
                        if (!objMM.STARTDATE.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_STARTTDATE", objMM.STARTDATE);
                        else
                            objParams[6] = new SqlParameter("@P_STARTTDATE", DBNull.Value);
                        if (!objMM.ENDDATE.Equals(DateTime.MinValue))
                            objParams[7] = new SqlParameter("@P_ENDDATE", objMM.ENDDATE);
                        else
                            objParams[7] = new SqlParameter("@P_ENDDATE", DBNull.Value);
                        objParams[8] = new SqlParameter("@P_DESIGNATION", objMM.DESIGNATION_ID);
                        objParams[9] = new SqlParameter("@P_DEPTNO", objMM.DEPTNO);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_INSERTUPDATE_COMMITEE_MEMBER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Meeting_Controller.AddUpdate_Comittee_Member->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateMemberRights(string IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_UPDATE_MEMBER_RIGHTS", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMemberRightsOfEmp(string UANO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UANO", UANO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_UPDATE_MEMBER_RIGHTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Get_MM_FILE_AGENDA(int PK_AGENDA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AGENDA_NO", PK_AGENDA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_FILE_AGENDA", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.Get_MM_FILE_TRAN-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet Get_MM_FILE_MEETING(int PK_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NO", PK_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_FILE_MEETING", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.Get_MM_FILE_TRAN_NEW-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdate_Agenda_Details(MeetingMaster objMM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[24];

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
                        objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[23].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_INSERTUPDATE_AGENDA", objParams, true);
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



                // This method is use to Insert Update Meeting Name
                public long AddUpdateMeetingName(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ID", objMM.ID);
                        objParams[1] = new SqlParameter("@P_MEETING_NO", objMM.MEETING_NO);
                        objParams[2] = new SqlParameter("@P_MEETING_NAME", objMM.MEETING_NAME);
                        objParams[3] = new SqlParameter("@P_USERID", objMM.USERID);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_MEETING_NAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdateMeetingName->" + ex.ToString());
                    }
                    return retstatus;
                }


                // This method is use to Insert Update Plan & Schedule
                public long AddUpdatePlanSchedule(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_PSNO", objMM.PSNO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objMM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_MEETING_NO", objMM.MEETING_NO);
                        objParams[3] = new SqlParameter("@P_MEETING_DATE", objMM.MEETING_DATE);
                        objParams[4] = new SqlParameter("@P_MEETING_TIME", objMM.MEETING_TIME);
                        objParams[5] = new SqlParameter("@P_VENUE", objMM.VENUE);
                        objParams[6] = new SqlParameter("@P_USERID", objMM.USERID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_PLAN_SCHEDULE_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdatePlanSchedule->" + ex.ToString());
                    }
                    return retstatus;
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_LOGIN_DATA_FOR_EMAIL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                // This method is used to add/update approval remark
                public int AddUpdateApprovalRemark(MeetingMaster objMM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_APPROVAL_ID", objMM.APPROVAL_ID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", objMM.MEETING_CODE);
                        objParams[2] = new SqlParameter("@P_USERID", objMM.USERID);
                        objParams[3] = new SqlParameter("@P_REMARK_TBL", objMM.REMARK_TBL);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_APPROVAL_REMARK_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdateApprovalRemark->" + ex.ToString());
                    }
                    return retstatus;
                }



                // This method is used to bind agenda list at member login for approval
                public DataSet GetAgendaToBind(int ID, string MEETING_CODE, int USERID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ID", ID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", MEETING_CODE);
                        objParams[2] = new SqlParameter("@P_USERID", USERID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_AGENDA_LIST_FOR_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetAgendaToBind-> " + ex.ToString());
                    }
                    return ds;
                }



                public int UpdateEmailReceivedConfirmation(int USERID, int COMMITTEEID, string RECEIVE_TYPE) //, string MEETING_CODE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_USERID", USERID);
                        objParams[1] = new SqlParameter("@P_COMMITTEEID", COMMITTEEID);
                        objParams[2] = new SqlParameter("@P_RECEIVE_TYPE", RECEIVE_TYPE);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_UPDATE_EMAIL_RECEIVED_CONFIRMATION", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.UpdateEmailReceivedConfirmation->" + ex.ToString());
                    }
                    return retstatus;
                }


                // This method is used to see the suggestions of Committee Members on Agendas 
                public DataSet GetListOfCommitteeMembers(int COMMITTEEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMMITTEEID", COMMITTEEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_COMMITTEE_MEMBERS_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetListOfCommitteeMembers-> " + ex.ToString());
                    }
                    return ds;
                }



                // This method is used to see the suggestions of Committee Members on Agendas 
                public DataSet GetSuggestionsOnAgenda(int USERID, string MEETING_CODE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERID", USERID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", MEETING_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_SUGGESTIONS_ON_AGENDA_BY_MEMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetSuggestionsOnAgenda-> " + ex.ToString());
                    }
                    return ds;
                }



                // This method is use to Insert Update Agenda Content
                public long AddUpdateAgendaContents(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ACID", objMM.ACID);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objMM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_COMMITTEE_NO", objMM.COMMITEE_NO);
                        objParams[3] = new SqlParameter("@P_AGENDA_ID", objMM.AGENDA_ID);
                        objParams[4] = new SqlParameter("@P_MEETING_CODE", objMM.MEETING_CODE);
                        objParams[5] = new SqlParameter("@P_CONTENT_TBL", objMM.CONTENT_TBL);
                        objParams[6] = new SqlParameter("@P_USERID", objMM.USERID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_AGENDA_CONTENTS_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdateAgendaContents->" + ex.ToString());
                    }
                    return retstatus;
                }




                // This method is used to bind meeting minutes list at member login for approval
                public DataSet GetMeetingMinutesToBind(int ID, string MEETING_CODE, int USERID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ID", ID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", MEETING_CODE);
                        objParams[2] = new SqlParameter("@P_USERID", USERID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_MOM_DRAFT_FOR_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetMeetingMinutesToBind-> " + ex.ToString());
                    }
                    return ds;
                }


                // This method is used to add/update Meeting Draft
                public int AddUpdateMeetingDraft(MeetingMaster objMM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DAID", objMM.DAID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", objMM.MEETING_CODE);
                        objParams[2] = new SqlParameter("@P_USERID", objMM.USERID);
                        objParams[3] = new SqlParameter("@P_DRAFT_REMARK_TBL", objMM.DRAFT_REMARK_TBL);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_APPROVAL_REMARK_DRAFT_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.AddUpdateApprovalRemark->" + ex.ToString());
                    }
                    return retstatus;
                }


                // This method is used to see the suggestions of Committee Members on Agendas 
                public DataSet GetMembersWithDraftSuggestions(int COMMITTEEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMMITTEEID", COMMITTEEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_MEMBERS_WITH_DRAFT_SUGGESTIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetMembersWithDraftSuggestions-> " + ex.ToString());
                    }
                    return ds;
                }



                // This method is used to see the suggestions of Committee Members on Meeting Draft 
                public DataSet GetSuggestionsOnDraft(int USERID, string MEETING_CODE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERID", USERID);
                        objParams[1] = new SqlParameter("@P_MEETING_CODE", MEETING_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_SUGGESTIONS_ON_DRAFT_BY_MEMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetSuggestionsOnDraft-> " + ex.ToString());
                    }
                    return ds;
                }



                //Sharad Akre 
                //Insert Update Venue Name
                //20/11/2019

                public long AddUpdate_Venue(MeetingMaster objMM)
                {
                    long retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PK_VENUEID", objMM.PK_COMMITEEDES);
                        objParams[1] = new SqlParameter("@P_VENUE", objMM.VENUE);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_VENUE_INSERTUPDATE", objParams, true);
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

                public int DeleteVenue(int VenueID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VENUEID", VenueID);
                        objSQLHelper.ExecuteNonQuerySP("PKG_MM_SP_DEL_VENUE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.DeleteDesignation-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }

                //************************
                //public int AddUpdate_Agenda_Details(MeetingMaster objMM)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[14];

                //        //@P_EDU_ID
                //        objParams[0] = new SqlParameter("@P_PK_AGENDA", objMM.PK_AGENDA_ID);
                //        objParams[1] = new SqlParameter("@P_FK_MEETING", objMM.FK_MEETING_ID);
                //        if (!objMM.MEETING_DATE.Equals(DateTime.MinValue))
                //            objParams[2] = new SqlParameter("@P_MEETINGDATE", objMM.MEETING_DATE);
                //        else
                //            objParams[2] = new SqlParameter("@P_MEETINGDATE", DBNull.Value);
                //        objParams[3] = new SqlParameter("@P_TIME", objMM.MEETING_TIME);
                //        objParams[4] = new SqlParameter("@P_AGENDANO", objMM.AGENDA_NO);//CLAIM ID
                //        objParams[5] = new SqlParameter("@P_TITLE", objMM.TITLE);
                //        objParams[6] = new SqlParameter("@P_FILEPATH", objMM.FILEPATH);
                //        objParams[7] = new SqlParameter("@P_USERID", objMM.USERID);

                //        if (!objMM.AUDIT_DATE.Equals(DateTime.MinValue))
                //            objParams[8] = new SqlParameter("@P_AUDITDATE", objMM.AUDIT_DATE);
                //        else
                //            objParams[8] = new SqlParameter("@P_AUDITDATE", DBNull.Value);
                //        objParams[9] = new SqlParameter("@P_VENUE", objMM.VENUE);
                //        objParams[10] = new SqlParameter("@P_LOCK", objMM.LOCK);
                //        objParams[11] = new SqlParameter("@P_TABLE_ITEM", objMM.TABLE_ITEM);
                //        objParams[12] = new SqlParameter("@P_MEETING_CODE", objMM.CODE);
                //        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[13].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_INSERTUPDATE_AGENDA", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //        {

                //            object ret1 = 1;
                //            foreach (DataRow dr in dt.Rows)
                //            {
                //                if (Convert.ToInt32(dr["FUID"]) < 0)
                //                {
                //                    SqlParameter[] objPar = null;
                //                    objPar = new SqlParameter[5];

                //                    objPar[0] = new SqlParameter("@P_FUID", dr["FUID"]);
                //                    objPar[1] = new SqlParameter("@P_PK_AGENDA", Convert.ToInt32(ret));
                //                    objPar[2] = new SqlParameter("@P_FILEPATH", dr["FILEPATH"]);
                //                    objPar[3] = new SqlParameter("@P_DISPLAYFILENAME", dr["DisplayFileName"]);
                //                    objPar[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //                    objPar[4].Direction = ParameterDirection.Output;

                //                    ret1 = objSQLHelper.ExecuteNonQuerySP("PKG_MM_INSERTUPDATE_FILE_AGENDA", objPar, true);
                //                    if (Convert.ToInt32(ret1) == -99)
                //                    {
                //                        retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //                    }

                //                }
                //            }
                //            if (Convert.ToInt32(ret1) == -99)
                //            {
                //                retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            }
                //            else
                //            {
                //                retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                //    }
                //    return retstatus;
                //}
                //***************************



            }
        }
    }
}