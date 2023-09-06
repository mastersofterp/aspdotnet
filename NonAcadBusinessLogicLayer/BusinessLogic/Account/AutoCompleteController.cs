using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
namespace IITMS
{
    namespace UAIMS
    {
        public class AutoCompleteController
        {
            public string _client_constr = string.Empty;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

            public AutoCompleteController()
            {
                //blank Constructor
            }

            public AutoCompleteController(string DbUserName, string DbPassword, String DataBase)
            {
                _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
            }

            public DataSet GetBankMasterData(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    ds = objSQLHelper.ExecuteDataSet("select CAST(bankno as VARCHAR(12))+'*'+CAST(bankname as VARCHAR(MAX)) bankname from ACC_BANK_DETAIL where bankname like '%" + PrefixText.Replace("'", "''") + "%'");

                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetChequePrintingData(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME)+'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME FROM ACC_" + CompName + "_PARTY where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('2') order by ACC_CODE");

                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetCostCategory(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select CAST(Cat_ID AS VARCHAR(12))+'*'+Category AS Category FROM ACC_" + CompName + "_CostCategory where Category like '%" + PrefixText.Replace("'", "''") + "%' and Cat_ID <> 0 order by Cat_ID");

                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetLedgerReport(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO  NOT IN ('1','2') order by ACC_CODE");
                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetMergeData(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' order by ACC_CODE");

                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            //Get Ledger head for Raising Payment Bill
            public DataSet GetFDRLedger(string PrefixText,string comp_code)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = comp_code.ToString();
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

            public DataSet GetFD_BankLedger(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["FDR_Comp_Code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetAgainstAccLedger(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

            public DataSet GetAccountEntryCashBank(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    Common objCommon = new Common();
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    string SetParameter = objCommon.LookUp("ACC_" + CompName + "_CONFIG", "PARAMETER", "CONFIGDESC='ALLOW CASH ACCOUNTS IN JOURNALS'");

                    if (HttpContext.Current.Session["BANKCASHCONTRA"] == "C")
                        ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE");
                    else if ((HttpContext.Current.Session["BANKCASHCONTRA"] == "J") && SetParameter== "Y") 
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%'  order by ACC_CODE");
                    else
                        ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' and PAYMENT_TYPE_NO  NOT IN ('1','2') order by ACC_CODE");
                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }


            public DataSet GetBankData(string PrefixText)
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    string CompName = HttpContext.Current.Session["comp_code"].ToString();
                    ds = objSQLHelper.ExecuteDataSet("select UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME from ACC_" + CompName + "_PARTY  where PARTY_NAME like '%" + PrefixText.Replace("'", "''") + "%' order by ACC_CODE");

                }
                catch (Exception ee)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                }
                return ds;
            }

        }
    }
}
