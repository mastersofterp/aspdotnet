    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IITMS;
    using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
    using System.Data.SqlClient;
    using IITMS.SQLServer.SQLDAL;
    using System.Data;
    using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
    using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

    namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
    {
        public class EventCreationController
        {
            string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

            //Added by Nikhil Vinod Lambe on 17/05/2021 to add event creation details
            //Added by Nikhil Vinod Lambe on 17/05/2021 to add event creation details
            public int AddEventCreation(EventCreation ec, int ua_no, string ipAddress, string Event_File, int IsPaid)
            {
                int status = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[15];
                    objParams[0] = new SqlParameter("@P_EVENT_TYPE", ec.EventType);
                    objParams[1] = new SqlParameter("@P_EVENT_TITLE", ec.EventTitle);
                    objParams[2] = new SqlParameter("@P_EVENT_START_DATE", ec.EventStartDate);
                    objParams[3] = new SqlParameter("@P_EVENT_END_DATE", ec.EventEndDate);
                    objParams[4] = new SqlParameter("@P_EVENT_REG_START_DATE", ec.EventStartRegDate);
                    objParams[5] = new SqlParameter("@P_EVENT_REG_END_DATE", ec.EventEndRegDate);
                    objParams[6] = new SqlParameter("@P_EVENT_VENUE", ec.EventVenue);
                    objParams[7] = new SqlParameter("@P_EVENT_DESC", ec.EventDesc);
                    objParams[8] = new SqlParameter("@P_UA_NO", ua_no);
                    objParams[9] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    objParams[10] = new SqlParameter("@P_EVENT_BROCHURE", Event_File);
                    objParams[11] = new SqlParameter("@P_PARTICIPANT_ID", ec.EventParticipant);
                    objParams[12] = new SqlParameter("@P_REG_FEE", ec.EventRegFee);
                    objParams[13] = new SqlParameter("@P_ISPAID", IsPaid);
                    objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[14].Direction = ParameterDirection.Output;
                    object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_EVENT_CREATION", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.AddEventCreation --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }
            //Added by Nikhil Vinod Lambe on 17/05/2021 to get details of event.
            public DataSet GetEventCreationDetails()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = new SqlParameter[0];
                    ds = objSqlHelper.ExecuteDataSetSP("GetEventCreationDetails",objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetEventCreationDetails --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }
            //Added by Nikhil Vinod Lambe on 17/05/2021 to get details of event by Title Id
            public DataSet GetEventCreationDetailsByTitleID(int EventTitleID)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_EVENT_TITLE_ID", EventTitleID);
                    ds = objSqlHelper.ExecuteDataSetSP("GetEventCreationDetailsByTitleID", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetEventCreationDetailsByTitleID --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }
            //Added by Nikhil Vinod Lambe on 17/05/2021 to update the event details.
            public int UpdateEventCreation(EventCreation ec, int ua_no, string ipAddress, string Event_File, int TitleID, int IsPaid)
            {
                int status = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[16];
                    objParams[0] = new SqlParameter("@P_EVENT_TYPE", ec.EventType);
                    objParams[1] = new SqlParameter("@P_EVENT_TITLE_ID", TitleID);
                    objParams[2] = new SqlParameter("@P_EVENT_TITLE", ec.EventTitle);
                    objParams[3] = new SqlParameter("@P_EVENT_START_DATE", ec.EventStartDate);
                    objParams[4] = new SqlParameter("@P_EVENT_END_DATE", ec.EventEndDate);
                    objParams[5] = new SqlParameter("@P_EVENT_REG_START_DATE", ec.EventStartRegDate);
                    objParams[6] = new SqlParameter("@P_EVENT_REG_END_DATE", ec.EventEndRegDate);
                    objParams[7] = new SqlParameter("@P_EVENT_VENUE", ec.EventVenue);
                    objParams[8] = new SqlParameter("@P_EVENT_DESC", ec.EventDesc);
                    objParams[9] = new SqlParameter("@P_UA_NO", ua_no);
                    objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    objParams[11] = new SqlParameter("@P_EVENT_BROCHURE", Event_File);
                    objParams[12] = new SqlParameter("@P_PARTICIPANT_ID", ec.EventParticipant);
                    objParams[13] = new SqlParameter("@P_REG_FEE", ec.EventRegFee);
                    objParams[14] = new SqlParameter("@P_ISPAID", IsPaid);
                    objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[15].Direction = ParameterDirection.Output;

                    object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_EVENT_CREATION", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.UpdateEventCreation --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }
            //Added by Nikhil Vinod Lambe on 18/05/2021 to get details of event for registration.
            public DataSet GetEventDetailsForRegister()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = new SqlParameter[0];
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_EVENT_DETAILS", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetEventDetailsForRegister --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }
            //Added by Nikhil Vinod Lambe on 18/05/2021 to register event details
            public int AddEventRegistration(int TitleId,string CandidateName,string Mobile,string Email,int Gender,int ParticipantId,int State,string City,string OrgName,string OrgAddress,string ipAddress)
            {
                int status = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[12];
                    objParams[0] = new SqlParameter("@P_EVENT_TITLE_ID",TitleId);
                    objParams[1] = new SqlParameter("@P_CANDIDATE_NAME",CandidateName);
                    objParams[2] = new SqlParameter("@P_MOBILE_NO",Mobile);
                    objParams[3] = new SqlParameter("@P_EMAIL_ID",Email);
                    objParams[4] = new SqlParameter("@P_GENDER",Gender);
                    objParams[5] = new SqlParameter("@P_PARTICIPANT_ID",ParticipantId);
                    objParams[6] = new SqlParameter("@P_STATE_ID",State);
                    objParams[7] = new SqlParameter("@P_CITY",City);
                    objParams[8] = new SqlParameter("@P_ORGANISATION_NAME",OrgName);
                    objParams[9] = new SqlParameter("@P_ORGANISATION_ADDRESS",OrgAddress);
                    objParams[10] = new SqlParameter("@P_IPADDRESS",ipAddress);                
                    objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[11].Direction = ParameterDirection.Output;

                    object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_EVENT_REGISTRATION", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.AddEventRegistration --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }

            //Added by Nikhil Vinod Lambe on 18/05/2021 to register event details
            public int AddEventRegistrationTemp(int TitleId, string CandidateName, string Mobile, string Email, int Gender, int ParticipantId, int State, string City, string OrgName, string OrgAddress, double Amount,string ipAddress, string orderid)
            {
                int status = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[14];
                    objParams[0] = new SqlParameter("@P_EVENT_TITLE_ID", TitleId);
                    objParams[1] = new SqlParameter("@P_CANDIDATE_NAME", CandidateName);
                    objParams[2] = new SqlParameter("@P_MOBILE_NO", Mobile);
                    objParams[3] = new SqlParameter("@P_EMAIL_ID", Email);
                    objParams[4] = new SqlParameter("@P_GENDER", Gender);
                    objParams[5] = new SqlParameter("@P_PARTICIPANT_ID", ParticipantId);
                    objParams[6] = new SqlParameter("@P_STATE_ID", State);
                    objParams[7] = new SqlParameter("@P_CITY", City);
                    objParams[8] = new SqlParameter("@P_ORGANISATION_NAME", OrgName);
                    objParams[9] = new SqlParameter("@P_ORGANISATION_ADDRESS", OrgAddress);
                    objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    objParams[11] = new SqlParameter("@P_PAID_AMOUNT", Amount);
                    objParams[12] = new SqlParameter("@P_ORDER_ID", orderid);
                    objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[13].Direction = ParameterDirection.Output;

                    object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_EVENT_REGISTRATION_TEMP", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.AddEventRegistration --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }

            public int InsertOnlinePayment_Log(int TitleId, int Participant, double Amount, string ORDER_ID, string Transactionid, string Name, string Mobile, string Email, string Status, string IpAddress)
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] param = new SqlParameter[]
                            {                         
                                new SqlParameter("@P_EVENT_TITLE_ID", TitleId),
                                new SqlParameter("@P_PARTICIPANT_ID", Participant),
                                new SqlParameter("@P_ACTUAL_AMT", Amount),
                                new SqlParameter("@P_ORDER_ID", ORDER_ID), 
                                new SqlParameter("@P_TRANSACTIONID", Transactionid),
                                new SqlParameter("@P_CANDIDATE_NAME", Name),
                                new SqlParameter("@P_MOBILE_NO", Mobile),
                                new SqlParameter("@P_EMAIL_ID", Email),
                                new SqlParameter("@P_STATUS", Status),
                                new SqlParameter("@P_IPADDRESS", IpAddress),
                                new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                            };
                    param[param.Length - 1].Direction = ParameterDirection.Output;
                    object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_EVENT_PAYMENT_LOG", param, true);

                    if (ret != null && ret.ToString() != "-99")
                        retStatus = Convert.ToInt32(ret);
                    else
                        retStatus = -99;
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
                }
                return retStatus;
            }

            public int InsertSuccessOnlinePayment(int TitleId, int Participant, double Amount, string ORDER_ID, string Transactionid, string Name, string Mobile, string Email, string Status, string IpAddress)
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] param = new SqlParameter[]
                            {                         
                                new SqlParameter("@P_EVENT_TITLE_ID", TitleId),
                                new SqlParameter("@P_PARTICIPANT_ID", Participant),
                                new SqlParameter("@P_ACTUAL_AMT", Amount),
                                new SqlParameter("@P_ORDER_ID", ORDER_ID), 
                                new SqlParameter("@P_TRANSACTIONID", Transactionid),
                                new SqlParameter("@P_CANDIDATE_NAME", Name),
                                new SqlParameter("@P_MOBILE_NO", Mobile),
                                new SqlParameter("@P_EMAIL_ID", Email),
                                new SqlParameter("@P_STATUS", Status),
                                new SqlParameter("@P_IPADDRESS", IpAddress),
                                new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                            };
                    param[param.Length - 1].Direction = ParameterDirection.Output;
                    object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_EVENT_PAYMENT_LOG", param, true);

                    if (ret != null && ret.ToString() != "-99")
                        retStatus = Convert.ToInt32(ret);
                    else
                        retStatus = -99;
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
                }
                return retStatus;
            }

            //Added by Nikhil Vinod Lambe on 19/05/2021 to get list of events.
            public DataSet GetEventDetailsList(int EventType, int EventTitle)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_EVENT_TYPE", EventType);
                    objParams[1] = new SqlParameter("@P_EVENT_TITLE_ID", EventTitle);
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_SHOW_EVENT_DETAILS_LIST", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetEventDetailsList --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }


            //Added by Nikhil Vinod Lambe on 19/05/2021 to get list of participants.
            public DataSet GetParticipantList_Excel(int EventType, int EventTitle)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_EVENT_TYPE", EventType);
                    objParams[1] = new SqlParameter("@P_EVENT_TITLE_ID", EventTitle);
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_PARTICIPANT_DETAILS_LIST_EXCEL", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetParticipantList_Excel --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }

            //Added by Nikhil Vinod Lambe on 19/05/2021 to get list of participants.
            public DataSet GetParticipantList(int EventType, int EventTitle)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                    SqlParameter[] objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_EVENT_TYPE", EventType);
                    objParams[1] = new SqlParameter("@P_EVENT_TITLE_ID", EventTitle);
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_PARTICIPANT_DETAILS_LIST", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EventCreationController.GetParticipantList_Excel --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }
        }
    }
