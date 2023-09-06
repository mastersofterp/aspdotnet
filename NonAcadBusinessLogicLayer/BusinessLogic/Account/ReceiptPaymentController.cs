using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ReceiptPaymentController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
               // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public ReceiptPaymentController()
                {
                }

                public ReceiptPaymentController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }
                public int AddReceiptPayment(ReceiptPayment objRP,string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New ReceiptPayment Group
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_RP_NAME", objRP.Rp_Name);
                        objParams[2] = new SqlParameter("@P_RPH_NO", objRP.Rph_No);
                        objParams[3] = new SqlParameter("@P_RPPRNO", objRP.RpprNo);
                        objParams[4] = new SqlParameter("@P_ACC_CODE", objRP.Acc_Code);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objRP.College_Code);
                        objParams[6] = new SqlParameter("@P_RP_NO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;                        

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_RECIEPT_PRINT_GROUP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.AddReceiptPayment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateReceiptPayment(ReceiptPayment objRP, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update ReceiptPayment Group
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_RP_NO", objRP.Rp_No);
                        objParams[2] = new SqlParameter("@P_RP_NAME", objRP.Rp_Name);
                        objParams[3] = new SqlParameter("@P_RPH_NO", objRP.Rph_No);
                        objParams[4] = new SqlParameter("@P_RPPRNO", objRP.RpprNo);
                        objParams[5] = new SqlParameter("@P_ACC_CODE", objRP.Acc_Code);
                        objParams[6] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;    

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_UPD_RECIEPT_PRINT_GROUP", objParams,true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.UpdateReceiptPayment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetReceiptPayment(int rp_no,object code_year)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_RP_NO", rp_no);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_RECIEPT_PRINT_GROUP", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.GetReceiptPayment-> " + ex.ToString());
                    }
                    return dtr;
                }
                //Added by Nikhil Vinod Lambe on 24/06/2021 to add student data for NEFT/RTGS
                public int Add_NEFT_RTGS_Payment_Offline(int idno, int receiptno, string receiptCode, int semester, string transactionID, DateTime transactionDate, string bankName,
                decimal amount, string fileUpload, int createdBy, string ipAddress)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_RECEIPT_NO", receiptno);
                        objParams[2] = new SqlParameter("@P_RECEIPT_CODE", receiptCode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_TRANSACTION_ID", transactionID);
                        objParams[5] = new SqlParameter("@P_TRANSACTION_DATE", transactionDate);
                        objParams[6] = new SqlParameter("@P_BANK_NAME", bankName);
                        objParams[7] = new SqlParameter("@P_AMOUNT", amount);
                        objParams[8] = new SqlParameter("@P_FILE_UPLOAD", fileUpload);
                        objParams[9] = new SqlParameter("@P_CREATED_BY", createdBy);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_NEFT_RTGS_ENTRY", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
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
                //Added by Nikhil Vinod Lambe on 25/06/2021 to get the NEFT/RTGS students list
                public DataSet Get_NEFT_RTGS_StudentList(int receiptno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
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
                        SQLHelper objSqlHelper = new SQLHelper(connectionString);
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
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
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
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
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



                #region RECEIPT PAYMENT GROUP REPORT
                public int GETRECPAYMENTDATA(string frmDt ,string toDate, int levelNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New ReceiptPayment Group
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE",  HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDATE", frmDt);
                        objParams[2] = new SqlParameter("@P_TODATE", toDate);
                        objParams[3] = new SqlParameter("@P_LEVELNO",Convert.ToString( levelNo));
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REC_PAY_GROUP_REPORT_PREPDATA", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.GETRECPAYMENTDATA-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPartyWthCount(int levelNo,int grpBy)
                {
                    DataSet ds = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@LEVEL_NO", levelNo);
                        objParams[1] = new SqlParameter("@P_GROUPBY", grpBy);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_REC_PAY_GROUP_GET_PARTYWTHCOUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.GetPartyWthCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeletePartyFromRptrp(int partyNo,int delNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PCD", partyNo);
                        objParams[1] = new SqlParameter("@P_DELETE_NO", delNo);
                                                                      
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REC_PAY_GROUP_DEL_PARTY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.DeletePartyFromRptrp-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public DataSet Get_RP_OPBAL(int partyNo,string frmDt,string toDt, int wthCreDebit)
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                                               
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_PARTY_NO", partyNo);
                        objParams[2] = new SqlParameter("@P_FROMDATE", frmDt);
                        objParams[3] = new SqlParameter("@P_TODATE", toDt);
                        objParams[4] = new SqlParameter("@P_WTH_CRE_DEBIT", wthCreDebit);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_REC_PAY_GROUP_OPBAL", objParams);
                                                
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.Get_RP_OPBAL-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsRptrp(ReceiptPayment objrp, int insertNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[17];
                         
                        objParams[0] = new SqlParameter("@P_LEVELNO", objrp.Level_No );
                        objParams[1] = new SqlParameter("@P_PCD", objrp.Party_No );
                        objParams[2] = new SqlParameter("@P_PNAME", objrp.Party_Name );
                        objParams[3] = new SqlParameter("@P_OBALANCE", objrp.OBalanace );
                        objParams[4] = new SqlParameter("@P_CLBALANCE", objrp.CLBalanace );
                        objParams[5] = new SqlParameter("@P_OTYPE", objrp.OType  );
                        objParams[6] = new SqlParameter("@P_CLTYPE", objrp.ClType );
                        objParams[7] = new SqlParameter("@P_DEBIT", objrp.Debit);
                        objParams[8] = new SqlParameter("@P_CREDIT", objrp.Credit);
                        objParams[9] = new SqlParameter("@P_LNO", objrp.LNo );
                        objParams[10] = new SqlParameter("@P_PRVBALANCE", objrp.PrvBalanace );
                        objParams[11] = new SqlParameter("@P_PRVTYPE", objrp.PrvType );
                        objParams[12] = new SqlParameter("@P_RPHEAD", objrp.Rph_No);
                        objParams[13] = new SqlParameter("@P_FGROUP",Convert.ToInt32("0"));
                        objParams[14] = new SqlParameter("@P_GRNO",objrp.Gr_No );
                        objParams[15] = new SqlParameter("@P_GRNAME", objrp.GrName);
                        objParams[16] = new SqlParameter("@P_INSERT_NO", insertNo);
                       
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REC_PAY_GROUP_ADD_RPTRP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.InsRptrp-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdSeqNo(int seqNo,int grNo,int partyNo, int updNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SEQNO", seqNo);
                        objParams[1] = new SqlParameter("@P_GRNO", grNo);
                        objParams[2] = new SqlParameter("@P_PCD", partyNo);
                        objParams[3] = new SqlParameter("@P_UPD_NO", updNo);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REC_PAY_GROUP_UPD_RPTRP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.UpdSeqNo-> " + ex.ToString());

                    }
                    return retStatus;
                }
                #endregion

                //Added By Vidisha on 28/9/2021 to get Data For Bulk Payment Report
                public DataSet GetBulkPaymentReportData(string PayMode, DateTime Date,DateTime ToDate,string Comp_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@PAYMENT_MODE", PayMode);
                        objParams[1] = new SqlParameter("@COMPANY_CODE", Comp_Code);
                        objParams[2] = new SqlParameter("@TRANSACTION_DATE", Date);
                        objParams[3] = new SqlParameter("@TRANSACTION_TO_DATE", ToDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_VOUCHER_FOR_BULK_PAYMENT_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.GetBulkPaymentReportData-> " + ex.ToString());
                    }

                    return ds;
                }


                //Added By TANU on 07/12/2021 to get Data For Bulk Payment Report
                public DataSet GetBulkAprovedPaymentReportData(string PayMode, string Date, string ToDate, string Comp_Code, string status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@PAYMENT_MODE", PayMode);
                        objParams[1] = new SqlParameter("@COMPANY_CODE", Comp_Code);
                        objParams[2] = new SqlParameter("@TRANSACTION_DATE", Date);
                        objParams[3] = new SqlParameter("@TRANSACTION_TO_DATE", ToDate);
                        objParams[4] = new SqlParameter("@_IS_APPROVED", status);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_VOUCHER_FOR_BULK_PAYMENT_REPORT_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReceiptPaymentController.GetBulkPaymentReportData-> " + ex.ToString());
                    }

                    return ds;
                }

            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          