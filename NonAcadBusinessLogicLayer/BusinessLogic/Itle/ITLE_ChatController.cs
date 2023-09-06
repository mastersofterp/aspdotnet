using System;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    /// <summary>
    /// This ITeachingPlanController is used to control ACD_ITEACHINGPLAN table.
    /// </summary>
    public partial class ITLE_ChatController
    {
        /// <summary>ConnectionStrings</summary>
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


        //--------Start Chat---------------
        public int AddChatRequest(ITLE_Chat  objChat)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
               
                  objParams[0]=new  SqlParameter("@Operation", objChat.Operation);
                  objParams[1]= new SqlParameter("@SP_UA_IDNORequestSender", objChat.UA_IDNORequestSender);
                  objParams[2]=new SqlParameter("@SP_UA_IDNORequestReceiver",objChat.UA_IDNORequestReceiver);
                  objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                  objParams[3].Direction = ParameterDirection.Output;

                    object ret = objSH.ExecuteNonQuerySP("SP_ITLE_CHATREQUEST", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLE_ChatController.AddChatRequest-> " + ex.ToString());
            }
            return retStatus;
        }



        public int AddRegistration(ITLE_Chat objChat)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@Operation", objChat.Operation);
                objParams[1] = new SqlParameter("@SP_UA_IDNO", objChat.UA_IDNO);
                objParams[2] = new SqlParameter("@SP_UA_IDNO_NETWORKMEMBERID", objChat.UA_IDNO_NETWORKMEMBERID);
                objParams[3] = new SqlParameter("@SP_ISActiveApply", objChat.ISActiveApply);
                objParams[4] = new SqlParameter("@SP_ISActiveFriendRequest", objChat.ISActiveFriendRequest);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSH.ExecuteNonQuerySP("SP_ITLE_CHATREGISTRATION", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLE_ChatController.AddRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AcceptRequest(ITLE_Chat objChat)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@Operation", objChat.Operation);
                objParams[1] = new SqlParameter("@SP_UA_IDNO", objChat.UA_IDNO);
                objParams[2] = new SqlParameter("@SP_UA_IDNO_NETWORKMEMBERID", objChat.UA_IDNO_NETWORKMEMBERID);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSH.ExecuteNonQuerySP("SP_ITLE_CHATREGISTRATION", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLE_ChatController.AacceptRequest-> " + ex.ToString());
            }
            return retStatus;
        }
        public int RejectRequest(ITLE_Chat objChat)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@Operation", objChat.Operation);
                objParams[1] = new SqlParameter("@SP_UA_IDNO", objChat.UA_IDNO);
                objParams[2] = new SqlParameter("@SP_UA_IDNO_NETWORKMEMBERID", objChat.UA_IDNO_NETWORKMEMBERID);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSH.ExecuteNonQuerySP("SP_ITLE_CHATREGISTRATION", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLE_ChatController.AacceptRequest-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet ShowChatRequest(ITLE_Chat objChat)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@Operation", objChat.Operation);
                objParams[1] = new SqlParameter("@SP_UA_IDNORequestReceiver", objChat.UA_IDNORequestReceiver);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                ds = objSH.ExecuteDataSetSP("SP_ITLE_CHATREQUEST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyUaNo-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet ShowSendRequest(ITLE_Chat objChat)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@Operation", objChat.Operation);
                objParams[1] = new SqlParameter("@SP_UA_IDNORequestSender", objChat.UA_IDNORequestSender);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                ds = objSH.ExecuteDataSetSP("SP_ITLE_CHATREQUEST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyUaNo-> " + ex.ToString());
            }
            return ds;
        }
        //--End Chat---------
    }
}