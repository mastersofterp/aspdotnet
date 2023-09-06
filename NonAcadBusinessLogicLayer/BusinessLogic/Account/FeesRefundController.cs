using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS.NITPRM;

namespace BusinessLogicLayer.BusinessLogic.Account
{
    public class FeesRefundController
    {
        public string _client_constr = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public FeesRefundController()
        {
        }

        public FeesRefundController(string DbUserName, string DbPassword, String DataBase)
        {
            _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
        }

        public DataSet GetRefundStudentList(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, string TransferType,int BRANCHNO,int SemNo,int BatchNo)
        {
            DataSet ds;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@PAY_TYPE", TransferType);
                objParams[1] = new SqlParameter("@FROM_DATE", FromDate);
                objParams[2] = new SqlParameter("@TODATE", ToDate);
                objParams[3] = new SqlParameter("@DEGREENO", DegreeNo);
                objParams[4] = new SqlParameter("@RECEIPTNO", RECIEPT_CODE);
                objParams[5] = new SqlParameter("@BRANCHNO", BRANCHNO);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", SemNo);
                objParams[7] = new SqlParameter("@P_BATCHNO", BatchNo);
                ds = objSQLHelper.ExecuteDataSetSP("GET_REFUND_FEES_STUDENT_DATA", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet BindRefundAmountFeeHeadWise(string con, string RECIEPT_CODE, string uano)
        {
            DataSet ds;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_REFUNDNO", uano);
                objParams[1] = new SqlParameter("@P_RECIEPT_CODE", RECIEPT_CODE);
                ds = objSQLHelper.ExecuteDataSetSP("USP_PKG_ACC_GET_DATA_REFUNDNO_WISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }

        public int RefundfeesStudentWise(string connection, AccountTransaction objAcc, DataTable DtFees, double Amount, string REFUNDNO, int VoucherNo, int BRANCHNO, int SEMNO, int BATCHNO)
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
                objParams[10] = new SqlParameter("@REFUNDNO", REFUNDNO);
                objParams[11] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                objParams[12] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                objParams[13] = new SqlParameter("@BRANCHNO", BRANCHNO);
                objParams[14] = new SqlParameter("@SEMNO", SEMNO);
                objParams[15] = new SqlParameter("@BATCHNO", BATCHNO);
                objParams[16] = new SqlParameter("@OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_REFUND_STUDENTWISE", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
            }
            return retStatus;
        }



        //----------------------------------------- For Modification ------------------------------------------------------

        public DataSet BindRefundVoucherNo(string con, int DEGREENO, string CompCode, string Database)
        {
            DataSet ds;
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@DEGREENO", DEGREENO);
                objParams[1] = new SqlParameter("@COMP_CODE", CompCode);
                objParams[2] = new SqlParameter("@DBNAME", Database);
                ds = objSQLHelper.ExecuteDataSetSP("GET_REFUND_FEES_VOUCHER_NO_DATA", objParams);
                //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                //SQLHelper objSQLHelper = new SQLHelper(con);
                //ds = objSQLHelper.ExecuteDataSet("SELECT DISTINCT D.VoucherNo COLLATE DATABASE_DEFAULT +'*'+ R.RECIEPT_CODE COLLATE DATABASE_DEFAULT AS VchNo, T.TRANSACTION_DATE, D.VoucherNo COLLATE DATABASE_DEFAULT + '-' + CONVERT(varchar(12),T.TRANSACTION_DATE,103) COLLATE DATABASE_DEFAULT AS VoucherNo FROM ACD_REFUND D INNER JOIN ACD_DCR R ON (D.DCR_NO = R.DCR_NO)  JOIN " + Database + ".dbo.ACC_" + CompCode + "_TRANS T ON (D.VoucherNo = T.VOUCHER_NO) WHERE (R.DEGREENO =0 OR 0=0) AND D.VoucherNo is NOT NULL AND D.IsRefund = 1 AND T.TRANSACTION_TYPE = 'P' ORDER BY T.TRANSACTION_DATE DESC");
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet BindStudentByVoucherNo(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, int VoucherNo)
        {
            DataSet ds;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@FROM_DATE", FromDate);
                objParams[1] = new SqlParameter("@TODATE", ToDate);
                objParams[2] = new SqlParameter("@DEGREENO", DegreeNo);
                objParams[3] = new SqlParameter("@VOUCHERNO", VoucherNo);
                ds = objSQLHelper.ExecuteDataSetSP("GET_REFUND_STUDENT_DATA_VOUCHENO_WISE", objParams);
                //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME,S.PADDRESS,S.PPINCODE from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) LEFT JOIN ACD_STU_ADDRESS S ON S.IDNO=A.IDNO where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " OR " + DegreeNo + " = 0) and A.VoucherNo=" + VoucherNo);
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
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_REFUND_VOUCHERDATA_BY_VOUCHERNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }

        public int ModifyfeesRefundStudentWise(string connection, AccountTransaction objAcc, DataTable DtFees, double Amount, string REFUNDNO, int VoucherNo)
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
                objParams[10] = new SqlParameter("@DCRNO", REFUNDNO);
                objParams[11] = new SqlParameter("@TRANSDATE", objAcc.TRANSACTION_DATE);
                objParams[12] = new SqlParameter("@VOUCHERNUMBER", VoucherNo);
                objParams[13] = new SqlParameter("@OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MODIFY_FEES_REFUND_STUDENTWISE", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
            }
            return retStatus;
        }

        //---------------------------------------------- Refund Report -------------------------------------------------------

        public DataSet GetRefundReport(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, int VoucherNo)
        {
            DataSet ds;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@FROM_DATE", FromDate);
                objParams[1] = new SqlParameter("@TODATE", ToDate);
                objParams[2] = new SqlParameter("@DEGREENO", DegreeNo);
                objParams[3] = new SqlParameter("@VOUCHERNO", VoucherNo);
                ds = objSQLHelper.ExecuteDataSetSP("GET_REFUND_STUDENT_DATA_VOUCHENO_WISE_EXCEL", objParams);
                //ds = objSQLHelper.ExecuteDataSet("select DCR_NO id,NAME,REC_NO,REC_DT,RECIEPT_CODE,TOTAL_AMT,DD_AMT,CASH_AMT,BRANCH,DEGREENAME,S.PADDRESS,S.PPINCODE from ACD_DCR A INNER JOIN ACD_DEGREE G ON(A.DEGREENO = G.DEGREENO) LEFT JOIN ACD_STU_ADDRESS S ON S.IDNO=A.IDNO where REC_DT between '" + FromDate.ToString("dd-MMM-yyyy") + "' and '" + ToDate.ToString("dd-MMM-yyyy") + "' and (G.DEGREENO=" + DegreeNo + " OR " + DegreeNo + " = 0) and A.VoucherNo=" + VoucherNo);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }


        //-----------------------------------------------Refund Verify /Approve Refund Transfer --------------------------------------------------

        public DataSet GetRefundStudentListForVerify(string con, DateTime FromDate, DateTime ToDate, int DegreeNo, string RECIEPT_CODE, string TransferType, int DEPTNO, string AUTHORITYTYPE)
        {
            DataSet ds;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(con);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@PAY_TYPE", TransferType);
                objParams[1] = new SqlParameter("@FROM_DATE", FromDate);
                objParams[2] = new SqlParameter("@TODATE", ToDate);
                objParams[3] = new SqlParameter("@DEGREENO", DegreeNo);
                objParams[4] = new SqlParameter("@RECEIPTNO", RECIEPT_CODE);
                objParams[5] = new SqlParameter("@DEPTNO", DEPTNO);
                objParams[6] = new SqlParameter("@AUTHORITY_TYPE", AUTHORITYTYPE);
                ds = objSQLHelper.ExecuteDataSetSP("GET_REFUND_FEES_STUDENT_DATA_FOR_VERIFY", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesTransferStudentwiesController.PopulateDegree-> " + ex.ToString());
            }
            return ds;
        }

    }
}

