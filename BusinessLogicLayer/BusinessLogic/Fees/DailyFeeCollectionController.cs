//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : DAILY FEE COLLECTION REPORT CONTROLLER
// CREATION DATE : 11-JUN-2009                                                        
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
    public class DailyFeeCollectionController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetCollectionData(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_FROM_DATE", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate as object : DBNull.Value as object)),
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAY_MODE", feeCollectRpt.PaymentMode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_FEE_COLLECTION_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeCollectionReportData(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEECOLLECT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDemandDraftsReportData(DailyFeeCollectionRpt feeCollectRpt, int version)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_VERSION", version),
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_BANKWISE_DD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        public DataSet GetFeeItemsDetailed_ReportData(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEEITEMS_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeItemsSummary_ReportData(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEEITEMS_SUMMARY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeItemsReportData(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEEITEMS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetOutstandingFeeReportData(DailyFeeCollectionRpt feeCollectRpt, bool showBalance)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_SHOWBALANCE", showBalance)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //public DataSet GetSelectedFeeItem(DailyFeeCollectionRpt dcrReport, string[] feeHead)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSelectedFeeItem = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //    {
        //        new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
        //        new SqlParameter("@P_FROM_DATE",(dcrReport.FromDate != DateTime.MinValue ? dcrReport.FromDate.ToString() as object : DBNull.Value as object)),
        //        new SqlParameter("@P_TO_DATE",(dcrReport.ToDate != DateTime.MinValue ? dcrReport.ToDate.ToString() as object : DBNull.Value as object)),
        //        new SqlParameter("@P_SEMESTERNO",dcrReport.SemesterNo),
        //        new SqlParameter("@P_BRANCHNO",dcrReport.BranchNo),
        //        new SqlParameter("@P_DEGREENO",dcrReport.DegreeNo),
        //        new SqlParameter("@P_YEAR",dcrReport.YearNo),
        //        new SqlParameter("@P_FEEHEAD1",feeHead[0]),
        //        new SqlParameter("@P_FEEHEAD2",feeHead[1]),
        //        new SqlParameter("@P_FEEHEAD3",feeHead[2]),
        //        new SqlParameter("@P_FEEHEAD4",feeHead[3]),
        //        new SqlParameter("@P_FEEHEAD5",feeHead[4])
        //    };
        //        ds = objSelectedFeeItem.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_SELECTED_FEEHEAD", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DailyFeeCollectionController.GetSelectedFeeItem --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}


        /// <summary>
        /// Abhishek Shirpurkar........
        /// </summary>
        /// <param name="dcrReport"></param>
        /// <param name="feeHead"></param>
        /// <returns></returns>
        public DataSet GetSelectedFeeItem(DailyFeeCollectionRpt dcrReport, string[] feeHead)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSelectedFeeItem = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
                new SqlParameter("@P_FROM_DATE",(dcrReport.FromDate != DateTime.MinValue ? dcrReport.FromDate.ToString() as object : DBNull.Value as object)),
                new SqlParameter("@P_TO_DATE",(dcrReport.ToDate != DateTime.MinValue ? dcrReport.ToDate.ToString() as object : DBNull.Value as object)),
                new SqlParameter("@P_SEMESTERNO",dcrReport.SemesterNo),
                new SqlParameter("@P_BRANCHNO",dcrReport.BranchNo),
                new SqlParameter("@P_DEGREENO",dcrReport.DegreeNo),
                new SqlParameter("@P_YEAR",dcrReport.YearNo),
                new SqlParameter("@P_FEEHEAD1",feeHead[0]),
                new SqlParameter("@P_FEEHEAD2",feeHead[1]),
                new SqlParameter("@P_FEEHEAD3",feeHead[2]),
                new SqlParameter("@P_FEEHEAD4",feeHead[3]),
                new SqlParameter("@P_FEEHEAD5",feeHead[4]),
                new SqlParameter("@P_FEEHEAD6",feeHead[5])
            };
                ds = objSelectedFeeItem.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_SELECTED_FEEHEAD", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DailyFeeCollectionController.GetSelectedFeeItem --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCautionMoneyReportData(DailyFeeCollectionRpt reportParams, string cautionMoneyHead)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSelectedFeeItem = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@P_RECIEPT_CODE",reportParams.ReceiptTypes),
                new SqlParameter("@P_FROM_DATE",(reportParams.FromDate != DateTime.MinValue ? reportParams.FromDate.ToString() as object : DBNull.Value as object)),
                new SqlParameter("@P_TO_DATE",(reportParams.ToDate != DateTime.MinValue ? reportParams.ToDate.ToString() as object : DBNull.Value as object)),
                new SqlParameter("@P_SEMESTERNO",reportParams.SemesterNo),
                new SqlParameter("@P_BRANCHNO",reportParams.BranchNo),
                new SqlParameter("@P_DEGREENO",reportParams.DegreeNo),
                new SqlParameter("@P_YEAR",reportParams.YearNo),
                new SqlParameter("@P_FEEHEAD",cautionMoneyHead),
                new SqlParameter("@P_PAIDAMOUNT",reportParams.PaidAmount)
               
            };
                ds = objSelectedFeeItem.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_CAUTION_MONEY_DEPOSIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DailyFeeCollectionController.GetCautionMoneyReportData --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRecieptFeeItems(string receiptCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE",receiptCode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_FEE_ITEMS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DailyFeeCollectionController.GetRecieptFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetSelectedFeeTitle(string RecieptCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSelectedFeeTitle = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE",RecieptCode)
                };
                ds = objSelectedFeeTitle.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEETITLE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DailyFeeCollectionController.GetSelectedFeeTitle() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetDemandDraftsReportDataSummary(DailyFeeCollectionRpt feeCollectRpt, int version)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_VERSION", version),
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_BANKWISE_DD_SUMMARY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetOutstandingFees(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_VERSION", version),
                    //new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    //new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPT_CODE",   feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    //new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_SHOWBALANCE", "0"),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeCollectionReportDataExcel(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPTCODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_PAYTYPENO", feeCollectRpt.PayTypeNo),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEECOLLECT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetOutstandingFeesExcel(DailyFeeCollectionRpt feeCollectRpt)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_VERSION", version),
                    //new SqlParameter("@P_COUNTERNO", feeCollectRpt.CounterNo),
                    //new SqlParameter("@P_PAYMENT_MODE", feeCollectRpt.PaymentMode),
                    new SqlParameter("@P_RECIEPT_CODE", feeCollectRpt.ReceiptTypes),
                    new SqlParameter("@P_DEGREENO", feeCollectRpt.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeCollectRpt.BranchNo),
                    //new SqlParameter("@P_YEARNO", feeCollectRpt.YearNo),
                    new SqlParameter("@P_SEMESTERNO", feeCollectRpt.SemesterNo),
                    new SqlParameter("@P_SHOWBALANCE", feeCollectRpt.ShowBalance),
                    new SqlParameter("@P_REC_FROM_DT", ((feeCollectRpt.FromDate != DateTime.MinValue) ? feeCollectRpt.FromDate.ToShortDateString() as object : DBNull.Value as object)),
                    new SqlParameter("@P_REC_TO_DATE", ((feeCollectRpt.ToDate != DateTime.MinValue) ? feeCollectRpt.ToDate.ToShortDateString() as object : DBNull.Value as object))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetUserFeesPaidData(int admbatch, int status, int programtype, int degree, int branch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_ADMBATCH",admbatch),
                    new SqlParameter("@P_APPLICATION_STATUS",status),
                    new SqlParameter("@P_PROGRAMTYPE",programtype),
                    new SqlParameter("@P_DEGREE",degree),
                     new SqlParameter("@P_BRANCH",branch)
                     
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_USERFEESPAID_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetUserJEEVerifed(int sessionno, int admcat, string appid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_QUALIFYNO",admcat),
                    new SqlParameter("@P_APPID",appid),
                                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_USERJEEVERIFY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUserJEEVerifed() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int GetUserjeeinsertData(int sessionno, string userno, int ENTR_EXAM_NO, int VERIFY_BY, string jeetotal)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                {
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_USERNOS", userno);
                    objParams[2] = new SqlParameter("@ENTR_EXAM_NO", ENTR_EXAM_NO);
                    objParams[3] = new SqlParameter("@VERIFY_BY", VERIFY_BY);
                    objParams[4] = new SqlParameter("@P_JEE_TOTAL", jeetotal);
                    objParams[5] = new SqlParameter("@P_IDNO", SqlDbType.Int);
                    objParams[5].Direction = ParameterDirection.Output;
                };

                if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_USERJEE_DATA", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Swapnil - 23082019 -- to paymode excel report
        /// </summary>
        /// <param name="dcrReport"></param>
        /// <returns></returns>
        public DataSet GetPaymodeReport(DailyFeeCollectionRpt dcrReport)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
                     new SqlParameter("@P_BRANCHNO", dcrReport.BranchNo),
                    new SqlParameter("@P_DEGREENO", dcrReport.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcrReport.SemesterNo),
                    new SqlParameter("@P_REC_FROM_DT", dcrReport.FromDate),
                     new SqlParameter("@P_REC_TO_DATE", dcrReport.ToDate),
                      new SqlParameter("@P_PAYMODE", dcrReport.PaymentMode),
                      new SqlParameter("@P_YERNO", dcrReport.YearNo)
               
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_REPORT_FOR_PAYMENT_MODE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetMeritList(int SESSIONNO, int DEGREENO, int QUALIFYNO, int CUTOFFMARKS, string app_date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_SESSIONNO",SESSIONNO),
                     new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_QUALIFYNO", QUALIFYNO),
                    new SqlParameter("@P_CUTOFFMARKS", CUTOFFMARKS),
                    new SqlParameter("@P_APPDATE", app_date)
               
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_REPORT_GENERATE_MERIT_LIST_FOR_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Created by Dileep Kare 
        /// date:27-12-2019
        /// for Refund Reciept Excel Sheet Report.
        /// Modified Date:06-01-2020
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <param name="recieptcode"></param>
        /// <returns></returns>
        public DataSet GetRefundReport(int sessionno, int degreeno, int branchno, string recieptcode, string fromdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                 {
                     new SqlParameter("@P_SESSIONNO",sessionno),
                     new SqlParameter("@P_DEGREENO",degreeno),
                     new SqlParameter("@P_BRANCHNO",branchno),
                     new SqlParameter("@P_RECIEPT_CODE",recieptcode),
                     new SqlParameter("@P_FROMDATE",fromdate),
                     new SqlParameter("@P_TODATE",todate)
                 };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_REFUNF_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetOutstandingReportExcel(DailyFeeCollectionRpt outstandingreport)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNo);
                objParams[4] = new SqlParameter("@P_SHOWBALANCE", outstandingreport.ShowBalance);
                objParams[5] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FDate);
                objParams[6] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);
                objParams[7] = new SqlParameter("@P_PAYTYPENO", outstandingreport.PayTypeNo);
                objParams[8] = new SqlParameter("@P_COUNTERNO", outstandingreport.CounterNo);
                objParams[9] = new SqlParameter("@P_PAYMODE", outstandingreport.PaymentMode);
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcel() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        /// <summary>
        /// Added by Swapnil - 23082019 -- to paymode excel report
        /// </summary>
        /// <param name="dcrReport"></param>
        /// <returns></returns>
        public DataSet GetPayTypeReport(DailyFeeCollectionRpt dcrReport)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
                     new SqlParameter("@P_BRANCHNO", dcrReport.BranchNo),
                    new SqlParameter("@P_DEGREENO", dcrReport.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcrReport.SemesterNo),
                    new SqlParameter("@P_REC_FROM_DT", dcrReport.FromDate),
                     new SqlParameter("@P_REC_TO_DATE", dcrReport.ToDate),
                      new SqlParameter("@P_PAYMODE", dcrReport.PaymentMode),
                      new SqlParameter("@P_YERNO", dcrReport.YearNo)
               
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_REPORT_FOR_PAY_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by SP - 21032022
        /// Modified By Rishabh on 01062022 - for multiselect Filter
        /// </summary>
        /// <param name="outstandingreport"></param>
        /// <returns></returns>
        public DataSet GetOutstandingReportExcelFormat2(DailyFeeCollectionRpt outstandingreport)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNos);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNos);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNos);
                objParams[4] = new SqlParameter("@P_SHOWBALANCE", outstandingreport.ShowBalance);
                objParams[5] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FDate);
                objParams[6] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);
                objParams[7] = new SqlParameter("@P_PAYTYPENO", outstandingreport.PayTypeNo);
                objParams[8] = new SqlParameter("@P_COUNTERNO", outstandingreport.CounterNo);
                objParams[9] = new SqlParameter("@P_PAYMODE", outstandingreport.PaymentMode);
                objParams[10] = new SqlParameter("@P_YRNOS", outstandingreport.YearNos);
                objParams[11] = new SqlParameter("@P_COLLEGE_ID", outstandingreport.CollegeNos);
                ds = objDataAccess.ExecuteDataSetSP("PKG_REPORT_OUTSTANDING_FORMAT_EXCEL", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcelFormat2() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertUserVerifyStatus(int userno, int branch, int degree, int verifyBy, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[7];
                {

                    objParams[0] = new SqlParameter("@P_USERNO", userno);
                    objParams[1] = new SqlParameter("@P_BRANCH", branch);
                    objParams[2] = new SqlParameter("@P_DEGREE", degree);
                    objParams[3] = new SqlParameter("@P_VERIFY_BY", verifyBy);
                    objParams[4] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    objParams[5] = new SqlParameter("@P_IDNO", SqlDbType.Int);
                    objParams[6].Direction = ParameterDirection.Output;
                };

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_INSERT_VERIFY_STATUS", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int GetUserjeeinsertData(int userno, int VERIFY_BY, string ipaddress)//, int Branchno, int Degreeno, int Batchno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                {

                    objParams[0] = new SqlParameter("@P_USERNO", userno);
                    objParams[1] = new SqlParameter("@P_VERIFY_BY", VERIFY_BY);
                    objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    // objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                    // objParams[4] = new SqlParameter("@P_DEGREENO", Degreeno);
                    // objParams[5] = new SqlParameter("@P_ADMBATCH", Batchno);
                    objParams[3] = new SqlParameter("@P_IDNO", SqlDbType.Int);
                    objParams[3].Direction = ParameterDirection.Output;
                };

                if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_USERJEE_DATA", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet GetOutstandingReportExcelFormatII(DailyFeeCollectionRpt outstandingreport)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNo);
                objParams[4] = new SqlParameter("@P_SHOWBALANCE", outstandingreport.ShowBalance);
                objParams[5] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FDate);
                objParams[6] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);
                objParams[7] = new SqlParameter("@P_PAYTYPENO", outstandingreport.PayTypeNo);
                objParams[8] = new SqlParameter("@P_COUNTERNO", outstandingreport.CounterNo);
                objParams[9] = new SqlParameter("@P_PAYMODE", outstandingreport.PaymentMode);
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES_FOR_EXCEL_FORMAT_II", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcelFormatII() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetNewOutstandingReportExcelFormatIII(DailyFeeCollectionRpt outstandingreport)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNo);
                objParams[4] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FromDate);
                objParams[5] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);

                ds = objDataAccess.ExecuteDataSetSP("PKG_DEMAND_WISE_REPORT_OUTSTANDINGFEES_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcelFormatII() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by SP - 21032022
        /// Modified By Rishabh on 01062022 - for multiselect Filter
        /// </summary>
        /// <param name="outstandingreport"></param>
        /// <returns></returns>
        public DataSet GetCrescentOutstandingReportExcelFormat2(DailyFeeCollectionRpt outstandingreport,int Admbatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNos);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNos);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNos);
                objParams[4] = new SqlParameter("@P_SHOWBALANCE", outstandingreport.ShowBalance);
                objParams[5] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FDate);
                objParams[6] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);
                objParams[7] = new SqlParameter("@P_PAYTYPENO", outstandingreport.PayTypeNo);
                objParams[8] = new SqlParameter("@P_COUNTERNO", outstandingreport.CounterNo);
                objParams[9] = new SqlParameter("@P_PAYMODE", outstandingreport.PaymentMode);
                objParams[10] = new SqlParameter("@P_YRNOS", outstandingreport.YearNos);
                objParams[11] = new SqlParameter("@P_COLLEGE_ID", outstandingreport.CollegeNos);
                objParams[12] = new SqlParameter("@P_ADMBATCH", Admbatch);
                ds = objDataAccess.ExecuteDataSetSP("PKG_REPORT_OUTSTANDING_FORMAT_EXCEL_CRESCENT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcelFormat2() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCrescentOutstandingReportExcelFormatII(DailyFeeCollectionRpt outstandingreport, int Admbatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", outstandingreport.ReceiptTypes);
                objParams[1] = new SqlParameter("@P_BRANCHNO", outstandingreport.BranchNos);
                objParams[2] = new SqlParameter("@P_DEGREENO", outstandingreport.DegreeNos);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", outstandingreport.SemesterNos);
                objParams[4] = new SqlParameter("@P_SHOWBALANCE", outstandingreport.ShowBalance);
                objParams[5] = new SqlParameter("@P_REC_FROM_DT", outstandingreport.FDate);
                objParams[6] = new SqlParameter("@P_REC_TO_DATE", outstandingreport.ToDate);
                objParams[7] = new SqlParameter("@P_PAYTYPENO", outstandingreport.PayTypeNo);
                objParams[8] = new SqlParameter("@P_COUNTERNO", outstandingreport.CounterNo);
                objParams[9] = new SqlParameter("@P_PAYMODE", outstandingreport.PaymentMode);
                objParams[10] = new SqlParameter("@P_ADMBATCH", Admbatch);
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_OUTSTANDINGFEES_FOR_EXCEL_FORMAT_II_CRESCENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.Businesslayer.BusinessEntities.FeeCollectionController.GetOutstandingReportExcelFormatII() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        
    }
}
