//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : FEE COLLECTION CONTROLLER CLASS
// CREATION DATE : 21-MAY-2009                                                        
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
    public class FeeCollectionController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet ShowStudentsForReconcile(int admbatch, int Degree, string appliid) ////int Paymenttype,//Commented by Irfan shaikh on 20190122
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMBATCH", admbatch),                
                    // new SqlParameter("@P_PAYMENT_TYPE", Paymenttype), /////Commented by Irfan shaikh on 20190122
                    new SqlParameter("@P_DEGREENO", Degree),              ///Added by Irfan shaikh on 20190122
                    new SqlParameter("@P_APPLIID", appliid)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_FOR_RECONCILATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ShowStudentsForReconcile() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Vinay Mishra on 22/06/2023 for Manual Registration Fees Entry
        public int InsertManualRegistrationFeeEntry(int userNo, string userName, string studName, int degdeptNo, string regFees, int flag)
        {
            int status = 0;

            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO", userNo),
                    new SqlParameter("@P_USERNAME", userName),
                    new SqlParameter("@P_STUDNAME", studName),
                    new SqlParameter("@P_DEGREE_DEPT_NO", degdeptNo),
                    new SqlParameter("@P_TOTAL_AMT", regFees),
                    new SqlParameter("@P_FLAG", flag)
                };

                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REGISTRATION_FEES_MANUAL_ENTRY", sqlParams, true);
                if (ret != null)
                {
                    status = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return status;
        }

        //Added by Sandeep on 06-02-2018
        public int InsertOnlinePayment_Exam_REgistration(int idno, int sessionno, int SEMESTERNO, string ORDER_ID, int PAYMENTTYPE, string REGUGLAR_COURSE_AMOUNT, string BACKLOG_COURSE_AMOUNT, string SUPPLY_COURSE_AMOUNT, string MAKEUP_COURSE_AMOUNT, string LATE_FEES_AMOUNT, string Reval_amount, string Recheck_amount, string TOTAL_AMOUNT, string COM_CODE1)//int dmno, , string AMOUNT,Int64 customerRef, string APTRANSACTIONID)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            //new SqlParameter("@P_DM_NO", dmno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            //  new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),
                            // new SqlParameter("@P_AMOUNT", AMOUNT),
                            new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE),
                            new SqlParameter("@P_REGUGLAR_COURSE_AMOUNT", REGUGLAR_COURSE_AMOUNT),
                            new SqlParameter("@P_BACKLOG_COURSE_AMOUNT", BACKLOG_COURSE_AMOUNT),
                            new SqlParameter("@P_SUPPLY_COURSE_AMOUNT", SUPPLY_COURSE_AMOUNT),
                            new SqlParameter("@P_MAKEUP_COURSE_AMOUNT", MAKEUP_COURSE_AMOUNT),
                            new SqlParameter("@P_LATE_FEES_AMOUNT", LATE_FEES_AMOUNT),
                            new SqlParameter("@P_REVAL_AMOUNT", Reval_amount),
                            new SqlParameter("@P_RECHECK_AMOUNT", Recheck_amount),
                            new SqlParameter("@P_TOTAL_AMOUNT", TOTAL_AMOUNT),
                            new SqlParameter("@P_COM_CODE", COM_CODE1),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PR_INS_ONLINE_PAYMENT_FOR_EXAM_REGISTRATION", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
            }
            return retStatus;
        }
        //end


        public int Insert_Payment_StudentApplicationForm(int idno, int sessionno, int SEMESTERNO, string ORDER_ID, int PAYMENTTYPE, int APNO, string REGUGLAR_COURSE_AMOUNT,
           string BACKLOG_COURSE_AMOUNT, string SUPPLY_COURSE_AMOUNT, string MAKEUP_COURSE_AMOUNT, string PaperSeeingFee, string ReadressalFee, string NameCorrectionFee,
           string DuplicategradeCardFee, string OfficailTranscriptFee, string ConsolidatedgcswFee, string ProvisionalDegreeFee, string LATE_FEES_AMOUNT, string amount,
           string COM_CODE1)//int dmno, , string AMOUNT,Int64 customerRef, string APTRANSACTIONID)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),
                            new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE),
                            new SqlParameter("@P_APNO", APNO),
                            new SqlParameter("@P_COM_CODE", COM_CODE1),
                            new SqlParameter("@P_REGUGLAR_COURSE_AMOUNT", REGUGLAR_COURSE_AMOUNT),
                            new SqlParameter("@P_BACKLOG_COURSE_AMOUNT", BACKLOG_COURSE_AMOUNT),
                            new SqlParameter("@P_SUPPLY_COURSE_AMOUNT", SUPPLY_COURSE_AMOUNT),
                            new SqlParameter("@P_MAKEUP_COURSE_AMOUNT", MAKEUP_COURSE_AMOUNT),
                            new SqlParameter("@P_PAPER_SEEING_AMOUNT", PaperSeeingFee),
                            new SqlParameter("@P_READRESSAL_AMOUNT", ReadressalFee),                  
                            new SqlParameter("@P_NAME_CORRECTION_AMOUNT", NameCorrectionFee),
                            new SqlParameter("@P_DUPLICATEGRADECARD_AMOUNT", DuplicategradeCardFee),
                            new SqlParameter("@P_OFFICIALTRANSCRIPT_AMOUNT", OfficailTranscriptFee),
                            new SqlParameter("@P_CONSOLIDATEDGRADECSW_AMOUNT", ConsolidatedgcswFee),
                            new SqlParameter("@P_PROVISIONALDEGREE_AMOUNT", ProvisionalDegreeFee),
                            new SqlParameter("@P_LATE_FEES_AMOUNT", LATE_FEES_AMOUNT),
                            new SqlParameter("@P_TOTAL_AMOUNT", amount),
                          
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PR_INS_ONLINE_PAYMENT_FOR_STUDENT_APPLICATION", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
            }
            return retStatus;
        }
        public int UpdateFessCollectionMaster(MiscFees misc)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", misc.Id);
                objParams[1] = new SqlParameter("@P_AMOUNT", misc.Amount);

                // if (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BANKDETAILS", objParams, false) != null)
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ONLINE_MISC_FEES_COLLECTION_MASTER", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.UpdateFessCollectionMaster-> " + ex.ToString());
            }
            return retStatus;
        }
        public int ReconcileStudentFees(int admbatch, string userids, string remarks)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Reconcile student fees
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_USERID", userids);
                objParams[2] = new SqlParameter("@P_REMARKS", remarks);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_RECON_STUDENTS_FEES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.ReconcileStudentFees()-> " + ex.ToString());
            }
            return retStatus;
        }

        #region Commented on 20190123 by Irfan Shaikh
        //public DataSet ShowStudentsByExcel(int admbatch, int paymenttype, string appliid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {
        //            new SqlParameter("@P_ADMBATCH", admbatch),                
        //            new SqlParameter("@P_PAYMENT_TYPE",paymenttype),
        //            new SqlParameter("@P_APPLIID", appliid)
        //        };

        //        ds = objDataAccess.ExecuteDataSetSP("PKG_REPORT_RECONCILE_STUDENT_LIST", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ShowStudentsForReconcile() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        #endregion Commented on 20190123 by Irfan Shaikh

        public DataSet ShowStudentsByExcel(int admbatch, int degree, string appliid) //// Added on 20190123 by Irfan Shaikh
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMBATCH", admbatch),                
                    new SqlParameter("@P_DEGREENO",degree),
                    new SqlParameter("@P_APPLIID", appliid)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_REPORT_RECONCILE_STUDENT_LIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ShowStudentsForReconcile() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeCollectionModes()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_FEECOLLECT_OPTIONS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeCollectionModes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet SearchStudents(string searchStr, string searchBy, string orderBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SEARCHSTRING", searchStr),                
                    new SqlParameter("@P_SEARCHBYFIELD", searchBy),
                    new SqlParameter("@P_ORDERBYFIELD", orderBy)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_STUDENT_AUTOSEARCH", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudents(StudentSearch studSearch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_NAME", studSearch.StudentName),
                    new SqlParameter("@P_ENROLLNO", studSearch.EnrollmentNo),
                    new SqlParameter("@P_SEMESTERNO", studSearch.SemesterNo),
                    new SqlParameter("@P_YEARNO", studSearch.YearNo),
                    new SqlParameter("@P_BRANCHNO", studSearch.BranchNo),
                    new SqlParameter("@P_DEGREENO", studSearch.DegreeNo),
                    new SqlParameter("@P_IDNO", studSearch.IdNo),
                     new SqlParameter("@P_srno", studSearch.Srno),

                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_SEARCH_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //public int GetStudentIdByEnrollmentNo(string enrollNo)
        //{
        //    int studentId = 0;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_ENROLLNO", enrollNo),
        //            new SqlParameter("@P_IDNO", studentId)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        object studId = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_GET_STUDENTID_BY_ENROLLNO", sqlParams, true);
        //        studentId = ((studId != null && studId.ToString() != string.Empty) ? Int32.Parse(studId.ToString()) : 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentIdByEnrollmentNo() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return studentId;
        //}

        public int GetStudentIdByEnrollmentNo(string enrollNo)
        {
            int studentId = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ENROLLNO", enrollNo),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object studId = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_GET_STUDENTID_BY_ENROLLNO", sqlParams, true);
                studentId = ((studId != null && studId.ToString() != string.Empty) ? Int32.Parse(studId.ToString()) : 0);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentIdByEnrollmentNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return studentId;
        }


        //public DataSet GetStudentInfoById(int studentId)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        { 
        //            new SqlParameter("@P_IDNO", studentId)
        //        };
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetPaidReceiptsInfoByStudId(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetStudentInfoByIdRefund(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_REFUND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoByIdRefund() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetFeeItems_Data(int sessionNo, int studentId, int semesterNo, string receiptType, int examtype, int currency, int payTypeNo, ref int status, DateTime TransDate)
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
                    new SqlParameter("@P_EXAMTYPE", examtype),
                    new SqlParameter("@P_CURRENCY",currency),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_TRANS_DATE",TransDate), // ADDED BY SHAILENDRA K. ON DATED 29.04.2023 AS PER DR. MANOJ SIR SUGGESTION CALCULATING LATE FINE ON TRANS DATE.
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT", sqlParams);
                //ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_SRK_29042023", sqlParams); // ADDED BY SHAILENDRA K. ON DATED 29.04.2023 AS PER DR. MANOJ SIR SUGGESTION CALCULATING LATE FINE ON TRANS DATE.
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Mode of receipt     Corresponding field name in counter_ref
        ///      C                   REC1
        ///      B                   REC2
        ///      A                   REC3
        ///      S                   REC4
        /// </summary>
        /// <param name="modeOfReceipt">Mode of recieving the payment.</param>
        /// <param name="userNo">Current User Id</param>
        /// <returns></returns>
        //public DataSet GetNewReceiptData(string modeOfReceipt, int userNo, string receipt_code)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {

        //            new SqlParameter("@P_MODE_OF_RECEIPT", modeOfReceipt),
        //            new SqlParameter("@P_RECEIPT_CODE", receipt_code),
        //            new SqlParameter("@P_USER_NO", userNo)
        //        };
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_NEW_RECEIPT_DATA", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetNewReceiptData() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet GetNewReceiptData(string modeOfReceipt, int userNo, string receipt_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {

                    new SqlParameter("@P_MODE_OF_RECEIPT", modeOfReceipt),
                    new SqlParameter("@P_RECEIPT_CODE", receipt_code),
                    new SqlParameter("@P_USER_NO", userNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_NEW_RECEIPT_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetNewReceiptData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        ////public bool SaveFeeCollection_Transaction(ref DailyCollectionRegister dcr)
        ////{
        ////    try
        ////    {
        ////        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        ////        SqlParameter[] sqlParams = new SqlParameter[] 
        ////        {                    
        ////            new SqlParameter("@P_IDNO", dcr.StudentId),
        ////            new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
        ////            new SqlParameter("@P_NAME", dcr.StudentName),
        ////            new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
        ////            new SqlParameter("@P_BRANCH", dcr.BranchName),
        ////            new SqlParameter("@P_YEAR", dcr.YearNo),
        ////            new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
        ////            new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
        ////            new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
        ////            new SqlParameter("@P_CURRENCY",dcr.Currency),
        ////            new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
        ////            new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
        ////            new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
        ////            new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
        ////            new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
        ////            new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
        ////            new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
        ////            new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
        ////            new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
        ////            new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
        ////            new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
        ////            new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
        ////            new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
        ////            new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
        ////            new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
        ////            new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
        ////            new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
        ////            new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
        ////            new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
        ////            new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
        ////            new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
        ////            new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
        ////            new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
        ////            new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
        ////            new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
        ////            new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
        ////            new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
        ////            new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
        ////            new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
        ////            new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
        ////            new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
        ////            new SqlParameter("@P_DD_AMT", dcr.DemandDraftAmount),
        ////            new SqlParameter("@P_CASH_AMT", dcr.CashAmount),
        ////            new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
        ////            new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
        ////            new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
        ////            new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
        ////            new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
        ////            new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
        ////            new SqlParameter("@P_FEE_CAT_NO", dcr.FeeCatNo),
        ////            new SqlParameter("@P_CAN", dcr.IsCancelled),
        ////            new SqlParameter("@P_DELET", dcr.IsDeleted),
        ////            new SqlParameter("@P_CHALAN_DATE", (dcr.ChallanDate != DateTime.MinValue)? dcr.ChallanDate as object : DBNull.Value as object),
        ////            new SqlParameter("@P_RECON", dcr.IsReconciled),
        ////            new SqlParameter("@P_COM_CODE", dcr.CompanyCode),
        ////            new SqlParameter("@P_RPENTRY", dcr.RpEntry),
        ////            new SqlParameter("@P_UA_NO", dcr.UserNo),
        ////            new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
        ////            new SqlParameter("@P_REMARK", dcr.Remark),
        ////            new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
        ////            //ADD 11/4/2012
        ////            new SqlParameter("@P_EXCESS_AMOUNT",dcr.ExcessAmount),
        ////            new SqlParameter("@P_EXCESS_BALANCE",dcr.ExcessAmount),
        ////            new SqlParameter("@P_DCRNO", dcr.DcrNo)
        ////        };
        ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        ////        dcr.DcrNo = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR", sqlParams, true);

        ////        // if payment type is demand draft(D) then save demand draft details also.
        ////        if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0)
        ////        {
        ////            foreach (DemandDrafts dd in dcr.PaidDemandDrafts)
        ////            {
        ////                sqlParams = new SqlParameter[] 
        ////                { 
        ////                    new SqlParameter("@P_DCRNO", dcr.DcrNo),
        ////                    new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
        ////                    new SqlParameter("@P_IDNO", dcr.StudentId),
        ////                    new SqlParameter("@P_BANKNO", dd.BankNo),
        ////                    new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
        ////                    new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
        ////                    new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
        ////                    new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
        ////                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode)
        ////                };
        ////                objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN", sqlParams, false);
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return true;
        ////}


        /// <summary>
        /// Updated by Rita on date 13/06/2019.....
        /// </summary>
        /// <param name="dcr"></param>
        /// <returns></returns>
        public bool SaveFeeCollection_Transaction(ref DailyCollectionRegister dcr, int checkAllow, string ipaddress, int OrgID, DateTime Transdate)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_YEAR", dcr.YearNo),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                    new SqlParameter("@P_DD_AMT", dcr.DemandDraftAmount),
                    new SqlParameter("@P_CASH_AMT", dcr.CashAmount),
                    new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
                    new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
                    new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
                    new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
                    new SqlParameter("@P_FEE_CAT_NO", dcr.FeeCatNo),
                    new SqlParameter("@P_CAN", dcr.IsCancelled),
                    new SqlParameter("@P_DELET", dcr.IsDeleted),
                    new SqlParameter("@P_CHALAN_DATE", (dcr.ChallanDate != DateTime.MinValue)? dcr.ChallanDate as object : DBNull.Value as object),
                    new SqlParameter("@P_RECON", dcr.IsReconciled),
                    new SqlParameter("@P_COM_CODE", dcr.CompanyCode),
                    new SqlParameter("@P_RPENTRY", dcr.RpEntry),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    //ADD 11/4/2012
                    new SqlParameter("@P_EXCESS_AMOUNT",dcr.ExcessAmount),
                    new SqlParameter("@P_EXCESS_BALANCE",dcr.ExcessAmount),
                    new SqlParameter("@P_LATE_FEE", dcr.Late_fee), //Added on 25/07/2017 for late fee ********************
                    new SqlParameter("@P_CREDIT_DEBITNO", dcr.CreditDebitNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_TRANS_REFFNO", dcr.TransReffNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_ISPAYTM", dcr.IsPaytm),//Added on 13/06/2019 by Rita...
                    new SqlParameter("@P_ALLOW_DEPOSIT", checkAllow),//Added on 31/07/2019 by Sunita
                    new SqlParameter("@P_IPADDRESS",ipaddress),//Added on 12/03/2020 by Dileep
                    new SqlParameter("@P_BANKID", dcr.BankId),
                    new SqlParameter("@P_INSTALLMENTFLAG", dcr.InstallmentFlag), //Added on 20/10/2022 by SwapnilP
                    new SqlParameter("@P_INSTALLMENTNO", dcr.InstallmentNo), //Added on 20/10/2022 by SwapnilP
                    new SqlParameter("@P_ORGID", OrgID),
                    new SqlParameter("@P_TRANSDATE", Transdate), // Added on 16_12_2022 by Rohit  
                    new SqlParameter("@P_DCRNO", dcr.DcrNo), 
                
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                dcr.DcrNo = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR", sqlParams, true);

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)

                if (dcr.PaymentType == "D" && dcr.DcrNo > 0)
                {
                    int status = 0;
                    //  int output=0;
                    int output1 = 0;
                    foreach (DemandDrafts dd in dcr.PaidDemandDrafts)
                    {
                        sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_DCRNO", dcr.DcrNo),
                            new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
                            new SqlParameter("@P_IDNO", dcr.StudentId),
                            new SqlParameter("@P_BANKNO", dd.BankNo),
                            new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
                            new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
                            new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
                            new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
                            new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        object output = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN", sqlParams, true);
                        output1 = Convert.ToInt32(output);
                        if (output1 == 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public DataSet GetOnlinePaymentConfigurationDetails_WithDegree_DAIICT(int Organizationid, int payid, int activityno, int degreeno, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_ACTIVITYNO", activityno),
                    new SqlParameter("@P_PAYID",payid),
                    new SqlParameter("@P_DEGREENO",degreeno),        //Added by Nikhil L. on 23082022 to add degreeno paramter.
                    new SqlParameter("@P_COLLEGE_ID",college_id)     //Added by Swapnil P. on 14092022 to add college_id paramter.
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_CONFIG_DETAILS_WITH_DEGREE_DAIICT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails_WithDegree() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public bool CreateNewDemand(FeeDemand feeDemand, int paymentTypeNoOld)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", feeDemand.EnrollmentNo),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    new SqlParameter("@P_DM_NO", feeDemand.DemandId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                feeDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATEDEMAND_SINGLE", sqlParams, true);
                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public int AddStudentExamFeesDeatails(ExamFeesStudent objExamFee)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO",objExamFee.IDNO),
                    new SqlParameter("@P_REGNO",objExamFee.REGNO),
                    new SqlParameter("@P_SESSIONNO",objExamFee.SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO",objExamFee.SEMESTERNO),
                    new SqlParameter("@P_SCHEMENO",objExamFee.SCHEMENO),
                    new SqlParameter("@P_CATEGORYNO",objExamFee.CATEGORYNO),
                    new SqlParameter("@P_EXAM_PTYPE",objExamFee.EXAM_PTYPE),
                    new SqlParameter("@P_EXAMTYPE",objExamFee.EXAMTYPE),
                    new SqlParameter("@P_COURSENO",objExamFee.COURSENO),
                    new SqlParameter("@P_CCODE",objExamFee.CCODE),
                    new SqlParameter("@P_SUBID",objExamFee.SUBID),
                    new SqlParameter("@P_S1FEES",objExamFee.S1FEES),
                    new SqlParameter("@P_S2FEES",objExamFee.S2FEES),
                    new SqlParameter("@P_S3FEES",objExamFee.S3FEES),
                    new SqlParameter("@P_S4FEES",objExamFee.S4FEES),
                    new SqlParameter("@P_S5FEES",objExamFee.S5FEES),
                    new SqlParameter("@P_S6FEES",objExamFee.S6FEES),
                    new SqlParameter("@P_S7FEES",objExamFee.S7FEES),
                    new SqlParameter("@P_S8FEES",objExamFee.S8FEES),
                    new SqlParameter("@P_S9FEES",objExamFee.S9FEES),
                    new SqlParameter("@P_S10FEES",objExamFee.S10FEES),
                    new SqlParameter("@P_FEESDATE",objExamFee.FEESDATE),
                    new SqlParameter("@P_IPADDRESS",objExamFee.IPADDRESS),
                    new SqlParameter("@P_COLLEGE_CODE",objExamFee.COLLEGE_CODE)
                };

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_STUDENT_EXAMFEES_INSERT", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.AddStudentExamFeesDeatails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int DeleteStudentExamFeesDeatails(ExamFeesStudent objExamFee)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add Fail Subject Details
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", objExamFee.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objExamFee.SEMESTERNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objExamFee.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objExamFee.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENO", objExamFee.COURSENO);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAMFEE_STUDENT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.DeleteStudentExamFeesDeatails-> " + ex.ToString());
            }

            return retStatus;

        }

        public int DeleteExamRegistationDemand(FeeDemand objFeeCollect)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add Fail Subject Details
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", objFeeCollect.StudentId);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objFeeCollect.SemesterNo);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objFeeCollect.SessionNo);
                objParams[3] = new SqlParameter("@P_RECIEPT_CODE", objFeeCollect.ReceiptTypeCode);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAMREGISTATION_DEMAND", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.DeleteExamRegistationDemand-> " + ex.ToString());
            }

            return retStatus;

        }

        public DataSet GetCurrentSemPaidFeeInfo(int studentId, int semesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_PREREGIST_SP_PAID_FEE_INFO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetCurrentSemPaidFeeInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet FindReceipts(string fieldName, string searchText)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_FIELDNAME", fieldName),
                    new SqlParameter("@P_SEARCHTEXT", searchText)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FIND_RECEIPTS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetReceiptInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool UpdateFeesCriteria(int paymentTypeNo, int scholarshipNo, int studentId)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_PAYMENT_TYPE_NO", paymentTypeNo),
                    new SqlParameter("@P_SCHOLARSHIP_NO", scholarshipNo),
                    new SqlParameter("@P_IDNO", studentId)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_UPDATE_FEES_CRITERIA", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateFeesCriteria() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool CancelReceipt(DailyCollectionRegister dcr)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_DCR_NO", dcr.DcrNo),
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_USER_ID", dcr.UserNo),
                    new SqlParameter("@P_REMARK", dcr.Remark)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CANCEL_RECEIPT", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateFeesCriteria() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool LockCollections(DateTime fromDate, DateTime toDate)
        {
            bool ret = false;
            try
            {
                int output = 0;
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_FROM_DATE", fromDate),
                    new SqlParameter("@P_TO_DATE", toDate),
                    new SqlParameter("@P_OUTPUT", output)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_LOCK_DCR", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.LockCollections() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public DataSet GetPaidReceiptsInfoByStudIdForExam(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA_FOR_EXAM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public CustomStatus CreateExamDemand(FeeDemand feeDemand, int reg_ex)
        {
            CustomStatus cs = CustomStatus.Error;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_REG_EX", reg_ex),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATEDEMAND_EXAM", sqlParams, true);

                return (CustomStatus)ret;


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateExamDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public int UpdateExamFeeAmount(FeeDemand feeDemand, int theoryAmt, int PracticalAmt, int OralAmt, int ProjectAmt)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_F4",theoryAmt),
                    new SqlParameter("@P_F5",PracticalAmt ),
                    new SqlParameter("@P_F6",OralAmt),
                    new SqlParameter("@P_F7",ProjectAmt),
                    new SqlParameter("@P_OUT",0)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_UDATE_EXAMFEE_AMOUNT", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public string UpdateDDInfo(int DCRNO, string DDNO, DateTime DDDATE, string DDBANK, string DDCITY, int BANKNO, int IDNO)
        {
            //CustomStatus cs = CustomStatus.Error;
            string retStatus = string.Empty;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_DCRNO", DCRNO),
                            new SqlParameter("@P_DD_NO", DDNO),
                            new SqlParameter("@P_DD_DT ", DDDATE),
                            new SqlParameter("@P_DD_BANK ", DDBANK),
                            new SqlParameter("@P_DD_CITY", DDCITY),
                            new SqlParameter("@P_BANKNO", BANKNO),
                            new SqlParameter("@P_IDNO", IDNO)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.BigInt;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEE_UPDATE_DDINFO", sqlParams, true);

                //if (Convert.ToInt32(ret) == -99)
                //    cs = CustomStatus.TransactionFailed;
                //else
                //    cs = CustomStatus.RecordSaved;
                //cs = ret.ToString();
                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.PreAdmissionRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public bool UpdateExcessStatus(string dcrno, string chk, double excessamount)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_DCR_NO", dcrno),
                    new SqlParameter("@P_CHK",chk),
                    new SqlParameter("@P_EXCESS_AMOUNT",excessamount),
                   
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_UPDATE_EXCESS_STATUS", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateExcessStatus() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }


        public bool ReconcileData(int idno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                };
                objDataAccess.ExecuteNonQuerySP("PKG_COURSE_REGISTERED_RECONCILE_CHALAN", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ReconcileData() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool ReconcileDataRemark(int idno, string remark, int sessionno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_SESSIONNO",sessionno)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_COURSE_REGISTERED_RECONCILE_CHALAN_REMARK", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ReconcileData() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }


        public DataSet GetPaidReceiptsExamInfoByStudId(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA_FOR_EXAM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public bool ReconcileDataForPro(int idno, int sessionno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                };
                objDataAccess.ExecuteNonQuerySP("PKG_COURSE_REGISTERED_RECONCILE_FOR_PRO", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ReconcileData() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }


        //for no due fee collection insert

        public int AddNoDueFeeAmount(string regno, int branch, int degree, int idno, string studname, int session, decimal fee, int uatype, int ua_no, string uaname, int status, int semesterno, string receiptcode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add New Course
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_REGNO", regno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_IDNO", idno);
                objParams[4] = new SqlParameter("@P_NAME", studname);
                objParams[5] = new SqlParameter("@P_SESSIONNO", session);
                objParams[6] = new SqlParameter("@P_FEEHEAD", fee);
                objParams[7] = new SqlParameter("@P_UATYPE", uatype);
                objParams[8] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[9] = new SqlParameter("@P_UA_NAME", uaname);
                objParams[10] = new SqlParameter("@P_STATUS", status);
                objParams[11] = new SqlParameter("@P_SEMESTER", semesterno);
                objParams[12] = new SqlParameter("@P_RECIEPT_CODE", receiptcode);
                //new fields
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_NODUEFEES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
            }

            return retStatus;
        }


        public bool UpdateReconcileDate(int idno, DateTime cashdate)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new  SqlParameter("@P_CASHDATE", cashdate),
                };
                objDataAccess.ExecuteNonQuerySP("PKG_COURSE_REGISTERED_RECONCILE_CASH_DATE", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateReconcileDate() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        //THIS METHOD IS USED TO INSERT MISCELLANEOUS FEES INTO THE DATABASE USEING PROC
        public int AddMiscFeesDesc(int idno, string name, int cbook, DateTime recdt, string counter, string remark, string chdd, string chddno, double chddamt, DateTime chdddt, int bankco, string bname, string bloc, string userid, string ipadd, string collcode, string paytype, double cgst, double sgst, int session, string trasreff, int rbd, int orgid) //  
        {
            int retstatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparam = null;
                {
                    objparam = new SqlParameter[24];
                    objparam[0] = new SqlParameter("@P_STUDSRNO", idno);
                    objparam[1] = new SqlParameter("@P_NAME", name);
                    objparam[2] = new SqlParameter("@P_CBOOKSRNO", cbook);
                    objparam[3] = new SqlParameter("@P_MISCRECPTDATE", recdt);
                    objparam[4] = new SqlParameter("@P_COUNTR", counter);
                    objparam[5] = new SqlParameter("@P_REMARK", remark);
                    objparam[6] = new SqlParameter("@P_CHDD", chdd);
                    objparam[7] = new SqlParameter("@P_CHDDNO", chddno);
                    objparam[8] = new SqlParameter("@P_CHDDAMT", chddamt);
                    if (chdddt == DateTime.MinValue)
                        objparam[9] = new SqlParameter("@P_CHDDDT", DBNull.Value);
                    else
                        objparam[9] = new SqlParameter("@P_CHDDDT", chdddt);
                    objparam[10] = new SqlParameter("@P_BANKCO", bankco);
                    objparam[11] = new SqlParameter("@P_BNAME", bname);
                    objparam[12] = new SqlParameter("@P_BLOC", bloc);
                    objparam[13] = new SqlParameter("@P_USERID", userid);
                    objparam[14] = new SqlParameter("@P_IPADDRESS", ipadd);
                    objparam[15] = new SqlParameter("@P_COLLEGE_CODE", collcode);
                    objparam[16] = new SqlParameter("@P_PAY_TYPE", paytype);
                    objparam[17] = new SqlParameter("@P_CGST", cgst);
                    objparam[18] = new SqlParameter("@P_SGST", sgst);
                    objparam[19] = new SqlParameter("@P_SESSIONNO", session);
                    objparam[20] = new SqlParameter("@P_TRANS_REFFNO", trasreff);
                    objparam[21] = new SqlParameter("@P_STATUS", rbd);
                    objparam[22] = new SqlParameter("@P_ORGID", orgid);
                    objparam[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objparam[23].Direction = ParameterDirection.Output;
                    object ret = objhelper.ExecuteNonQuerySP("PKG_MISCELLANEOUS_FEES_DETAIL_INSERT", objparam, true);
                    retstatus = Convert.ToInt32(ret);

                }
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FEECOLLECTION.MISC_DESCRIPTION-> " + ex.ToString());
            }

            return retstatus;

        }

        public int AddMiscFee(MiscFees misc)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparams = null;

                //new SqlParameter("@P_MISCHEADSRNO",misc.Mischeadno),
                objparams = new SqlParameter[8];
                objparams[0] = new SqlParameter("@P_MISCHEADCODE", misc.Misccode);
                objparams[1] = new SqlParameter("@P_MISCHEAD", misc.Mischead);
                objparams[2] = new SqlParameter("@P_CBOOKSRNO", misc.MiscCasHbook);
                objparams[3] = new SqlParameter("@P_USERID", misc.Userid);
                objparams[4] = new SqlParameter("@P_IPADDRESS", misc.Ipaddress);
                //objparams[5] = new SqlParameter("@P_AUDITDATE",misc.Auditdate);
                objparams[5] = new SqlParameter("@P_COLLEGE_CODE", misc.CollegeCode);
                objparams[6] = new SqlParameter("@P_MISCHEADSRNO", misc.MiscSrno);
                objparams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objparams[7].Direction = ParameterDirection.Output;
                object ret = objsqlhelper.ExecuteNonQuerySP("PKG_MISCHEAD_INSERT_UPDATE", objparams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddMiscTransDesc(int miscdcrno, string mischeadcode, string mischead, double miscamt, string userid, string ipaddress, string collegecode, int orgid)
        {
            int retstatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparam = null;
                {
                    objparam = new SqlParameter[9];
                    objparam[0] = new SqlParameter("@P_MISCDCRSRNO", miscdcrno);
                    //objparam[1] = new SqlParameter("@P_MISCHEADSRNO", mischeadsrno);
                    objparam[1] = new SqlParameter("@P_MISCHEADCODE", mischeadcode);
                    objparam[2] = new SqlParameter("@P_MISCHEAD", mischead);
                    objparam[3] = new SqlParameter("@P_MISCAMT", miscamt);
                    objparam[4] = new SqlParameter("@P_USERID", userid);
                    objparam[5] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    objparam[6] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                    objparam[7] = new SqlParameter("@P_ORGID", orgid);
                    objparam[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objparam[8].Direction = ParameterDirection.Output;

                    object ret = objsqlhelper.ExecuteNonQuerySP("PKG_MISC_TRANS_INSERT", objparam, true);
                    if (Convert.ToInt32(ret) == -99)
                        retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    else
                        retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
            }

            return retstatus;

        }


        public int updateMiscdcr(int countr)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[2];
                    sqlparam[0] = new SqlParameter("@P_COUNTR", countr);
                    sqlparam[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    sqlparam[1].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_MISCELLANEOUS_CANCEL_RECEIPT", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentIdByEnrollmentNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        //Method for getting student details for Online payment--student login


        public DataSet GetStudentInfoById_ForOnlinePayment(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_ONLINE_PAYMENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetFeeItems_Data_ForOnlinePayment(int sessionNo, int studentId, int semesterNo, string receiptType, int payTypeNo, int ADMBATCH)//,ref int status)
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
                    new SqlParameter("@P_ADMBATCH",ADMBATCH)
                   // new SqlParameter("@P_OUT", status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEEITEMS_AMOUNT_ONLINEPAYMENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /*****************FOR ONLINE PAYMENT STUDENT LOGIN************************/
        public int InsertOnlinePayment_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string Regno, string msg)
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
                    sqlparam[9] = new SqlParameter("@P_ORGID", 0);
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int BulkInsertOnlinePayment_DCR(string Order_ID, int IDNO, string IsValid, string Receipt_Code, string TransactionDate, string TransactionID, string PayStatus, string Message, string TranDateTime)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[9];
                    sqlparam[0] = new SqlParameter("@P_ORDER_ID", Order_ID);
                    sqlparam[1] = new SqlParameter("@P_IDNO", IDNO);
                    sqlparam[2] = new SqlParameter("@P_ISVALID", IsValid);
                    sqlparam[3] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
                    sqlparam[4] = new SqlParameter("@P_TRANSACTION_DATE", TransactionDate);
                    sqlparam[5] = new SqlParameter("@P_TRANSID", TransactionID);
                    sqlparam[6] = new SqlParameter("@P_PAY_STATUS", PayStatus);
                    sqlparam[7] = new SqlParameter("@P_MESSAGE", Message);
                    sqlparam[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[8].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[8].Direction.ToString();
                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_AUTO_UPDATED_TRANSACTION", sqlparam, true);
                return retStatus1 = Convert.ToInt32(studid);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SubmitData() --> " + ex.Message + " " + ex.StackTrace);
            }
        }


        //public int OnlinePayment_updatePAyment(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string MESSAGE, string ap_SecureHash, string REC_TYPE)//, int ORDER_ID)
        public int OnlinePayment_updatePAyment(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string REC_TYPE, string RS)//, int ORDER_ID)s
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[7];
                    sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);
                    sqlparam[1] = new SqlParameter("@P_TRANSACTIONID", TRANSACTIONID);
                    sqlparam[2] = new SqlParameter("@P_AMOUNT", AMOUNT);
                    sqlparam[3] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);
                    //sqlparam[4] = new SqlParameter("@P_MESSAGE", MESSAGE);
                    //sqlparam[5] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);
                    sqlparam[4] = new SqlParameter("@P_REC_TYPE", REC_TYPE);
                    sqlparam[5] = new SqlParameter("@P_RS", RS);
                    sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[6].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PR_UPD_ONLINE_PAYMENT_TRANSACTION_DCR", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        /******************************FOR ONLINE PAYMENT-EXAM REGISTRATION STUDENT LOGIN[10-10-2016]**********************************************************/
        //public int InsertOnlinePayment_Exam_REgistration(int idno, int sessionno, int SEMESTERNO, string ORDER_ID, int PAYMENTTYPE, string REGUGLAR_COURSE_AMOUNT, string BACKLOG_COURSE_AMOUNT, string LATE_FEES_AMOUNT, string TOTAL_AMOUNT)//int dmno, , string AMOUNT,Int64 customerRef, string APTRANSACTIONID)
        //{
        //    int retStatus = 0;
        //    try
        //    {
        //        SQLHelper objSqlHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] param = new SqlParameter[]
        //                {                         
        //                    //new SqlParameter("@P_DM_NO", dmno),
        //                    new SqlParameter("@P_IDNO", idno),
        //                    new SqlParameter("@P_SESSIONNO", sessionno),
        //                    //  new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID),
        //                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
        //                    new SqlParameter("@P_ORDER_ID", ORDER_ID),
        //                    // new SqlParameter("@P_AMOUNT", AMOUNT),
        //                    new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE),
        //                    new SqlParameter("@P_REGUGLAR_COURSE_AMOUNT", REGUGLAR_COURSE_AMOUNT),
        //                    new SqlParameter("@P_BACKLOG_COURSE_AMOUNT", BACKLOG_COURSE_AMOUNT),
        //                    new SqlParameter("@P_LATE_FEES_AMOUNT", LATE_FEES_AMOUNT),

        //                    new SqlParameter("@P_TOTAL_AMOUNT", TOTAL_AMOUNT),

        //                    new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
        //                };
        //        param[param.Length - 1].Direction = ParameterDirection.Output;
        //        object ret = objSqlHelper.ExecuteNonQuerySP("PR_INS_ONLINE_PAYMENT_FOR_EXAM_REGISTRATION", param, true);

        //        if (ret != null && ret.ToString() != "-99")
        //            retStatus = Convert.ToInt32(ret);
        //        else
        //            retStatus = -99;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
        //    }
        //    return retStatus;
        //}

        //Added on[17-05-2017]
        public DataSet GetPaidReceiptsInfoByStudId_FORPAYMENT(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GETS_PAID_FEES_DETAILS_FOR_PAYMENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId_FORPAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int GetFeeItems_Data_ISEXISTS(int sessionNo, int studentId, int semesterNo, string receiptType, int payTypeNo, string PAYTYPE, int ADMBATCH)//,ref int status), int status
        {
            int countrno = 0;
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
                    new SqlParameter("@P_PAYTYPE",PAYTYPE),
                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object studid = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_IS_EXISTS", sqlParams, true);
                if (studid != null && studid.ToString() != "-99")
                {
                    //countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                    //if (!(studid is DBNull))
                    //{
                    countrno = Convert.ToInt32(studid);

                    //}
                }
                else
                    countrno = -99;
                //countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);

            }
            return countrno;
        }

        // Added on 24/05/2017
        public DataSet Get_ACPC_STUDENT_FOR_DCR_ENTRY(int degreeno, int branchno, int admbatch, int sem, string RecieptCode, int categoryno, string collcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_ADMBATCH", admbatch),
                    new SqlParameter("@P_SEMESTERNO", sem),
                    new SqlParameter("@P_RECIEPT_CODE", RecieptCode),
                    new SqlParameter("@P_CATEGORYNO", categoryno),
                    new SqlParameter("@P_COLLEGECODE", collcode),
                };
                ds = objDataAccess.ExecuteDataSetSP("GET_STUDENT_FOR_ACPC", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_ACPC_STUDENT_FOR_DCR_ENTRY() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool CreateNewDemandmis(FeeDemand feeDemand, int paymentTypeNoOld, ref DailyCollectionRegister dcr)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", feeDemand.EnrollmentNo),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    
                   
                     new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                     new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                     new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                     new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                     new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                     new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                     new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                     new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                     new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                     new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                     new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                     new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                     new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                     new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                     new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                     new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                     new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                     new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                     new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                     new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                     new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                     new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                     new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                     new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                     new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                     new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                     new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                     new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                     new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                     new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                     new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                     new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                     new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                     new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                     new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                     new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                     new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                     new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                     new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                     new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                     new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                      new SqlParameter("@P_DM_NO", feeDemand.DemandId),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATEDEMAND_SINGLE_MIS", sqlParams, true);
                feeDemand.DemandId = 0;

                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public DataSet Get_COLLEGE_PAYMENTDATA(int collegeid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_COLLEGEID", collegeid),
                };
                ds = objDataAccess.ExecuteDataSetSP("GET_COLLEGE_PAYMENTDATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_COLLEGE_PAYMENTDATA() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //This method is added by Akhileh Kumar to Insert ACPC Student Record 
        public int InsertACPCStudentsDetailsIntoDCR(ACPC acp)
        {
            int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", acp.SessionN0);
                objParams[1] = new SqlParameter("@P_IDNO", acp.IdNo);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", acp.SemesterNo);
                objParams[3] = new SqlParameter("@P_DEGREENO", acp.DegreeNo);
                objParams[4] = new SqlParameter("@P_RECIEPT_CODE", acp.RecieptCode);
                objParams[5] = new SqlParameter("@P_RECON", acp.IsReconciled);
                objParams[6] = new SqlParameter("@P_DM_NO", acp.DemandNo);
                objParams[7] = new SqlParameter("@P_BRANCH", acp.Branch);
                objParams[8] = new SqlParameter("@P_REMARK", acp.Remark);
                objParams[9] = new SqlParameter("@P_AMOUNT", acp.Amount);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_ACPC", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FeeCollectionController.InsertACPCStudentsDetailsIntoDCR -> " + ex.ToString());
            }
            return retStatus;
        }


        // Added on 10/08/2017 for export the student in excel
        public DataSet Get_STUDENT_FOR_FEE_PAYMENT(int sessionNo, string receiptCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_SESSION_RECEIPT_WISE_FEES_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GET_FEESDETAILS_IDNOWISE(int IDNO, int userType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_USERTYPE", userType),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_FEESDETAILS_IDNOWISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GET_FEESDETAILS_IDNOWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GET_STUDENT_CONDONANCE(int IDNO, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", sessionno),
                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_CIE_CONDONANCE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GET_FEESDETAILS_IDNOWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 06-04-2019 | FOR DUES NO DUES FEE INSERT
        /// </summary>
        public int NoDuesInsert(int idno, int sessionno, int colg, int degreeno, int branchno, int schemeno, int semesterno, int stud_type, decimal amount, string remarks, int UA_NO, int UA_TYPE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COLLEGE_ID", colg),
                            new SqlParameter("@P_DEGREENO",degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_STUD_TYPE",stud_type),   
                            new SqlParameter("@P_AMOUNT", amount),  
                            new SqlParameter("@P_REMARKS", remarks), 
                            new SqlParameter("@P_UA_NO", UA_NO), 
                            new SqlParameter("@P_UA_TYPE", UA_TYPE),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_DUES_NO_DUES_FEES", objParams, true); //Added by roshan 28-12-2016

                if (ret != null && ret.ToString() == "1")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret != null && ret.ToString() == "2")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.NoDuesInsert --> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 06-04-2019 | FOR DUES NO DUES FEE INSERT
        /// </summary>
        public int NoDuesCollectionInsert(int idno, int sessionno, int colg, int degreeno, int branchno, int schemeno, int semesterno, decimal acct_amount,
                                            string acct_remarks, int acct_UA_NO, int UA_TYPE, decimal total_amount, string reciept_code, int ptype, string colgcode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COLLEGE_ID", colg),
                            new SqlParameter("@P_DEGREENO",degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            //new SqlParameter("@P_STUD_TYPE",stud_type),   
                            new SqlParameter("@P_ACCT_AMOUNT", acct_amount),  
                            new SqlParameter("@P_TOT_AMOUNT", total_amount),
                            new SqlParameter("@P_ACCT_REMARKS", acct_remarks), 
                            new SqlParameter("@P_ACCT_UA_NO", acct_UA_NO), 
                            new SqlParameter("@P_UA_TYPE", UA_TYPE),
                            new SqlParameter("@P_RECIEPT_TYPE_CODE", reciept_code),
                            new SqlParameter("@P_PTYPE", ptype),
                            new SqlParameter("@P_COLLEGE_CODE", colgcode),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_NO_DUES_COLLECTION_FEES", objParams, true); //Added by roshan 28-12-2016

                if (ret != null && ret.ToString() == "1")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret != null && ret.ToString() == "2")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.NoDuesInsert --> " + ex.ToString());
            }
            return retStatus;
        }


        /////// <summary>
        /////// ADDED BY: M. REHBAR SHEIKH ON 25-04-2019 | 
        /////// </summary>
        ////public bool CreateDemandForExamination(FeeDemand feeDemand, int paymentTypeNoOld, string Enroll, decimal TotalFees)
        ////{
        ////    try
        ////    {
        ////        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        ////        SqlParameter[] sqlParams = new SqlParameter[] 
        ////        {
        ////            new SqlParameter("@P_IDNO", feeDemand.StudentId),
        ////            new SqlParameter("@P_NAME", feeDemand.StudentName),
        ////            new SqlParameter("@P_ENROLLNMENTNO", Enroll),
        ////            new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
        ////            new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
        ////            new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
        ////            new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
        ////            new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
        ////            new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
        ////            new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
        ////            new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
        ////            new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
        ////            new SqlParameter("@P_UA_NO", feeDemand.UserNo),
        ////            new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
        ////            new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
        ////            new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
        ////            new SqlParameter("@P_TOTALAMOUNT", TotalFees),
        ////            new SqlParameter("@P_DM_NO", feeDemand.DemandId)
        ////        };
        ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        ////        feeDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_EXAM_FEE_COLLECT_CREATE_DEMAND_FOR_SINGLE_STUDENT", sqlParams, true);
        ////        if (feeDemand.DemandId == -99)
        ////            return false;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return true;
        ////}
        /// <summary>
        /// Added by S.Patil -24082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <param name="degreno"></param>
        /// <param name="branchno"></param>
        /// <param name="semesterno"></param>
        /// <param name="collegeid"></param>
        /// <returns></returns>

        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_BRANCH_DEGREE_WISE(int sessionNo, string receiptCode, int degreno, int branchno, int semesterno, int collegeid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                     new SqlParameter("@P_DEGREENO", degreno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                     new SqlParameter("@P_SEMESTERNO", semesterno),
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEES_REPORT_BRANCH_WISE1_EXCELREPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by S.Patil -24082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <param name="semesterno"></param>
        /// <returns></returns>

        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_COLLECTION_DD_WISE(int sessionNo, string receiptCode, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                   new SqlParameter("@P_SEMESTERNO", semesterno),
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_FEES_COLLECT_DD_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by S.Patil -24082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <param name="semesterno"></param>
        /// <returns></returns>

        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_COLLECTION_CASH_WISE(int sessionNo, string receiptCode, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                   new SqlParameter("@P_SEMESTERNO", semesterno),
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_FEES_COLLECT_CASH1_FOR_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by S.Patil -24082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <returns></returns>

        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS(int sessionNo, string receiptCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_SESSION_RECEIPT_WISE_FEES_REPORT_EXCEL_20180117", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by S.Patil -24082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <returns></returns>
        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE(string receiptCode, int semesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_SESSION_RECEIPT_WISE_DEMAND_FEES_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeno, int branchno, string paymode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_PAY_MODE", paymode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_SESSION_RECEIPT_WISE_DEMAND_FEES_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet Get_FEE_PAYMENT_WITH_STUDENT_WISE(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo, int year, int admstatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_ADM_STATUS ", admstatus),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",academicyearid),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }




        public DataSet Get_FEE_PAYMENT_WITH_DCR(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int DegreeNo, int Branchno, int yearid, int AcademicYearID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                     //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROMDATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_CODE", receiptCode),
                    new SqlParameter("@P_DEGREENO",DegreeNo),
                    new SqlParameter("@P_BRANCHNO",Branchno),
                    new SqlParameter("@P_YEAR",yearid),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",AcademicYearID),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_BRANCH_TOTAL_STUD_COUNT_N_AMT_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet Get_FEE_PAYMENT_WITH_STUDENT_WISE(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by Rita Munde -23092019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <returns></returns>
        public DataSet GET_STUDENT_FOR_EXCESS_AMOUNT_WITH_HEADS_DEMANDWISE(string receiptCode, DateTime from_dt, DateTime to_date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                     
                    new SqlParameter("@P_RECIEPT_CODE", receiptCode),
                    new SqlParameter("@P_REC_FROM_DT", from_dt),
                    new SqlParameter("@P_REC_TO_DATE", to_date)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_REPORT_FOR_EXCESS_AMOUNT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 25-04-2019 | 
        /// </summary>
        public bool CreateDemandForExaminationForBacklog(FeeDemand feeDemand, int paymentTypeNoOld, string Enroll, decimal TotalFees)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", Enroll),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    new SqlParameter("@P_TOTALAMOUNT", TotalFees),
                    new SqlParameter("@P_DM_NO", feeDemand.DemandId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                feeDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_EXAM_FEE_COLLECT_CREATE_DEMAND_FOR_SINGLE_STUDENT_BACKLOG", sqlParams, true);
                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        /// <summary>
        /// Added By Dileep K
        /// Date:25-02-2020
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public DataSet GetStudentInfoByIdFor_Scholarship(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_SHCOLARSHIP", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep K
        /// Date:25-02-2020
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="studentId"></param>
        /// <param name="semesterNo"></param>
        /// <param name="receiptType"></param>
        /// <param name="examtype"></param>
        /// <param name="currency"></param>
        /// <param name="payTypeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataSet GetFeeItems_Data_ForScholarship(int sessionNo, int studentId, int semesterNo, string receiptType, int examtype, int currency, int payTypeNo, ref int status)
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
                    new SqlParameter("@P_EXAMTYPE", examtype),
                    new SqlParameter("@P_CURRENCY",currency),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_FOR_SCHOLARSHIP", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added By Dileep K
        /// Date:25-02-2020
        /// </summary>
        /// <param name="dcr"></param>
        /// <param name="checkAllow"></param>
        /// <returns></returns>
        public bool SaveFeeCollection_TransactionScholarship(ref DailyCollectionRegister dcr, int checkAllow, int ScholarshipId)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_YEAR", dcr.YearNo),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                    new SqlParameter("@P_DD_AMT", dcr.DemandDraftAmount),
                    new SqlParameter("@P_CASH_AMT", dcr.CashAmount),
                    new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
                    new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
                    new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
                    new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
                    new SqlParameter("@P_FEE_CAT_NO", dcr.FeeCatNo),
                    new SqlParameter("@P_CAN", dcr.IsCancelled),
                    new SqlParameter("@P_DELET", dcr.IsDeleted),
                    new SqlParameter("@P_CHALAN_DATE", (dcr.ChallanDate != DateTime.MinValue)? dcr.ChallanDate as object : DBNull.Value as object),
                    new SqlParameter("@P_RECON", dcr.IsReconciled),
                    new SqlParameter("@P_COM_CODE", dcr.CompanyCode),
                    new SqlParameter("@P_RPENTRY", dcr.RpEntry),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    //ADD 11/4/2012
                    new SqlParameter("@P_EXCESS_AMOUNT",dcr.ExcessAmount),
                    new SqlParameter("@P_EXCESS_BALANCE",dcr.ExcessAmount),
                    new SqlParameter("@P_LATE_FEE", dcr.Late_fee), //Added on 25/07/2017 for late fee ********************
                    new SqlParameter("@P_CREDIT_DEBITNO", dcr.CreditDebitNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_TRANS_REFFNO", dcr.TransReffNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_ISPAYTM", dcr.IsPaytm),//Added on 13/06/2019 by Rita...
                    new SqlParameter("@P_ALLOW_DEPOSIT", checkAllow),//Added on 31/07/2019 by Sunita
                    //new SqlParameter("@P_DATE",  ),
                    new SqlParameter("@P_SCHOLARSHIP_ID", ScholarshipId),
                    new SqlParameter("@P_ORGANIZATIONID",  Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),                 
                    new SqlParameter("@P_DCRNO", dcr.DcrNo),    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                dcr.DcrNo = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_FOR_SCHOLARSHIP_MUTI_FEE_HEAD", sqlParams, true);

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)

                if (dcr.PaymentType == "D" && dcr.DcrNo > 0)
                {
                    int status = 0;
                    //  int output=0;
                    int output1 = 0;
                    foreach (DemandDrafts dd in dcr.PaidDemandDrafts)
                    {
                        sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_DCRNO", dcr.DcrNo),
                            new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
                            new SqlParameter("@P_IDNO", dcr.StudentId),
                            new SqlParameter("@P_BANKNO", dd.BankNo),
                            new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
                            new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
                            new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
                            new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
                            new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        object output = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN", sqlParams, true);
                        output1 = Convert.ToInt32(output);
                        if (output1 == 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }
        /// <summary>
        /// ADDED  BY DILEEP KARE
        /// DATE:26-02-2020
        /// </summary>
        /// <param name="dcrno"></param>
        /// <param name="chk"></param>
        /// <param name="excessamount"></param>
        /// <returns></returns>
        public bool UpdateShcolorshipStatusForMultipleFeeHeads(string ScholorshipNO, double SchAdjAmt, int uano, string ipaddress)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SCHLSHP_NO", ScholorshipNO),
                    new SqlParameter("@P_SCH_ADJ_AMT",SchAdjAmt),
                    new SqlParameter("@P_ADJ_UANO",uano),
                    new SqlParameter("@P_ADJ_IP_ADDRESS",ipaddress)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_UPDATE_SCHOLORSHIP_STATUS_FOR_MULTIPLE_FEE_HEAD", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateShcolorshipStatus() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ADDED BY: K.Amith ON 19-02-2020 |
        /// </summary>
        public DataSet Get_Student_Scholership_Report(int admBatch, int year)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_ADMBATCH", admBatch),

                    new SqlParameter("@P_ACADEMIC_YEAR_ID", admBatch),
                    new SqlParameter("@P_YEAR",year)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_SCHOLARSHIP_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Scholership_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        /// <summary>
        /// ADDED BY: K.Amith ON 19-02-2020 | 
        /// </summary>
        public DataSet Get_Student_Scholership_Summary_Report(int admBatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_ADMBATCH", admBatch)                    
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_SCHOLARSHIP_SUMMARY_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Scholership_Summary_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare
        /// date:22-02-2020
        /// </summary>
        /// <param name="admBatch"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <param name="semesterno"></param>
        /// <returns></returns>

        public DataSet Get_Scholership_Adjusted_Amount_Excel_Report(int admBatch, int degreeno, int branchno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_ADMBATCH", admBatch), 
                     new SqlParameter("@P_DEGREENO", degreeno),
                      new SqlParameter("@P_BRANCHNO", branchno),
                       new SqlParameter("@P_SEMESTERNO", semesterno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_SCHOLARSHIP_ADJUSTED_AMOUNT_FOR_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Scholership_Summary_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //New Methods By Amit k on 19-02-2020

        public DataSet GetStudentDetails_For_Scholarship(int idno, int sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                //objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_FOR_SCHOLARSHIP_ALLOTMENT_BY_REGNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        //New Methods By Amit k on 19-02-2020
        public int GetTotalSemesterCount(int idno)
        {
            int TotalSemCount = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                TotalSemCount = (Int32)objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GET_SEMESTER_COUNT", objParams, true);
                return TotalSemCount;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTotalSemesterCount-> " + ex.ToString());
            }
        }

        //New Methods By Amit k on 19-02-2020
        public string InsertStudentScholershipDetails(Student objS)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //Add New student
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objS.IdNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", objS.DegreeNo);
                objParams[2] = new SqlParameter("@P_BRANCHNO", objS.BranchNo);
                objParams[3] = new SqlParameter("@P_IPADDRESS", objS.IPADDRESS);
                objParams[4] = new SqlParameter("@P_UANO", objS.Uano);
                objParams[5] = new SqlParameter("@P_ADMBATCH", objS.AdmBatch);
                objParams[6] = new SqlParameter("@P_SCHOLERSHIPTYPENO", objS.ScholershipTypeNo);
                objParams[7] = new SqlParameter("@P_SEMESTERNO", objS.SemesterNo);
                objParams[8] = new SqlParameter("@P_SEMESTERAMOUNT", objS.Amount);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_STUDENT_SCHOLERSHIP_DETAILS", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertStudentScholershipDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        //New Methods By Amit k on 19-02-2020
        public DataSet GetStudentScholershipDetails(int degreeno, int branchno, int semesterno, int prev_status, int admbatch, int colg)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", colg);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_SCHOLARSHIP_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
            }
            return ds;
        }

        //New Methods By Amit k on 19-02-2020
        public string UpdateBulkStudentScholarshipDetails(Student objStudent)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //Add New student
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                objParams[1] = new SqlParameter("@P_COLLEGID", objStudent.College_ID);
                objParams[2] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                objParams[5] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                objParams[6] = new SqlParameter("@P_ScholershipTypeNo", objStudent.ScholershipTypeNo);
                objParams[7] = new SqlParameter("@P_UANO", objStudent.Uano);
                objParams[8] = new SqlParameter("@P_SEM_AMOUNT", objStudent.Amount);
                objParams[9] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_BULK_STD_SCHOLARSHIP_DETAILS", objParams, true);
                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudent-> " + ex.ToString());
            }

            return retStatus;
        }



        /// <summary>
        /// Added By Dileep kare
        /// Updated by Saurabh dated on 09-05-2022
        /// 22-02-2020
        /// </summary>
        /// <param name="dcr"></param>
        /// <param name="f4"></param>
        /// <returns></returns>
        public int InsertScholarshipAdjustment(ref DailyCollectionRegister dcr, double f4, int bankid, string transactionid, int academicYearID, int ScholarshipId)
        {
            int ret = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_YEAR", dcr.YearNo),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F4", f4),
                    new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
                   // new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
                    new SqlParameter("@P_DATE",dcr.ReceiptDate),
                    new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
                    new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),

                   // new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
                   // new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    //new SqlParameter("@P_BANKNAME",bankname ),

                    new SqlParameter ("@P_BANKID",bankid),
                    new SqlParameter("@P_REMARK",dcr.Remark),
                    new SqlParameter("@P_TRANSACTIONID",transactionid ),
                    new SqlParameter("@P_ACADEMIC_YEAR",academicYearID),
                    new SqlParameter("@P_SCHOLARSHIP_ID",ScholarshipId),
                    new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),                   
                    new SqlParameter("@P_DCRNO", dcr.DcrNo),
                   
                };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_INS_SCHOLARSHIP_ADJUSTMENT", sqlParams, true);

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }


        /// <summary>
        /// ADDED  BY DILEEP KARE
        /// DATE:20-02-2020
        /// </summary>
        /// <param name="dcrno"></param>
        /// <param name="chk"></param>
        /// <param name="excessamount"></param>
        /// <returns></returns>
        public bool UpdateShcolorshipStatus(string ScholorshipNO, double SchAdjAmt, int uano, string ipaddress)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SCHLSHP_NO", ScholorshipNO),
                    new SqlParameter("@P_SCH_ADJ_AMT",SchAdjAmt),
                    new SqlParameter("@P_ADJ_UANO",uano),
                    new SqlParameter("@P_ADJ_IP_ADDRESS",ipaddress)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_UPDATE_SCHOLORSHIP_STATUS", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateExcessStatus() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        //added by AMAN on 15/04/2020
        public DataSet GetStudentInfoByIdNew(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_NEW", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        public DataSet GetFeesDetails(int studentId, int degreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DEGREENO", degreeNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_FEES_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        public DataSet GetPaidReceiptsInfoByStudIdAndReceiptCode(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
            
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_STUDENT_RECIEPT_DETAILS", sqlParams);
                //ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA_RECEIPT_WISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
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
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_STDUENT_FEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
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
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR", param, true);

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


        public int InsertOnlinePaymentlog(string userno, string recipt, string PaymentMode, string Amount, string status, string PayId)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[7];
                    sqlparam[0] = new SqlParameter("@P_IDNO", userno);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[3] = new SqlParameter("@P_AMOUNT", Amount);
                    sqlparam[4] = new SqlParameter("@P_STATUS", status);
                    sqlparam[5] = new SqlParameter("@P_PAYID", PayId);
                    sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[6].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ONLINE_PAYMENT_FOR_LOG_ADMIN", sqlparam, true);
                //if (Convert.ToInt32(studid) == -99)
                //{
                //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //}
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }

        }





        public int InsertAdmissionOnlinePayment_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string Regno)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[9];
                    sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                    sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[4] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[5] = new SqlParameter("@P_CASHBOOK", CashBook);
                    sqlparam[6] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[7] = new SqlParameter("@P_PAY_STATUS", StatusF);

                    sqlparam[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[8].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_ADMISSION_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertAdmissionOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int InsertOnlinePayment_TempDCR_Student(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, DailyCollectionRegister dcr)
        {
            try
            {
                int retStatus1 = 0;
                int retStatus = Convert.ToInt32(CustomStatus.Others);
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                    new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_ORDER_ID", ORDER_ID),               
                    new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                    new SqlParameter("@P_OUT",SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object studid = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_TEMP_ONLINEPAYMENT", sqlParams, true);
                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)

            }

            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }

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

        /// <summary>
        /// Added by S Patil -11082020
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="receiptCode"></param>
        /// <returns></returns>
        public DataSet GetOSDataUpToSem_FutureSem(string receiptCode, DateTime from_dt, DateTime to_date, int os_SemFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                     
                    new SqlParameter("@P_RECIEPT_CODE", receiptCode),
                    new SqlParameter("@P_REC_FROM_DT", from_dt),
                    new SqlParameter("@P_REC_TO_DATE", to_date),
                    new SqlParameter("@P_SEM_FLAG", os_SemFlag),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_OUTSTANDINGFEES_UPTO_SEM_FUTURE_SEM_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOSDataUpToSem_FutureSem() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added By Nikhil Lambe on 13082020 to get students paid list
        public DataSet Get_Paid_Students_List(string Receipt_Code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
                ds = sqlHelper.ExecuteDataSetSP("GET_PIAD_STUDENTS_BY_RECEIPT_CODE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Paid_Students_List --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Nikhil Lambe on 13082020 to get Receipt info
        public DataSet GetReceiptInfo(int IDNO, string Receipt_Code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
                ds = objSQLHelper.ExecuteDataSetSP("ACD_SHOW_RECEIPT_INFO_ON_POPUP", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FeeCollectionController.GetReceiptInfo-> " + ex.ToString());
            }
            return ds;

        }

        /// <summary>
        /// Added By S.Patil - 18082020
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="rectype"></param>
        /// <returns></returns>
        /// <summary>
        /// Added By S.Patil - 18082020
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="rectype"></param>
        /// //Updated by Sakshi M in 17112023 
        public DataSet GetFeeDetails_Fees_Report(int sem, string rectype, string pay_type, int admbatch, int degree, int branch, int year)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                     
                    new SqlParameter("@P_SEMESTERNO", sem),
                    new SqlParameter("@P_RECIEPT_TYPE", rectype),
                    new SqlParameter("@P_PAY_TYPE_CODE", pay_type),
                    new SqlParameter("@P_ACDBATCH", admbatch),
                    new SqlParameter("@P_DEGREENO", degree),
                    new SqlParameter("@P_BRANCHNO", branch),
                    new SqlParameter("@P_YEAR", year),
                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SHOW_SEMESTERWISE_FEE_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOSDataUpToSem_FutureSem() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        /// <summary>
        /// Added By S.Patil - 18082020
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="rectype"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Added By Dileep
        /// 03032020
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public DataSet GetStudentInfoByIdforLedgerReport(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_LEDGER_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// added by Dileep
        /// date 03032020
        /// </summary>
        /// <param name="enrollNo"></param>
        /// <returns></returns>
        public int GetStudentIdByEnrollmentNoforLedgerreport(string enrollNo)
        {
            int studentId = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ENROLLNO", enrollNo),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object studId = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_GET_STUDENTID_BY_ENROLLNO_FOR_LEGERREPORT", sqlParams, true);
                studentId = ((studId != null && studId.ToString() != string.Empty) ? Int32.Parse(studId.ToString()) : 0);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentIdByEnrollmentNoforLedgerreport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return studentId;
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
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_PAID_RECEIPT_DATA_ONLINE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        /// <summary>
        /// Added By Dileep on 05/11/2020
        /// </summary>
        /// <param name="feeDemand"></param>
        /// <param name="paymentTypeNoOld"></param>
        /// <param name="Enroll"></param>
        /// <param name="TotalFees"></param>
        /// <returns></returns>
        public bool CreateDemandForExaminationForFineAllotment(FeeDemand feeDemand, DailyCollectionRegister dcr)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                     new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTALAMOUNT", feeDemand.TotalFeeAmount),
                    new SqlParameter("@P_DM_NO", feeDemand.DemandId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                feeDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_FINE_ALLOTMENT", sqlParams, true);
                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        /// <summary>
        /// Added By Dileep on 05/11/2020
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="studentId"></param>
        /// <param name="semesterNo"></param>
        /// <param name="receiptType"></param>
        /// <param name="examtype"></param>
        /// <param name="currency"></param>
        /// <param name="payTypeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataSet GetFeeItems_DataFineAllotment(int sessionNo, int studentId, int semesterNo, string receiptType, int examtype, int currency, int payTypeNo, ref int status)
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
                    new SqlParameter("@P_EXAMTYPE", examtype),
                    new SqlParameter("@P_CURRENCY",currency),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_FOR_FINE_ALLOTMENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // BY SAFAL ON 03022021

        public bool CreateNewDemandFromBranchChange(FeeDemand feeDemand, int paymentTypeNoOld, ref DailyCollectionRegister dcr)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", feeDemand.EnrollmentNo),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    
                   
                     new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                     new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                     new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                     new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                     new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                     new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                     new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                     new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                     new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                     new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                     new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                     new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                     new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                     new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                     new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                     new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                     new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                     new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                     new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                     new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                     new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                     new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                     new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                     new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                     new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                     new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                     new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                     new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                     new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                     new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                     new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                     new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                     new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                     new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                     new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                     new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                     new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                     new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                     new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                     new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                     new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                     new SqlParameter("@P_COLLEGE_ID",feeDemand.College_ID ),
                      new SqlParameter("@P_DM_NO", feeDemand.DemandId),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                objDataAccess.ExecuteNonQuerySP("PKG_MODIFY_DEMAND_FROM_BRANCH_CHANGE", sqlParams, true);
                feeDemand.DemandId = 0;

                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 25-04-2019 | 
        /// </summary>
        public bool CreateDemandForExamination(FeeDemand feeDemand, int paymentTypeNoOld, string Enroll, decimal TotalFees)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", Enroll),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_PAYTYPENO_OLD", paymentTypeNoOld),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    new SqlParameter("@P_REGULARFEES",feeDemand.RegularFees),
                    new SqlParameter("@P_BACKLOGFEES",feeDemand.BacklogFees),
                    new SqlParameter("@P_TOTALAMOUNT", TotalFees),
                    new SqlParameter("@P_DM_NO", feeDemand.DemandId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                feeDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_EXAM_FEE_COLLECT_CREATE_DEMAND_FOR_SINGLE_STUDENT", sqlParams, true);
                if (feeDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateNewDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public int InsertExamPayment_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string Regno)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[9];
                    sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                    sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[4] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[5] = new SqlParameter("@P_CASHBOOK", CashBook);
                    sqlparam[6] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[7] = new SqlParameter("@P_PAY_STATUS", StatusF);

                    sqlparam[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[8].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_EXAM_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertExamPayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public DataSet GetStudentInstallmentDetails(int studentId, int semesterno, string recieptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_INSTALLMENT_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet GetStudentInstallmentDetailsForFeeCollection(int studentId, int semesterno, string recieptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_INSTALLMENT_DETAILS_FOR_FEE_COLLECTION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet GetStudentInstallmentDetailsSemesterreg(int studentId, int semesterno, string recieptcode, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode),
                    new SqlParameter("@P_SESSIONNO", sessionno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_INSTALLMENT_DETAILS_SEM_REG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertInstallmentOnlinePayment_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, int installno, string msg)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                    sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[4] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[5] = new SqlParameter("@P_INSTALLNO", installno);
                    sqlparam[6] = new SqlParameter("@P_MESSAGE", msg);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertInstallmentOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        //Added by Dileep Kare on 03.08.2021 to get Receipt info
        public DataSet GetReceiptInfoCompleteDetails(int IDNO, string Receipt_Code, int Semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("ACD_SHOW_RECEIPT_INFO_ON_POPUP_COMPLETE_DETAILS", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FeeCollectionController.GetReceiptInfoCompleteDetails-> " + ex.ToString());
            }
            return ds;

        }

        public int InsertAdmissionChallanPayment_DCR(int IdNo, int semesterno, int counterno, int ua_no)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[5];
                    sqlparam[0] = new SqlParameter("@P_IDNO", IdNo);
                    sqlparam[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                    sqlparam[2] = new SqlParameter("@P_COUNTERNO", counterno);
                    sqlparam[3] = new SqlParameter("@P_UA_NO", ua_no);
                    sqlparam[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[4].Direction = ParameterDirection.Output;
                    // string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_ADMISSION_INSERT_CHALLAN_PAYMENT_CHALLANTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertAdmissionChallanPayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int InsertChallanPayment_ChallanDCR(int IDNO, int SEMESTERNO, string RECEIPTCODE, int COLLEGE_CODE)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_CHALLAN_PAYMENT_DCR", param, true);

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

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetUniversityFeeData(string Reciept_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_UNIVERSITY_LEVEL_FEE_COLLECTION_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUniversityFeeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <param name="College_id"></param>
        /// <returns></returns>
        public DataSet GetUniversityCollegeFeeData(string Reciept_code, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                    new SqlParameter("@P_COLLEGE_ID", College_id),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_UNIVERSTIY_LEVEL_COLLEGES_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUniversityCollegeFeeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added By Dileep Kare on 13.08.2021
        /// </summary>
        /// <param name="Admission_Batch"></param>
        /// <returns></returns>
        public DataSet GetAdmissionBatchFeeData(string Reciept_code, int Admission_Batch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                    new SqlParameter("@P_ADMBATCH", Admission_Batch),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_ADMISSION_BATCH_WISE_FEE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetAdmissionBatchFeeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 13.08.2021
        /// </summary>
        /// <param name="Admission_Batch"></param>
        /// <param name="College_id"></param>
        /// <returns></returns>
        public DataSet GetAdmissionBatchCollege(string Reciept_Code, int Admission_Batch, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_Code),
                    new SqlParameter("@P_ADMBATCH", Admission_Batch),
                    new SqlParameter("@P_COLLEGE_ID", College_id),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_ADMISSION_BATCH_COLLEGE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUniversityCollegeFeeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 13.08.2021
        /// </summary>
        /// <param name="Admission_Batch"></param>
        /// <param name="College_id"></param>
        /// <returns></returns>
        public DataSet GetDegreeBranchWiseReport(string Reciept_Code, int Degreeno, int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_Code),
                    new SqlParameter("@P_DEGREENO", Degreeno),
                    new SqlParameter("@P_BRANCHNO", Branchno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_DEGREE_BRANCH_FEECOLLECTION_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetDegreeBranchWiseReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 13.08.2021
        /// </summary>
        /// <param name="Admission_Batch"></param>
        /// <param name="College_id"></param>
        /// <returns></returns>
        public DataSet GetFinancialYearWiseReport(string Receipt_Code, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Receipt_Code),
                    new SqlParameter("@P_COLLEGE_ID", College_id)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_FINACIAL_YEAR_FEECOLLECTION_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFinancialYearWiseReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetFinancialYearCollegeList(string Reciept_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_FINANCIAL_YEAR_FEE_COLLECTION_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUniversityFeeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetStudentLedgerExcelReport(string Reciept_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEDGRE_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentLedgerExcelReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetUniversityLevelExcelReport(string Reciept_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEDGRE_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetUniversityLevelExcelReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetAdmissionBatchExcelReport(string Reciept_Code, int AdmBatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_Code),
                    new SqlParameter("@P_ADMBATCH", AdmBatch),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_ADMISSION_BATCH_WISE_FEES_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetAdmissionBatchExcelReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetDegreeBranchExcelReport(string Reciept_Code, int Degreeno, int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_Code),
                    new SqlParameter("@P_DEGREENO", Degreeno),
                    new SqlParameter("@P_BRANCHNO",Branchno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_DEGREE_BRANCH_WISE_FEES_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetDegreeBranchExcelReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare 13.08.2021
        /// </summary>
        /// <param name="Reciept_code"></param>
        /// <returns></returns>
        public DataSet GetFinancialYearExcelReport(string Reciept_code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RECIEPT_CODE", Reciept_code),
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_FINANCIAL_YEAR_FEES_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFinancialYearExcelReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Nikhil Vinod Lambe on 25/06/2021 to get the NEFT/RTGS students list
        public DataSet Get_NEFT_RTGS_StudentList(int receiptno, int semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_RECEIPT_NO", receiptno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_NEFT_RTGS_STUDENTS_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.Get_NEFT_RTGS_StudentList-> " + ex.ToString());
            }

            return ds;
        }
        //Added by Nikhil Vinod Lambe on 24/06/2021 to add student data for NEFT/RTGS
        public int Update_Recon_status_For_NEFT_RTGS(int idno, int receiptno, int sessionno, int semester, string transactionID)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_RECEIPT_NO", receiptno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[4] = new SqlParameter("@P_TRANSACTION_ID", transactionID);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_RECON_STATUS_FOR_NEFT_RTGS", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.Add_NEFT_RTGS_Payment_Offline --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 25/06/2021 to get the NEFT/RTGS details by Idno 
        public DataSet Get_NEFT_RTGS_StudentListByIdno(int idno, int receiptno, int semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_RECEIPT_NO", receiptno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_NEFT_RTGS_DETAILS_BY_IDNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.Get_NEFT_RTGS_StudentListByIdno-> " + ex.ToString());
            }

            return ds;
        }
        //Added by Nikhil Vinod Lambe on 26/06/2021 to get the NEFT/RTGS students list for excel
        public DataSet Get_NEFT_RTGS_StudentListExcel(int receiptno, int semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_RECEIPT_NO", receiptno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_NEFT_RTGS_STUDENTS_LIST_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.Get_NEFT_RTGS_StudentListExcel-> " + ex.ToString());
            }

            return ds;
        }

        // ADDED METHODS RELATED TO ONLINE FEES PAYMENT BY NARESH BEERLA ON 23112021

        public int InsertPendingAmountInDCR(int idno, int semno, string rcpt_code, double Adjust_Amt, string payment_mode, int OrderID, int sessionno, string recipt, int InstallmentNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparams = new SqlParameter[9];
                // objparams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objparams[0] = new SqlParameter("@P_IDNO", idno);
                objparams[1] = new SqlParameter("@P_SEM", semno);
                objparams[2] = new SqlParameter("@P_RCPT_CODE", rcpt_code);
                objparams[3] = new SqlParameter("@P_ADJUST_AMT", Adjust_Amt);
                objparams[4] = new SqlParameter("@P_payment_mode", payment_mode);
                objparams[5] = new SqlParameter("@P_OrderID", OrderID);
                //add for update dcrno in installment table
                objparams[6] = new SqlParameter("@P_Sessionno", sessionno);
                objparams[7] = new SqlParameter("@P_RECIEPTNO", recipt);
                objparams[8] = new SqlParameter("@P_InstallmentNo", InstallmentNo);

                if (objsqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT__PENDING_AMOUNT_ROW_DCR", objparams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
            }

            return retStatus;
        }

        public int SubmitFeesofStudent(StudentFees objSF, int PAYMENTTYPE, int GATEWAYID, string Recpt_Code, int sem)
        {
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_USERNO", objSF.UserNo);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSF.SessionNo);
                objParams[2] = new SqlParameter("@P_AMOUNT", objSF.Amount);
                objParams[3] = new SqlParameter("@P_TRANSDATE", objSF.TransDate);
                //objParams[4] = new SqlParameter("@P_BANKNO", objSF.Bankno);
                objParams[4] = new SqlParameter("@P_BRANCHNAME", objSF.BranchName);
                //objParams[6] = new SqlParameter("@P_DD_CHEQUENO", objSF.Ddno);
                objParams[5] = new SqlParameter("@P_ORDER_ID", objSF.OrderID);
                //    objParams[8] = new SqlParameter("@P_ReceiptNo", objSF.ReceiptNo);
                //objParams[8] = new SqlParameter("@P_CATAPPLIED", objSF.Catapplied);
                //objParams[9] = new SqlParameter("@P_UNATINO", objSF.Unitno);
                objParams[6] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
                objParams[7] = new SqlParameter("@P_GATEWAYID", GATEWAYID);
                objParams[8] = new SqlParameter("@P_RCPT_CODE", Recpt_Code);
                objParams[9] = new SqlParameter("@P_SEMSTERNO", sem);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_FEES_LOG_STU", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public DataSet GetFeesPaymentData(int idno, string receiptType, int session)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_idno", idno);
                sqlParams[1] = new SqlParameter("@P_receiptType", receiptType);
                sqlParams[2] = new SqlParameter("@P_session", session);

                ds = objDataAccess.ExecuteDataSetSP("PKG_Fees_Installment_Details", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.GetDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertAmountInDCR_forBankChallan(int idno, int semno, string rcpt_code, double Adjust_Amt, string payment_mode, int OrderID, int sessionno, string recipt, int InstallmentNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparams = new SqlParameter[10];
                // objparams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objparams[0] = new SqlParameter("@P_IDNO", idno);
                objparams[1] = new SqlParameter("@P_SEM", semno);
                objparams[2] = new SqlParameter("@P_RCPT_CODE", rcpt_code);
                objparams[3] = new SqlParameter("@P_ADJUST_AMT", Adjust_Amt);
                objparams[4] = new SqlParameter("@P_payment_mode", payment_mode);
                objparams[5] = new SqlParameter("@P_OrderID", OrderID);
                //add for update dcrno in installment table
                objparams[6] = new SqlParameter("@P_Sessionno", sessionno);
                objparams[7] = new SqlParameter("@P_RECIEPTNO", recipt);
                objparams[8] = new SqlParameter("@P_InstallmentNo", InstallmentNo);

                objparams[9] = new SqlParameter("@P_tempdcr", SqlDbType.Int);
                objparams[9].Direction = ParameterDirection.Output;

                object ret = objsqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT__PENDING_AMOUNT_ROW_DCR_BankChallan", objparams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(ret);// (CustomStatus.RecordSaved);
                ////////////////////////
                //COMET BY PANKAJ 21032020  if (objsqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT__PENDING_AMOUNT_ROW_DCR_BankChallan", objparams, false) != null)
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Added by Swapnil For get PG ConfigurationDetails
        /// </summary>
        /// <param name="Organizationid"></param>
        /// <param name="payid"></param>
        /// <returns></returns>
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }
        public int OnlineInstallmentFeesPayment(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string MESSAGE, string ap_SecureHash, string remark, string gatewaytxnid, string EXTRA_CHARGE, string recieptcode)//, int ORDER_ID), string REC_TYPE
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[11];
                    sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);  // -- bank id 
                    sqlparam[1] = new SqlParameter("@P_TRANSACTIONID", TRANSACTIONID);  // -- orderid
                    sqlparam[2] = new SqlParameter("@P_AMOUNT", Convert.ToDecimal(AMOUNT));                //-- amt 
                    sqlparam[3] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail
                    sqlparam[4] = new SqlParameter("@P_MESSAGE", MESSAGE);    // message 
                    sqlparam[5] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);  // dont knw
                    sqlparam[6] = new SqlParameter("@P_REMARK", remark);    // remark 
                    sqlparam[7] = new SqlParameter("@P_GATEWAYTXNID", gatewaytxnid); // bank id 
                    sqlparam[8] = new SqlParameter("@P_EXTRA_CHARGE", EXTRA_CHARGE);
                    sqlparam[9] = new SqlParameter("@P_RECIEPT_TYPE", recieptcode);
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_UPD_ONLINE_PAYMENT_DETAILS_FOR_ONLINE_FEES_PAYMENT", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }
        public DataSet GetStudentInfoByIdRefund(int studentId, int Organizationid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_ORGANIZATIONID",Organizationid)//added by dileep kare on 31.12.2021
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO_REFUND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoByIdRefund() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added by Rishabh B. to save Payment Category Configuration - 24/02/2022
        /// </summary>
        public int SavePaymentCategoryConfig(string ReceiptCode, int Paytype, string Fee)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[5];

                sqlParams[0] = new SqlParameter("@P_RECEIPT_TYPE", ReceiptCode);
                sqlParams[1] = new SqlParameter("@P_PAYMENT_TYPE", Paytype);
                sqlParams[2] = new SqlParameter("@P_FEE", Fee);
                sqlParams[3] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                sqlParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_ACD_PAYMENT_CAT_CONFIGURATION", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.SavePaymentCategoryConfig() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Added by Rishabh B. to Get Payment Category Configuration data - 24/02/2022
        /// </summary>
        public DataSet GetPaymentCategoryConfigData(int PCCNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PCCNO ", PCCNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PAYMENTCAT_CONFIGURATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetPaymentCategoryConfigData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentArrears_Excel_Report(DateTime Fromdate, DateTime Todate, int Semesterno, int Degreeno, int Branchno, string Reciept_code, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_FROM_DATE", Fromdate);
                objParams[1] = new SqlParameter("@P_TODATE", Todate);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", Reciept_code);
                objParams[6] = new SqlParameter("@P_ACADEMIC_YEAR_ID", academicyearid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_ARREARS_AMOUNTS_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetStudentArrears_Excel_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetStudentInfoForOfflineFeeCollect(string username)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNAME", username);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_DETAILS_FOR_OFFLINE_FEE_COLLECT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetPaymentCategoryConfigData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        /// <summary>
        /// Added by Nikhil L. on 30/05/2022 to update the recon status in DCR ONLINE for online admission.
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="payMode"></param>
        /// <returns></returns>
        public int Update_Recon_Status_OA(int userNo, string payMode, string payType, DateTime dd_Date, string dd_City, decimal dd_Amount, int dd_BankNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@P_USERNO", userNo);
                sqlParams[1] = new SqlParameter("@P_PAY_MODE", payMode);
                //sqlParams[2] = new SqlParameter("@P_FEE", Fee);
                sqlParams[2] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                sqlParams[3] = new SqlParameter("@P_PAY_TYPE", payType);
                sqlParams[4] = new SqlParameter("@P_DD_DATE", dd_Date);
                sqlParams[5] = new SqlParameter("@P_DD_CITY", dd_City);
                sqlParams[6] = new SqlParameter("@P_DD_AMOUNT", dd_Amount);
                sqlParams[7] = new SqlParameter("@P_DD_BANK_NO", dd_BankNo);
                sqlParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_OA_UPDATE_RECON_FEE_DETAILS", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.Update_Recon_Status_OA() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Added By Rishabh on 30052022 for Excel Data
        /// upadated  by Saurabh S.  @P_ACADEMIC_YEAR_ID
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public DataSet GetStudentLedgerReportData(DateTime fromdate, DateTime todate, int degreeno, int branchno, int year, string recpttyp, int admstatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[1] = new SqlParameter("@P_TODATE", todate);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_YEAR", year);
                objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", recpttyp);
                objParams[6] = new SqlParameter("@P_ADM_STATUS", admstatus);
                objParams[7] = new SqlParameter("@P_ACADEMIC_YEAR_ID", academicyearid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_LEDGER_REPORT_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetStudentLedgerReportData-> " + ex.ToString());
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

        public int AddExamDetailsFeeLog(StudentFees objSF, int PAYMENTTYPE, int GATEWAYID, string Recpt_Code, int revalapplytype) // revalapplytype is not using 
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_USERNO", objSF.UserNo);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSF.SessionNo);
                objParams[2] = new SqlParameter("@P_AMOUNT", objSF.Amount);
                //objParams[3] = new SqlParameter("@P_TRANSDATE", objSF.TransDate);
                //objParams[4] = new SqlParameter("@P_BRANCHNAME", objSF.BranchName);
                objParams[3] = new SqlParameter("@P_ORDER_ID", objSF.OrderID);
                objParams[4] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
                objParams[5] = new SqlParameter("@P_GATEWAYID", GATEWAYID);
                objParams[6] = new SqlParameter("@P_RCPT_CODE", Recpt_Code);
                objParams[7] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_EXAM_DETAILS_FEES_LOG_STUDENT_IDNO_WISE", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public int CreateUser(string UA_NAME, string UA_PWD, string UA_FULLNAME, string UA_EMAIL, string UA_ACC, int IDNO)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
                objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);
                objParams[2] = new SqlParameter("@P_UA_FULLNAME", UA_FULLNAME);
                objParams[3] = new SqlParameter("@P_UA_EMAIL", UA_EMAIL);
                objParams[4] = new SqlParameter("@P_UA_ACC", UA_ACC);
                objParams[5] = new SqlParameter("@P_IDNO", IDNO);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_USER_CREATION_OA", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 1234)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added By Dileep
        /// 03032020
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="semesterno"></param>
        /// <returns></returns>
        public DataSet GetSemesterwiseFeeDetails(int idno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] SqlParams = new SqlParameter[]
            {
                new  SqlParameter("@P_IDNO",idno),
                new SqlParameter("@P_SEMESTERNO",semesterno)
               
            };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_PREV_SEMESTER_FEE_DETAILS", SqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionCOntroller.GetConsolidateOutstandingReportDetails()-->" + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //=======================================================================
        public bool SaveFeeCollection_Transaction_RCPIT(ref DailyCollectionRegister dcr, int checkAllow, string ipaddress, int OrgID)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_YEAR", dcr.YearNo),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                    new SqlParameter("@P_DD_AMT", dcr.DemandDraftAmount),
                    new SqlParameter("@P_CASH_AMT", dcr.CashAmount),
                    new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
                    new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
                    new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
                    new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
                    new SqlParameter("@P_FEE_CAT_NO", dcr.FeeCatNo),
                    new SqlParameter("@P_CAN", dcr.IsCancelled),
                    new SqlParameter("@P_DELET", dcr.IsDeleted),
                    new SqlParameter("@P_CHALAN_DATE", (dcr.ChallanDate != DateTime.MinValue)? dcr.ChallanDate as object : DBNull.Value as object),
                    new SqlParameter("@P_RECON", dcr.IsReconciled),
                    new SqlParameter("@P_COM_CODE", dcr.CompanyCode),
                    new SqlParameter("@P_RPENTRY", dcr.RpEntry),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    //ADD 11/4/2012
                    new SqlParameter("@P_EXCESS_AMOUNT",dcr.ExcessAmount),
                    new SqlParameter("@P_EXCESS_BALANCE",dcr.ExcessAmount),
                    new SqlParameter("@P_LATE_FEE", dcr.Late_fee), //Added on 25/07/2017 for late fee ********************
                    new SqlParameter("@P_CREDIT_DEBITNO", dcr.CreditDebitNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_TRANS_REFFNO", dcr.TransReffNo),//Added on 13/05/2019 by Rita...
                    new SqlParameter("@P_ISPAYTM", dcr.IsPaytm),//Added on 13/06/2019 by Rita...
                    new SqlParameter("@P_ALLOW_DEPOSIT", checkAllow),//Added on 31/07/2019 by Sunita
                    new SqlParameter("@P_IPADDRESS",ipaddress),//Added on 12/03/2020 by Dileep
                    new SqlParameter("@P_BANKID", dcr.BankId),
                    new SqlParameter("@P_INSTALLMENTFLAG", dcr.InstallmentFlag), //Added on 20/10/2022 by SwapnilP
                    new SqlParameter("@P_INSTALLMENTNO", dcr.InstallmentNo), //Added on 20/10/2022 by SwapnilP
                    new SqlParameter("@P_ORGID", OrgID),
                    new SqlParameter("@P_DCRNO", dcr.DcrNo), 
                
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                dcr.DcrNo = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_RCPIT", sqlParams, true);

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)

                if (dcr.PaymentType == "D" && dcr.DcrNo > 0)
                {
                    int status = 0;
                    //  int output=0;
                    int output1 = 0;
                    foreach (DemandDrafts dd in dcr.PaidDemandDrafts)
                    {
                        sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_DCRNO", dcr.DcrNo),
                            new SqlParameter("@P_DD_NO", dd.DemandDraftNo),
                            new SqlParameter("@P_IDNO", dcr.StudentId),
                            new SqlParameter("@P_BANKNO", dd.BankNo),
                            new SqlParameter("@P_DD_DT", dd.DemandDraftDate),
                            new SqlParameter("@P_DD_BANK", dd.DemandDraftBank),
                            new SqlParameter("@P_DD_CITY", dd.DemandDraftCity),
                            new SqlParameter("@P_DD_AMOUNT", dd.DemandDraftAmount),
                            new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        object output = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCRTRAN_RCPIT", sqlParams, true);
                        output1 = Convert.ToInt32(output);
                        if (output1 == 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public DataSet GetPreviousReceiptsData(int studentId, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                     new SqlParameter("@P_SEMESTERNO", semesterno),
                    
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_PREV_SEMESTER_FEE_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY RISHABH ON 21/06/2022 TO CREATE DEMAND FOR REDO COURSE REGISTRATION
        /// </summary>
        public int CreateDemandForRedoCourseReg(FeeDemand feeDemand, string Enroll, decimal TotalFees)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", feeDemand.StudentId),
                    new SqlParameter("@P_NAME", feeDemand.StudentName),
                    new SqlParameter("@P_ENROLLNMENTNO", Enroll),
                    new SqlParameter("@P_SESSIONNO", feeDemand.SessionNo),
                    new SqlParameter("@P_BRANCHNO", feeDemand.BranchNo),
                    new SqlParameter("@P_DEGREENO", feeDemand.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", feeDemand.SemesterNo),
                    new SqlParameter("@P_BATCHNO", feeDemand.AdmBatchNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeDemand.ReceiptTypeCode),
                    new SqlParameter("@P_PAYTYPENO_NEW", feeDemand.PaymentTypeNo),
                    new SqlParameter("@P_COUNTER_NO", feeDemand.CounterNo),
                    new SqlParameter("@P_UA_NO", feeDemand.UserNo),
                    new SqlParameter("@P_PARTICULAR", feeDemand.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", feeDemand.CollegeCode),
                    new SqlParameter("@P_EXAMTYPE", feeDemand.ExamType),
                    new SqlParameter("@P_TOTALAMOUNT", TotalFees),
                    new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_DM_NO", feeDemand.DemandId)
                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACD_CREATE_DEMAND_FOR_REDO_COURSE_REG", sqlParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateDemandForRedoCourseReg() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public bool UpdateReconcileDateUnder(int idno, DateTime cashdate, string Remark, int sessionno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new  SqlParameter("@P_CASHDATE", cashdate),
                    new  SqlParameter("@P_REMARK", Remark),
                    new  SqlParameter("@P_SESSIONNO", sessionno),
                };
                objDataAccess.ExecuteNonQuerySP("PKG_COURSE_REGISTERED_RECONCILE_CASH_DATE_UNDER", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateReconcileDate() --> " + ex.Message + " " + ex.StackTrace);
                //return false;
            }
            return true;
        }
        public DataSet GetAllLABELBULK(string ENROLL)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_ENROLLNO", ENROLL);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GETLABELDATA_BULKBILLDESK_DETAILS", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateDemandForRedoCourseReg() --> " + ex.Message + " " + ex.StackTrace);
            }

            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        public DataSet GetAllBULK(string ENROLL, string receiptCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objparams = new SqlParameter[0];
                objparams = new SqlParameter[2];
                objparams[0] = new SqlParameter("@P_ENROLLNO", ENROLL);
                objparams[1] = new SqlParameter("@P_RECIEPT_CODE", receiptCode);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GETDATA_BULKBILLDESK_DETAILS", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateDemandForRedoCourseReg() --> " + ex.Message + " " + ex.StackTrace);
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        public DataSet GetStudentArrears_Headwise_Report(DateTime Fromdate, DateTime Todate, int Degreeno, int Branchno, int year, string Reciept_code, int admissionStatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_FROMDATE", Fromdate);
                objParams[1] = new SqlParameter("@P_TODATE", Todate);
                objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParams[2] = new SqlParameter("@P_YEAR", year);
                objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", Reciept_code);
                objParams[6] = new SqlParameter("@P_ADM_STATUS", admissionStatus);
                objParams[7] = new SqlParameter("@P_ACADEMIC_YEAR_ID", academicyearid);
                objParams[8] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_ARREARS_AMOUNTS_REPORT_HEADWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetStudentArrears_Excel_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Saurabh S.
        public DataSet Get_CURRENT_STUDENT_DETAILS_AND_BALNCE_REPORT(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo, int year, int admstatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_ADM_STATUS ", admstatus),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",academicyearid),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_CURRENT_STUDENT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Saurabh S.
        public DataSet Get_FEE_PAYMENT_WITH_STUDENT_WISE_AND_FACULTY_DESCRIPTION(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo, int year, int admstatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_ADM_STATUS ", admstatus),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",academicyearid),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_EXCEL_RCPIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet Get_Fee_Details_DCR_Excel_Report_FormatII(int degreeNo, int branchNo, int year, int Academicyear)
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
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_LEGER_FEES_REPORT_EXCEL_RCPIT_FORMAT_2", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 24-08-2022 to get payment config details with degree.
        /// </summary>
        /// <param name="Organizationid"></param>
        /// <param name="payid"></param>
        /// <param name="activityno"></param>
        /// <param name="degreeno"></param>
        /// <returns></returns>
        public DataSet GetOnlinePaymentConfigurationDetails_WithDegree(int Organizationid, int payid, int activityno, int degreeno, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_ACTIVITYNO", activityno),
                    new SqlParameter("@P_PAYID",payid),
                    new SqlParameter("@P_DEGREENO",degreeno),        //Added by Nikhil L. on 23082022 to add degreeno paramter.
                    new SqlParameter("@P_COLLEGE_ID",college_id)     //Added by Swapnil P. on 14092022 to add college_id paramter.
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_CONFIG_DETAILS_WITH_DEGREE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails_WithDegree() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }


        public DataSet GetOnlinePaymentConfigurationAllDetailsV2(int ConfigID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_CONFIGID", ConfigID),
                      
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PG_CONFIG_ALL_DETAILS_V2", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails_WithDegree() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }






        public DataSet GetOnlinePaymentConfigurationDetails_V2(int Organizationid, int Semesterno, string ReceiptType, int Collegeid, int Degreeno, int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_SEMESTERNO", Semesterno),
                    new SqlParameter("@P_RECIEPT_TYPE",ReceiptType),
                    new SqlParameter("@P_COLLEGE_ID",Collegeid),        //Added by Nikhil L. on 23082022 to add degreeno paramter.
                    new SqlParameter("@P_DEGREENO",Degreeno),  
                    new SqlParameter("@P_BRANCHNO",Branchno), //Added by Swapnil P. on 14092022 to add college_id paramter.
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PG_CONFIG_DETAILS_V2", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails_WithDegree() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        #region "Photo Reval Fee Collection"
        public int AddPhotoRevalFeeLog(StudentFees objSF, int PAYMENTTYPE, int GATEWAYID, string Recpt_Code, int revalapplytype)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_USERNO", objSF.UserNo);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSF.SessionNo);
                objParams[2] = new SqlParameter("@P_AMOUNT", objSF.Amount);
                //objParams[3] = new SqlParameter("@P_TRANSDATE", objSF.TransDate);
                //objParams[4] = new SqlParameter("@P_BRANCHNAME", objSF.BranchName);
                objParams[3] = new SqlParameter("@P_ORDER_ID", objSF.OrderID);
                objParams[4] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
                objParams[5] = new SqlParameter("@P_GATEWAYID", GATEWAYID);
                objParams[6] = new SqlParameter("@P_RCPT_CODE", Recpt_Code);
                objParams[7] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_FEES_LOG_STU_PHOTO_REVAL", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public int OnlinePhotoRevalPayment(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string MESSAGE, string ap_SecureHash, string remark, string gatewaytxnid, string EXTRA_CHARGE, int revalapplytype)//, int ORDER_ID), string REC_TYPE
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[11];
                    sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);  // -- bank id 
                    sqlparam[1] = new SqlParameter("@P_TRANSACTIONID", TRANSACTIONID);  // -- orderid
                    sqlparam[2] = new SqlParameter("@P_AMOUNT", AMOUNT);                //-- amt 
                    sqlparam[3] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail
                    sqlparam[4] = new SqlParameter("@P_MESSAGE", MESSAGE);    // message 
                    sqlparam[5] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);  // dont knw
                    sqlparam[6] = new SqlParameter("@P_REMARK", remark);    // remark 
                    sqlparam[7] = new SqlParameter("@P_GATEWAYTXNID", gatewaytxnid); // bank id 
                    sqlparam[8] = new SqlParameter("@P_EXTRA_CHARGE", EXTRA_CHARGE);
                    sqlparam[9] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_UPD_ONLINE_PAYMENT_DETAILS_FOR_PHOTO_REVAL", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        public int AddMiscFeesDesc(int idno, string name, int cbook, string recdt, string counter, string remark, string chdd, string chddno, double chddamt, DateTime chdddt, int bankco, string bname, string bloc, string userid, string ipadd, string collcode, string paytype, double cgst, double sgst, int session, string cgstper, string sgstper, string mischeadcode, string mischead, string miscamt) //  
        {
            int retstatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparam = null;
                {
                    objparam = new SqlParameter[26];
                    objparam[0] = new SqlParameter("@P_STUDSRNO", idno);
                    objparam[1] = new SqlParameter("@P_NAME", name);
                    objparam[2] = new SqlParameter("@P_CBOOKSRNO", cbook);
                    objparam[3] = new SqlParameter("@P_MISCRECPTDATE", recdt);
                    objparam[4] = new SqlParameter("@P_COUNTR", counter);
                    objparam[5] = new SqlParameter("@P_REMARK", remark);
                    objparam[6] = new SqlParameter("@P_CHDD", chdd);
                    objparam[7] = new SqlParameter("@P_CHDDNO", chddno);
                    objparam[8] = new SqlParameter("@P_CHDDAMT", chddamt);
                    //if (chdddt == DateTime.MinValue)
                    //    objparam[9] = new SqlParameter("@P_CHDDDT", DBNull.Value);
                    //else
                    objparam[9] = new SqlParameter("@P_CHDDDT", chdddt);
                    objparam[10] = new SqlParameter("@P_BANKCO", bankco);
                    objparam[11] = new SqlParameter("@P_BNAME", bname);
                    objparam[12] = new SqlParameter("@P_BLOC", bloc);
                    objparam[13] = new SqlParameter("@P_USERID", userid);
                    objparam[14] = new SqlParameter("@P_IPADDRESS", ipadd);
                    objparam[15] = new SqlParameter("@P_COLLEGE_CODE", collcode);
                    objparam[16] = new SqlParameter("@P_PAY_TYPE", paytype);
                    objparam[17] = new SqlParameter("@P_CGST", cgst);
                    objparam[18] = new SqlParameter("@P_SGST", sgst);
                    objparam[19] = new SqlParameter("@P_SESSIONNO", session);
                    objparam[20] = new SqlParameter("@P_CGSTPER", cgstper);
                    objparam[21] = new SqlParameter("@P_SGSTPER", sgstper);
                    objparam[22] = new SqlParameter("@P_MISCHEADCODE", mischeadcode);
                    objparam[23] = new SqlParameter("@P_MISCHEAD", mischead);
                    objparam[24] = new SqlParameter("@P_MISCAMT", miscamt);
                    objparam[25] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objparam[25].Direction = ParameterDirection.Output;
                    object ret = objhelper.ExecuteNonQuerySP("PKG_MISCELLANEOUS_FEES_DETAIL_INSERT_SVCE", objparam, true);
                    retstatus = Convert.ToInt32(ret);

                }
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FEECOLLECTION.MISC_DESCRIPTION-> " + ex.ToString());
            }

            return retstatus;

        }
        //THIS METHOD IS USED TO INSERT MISCELLANEOUS FEES INTO THE DATABASE USEING PROC
        public int AddMiscFeesDesc(int idno, string name, int cbook, string recdt, string counter, string remark, string chdd, string chddno, double chddamt, DateTime chdddt, int bankco, string bname, string bloc, string userid, string ipadd, string collcode, string paytype, double cgst, double sgst, int session, string cgstper, string sgstper, string mischeadcode, string mischead, string miscamt, int Head_Count) //  
        {
            int retstatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objhelper = new SQLHelper(_connectionString);
                SqlParameter[] objparam = null;
                {
                    objparam = new SqlParameter[27];
                    objparam[0] = new SqlParameter("@P_STUDSRNO", idno);
                    objparam[1] = new SqlParameter("@P_NAME", name);
                    objparam[2] = new SqlParameter("@P_CBOOKSRNO", cbook);
                    objparam[3] = new SqlParameter("@P_MISCRECPTDATE", recdt);
                    objparam[4] = new SqlParameter("@P_COUNTR", counter);
                    objparam[5] = new SqlParameter("@P_REMARK", remark);
                    objparam[6] = new SqlParameter("@P_CHDD", chdd);
                    objparam[7] = new SqlParameter("@P_CHDDNO", chddno);
                    objparam[8] = new SqlParameter("@P_CHDDAMT", chddamt);
                    //if (chdddt == DateTime.MinValue)
                    //    objparam[9] = new SqlParameter("@P_CHDDDT", DBNull.Value);
                    //else
                    objparam[9] = new SqlParameter("@P_CHDDDT", chdddt);
                    objparam[10] = new SqlParameter("@P_BANKCO", bankco);
                    objparam[11] = new SqlParameter("@P_BNAME", bname);
                    objparam[12] = new SqlParameter("@P_BLOC", bloc);
                    objparam[13] = new SqlParameter("@P_USERID", userid);
                    objparam[14] = new SqlParameter("@P_IPADDRESS", ipadd);
                    objparam[15] = new SqlParameter("@P_COLLEGE_CODE", collcode);
                    objparam[16] = new SqlParameter("@P_PAY_TYPE", paytype);
                    objparam[17] = new SqlParameter("@P_CGST", cgst);
                    objparam[18] = new SqlParameter("@P_SGST", sgst);
                    objparam[19] = new SqlParameter("@P_SESSIONNO", session);
                    objparam[20] = new SqlParameter("@P_CGSTPER", cgstper);
                    objparam[21] = new SqlParameter("@P_SGSTPER", sgstper);
                    objparam[22] = new SqlParameter("@P_MISCHEADCODE", mischeadcode);
                    objparam[23] = new SqlParameter("@P_MISCHEAD", mischead);
                    objparam[24] = new SqlParameter("@P_MISCAMT", miscamt);
                    objparam[25] = new SqlParameter("@P_HEAD_COUNT", Head_Count);
                    objparam[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objparam[26].Direction = ParameterDirection.Output;
                    object ret = objhelper.ExecuteNonQuerySP("PKG_MISCELLANEOUS_FEES_DETAIL_INSERT_SVCE", objparam, true);
                    retstatus = Convert.ToInt32(ret);

                }
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FEECOLLECTION.MISC_DESCRIPTION-> " + ex.ToString());
            }
            return retstatus;
        }
        //START FOR DEGREE&BRANCH_EXCEL REPORT

        public DataSet GET_REPORT_EXCEL(int sessionno, string branchno, int semesterno, string rectype, string degreeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHlper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_RECIEPT_TYPE", rectype);
                objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                //objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSqlHlper.ExecuteDataSetSP("PKG_FEES_REPORT_BRANCH_WISE1_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GET_REPORT_EXCEL-> " + ex.ToString());
            }
            return ds;
        }
        //END FOR DEGREE&BRANCH_EXCEL REPORT

        //START FOR MISC_EXCEL REPORT

        public DataSet GET_MISC_EXCEL(string receiptno, DateTime fromDate, DateTime toDate, string paytype, int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHlper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_CBOOKSRNO", receiptno);
                objParams[1] = new SqlParameter("@P_TODATE", toDate.ToString("yyyy/MM/dd"));
                objParams[2] = new SqlParameter("@P_FROMDATE", fromDate.ToString("yyyy/MM/dd"));
                objParams[3] = new SqlParameter("@P_PAYTYPE", paytype);
                objParams[4] = new SqlParameter("@P_USERNO", userno);
                //objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSqlHlper.ExecuteDataSetSP("PKG_MISC_FEES_COLLECTION_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GET_MISC_EXCEL-> " + ex.ToString());
            }
            return ds;
        }
        //END FOR MISC_EXCEL REPORT

        //START FOR GET_STUDENT_DD EXCEL REPORT

        public DataSet GET_STUDENT_DD(string Receiptno, int semseterno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objsqlHlper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semseterno);
                objParams[2] = new SqlParameter("@P_RECIEPT_TYPE", Receiptno);
                ds = objsqlHlper.ExecuteDataSetSP("PKG_ACAD_FEES_COLLECT_DD_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GET_MISC_EXCEL-> " + ex.ToString());
            }
            return ds;
        }
        //END FOR GET_STUDENT_DD EXCEL REPORT
        //START FOR DEGREE&BRANCH_EXCEL REPORT NEW FORMAT

        public DataSet GET_REPORT_EXCEL_NEW(int sessionno, string branchno, int semesterno, string rectype, string degreeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHlper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_RECIEPT_TYPE", rectype);
                objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                //objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //ds = objSqlHlper.ExecuteDataSetSP("PKG_FEES_REPORT_BRANCH_WISE1_EXCEL", objParams);
                ds = objSqlHlper.ExecuteDataSetSP("PKG_FEES_REPORT_BRANCH_WISE1_EXCEL_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GET_REPORT_EXCEL-> " + ex.ToString());
            }
            return ds;
        }
        //END FOR DEGREE&BRANCH_EXCEL REPORT NEW FORMAT

        #region "Arrear Exam Fee"
        public int AddArrearFeeLog(StudentFees objSF, int PAYMENTTYPE, int GATEWAYID, string Recpt_Code, int Semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_USERNO", objSF.UserNo);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSF.SessionNo);
                objParams[2] = new SqlParameter("@P_AMOUNT", objSF.Amount);
                //objParams[3] = new SqlParameter("@P_TRANSDATE", objSF.TransDate);
                //objParams[4] = new SqlParameter("@P_BRANCHNAME", objSF.BranchName);
                objParams[3] = new SqlParameter("@P_ORDER_ID", objSF.OrderID);
                objParams[4] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
                objParams[5] = new SqlParameter("@P_GATEWAYID", GATEWAYID);
                objParams[6] = new SqlParameter("@P_RCPT_CODE", Recpt_Code);
                //objParams[7] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                objParams[7] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_FEES_LOG_STU_ARREAR_EXAM", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }
        //Added on 19082022

        //use for double verification
        public int OnlinePaymentUpdation(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string MESSAGE, string ap_SecureHash, string remark, string gatewaytxnid, int revalapplytype)//, int ORDER_ID), string REC_TYPE
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[10];
                    sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);  // -- bank id 
                    sqlparam[1] = new SqlParameter("@P_TRANSACTIONID", TRANSACTIONID);  // -- orderid
                    sqlparam[2] = new SqlParameter("@P_AMOUNT", AMOUNT);                //-- amt 
                    sqlparam[3] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail
                    sqlparam[4] = new SqlParameter("@P_MESSAGE", MESSAGE);    // message 
                    sqlparam[5] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);  // dont knw
                    sqlparam[6] = new SqlParameter("@P_REMARK", remark);    // remark 
                    sqlparam[7] = new SqlParameter("@P_GATEWAYTXNID", gatewaytxnid); // bank id 
                    sqlparam[8] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                    sqlparam[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[9].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_UPD_ONLINE_PAYMENT_DETAILS_DOUBLEVERIFY", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }
        public int InsertPayment_Log_TempDCR(int IDNO, int SESSIONNO, string SEMESTERNOS, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNOS),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                // object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR", param, true);
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_ONLINE_PAYMENT_LOG_DCR_TEMP", param, true);

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

        #endregion
        #endregion


        public int InsertInstallmentOnlinePayment_TempDCRCashfree(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data, int installmentno)
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
                            new SqlParameter("@P_INSTALLMENTNO", installmentno),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR_CASHFREE", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCRCashfree-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_TempDCRCashfree(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, int uano, string msg)
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
                            new SqlParameter("@P_UANO", uano),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR_CASHFREE", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCRCashfree-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertInstallmentOnlinePayment_DCRCashfree(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, int installno, string msg)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                    sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[4] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[5] = new SqlParameter("@P_INSTALLNO", installno);
                    sqlparam[6] = new SqlParameter("@P_MESSAGE", msg);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR_CASHFREE", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertInstallmentOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }


        public int InsertOnlinePayment_DCRCashfree(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string msg)
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
                    sqlparam[9] = new SqlParameter("@P_ORGID", 0);
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR_CASHFREE", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Added by Swapnil For get PG ConfigurationDetails
        /// </summary>
        /// <param name="Organizationid"></param>
        /// <param name="payid"></param>
        /// <returns></returns>
        public DataSet GetStudentDetailsFromOrderId(string Orderid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORDERID", Orderid)
                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_DETAILS_FROM_ORDERID", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        //Added bt Tejaswini Dhoble[18-11-2022]

        public DataSet GetReceiptCodeList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_RECEIPTCODE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetReceiptCodeList-> " + ex.ToString());
            }
            return ds;
        }

        //Adding By Tejaswini Dhoble[18-11-2022]
        public DataSet EditReceiptCodeData(int ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_RCNO", ID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_RECEIPTCODE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.EditReceiptCodeData-> " + ex.ToString());
            }
            return ds;
        }

        // Added by Tejaswini Dhoble [19-11-2022]
        public int InsertReceiptCodeData(int id, string RC_NAME, string RECIEPT_CODE, string status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                              new SqlParameter("@P_RCNO",id),                         
                             new SqlParameter("@P_RC_NAME",RC_NAME),
                              new SqlParameter("@P_RECIEPT_CODE",RECIEPT_CODE),
                            new SqlParameter("@P_ACTIVESTATUS", status),                                                
                               new SqlParameter("@P_Action","INSERT_RECEIPTCODE"), 
                               new SqlParameter("@P_OUT", SqlDbType.Int),
                              };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_RECEIPTCODE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 3)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertFacultyDisciplineData-> " + ex.ToString());
            }
            return retStatus;
        }

        //Added by Tejaswini Dhoble[18-11-2022]
        public int UpdateReceiptCodeData(int id, string RC_NAME, string RECIEPT_CODE, string status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_RCNO", id),
                           new SqlParameter("@P_RC_NAME",RC_NAME),
                              new SqlParameter("@P_RECIEPT_CODE",RECIEPT_CODE),
                            new SqlParameter("@P_ACTIVESTATUS", status),                                                
                               new SqlParameter("@P_Action","UPDATE_RECEIPTCODE"), 
                               new SqlParameter("@P_OUT", SqlDbType.Int),
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_RECEIPTCODE", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateFacultyDisciplineData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertInstallmentOnlinePayment_TempDCRnew(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data, int semreg, string Groupids, int Schemeno, int Sessionno)
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
                            new SqlParameter("@P_SEMREGFLAG", semreg),
                            new SqlParameter("@P_GROUPIDS", Groupids),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                            new SqlParameter("@P_MESSAGE", data),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR_RAZORPAY", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_TempDCRnew(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg, int uano, int semreg, string Groupids, int Schemeno, int Sessionno)
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
                            new SqlParameter("@P_UANO", uano),
                            new SqlParameter("@P_SEMREGFLAG", semreg),
                            new SqlParameter("@P_GROUPIDS", Groupids),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_SESSIONNO1", Sessionno),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR_RAZORPAY", param, true);

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

        public int OnlinePhotoRevalPaymentDetails(string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT, string TRANSACTIONSTATUS, string MESSAGE, string ap_SecureHash, string remark, string gatewaytxnid, string EXTRA_CHARGE, int revalapplytype)//, int ORDER_ID), string REC_TYPE
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[11];
                    sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);  // -- bank id 
                    sqlparam[1] = new SqlParameter("@P_TRANSACTIONID", TRANSACTIONID);  // -- orderid
                    sqlparam[2] = new SqlParameter("@P_AMOUNT", AMOUNT);                //-- amt 
                    sqlparam[3] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail
                    sqlparam[4] = new SqlParameter("@P_MESSAGE", MESSAGE);    // message 
                    sqlparam[5] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);  // dont knw
                    sqlparam[6] = new SqlParameter("@P_REMARK", remark);    // remark 
                    sqlparam[7] = new SqlParameter("@P_GATEWAYTXNID", gatewaytxnid); // bank id 
                    sqlparam[8] = new SqlParameter("@P_EXTRA_CHARGE", EXTRA_CHARGE);
                    sqlparam[9] = new SqlParameter("@P_REVAL_TYPE", revalapplytype);
                    sqlparam[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[10].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_UPD_ONLINE_PAYMENT_DETAILS_FOR_PHOTO_REVAL_CRESCENT", sqlparam, true);

                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }


        //Added by Tejaswini[29-11-22]
        public DataSet GetReceiptGenerationList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_RECEIPT_NO_GENERATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetReceiptGenerationList-> " + ex.ToString());
            }
            return ds;
        }

        //Added by Tejaswini[29-11-22]
        public int InsertReceiptGenerationData(int degreeno, string receiptCode, string paymentMode, string counter, string className)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DEGREENO", degreeno), 
                            new SqlParameter("@P_RECIEPT_CODE",receiptCode),
                            new SqlParameter("@P_PAY_MODE_CODE",paymentMode ), 
                            new SqlParameter("@P_COUNTER",counter),
                            new SqlParameter("@P_CLASS",className),                       
                              new SqlParameter("@P_OUT", SqlDbType.Int),
                              
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_RECEIPT_NO_GENERATION", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FeeCollectionController.InsertReceiptGenerationData" + ex.ToString());
            }
            return retStatus;
        }

        public int Insert_Scholarship_Adjustment_Single_Student(ref DailyCollectionRegister dcr, double f4, int bankid, string transactionid, int academicYearID, int ScholarshipId)
        {
            int ret = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_YEAR", dcr.YearNo),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F4", f4),
                    new SqlParameter("@P_COUNTER_NO", dcr.CounterNo),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_REC_NO", dcr.ReceiptNo),
                   //new SqlParameter("@P_REC_DT", (dcr.ReceiptDate != DateTime.MinValue)? dcr.ReceiptDate as object : DBNull.Value as object),
                    new SqlParameter("@P_DATE",dcr.ReceiptDate),
                    new SqlParameter("@P_PAY_MODE_CODE", dcr.PaymentModeCode),
                    new SqlParameter("@P_PAY_TYPE", dcr.PaymentType),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),

                   // new SqlParameter("@P_PRINTDATE", (dcr.PrintDate != DateTime.MinValue) ? dcr.PrintDate as object  : DBNull.Value as object),
                   // new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    //new SqlParameter("@P_BANKNAME",bankname),

                    new SqlParameter ("@P_BANKID",bankid),
                    new SqlParameter("@P_REMARK",dcr.Remark),
                    new SqlParameter("@P_TRANSACTIONID",transactionid ),
                    new SqlParameter("@P_ACADEMIC_YEAR",academicYearID),
                    new SqlParameter("@P_SCHOLARSHIP_ID",ScholarshipId),      
                    new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_DCRNO", dcr.DcrNo),
                };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_INS_SCHOLARSHIP_ADJUSTMENT_SINGLE_STUDENT", sqlParams, true);

                // if payment type is demand draft(D) then save demand draft details also.
                // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public bool Update_Shcolorship_Status_Single_Student(string ScholorshipNO, double SchAdjAmt, int uano, string ipaddress)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SCHLSHP_NO", ScholorshipNO),
                    new SqlParameter("@P_SCH_ADJ_AMT",SchAdjAmt),
                    new SqlParameter("@P_ADJ_UANO",uano),
                    new SqlParameter("@P_ADJ_IP_ADDRESS",ipaddress)
                };
                objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_UPDATE_SCHOLORSHIP_STATUS_SINGLE_STUDE_ADJ", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateExcessStatus() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        public DataSet GetStudentDiscountDetailsSemesterreg(int studentId, int semesterno, string recieptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_DISCOUNT_DETAILS_SEM_REG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet GetStudentScholarshipDetailsSemesterreg(int studentId, int semesterno, string recieptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_SCHOLARSHIP_DETAILS_SEM_REG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet BindValueAddedGroups(int Semesterno, int Schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SEMESTERNO", Semesterno),
                    new SqlParameter("@P_SCHEMENO", Schemeno),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SP_BIND_VALUE_ADDED_GROUPS_ANDROID", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }



        //added by Saurabh S on 20_12_2022
        public DataSet GetBulkCancelRecieptExcelReport(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                                        
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),                                
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",academicyearid),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_BULK_CANCEL_RECIEPT_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by Saurabh S on 20_12_2022
        public DataSet GetStudentListForBulkReciept(int degreeno, int branchno, int semesterno, int admbatch, int collegeid)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", collegeid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_FOR_BULK_FEES_RECIEPT_CARD", objParams);

            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetOnlinePaymentConfigurationDetails_RCPIPER(int Organizationid, int payid, int activityno, string order_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_ACTIVITYNO", activityno),
                    new SqlParameter("@P_PAYID",payid),
                    new SqlParameter("@P_ORDER_ID",order_id)

                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_CONFIG_DETAILS_RCPIPER", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }
        //added by jay on date 04012022
        public DataSet Get_STUDENT_FOR_ONLINE_DCR_REPORT(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),            
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_DCR_FEES_ONLINE_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_ONLINE_DCR_REPORT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet Get_Student_Scholership_DCR_Report(int admBatch, int year)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_ADMBATCH", admBatch),

                    new SqlParameter("@P_ACADEMIC_YEAR_ID", admBatch),
                    new SqlParameter("@P_YEAR",year)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_STUDENT_SCHOLARSHIP_DCR_REPORT_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Scholership_Report() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentLedgerReportDataFormatII(DateTime fromdate, DateTime todate, int degreeno, int branchno, int year, string recpttyp, int admstatus, int academicyearid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[1] = new SqlParameter("@P_TODATE", todate);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_YEAR", year);
                objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", recpttyp);
                objParams[6] = new SqlParameter("@P_ADM_STATUS", admstatus);
                objParams[7] = new SqlParameter("@P_ACADEMIC_YEAR_ID", academicyearid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_LEDGER_REPORT_EXCEL_FORMAT_II", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.GetStudentLedgerReportData-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetdataForConsolidatedReport(string receiptCode, int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_FROM_DATE", from_dt),
                    new SqlParameter("@P_TODATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECIEPT_TYPE", receiptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),                                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_CONSOLIDATED_FEE_COLLECTION_AND_OUTSTANDING_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by Rohit M on 07_02_2023
        public int UpdateUser(string UA_NAME, string UA_PWD, int IDNO, int FirstTimePay)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
                objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);
                objParams[2] = new SqlParameter("@P_IDNO", IDNO);
                objParams[3] = new SqlParameter("@P_FIRSTTIMEPAY", FirstTimePay);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_USER_UPDATE_OA", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 1234)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
            }
            return retStatus;
        }
        //added by Rohit M on 14_02_2023
        public int CreateUser_JECRC(string UA_NAME, string UA_PWD, string UA_FULLNAME, string UA_EMAIL, string UA_ACC, int IDNO)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
                objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);
                objParams[2] = new SqlParameter("@P_UA_FULLNAME", UA_FULLNAME);
                objParams[3] = new SqlParameter("@P_UA_EMAIL", UA_EMAIL);
                objParams[4] = new SqlParameter("@P_UA_ACC", UA_ACC);
                objParams[5] = new SqlParameter("@P_IDNO", IDNO);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_USER_CREATION_OA_JECRC", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 1234)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet Get_Student_Admission_Register_Adademic_Report_Excel(int CollegeId, int degreeno, int branchno, int AcademicYear, int year, int semesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_COLLEGE_ID", CollegeId),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID", AcademicYear),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),            
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_ADMISSION_REGISTER_REPORT_ACADEMIC_YEAR_WISE_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Admission_Register_Adademic_Report_Excel() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetdataForConsolidatedOutstandingReport(int semesterNo, DateTime from_dt, DateTime to_date, int degreeNo, int branchNo, string rectype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_REC_FROM_DT", from_dt),
                    new SqlParameter("@P_REC_TO_DATE", to_date),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),                
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo), 
                    new SqlParameter("@P_RECIEPT_CODE", rectype ), 
                                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_OVERALL_AND_CONSOLIDATED_OUTSTANDING_REPORT_EXCEL_CPU", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by Rohit M on 08_02_2023

        public DataSet GetAllFeesDetailsStudIdOnline(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_ALL_FEE_DATA_ONLINE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }



        //added by Nehal on 04/04/23
        public DataSet GetPaymentModificationReportExcel(int AdcYear)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_ACADEMIC_YEAR", AdcYear),
                                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PAYMENT_MODIFICATIONS_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaymentModificationReportExcel() --> " + ex.Message + " " + ex.StackTrace);
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

        public DataSet Get_Tally_Integration_Reports_Excel(DateTime from_dt, DateTime to_date, int degreeNo, int Branchno, int AcademicYear, int semesterNo, int Year)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@FROMDATE", from_dt),
                    new SqlParameter("@TODATE", to_date),
                    new SqlParameter("@DEGREENO", degreeNo), 
                    new SqlParameter("@BRANCHNO", Branchno), 
                    new SqlParameter("@ACADEMIC_YEAR", AcademicYear),
                    new SqlParameter("@SEMESTERNO", semesterNo), 
                    new SqlParameter("@YEAR", Year ), 
                                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_TALLY_INTEGRATION_DETAILS_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetStudentScholarshipDiscountDetailsSemesterReg(int studentId, int semesterno, string recieptcode, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", recieptcode),
                    new SqlParameter("@P_SESSIONNO", sessionno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_SCHOLARSHIP_DISCOUNT_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        public DataSet GetDetailsOfSemesterAdmissionPaymentConfiguration(int PaymodeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_PAYMODENO", PaymodeNo),              
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PAYMENT_CONFIG_DATA_OF_SEMESTER_ADMISSION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetDetailsOfSemesterAdmissionPaymentConfiguration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //New Method Added by -Gopal M 04-08-2023
        public int InsertIOBPayOnlinePaymentlog(int idno, decimal amount, string order_id, string token_id, string semesterno, string ipaddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[7];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[2] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[3] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[4] = new SqlParameter("@P_SEMESTER", semesterno);
                    sqlparam[5] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[6].Direction = ParameterDirection.Output;
                    // string idcat = sqlparam[4].Direction.ToString();

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_INSERT_IOBPAY_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        public int UpdateIOBPayOnlinePaymentlog(int idno, string order_id, string token_id, string track_id, string fee_type, string status, string trxdate)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[2] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[3] = new SqlParameter("@P_TRACK_ID", track_id);
                    sqlparam[4] = new SqlParameter("@P_FEE_TYPE", fee_type);
                    sqlparam[5] = new SqlParameter("@P_STATUS", status);
                    sqlparam[6] = new SqlParameter("@P_TRANSACTION_TIMESTAMP", trxdate);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_IOBPAY_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        public DataSet GetSpecialisationGroupsbyIdno(int idno, int mode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_MODE", mode),
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_SPECIALIZATION_GROUP_BY_IDNO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetSpecialisationGroupsbyIdno() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        //Added  On Dated 22-11-2023 as per T-No : -50465
        public DataSet Get_Student_Admission_Register_Adademic_Report_Excel_Format_II(int batchname, int collegeid, int degreeno, int branchno, int year, int semesterNo, int Academicyear)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {      
                    new SqlParameter("@P_COLLEGE_ID",collegeid),
                    new SqlParameter("@P_BATCHNAME", batchname),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_ACAD_YEAR", Academicyear),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),            
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_WISE_FEES_PAID_RCPIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Student_Admission_Register_Adademic_Report_Excel_Format_II() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //New Method Added by -Gopal M 29-12-2023 Ticket#51702
        public int InsertISGPayOnlinePaymentlog(int idno, decimal amount, string order_id, string token_id, string semesterno, string ipaddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[7];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[2] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[3] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[4] = new SqlParameter("@P_SEMESTER", semesterno);
                    sqlparam[5] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[6].Direction = ParameterDirection.Output;
                    // string idcat = sqlparam[4].Direction.ToString();

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ISGPAY_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        public int UpdateISGPayOnlinePaymentlog(int idno, string order_id, string token_id, string track_id, string fee_type, string status, string trxdate)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[2] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[3] = new SqlParameter("@P_TRACK_ID", track_id);
                    sqlparam[4] = new SqlParameter("@P_FEE_TYPE", fee_type);
                    sqlparam[5] = new SqlParameter("@P_STATUS", status);
                    sqlparam[6] = new SqlParameter("@P_TRANSACTION_TIMESTAMP", trxdate);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ISGPAY_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        //Added RazorPay payment Integration by -Gopal M 31-01-2023 Ticket#53523
        public int InsertInstallmentOnlinePayment_TempDCR_Razorpay(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data, int installmentno)
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
                                                     new SqlParameter("@P_INSTALLMENT_NO", installmentno),
                                                     new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                                                     };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR_RAZORPAY_PG", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_TempDCR_Razorpay(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg)
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
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR_RAZORPAY_PG", param, true);

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

        public int InsertRazorPayNotCaptureTransaction(int SessionNo, int UserNo, string RazorPayId, double Amount, double RazorPay_Amount, double Fee, double Tax, string OrderId, string RazorPayOrderId, string IPAddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[1] = new SqlParameter("@P_USERNO", UserNo);
                objParams[2] = new SqlParameter("@P_RAZOR_PAYMENT_ID", RazorPayId);
                objParams[3] = new SqlParameter("@P_AMOUNT", Amount);
                objParams[4] = new SqlParameter("@P_RAZORPAY_AMOUNT", RazorPay_Amount);
                objParams[5] = new SqlParameter("@P_FEE", Fee);
                objParams[6] = new SqlParameter("@P_TAX", Tax);
                objParams[7] = new SqlParameter("@P_ORDER_ID", OrderId);
                objParams[8] = new SqlParameter("@P_RAZORPAY_ORDER_ID", RazorPayOrderId);
                objParams[9] = new SqlParameter("@P_IPADDRESS", IPAddress);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("INS_RAZORPAY_NOT_CAPTURE_TRANS", objParams, false);
                if ((int)ret != -99 && (int)ret != 0)
                {
                    retStatus = (int)ret;
                }
                else
                {
                    retStatus = -99;
                }
                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
        }

          //Added Paytm payment Integration by -Gopal M 29-12-2023 Ticket#51702
        public int InsertPAYTMOnlinePaymentlog(int idno, decimal amount, string order_id, string token_id, string semesterno, string ipaddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[7];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[2] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[3] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[4] = new SqlParameter("@P_SEMESTER", semesterno);
                    sqlparam[5] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[6].Direction = ParameterDirection.Output;
                    // string idcat = sqlparam[4].Direction.ToString();

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PAYTM_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertPAYTMOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        public int UpdatePAYTMOnlinePaymentlog(int idno, string order_id, string token_id, string track_id, string fee_type, string status, string trxdate)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_IDNO", idno);
                    sqlparam[1] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[2] = new SqlParameter("@P_TOKEN_ID", token_id);
                    sqlparam[3] = new SqlParameter("@P_TRACK_ID", track_id);
                    sqlparam[4] = new SqlParameter("@P_FEE_TYPE", fee_type);
                    sqlparam[5] = new SqlParameter("@P_STATUS", status);
                    sqlparam[6] = new SqlParameter("@P_TRANSACTION_TIMESTAMP", trxdate);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;

                };
                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_PAYTM_TRANSACTIONS_LOG", sqlparam, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.UpdatePAYTMOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;

        }

        public int InsertInstallmentOnlinePayment_TempDCR_PAYTM(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data, string MID)
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
                            new SqlParameter("@P_MID", MID),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR_PAYTM", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }
 
        public int InsertOnlinePayment_TempDCR_PAYTM(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg, string MID)
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
                            new SqlParameter("@P_MID",MID),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR_PAYTM", param, true);

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

    }
}