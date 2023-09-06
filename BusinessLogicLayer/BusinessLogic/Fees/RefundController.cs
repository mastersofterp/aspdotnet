//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : REFUND CONTROLLER
// CREATION DATE : 29-JUL-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
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

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class RefundController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllCollections(int studentId, int dcrNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DCR_NO", dcrNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_REFUND_GET_ALL_COLLECTION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RefundController.GetAllCollections() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAmountsToRefund(int dcr_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DCR_NO", dcr_no)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_REFUND_FEE_ITEMS_AMOUNT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RefundController.GetAmountsToRefund() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetVoucherNo(int UserId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_USER_NO", UserId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_REFUND_GET_VOUCHER_NO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RefundController.GetVoucherNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //public bool SaveRefund_Transaction(ref Refund refund)
        //{
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {                    
        //            new SqlParameter("@P_DCR_NO", refund.DCR_NO),
        //            new SqlParameter("@P_REC_NO", refund.ReceiptNo),
        //            new SqlParameter("@P_IDNO", refund.IDNO),
        //            new SqlParameter("@P_VOUCHER_NO", refund.VoucherNo),
        //            new SqlParameter("@P_VOUCHER_DATE", refund.VoucherDate),
        //            new SqlParameter("@P_F1", refund.FeeItemsAmount[0]),
        //            new SqlParameter("@P_F2", refund.FeeItemsAmount[1]),
        //            new SqlParameter("@P_F3", refund.FeeItemsAmount[2]),
        //            new SqlParameter("@P_F4", refund.FeeItemsAmount[3]),
        //            new SqlParameter("@P_F5", refund.FeeItemsAmount[4]),
        //            new SqlParameter("@P_F6", refund.FeeItemsAmount[5]),
        //            new SqlParameter("@P_F7", refund.FeeItemsAmount[6]),
        //            new SqlParameter("@P_F8", refund.FeeItemsAmount[7]),
        //            new SqlParameter("@P_F9", refund.FeeItemsAmount[8]),
        //            new SqlParameter("@P_F10",refund.FeeItemsAmount[9]),
        //            new SqlParameter("@P_F11",refund.FeeItemsAmount[10]),
        //            new SqlParameter("@P_F12",refund.FeeItemsAmount[11]),
        //            new SqlParameter("@P_F13",refund.FeeItemsAmount[12]),
        //            new SqlParameter("@P_F14",refund.FeeItemsAmount[13]),
        //            new SqlParameter("@P_F15",refund.FeeItemsAmount[14]),
        //            new SqlParameter("@P_F16",refund.FeeItemsAmount[15]),
        //            new SqlParameter("@P_F17",refund.FeeItemsAmount[16]),
        //            new SqlParameter("@P_F18",refund.FeeItemsAmount[17]),
        //            new SqlParameter("@P_F19",refund.FeeItemsAmount[18]),
        //            new SqlParameter("@P_F20",refund.FeeItemsAmount[19]),
        //            new SqlParameter("@P_F21",refund.FeeItemsAmount[20]),
        //            new SqlParameter("@P_F22",refund.FeeItemsAmount[21]),
        //            new SqlParameter("@P_F23",refund.FeeItemsAmount[22]),
        //            new SqlParameter("@P_F24",refund.FeeItemsAmount[23]),
        //            new SqlParameter("@P_F25",refund.FeeItemsAmount[24]),
        //            new SqlParameter("@P_F26",refund.FeeItemsAmount[25]),
        //            new SqlParameter("@P_F27",refund.FeeItemsAmount[26]),
        //            new SqlParameter("@P_F28",refund.FeeItemsAmount[27]),
        //            new SqlParameter("@P_F29",refund.FeeItemsAmount[28]),
        //            new SqlParameter("@P_F30",refund.FeeItemsAmount[29]),
        //            new SqlParameter("@P_F31", refund.FeeItemsAmount[30]),
        //            new SqlParameter("@P_F32", refund.FeeItemsAmount[31]),
        //            new SqlParameter("@P_F33", refund.FeeItemsAmount[32]),
        //            new SqlParameter("@P_F34", refund.FeeItemsAmount[33]),
        //            new SqlParameter("@P_F35", refund.FeeItemsAmount[34]),
        //            new SqlParameter("@P_F36", refund.FeeItemsAmount[35]),
        //            new SqlParameter("@P_F37", refund.FeeItemsAmount[36]),
        //            new SqlParameter("@P_F38", refund.FeeItemsAmount[37]),
        //            new SqlParameter("@P_F39", refund.FeeItemsAmount[38]),
        //            new SqlParameter("@P_F40",refund.FeeItemsAmount[39]),
        //            new SqlParameter("@P_EXCESS_AMOUNT", refund.excessamount),
        //            new SqlParameter("@P_TOTAL_AMT", refund.TotalAmount),
        //            new SqlParameter("@P_CHQ_DD_AMT", refund.ChequeDD_Amount),
        //            new SqlParameter("@P_CASH_AMT", refund.CashAmount),                    
        //            new SqlParameter("@P_PAY_TYPE", refund.PayType),
        //            new SqlParameter("@P_CAN", refund.IsCanceled),                   
        //            new SqlParameter("@P_PRINTDATE", (refund.PrintDate != DateTime.MinValue) ? refund.PrintDate as object  : DBNull.Value as object),
        //            new SqlParameter("@P_REMARK", refund.Remark),
        //            new SqlParameter("@P_COUNTER_NO", refund.CounterNo),
        //            new SqlParameter("@P_UA_NO", refund.UserNo),
        //            new SqlParameter("@P_COLLEGE_CODE", refund.CollegeCode),
        //            new SqlParameter("@P_REFUND_NO", refund.RefundNo)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        object obj = objDataAccess.ExecuteNonQuerySP("PKG_REFUND_INS_REFUND", sqlParams, true);

        //        if (obj != null && obj.ToString() != string.Empty)
        //            refund.RefundNo = int.Parse(obj.ToString());

        //        // if pay type is demand draft(D) Or Cheque then save demand draft/Cheque details also.
        //        if ((refund.PayType == "D" || refund.PayType == "Q") && refund.RefundNo > 0)
        //        {
        //            foreach (DemandDrafts dd in refund.PaidChequesDemandDrafts)
        //            {
        //                sqlParams = new SqlParameter[] 
        //                { 
        //                    new SqlParameter("@P_DCRNO", refund.RefundNo),
        //                    new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
        //                    new SqlParameter("@P_IDNO", refund.IDNO),
        //                    new SqlParameter("@P_BANKNO", dd.BankNo),
        //                    new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
        //                    new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
        //                    new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
        //                    new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
        //                    new SqlParameter("@P_COLLEGE_CODE", refund.CollegeCode)
        //                };
        //                objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN_REFUND", sqlParams, false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RefundController.SaveRefund_Transaction() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return true;
        //}

        //Added by Deepali [19-08-2020]

        public bool SaveRefund_Transaction(ref Refund refund, int OnPaymode)
            {
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_DCR_NO", refund.DCR_NO),
                    new SqlParameter("@P_REC_NO", refund.ReceiptNo),
                    new SqlParameter("@P_IDNO", refund.IDNO),
                    new SqlParameter("@P_VOUCHER_NO", refund.VoucherNo),
                    new SqlParameter("@P_VOUCHER_DATE", refund.VoucherDate),
                    new SqlParameter("@P_F1", refund.FeeItemsAmount[0]),
                    new SqlParameter("@P_F2", refund.FeeItemsAmount[1]),
                    new SqlParameter("@P_F3", refund.FeeItemsAmount[2]),
                    new SqlParameter("@P_F4", refund.FeeItemsAmount[3]),
                    new SqlParameter("@P_F5", refund.FeeItemsAmount[4]),
                    new SqlParameter("@P_F6", refund.FeeItemsAmount[5]),
                    new SqlParameter("@P_F7", refund.FeeItemsAmount[6]),
                    new SqlParameter("@P_F8", refund.FeeItemsAmount[7]),
                    new SqlParameter("@P_F9", refund.FeeItemsAmount[8]),
                    new SqlParameter("@P_F10",refund.FeeItemsAmount[9]),
                    new SqlParameter("@P_F11",refund.FeeItemsAmount[10]),
                    new SqlParameter("@P_F12",refund.FeeItemsAmount[11]),
                    new SqlParameter("@P_F13",refund.FeeItemsAmount[12]),
                    new SqlParameter("@P_F14",refund.FeeItemsAmount[13]),
                    new SqlParameter("@P_F15",refund.FeeItemsAmount[14]),
                    new SqlParameter("@P_F16",refund.FeeItemsAmount[15]),
                    new SqlParameter("@P_F17",refund.FeeItemsAmount[16]),
                    new SqlParameter("@P_F18",refund.FeeItemsAmount[17]),
                    new SqlParameter("@P_F19",refund.FeeItemsAmount[18]),
                    new SqlParameter("@P_F20",refund.FeeItemsAmount[19]),
                    new SqlParameter("@P_F21",refund.FeeItemsAmount[20]),
                    new SqlParameter("@P_F22",refund.FeeItemsAmount[21]),
                    new SqlParameter("@P_F23",refund.FeeItemsAmount[22]),
                    new SqlParameter("@P_F24",refund.FeeItemsAmount[23]),
                    new SqlParameter("@P_F25",refund.FeeItemsAmount[24]),
                    new SqlParameter("@P_F26",refund.FeeItemsAmount[25]),
                    new SqlParameter("@P_F27",refund.FeeItemsAmount[26]),
                    new SqlParameter("@P_F28",refund.FeeItemsAmount[27]),
                    new SqlParameter("@P_F29",refund.FeeItemsAmount[28]),
                    new SqlParameter("@P_F30",refund.FeeItemsAmount[29]),
                    new SqlParameter("@P_F31", refund.FeeItemsAmount[30]),
                    new SqlParameter("@P_F32", refund.FeeItemsAmount[31]),
                    new SqlParameter("@P_F33", refund.FeeItemsAmount[32]),
                    new SqlParameter("@P_F34", refund.FeeItemsAmount[33]),
                    new SqlParameter("@P_F35", refund.FeeItemsAmount[34]),
                    new SqlParameter("@P_F36", refund.FeeItemsAmount[35]),
                    new SqlParameter("@P_F37", refund.FeeItemsAmount[36]),
                    new SqlParameter("@P_F38", refund.FeeItemsAmount[37]),
                    new SqlParameter("@P_F39", refund.FeeItemsAmount[38]),
                    new SqlParameter("@P_F40",refund.FeeItemsAmount[39]),
                    new SqlParameter("@P_EXCESS_AMOUNT", refund.excessamount),
                    new SqlParameter("@P_TOTAL_AMT", refund.TotalAmount),
                    new SqlParameter("@P_CHQ_DD_AMT", refund.ChequeDD_Amount),
                    new SqlParameter("@P_CASH_AMT", refund.CashAmount),                    
                    new SqlParameter("@P_PAY_TYPE", refund.PayType),
                    new SqlParameter("@P_CAN", refund.IsCanceled),                   
                    new SqlParameter("@P_PRINTDATE", (refund.PrintDate != DateTime.MinValue) ? refund.PrintDate as object  : DBNull.Value as object),
                    new SqlParameter("@P_REMARK", refund.Remark),
                    new SqlParameter("@P_COUNTER_NO", refund.CounterNo),
                    new SqlParameter("@P_UA_NO", refund.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", refund.CollegeCode),
                     new SqlParameter("@P_CANCELLATION_CHARGES",refund.Cancellation_Charges),//added by dileep kare on 31.12.2021
                    new SqlParameter("@P_ORGANIZATIONID",refund.Organizationid),//added by dileep kare on 31.12.2021
                    new SqlParameter("@P_REFUND_NO", refund.RefundNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_REFUND_INS_REFUND", sqlParams, true);

                if (obj != null && obj.ToString() != string.Empty)
                    refund.RefundNo = int.Parse(obj.ToString());

                // if pay type is demand draft(D) Or Cheque then save demand draft/Cheque details also.
                if ((refund.PayType == "D" || refund.PayType == "Q") && refund.RefundNo > 0)
                    {
                    foreach (DemandDrafts dd in refund.PaidChequesDemandDrafts)
                        {
                        sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_DCRNO", refund.RefundNo),
                            new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
                            new SqlParameter("@P_IDNO", refund.IDNO),
                            new SqlParameter("@P_BANKNO", dd.BankNo),
                            new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
                            new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
                            new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
                            new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
                            new SqlParameter("@P_COLLEGE_CODE", refund.CollegeCode)
                        };
                        objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN_REFUND", sqlParams, false);
                        }
                    }

                if ((refund.PayType == "O") && refund.RefundNo > 0)
                    {
                    foreach (DemandDrafts dd in refund.PaidChequesDemandDrafts)
                        {
                        sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_DCRNO", refund.DCR_NO),
                            new SqlParameter("@P_IDNO", refund.IDNO),
                            new SqlParameter("@P_ONLINE_MODE",OnPaymode),
                            new SqlParameter("@P_TRANSACTION_NO", dd.DemandDraftNo),
                            new SqlParameter("@P_BANKNO", dd.BankNo),
                            new SqlParameter("@P_TRANSACTION_DATE", dd.DemandDraftDate),
                            new SqlParameter("@P_ONLINE_BANK", dd.DemandDraftBank),
                            new SqlParameter("@P_ONLINE_CITY", dd.DemandDraftCity),
                            new SqlParameter("@P_ONLINE_AMOUNT",dd.DemandDraftAmount )
                        };
                        objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_ONLINE_REFUND", sqlParams, false);
                        }
                    }
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RefundController.SaveRefund_Transaction() --> " + ex.Message + " " + ex.StackTrace);
                }
            return true;
            }
        public DataSet GetActiveStudentDetail(int admbatch, int degree, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_ADMBATCH", admbatch),
                    new SqlParameter("@P_DEGREENO", degree),
                    new SqlParameter("@P_BRANCHNO", branchno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACTIVE_STUDENT_DETAIL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.RefundController.GetActiveStudentDetail() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        //Added by Deepali [19-08-2020]
        public DataSet GetActiveStudentCount(int admbatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_ADMBATCH", admbatch),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACTIVE_STUDENT_COUNT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RefundController.GetActiveStudentCount() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}