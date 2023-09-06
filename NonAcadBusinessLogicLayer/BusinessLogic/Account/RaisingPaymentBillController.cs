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
            public class RaisingPaymentBillController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public RaisingPaymentBillController()
                {
                    //Blank Constructor
                }

                public RaisingPaymentBillController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }

                //Get Ledger head for Raising Payment Bill
                public DataSet GetAccountEntryLedger(string PrefixText)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        string CompName = HttpContext.Current.Session["BillComp_Code"].ToString();
                        //if (HttpContext.Current.Session["BANKCASHCONTRA"].ToString() == "C")
                        //    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                        //else
                        ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO  NOT IN ('1','2') order by ACC_CODE");
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return ds;
                }

                //Get Bank Ledger Head for Raising Payment Bill 
                public DataSet GetAccountEntryCashBank(string PrefixText)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        string CompName = HttpContext.Current.Session["BillComp_Code"].ToString();
                        //if (HttpContext.Current.Session["BANKCASHCONTRA"].ToString() == "C")
                        //    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                        //else
                        ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return ds;
                }

                //Get Pending Raising Payment Bill List for Department Authority
                public DataSet GetPendingBillList(int Ua_no, string Comp_code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", Ua_no);
                        objParams[1] = new SqlParameter("@P_COMPANY_CODE", Comp_code);
                       

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_RAISINGBILL_PENDINGLIST", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetPendingBillList-> " + ee.ToString());
                    }
                    return ds;
                }

                //Get Pending Raising Payment Bill List for Case Worker
                public DataSet GetPendingBillListforCaseWorker(int Ua_no,int Comp_Acc, string Comp_Code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", Ua_no);
                        objParams[1] = new SqlParameter("@P_COMP_ACC", Comp_Acc);
                        objParams[2] = new SqlParameter("@P_COMP_CODE", Comp_Code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_RAISINGBILL_PENDINGLIST_CASEWORKER", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetPendingBillList-> " + ee.ToString());
                    }
                    return ds;
                }

                //Get Single Pending List Details by BILL Number....
                public DataSet GetSingleRecordsPendingBill(int BillNo, int UA_NO, string Comp_Code)   // Added By Akshay Dixit On 14-07-2022 for IS Single Authority
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BILL_NO", BillNo);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_Comp_Code", Comp_Code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_PENDING_BILL_BY_BILLNO", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetSingleRecordsPendingBill-> " + ee.ToString());
                    }
                    return ds;
                }
         

                //Get Single Pending List Details by BILL Number....Case Worker
                public DataSet GetSingleRecordsPendingBillCaseWorker(int BillNo, int UA_NO)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BILL_NO", BillNo);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_PENDING_BILL_BY_BILLNO_APPROVAL", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetSingleRecordsPendingBillCaseWorker-> " + ee.ToString());
                    }
                    return ds;
                }

                //public DataSet GetSelectedBillDetails(int EMPNO, string Bill_Status,string comp_code)
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_EMPNO", EMPNO);
                //        objParams[1] = new SqlParameter("@P_USERNO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                //        objParams[2] = new SqlParameter("@P_BILL_STATUS", Bill_Status.ToString());
                //        objParams[3] = new SqlParameter("@P_COMPANY_CODE ", comp_code);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_BILLLIST", objParams);
                //    }
                //    catch (Exception ee)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetSingleRecordsPendingBillCaseWorker-> " + ee.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetSelectedBillDetails(int EMPNO, string Bill_Status)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EMPNO", EMPNO);
                        objParams[1] = new SqlParameter("@P_USERNO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                        objParams[2] = new SqlParameter("@P_BILL_STATUS", Bill_Status.ToString());
                        //objParams[3] = new SqlParameter("@P_AP_STATUS", AP_status.ToString());

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_BILLLIST", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetSingleRecordsPendingBillCaseWorker-> " + ee.ToString());
                    }
                    return ds;
                }


                public DataSet GetSelectedDirectBillDetails(int EMPNO, string Bill_Status, string AP_status)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EMPNO", EMPNO);
                        objParams[1] = new SqlParameter("@P_USERNO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                        objParams[2] = new SqlParameter("@P_BILL_STATUS", Bill_Status.ToString());
                        objParams[3] = new SqlParameter("@P_AP_STATUS", AP_status.ToString());

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_DIRECT_BILLLIST", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetSingleRecordsPendingBillCaseWorker-> " + ee.ToString());
                    }
                    return ds;
                }
                public int AddRaisingPaymentBill(RaisingPaymentBill objRPB, int Userno, int college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[60];
                        objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[1] = new SqlParameter("@P_SERIAL_NO", objRPB.SERIAL_NO);
                        objParams[2] = new SqlParameter("@P_ACCOUNT", objRPB.ACCOUNT);
                        objParams[3] = new SqlParameter("@P_DEPT_ID", objRPB.DEPT_ID);
                        objParams[4] = new SqlParameter("@P_APPROVAL_NO", objRPB.APPROVAL_NO);
                        objParams[5] = new SqlParameter("@P_APPROVAL_DATE", Convert.ToDateTime(objRPB.APPROVAL_DATE).ToString("dd-MMM-yyyy"));
                        objParams[6] = new SqlParameter("@P_APPROVED_BY", objRPB.APPROVED_BY);
                        objParams[7] = new SqlParameter("@P_SUPPLIER_NAME", objRPB.SUPPLIER_NAME);
                        objParams[8] = new SqlParameter("@P_PAYEE_NAME_ADDRESS", objRPB.PAYEE_NAME_ADDRESS);
                        objParams[9] = new SqlParameter("@P_NATURE_SERVICE", objRPB.NATURE_SERVICE);
                        objParams[10] = new SqlParameter("@P_GSTIN_NO", objRPB.GSTIN_NO);
                        objParams[11] = new SqlParameter("@P_BILL_AMT", objRPB.BILL_AMT);
                        objParams[12] = new SqlParameter("@P_ISTDS", objRPB.ISTDS);
                        objParams[13] = new SqlParameter("@P_SECTION_NO", objRPB.SECTION_NO);
                        objParams[14] = new SqlParameter("@P_TDS_PERCENT", objRPB.TDS_PERCENT);
                        objParams[15] = new SqlParameter("@P_TDS_AMT", objRPB.TDS_AMT);
                        objParams[16] = new SqlParameter("@P_GST_AMT", objRPB.GST_AMT);
                        objParams[17] = new SqlParameter("@P_TOTAL_BILL_AMT", objRPB.TOTAL_BILL_AMT);
                        objParams[18] = new SqlParameter("@P_PAN_NO", objRPB.PAN_NO);
                        objParams[19] = new SqlParameter("@P_REMARK", objRPB.REMARK);
                        objParams[20] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[21] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[22] = new SqlParameter("@P_COMP_CODE", objRPB.COMPANY_CODE);
                        objParams[23] = new SqlParameter("@P_BUDGET_NO", objRPB.BUDGET_NO);
                        objParams[24] = new SqlParameter("@P_LEDGER_NO", objRPB.LEDGER_NO);
                        objParams[25] = new SqlParameter("@P_BILL_TYPE", objRPB.BILL_TYPE);
                        objParams[26] = new SqlParameter("@P_NET_AMT", objRPB.NET_AMT);

                        if (objRPB.INVOICEDATE == "")
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", objRPB.INVOICEDATE);
                        }
                        else
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", Convert.ToDateTime(objRPB.INVOICEDATE).ToString("dd-MMM-yyyy"));
                        }
                        
                        objParams[28] = new SqlParameter("@P_INVOICE_NO", objRPB.INVOICENO);

                        objParams[29] = new SqlParameter("@P_ISIGST", objRPB.ISIGST);
                        objParams[30] = new SqlParameter("@P_ISGST", objRPB.ISGST);
                        objParams[31] = new SqlParameter("@P_CGST_PER", objRPB.CGST_PER);
                        objParams[32] = new SqlParameter("@P_CGST_AMOUNT", objRPB.CGST_AMT);
                        objParams[33] = new SqlParameter("@P_CGST_SECTIONNO", objRPB.CGST_SECTION); 
                        objParams[34] = new SqlParameter("@P_SGST_PER", objRPB.SGST_PER);
                        objParams[35] = new SqlParameter("@P_SGST_AMOUNT", objRPB.SGST_AMT);
                        objParams[36] = new SqlParameter("@P_SGST_SECTIONNO", objRPB.SGST_SECTION);
                        objParams[37] = new SqlParameter("@P_IGST_PER", objRPB.IGST_PER);
                        objParams[38] = new SqlParameter("@P_IGST_AMOUNT", objRPB.IGST_AMT);
                        objParams[39] = new SqlParameter("@P_IGST_SECTIONNO", objRPB.IGST_SECTION);
                        //------------------Added By Akshay On 03-05-2022-------------------------
                        objParams[40] = new SqlParameter("@P_TDSONGST_AMOUNT", objRPB.TDS_ON_GST_AMT);
                        objParams[41] = new SqlParameter("@P_ISTDSONGST", objRPB.ISTDSONGST);
                        objParams[42] = new SqlParameter("@P_TDSGST_ON_AMT", objRPB.TDSGST_ON_AMT);
                        objParams[43] = new SqlParameter("@P_TDSONGSTPER", objRPB.TDSGST_PERCENT);
                        objParams[44] = new SqlParameter("@P_TDSONGST_SECTION", objRPB.TDSGST_SECTION_NO);
                        objParams[45] = new SqlParameter("@P_TRANS_TDSONGSTID", objRPB.TDSonGSTLedgerId);
                        objParams[46] = new SqlParameter("@P_ISTDSONCGSTSGST", objRPB.ISTDSONCGSTSGST);
                        objParams[47] = new SqlParameter("@P_TDSONCGST_AMOUNT", objRPB.TDS_ON_CGST_AMT);
                        objParams[48] = new SqlParameter("@P_TDSONSGST_AMOUNT", objRPB.TDS_ON_SGST_AMT);
                        objParams[49] = new SqlParameter("@P_TDSCGST_ON_AMT", objRPB.TDSCGST_ON_AMT);
                        objParams[50] = new SqlParameter("@P_TDSSGST_ON_AMT", objRPB.TDSSGST_ON_AMT);
                        objParams[51] = new SqlParameter("@P_TDSONCGSTPER", objRPB.TDSCGST_PERCENT);
                        objParams[52] = new SqlParameter("@P_TDSONSGSTPER", objRPB.TDSSGST_PERCENT);
                        objParams[53] = new SqlParameter("@P_TDSONCGST_SECTION", objRPB.TDSCGST_SECTION_NO);
                        objParams[54] = new SqlParameter("@P_TDSONSGST_SECTION", objRPB.TDSSGST_SECTION_NO);
                        objParams[55] = new SqlParameter("@P_TRANS_TDSONCGSTID", objRPB.TDSonCGSTLedgerId);
                        objParams[56] = new SqlParameter("@P_TRANS_TDSONSGSTID", objRPB.TDSonSGSTLedgerId);

                        objParams[57] = new SqlParameter("@P_TDS_ON_AMT", objRPB.TDS_ON_AMT);
                        objParams[58] = new SqlParameter("@P_TOTAL_TDS_AMT", objRPB.TOTAL_TDS_AMT);
                        //objParams[57] = new SqlParameter("@P_TRANS_CGST_ID", CGSTID);
                        //objParams[58] = new SqlParameter("@P_TRANS_SGST_ID", SGSTID);
                        //objParams[59] = new SqlParameter("@P_TRANS_IGST_ID", IGSTID);
                        //---------------------------------------------------------------------------
                        objParams[59] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[59].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSERT_RAISING_PAYMENT_BILL", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.AddRaisingPaymentBill-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public int AddDirectRaisingBill(RaisingPaymentBill objRPB, int Userno, int college_code, int CGSTID, int SGSTID, int IGSTID,DataTable dtDoc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group

                        objParams = new SqlParameter[76];

                        //objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[1] = new SqlParameter("@P_SERIAL_NO", objRPB.SERIAL_NO);
                        objParams[2] = new SqlParameter("@P_ACCOUNT", objRPB.ACCOUNT);
                        objParams[3] = new SqlParameter("@P_DEPT_ID", objRPB.DEPT_ID);
                        objParams[4] = new SqlParameter("@P_APPROVAL_NO", objRPB.APPROVAL_NO);
                        objParams[5] = new SqlParameter("@P_APPROVAL_DATE", Convert.ToDateTime(objRPB.APPROVAL_DATE).ToString("dd-MMM-yyyy"));
                        objParams[6] = new SqlParameter("@P_APPROVED_BY", objRPB.APPROVED_BY);
                        objParams[7] = new SqlParameter("@P_SUPPLIER_NAME", objRPB.SUPPLIER_NAME);
                        objParams[8] = new SqlParameter("@P_PAYEE_NAME_ADDRESS", objRPB.PAYEE_NAME_ADDRESS);
                        objParams[9] = new SqlParameter("@P_NATURE_SERVICE", objRPB.NATURE_SERVICE);
                        objParams[10] = new SqlParameter("@P_GSTIN_NO", objRPB.GSTIN_NO);
                        objParams[11] = new SqlParameter("@P_BILL_AMT", objRPB.BILL_AMT);
                        objParams[12] = new SqlParameter("@P_ISTDS", objRPB.ISTDS);
                        objParams[13] = new SqlParameter("@P_SECTION_NO", objRPB.SECTION_NO);
                        objParams[14] = new SqlParameter("@P_TDS_PERCENT", objRPB.TDS_PERCENT);
                        objParams[15] = new SqlParameter("@P_TDS_AMT", objRPB.TDS_AMT);
                        objParams[16] = new SqlParameter("@P_GST_AMT", objRPB.GST_AMT);
                        objParams[17] = new SqlParameter("@P_TOTAL_BILL_AMT", objRPB.TOTAL_BILL_AMT);
                        objParams[18] = new SqlParameter("@P_PAN_NO", objRPB.PAN_NO);
                        objParams[19] = new SqlParameter("@P_REMARK", objRPB.REMARK);
                        objParams[20] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[21] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[22] = new SqlParameter("@P_COMP_CODE", objRPB.COMPANY_CODE);
                        objParams[23] = new SqlParameter("@P_BUDGET_NO", objRPB.BUDGET_NO);
                        objParams[24] = new SqlParameter("@P_LEDGER_NO", objRPB.LEDGER_NO);
                        objParams[25] = new SqlParameter("@P_BILL_TYPE", objRPB.BILL_TYPE);
                        objParams[26] = new SqlParameter("@P_NET_AMT", objRPB.NET_AMT);

                        if (objRPB.INVOICEDATE == "")
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", objRPB.INVOICEDATE);
                        }
                        else
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", Convert.ToDateTime(objRPB.INVOICEDATE).ToString("dd-MMM-yyyy"));
                        }

                        objParams[28] = new SqlParameter("@P_INVOICE_NO", objRPB.INVOICENO);

                        objParams[29] = new SqlParameter("@P_TRANS_DATE", objRPB.TransDate);

                        objParams[30] = new SqlParameter("@P_ISIGST", objRPB.ISIGST);
                        objParams[31] = new SqlParameter("@P_ISGST", objRPB.ISGST);
                        objParams[32] = new SqlParameter("@P_CGST_PER", objRPB.CGST_PER);
                        objParams[33] = new SqlParameter("@P_CGST_AMOUNT", objRPB.CGST_AMT);
                        objParams[34] = new SqlParameter("@P_CGST_SECTIONNO", objRPB.CGST_SECTION);
                        objParams[35] = new SqlParameter("@P_SGST_PER", objRPB.SGST_PER);
                        objParams[36] = new SqlParameter("@P_SGST_AMOUNT", objRPB.SGST_AMT);
                        objParams[37] = new SqlParameter("@P_SGST_SECTIONNO", objRPB.SGST_SECTION);
                        objParams[38] = new SqlParameter("@P_IGST_PER", objRPB.IGST_PER);
                        objParams[39] = new SqlParameter("@P_IGST_AMOUNT", objRPB.IGST_AMT);
                        objParams[40] = new SqlParameter("@P_IGST_SECTIONNO", objRPB.IGST_SECTION);
                        objParams[41] = new SqlParameter("@P_TRANS_CGST_ID", CGSTID);
                        objParams[42] = new SqlParameter("@P_TRANS_SGST_ID", SGSTID);
                        objParams[43] = new SqlParameter("@P_TRANS_IGST_ID", IGSTID);
                        objParams[44] = new SqlParameter("@P_TDSLEDGERID", objRPB.TDSLedgerId);
                        objParams[45] = new SqlParameter("@P_BANKLEDGERID", objRPB.BankLedgerId);
                        objParams[46] = new SqlParameter("@P_NARRATION", objRPB.Narration);
                        objParams[47] = new SqlParameter("@P_TDS_ON_AMT", objRPB.TDS_ON_AMT);

                        objParams[48] = new SqlParameter("@P_ISTDSONGST", objRPB.ISTDSONGST);
                        objParams[49] = new SqlParameter("@P_TRANS_TDSONGSTID", objRPB.TDSonGSTLedgerId);
                        objParams[50] = new SqlParameter("@P_TDSONGST_AMOUNT", objRPB.TDS_ON_GST_AMT);
                        objParams[51] = new SqlParameter("@P_TDSGST_ON_AMT", objRPB.TDSGST_ON_AMT);
                        objParams[52] = new SqlParameter("@P_TDSONGSTPER", objRPB.TDSGST_PERCENT);
                        objParams[53] = new SqlParameter("@P_TDSONGST_SECTION", objRPB.TDSGST_SECTION_NO);

                        objParams[54] = new SqlParameter("@P_ISTDSONCGSTSGST", objRPB.ISTDSONCGSTSGST);
                        objParams[55] = new SqlParameter("@P_TRANS_TDSONCGSTID", objRPB.TDSonCGSTLedgerId);
                        objParams[56] = new SqlParameter("@P_TDSONCGST_AMOUNT", objRPB.TDS_ON_CGST_AMT);
                        objParams[57] = new SqlParameter("@P_TDSCGST_ON_AMT", objRPB.TDSCGST_ON_AMT);
                        objParams[58] = new SqlParameter("@P_TDSONCGSTPER", objRPB.TDSCGST_PERCENT);
                        objParams[59] = new SqlParameter("@P_TDSONCGST_SECTION", objRPB.TDSCGST_SECTION_NO);
                                               
                        objParams[60] = new SqlParameter("@P_TRANS_TDSONSGSTID", objRPB.TDSonSGSTLedgerId);
                        objParams[61] = new SqlParameter("@P_TDSONSGST_AMOUNT", objRPB.TDS_ON_SGST_AMT);
                        objParams[62] = new SqlParameter("@P_TDSSGST_ON_AMT", objRPB.TDSSGST_ON_AMT);
                        objParams[63] = new SqlParameter("@P_TDSONSGSTPER", objRPB.TDSSGST_PERCENT);
                        objParams[64] = new SqlParameter("@P_TDSONSGST_SECTION", objRPB.TDSSGST_SECTION_NO);

                        objParams[65] = new SqlParameter("@P_ISSECURITY", objRPB.ISSECURITY);
                        objParams[66] = new SqlParameter("@P_SECURITY_AMT", objRPB.SECURITY_AMT);
                        objParams[67] = new SqlParameter("@P_SECURITY_PER", objRPB.SECURITY_PER);
                        objParams[68] = new SqlParameter("@P_TOTAL_TDS_AMT", objRPB.TOTAL_TDS_AMT);
                        objParams[69] = new SqlParameter("@P_TRANS_SECURITYID", objRPB.SecurityLedgerId);
                        objParams[70] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH); //Added by Vidisha on 13-05-2021 for multiple bill upload
                        objParams[71] = new SqlParameter("@P_ACC_BILL_RAISED_UPLOAD_DOC", dtDoc);	//Added by Vidisha on 13-05-2021 for multiple bill upload	
                        objParams[72] = new SqlParameter("@P_ProjectId", objRPB.ProjectId);//Added by gopal anthati on 19-08-2021
                        objParams[73] = new SqlParameter("@P_ProjectSubId", objRPB.ProjectSubId);//Added by gopal anthati on 19-08-2021
                        objParams[74] = new SqlParameter("@P_EXPENSE_LEDGER_NO", objRPB.EXPENSE_LEDGER_NO);//Added by gopal anthati on 19-08-2021

                        //objParams[75] = new SqlParameter("@P_PROVIDER_TYPE", objRPB.PROVIDER_TYPE);
                        //objParams[76] = new SqlParameter("@P_PAYEE_NATURE", objRPB.PAYEE_NATURE);
                        //objParams[77] = new SqlParameter("@P_SUPPLIER_ID", objRPB.SUPPLIER_ID);


                        objParams[75] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[75].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_DIRECT_BILL_RAISING", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            else if (ret.ToString().Equals("2"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            else //if (ret.ToString().Equals("1"))
                            {
                                objRPB.RAISE_PAY_NO = Convert.ToInt32(ret);
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }

                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.AddRaisingPaymentBill-> " + ee.ToString());
                    }
                    return retStatus;
                }

                //PKG_ACC_UPDATE_PENDIBILL_BY_AUTHORITY
                public int UpdateBillByAuthority(RaisingPaymentBill objRPB, int userno, int CollegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[63];
                        objParams[0] = new SqlParameter("@P_SERIAL_NO", objRPB.SERIAL_NO);
                        objParams[1] = new SqlParameter("@P_ACCOUNT", objRPB.ACCOUNT);
                        objParams[2] = new SqlParameter("@P_DEPT_ID", objRPB.DEPT_ID);
                        objParams[3] = new SqlParameter("@P_APPROVAL_NO", objRPB.APPROVAL_NO);
                        objParams[4] = new SqlParameter("@P_APPROVAL_DATE", Convert.ToDateTime(objRPB.APPROVAL_DATE).ToString("dd-MMM-yyyy"));
                        objParams[5] = new SqlParameter("@P_APPROVED_BY", objRPB.APPROVED_BY);
                        objParams[6] = new SqlParameter("@P_SUPPLIER_NAME", objRPB.SUPPLIER_NAME);
                        objParams[7] = new SqlParameter("@P_PAYEE_NAME_ADDRESS", objRPB.PAYEE_NAME_ADDRESS);
                        objParams[8] = new SqlParameter("@P_NATURE_SERVICE", objRPB.NATURE_SERVICE);
                        objParams[9] = new SqlParameter("@P_GSTIN_NO", objRPB.GSTIN_NO);
                        objParams[10] = new SqlParameter("@P_BILL_AMT", objRPB.BILL_AMT);
                        objParams[11] = new SqlParameter("@P_ISTDS", objRPB.ISTDS);
                        objParams[12] = new SqlParameter("@P_SECTION_NO", objRPB.SECTION_NO);
                        objParams[13] = new SqlParameter("@P_TDS_PERCENT", objRPB.TDS_PERCENT);
                        objParams[14] = new SqlParameter("@P_TDS_AMT", objRPB.TDS_AMT);
                        objParams[15] = new SqlParameter("@P_GST_AMT", objRPB.GST_AMT);
                        objParams[16] = new SqlParameter("@P_TOTAL_BILL_AMT", objRPB.TOTAL_BILL_AMT);
                        objParams[17] = new SqlParameter("@P_PAN_NO", objRPB.PAN_NO);
                        objParams[18] = new SqlParameter("@P_REMARK", objRPB.REMARK);
                        objParams[19] = new SqlParameter("@P_USER_NO", userno);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);
                        objParams[21] = new SqlParameter("@P_COMP_CODE", objRPB.COMPANY_CODE);
                        objParams[22] = new SqlParameter("@P_BUDGET_NO", objRPB.BUDGET_NO);
                        objParams[23] = new SqlParameter("@P_LEDGER_NO", objRPB.LEDGER_NO);
                        objParams[24] = new SqlParameter("@P_BILL_TYPE", objRPB.BILL_TYPE);
                        objParams[25] = new SqlParameter("@P_NET_AMT", objRPB.NET_AMT);
                        if (objRPB.INVOICEDATE == "")
                        {
                            objParams[26] = new SqlParameter("@P_INVOICE_DATE", objRPB.INVOICEDATE);
                        }
                        else
                        {
                            objParams[26] = new SqlParameter("@P_INVOICE_DATE", Convert.ToDateTime(objRPB.INVOICEDATE).ToString("dd-MMM-yyyy"));
                        }
                        objParams[27] = new SqlParameter("@P_INVOICE_NO", objRPB.INVOICENO);
                        objParams[28] = new SqlParameter("@P_ISIGST", objRPB.ISIGST);
                        objParams[29] = new SqlParameter("@P_ISGST", objRPB.ISGST);
                        objParams[30] = new SqlParameter("@P_CGST_PER", objRPB.CGST_PER);
                        objParams[31] = new SqlParameter("@P_CGST_AMOUNT", objRPB.CGST_AMT);
                        objParams[32] = new SqlParameter("@P_CGST_SECTIONNO", objRPB.CGST_SECTION);
                        objParams[33] = new SqlParameter("@P_SGST_PER", objRPB.SGST_PER);
                        objParams[34] = new SqlParameter("@P_SGST_AMOUNT", objRPB.SGST_AMT);
                        objParams[35] = new SqlParameter("@P_SGST_SECTIONNO", objRPB.SGST_SECTION);
                        objParams[36] = new SqlParameter("@P_IGST_PER", objRPB.IGST_PER);
                        objParams[37] = new SqlParameter("@P_IGST_AMOUNT", objRPB.IGST_AMT);
                        objParams[38] = new SqlParameter("@P_IGST_SECTIONNO", objRPB.IGST_SECTION);
                        objParams[39] = new SqlParameter("@P_TDS_ON_AMT", objRPB.TDS_ON_AMT);
                        objParams[40] = new SqlParameter("@P_PAYMENT_MODE", objRPB.PAYMENT_MODE);


                        objParams[41] = new SqlParameter("@P_ISTDSONGST", objRPB.ISTDSONGST);                        
                        objParams[42] = new SqlParameter("@P_TDSONGST_AMOUNT", objRPB.TDS_ON_GST_AMT);
                        objParams[43] = new SqlParameter("@P_TDSGST_ON_AMT", objRPB.TDSGST_ON_AMT);
                        objParams[44] = new SqlParameter("@P_TDSONGSTPER", objRPB.TDSGST_PERCENT);
                        objParams[45] = new SqlParameter("@P_TDSONGST_SECTION", objRPB.TDSGST_SECTION_NO);

                        objParams[46] = new SqlParameter("@P_ISTDSONCGSTSGST", objRPB.ISTDSONCGSTSGST);                       
                        objParams[47] = new SqlParameter("@P_TDSONCGST_AMOUNT", objRPB.TDS_ON_CGST_AMT);
                        objParams[48] = new SqlParameter("@P_TDSCGST_ON_AMT", objRPB.TDSCGST_ON_AMT);
                        objParams[49] = new SqlParameter("@P_TDSONCGSTPER", objRPB.TDSCGST_PERCENT);
                        objParams[50] = new SqlParameter("@P_TDSONCGST_SECTION", objRPB.TDSCGST_SECTION_NO);
                        
                        objParams[51] = new SqlParameter("@P_TDSONSGST_AMOUNT", objRPB.TDS_ON_SGST_AMT);
                        objParams[52] = new SqlParameter("@P_TDSSGST_ON_AMT", objRPB.TDSSGST_ON_AMT);
                        objParams[53] = new SqlParameter("@P_TDSONSGSTPER", objRPB.TDSSGST_PERCENT);
                        objParams[54] = new SqlParameter("@P_TDSONSGST_SECTION", objRPB.TDSSGST_SECTION_NO);

                        objParams[55] = new SqlParameter("@P_ISSECURITY", objRPB.ISSECURITY);
                        objParams[56] = new SqlParameter("@P_SECURITY_AMT", objRPB.SECURITY_AMT);

                        objParams[57] = new SqlParameter("@P_ProjectId", objRPB.ProjectId);//Added by gopal anthati on 19-08-2021
                        objParams[58] = new SqlParameter("@P_ProjectSubId", objRPB.ProjectSubId);//Added by gopal anthati on 19-08-2021

                       
                        //------------------Added By Akshay On 03-05-2022-------------------------
                        objParams[59] = new SqlParameter("@P_TRANS_TDSONGSTID", objRPB.TDSonGSTLedgerId);
                        objParams[60] = new SqlParameter("@P_TRANS_TDSONCGSTID", objRPB.TDSonCGSTLedgerId);
                        objParams[61] = new SqlParameter("@P_TRANS_TDSONSGSTID", objRPB.TDSonSGSTLedgerId);
                        //--------------------------------------------------------------------------------------
                        objParams[62] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[62].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPDATE_PENDIBILL_BY_AUTHORITY", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.UpdateBillByAuthority-> " + ee.ToString());
                    }
                    return retStatus;
                }
                public int ApprovePendingBill(RaisingPaymentBill objRPB, int Userno, int college_code, string status, string remarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_BILLNO", objRPB.SERIAL_NO);
                        objParams[1] = new SqlParameter("@P_UA_NO", Userno);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_APR_REMARKS", remarks);
                        objParams[4] = new SqlParameter("@P_APR_DATE", objRPB.APPROVAL_DATE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_APPROVE_PENIDING_BILL", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else if (ret.ToString().Equals("3"))
                                retStatus = 3;
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.ApprovePendingBill-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public int UpdateTransDetails(int billno, string TransDate, int tdsid, int bankid, string narration, int CgstId, int SgstId, int IgstId, int TRANS_TDSONGSTID, int TRANS_TDSONCGSTID, int TRANS_TDSONSGSTID, int TRANS_SECURITYID, string CHEQUENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_BILLNO", billno);
                        objParams[1] = new SqlParameter("@P_TRANS_DATE", Convert.ToDateTime(TransDate).ToString("dd-MMM-yyyy"));
                        objParams[2] = new SqlParameter("@P_TRANS_TDSID", tdsid);
                        objParams[3] = new SqlParameter("@P_TRANS_BANKID", bankid);
                        objParams[4] = new SqlParameter("@P_TRANS_NARRATION", narration);
                        objParams[5] = new SqlParameter("@P_TRANS_CGST_ID", CgstId);
                        objParams[6] = new SqlParameter("@P_TRANS_SGST_ID", SgstId);
                        objParams[7] = new SqlParameter("@P_TRANS_IGST_ID", IgstId);
                        objParams[8] = new SqlParameter("@P_TRANS_TDSONGSTID", TRANS_TDSONGSTID);
                        objParams[9] = new SqlParameter("@P_TRANS_TDSONCGSTID", TRANS_TDSONCGSTID);
                        objParams[10] = new SqlParameter("@P_TRANS_TDSONSGSTID", TRANS_TDSONSGSTID);
                        objParams[11] = new SqlParameter("@P_TRANS_SECURITYID", TRANS_SECURITYID);
                        objParams[12] = new SqlParameter("@P_TRANS_CHEQUENO", CHEQUENO);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPD_BILL_TRANSDETAILS", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.ApprovePendingBill-> " + ee.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY AKSHAY DIXIT ON 15-07-2022 FOR SINGLE AUTHORITY
                public int ApprovePendingBillByCaseWorker(RaisingPaymentBill objRPB, int Userno, int college_code, string status, string remarks,string Comp_Code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_BILLNO", objRPB.SERIAL_NO);
                        objParams[1] = new SqlParameter("@P_UA_NO", Userno);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_APR_REMARKS", remarks);
                        objParams[4] = new SqlParameter("@P_APR_DATE", objRPB.APPROVAL_DATE);
                        objParams[5] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_APPROVE_PENIDING_BILL_CASEWORKER_SINGLE_AUTH", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else if (ret.ToString().Equals("3"))
                                retStatus = 3;
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.ApprovePendingBill-> " + ee.ToString());
                    }
                    return retStatus;
                }

               
                public int ApprovePendingBillByCaseWorker(RaisingPaymentBill objRPB, int Userno, int college_code, string status, string remarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_BILLNO", objRPB.SERIAL_NO);
                        objParams[1] = new SqlParameter("@P_UA_NO", Userno);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_APR_REMARKS", remarks);
                        objParams[4] = new SqlParameter("@P_APR_DATE", objRPB.APPROVAL_DATE);                      
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_APPROVE_PENIDING_BILL_CASEWORKER", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else if (ret.ToString().Equals("3"))
                                retStatus = 3;
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.ApprovePendingBill-> " + ee.ToString());
                    }
                    return retStatus;
                }


                //Get Account as per Assigned Company to User
                public DataSet GetCompAccount(int userno)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_BINDCOMP_ACCOUNT", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetCompAccount-> " + ee.ToString());
                    }
                    return ds;
                }

                //Bill Check list after voucher created
                public DataSet BillCheckList(int Bankno, string comp_code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BANKNO", Bankno);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", comp_code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_BILLCHECK_APPROVAL_LIST", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }

                //Account list with Company user wise
                public DataSet GetCompAccount(int userno,string code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);
                        objParams[1] = new SqlParameter("@P_STATUS", code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_BINDCOMP_ACCOUNT_CHEQUE", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }

                //To Get Request No with date and Bank name
                public DataSet GetRequestNoDetails(string compcode,int BankId,int AccId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_BANKID", BankId);
                        objParams[2] = new SqlParameter("@P_ACC_ID", AccId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GETCHQ_REQUESTNO", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }


                //To Return Records from Cheque writing
                public int ReturnChequeBill(string RetRemark,int userno,int BillNo,string PayeeName, int voucherNo,string compcode,int ctrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_RETURN_REMARK", RetRemark);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        objParams[2] = new SqlParameter("@P_BILL_NO", BillNo);
                        objParams[3] = new SqlParameter("@P_PAYEE_NAME", PayeeName);
                        objParams[4] = new SqlParameter("@P_VOUCHERNO", voucherNo);
                        objParams[5] = new SqlParameter("@P_COMPCODE", compcode);
                        objParams[6] = new SqlParameter("@P_CTRNO", ctrno);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_CHQ_BILL_RETURN", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.InsertCheckBillApproval-> " + ee.ToString());
                    }
                    return retStatus;
                }

                //To get records for cheque printing
                public DataSet GetChequeList(string Req_No, string compcode,int BankId,int CompAccountId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CHQ_REQ_NO", Req_No);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[2] = new SqlParameter("@P_COMP_ACCOUNT", CompAccountId);
                        objParams[3] = new SqlParameter("@P_BANKID", BankId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_CHQPRINTLIST", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }

                //Bill Check list after voucher created
                public DataSet GetCheckRequestNo(string comp_code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", comp_code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_CHECK_REQNO", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }

                public int InsertCheckBillApproval(RaisingPaymentBill objRPB, int voucherno, string checkno,string BankAccNo,string chkAprno,int Seqno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_CHKBILLID", objRPB.RAISE_PAY_NO);
                        objParams[1] = new SqlParameter("@P_BILL_NO", objRPB.SERIAL_NO.ToString());
                        objParams[2] = new SqlParameter("@P_CHKBILL_APPRNO", chkAprno.ToString());
                        objParams[3] = new SqlParameter("@P_VOUCHER_NO", voucherno);
                        objParams[4] = new SqlParameter("@P_PAYEE_NAME", objRPB.PAYEE_NAME_ADDRESS);
                        objParams[5] = new SqlParameter("@P_CHECKNO", checkno);
                        objParams[6] = new SqlParameter("@P_CHECKDATE", objRPB.APPROVAL_DATE);
                        objParams[7] = new SqlParameter("@P_BANKACCNO", BankAccNo);
                        objParams[8] = new SqlParameter("@P_COMPANY_CODE", objRPB.COMPANY_CODE);
                        objParams[9] = new SqlParameter("@P_NATURE_SERVICE", objRPB.NATURE_SERVICE);
                        objParams[10] = new SqlParameter("@P_AMOUNT", objRPB.TOTAL_BILL_AMT);
                        objParams[11] = new SqlParameter("@P_DEPTID", objRPB.DEPT_ID);
                        objParams[12] = new SqlParameter("@P_APPR_LETTER_DETAILS", objRPB.REMARK);
                        objParams[13] = new SqlParameter("@P_SEQ_NO", Seqno);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", 33);
                        objParams[15] = new SqlParameter("@P_USERNO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_APPROVED_BILLCHECK", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.InsertCheckBillApproval-> " + ee.ToString());
                    }
                    return retStatus;
                }

                #region ReimbursmentBillReport
                public DataSet GetReimbursementBillReport(int Dept_no, DateTime From_date, DateTime To_date, string comp_code)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEPT_NO", Dept_no);
                        objParams[1] = new SqlParameter("@P_COMPANY_CODE", comp_code);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", From_date);
                        objParams[3] = new SqlParameter("@P_TO_DATE", To_date);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_REPORT_BILL_REIMBURSEMENT", objParams);
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.BillCheckList-> " + ee.ToString());
                    }
                    return ds;
                }

                #endregion

                //Added by vijay andoju for trackbill status
                public DataSet GetBillStatus(int TransactionNo)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRANSACTION_NO", TransactionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PAYMENTBILL_STATUS", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                // Added By Akshay Dixit on 18-07-2022 for trackbill status for single authority
                public DataSet GetBillStatusNew(int TransactionNo)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRANSACTION_NO", TransactionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PAYMENTBILL_STATUS_NEW", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                //Added by vijay andoju for budget balance show on 01102020
                public DataSet GetBudegetBalanceNEW(int Dept_No, int Budget_No,DateTime Date)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEPT_NO", Dept_No);
                        objParams[1] = new SqlParameter("@P_BUDGETNO", Budget_No);
                        objParams[2] = new SqlParameter("@P_DATE", Date);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_BUDGET_BALANCE_AMOUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAlertDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by gopal anthati on 19-08-2021 to get project balance
                public string GetSponsorProjectBalance(int ProjectId, int ProjectSubId, string CodeYear)
                {
                    string RemainingBalance = "";
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ProjectId", ProjectId);
                        objParams[1] = new SqlParameter("@P_ProjectSubId", ProjectSubId);
                        objParams[2] = new SqlParameter("@P_CODE_YEAR", CodeYear);
                        RemainingBalance = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SPONSOR_PROJECT_BALANCE", objParams).Tables[0].Rows[0][0].ToString();

                    }
                    catch (Exception ex)
                    {


                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }
                    return RemainingBalance;
                }

                public DataSet GetBillList(string Status, string FromDate, string Todate,string Comp_Code)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STATUS", Status);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", Todate);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", Comp_Code);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_RAISING_BILL_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetBillList-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteJournalVoucher(string voucherno, string compcode, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_VOUCHERNO", voucherno);
                        objParams[1] = new SqlParameter("@P_COMPCODE", compcode);
                        objParams[2] = new SqlParameter("@P_TRANSACTION_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_DELETE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.DeleteJournalVoucher-> " + ex.ToString());
                    }
                    return retStatus;

                }

                //added by tanu 13/12/2021 for add multiple bill after raising bill approval 

                public int AddBillAfterApproval(RaisingPaymentBill objRPB, int Userno, DataTable dtDoc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[1] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[2] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH); 
                        objParams[3] = new SqlParameter("@P_ACC_BILL_RAISED_UPLOAD_DOC", dtDoc);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_BILL_AFTER_APPROVAL", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            else if (ret.ToString().Equals("2"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            else 
                            {
                                objRPB.RAISE_PAY_NO = Convert.ToInt32(ret);
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }

                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.AddRaisingPaymentBill-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public int AddDirectRaisingBillpayment(RaisingPaymentBill objRPB, int Userno, int college_code, int CGSTID, int SGSTID, int IGSTID, DataTable dtDoc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group

                        objParams = new SqlParameter[79];

                        //objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[0] = new SqlParameter("@P_RAISE_PAY_NO", objRPB.RAISE_PAY_NO);
                        objParams[1] = new SqlParameter("@P_SERIAL_NO", objRPB.SERIAL_NO);
                        objParams[2] = new SqlParameter("@P_ACCOUNT", objRPB.ACCOUNT);
                        objParams[3] = new SqlParameter("@P_DEPT_ID", objRPB.DEPT_ID);
                        objParams[4] = new SqlParameter("@P_APPROVAL_NO", objRPB.APPROVAL_NO);
                        objParams[5] = new SqlParameter("@P_APPROVAL_DATE", Convert.ToDateTime(objRPB.APPROVAL_DATE).ToString("dd-MMM-yyyy"));
                        objParams[6] = new SqlParameter("@P_APPROVED_BY", objRPB.APPROVED_BY);
                        objParams[7] = new SqlParameter("@P_SUPPLIER_NAME", objRPB.SUPPLIER_NAME);
                        objParams[8] = new SqlParameter("@P_PAYEE_NAME_ADDRESS", objRPB.PAYEE_NAME_ADDRESS);
                        objParams[9] = new SqlParameter("@P_NATURE_SERVICE", objRPB.NATURE_SERVICE);
                        objParams[10] = new SqlParameter("@P_GSTIN_NO", objRPB.GSTIN_NO);
                        objParams[11] = new SqlParameter("@P_BILL_AMT", objRPB.BILL_AMT);
                        objParams[12] = new SqlParameter("@P_ISTDS", objRPB.ISTDS);
                        objParams[13] = new SqlParameter("@P_SECTION_NO", objRPB.SECTION_NO);
                        objParams[14] = new SqlParameter("@P_TDS_PERCENT", objRPB.TDS_PERCENT);
                        objParams[15] = new SqlParameter("@P_TDS_AMT", objRPB.TDS_AMT);
                        objParams[16] = new SqlParameter("@P_GST_AMT", objRPB.GST_AMT);
                        objParams[17] = new SqlParameter("@P_TOTAL_BILL_AMT", objRPB.TOTAL_BILL_AMT);
                        objParams[18] = new SqlParameter("@P_PAN_NO", objRPB.PAN_NO);
                        objParams[19] = new SqlParameter("@P_REMARK", objRPB.REMARK);
                        objParams[20] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[21] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[22] = new SqlParameter("@P_COMP_CODE", objRPB.COMPANY_CODE);
                        objParams[23] = new SqlParameter("@P_BUDGET_NO", objRPB.BUDGET_NO);
                        objParams[24] = new SqlParameter("@P_LEDGER_NO", objRPB.LEDGER_NO);
                        objParams[25] = new SqlParameter("@P_BILL_TYPE", objRPB.BILL_TYPE);
                        objParams[26] = new SqlParameter("@P_NET_AMT", objRPB.NET_AMT);

                        if (objRPB.INVOICEDATE == "")
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", objRPB.INVOICEDATE);
                        }
                        else
                        {
                            objParams[27] = new SqlParameter("@P_INVOICE_DATE", Convert.ToDateTime(objRPB.INVOICEDATE).ToString("dd-MMM-yyyy"));
                        }

                        objParams[28] = new SqlParameter("@P_INVOICE_NO", objRPB.INVOICENO);

                        objParams[29] = new SqlParameter("@P_TRANS_DATE", objRPB.TransDate);

                        objParams[30] = new SqlParameter("@P_ISIGST", objRPB.ISIGST);
                        objParams[31] = new SqlParameter("@P_ISGST", objRPB.ISGST);
                        objParams[32] = new SqlParameter("@P_CGST_PER", objRPB.CGST_PER);
                        objParams[33] = new SqlParameter("@P_CGST_AMOUNT", objRPB.CGST_AMT);
                        objParams[34] = new SqlParameter("@P_CGST_SECTIONNO", objRPB.CGST_SECTION);
                        objParams[35] = new SqlParameter("@P_SGST_PER", objRPB.SGST_PER);
                        objParams[36] = new SqlParameter("@P_SGST_AMOUNT", objRPB.SGST_AMT);
                        objParams[37] = new SqlParameter("@P_SGST_SECTIONNO", objRPB.SGST_SECTION);
                        objParams[38] = new SqlParameter("@P_IGST_PER", objRPB.IGST_PER);
                        objParams[39] = new SqlParameter("@P_IGST_AMOUNT", objRPB.IGST_AMT);
                        objParams[40] = new SqlParameter("@P_IGST_SECTIONNO", objRPB.IGST_SECTION);
                        objParams[41] = new SqlParameter("@P_TRANS_CGST_ID", CGSTID);
                        objParams[42] = new SqlParameter("@P_TRANS_SGST_ID", SGSTID);
                        objParams[43] = new SqlParameter("@P_TRANS_IGST_ID", IGSTID);
                        objParams[44] = new SqlParameter("@P_TDSLEDGERID", objRPB.TDSLedgerId);
                        objParams[45] = new SqlParameter("@P_BANKLEDGERID", objRPB.BankLedgerId);
                        objParams[46] = new SqlParameter("@P_NARRATION", objRPB.Narration);
                        objParams[47] = new SqlParameter("@P_TDS_ON_AMT", objRPB.TDS_ON_AMT);

                        objParams[48] = new SqlParameter("@P_ISTDSONGST", objRPB.ISTDSONGST);
                        objParams[49] = new SqlParameter("@P_TRANS_TDSONGSTID", objRPB.TDSonGSTLedgerId);
                        objParams[50] = new SqlParameter("@P_TDSONGST_AMOUNT", objRPB.TDS_ON_GST_AMT);
                        objParams[51] = new SqlParameter("@P_TDSGST_ON_AMT", objRPB.TDSGST_ON_AMT);
                        objParams[52] = new SqlParameter("@P_TDSONGSTPER", objRPB.TDSGST_PERCENT);
                        objParams[53] = new SqlParameter("@P_TDSONGST_SECTION", objRPB.TDSGST_SECTION_NO);

                        objParams[54] = new SqlParameter("@P_ISTDSONCGSTSGST", objRPB.ISTDSONCGSTSGST);
                        objParams[55] = new SqlParameter("@P_TRANS_TDSONCGSTID", objRPB.TDSonCGSTLedgerId);
                        objParams[56] = new SqlParameter("@P_TDSONCGST_AMOUNT", objRPB.TDS_ON_CGST_AMT);
                        objParams[57] = new SqlParameter("@P_TDSCGST_ON_AMT", objRPB.TDSCGST_ON_AMT);
                        objParams[58] = new SqlParameter("@P_TDSONCGSTPER", objRPB.TDSCGST_PERCENT);
                        objParams[59] = new SqlParameter("@P_TDSONCGST_SECTION", objRPB.TDSCGST_SECTION_NO);

                        objParams[60] = new SqlParameter("@P_TRANS_TDSONSGSTID", objRPB.TDSonSGSTLedgerId);
                        objParams[61] = new SqlParameter("@P_TDSONSGST_AMOUNT", objRPB.TDS_ON_SGST_AMT);
                        objParams[62] = new SqlParameter("@P_TDSSGST_ON_AMT", objRPB.TDSSGST_ON_AMT);
                        objParams[63] = new SqlParameter("@P_TDSONSGSTPER", objRPB.TDSSGST_PERCENT);
                        objParams[64] = new SqlParameter("@P_TDSONSGST_SECTION", objRPB.TDSSGST_SECTION_NO);

                        objParams[65] = new SqlParameter("@P_ISSECURITY", objRPB.ISSECURITY);
                        objParams[66] = new SqlParameter("@P_SECURITY_AMT", objRPB.SECURITY_AMT);
                        objParams[67] = new SqlParameter("@P_SECURITY_PER", objRPB.SECURITY_PER);
                        objParams[68] = new SqlParameter("@P_TOTAL_TDS_AMT", objRPB.TOTAL_TDS_AMT);
                        objParams[69] = new SqlParameter("@P_TRANS_SECURITYID", objRPB.SecurityLedgerId);
                        objParams[70] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH); //Added by Vidisha on 13-05-2021 for multiple bill upload
                        objParams[71] = new SqlParameter("@P_ACC_BILL_RAISED_UPLOAD_DOC", dtDoc);	//Added by Vidisha on 13-05-2021 for multiple bill upload	
                        objParams[72] = new SqlParameter("@P_ProjectId", objRPB.ProjectId);//Added by gopal anthati on 19-08-2021
                        objParams[73] = new SqlParameter("@P_ProjectSubId", objRPB.ProjectSubId);//Added by gopal anthati on 19-08-2021
                        objParams[74] = new SqlParameter("@P_EXPENSE_LEDGER_NO", objRPB.EXPENSE_LEDGER_NO);//Added by gopal anthati on 19-08-2021

                        objParams[75] = new SqlParameter("@P_PROVIDER_TYPE", objRPB.PROVIDER_TYPE);
                        objParams[76] = new SqlParameter("@P_PAYEE_NATURE", objRPB.PAYEE_NATURE);
                        objParams[77] = new SqlParameter("@P_SUPPLIER_ID", objRPB.SUPPLIER_ID);


                        objParams[78] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[78].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_DIRECT_BILL_RAISING_PAYMENT", objParams, true);//PKG_ACC_SP_INS_UPD_DIRECT_BILL_RAISING

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            else if (ret.ToString().Equals("2"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            else //if (ret.ToString().Equals("1"))
                            {
                                objRPB.RAISE_PAY_NO = Convert.ToInt32(ret);
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }

                        }
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.AddRaisingPaymentBill-> " + ee.ToString());
                    }
                    return retStatus;
                }


            }
        }
    }
}
