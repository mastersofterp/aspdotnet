using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class storeIntegrationController
            {
                Common objCommon = new Common();
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public storeIntegrationController()
                {

                }

                public DataSet getVendorName(string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select SP.PNO,sp.PNAME+' ('+spc.PCNAME+') ' as VENDORNAME from store_party SP inner join STORE_PARTY_CATEGORY SPC on (sp.PCNO=spc.PCNO)");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public void DELETEFROMSTOREMAPPINGTABLE(string COMPCODE)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from ACC_" + COMPCODE + "_STORE_MAPPING");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                }

                public int SetStoreMapping(storeIntegration objStore, string Code_Year)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", Code_Year);
                        objParams[1] = new SqlParameter("@P_PNO", objStore.PNO);
                        objParams[2] = new SqlParameter("@P_PNAME", objStore.PNAME);
                        objParams[3] = new SqlParameter("@P_PARTY_NO", objStore.PARTY_NO);
                        objParams[4] = new SqlParameter("@P_PARTY_NO_EXPE", objStore.PARTY_NO_EXPE);
                        objSQLHelper.ExecuteNonQuerySP("PKG_STORE_MAPPING", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = 0;
                    }
                    return retStatus;
                }


                public DataSet getInvoiceNo(string conn, storeIntegration objStore)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select INVTRNO,INVNO,NETAMT from STORE_INVOICE WHERE PNO=" + objStore.PNO + " AND INVDT BETWEEN convert(datetime,'" + objStore.FromDate + "',112) AND convert(datetime,'" + objStore.ToDate + "',112) AND ISPASS_ORDER is not null and ISTRANSFER is null");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet getPassOrderDetail(string conn, string fromDate, string toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select SPO.PASSORD_TRNO,SPO.PASSORDNO,convert(varchar(10),SPO.PASSORD_DATE,105) as PASSORD_DATE,SI.INVNO,convert(varchar(10),SI.INVDT,105) as INVDT,SPO.PAYMENT_AMOUNT,SP.PNAME from store_pass_order SPO inner join STORE_INVOICE SI on (SPO.INVTRNO=SI.INVTRNO) inner join STORE_PARTY SP on (SPO.PNO=SP.PNO) where SPO.PAYMENT_STATUS='N' and SPO.PASSORD_DATE between '" + fromDate.ToString() + "' and '" + toDate.ToString() + "' order by SPO.PASSORD_DATE desc");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet getPaidPassOrderDetail(string conn, string fromDate, string toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select SPO.PASSORD_TRNO,SPO.PASSORDNO,convert(varchar(10),SPO.PASSORD_DATE,105) as PASSORD_DATE,SI.INVNO,convert(varchar(10),SI.INVDT,105) as INVDT,SPO.PAYMENT_AMOUNT,SP.PNAME,SPO.VCHNO from store_pass_order SPO inner join STORE_INVOICE SI on (SPO.INVTRNO=SI.INVTRNO) inner join STORE_PARTY SP on (SPO.PNO=SP.PNO) where SPO.PAYMENT_STATUS='Y' and SPO.PASSORD_DATE between '" + fromDate.ToString() + "' and '" + toDate.ToString() + "' order by SPO.PASSORD_DATE desc");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public int MakeJVEntry(storeIntegration objStore, string Code_Year)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Code_Year);
                        objParams[1] = new SqlParameter("@P_PNO", objStore.PNO);
                        objParams[2] = new SqlParameter("@P_NETAMOUNT", objStore.Net_Amount);

                        objParams[3] = new SqlParameter("@P_INVTRNO", objStore.INVTRNO);
                        objParams[4] = new SqlParameter("@P_DATABASE", objStore.DATABASE);

                        objParams[5] = new SqlParameter("@P_COLLEGECODE", objStore.College_code);
                        objParams[6] = new SqlParameter("@P_UA_NO", objStore.UA_NO);
                        objParams[7] = new SqlParameter("@P_OUT", "0");
                        objParams[7].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_STORE_JV", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = 0;
                    }
                    return retStatus;
                }

                public int setPaymentStatus(int PASSORD_TRNO, string VCHNO, string DATABASE)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PASSORD_TRNO", PASSORD_TRNO);
                        objParams[1] = new SqlParameter("@P_VCHNO", VCHNO);
                        objParams[2] = new SqlParameter("@P_DATABASE", DATABASE);

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_STORE_SET_PAYMENT_STATUS", objParams, true);
                        retStatus = 1;
                    }
                    catch (Exception ex)
                    {
                        retStatus = 0;
                    }
                    return retStatus;
                }

                public DataSet getVocherNo(string conn)
                {
                    DataSet ds=new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select VCHNO from store_pass_order where VCHNO is not NULL");
                    }
                        catch (Exception ex)
                    {
                       
                    }
                    return ds;

                }
            }
        }
    }
}
