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
            public class DeleteStoreInvoiceJournalController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //ADDE BY VIDISHA ON 08102020 TO GET INVOICE DETAILS

                public int GetInvoicePaymentDetails(int TRANNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ACC_STRINV_TRNO", TRANNO);                       
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_GET_STORE_PAYMENT_DEATILS_BY_INVOICE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.GetInvoicePaymentDetails-> " + ex.ToString());
                    }
                    return retStatus;//Convert.ToInt32(ret.ToString());
                }

                public DataSet GetInvoiceDetails(int PNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", PNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_STORE_INVOICE_DEATILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.GetInvoiceDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteTransactionInvoiceStore(int TRANNO, string COMPANY_CODE, string VCH_NO, string VCH_TYPE, string CODE_YEAR,int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ACC_STRINV_TRNO", TRANNO);
                        objParams[1] = new SqlParameter("@P_VCH_NO", VCH_NO);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", COMPANY_CODE);
                        objParams[3] = new SqlParameter("@P_CODE_YEAR", CODE_YEAR);
                        objParams[4] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[5] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_DELETE_STORE_INVOICE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.DeleteTransactionInvoiceStore-> " + ex.ToString());
                    }
                    return retStatus;// Convert.ToInt32(ret.ToString());
                }

                public DataSet GetMergeData(string PrefixText)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        string CompName = HttpContext.Current.Session["comp_code"].ToString();
                        //ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' order by ACC_CODE");
                        ds = objSQLHelper.ExecuteDataSet("select UPPER(PNAME) +'*'+CAST(PNO AS VARCHAR(12)) AS VENDOR_NAME from STORE_PARTY  where PNAME like '%" + PrefixText.Replace("'", "''") + "%' order by PNAME");

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.GetMergeData-> " + ee.ToString());
                    }
                    return ds;
                }

                public int DeleteTransactionPaymentStore(int TRANNO, string COMPANY_CODE, string VCH_NO, string VCH_TYPE, string CODE_YEAR, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ACC_STRINV_PAYMENT_TRNO", TRANNO);
                        objParams[1] = new SqlParameter("@P_VCH_NO", VCH_NO);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", COMPANY_CODE);
                        objParams[3] = new SqlParameter("@P_CODE_YEAR", CODE_YEAR);
                        objParams[4] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[5] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_DELETE_STORE_PAYMENT_TRANSACTION", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.DeleteTransactionPaymentStore-> " + ex.ToString());
                    }
                    return retStatus;//Convert.ToInt32(ret.ToString());
                }

                public DataSet GetPaymentDetails(int PNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", PNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_STORE_PAYMENT_DEATILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStoreInvoiceJournalController.GetPaymentDetails-> " + ex.ToString());
                    }
                    return ds;
                }


            }

        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS  
}//END: IITMSVOUCHER_NO", objParty.VOUCHER_NO);

