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
            public class CashBankGroupController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public CashBankGroupController()
                {
                }
                public CashBankGroupController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddUpdateCashBankGroupDetails(object code_year, string PartyName, string PartyId, string isAdd)
                {
                    int retStatus=Convert.ToInt32(CustomStatus.Others);

                    
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_NAME", PartyName);
                        objParams[2] = new SqlParameter("@P_PARTY_IDS ", PartyId);
                        objParams[3] = new SqlParameter("@P_IS_ADD", isAdd);
                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        //@P_STATUS
                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PARENT_CHILD_COMMON_INSERT_UPDATE", objParams, true));
                        if (ret == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved); 
                        
                        }
                        else if (ret == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                        else if (ret == -1001)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);

                        }
                       else if (ret == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                        }


                        


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBankGroupController.AddcashBankGroupDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Developed by :Prakash Radhwani
                /// Purpose      :To get the Bank Details for Bank Reconcilation.
                /// </summary>
                public DataSet GetBankReConcilation_ForReport(string cyear, string ledger, string Fdate, string Tdate)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", cyear);
                        objParams[1] = new SqlParameter("@P_LEDGER", ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE ",Fdate);
                        objParams[3] = new SqlParameter("@P_TODATE", Tdate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_BANK_RECONCILE_STATEMENT", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BankController.GetBank_ForReport-> " + ee.ToString());
                    }
                }
            }
            

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS