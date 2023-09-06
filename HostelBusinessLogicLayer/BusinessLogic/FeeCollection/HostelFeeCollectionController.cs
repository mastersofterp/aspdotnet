//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : FEE COLLECTION CONTROLLER CLASS
// CREATION DATE : 27-JULY-2022                                                        
// CREATED BY    : SAURABH LONARE
// MODIFIED DATE :
// MODIFIED DESC :
// PURPOSE       : FEE RELATED ONLINE PAYMENT RELATED METHODS
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

namespace HostelBusinessLogicLayer.BusinessLogic
{
    public class HostelFeeCollectionController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetOnlinePaymentConfigurationDetails(int Organizationid, int payid, int activityno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_ACTIVITYNO", activityno),
                    new SqlParameter("@P_PAYID",payid)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_CONFIG_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.GetOnlinePaymentConfigurationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet GetStudentFeesforOnlinePayment(string RecieptCode, int Sem, int IDNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPTCODE", RecieptCode),
                    new SqlParameter("@P_SEMESTERNO", Sem),
                    new SqlParameter("@P_IDNO", IDNO)
                };
               // ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_STDUENT_FEES", sqlParams); commented by Saurabh L on 30 May 2023

                ds = objDataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STDUENT_FEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.GetStudentFeesforOnlinePayment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertOnlinePaymentlog(string userno, string recipt, string PaymentMode, string Amount, string status, string PayId, int OrgId, int RoomNo)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[9];
                    sqlparam[0] = new SqlParameter("@P_IDNO", userno);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[3] = new SqlParameter("@P_AMOUNT", Amount);
                    sqlparam[4] = new SqlParameter("@P_STATUS", status);
                    sqlparam[5] = new SqlParameter("@P_PAYID", PayId);
                    sqlparam[6] = new SqlParameter("@P_ORGANIZATIONID", OrgId);
                    sqlparam[7] = new SqlParameter("@P_ROOM_NO", RoomNo);
                    sqlparam[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[8].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();
                };
               
                // object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ONLINE_PAYMENT_FOR_LOG_ADMIN", sqlparam, true);
                //Above line commited by Saurabh L on 31 May 2023 and added below line

                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_HOSTEL_ONLINE_PAYMENT_LOG", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }

        }

        public int InsertInstallmentOnlinePayment_TempDCR(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_DM_NO", Dmno),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_AMOUNT", amount),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_UANO", uano),
                            new SqlParameter("@P_MESSAGE", data),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_TempDCR(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_ONLINE_PAYMENT_DCR_TEMP", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string Regno, string msg, int OrgID)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[11];
                    sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                    sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[4] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[5] = new SqlParameter("@P_CASHBOOK", CashBook);
                    sqlparam[6] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[7] = new SqlParameter("@P_PAY_STATUS", StatusF);
                    sqlparam[8] = new SqlParameter("@P_MESSAGE", msg);
                    sqlparam[9] = new SqlParameter("@P_ORGID", OrgID);  // P_ORGID added by Saurabh L on 23/09/2022
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public DataSet GetStudentInfoById(int studentId, int OrgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_ORGANIZATION_ID",OrgId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessLogic.HostelFeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeItems_Data_For_Student(int sessionNo, int studentId, int semesterNo, string receiptType, int payTypeNo, ref int status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECEIPT_CODE", receiptType),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_FOR_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetPaidReceiptsInfoByStudIdOnline(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
               // ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA_ONLINE", sqlParams);
                //Added by Saurabh on 09/08/2022 Purpose: Only Hostel RECEIPT Report show
                ds = objDataAccess.ExecuteDataSetSP("PKG_HOSTEL_FEECOLLECT_PAID_RECEIPT_DATA_ONLINE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudIdOnline() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public string CreateHostelFeeDemand(string studentIDs, String ReceiptTypeCode, string UserNo, int sessionno, string CollegeCode, int SemesterNo, bool overwrite, int ForSemester, int PayType, int roomno)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STUDENT_NO", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", ReceiptTypeCode),
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    //new SqlParameter("@P_COLLEGE_ID", collegeid),
                    new SqlParameter("@P_SEMESTERNO", SemesterNo),
                    new SqlParameter("@P_FORSEMESTERNO", ForSemester),
                    new SqlParameter("@P_PAY_FULL_HALF", PayType), // ADDED PATMENT TYPE WHICH NOT PASS BY SHUBHAM ON 06/06/22
                    new SqlParameter("@P_ROOMNO", roomno),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].DbType = DbType.String;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_HOSTEL_CREATE_DEMAND_BY_STUDENT", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public int InsertHostelDemandLog(int IDNO, int SESSIONNO, int SEMESTERNO, int OrgId)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORGANIZATION_ID",OrgId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_DEMAND_LOG", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessEntities.HostelFeeCollectionController.InsertHostelDemandLog-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}
