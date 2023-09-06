using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Globalization;
using System.IO;
using System.Configuration;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Con_TransferRecordsToTally
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public string GenerateXML(Ent_TransferRecordsToTally objTRM)
                {
                    string ret = string.Empty;
                    StringBuilder sb = new StringBuilder();

                    try
                    {
                        sb.Append("<ENVELOPE>");
                        sb.Append(" <HEADER>");
                        sb.Append("  <VERSION>1</VERSION>");
                        sb.Append("  <TALLYREQUEST>Import</TALLYREQUEST>");
                        sb.Append("  <TYPE>Data</TYPE>");
                        sb.Append("  <ID>Vouchers</ID>");
                        sb.Append(" </HEADER>");
                        sb.Append(" <BODY>");
                        sb.Append(" <DESC></DESC>");
                        sb.Append(" <DATA>");
                        sb.Append("  <TALLYMESSAGE>");
                        sb.Append("    <VOUCHER VCHTYPE=\"" + objTRM.VoucherTypeName + "\" ACTION=\"Create\">");
                        sb.Append("     <DATE>" + objTRM.VoucherDate.ToString("dd-MMM-yyyy") + "</DATE>");
                        sb.Append("      <NARRATION>" + objTRM.Narration + "</NARRATION>");
                        sb.Append("      <VOUCHERTYPENAME>" + objTRM.VoucherTypeName + "</VOUCHERTYPENAME>");
                        sb.Append("      <EFFECTIVEDATE>" + objTRM.VoucherDate.ToString("dd-MMM-yyyy") + "</EFFECTIVEDATE>");

                        double ReceiptAmount = 0.0;
                        foreach (DataRow dr in objTRM.HeadsTable.Rows)
                        {

                            string LedgerName = "";
                            double Amount = 0.00;
                            LedgerName = Convert.ToString(dr["LedgerName"]);
                            Amount = Convert.ToDouble(dr["Amount"]);
                            /////////// formating accHeadName ///////////////////

                            LedgerName = LedgerName.Replace("&", "&amp;");  // for &
                            LedgerName = LedgerName.Replace("\"", "&quot;"); // for "
                            LedgerName = LedgerName.Replace("'", "&apos;"); // for '
                            LedgerName = LedgerName.Replace("<", "&lt;"); // for <
                            LedgerName = LedgerName.Replace(">", "&gt;"); // for >

                            /////////////////////////////////////////////////////

                            sb.Append("      <ALLLEDGERENTRIES.LIST>");
                            sb.Append("       <LEDGERNAME>" + LedgerName + "</LEDGERNAME>");
                            sb.Append("       <AMOUNT>" + Amount.ToString("0.00") + "</AMOUNT>");
                            sb.Append("      </ALLLEDGERENTRIES.LIST>");

                            ReceiptAmount = ReceiptAmount + Amount;

                        }


                        sb.Append("      <ALLLEDGERENTRIES.LIST>");
                        sb.Append("       <LEDGERNAME>" + objTRM.ReceiptLedgerName + "</LEDGERNAME>");
                        sb.Append("       <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>");
                        sb.Append("       <AMOUNT>-" + ReceiptAmount.ToString("0.00") + "</AMOUNT>");



                        sb.Append("       <BANKALLOCATIONS.LIST>");

                        if (objTRM.InstrumentDate != DateTime.MinValue)
                        {
                            sb.Append("        <DATE>" + objTRM.VoucherDate.ToString("dd-MMM-yyyy") + "</DATE>");
                            sb.Append("        <INSTRUMENTDATE>" + objTRM.InstrumentDate.ToString("dd-MMM-yyyy") + "</INSTRUMENTDATE>");
                            sb.Append("        <NAME>" + objTRM.InstrumentNumber + "</NAME>");
                            sb.Append("        <TRANSACTIONTYPE>Cheque/DD</TRANSACTIONTYPE>");
                            sb.Append("        <BANKNAME>" + objTRM.BankName + "</BANKNAME>");
                            sb.Append("        <BANKBRANCHNAME>" + objTRM.BankLocation + "</BANKBRANCHNAME>");
                            sb.Append("        <PAYMENTFAVOURING>" + objTRM.PaymentFavoring + "</PAYMENTFAVOURING>");
                            sb.Append("        <INSTRUMENTNUMBER>" + objTRM.InstrumentNumber + "</INSTRUMENTNUMBER>");
                            sb.Append("        <STATUS>No</STATUS>");
                            sb.Append("        <PAYMENTMODE>Transacted</PAYMENTMODE>");
                            sb.Append("        <BANKPARTYNAME>" + objTRM.BankPartyName + "</BANKPARTYNAME>");
                            sb.Append("        <ISCONNECTEDPAYMENT>No</ISCONNECTEDPAYMENT>");
                            sb.Append("        <ISSPLIT>No</ISSPLIT>");
                            sb.Append("        <ISCONTRACTUSED>No</ISCONTRACTUSED>");
                            sb.Append("        <CHEQUEPRINTED> 1</CHEQUEPRINTED>");
                            sb.Append("        <AMOUNT>-" + ReceiptAmount.ToString("0.00") + "</AMOUNT>");
                            sb.Append("        <CONTRACTDETAILS.LIST></CONTRACTDETAILS.LIST>");
                        }
                        sb.Append("       </BANKALLOCATIONS.LIST>");
                        sb.Append("      </ALLLEDGERENTRIES.LIST>");
                        sb.Append("    </VOUCHER>");
                        sb.Append("  </TALLYMESSAGE>");
                        sb.Append(" </DATA>");
                        sb.Append(" </BODY>");
                        sb.Append("</ENVELOPE>");

                        ret = sb.ToString();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    return ret;
                }
                //comment on 31052017 by nikhild
                //public DataSet GetRecords(Ent_TransferRecordsToTally ObjTRM, string fromdate, string todate)
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@CashBookId", ObjTRM.CashBookId);
                //        objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                //        objParams[2] = new SqlParameter("@FromDate", fromdate);
                //        objParams[3] = new SqlParameter("@ToDate", todate);
                //        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTransactionWiseTotalFeesForTallyTransfer]", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw;
                //    }
                //    return ds;
                //}

                //modification done in 14/02/2018
                public DataSet GetRecordsForTrans(Ent_TransferRecordsToTally ObjTRM, string fromdate, string todate, string paytype)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTRM.CashBookId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                        objParams[2] = new SqlParameter("@FromDate", fromdate);
                        objParams[3] = new SqlParameter("@ToDate", todate);
                        objParams[4] = new SqlParameter("@Degreeno", ObjTRM.Degreeno);
                        objParams[5] = new SqlParameter("@Shiftno", ObjTRM.Shift);
                        objParams[6] = new SqlParameter("@Paytype", paytype);
                        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTransactionWiseTotalFeesForTallyTransfer_New_Combined]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }
                //added new on date 14/01/2018
                public DataSet GetRecords(Ent_TransferRecordsToTally ObjTRM, string fromdate, string todate,string paytype)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTRM.CashBookId);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                        objParams[2] = new SqlParameter("@FromDate", fromdate);
                        objParams[3] = new SqlParameter("@ToDate", todate);
                        objParams[4] = new SqlParameter("@Degreeno", ObjTRM.Degreeno);
                        objParams[5] = new SqlParameter("@Shiftno", ObjTRM.Shift);
                        objParams[6] = new SqlParameter("@Paytype", paytype);
                        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTransactionWiseTotalFeesForTallyTransfer]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }
                //public DataSet GetRecords(Ent_TransferRecordsToTally ObjTRM, string fromdate, string todate)
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@CashBookId", ObjTRM.CashBookId);
                //        objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                //        objParams[2] = new SqlParameter("@FromDate", fromdate);
                //        objParams[3] = new SqlParameter("@ToDate", todate);
                //        objParams[4] = new SqlParameter("@Degreeno", ObjTRM.Degreeno);
                //        objParams[5] = new SqlParameter("@Shiftno", ObjTRM.Shift);
                //        ds = objSQLHelper.ExecuteDataSetSP("[UspGetTransactionWiseTotalFeesForTallyTransfer]", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw;
                //    }
                //    return ds;
                //}
                public long UpdateTallyResponce(string DCRTranIds, string TallyResponses,bool IsTransfers, Ent_TransferRecordsToTally ObjTRM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                       // objParams[0] = new SqlParameter("@TallyTransferTable", ObjTRM.HeadsTable);
                        objParams[0] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                        objParams[1] = new SqlParameter("@ModifiedBy", ObjTRM.ModifiedBy);
                        objParams[2] = new SqlParameter("@IPAddress", ObjTRM.IPAddress);
                        objParams[3] = new SqlParameter("@MACAddress", ObjTRM.MACAddress);
                        objParams[4] = new SqlParameter("@DCRTranIds", DCRTranIds);
                        objParams[5] = new SqlParameter("@TallyResponses", TallyResponses);
                        objParams[6] = new SqlParameter("@IsTransfers", IsTransfers);
                        objParams[7] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspUpdateDCRTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }


                public string GenerateXMLForRefund(Ent_TransferRecordsToTally objTRM)
                {
                    string ret = string.Empty;
                    StringBuilder sb = new StringBuilder();

                    try
                    {
                        sb.Append("<ENVELOPE>");
                        sb.Append(" <HEADER>");
                        sb.Append("  <VERSION>1</VERSION>");
                        sb.Append("  <TALLYREQUEST>Import</TALLYREQUEST>");
                        sb.Append("  <TYPE>Data</TYPE>");
                        sb.Append("  <ID>Vouchers</ID>");
                        sb.Append(" </HEADER>");
                        sb.Append(" <BODY>");
                        sb.Append(" <DESC></DESC>");
                        sb.Append(" <DATA>");
                        sb.Append("  <TALLYMESSAGE>");
                        sb.Append("    <VOUCHER VCHTYPE=\"" + "Payment" + "\" ACTION=\"Create\">");
                        //    sb.Append("     <DATE>" + objTRM.Voucher_From_dt.ToString() + "</DATE>");
                        sb.Append("     <DATE>" + DateTime.ParseExact(objTRM.Voucher_From_dt.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + "</DATE>");


                        sb.Append("      <NARRATION>" + objTRM.Narration + "</NARRATION>");
                        sb.Append("      <VOUCHERTYPENAME>" + "Payment" + "</VOUCHERTYPENAME>");
                        sb.Append("      <EFFECTIVEDATE>" + DateTime.ParseExact(objTRM.Voucher_From_dt.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + "</EFFECTIVEDATE>");

                        double ReceiptAmount = 0.0;
                        foreach (DataRow dr in objTRM.HeadsTable.Rows)
                        {

                            string LedgerName = "";
                            double Amount = 0.00;
                            LedgerName = Convert.ToString(dr["LedgerName"]);
                            Amount = Convert.ToDouble(dr["Amount"]);
                            /////////// formating accHeadName ///////////////////

                            LedgerName = LedgerName.Replace("&", "&amp;");  // for &
                            LedgerName = LedgerName.Replace("\"", "&quot;"); // for "
                            LedgerName = LedgerName.Replace("'", "&apos;"); // for '
                            LedgerName = LedgerName.Replace("<", "&lt;"); // for <
                            LedgerName = LedgerName.Replace(">", "&gt;"); // for >

                            /////////////////////////////////////////////////////

                            sb.Append("      <ALLLEDGERENTRIES.LIST>");
                            sb.Append("       <LEDGERNAME>" + LedgerName + "</LEDGERNAME>");
                            sb.Append("       <AMOUNT>-" + Amount.ToString("0.00") + "</AMOUNT>");
                            sb.Append("      </ALLLEDGERENTRIES.LIST>");

                            ReceiptAmount = ReceiptAmount + Amount;

                        }


                        sb.Append("      <ALLLEDGERENTRIES.LIST>");
                        sb.Append("       <LEDGERNAME>" + objTRM.ReceiptLedgerName + "</LEDGERNAME>");
                        sb.Append("       <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>");
                        sb.Append("       <AMOUNT>" + ReceiptAmount.ToString("0.00") + "</AMOUNT>");



                        //sb.Append("       <BANKALLOCATIONS.LIST>");

                        //if (objTRM.InstrumentDate != DateTime.MinValue)
                        //{
                        //    sb.Append("        <DATE>" + objTRM.VoucherDate.ToString("dd-MMM-yyyy") + "</DATE>");
                        //    sb.Append("        <INSTRUMENTDATE>" + objTRM.InstrumentDate.ToString("dd-MMM-yyyy") + "</INSTRUMENTDATE>");
                        //    sb.Append("        <NAME>" + objTRM.InstrumentNumber + "</NAME>");
                        //    sb.Append("        <TRANSACTIONTYPE>Cheque/DD</TRANSACTIONTYPE>");
                        //    sb.Append("        <BANKNAME>" + objTRM.BankName + "</BANKNAME>");
                        //    sb.Append("        <BANKBRANCHNAME>" + objTRM.BankLocation + "</BANKBRANCHNAME>");
                        //    sb.Append("        <PAYMENTFAVOURING>" + objTRM.PaymentFavoring + "</PAYMENTFAVOURING>");
                        //    sb.Append("        <INSTRUMENTNUMBER>" + objTRM.InstrumentNumber + "</INSTRUMENTNUMBER>");
                        //    sb.Append("        <STATUS>No</STATUS>");
                        //    sb.Append("        <PAYMENTMODE>Transacted</PAYMENTMODE>");
                        //    sb.Append("        <BANKPARTYNAME>" + objTRM.BankPartyName + "</BANKPARTYNAME>");
                        //    sb.Append("        <ISCONNECTEDPAYMENT>No</ISCONNECTEDPAYMENT>");
                        //    sb.Append("        <ISSPLIT>No</ISSPLIT>");
                        //    sb.Append("        <ISCONTRACTUSED>No</ISCONTRACTUSED>");
                        //    sb.Append("        <CHEQUEPRINTED> 1</CHEQUEPRINTED>");
                        //    sb.Append("        <AMOUNT>-" + ReceiptAmount.ToString("0.00") + "</AMOUNT>");
                        //    sb.Append("        <CONTRACTDETAILS.LIST></CONTRACTDETAILS.LIST>");
                        //}
                        //sb.Append("       </BANKALLOCATIONS.LIST>");
                        sb.Append("      </ALLLEDGERENTRIES.LIST>");
                        sb.Append("    </VOUCHER>");
                        sb.Append("  </TALLYMESSAGE>");
                        sb.Append(" </DATA>");
                        sb.Append(" </BODY>");
                        sb.Append("</ENVELOPE>");

                        ret = sb.ToString();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    return ret;
                }

                public long UpdateSuplementrySalPayrollTallyResponce(Ent_TransferRecordsToTally ObjTRM, string PayMonth, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                        objParams[1] = new SqlParameter("@ModifiedBy", ObjTRM.ModifiedBy);
                        objParams[2] = new SqlParameter("@IPAddress", ObjTRM.IPAddress);
                        objParams[3] = new SqlParameter("@MACAddress", ObjTRM.MACAddress);
                        objParams[4] = new SqlParameter("@PayMonth", PayMonth);
                        objParams[5] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspUpdateSuppleSalPayrollTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }

                public DataSet GetSuplementrySalRecordsPayrollTransfer(Ent_TransferRecordsToTally ObjTRM, string stafftype, string Paymonth, int CollegeId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@PayStaffId", stafftype);
                        objParams[1] = new SqlParameter("@CollegeId", CollegeId);
                        objParams[2] = new SqlParameter("@PayMonthYear", Paymonth);


                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetTransactionWiseSupplementrySalaryTransferTally]", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    return ds;
                }

                public long UpdatePayrollTallyResponce(Ent_TransferRecordsToTally ObjTRM, string PayMonth, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                        objParams[1] = new SqlParameter("@ModifiedBy", ObjTRM.ModifiedBy);
                        objParams[2] = new SqlParameter("@IPAddress", ObjTRM.IPAddress);
                        objParams[3] = new SqlParameter("@MACAddress", ObjTRM.MACAddress);
                        objParams[4] = new SqlParameter("@PayMonth", PayMonth);
                        objParams[5] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("[UspUpdatePayrollTallyTransfer]", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                Message = "Transaction Failed!";
                            }
                            else
                            {
                                pkid = Convert.ToInt64(ret.ToString());
                            }
                        }
                        else
                        {
                            pkid = -99;
                            Message = "Transaction Failed!";
                        }
                    }
                    catch (Exception ex)
                    {
                        pkid = -99;
                        throw;

                    }
                    return pkid;
                }

                public DataSet GetRecordsPayrollTransfer(Ent_TransferRecordsToTally ObjTRM, string stafftype, string Paymonth, int CollegeId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@PayStaffId", stafftype);
                        objParams[1] = new SqlParameter("@CollegeId", CollegeId);
                        objParams[2] = new SqlParameter("@PayMonthYear", Paymonth);

                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetTransactionWisePayrollTransferTally]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet GetTallyCompanyName(Ent_TransferRecordsToTally ObjTRM, int CollegeId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@CollegeId", CollegeId);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetSelectedCompanyForTally]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet getEmpInfo(string MONTH, string staffNo, string compcode)
                {
                    DataSet dsDetail = new DataSet();


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_MONTH", MONTH);
                        objParams[1] = new SqlParameter("@p_STAFFNO", staffNo);
                        objParams[2] = new SqlParameter("@p_Compcode", compcode);
                        dsDetail = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_EMP_INFO", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        dsDetail.Dispose();
                    }
                    return dsDetail;
                }
            }
        }
    }
}
