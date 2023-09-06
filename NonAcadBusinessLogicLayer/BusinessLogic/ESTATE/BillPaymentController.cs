using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class BillPaymentController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Bill Payment

        public long AddUpdateBillPayment(BillPayment objBPay)
        {
            long retstatus = 0;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_P_ID", objBPay.PID);
                objParams[1] = new SqlParameter("@P_DATE", objBPay.DATE);
                objParams[2] = new SqlParameter("@P_RECEIPT_NO", objBPay.RECEIPT_NO);
                objParams[3] = new SqlParameter("@P_RECEIPT_DATE", objBPay.RECEIPT_DATE);
                objParams[4] = new SqlParameter("@P_PAID_AMOUNT", objBPay.PAID_AMOUNT);
                objParams[5] = new SqlParameter("@P_BALANCE_AMOUNT", objBPay.BALANCE_AMOUNT);
                objParams[6] = new SqlParameter("@P_TOTAL_AMOUNT", objBPay.TOTAL_AMOUNT);  
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
               
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_BILL_PAYMENT_INSERT_UPDATE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAppController.AddUpdateOnlineApplication->" + ex.ToString());
            }
            return retstatus;
        }


        public DataSet GetBillDetails(int PID, string bDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_P_ID", PID);
                objParams[1] = new SqlParameter("@P_DATE", bDate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_GET_BILL_DETAILS_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BillPaymentController.GetBillDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetFirstBillProcessData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);               
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_FIRST_BILL_PROCESS_DATA", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }

        public int InsertArrear(BillPayment objBPay)
        {
            int retstatus = 0;
            try
            {               
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_ARREAR_TBL", objBPay.ARREAR_TABLE);              
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ARREAR_INSERT_UPDATE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);               
                else
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BillPaymentController.InsertArrear->" + ex.ToString());
            }
            return retstatus;
        }


        public SqlDataReader AutoCompleteEstateEmployee(string preFix)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                dr = objSQLHelper.ExecuteReaderSP("PKG_ESTATE_AUTOCOMPLETE_ALL_EMPLOYEE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LibAutoComplete.AutoCompleteAccNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        #endregion

    }
}
