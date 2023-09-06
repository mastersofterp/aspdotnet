//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using IITMS.SQLServer.SQLDAL;
//using System.Data.SqlClient;
//using System.Web;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using BusinessLogicLayer.BusinessEntities.Academic;
//using BusinessLogicLayer.BusinessLogic.Academic;

using System;
using System.Data;
using System.Web;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.Text;
//namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
//{
    namespace IITMS
    {
        namespace UAIMS
      {
           namespace BusinessLayer.BusinessLogic
         {
    public class Con_Acd_TransferRecordsToTally
    {
        // string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        public string GenerateXML(Ent_Acd_TransferRecordsToTally objTRM)
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
                    if (LedgerName != string.Empty)
                    {                       
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
                    }             
                    ReceiptAmount = ReceiptAmount + Amount;

                }
                foreach (DataRow dr in objTRM.HeadsTable.Rows)
                {

                    string BankName = "";
                    double Amount = 0.00;
                    BankName = Convert.ToString(dr["BankName"]);
                    Amount = Convert.ToDouble(dr["Amount"]);

                    if (BankName != string.Empty)
                    {
                        BankName = BankName.Replace("&", "&amp;");  // for &
                        BankName = BankName.Replace("\"", "&quot;"); // for "
                        BankName = BankName.Replace("'", "&apos;"); // for '
                        BankName = BankName.Replace("<", "&lt;"); // for <
                        BankName = BankName.Replace(">", "&gt;"); // for >

                        /////////////////////////////////////////////////////

                        sb.Append("      <ALLLEDGERENTRIES.LIST>");
                        sb.Append("       <LEDGERNAME>" + BankName + "</LEDGERNAME>");
                        sb.Append("       <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>");
                        sb.Append("       <AMOUNT>-" + Amount.ToString("0.00") + "</AMOUNT>");
                        sb.Append("      </ALLLEDGERENTRIES.LIST>");
                    }
                   // ReceiptAmount = ReceiptAmount + Amount;
                }


                sb.Append("      <ALLLEDGERENTRIES.LIST>");
                //sb.Append("       <LEDGERNAME>" + objTRM.ReceiptLedgerName + "</LEDGERNAME>");
            //    sb.Append("       <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>");
            //    sb.Append("       <AMOUNT>-" + ReceiptAmount.ToString("0.00") + "</AMOUNT>");
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



        public DataSet GetRecords(Ent_Acd_TransferRecordsToTally ObjTRM, string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@CashBookId", ObjTRM.CashBookId);
                objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                objParams[2] = new SqlParameter("@FromDate", fromdate);
                objParams[3] = new SqlParameter("@ToDate", todate);
                ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACD_Tally_GetTransactionWiseTotalFeesForTallyTransfer]", objParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }






        public long UpdateTallyResponce(Ent_Acd_TransferRecordsToTally ObjTRM, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@TallyTransferTable", ObjTRM.HeadsTable);
                objParams[1] = new SqlParameter("@CollegeId", ObjTRM.CollegeId);
                objParams[2] = new SqlParameter("@ModifiedBy", ObjTRM.ModifiedBy);
                objParams[3] = new SqlParameter("@IPAddress", ObjTRM.IPAddress);
                objParams[4] = new SqlParameter("@MACAddress", ObjTRM.MACAddress);
             //   objParams[5] = new SqlParameter("@P_DCRID", ObjTRM.DcrId);
                objParams[5] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACD_Tally_UpdateDCRTallyTransfer]", objParams, true);

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


                    }
                }
            }
        }
    
