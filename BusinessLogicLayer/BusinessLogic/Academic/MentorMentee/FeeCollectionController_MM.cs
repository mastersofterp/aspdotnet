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
namespace BusinessLogicLayer.BusinessLogic.Academic.MentorMentee
{
    public class FeeCollectionController_MM
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet Get_Fee_Details_DCR_Excel_Report_FormatII(int degreeNo, int branchNo, int year, int Academicyear, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                                            
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_YEAR", year),  
                    new SqlParameter("@P_ACADEMIC_YEAR_ID", Academicyear),
                    new SqlParameter("@P_FAC_ADVISOR", uano)
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_EXCEL_RCPIT_FORMAT_2_MM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController_MM.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //public DataSet GetFeeDetails_Fees_Report(int sem, string rectype, string pay_type)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {                                     
        //            new SqlParameter("@P_SEMESTERNO", sem),
        //            new SqlParameter("@P_RECIEPT_TYPE", rectype),
        //            new SqlParameter("@P_PAY_TYPE_CODE", pay_type),
                  
        //        };
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_SHOW_SEMESTERWISE_FEE_DETAILS", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOSDataUpToSem_FutureSem() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        public DataSet GetFeeDetails_Fees_Report_UpTpSem(DateTime from_dt, DateTime to_date, string rectype, string ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                     
                    new SqlParameter("@P_REC_FROM_DT", from_dt),
                    new SqlParameter("@P_REC_TO_DATE", to_date),
                    new SqlParameter("@P_RECIEPT_TYPE", rectype),
                     new SqlParameter("@P_PAY_TYPE_CODE", ptype),
                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SHOW_DATA_UPTO_SEM_FUTURE_SEM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOSDataUpToSem_FutureSem() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

     
        public DataSet GetReceiptTypeforFeeReport()
            {
            DataSet ds = null;
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FEE_RECEIPT_TYPE_FOR_REPORT", sqlParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaymentModificationReportExcel() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
    
    }
}
