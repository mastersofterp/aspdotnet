using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class applicationReceivedController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetExstStudentDetailsByApplicationID(string appid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_APPLID", appid);
                ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENT_EXIST_DETAILS_BY_APPLID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.GetExstDetailsByRegNo -> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateStudentApplicationStatus(int userno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_USER_APPLICATION_STATUS", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.UpdateStudentApplicationStatus-> " + ex.ToString());
            }
            return retStatus;
        }

        //WHEN ALLOTMENT DONE ROUNDWISE THEN NO NEED OF BRANCHNO PARAMETER FOR THIS METHOS
        //IF ALLOTMENT DONE WITHOUT HAVING ALLOTES STATUS THEN BRANCHNO PARAMETER IS USED FOR THIS METHOD[20-JULY-2016]

        public int TransferStudentDataToMain(int userno, int PTYPE, int DEGREENO, int BRANCHNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_PTYPE", PTYPE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TRANSFER_STUD_DATA", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordSaved));

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.applicationReceivedController.TransferStudentDataToMain-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UnlockStudentApplication(int userno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_USER_APPLICATION", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.UpdateStudentApplicationStatus-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Nikhil L. on 05/11/2022 to get PhD students for admission confirmation.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataSet GetPhDStudentInfo_AdmConfirm(string userName)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNAME", userName);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PHD_STUDENT_INFO_ADM_CONFIRMATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.applicationReceivedController.GetPhDStudentInfo_AdmConfirm -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAmountDetails_ByUserno_ForPhD(int userNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", userNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_AMOUNT_DETAILS_FOR_PHD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.applicationReceivedController.GetAmountDetails_ByUserno_ForPhD() -> " + ex.ToString());
            }
            return ds;
        }
        public int TransferStudentDataToMain_PhD(int userno, int PTYPE, int DEGREENO, int BRANCHNO, int UANO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_PTYPE", PTYPE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_UA_NO", UANO);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TRANSFER_STUD_DATA_FOR_PHD", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordSaved));

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.applicationReceivedController.TransferStudentDataToMain-> " + ex.ToString());
            }
            return retStatus;
        }

        //public int UpdateStudentApplicationStatus(int userno)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[2];
        //        objParams[0] = new SqlParameter("@P_USERNO", userno);
        //        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[1].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_USER_APPLICATION_STATUS", objParams, true);

        //        if (Convert.ToInt16(ret) == 0)
        //            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
        //        else
        //            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32((CustomStatus.Error));
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.UpdateStudentApplicationStatus-> " + ex.ToString());
        //    }
        //    return retStatus;
        //}

    }
}
