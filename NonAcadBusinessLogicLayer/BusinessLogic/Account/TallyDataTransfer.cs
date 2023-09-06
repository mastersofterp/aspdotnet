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
        namespace BusinessLayer.BusinessLogic
        {
            public class TallyDataTransfer
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public TallyDataTransfer()
                {
                }


                public TallyDataTransfer(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddTallyVocher(DataTable XMLTALLYVOUCHERXML, DataTable XMLBANKALLOCATIONS, DataTable XMLALLLEDGERENTRIES, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        if (!XMLALLLEDGERENTRIES.Columns.Contains("NARRATION"))
                        {
                            DataColumn Col = XMLALLLEDGERENTRIES.Columns.Add("NARRATION", System.Type.GetType("System.String"));
                            Col.SetOrdinal(1);
                        }

                        //Add New MainGroup Group
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ACC_TALLYVOUCHERXML", XMLTALLYVOUCHERXML);
                        objParams[1] = new SqlParameter("@P_ACC_TALLYBANKALLOCATIONSXML", XMLBANKALLOCATIONS);
                        objParams[2] = new SqlParameter("@P_ACC_TALLY_ALLLEDGERENTRIESXML", XMLALLLEDGERENTRIES);

                        objParams[3] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_DATA_FROM_TALLY", objParams, true);
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
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddTallyLedger(DataTable TALLYLEDGER, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACC_TALLY_LEDGER_MAPPING", TALLYLEDGER);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", code_year);

                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_ADD_TALLY_LEDGER_MAPPING", objParams, true);
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
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
