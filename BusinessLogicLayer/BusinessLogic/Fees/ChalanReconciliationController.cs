//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : CHALAN RECONCILIATION CONTROLLER CLASS
// CREATION DATE : 21-AUG-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ChalanReconciliationController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        
        public DataSet FindChalan(string fieldName, string searchText)
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
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FIND_CHALAN1", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.FindChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool ReconcileChalan(DailyCollectionRegister dcr)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_REC_DT", dcr.ReceiptDate),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_DCR_NO", dcr.DcrNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_RECONCILE_CHALAN", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.ReconcileChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public bool DeleteChalan(DailyCollectionRegister dcr, string ipaddress)
            {
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_UA_NO",dcr.UserNo),        //Added By Tejaswini 
                    new SqlParameter("@P_IP_ADDRESS",ipaddress),     //Added By Tejaswini
                    new SqlParameter("@P_DCR_NO", dcr.DcrNo)

                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_DELETE_CHALAN", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.DeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
                }
            }


        //Added by Nikhil Lambe
        public DataSet FindChalan_OnExam(string fieldName, string searchText, int SemesterNo, string RecType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_FIELDNAME", fieldName),
                    new SqlParameter("@P_SEARCHTEXT", searchText),
                    new SqlParameter("@P_SEMESTERNO",SemesterNo) ,
                    new SqlParameter("@P_RECEIPT_CODE",RecType) 
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FIND_CHALAN_ON_EXAM_REG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ChalanReconciliationController.FindChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public bool DeleteLateFee(DailyCollectionRegister dcr,int IsCanceledOrModified)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO",dcr.StudentId),
                    new SqlParameter("@P_REMARK",dcr.Remark),
                    new SqlParameter("@P_LATE_FEE",dcr.CashAmount),
                    new SqlParameter("@P_RECEIPT_TYPECODE",dcr.ReceiptTypeCode),
                    new SqlParameter("@P_IS_CANCELED_OR_MODIFIED",IsCanceledOrModified),
					new SqlParameter("@P_UA_NO",dcr.UserNo),
                    new SqlParameter("@P_DCR_NO",dcr.DcrNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_DELETE_LT_FEE", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ChalanReconciliationController.DeleteLateFee() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        //Added By Nikhil Lambe on 17082020 to Modify the Late Fee
        public DataSet Modify_Late_fee(int Semesterno, string EnrollmentNo, double LateFee, int Dmno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SEMESTER",Semesterno),
                    new SqlParameter("@P_ENROLLMENT",EnrollmentNo),
                    new SqlParameter("@P_LATE_FEE",LateFee),
                    new SqlParameter("@P_DM_NO",Dmno)
                };

                object ret = sqlHelper.ExecuteNonQuerySP("ACD_PKG_MODIFY_LATE_FEE", sqlParams, true);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ChalanReconciliationController.Modify_Late_fee() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //Added by Deepali on 12/08/2020 
        //public bool Generate_Receipt_By_Student(int IDNO, int SEMESTERNO, string RECEIPTCODE, DailyCollectionRegister dcr)
        //{
        //    try
        //    {
        //        int retStatus1 = 0;
        //        int retStatus = Convert.ToInt32(CustomStatus.Others);
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {                    
        //            new SqlParameter("@P_IDNO", IDNO),
        //            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
        //            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
        //            new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
        //            new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
        //            new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
        //            new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
        //            new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
        //            new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
        //            new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
        //            new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
        //            new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
        //            new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
        //            new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
        //            new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
        //            new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
        //            new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
        //            new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
        //            new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
        //            new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
        //            new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
        //            new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
        //            new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
        //            new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
        //            new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
        //            new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
        //            new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
        //            new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
        //            new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
        //            new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
        //            new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
        //            new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
        //            new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
        //            new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
        //            new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
        //            new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
        //            new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
        //            new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
        //            new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
        //            new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
        //            new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
        //            new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
        //            new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
        //            new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
        //            new SqlParameter("@P_UA_NO", dcr.UserNo),
                    
        //            new SqlParameter("@P_DCRNO", dcr.DcrNo),
        //        };
                
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
        //        dcr.DcrNo = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_RECEIPT_BY_STUDENT", sqlParams, true);
        //        //retStatus1 = Convert.ToInt32(studid);
        //        //return retStatus1;

        //        // if payment type is demand draft(D) then save demand draft details also.
        //        // if (dcr.PaymentType == "D" && dcr.DcrNo > 0 || dcr.PaymentType == "T" && dcr.DcrNo > 0 || dcr.PaymentType == "C" && dcr.DcrNo > 0)

        //    }

        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return true;
        //}

        public int Generate_Receipt_By_Student(int IDNO, int SESSIONNO, int SEMESTERNO, int PAYSERVICETYPE, string RECEIPTCODE)
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
                            new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_BANK_CHALLAN_INS_BY_STUDENT", param, true);

                if (ret != null && ret.ToString() != "-1")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

 //Added by Deepali on 12/08/2020 
        public DataSet GetDemand(int studentId, string ReceiptType, int SemesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_RECEIPT_TYPE", ReceiptType),
                    new SqlParameter("@P_SEMESTERNO", SemesterNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_DEMAND_GET_DEMAND_FOR_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by pooja 01_02_2022
        public DataSet BindPendingChallan()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];
                //{
                //    new SqlParameter("@P_FIELDNAME", fieldName),
                //    new SqlParameter("@P_SEARCHTEXT", searchText)
                //};
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PENDING_CHALLAN", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetAllSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet BindPendingChallan_Excel()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];
                //{
                // new SqlParameter("@P_STATUS", STATUS)

                //};
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PENDING_CHALLAN_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetAllSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet BindPendingChallanNew(int STATUS)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                 new SqlParameter("@P_STATUS", STATUS)
                 
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PENDING_CHALLAN_NEW", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetAllSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet BindAllChallanReconNew(string College, string Session, int Paymode, int Status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                 new SqlParameter("@P_College", College),
                 new SqlParameter("@P_Session", Session),
                  new SqlParameter("@P_Status", Status),
                 new SqlParameter("@P_Paymode", Paymode),      
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_ALL_CHALLAN_NEW", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetAllSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool ReconcileChalanNew(DailyCollectionRegister dcr, int STATUS, decimal RecievedAmt, int Installno)
            {
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_REC_DT", dcr.ReceiptDate),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_STATUS", STATUS),
                    new SqlParameter("@P_RECEIVED_AMOUNT", RecievedAmt),
                    new SqlParameter("@P_INSTALLMENTNO", Installno) ,  
                    new SqlParameter("@P_DCR_NO", dcr.DcrNo)                  
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_RECONCILE_CHALAN_NEW", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.ReconcileChalan() --> " + ex.Message + " " + ex.StackTrace);
                }
            }

        //added by aashna 05-11-2022
        public int AddChallaneReconcilation(string IDNO, string SEMESTERNO, string DCRNO, int ORGANIZATIONID, int UANO, string REMARK, string IP_ADDRESS)
            {
            int retStatus = 0;
            try
                {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),                           
                            new SqlParameter("@P_DCR_NO", DCRNO),
                            new SqlParameter("@P_ORGANIZATION_ID", ORGANIZATIONID),
                            new SqlParameter("@P_UA_NO",UANO),
                            new SqlParameter("@P_REMARK",REMARK),
                            new SqlParameter("@P_IP_ADDRESS",IP_ADDRESS),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_DCR_CHALLAN_DETAILS", param, true);

                if (ret != null && ret.ToString() != "-1")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -1;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
                }
            return retStatus;
            }


        //Added by Neha Baranwal 17Jan2020
        #region "Photo Copy AND Reval"

        public DataSet GetChallanDetails(string AdmOrRegNo, string receiptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMORREGNO", AdmOrRegNo),
                    new SqlParameter("@P_REC_CODE", receiptcode)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FIND_REVAL_CHALAN", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public int RevalReconcileChalan(DailyCollectionRegister dcr, int RevalType)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[6];
                    sqlparam[0] = new SqlParameter("@P_IDNO", dcr.StudentId);
                    sqlparam[1] = new SqlParameter("@P_SESSIONNO", dcr.SessionNo);
                    sqlparam[2] = new SqlParameter("@P_REC_DT", dcr.ReceiptDate);
                    sqlparam[3] = new SqlParameter("@P_REMARK", dcr.Remark);
                    sqlparam[4] = new SqlParameter("@P_REVAL_TYPE", RevalType);
                    sqlparam[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[5].Direction = ParameterDirection.Output;
                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_FEECOLLECT_RECONCILE_REVAL_CHALAN", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.RevalReconcileChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }


        public int RevalDeleteChalan(DailyCollectionRegister dcr, int RevalType)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[5];
                    sqlparam[0] = new SqlParameter("@P_IDNO", dcr.StudentId);
                    sqlparam[1] = new SqlParameter("@P_SESSIONNO", dcr.SessionNo);
                    sqlparam[2] = new SqlParameter("@P_REMARK", dcr.Remark);
                    sqlparam[3] = new SqlParameter("@P_REVAL_TYPE", RevalType);
                    sqlparam[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[4].Direction = ParameterDirection.Output;
                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_FEECOLLECT_DELETE_REVAL_CHALAN", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.RevalDeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        public int ArrearReconcileChalan(DailyCollectionRegister dcr)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[6];
                    sqlparam[0] = new SqlParameter("@P_IDNO", dcr.StudentId);
                    sqlparam[1] = new SqlParameter("@P_SESSIONNO", dcr.SessionNo);
                    sqlparam[2] = new SqlParameter("@P_REC_DT", dcr.ReceiptDate);
                    sqlparam[3] = new SqlParameter("@P_REMARK", dcr.Remark);
                    sqlparam[4] = new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo);
                    sqlparam[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[5].Direction = ParameterDirection.Output;
                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_FEECOLLECT_RECONCILE_ARREAR_CHALAN", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ArrearReconcileChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }


        public int ArrearDeleteChalan(DailyCollectionRegister dcr)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[5];
                    sqlparam[0] = new SqlParameter("@P_IDNO", dcr.StudentId);
                    sqlparam[1] = new SqlParameter("@P_SESSIONNO", dcr.SessionNo);
                    sqlparam[2] = new SqlParameter("@P_REMARK", dcr.Remark);
                    sqlparam[3] = new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo);
                    sqlparam[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[4].Direction = ParameterDirection.Output;
                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_FEECOLLECT_DELETE_ARREAR_CHALAN", sqlparam, true);
                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.ArrearDeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        public DataSet GetChallanDetails(string AdmOrRegNo, string receiptcode, int SessionNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMORREGNO", AdmOrRegNo),
                    new SqlParameter("@P_REC_CODE", receiptcode),
                    new SqlParameter("@P_SESSIONNO", SessionNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FIND_REVAL_CHALAN", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.GetChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        #endregion
    }
}