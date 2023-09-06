using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    /// <summary>
    /// This ILibraryController is used to control ILibrary table.
    /// </summary>
    public partial class IMailController
    {
        /// <summary>
        /// ConnectionStrings
        /// </summary>
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetReceivedMails(int userId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_USER_ID", userId)
                        };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_RECEIVED_MAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSentMails(int userId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_USER_ID", userId)
                        };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_SENT_MAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetMailById(int mailId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_MAIL_ID", mailId)
                        };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_MAIL_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetMailAttachments(int mailId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MAIL_ID", mailId)
                };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_MAIL_ATTACHMENTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public bool SetMailStatus(int mailId, int UserId, char status)
        {
            try
            {
                SQLHelper dbAction = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MAIL_ID", mailId),
                    new SqlParameter("@P_USER_ID", UserId),
                    new SqlParameter("@P_STATUS", status)
                };
                dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_SET_MAIL_STATUS", objParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.UpdateAnnouncement-> " + ex.ToString());
            }
            return true;
        }


        public bool SetOutBoxMailStatus(int mailId, int UserId, char status)
        {
            try
            {
                SQLHelper dbAction = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MAIL_ID", mailId),
                    new SqlParameter("@P_USER_ID", UserId),
                    new SqlParameter("@P_STATUS", status)
                };
                dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_SET_OUTBOX_MAIL_STATUS", objParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.UpdateAnnouncement-> " + ex.ToString());
            }
            return true;
        }


        public bool SendMail(Mail msg)
        {
            bool isCompleted = false;
            try
            {
                SQLHelper dbAction = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_PARENT_ID", msg.ParentId),
                    new SqlParameter("@P_SENDER_ID", msg.SenderId),
                    new SqlParameter("@P_SENT_DATE", msg.SentDate),
                    new SqlParameter("@P_SUBJECT", msg.Subject),
                    new SqlParameter("@P_BODY", msg.Body),
                    new SqlParameter("@P_MAIL_ID", msg.MailId)
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_INS_MAIL", objParams, true);

                if (obj != null && obj.ToString() != "" && Convert.ToInt32(obj) > 0)
                {
                    foreach (MailReceiver item in msg.Receivers)
                    {
                        objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_MAIL_ID", Convert.ToInt32(obj)),
                            new SqlParameter("@P_RECEIVER_ID", item.ReceiverId),
                            new SqlParameter("@P_STATUS", item.Status),
                            new SqlParameter("@P_USERNAME", item.UserName),
                            new SqlParameter("@P_RECORD_ID", item.RecordId)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_INS_MAIL_RECEIVER", objParams, true);
                    }

                    foreach (MailAttachment item in msg.Attachments)
                    {
                        objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_MAIL_ID", Convert.ToInt32(obj)),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_INS_MAIL_ATTACHMENT", objParams, true);
                    }
                    isCompleted = true;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.UpdateAnnouncement-> " + ex.ToString());
            }
            return isCompleted;
        }

        public DataSet GetContacts(int userId, char type, int sessionNo, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USER_ID", userId),
                    new SqlParameter("@P_TYPE", type),
                    new SqlParameter("@P_SESSION_NO", sessionNo),
                    new SqlParameter("@P_BRANCH_NO", branchNo)
                };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_CONTACTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetNotificationCount(int userId, int sessionNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USER_ID", userId),
                    new SqlParameter("@P_SESSION_NO", sessionNo)
                };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_NOTIFICATION_COUNT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDeletedMails(int userId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_USER_ID", userId)
                        };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_DELETED_MAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }


        // this is used for send sms to students by faculty.

        public int SendSmsByFaculty(string txtMsg,string mobilenumber)
        {
            int retStatus = Convert. ToInt32(CustomStatus. Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_TEXT",txtMsg);
                objParams[1] = new SqlParameter("@P_MOBILENO",mobilenumber);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType. Int);
                objParams[2]. Direction = ParameterDirection. Output;

                object ret = objSQL. ExecuteNonQuerySP("PKG_SMS_SEND_SMS_THROUGH_STOREDPROCEDURE_FOR_ITLE_FACULTY", objParams, true);

                if (Convert. ToInt32(ret) == -99)
                    retStatus = Convert. ToInt32(CustomStatus. TransactionFailed);
                else
                    retStatus = Convert. ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert. ToInt32(CustomStatus. Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.DeleteEBookByUaNo -> " + ex. ToString());
            }

            return retStatus;
        }



        public bool SetTrashMailStatus(int mailId, int UserId)
        {
            try
            {
                SQLHelper dbAction = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MAIL_ID", mailId),
                    new SqlParameter("@P_USER_ID", UserId)
                    //new SqlParameter("@P_STATUS", status)
                };
                dbAction.ExecuteNonQuerySP("PKG_ITLE_MSG_SET_TRASH_MAIL_STATUS", objParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.SetTrashMailStatus-> " + ex.ToString());
            }
            return true;
        }


        public int InsertEmailGroup(int EGROUPNO, int FACULTY_UANO, string STUD_UANO, string GROUP_NAME, string COLLEGE_CODE, DateTime CREATEDDATE,int COURSENO,int SESSIONNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];

               
                objParams[0] = new SqlParameter("@P_FACULTY_UANO", FACULTY_UANO);
                objParams[1] = new SqlParameter("@P_STUD_UANO", STUD_UANO);
                objParams[2] = new SqlParameter("@P_GROUP_NAME", GROUP_NAME);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                objParams[4] = new SqlParameter("@P_CREATEDDATE", CREATEDDATE);
                objParams[5] = new SqlParameter("@P_COURSENO", COURSENO);
                objParams[6] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[7] = new SqlParameter("@P_EGROUPNO", EGROUPNO);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);

                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_INSERT_EMAIL_GROUP", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.DeleteEBookByUaNo -> " + ex.ToString());
            }

            return retStatus;
        }




        public DataSet GetGroupStudent(int userId,int sessionNo, int groupno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USER_ID", userId),
                    //new SqlParameter("@P_TYPE", type),
                    new SqlParameter("@P_SESSION_NO", sessionNo),
                    new SqlParameter("@P_GROUP_NO", groupno)
                };
                ds = objSQL.ExecuteDataSetSP("PKG_ITLE_MSG_GET_GROUP_CONTACTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
            }
            return ds;
        }




    }
}