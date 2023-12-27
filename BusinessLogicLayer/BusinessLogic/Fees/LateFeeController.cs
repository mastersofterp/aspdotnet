//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : LATE FEE CONTROLLER
// CREATION DATE : 21-JUL-2009                                                        
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
    public class LateFeeController
    {
        public struct LateFeeCriteria
        {
            public string receiptType;
            public string feeItemHead;
            public decimal lateFeeAmount;
            public bool addFeeAmount;
            public DateTime fromDate;
            public DateTime toDate;
            public int branchNo;
            public int semesterNo;
        }

        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        ////public string ChargeLateFeeToStudents(LateFeeCriteria lateFeeCriteria)
        ////{
        ////    string strOutput = "0";
        ////    try
        ////    {
        ////        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        ////        SqlParameter[] sqlParams = new SqlParameter[]
        ////        {
        ////            new SqlParameter("@P_RECIEPT_CODE", lateFeeCriteria.receiptType),
        ////            new SqlParameter("@P_FEE_HEAD", lateFeeCriteria.feeItemHead),                    
        ////            new SqlParameter("@P_LATE_FEE_AMT", lateFeeCriteria.lateFeeAmount),
        ////            new SqlParameter("@P_OVERWRITE_AMT", lateFeeCriteria.addFeeAmount),
        ////            new SqlParameter("@P_FROM_DATE", lateFeeCriteria.fromDate),
        ////            new SqlParameter("@P_TO_DATE", lateFeeCriteria.toDate),
        ////            new SqlParameter("@P_BRANCHNO", lateFeeCriteria.branchNo),
        ////            new SqlParameter("@P_SEMESTERNO", lateFeeCriteria.semesterNo),
        ////            new SqlParameter("@P_OUTPUT", SqlDbType.NVarChar, 4000)
        ////        };
        ////        sqlParams[sqlParams.Length - 1].Value = strOutput;
        ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        ////        object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CHARGE_LATE_FEE", sqlParams, true);

        ////        if (output != null && output.ToString() == "-99")
        ////            return "-99";
        ////        else
        ////            strOutput = output.ToString();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.LateFeeController.ChargeLateFeeToStudents() --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return strOutput;
        ////}

        ////public DataSet GET__LATE_FEES_DETAILS_FOR_EDIT(int LATE_FEE_NO)
        ////{
        ////    DataSet ds = null;
        ////    try
        ////    {
        ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        ////        SqlParameter[] objParams = new SqlParameter[1];
        ////        objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
        ////        ds = objSQLHelper.ExecuteDataSetSP("GET__LATE_FEES_DETAILS_FOR_EDIT", objParams);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return ds;
        ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET__LATE_FEES_DETAILS_FOR_EDIT-> " + ex.ToString());
        ////    }
        ////    return ds;
        ////}

        ////// --created on[25-11-2016]
        ////public int Insert_Late_Fees_Details(int DEGREENO, string RECEIPT_TYPE, DateTime LAST_DATE, string FEE_ITEM)//, string REGULAR_FEES
        ////{
        ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
        ////    try
        ////    {
        ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        ////        SqlParameter[] sqlParams = new SqlParameter[]
        ////                { 
        ////                    new SqlParameter("@P_DEGREENO", DEGREENO),
        ////                    new SqlParameter("@P_RECEIPT_TYPE",RECEIPT_TYPE),
        ////                    new SqlParameter("@P_LAST_DATE", LAST_DATE),
        ////                   // new SqlParameter("@P_REGULAR_FEES", REGULAR_FEES),
        ////                    new SqlParameter("@P_FEE_ITEM", FEE_ITEM),
        ////                    new SqlParameter("@P_OUTPUT", SqlDbType.Int)
        ////                };
        ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

        ////        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_DETAILS", sqlParams, true);
        ////        if (Convert.ToInt32(ret) == -99)
        ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        ////        else
        ////            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        retStatus = Convert.ToInt32(CustomStatus.Error);
        ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Insert_Late_Fees_Details-> " + ex.ToString());
        ////    }
        ////    return retStatus;
        ////}

        ////public int UpdateLate_FeesDetails(int LATE_FEE_NO, string SEQ_NO, string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT)//
        ////{
        ////    int retStatus = Convert.ToInt32(CustomStatus.Others);

        ////    try
        ////    {
        ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        ////        SqlParameter[] objParams = null;

        ////        //update
        ////        objParams = new SqlParameter[6];
        ////        objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
        ////        objParams[1] = new SqlParameter("@P_SEQ_NO", SEQ_NO);
        ////        objParams[2] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
        ////        objParams[3] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
        ////        objParams[4] = new SqlParameter("@P_AMOUNT", AMOUNT);
        ////        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
        ////        objParams[5].Direction = ParameterDirection.Output;

        ////        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_LATE_FEES_DETAILS", objParams, true));
        ////        if (ret.ToString() == "2" && ret != null)
        ////        {
        ////            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        ////        }
        ////        else if (ret.ToString() == "-99")
        ////        {
        ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        retStatus = Convert.ToInt32(CustomStatus.Error);
        ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.UpdateLate_FeesDetails-> " + ex.ToString());
        ////    }
        ////    return retStatus;
        ////}

        ////public int Add_LateFees_Master_Details(int LATE_FEE_NO, string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT)
        ////{
        ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
        ////    try
        ////    {
        ////        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        ////        SqlParameter[] objParams = null;
        ////        objParams = new SqlParameter[5];
        ////        objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
        ////        objParams[1] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
        ////        objParams[2] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
        ////        objParams[3] = new SqlParameter("@P_AMOUNT", AMOUNT);
        ////        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
        ////        objParams[4].Direction = ParameterDirection.Output;
        ////        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_MASTER_DETAILS", objParams, true));
        ////        if (ret.ToString() == "1" && ret != null)
        ////        {
        ////            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        ////        }
        ////        else if (ret.ToString() == "-99")
        ////        {
        ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        retStatus = Convert.ToInt32(CustomStatus.Error);
        ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Add_LateFees_Master_Details-> " + ex.ToString());
        ////    }
        ////    return retStatus;
        ////}

        public int Delete_LateFeesDetails(int Common_no, int ua_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_COMMON_NO", Common_no);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_LATEFEES_DETAILS", objParams, true));
                if (ret.ToString() == "3" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.DeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int Insert_New_Late_Fees_Details(string DEGREENO, string RECEIPT_TYPE, DateTime LAST_DATE, string FEE_ITEM, int SESSIONNO, int UANO, int college_ID, int ReAdmissionFlag, double reAdmAmount)//, string REGULAR_FEES
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DEGREENO", DEGREENO),
                            new SqlParameter("@P_RECEIPT_TYPE",RECEIPT_TYPE),
                            new SqlParameter("@P_LAST_DATE", LAST_DATE),
                            new SqlParameter("@P_FEE_ITEM", FEE_ITEM),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_UA_NO", UANO),
                            new SqlParameter("@P_COLLEGE_ID", college_ID),
                            new SqlParameter("@P_READMISSION_FLAG", ReAdmissionFlag),
                            new SqlParameter("@P_READMISSION_FEE", reAdmAmount),
                            new SqlParameter("@P_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_NEW_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Insert_Late_Fees_Details-> " + ex.ToString());
            }
            return retStatus;
        }


        public int UpdateLate_New_FeesDetails(DateTime LATE_FEES_DATE, string DEGREE, int COMMON_NO, string FEE_ITEM, string RECEIPT_TYPE, int SESSIONNO, int UANO, int college_ID, int ReAdmissionFlag, double reAdmAmount)//string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT,int LATE_FEE_NO, string SEQ_NO,
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[12];
                // objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                // objParams[1] = new SqlParameter("@P_SEQ_NO", SEQ_NO);

                //objParams[0] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
                //objParams[1] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
                //objParams[2] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[0] = new SqlParameter("@P_LATE_FEES_DATE", LATE_FEES_DATE);
                objParams[1] = new SqlParameter("@P_DEGREE", DEGREE);
                objParams[2] = new SqlParameter("@P_COMMON_NO", COMMON_NO);
                objParams[3] = new SqlParameter("@P_FEE_ITEM", FEE_ITEM);
                objParams[4] = new SqlParameter("@P_RECEIPT_TYPE", RECEIPT_TYPE);
                objParams[5] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[6] = new SqlParameter("@P_UA_NO", UANO);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_ID);
                objParams[8] = new SqlParameter("@P_READMISSION_FLAG", ReAdmissionFlag);
                objParams[9] = new SqlParameter("@P_READMISSION_FEE", reAdmAmount);
                objParams[10] = new SqlParameter("@P_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NEW_LATE_FEES_DETAILS", objParams, true));
                if (ret.ToString() == "2" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.UpdateLate_FeesDetails-> " + ex.ToString());
            }
            return retStatus;
        }



        public int Add_LateFees_Master_Details(int LATE_FEE_NO, string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT,int FixedAmtFlag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                objParams[1] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
                objParams[2] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
                objParams[3] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[4] = new SqlParameter("@P_FIXEDAMTFLAG", FixedAmtFlag);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_MASTER_DETAILS", objParams, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Add_LateFees_Master_Details-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet GET__LATE_FEES_DETAILS(int SESSIONNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_LATEFEES_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET__LATE_FEES_DETAILS_FOR_EDIT-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GET__LATE_FEES_DETAILS_FOR_EDIT(int LATE_FEE_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                ds = objSQLHelper.ExecuteDataSetSP("GET__LATE_FEES_DETAILS_FOR_EDITS", objParams);
            }
            catch (Exception ex)
            {

                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET__LATE_FEES_DETAILS_FOR_EDITS-> " + ex.ToString());
            }
            return ds;
        }

        //added on 19-02-2021 by Vaishali to insert the reg & backlog fee details
        public int Insert_Reg_Backlog_Fees_Details(double RegFAmt, double backlogFAmtSem1, double backlogFAmtSem2, double backlogFAmtSem3, double backlogFAmtSem4, double backlogFAmtSem5, double backlogFAmtSem6, double backlogFAmtSem7, double backlogFAmtSem8, string IpAddress, int ua_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_REG_FAMT", RegFAmt),
                            new SqlParameter("@P_BACKLOG_FAMTSEM1",backlogFAmtSem1),
                            new SqlParameter("@P_BACKLOG_FAMTSEM2", backlogFAmtSem2),
                            new SqlParameter("@P_BACKLOG_FAMTSEM3", backlogFAmtSem3),
                            new SqlParameter("@P_BACKLOG_FAMTSEM4", backlogFAmtSem4),
                            new SqlParameter("@P_BACKLOG_FAMTSEM5", backlogFAmtSem5),
                            new SqlParameter("@P_BACKLOG_FAMTSEM6", backlogFAmtSem6),
                            new SqlParameter("@P_BACKLOG_FAMTSEM7", backlogFAmtSem7),
                            new SqlParameter("@P_BACKLOG_FAMTSEM8", backlogFAmtSem8),
                            new SqlParameter("@P_IPADDRESS",IpAddress),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_REG_BACKLOG_FEE_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Insert_Reg_Backlog_Fees_Details-> " + ex.ToString());
            }
            return retStatus;
        }

        // Added By Shailendra K on dated 18.02.2023 for tkt no.39324
        public DataSet GET_LATE_FEES_CANCEL_STUD_DETAILS(DateTime FromDT, DateTime ToDT)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_FROMDT", FromDT);
                objParams[1] = new SqlParameter("@P_TODT", ToDT);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_LATE_FEE_CANCEL_STUD_DETAILS", objParams);
            }
            catch (Exception ex)
            {

                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET_LATE_FEES_CANCEL_STUD_DETAILS-> " + ex.ToString());
            }
            return ds;
        }

        #region ADDED BY GAURAV FOR EXAM LATE FEE

        public DataSet GET__LATE_FEES_DETAILS_EXAM(int SESSIONNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_LATEFEES_DETAILS_EXAM", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET__LATE_FEES_DETAILS_FOR_EDIT-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GET__LATE_FEES_DETAILS_FOR_EDIT_EXAM(int LATE_FEE_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                ds = objSQLHelper.ExecuteDataSetSP("GET__LATE_FEES_DETAILS_FOR_EDITS_EXAM", objParams);
            }
            catch (Exception ex)
            {

                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET__LATE_FEES_DETAILS_FOR_EDITS-> " + ex.ToString());
            }
            return ds;
        }

        public int Insert_New_Late_Fees_Details_EXAM(string DEGREENO, string SEMESTERNOS, string RECEIPT_TYPE, DateTime LAST_DATE, string FEE_ITEM, int SESSIONNO, int UANO, int college_ID, int ReAdmissionFlag, double reAdmAmount)//, string REGULAR_FEES
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DEGREENO", DEGREENO),
                              new SqlParameter("@P_SEMESTERNOS", SEMESTERNOS),
                            new SqlParameter("@P_RECEIPT_TYPE",RECEIPT_TYPE),
                            new SqlParameter("@P_LAST_DATE", LAST_DATE),
                            new SqlParameter("@P_FEE_ITEM", FEE_ITEM),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_UA_NO", UANO),
                            new SqlParameter("@P_COLLEGE_ID", college_ID),
                            new SqlParameter("@P_READMISSION_FLAG", ReAdmissionFlag),
                            new SqlParameter("@P_READMISSION_FEE", reAdmAmount),
                            new SqlParameter("@P_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_NEW_DETAILS_EXAM", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Insert_Late_Fees_Details-> " + ex.ToString());
            }
            return retStatus;
        }
        public int Add_LateFees_Master_Details_EXAM(int LATE_FEE_NO, string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT, int FixedAmtFlag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                objParams[1] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
                objParams[2] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
                objParams[3] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[4] = new SqlParameter("@P_FIXEDAMTFLAG", FixedAmtFlag);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_INS_LATE_FEES_MASTER_DETAILS_EXAM", objParams, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.Add_LateFees_Master_Details-> " + ex.ToString());
            }
            return retStatus;
        }



        public int UpdateLate_New_FeesDetails_EXAM(DateTime LATE_FEES_DATE, string DEGREE, int COMMON_NO, string FEE_ITEM, string RECEIPT_TYPE, int SESSIONNO, int UANO, int college_ID, int ReAdmissionFlag, double reAdmAmount)//string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT,int LATE_FEE_NO, string SEQ_NO,
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[12];
                // objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                // objParams[1] = new SqlParameter("@P_SEQ_NO", SEQ_NO);

                //objParams[0] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
                //objParams[1] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
                //objParams[2] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[0] = new SqlParameter("@P_LATE_FEES_DATE", LATE_FEES_DATE);
                objParams[1] = new SqlParameter("@P_DEGREE", DEGREE);
                objParams[2] = new SqlParameter("@P_COMMON_NO", COMMON_NO);
                objParams[3] = new SqlParameter("@P_FEE_ITEM", FEE_ITEM);
                objParams[4] = new SqlParameter("@P_RECEIPT_TYPE", RECEIPT_TYPE);
                objParams[5] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[6] = new SqlParameter("@P_UA_NO", UANO);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_ID);
                objParams[8] = new SqlParameter("@P_READMISSION_FLAG", ReAdmissionFlag);
                objParams[9] = new SqlParameter("@P_READMISSION_FEE", reAdmAmount);
                objParams[10] = new SqlParameter("@P_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NEW_LATE_FEES_DETAILS_EXAM", objParams, true));
                if (ret.ToString() == "2" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.UpdateLate_FeesDetails-> " + ex.ToString());
            }
            return retStatus;
        }

        public int Delete_LateFeesDetails_EXAM(int LATE_FEE_NO, int ua_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_LATEFEES_DETAILS_EXAM", objParams, true));
                if (ret.ToString() == "3" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ChalanReconciliationController.DeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }





        public int UpdateLate_New_FeesDetails_EXAM(DateTime LATE_FEES_DATE, string DEGREE, int COMMON_NO, string FEE_ITEM, string RECEIPT_TYPE, int SESSIONNO, int UANO, int college_ID, int ReAdmissionFlag, double reAdmAmount, string semesternos)//string DAY_NO_FROM, string DAY_NO_TO, string AMOUNT,int LATE_FEE_NO, string SEQ_NO,
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[13];
                // objParams[0] = new SqlParameter("@P_LATE_FEE_NO", LATE_FEE_NO);
                // objParams[1] = new SqlParameter("@P_SEQ_NO", SEQ_NO);

                //objParams[0] = new SqlParameter("@P_DAY_NO_FROM", DAY_NO_FROM);
                //objParams[1] = new SqlParameter("@P_DAY_NO_TO", DAY_NO_TO);
                //objParams[2] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[0] = new SqlParameter("@P_LATE_FEES_DATE", LATE_FEES_DATE);
                objParams[1] = new SqlParameter("@P_DEGREE", DEGREE);
                objParams[2] = new SqlParameter("@P_COMMON_NO", COMMON_NO);
                objParams[3] = new SqlParameter("@P_FEE_ITEM", FEE_ITEM);
                objParams[4] = new SqlParameter("@P_RECEIPT_TYPE", RECEIPT_TYPE);
                objParams[5] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[6] = new SqlParameter("@P_UA_NO", UANO);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_ID);
                objParams[8] = new SqlParameter("@P_READMISSION_FLAG", ReAdmissionFlag);
                objParams[9] = new SqlParameter("@P_READMISSION_FEE", reAdmAmount);
                objParams[10] = new SqlParameter("@P_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[11] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NEW_LATE_FEES_DETAILS_EXAM", objParams, true));
                if (ret.ToString() == "2" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.UpdateLate_FeesDetails-> " + ex.ToString());
            }
            return retStatus;
        }




        #endregion 
    }
}