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
            public class AccountConfigurationController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public AccountConfigurationController()
                {
                    //blank Constructor
                }

                public AccountConfigurationController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }






               
               // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddUpdateConfiguration(AccountConfiguration objMG,string Company_Code, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_CONFIGID", objMG.ConfigId);
                        objParams[1] = new SqlParameter("@P_CONFIGDESC", objMG.ConfiguraionDesc);
                        objParams[2] = new SqlParameter("@P_PARAMETER", objMG.ConfigValue);
                        objParams[3] = new SqlParameter("@P_COMPANY_CODE", Company_Code);
                        objParams[4] = new SqlParameter("@P_YEAR", code_year);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REF_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1")) 
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2")) 
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }                        

                    }
                    catch (Exception ee)
                    {
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                
                public DataTableReader GetConfigurationDetails(int ConfigId,object code_year)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_CONFIGID", ConfigId);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_CONFIGURATION", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                       // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetConfigurationDetails-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetConfigurationDetails-> " + ee.ToString());
                    }
                    return dtr;
                }
                public DataSet GetVoucherNo1(object code_year, string VCH_TYPE, string TRANDATE)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[2] = new SqlParameter("@TRANDATE", TRANDATE);
                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACC_SP_RET_MAX_VOUCHERNO_VCH_TYPEWISE]", objParams);

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return ds;
                }
                public DataSet GetMaxVoucherNo1(object code_year)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_MAX_VOUCHERNO", objParams);

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return ds;
                }
                public DataTableReader GetMaxVoucherNo(object code_year, string TRANDATE)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_TRANSACTION_DATE", TRANDATE);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_MAX_VOUCHERNO", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return dtr;
                }

                public DataTableReader GetVoucherNo(object code_year, string VCH_TYPE,string TRANDATE)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[2] = new SqlParameter("@TRANDATE", TRANDATE);
                        dtr = objSQLHelper.ExecuteDataSetSP("[PKG_ACC_SP_RET_MAX_VOUCHERNO_VCH_TYPEWISE]", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return dtr;
                }

                public DataTableReader GetMaxVoucherNoSegregated(object code_year, string VoucherType)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VOUCHER_TYPE", VoucherType);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_MAX_VOUCHERNO_STRING", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                       // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNoSegregated-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNoSegregated-> " + ee.ToString());
                    }
                    return dtr;
                }

                public int AddSignature(AccountConfiguration objAC)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SIGN1", objAC.Signature1);
                        objParams[1] = new SqlParameter("@P_SIGN2", objAC.Signature2);
                        objParams[2] = new SqlParameter("@P_SIGN3", objAC.Signature3);
                        objParams[3] = new SqlParameter("@P_SIGN4", objAC.Signature4);
                        objParams[4] = new SqlParameter("@P_SIGN5", objAC.Signature5);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CONFIG_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ee)
                    {
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNoSegregated-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddSignature-> " + ee.ToString());
                    }
                    return retStatus;
                }




                //added by tanu 26/02/2022  for get voucher number in accounting voucher page
                public DataTableReader GetTempMaxVoucherNo(object code_year, string TRANDATE)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_TRANSACTION_DATE", TRANDATE);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_TEMP_MAX_VOUCHERNO", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetTempMaxVoucherNo-> " + ee.ToString());
                    }
                    return dtr;
                }

                public DataTableReader GetTempVoucherNo(object code_year, string VCH_TYPE, string TRANDATE)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[2] = new SqlParameter("@TRANDATE", TRANDATE);
                        dtr = objSQLHelper.ExecuteDataSetSP("[PKG_ACC_SP_RET_TEMP_MAX_VOUCHERNO_VCH_TYPEWISE]", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ee)
                    {
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.GetMaxVoucherNo-> " + ee.ToString());
                    }
                    return dtr;
                }

                //Added BY Akshay Dixit On 19-10-2022 To Insert/Update AO FO.
                public int AddUpdateAOFOConfiguration(int AO, int FO, char IsTemp)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_AO", AO);
                        objParams[1] = new SqlParameter("@P_FO", FO);
                        objParams[2] = new SqlParameter("@P_IsTemp", IsTemp);                    
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_AOFO_INSERT_UPDATE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

             }


        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS