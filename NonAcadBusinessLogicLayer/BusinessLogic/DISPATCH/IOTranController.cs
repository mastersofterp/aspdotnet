//================================================
//MODIFIED BY   : MRUNAL SINGH
//MODIFIED DATE : 06-12-2014
//DESCRIPTION   : TO SAVE ADDRESS OF THE MULTIPLE RECEIVERS FOR DEPARTMENT DISPATCH OUTWARD
//================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public  class IOTranController
    {

        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddInwardEntry(IOTRAN objIOTran)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[28];
                objParams[0] = new SqlParameter("@P_IOTYPE", objIOTran.IOTYPE);
                objParams[1] = new SqlParameter("@P_CENTRALRECSENTDT", objIOTran.CENTRALRECSENTDT);
                objParams[2] = new SqlParameter("@P_CENTRALENTRYDT", objIOTran.CENTRALENTRYDT);
                objParams[3] = new SqlParameter("@P_OUTREFERENCENO", objIOTran.OUTREFERENCENO);
                objParams[4] = new SqlParameter("@P_CENTRALREFERENCENO", objIOTran.CENTRALREFERENCENO);
                objParams[5] = new SqlParameter("@P_IOFROM", objIOTran.IOFROM);
                objParams[6] = new SqlParameter("@P_TOUSER", objIOTran.TOUSER);
                objParams[7] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[8] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[9] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[10] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[11] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);

                //objParams[12] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                //objParams[13] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);

                //if (objIOTran.CHEQDT == DateTime.MinValue)
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                //}
                //else
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                //}

               // objParams[15] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);
                //  objParams[28] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);
                objParams[12] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objIOTran.COLLEGE_CODE);
                objParams[14] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                objParams[15] = new SqlParameter("@P_CREATED_DATE", objIOTran.CREATED_DATE);
                objParams[16] = new SqlParameter("@P_ADDLINE", objIOTran.ADDLINE);
                objParams[17] = new SqlParameter("@P_STATENO", objIOTran.STATENO);
                objParams[18] = new SqlParameter("@P_COUNTRYNO", objIOTran.COUNTRYNO);

                objParams[19] = new SqlParameter("@P_TODEPT", objIOTran.TODEPT);
                objParams[20] = new SqlParameter("@P_DESIGNO", objIOTran.DESIGNO);
                objParams[21] = new SqlParameter("@P_IN_TO_USER", objIOTran.IN_TO_USER);
                objParams[22] = new SqlParameter("@P_RFID", objIOTran.RFID);
                objParams[23] = new SqlParameter("@P_PEON", objIOTran.PEON);
                objParams[24] = new SqlParameter("@P_CHEQUE_DETAILS_TABLE", objIOTran.CHEQUE_DETAILS_TABLE);
                objParams[25] = new SqlParameter("@P_USERTYPE", objIOTran.USERTYPE);
                objParams[26] = new SqlParameter("@P_USERFLAG", objIOTran.USERFLAG);
                objParams[27] = new SqlParameter("@P_IOTRANNO", SqlDbType.Int);                
                objParams[27].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_INWARD_INSERT", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetLetter()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_MOVENMENT_LETTER", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.GetLetter->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetPendingLetter()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_MOVENMENT_LETTER_PENDING", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.GetPendingLetter->" + ex.ToString());
            }
            return ds;
        }
        public DataSet Disp_Letter(DateTime DiFrmDt, DateTime DiToDt, DateTime UFrmDt, DateTime UToDt, string Ref, int User, int Type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];

                if (!DiFrmDt.Equals(DateTime.MinValue))
                    objParams[0] = new SqlParameter("@P_FROMDT", DiFrmDt);
                else
                    objParams[0] = new SqlParameter("@P_FROMDT", DBNull.Value);

                if (!DiToDt.Equals(DateTime.MinValue))
                    objParams[1] = new SqlParameter("@P_TODT", DiToDt);
                else
                    objParams[1] = new SqlParameter("@P_TODT", DBNull.Value);

                if (!UFrmDt.Equals(DateTime.MinValue))
                    objParams[2] = new SqlParameter("@P_UFROMDT", UFrmDt);
                else
                    objParams[2] = new SqlParameter("@P_UFROMDT", DBNull.Value);

                if (!UToDt.Equals(DateTime.MinValue))
                    objParams[3] = new SqlParameter("@P_UTODT", UToDt);
                else
                    objParams[3] = new SqlParameter("@P_UTODT", DBNull.Value);

                objParams[4] = new SqlParameter("@P_DISPREREFNO", Ref);
                objParams[5] = new SqlParameter("@P_USER", User);
                objParams[6] = new SqlParameter("@P_TYPE", Type);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_MOVENMENT_LETTER_OUTWARD", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.Disp_Letter->" + ex.ToString());
            }
            return ds;
        }
        public int AddOutwardCCEntry(IOTRAN objIOTrancc)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IOTO", objIOTrancc.IOTO);
                objParams[1] = new SqlParameter("@P_REMARK", objIOTrancc.REMARK);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objIOTrancc.COLLEGE_CODE);
                objParams[3] = new SqlParameter("@P_CREATOR", objIOTrancc.CREATOR);
                objParams[4] = new SqlParameter("@P_CREATED_DATE", objIOTrancc.CREATED_DATE);
                objParams[5] = new SqlParameter("@P_SRNO", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_IO_OUTWARD_CC_INSERT", objParams, true);
                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranCCController.AddInwardCCEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        public int UpdateInwardEntry(IOTRAN objIOTran)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[23];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objIOTran.IOTRANNO);
                objParams[1] = new SqlParameter("@P_CENTRALRECSENTDT", objIOTran.CENTRALRECSENTDT);
                objParams[2] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[3] = new SqlParameter("@P_OUTREFERENCENO", objIOTran.OUTREFERENCENO);
                objParams[4] = new SqlParameter("@P_CENTRALREFERENCENO", objIOTran.CENTRALREFERENCENO);
                objParams[5] = new SqlParameter("@P_IOFROM", objIOTran.IOFROM);
                objParams[6] = new SqlParameter("@P_TOUSER", objIOTran.TOUSER);
                objParams[7] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[8] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[9] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[10] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[11] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);

                //objParams[12] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                //objParams[13] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);

                //if (objIOTran.CHEQDT == DateTime.MinValue)
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                //}
                //else
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                //}
                //objParams[15] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);
                //objParams[24] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);

                objParams[12] = new SqlParameter("@P_ADDRESS2", objIOTran.ADDLINE);
                objParams[13] = new SqlParameter("@P_STATENO", objIOTran.STATENO);
                objParams[14] = new SqlParameter("@P_COUNTRYNO", objIOTran.COUNTRYNO);
                objParams[15] = new SqlParameter("@P_TODEPT", objIOTran.TODEPT);
                objParams[16] = new SqlParameter("@P_DESIGNO", objIOTran.DESIGNO);
                objParams[17] = new SqlParameter("@P_IN_TO_USER", objIOTran.IN_TO_USER);
                objParams[18] = new SqlParameter("@P_RFID", objIOTran.RFID);
                objParams[19] = new SqlParameter("@P_PEON", objIOTran.PEON);
                objParams[20] = new SqlParameter("@P_CHEQUE_DETAILS_TABLE", objIOTran.CHEQUE_DETAILS_TABLE);
                objParams[21] = new SqlParameter("@P_USERTYPE", objIOTran.USERTYPE);
                objParams[22] = new SqlParameter("@P_USERFLAG", objIOTran.USERFLAG);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_INWARD_UPDATE", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }

            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetBySrNo(int SRNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SRNO", SRNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_GETBYSRNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetBySrNo-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetRefernceNo(string IOType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@P_IOTYPE", IOType);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_REFNO", objParameter);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetRefernceNo->" + ex.Message);
            }
            return ds;

        }
        public DataSet GetFromInfo(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_FROM_INFO", objParameter);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetFromInfo->" + ex.Message);
            }
            return ds;
        }

        public DataSet GetUserByBranchNo(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objSQLParams = new SqlParameter[1];
                objSQLParams[0] = new SqlParameter("@P_BRANCHNO", DeptNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_USER_LIST_BY_DEPTNO", objSQLParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllInward-> " + ex.ToString());
            }
            return ds;

        }
        public DataSet GetUserByIONo(string IONo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IONO", IONo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_IONO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetUserByIONo->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllInward(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_TODEPT", DeptNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_GETALL_BYDEPT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllInward-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetAllInwardByIdN(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_TODEPT", DeptNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_GETALL_BY_IDNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllInward-> " + ex.ToString());
            }
            return ds;
        }

        // This method is used to get data of inwards of HOD.
        public DataSet GetAllInwardByUserId(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_TODEPT", DeptNo);                
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_GETALL_BY_USERNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllInwardByUserId-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetInwardByInwardNo(int IOTRANNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_GETBYIOTRANNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetInwardByInwardNo -> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetChequeDetails(int IOTRANNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_INWARD_CHEQUE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetChequeDetails -> " + ex.ToString());
            }

            return ds;
        }


        public int UpdateMovement(IOTRAN objIO, string IdNo, string LtrNo)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IOTRANNO", LtrNo);
                objParams[1] = new SqlParameter("@P_MOV_DATE", objIO.MOV_DATE);
                objParams[2] = new SqlParameter("@P_REMARK", objIO.REMARK);
                objParams[3] = new SqlParameter("@P_DEPTNO", objIO.DEPTIONO);
                objParams[4] = new SqlParameter("@P_UA_IDNO", IdNo);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_MOVENMENT_UPDATE", objParams, true) != null)
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateMovement->" + ex.ToString());
            }
            return retstatus;
        }
        public int AddMovement(IOTRAN objIO, string IdNo, string LtrNo, string peon)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IOTRANNO", LtrNo);
                objParams[1] = new SqlParameter("@P_MOV_DATE", objIO.MOV_DATE);
                objParams[2] = new SqlParameter("@P_REMARK", objIO.REMARK);
                objParams[3] = new SqlParameter("@P_DEPTNO", objIO.TODEPT);
                objParams[4] = new SqlParameter("@P_UA_IDNO", IdNo);
                objParams[5] = new SqlParameter("@P_PEON", peon);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_MOVENMENT_INSERT", objParams, true) != null)
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddMovement->" + ex.ToString());
            }
            return retstatus;
        }
        public DataSet GetMovementStatus(int IOTRANNO, int PType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);
                objParams[1] = new SqlParameter("@P_TYPE", PType);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_MOVEMENT_GET_STATUS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetMovementStatus -> " + ex.ToString());
            }
            return ds;
        }
        public int UpdateNotReceive(int iotranno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", iotranno);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_INWARD_NOT_RECEIVE", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateNotReceive-> " + ex.ToString());
            }
            return retStatus;
        }
        public int UpdateDept_InwardEntry(IOTRAN objio, int IdNo)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objio.IOTRANNO);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objio.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTREMARKS", objio.DEPTREMARKS);
                objParams[3] = new SqlParameter("@P_TODEPT", objio.TODEPT);
                objParams[4] = new SqlParameter("@P_UA_IDNO", IdNo);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_INWARD_UPDATE_DEPT", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateDept_InwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddOutwardEntry(IOTRAN objIOTran, CarrierMaster objCM, string to, string remark, string address, string cityno, string pinno, string addline, string state, string country, string contactno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[34];
                objParams[0] = new SqlParameter("@P_IOTYPE", objIOTran.IOTYPE);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objIOTran.DEPTRECSENTDT);

                objParams[2] = new SqlParameter("@P_DEPTREFERENCENO", objIOTran.DEPTREFERENCENO);
                objParams[3] = new SqlParameter("@P_IOTO", objIOTran.IOTO);
                objParams[4] = new SqlParameter("@P_FROMDEPT", objIOTran.FROMDEPT);
                objParams[5] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[6] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[7] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[8] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[9] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);
                objParams[10] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                objParams[11] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);


                if (objIOTran.CHEQDT == DateTime.MinValue)
                {
                    objParams[12] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                }
                else
                {
                    objParams[12] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                }

                objParams[13] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);

                objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objIOTran.COLLEGE_CODE);
                objParams[15] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                objParams[16] = new SqlParameter("@P_CREATED_DATE", objIOTran.CREATED_DATE);
                objParams[17] = new SqlParameter("@P_IOTOS", to);
                objParams[18] = new SqlParameter("@P_REMARK", remark);
                objParams[19] = new SqlParameter("@P_MUL_ADDRESS", address);
                objParams[20] = new SqlParameter("@P_CITYNO_MUL", cityno);
                objParams[21] = new SqlParameter("@P_PINNO", pinno);
                objParams[22] = new SqlParameter("@P_CENTRAREFIONO", objIOTran.CENTRALREFERENCENO);

                objParams[23] = new SqlParameter("@P_ADDLINE", addline);
                objParams[24] = new SqlParameter("@P_STATE", state);
                objParams[25] = new SqlParameter("@P_COUNTRY", country);

                objParams[26] = new SqlParameter("@P_CARRIERNO", objCM.carrierNo);
                objParams[27] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[28] = new SqlParameter("@P_LETTERCAT", objCM.letterCategory);
                objParams[29] = new SqlParameter("@P_CONTACTNO", contactno);
                objParams[30] = new SqlParameter("@P_RFID_NUMBER", objIOTran.RFID);
                objParams[31] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);

                objParams[32] = new SqlParameter("@P_TRACKING_NO", objIOTran.TRACKING_NO);  //24/06/2022

                objParams[33] = new SqlParameter("@P_IOTRANNO", SqlDbType.Int);
                objParams[33].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARD_INSERT", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddOutwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        public int UpdateOutwardEntry(IOTRAN objIOTran, CarrierMaster objCM, string to, string remark, string address, string cityno, string pinno, string addline, string state, string country, string contactno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[31];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objIOTran.IOTRANNO);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objIOTran.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTIONO", objIOTran.DEPTIONO);
                objParams[3] = new SqlParameter("@P_DEPTREFERENCENO", objIOTran.DEPTREFERENCENO);
                objParams[4] = new SqlParameter("@P_IOTO", objIOTran.IOTO);
                objParams[5] = new SqlParameter("@P_FROMDEPT", objIOTran.FROMDEPT);
                objParams[6] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[7] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[8] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[9] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[10] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);
                objParams[11] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                objParams[12] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);
                objParams[13] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                if (objIOTran.CHEQDT == DateTime.MinValue)
                {
                    objParams[14] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                }
                else
                {
                    objParams[14] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                }
                objParams[15] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);
                objParams[16] = new SqlParameter("@P_IOTOS", to);
                objParams[17] = new SqlParameter("@P_REMARK", remark);
                objParams[18] = new SqlParameter("@P_MUL_ADDRESS", address);
                objParams[19] = new SqlParameter("@P_CITYNO_MUL", cityno);
                objParams[20] = new SqlParameter("@P_PINNO", pinno);

                objParams[21] = new SqlParameter("@P_ADDLINE", addline);
                objParams[22] = new SqlParameter("@P_STATE", state);
                objParams[23] = new SqlParameter("@P_COUNTRY", country);

                objParams[24] = new SqlParameter("@P_CARRIERNO", objCM.carrierNo);
                objParams[25] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[26] = new SqlParameter("@P_LETTERCAT", objCM.letterCategory);
                objParams[27] = new SqlParameter("@P_CONTACTNO", contactno);
                objParams[28] = new SqlParameter("@P_RFID_NUMBER", objIOTran.RFID);
                objParams[29] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);

                objParams[30] = new SqlParameter("@P_TRACKING_NO", objIOTran.TRACKING_NO);  //24/06/2022

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARD_UPDATE", objParams, true);
                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }

            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetAllOutward(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_FROMDEPT", DeptNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_GETALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllOutward-> " + ex.ToString());
            }
            return ds;
        }

        //Created by swati ghate
        //created date:09-may-2014
        //--purpose:to display datewise record
        //--used in page : department outward entry
        public DataSet GetAllOutwardDatewise(string frmdt, string todate, int deptno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                objParams[1] = new SqlParameter("@P_TODATE", todate);
                objParams[2] = new SqlParameter("@P_FROMDEPT", deptno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_OUTWARD_DETAIL_new", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllOutwardDatewise-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllInwardDatewise(DateTime frmdt, DateTime todate, int deptno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                objParams[1] = new SqlParameter("@P_TODATE", todate);
                objParams[2] = new SqlParameter("@P_FROMDEPT", deptno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_INWARD_DETAIL_new", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllOutwardDatewise-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetOutwardByOutwardNo(int IOTRANNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_GETBYIOTRANNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetInwardByInwardNo -> " + ex.ToString());
            }

            return ds;
        }
        public int UpdateDept_OutwardEntry(IOTRAN objio)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objio.IOTRANNO);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objio.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTREFERENCENO", objio.DEPTREFERENCENO);
                objParams[3] = new SqlParameter("@P_DEPTREMARKS", objio.DEPTREMARKS);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARD_UPDATE_DEPT", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateDept_OutwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddOutwardDispatch(IOTRAN objIOTran, string to, string remark, string address)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[28];
                objParams[0] = new SqlParameter("@P_IOTYPE", objIOTran.IOTYPE);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objIOTran.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTIONO", objIOTran.DEPTIONO);
                objParams[3] = new SqlParameter("@P_DEPTREFERENCENO", objIOTran.DEPTREFERENCENO);
                objParams[4] = new SqlParameter("@P_IOTO", objIOTran.IOTO);
                objParams[5] = new SqlParameter("@P_CENTRALRECSENTDT", objIOTran.CENTRALRECSENTDT);
                objParams[6] = new SqlParameter("@P_CENTRALREFERENCENO", objIOTran.CENTRALREFERENCENO);
                objParams[7] = new SqlParameter("@P_FROMDEPT", objIOTran.FROMDEPT);
                objParams[8] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[9] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[10] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[11] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[12] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);
                objParams[13] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                objParams[14] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);


                if (objIOTran.CHEQDT == DateTime.MinValue)
                {
                    objParams[15] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                }
                else
                {
                    objParams[15] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                }

                objParams[16] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);

                objParams[17] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objIOTran.COLLEGE_CODE);
                objParams[19] = new SqlParameter("@P_MUL_ADDRESS", address);
                objParams[20] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                objParams[21] = new SqlParameter("@P_CREATED_DATE", objIOTran.CREATED_DATE);

                objParams[22] = new SqlParameter("@P_WEIGHT", objIOTran.WEIGHT);
                objParams[23] = new SqlParameter("@P_COST", objIOTran.COST);
                objParams[24] = new SqlParameter("@P_EXTRACOST", objIOTran.EXTRACOST);
                objParams[25] = new SqlParameter("@P_IOTOS", to);
                objParams[26] = new SqlParameter("@P_REMARK", remark);

                objParams[27] = new SqlParameter("@P_IOTRANNO", SqlDbType.Int);
                objParams[27].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARDDISPATCH_INSERT", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddOutwardDispatch-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddOutwardDispatch(IOTRAN objIOTran, CarrierMaster objCM, string to, string remark, string address, string citynumber, string pinno, string addline, string state, string country, string contactno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[36];
                objParams[0] = new SqlParameter("@P_IOTYPE", objIOTran.IOTYPE);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objIOTran.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTIONO", objIOTran.DEPTIONO);
                objParams[3] = new SqlParameter("@P_DEPTREFERENCENO", objIOTran.DEPTREFERENCENO);
                objParams[4] = new SqlParameter("@P_IOTO", objIOTran.IOTO);
                objParams[5] = new SqlParameter("@P_CENTRALRECSENTDT", objIOTran.CENTRALRECSENTDT);
                objParams[6] = new SqlParameter("@P_CENTRALREFERENCENO", objIOTran.CENTRALREFERENCENO);
                objParams[7] = new SqlParameter("@P_FROMDEPT", objIOTran.FROMDEPT);
                objParams[8] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[9] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[10] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[11] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[12] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);
                //objParams[13] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                //objParams[14] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);


                //if (objIOTran.CHEQDT == DateTime.MinValue)
                //{
                //    objParams[15] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                //}
                //else
                //{
                //    objParams[15] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                //}

                //objParams[16] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);
                //objParams[35] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);
                objParams[13] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objIOTran.COLLEGE_CODE);
                objParams[15] = new SqlParameter("@P_MUL_ADDRESS", address);
                objParams[16] = new SqlParameter("@P_CITYNO_MUL", citynumber);
                objParams[17] = new SqlParameter("@P_PINNO", pinno);
                objParams[18] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                objParams[19] = new SqlParameter("@P_CREATED_DATE", objIOTran.CREATED_DATE);

                objParams[20] = new SqlParameter("@P_WEIGHT", objIOTran.WEIGHT);
                objParams[21] = new SqlParameter("@P_COST", objIOTran.COST);
                objParams[22] = new SqlParameter("@P_EXTRACOST", objIOTran.EXTRACOST);
                objParams[23] = new SqlParameter("@P_IOTOS", to);
                objParams[24] = new SqlParameter("@P_REMARK", remark);

                objParams[25] = new SqlParameter("@P_ADDLINE", addline);
                objParams[26] = new SqlParameter("@P_STATE", state);
                objParams[27] = new SqlParameter("@P_COUNTRY", country);
                objParams[28] = new SqlParameter("@P_CARRIERNO", objCM.carrierNo);
                objParams[29] = new SqlParameter("@P_CONTACTNO", contactno);
                objParams[30] = new SqlParameter("@P_TRACKING_NO", objIOTran.TRACKING_NO);
                
                objParams[31] = new SqlParameter("@P_TOTAL_COST", objIOTran.TOTAL_COST);
                objParams[32] = new SqlParameter("@P_NO_OF_PERSONS", objIOTran.NO_OF_PERSONS);
                objParams[33] = new SqlParameter("@P_UNIT", objIOTran.UNIT);
                objParams[34] = new SqlParameter("@P_CHEQUE_DETAILS_TABLE", objIOTran.CHEQUE_DETAILS_TABLE);
                objParams[35] = new SqlParameter("@P_IOTRANNO", SqlDbType.Int);
                objParams[35].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARDDISPATCH_INSERT", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddOutwardDispatch-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateOutwardDispatch(IOTRAN objIOTran, CarrierMaster objCM, string to, string remark, DateTime dT, string address, string citynumber, string pinno, string addline, string state, string country, string contactno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[33];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objIOTran.IOTRANNO);
                objParams[1] = new SqlParameter("@P_DEPTRECSENTDT", objIOTran.DEPTRECSENTDT);
                objParams[2] = new SqlParameter("@P_DEPTREFERENCENO", objIOTran.DEPTREFERENCENO);
                objParams[3] = new SqlParameter("@P_IOTO", objIOTran.IOTO);
                objParams[4] = new SqlParameter("@P_CENTRALRECSENTDT", objIOTran.CENTRALRECSENTDT);
                objParams[5] = new SqlParameter("@P_CENTRALREFERENCENO", objIOTran.CENTRALREFERENCENO);
                objParams[6] = new SqlParameter("@P_FROMDEPT", objIOTran.FROMDEPT);
                objParams[7] = new SqlParameter("@P_ADDRESS", objIOTran.ADDRESS);
                objParams[8] = new SqlParameter("@P_CITYNO", objIOTran.CITYNO);
                objParams[9] = new SqlParameter("@P_PINCODE", objIOTran.PINCODE);
                objParams[10] = new SqlParameter("@P_LETTERTYPENO", objIOTran.LETTERTYPENO);
                objParams[11] = new SqlParameter("@P_SUBJECT", objIOTran.SUBJECT);
                //objParams[12] = new SqlParameter("@P_CHQDDNO", objIOTran.CHQDDNO);
                //objParams[13] = new SqlParameter("@P_CHEQAMT", objIOTran.CHEQAMT);

                //if (objIOTran.CHEQDT == DateTime.MinValue)
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", DBNull.Value);
                //}
                //else
                //{
                //    objParams[14] = new SqlParameter("@P_CHEQDT", objIOTran.CHEQDT);
                //}

                //objParams[15] = new SqlParameter("@P_BANKNAME", objIOTran.BANKNAME);
                //objParams[33] = new SqlParameter("@P_CHEUQE_ID", objIOTran.CHEUQE_ID);
                objParams[12] = new SqlParameter("@P_POSTTYPENO", objIOTran.POSTTYPENO);
                objParams[13] = new SqlParameter("@P_WEIGHT", objIOTran.WEIGHT);
                objParams[14] = new SqlParameter("@P_COST", objIOTran.COST);
                objParams[15] = new SqlParameter("@P_MUL_ADDRESS", address);
                objParams[16] = new SqlParameter("@P_CITYNO_MUL", citynumber);
                objParams[17] = new SqlParameter("@P_PINNO", pinno);
                objParams[18] = new SqlParameter("@P_EXTRACOST", objIOTran.EXTRACOST);
                objParams[19] = new SqlParameter("@P_IOTOS", to);
                objParams[20] = new SqlParameter("@P_REMARK", remark);
                objParams[21] = new SqlParameter("@P_CREATOR", objIOTran.CREATOR);
                objParams[22] = new SqlParameter("@P_DATETIME", dT);
                objParams[23] = new SqlParameter("@P_ADDLINE", addline);
                objParams[24] = new SqlParameter("@P_STATE", state);
                objParams[25] = new SqlParameter("@P_COUNTRY", country);
                objParams[26] = new SqlParameter("@P_CARRIERNO", objCM.carrierNo);
                objParams[27] = new SqlParameter("@P_CONTACTNO", contactno);
                objParams[28] = new SqlParameter("@P_TRACKING_NO", objIOTran.TRACKING_NO);                
                objParams[29] = new SqlParameter("@P_TOTAL_COST", objIOTran.TOTAL_COST);
                objParams[30] = new SqlParameter("@P_NO_OF_PERSONS", objIOTran.NO_OF_PERSONS);
                objParams[31] = new SqlParameter("@P_UNIT", objIOTran.UNIT);
                objParams[32] = new SqlParameter("@P_CHEQUE_DETAILS_TABLE", objIOTran.CHEQUE_DETAILS_TABLE);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_OUTWARDDISPATCH_UPDATE", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }

            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateOutwardDispatch-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetAllOutwardDispatch(int DeptNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_FROMDEPT", DeptNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_DISPATCH_OUTWARD_GETALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllOutwardDispatch-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetRecieverOutwardDispatchByIotranNo(int IOTRANNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_RECEIVER_GETBYIOTRANNO", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetRecieverOutwardDispatchByIotranNo ->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetOutwardDispatchByOutwardNo(int IOTRANNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_GETBYIOTRANNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetInwardByInwardNo -> " + ex.ToString());
            }

            return ds;
        }

        public DataSet verifyInwardRecord(DateTime FrmDT, DateTime Todt, int DeptNo, int UserNo, int PostType, int CarrierNo, int LetterCat, int ddlCheque, char UserType, int Ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_FROM_DATE", FrmDT);
                objParams[1] = new SqlParameter("@P_TO_DATE", Todt);
                objParams[2] = new SqlParameter("@P_DEPTNO", DeptNo);
                objParams[3] = new SqlParameter("@P_USERNO", UserNo);
                objParams[4] = new SqlParameter("@P_POSTTYPE", PostType);
                objParams[5] = new SqlParameter("@P_CHEQUE", ddlCheque);
                objParams[6] = new SqlParameter("@P_USERTYPE", UserType);
                objParams[7] = new SqlParameter("@P_type", Ptype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.verifyInwardRecord-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet VerifyOutwardRecord(DateTime FrmDT, DateTime ToDt, int DeptNo, int UserNo, int PostType, int CarrierNo, int LetterCat, int ddlCheque, int Ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_FROM_DATE", FrmDT);
                objParams[1] = new SqlParameter("@P_TO_DATE", ToDt);
                objParams[2] = new SqlParameter("@P_DEPTNO", DeptNo);
                objParams[3] = new SqlParameter("@P_USERNO", UserNo);
                objParams[4] = new SqlParameter("@P_POSTTYPE", PostType);
                objParams[5] = new SqlParameter("@P_CARRIERNO", CarrierNo);
                objParams[6] = new SqlParameter("@P_LETTERCAT", LetterCat);
                objParams[7] = new SqlParameter("@P_CHEQUE", ddlCheque);
                objParams[8] = new SqlParameter("@P_type", Ptype);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.VerifyOutwardRecord->" + ex.ToString());
            }
            return ds;
        }

        public DataSet VerifyInwardOutwardRecord(DateTime FrmDT, DateTime ToDt, int UserNo, int DeptNo, int PostType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_FROM_DATE", FrmDT);
                objParams[1] = new SqlParameter("@P_TO_DATE", ToDt);
                objParams[2] = new SqlParameter("@P_USERNO", UserNo);
                objParams[3] = new SqlParameter("@P_DEPTNO", DeptNo);
                objParams[4] = new SqlParameter("@P_POSTTYPENO", PostType);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_IO_USER_DEPT_POSTTYPE_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.VerifyInwardOutwardRecord->" + ex.ToString());
            }
            return ds;
        }
        public DataSet VerifyInwardRecordDept(DateTime FrmDT, DateTime ToDt, int DeptNo, int Ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_FROM_DATE", FrmDT);
                objParams[1] = new SqlParameter("@P_TO_DATE", ToDt);
                objParams[2] = new SqlParameter("@P_DEPTNO", DeptNo);
                objParams[3] = new SqlParameter("@P_USERNO", Ptype);
                objParams[4] = new SqlParameter("@P_POSTTYPE", Ptype);
                objParams[5] = new SqlParameter("@P_CHEQUE", Ptype);
                objParams[6] = new SqlParameter("@P_type", Ptype);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_INWARD_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.VerifyInwardRecordDept-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet VerifyOutwardRecordDept(DateTime FrmDT, DateTime ToDt, int DeptNo, int UserNo, int PostType, int CarrierNo, int LetterCat, int ddlCheque, int Ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_FROM_DATE", FrmDT);
                objParams[1] = new SqlParameter("@P_TO_DATE", ToDt);
                objParams[2] = new SqlParameter("@P_DEPTNO", DeptNo);
                objParams[3] = new SqlParameter("@P_USERNO", UserNo);
                objParams[4] = new SqlParameter("@P_POSTTYPE", PostType);
                objParams[5] = new SqlParameter("@P_CARRIERNO", CarrierNo);
                objParams[6] = new SqlParameter("@P_LETTERCAT", LetterCat);
                objParams[7] = new SqlParameter("@P_CHEQUE", ddlCheque);
                objParams[8] = new SqlParameter("@P_type", Ptype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_OUTWARD_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController. VerifyOutwardRecordDept->" + ex.ToString());
            }
            return ds;
        }


        public int AddAudit(int amt, DateTime dt)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_AMOUNT", amt);
                objParams[1] = new SqlParameter("@P_AMT_DATE", dt);
                objParams[2] = new SqlParameter("@P_AU_NO", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_AUDIT_INSERT", objParams, true);
                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IOTranCCController.AddAudit-> " + ex.ToString());
            }
            return retStatus;
        }


        public int UpdAudit(int amt, DateTime dt, int au_no)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_AMOUNT", amt);
                objParams[1] = new SqlParameter("@P_AMT_DATE", dt);
                objParams[2] = new SqlParameter("@P_AU_NO", au_no);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_AUDIT_UPD", objParams, true);
                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IOTranCCController.UpdAudit-> " + ex.ToString());
            }
            return retStatus;
        }

        public CustomStatus AddOutwardDispatch(IOTRAN objIOtran)
        {
            throw new NotImplementedException();
        }


        public int DeleteDeptOutward(int IOTRANNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IOTRANNO", IOTRANNO);
                objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_DEPT_OUTWARD_DELETE", objParams, false);
                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.DeleteDeptOutward-> " + ex.ToString());
            }
            return Convert.ToInt32(retStatus);
        }




        #region Dept Outward Accept/ Return

        public DataSet GetDepartmentOutwards()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_DEPT_OUTWARD_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetAllOutwardDispatch-> " + ex.ToString());
            }
            return ds;
        }



        public int UpdateAcceptRejectOutward(IOTRAN objIOTran, string strIOtranNo, string strStatus)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IOTRANNO", strIOtranNo);
                objParams[1] = new SqlParameter("@P_ACCEPT_REJECT_BY", objIOTran.IN_TO_USER);
                objParams[2] = new SqlParameter("@P_STR_STATUS", strStatus);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_UPDATE_ACCEPT_REJECT_STATUS", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }

            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion


        #region Return Dispatch
        public DataSet SearchDispatch(string RefNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_REFNO", RefNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IO_GET_SEARCH_DISPATCH", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.SearchDispatch-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateReturnRemark(IOTRAN objIOTran, DateTime strRDate, string strRRemark)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IOTRANNO", objIOTran.IOTRANNO);
                objParams[1] = new SqlParameter("@P_RETURN_DATE", strRDate);
                objParams[2] = new SqlParameter("@P_RETURN_REMARK", strRRemark);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_UPDATE_RETURN_REMARK", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }

            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.UpdateInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }
        #endregion
        //End IOTranController
    }//End BusinessLayer.BusinessLogic 
        }//UAIMS
    }
}//IITMS

