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
            public class FeesTransferStudentwiseController
            {
                public string _client_constr = string.Empty;
                public string LinkServer = "[45.35.13.193]";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public FeesTransferStudentwiseController()
                {
                }
                public FeesTransferStudentwiseController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public DataSet PopulateDegreeFromRF(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select DEGREENO,DEGREENAME from acd_degree where DEGREENAME!='' AND DEGREENO>0");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }





                public DataSet PopulateReceiptType(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select RECIEPT_CODE,RECIEPT_TITLE from ACD_RECIEPT_TYPE where RECIEPT_TITLE!='' AND RECIEPT_CODE>''");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }


                //public DataSet populateTable(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, int BranchNo, string Aided_NoAided, string TransferType, string CompanyCode)
                //{
                //    DataSet ds;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(con);
                //        //if (TransferType == "C")
                //        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                //        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");
                //        //else
                //        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                //        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");

                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                //        objParams[1] = new SqlParameter("@P_ToDate", ToDate);
                //        objParams[2] = new SqlParameter("@P_DegreeNo", DegreeNo);
                //        objParams[3] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                //        objParams[4] = new SqlParameter("@P_BRANCHNO", BranchNo);
                //        objParams[5] = new SqlParameter("@P_COLLEGE_JSS", Aided_NoAided);
                //        objParams[6] = new SqlParameter("@P_TransferType", TransferType);
                //        objParams[7] = new SqlParameter("@P_CompanyCode", CompanyCode);
                //        ds = objSQLHelper.ExecuteDataSetSP("USP_ACC_GET_STUDENT_DCR_NO_WISE", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet populateTable(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, string TransferType, string CompanyCode, int SEMESTERNO, int BATCHNO, int BranchNo)
                {
                    DataSet ds; //6
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //if (TransferType == "C")
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");
                        //else
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                        objParams[1] = new SqlParameter("@P_ToDate", ToDate);
                        objParams[2] = new SqlParameter("@P_DegreeNo", DegreeNo);
                        objParams[3] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[4] = new SqlParameter("@P_TransferType", TransferType);
                        objParams[5] = new SqlParameter("@P_CompanyCode", CompanyCode);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_BATCHNO", BATCHNO);
                        objParams[8] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_ACC_GET_STUDENT_DCR_NO_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateTable(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, int BranchNo, string Aided_NoAided, string TransferType, string CompanyCode)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //if (TransferType == "C")
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");
                        //else
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                        objParams[1] = new SqlParameter("@P_ToDate", ToDate);
                        objParams[2] = new SqlParameter("@P_DegreeNo", DegreeNo);
                        objParams[3] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_JSS", Aided_NoAided);
                        objParams[6] = new SqlParameter("@P_TransferType", TransferType);
                        objParams[7] = new SqlParameter("@P_CompanyCode", CompanyCode);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_ACC_GET_STUDENT_DCR_NO_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetBankCashSum(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, string TransferType, string DCRNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        if (TransferType == "C")
                            ds = objSQLHelper.ExecuteDataSet("select SUM(CASH_AMT) AS AMT from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and G.DEGREENO='" + DegreeNo + "' and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL and DCR_NO in (" + DCRNO + ")");
                        else
                            ds = objSQLHelper.ExecuteDataSet("select SUM(DD_AMT) AS AMT from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and G.DEGREENO='" + DegreeNo + "' and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL and DCR_NO in (" + DCRNO + ")");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateListView(string con, string RECIEPT_CODE, string uano)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DCRNO", uano);
                        objParams[1] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_PKG_ACC_GET_DATA_DCRNO_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateListViewDDWise(string con, string RECIEPT_CODE, string uano,string DDNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DCRNO", uano);
                        objParams[1] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[2] = new SqlParameter("@P_DDNO", DDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_PKG_ACC_GET_FEEHEADDATA_DDWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }



                public int AddfeesStudentWise(string connection, AccountTransaction objAcc, DataTable DtFees, double Amount, string DCRNO, int VoucherNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connection);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@COMPCODE", objAcc.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@ACC_FEE_TRANSFER_STUDENTWISE", DtFees);
                        objParams[2] = new SqlParameter("@TRANSACTION_TYPE", objAcc.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@TRANS", objAcc.TRAN);
                        objParams[4] = new SqlParameter("@OPARTY", objAcc.OPARTY_NO);
                        objParams[5] = new SqlParameter("@DEGREENO", objAcc.DEGREE_NO);
                        objParams[6] = new SqlParameter("@RECEPTTYPE", objAcc.CBTYPE);
                        objParams[7] = new SqlParameter("@USER", objAcc.USER);
                        objParams[8] = new SqlParameter("@AMOUNT", Amount);
                        objParams[9] = new SqlParameter("@PARTICULARS", objAcc.PARTICULARS);
                        objParams[10] = new SqlParameter("@DCRNO", DCRNO);
                        objParams[11] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                        objParams[12] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                        objParams[13] = new SqlParameter("@BRANCH_NO", objAcc.BRANCH_NO);
                        objParams[14] = new SqlParameter("@BATCH_NO", objAcc.BATCH_NO);
                        objParams[15] = new SqlParameter("@SEM_NO", objAcc.SEM_NO);

                        objParams[16] = new SqlParameter("@OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_STUDENTWISE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddReconcilefeesStudentWise(string connection, AccountTransaction objAcc, int BranchNo, string Aided_noaided, DataTable DtFees, double Amount, string DCRNO, string checkno, int VoucherNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connection);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@COMPCODE", objAcc.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@ACC_FEE_TRANSFER_STUDENTWISE", DtFees);
                        objParams[2] = new SqlParameter("@BRANCHNO", BranchNo);
                        objParams[3] = new SqlParameter("@COLLEGE_JSS", Aided_noaided);
                        objParams[4] = new SqlParameter("@TRANSACTION_TYPE", objAcc.TRANSACTION_TYPE);
                        objParams[5] = new SqlParameter("@TRANS", objAcc.TRAN);
                        objParams[6] = new SqlParameter("@OPARTY", objAcc.OPARTY_NO);
                        objParams[7] = new SqlParameter("@DEGREENO", objAcc.DEGREE_NO);
                        objParams[8] = new SqlParameter("@RECEPTTYPE", objAcc.CBTYPE);
                        objParams[9] = new SqlParameter("@USER", objAcc.USER);
                        objParams[10] = new SqlParameter("@AMOUNT", Amount);
                        objParams[11] = new SqlParameter("@PARTICULARS", objAcc.PARTICULARS);
                        objParams[12] = new SqlParameter("@DCRNO", DCRNO);
                        objParams[13] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                        objParams[14] = new SqlParameter("@CHEQUE_NO", checkno);
                        objParams[15] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                        objParams[16] = new SqlParameter("@OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_RECONCILE_TRANSFER_STUDENTWISE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //*************************************** USED FOR MODIFICATION ********************************************************//

                public int ModifyfeesStudentWise(string connection, AccountTransaction objAcc, DataTable DtFees, double Amount, string DCRNO, int VoucherNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connection);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@COMPCODE", objAcc.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@ACC_FEE_TRANSFER_STUDENTWISE", DtFees);
                        objParams[2] = new SqlParameter("@TRANSACTION_TYPE", objAcc.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@TRANS", objAcc.TRAN);
                        objParams[4] = new SqlParameter("@OPARTY", objAcc.OPARTY_NO);
                        objParams[5] = new SqlParameter("@DEGREENO", objAcc.DEGREE_NO);
                        objParams[6] = new SqlParameter("@RECEPTTYPE", objAcc.CBTYPE);
                        objParams[7] = new SqlParameter("@USER", objAcc.USER);
                        objParams[8] = new SqlParameter("@AMOUNT", Amount);
                        objParams[9] = new SqlParameter("@PARTICULARS", objAcc.PARTICULARS);
                        objParams[10] = new SqlParameter("@DCRNO", DCRNO);
                        objParams[11] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                        objParams[12] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                        objParams[13] = new SqlParameter("@OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MODIFY_FEES_TRANSFER_STUDENTWISE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet PopulateDegreeFromRF(string con, int DEGREENO, string CompCode, string Database)
                {
                    DataSet ds;
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("SELECT DISTINCT D.VoucherNo COLLATE DATABASE_DEFAULT +'*'+ D.RECIEPT_CODE COLLATE DATABASE_DEFAULT AS VchNo, T.TRANSACTION_DATE, D.VoucherNo COLLATE DATABASE_DEFAULT + '-' + CONVERT(varchar(12),T.TRANSACTION_DATE,103) COLLATE DATABASE_DEFAULT AS VoucherNo FROM ACD_DCR D JOIN " + Database + ".dbo.ACC_" + CompCode + "_TRANS T ON (D.VoucherNo = T.VOUCHER_NO) WHERE (D.DEGREENO =" + DEGREENO + " OR " + DEGREENO + "=0) AND D.VoucherNo is NOT NULL AND D.IsTransfer = 1 AND D.CAN = 0 AND D.DELET = 0 AND T.TRANSACTION_TYPE = 'R' ORDER BY T.TRANSACTION_DATE DESC");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet PopulateVoucherFromRF(string con, int DEGREENO, string CompCode, string Database)
                {
                    DataSet ds;
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("SELECT DISTINCT D.VoucherNo COLLATE DATABASE_DEFAULT +'*'+ D.RECIEPT_CODE COLLATE DATABASE_DEFAULT AS VchNo, T.TRANSACTION_DATE, D.VoucherNo COLLATE DATABASE_DEFAULT + '-' + CONVERT(varchar(12),T.TRANSACTION_DATE,103) COLLATE DATABASE_DEFAULT AS VoucherNo FROM ACD_DCR D JOIN " + Database + ".dbo.ACC_" + CompCode + "_TRANS T ON (D.VoucherNo = T.VOUCHER_NO) WHERE (D.DEGREENO =" + DEGREENO + " OR " + DEGREENO + "=0) AND D.VoucherNo is NOT NULL AND D.IsTransfer = 1 AND T.TRANSACTION_TYPE = 'R' and T.TRANSFER_ENTRY = 1 ORDER BY T.TRANSACTION_DATE DESC");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }



                /* For Modify Fees Transfer */


                public DataSet populateListViewForModify(string con, string RECIEPT_CODE, string uano)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DCRNO", uano);
                        objParams[1] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_PKG_ACC_GET_DATA_DCRNO_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateTableModify(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, int VoucherNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME,S.PADDRESS,S.PPINCODE from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) LEFT JOIN ACD_STU_ADDRESS S ON S.IDNO=A.IDNO where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " OR " + DegreeNo + " = 0) and A.VoucherNo=" + VoucherNo);
                        //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " OR " + DegreeNo + " = 0) and A.VoucherNo=" + VoucherNo + " AND CAN=0 AND DELET=0");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateModifyTable(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, int VoucherNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and (A.VoucherNo IS NULL OR a.VoucherNo = " + VoucherNo + ") AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetVoucherData(string con, string COMPANY_CODE, string VOUCHERNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VOUCHERNO", VOUCHERNO);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", COMPANY_CODE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_VOUCHERDATA_BY_VOUCHERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                // ---------------------- Bind Company -------------------------------

                public DataSet BindCollege(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select COLLEGE_ID,COLLEGE_NAME from ACD_COLLEGE_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCollegeData(string con, string CompanyId)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPANY_NO", CompanyId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_COLLEGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateTableSvce(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, int BranchNo, string Aided_NoAided, string TransferType, string CompanyCode)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //if (TransferType == "C")
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE='" + TransferType + "' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");
                        //else
                        //    //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND DELET = 0 AND REC_NO <> ''");
                        //    ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " or " + DegreeNo + " = 0) and A.RECIEPT_CODE='" + RECIEPT_CODE + "' and PAY_TYPE <> 'C' and A.IsTransfer IS NULL and A.VoucherNo IS NULL AND CAN = 0 AND RECON=1 AND REC_NO <> ''");

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                        objParams[1] = new SqlParameter("@P_ToDate", ToDate);
                        objParams[2] = new SqlParameter("@P_DegreeNo", DegreeNo);
                        objParams[3] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_JSS", Aided_NoAided);
                        objParams[6] = new SqlParameter("@P_TransferType", TransferType);
                        objParams[7] = new SqlParameter("@P_CompanyCode", CompanyCode);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_ACC_GET_STUDENT_DCR_NO_WISE_SVCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet populateListViewDDWiseSVCE(string con, string RECIEPT_CODE, string uano, string DDNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DCRNO", uano);
                        objParams[1] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[2] = new SqlParameter("@P_DDNO", DDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_PKG_ACC_GET_FEEHEADDATA_DDWISE_SVCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddReconcilefeesStudentWiseSVCE(string connection, AccountTransaction objAcc, int BranchNo, string Aided_noaided, DataTable DtFees, double Amount, string DCRNO, string checkno, int VoucherNo, string FeeTransType)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connection);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@COMPCODE", objAcc.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@ACC_FEE_TRANSFER_STUDENTWISE", DtFees);
                        objParams[2] = new SqlParameter("@BRANCHNO", BranchNo);
                        objParams[3] = new SqlParameter("@COLLEGE_JSS", Aided_noaided);
                        objParams[4] = new SqlParameter("@TRANSACTION_TYPE", objAcc.TRANSACTION_TYPE);
                        objParams[5] = new SqlParameter("@TRANS", objAcc.TRAN);
                        objParams[6] = new SqlParameter("@OPARTY", objAcc.OPARTY_NO);
                        objParams[7] = new SqlParameter("@DEGREENO", objAcc.DEGREE_NO);
                        objParams[8] = new SqlParameter("@RECEPTTYPE", objAcc.CBTYPE);
                        objParams[9] = new SqlParameter("@USER", objAcc.USER);
                        objParams[10] = new SqlParameter("@AMOUNT", Amount);
                        objParams[11] = new SqlParameter("@PARTICULARS", objAcc.PARTICULARS);
                        objParams[12] = new SqlParameter("@DCRNO", DCRNO);
                        objParams[13] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                        objParams[14] = new SqlParameter("@CHEQUE_NO", checkno);
                        objParams[15] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                        objParams[16] = new SqlParameter("@FEETRANS_TYPE",FeeTransType);
                        objParams[17] = new SqlParameter("@OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_RECONCILE_TRANSFER_STUDENTWISE_SVCE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }
            
               //--------------------------------------THIS METHOD USE FOR DELETE RECEIPT WISE TRANSFER FEES----------------------------------------------
               //ADDED BY TANU 12/10/2021


                public DataSet populateDataForTF(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, int BranchNo, string Aided_NoAided, string TransferType, string CompanyCode)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                        objParams[1] = new SqlParameter("@P_ToDate", ToDate);
                        objParams[2] = new SqlParameter("@P_DegreeNo", DegreeNo);
                        objParams[3] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_JSS", Aided_NoAided);
                        objParams[6] = new SqlParameter("@P_TransferType", TransferType);
                        objParams[7] = new SqlParameter("@P_CompanyCode", CompanyCode);
                        ds = objSQLHelper.ExecuteDataSetSP("USP_ACC_GET_RECEIPT_WISE_FEES_TRANSFER_SVCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteReceiptfeesTransaction(string con, string compcode, int DegreeNo, string VCH_TYPE, DateTime fromdate, DateTime todate, AccountTransaction objAcc)
                {
                    int retStatus = 0;
                 
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[2] = new SqlParameter("@P_Cbtype", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_DATE", fromdate);
                        objParams[4] = new SqlParameter("@P_DATETo", todate);
                        objParams[5] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objAcc.TransFieldsTbl_TRAN);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_DELETE_RECEIPT_WISE_FEES_TRANSFER", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                  
                }
            }
        }
    }
}
