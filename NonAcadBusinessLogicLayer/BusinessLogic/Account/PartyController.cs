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
        { /// <summary>
            /// This PartyController is used to control Acc_Section table.
            /// </summary>
            public class PartyController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                //private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public PartyController()
                {

                }

                public PartyController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public int AddParty(Party objParty, Trans objTrans, string code_year, int CCApplicable)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New Party
                        objParams = new SqlParameter[28];
                        //Acc_Party
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTY_NAME", objParty.Party_Name);
                        objParams[2] = new SqlParameter("@P_PARTY_ADDRESS", objParty.Party_Address);
                        objParams[3] = new SqlParameter("@P_CREDIT", objParty.Credit);
                        objParams[4] = new SqlParameter("@P_DEBIT", objParty.Debit);
                        objParams[5] = new SqlParameter("@P_PAYMENT_TYPE_NO", objParty.Payment_Type_No);
                        objParams[6] = new SqlParameter("@P_STATUS", objParty.Status);
                        objParams[7] = new SqlParameter("@P_OPBALANCE", objParty.OpeningBalance);
                        objParams[8] = new SqlParameter("@P_MGRP_NO", objParty.Mgrp_No);
                        objParams[9] = new SqlParameter("@P_RP_NO", objParty.RP_No);
                        objParams[10] = new SqlParameter("@P_PGRP_NO", objParty.PGrp_No);
                        objParams[11] = new SqlParameter("@P_ACC_CODE", objParty.Account_Code);
                        objParams[12] = new SqlParameter("@P_FREEZE", objParty.Freeze);
                        objParams[13] = new SqlParameter("@P_STOPOB", objParty.StopOB);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objParty.College_Code);
                        //Acc_Trans
                        objParams[15] = new SqlParameter("@P_TRANSACTION_DATE", objTrans.Transaction_Date);
                        objParams[16] = new SqlParameter("@P_TRANSACTION_TYPE", objTrans.Transaction_Type);
                        objParams[17] = new SqlParameter("@P_TRAN", objTrans.Tran);
                        //Acc party Contact
                        objParams[18] = new SqlParameter("@P_PARTY_CONTACT", objParty.Party_Contact);
                        objParams[19] = new SqlParameter("@P_BANK_ACCOUNT_NO", objParty.Bank_Account_No);
                        objParams[20] = new SqlParameter("@P_SETDEFAULT", objParty.SetDefault);
                        objParams[21] = new SqlParameter("@P_ISCCAPPLICABLE", CCApplicable);
                        objParams[22] = new SqlParameter("@ISBudgetHead", objParty.IsBudgetHead);
                        //Acc Party
                        objParams[23] = new SqlParameter("@P_TINNO", objParty.TINNO);
                        objParams[24] = new SqlParameter("@P_PANNO", objParty.PANNO);

                        objParams[25] = new SqlParameter("@P_GSTNO", objParty.GSTNo);
                        //ADDDED BY PAWAN NIKHARE
                        objParams[26] = new SqlParameter("@P_Work_Nature", objParty.Work_Nature);
                        objParams[27] = new SqlParameter("@P_PARTY_NO", SqlDbType.Int);


                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_PARTY", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PartyController.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParty(Party objParty, Trans objTrans, string code_year, int CCApplicable)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update New Party
                        objParams = new SqlParameter[28];
                        //Acc_Party
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", objParty.Party_No);
                        objParams[2] = new SqlParameter("@P_PARTY_NAME", objParty.Party_Name);
                        objParams[3] = new SqlParameter("@P_PARTY_ADDRESS", objParty.Party_Address);
                        objParams[4] = new SqlParameter("@P_CREDIT", objParty.Credit);
                        objParams[5] = new SqlParameter("@P_DEBIT", objParty.Debit);
                        objParams[6] = new SqlParameter("@P_PAYMENT_TYPE_NO", objParty.Payment_Type_No);
                        objParams[7] = new SqlParameter("@P_STATUS", objParty.Status);
                        objParams[8] = new SqlParameter("@P_OPBALANCE", objParty.OpeningBalance);
                        objParams[9] = new SqlParameter("@P_MGRP_NO", objParty.Mgrp_No);
                        objParams[10] = new SqlParameter("@P_RP_NO", objParty.RP_No);
                        objParams[11] = new SqlParameter("@P_PGRP_NO", objParty.PGrp_No);
                        objParams[12] = new SqlParameter("@P_ACC_CODE", objParty.Account_Code);
                        objParams[13] = new SqlParameter("@P_FREEZE", objParty.Freeze);
                        objParams[14] = new SqlParameter("@P_STOPOB", objParty.StopOB);
                        objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objParty.College_Code);
                        //Acc_Trans
                        objParams[16] = new SqlParameter("@P_TRANSACTION_DATE", objTrans.Transaction_Date);
                        objParams[17] = new SqlParameter("@P_TRANSACTION_TYPE", objTrans.Transaction_Type);
                        objParams[18] = new SqlParameter("@P_TRAN", objTrans.Tran);
                        objParams[19] = new SqlParameter("@P_PARTY_CONTACT", objParty.Party_Contact);
                        objParams[20] = new SqlParameter("@P_BANK_ACCOUNT_NO", objParty.Bank_Account_No);
                        objParams[21] = new SqlParameter("@P_SETDEFAULT", objParty.SetDefault);
                        objParams[22] = new SqlParameter("@P_ISCCAPPLICABLE", CCApplicable);
                        //Acc Party
                        objParams[23] = new SqlParameter("@P_TINNO", objParty.TINNO);
                        objParams[24] = new SqlParameter("@P_PANNO", objParty.PANNO);
                        objParams[25] = new SqlParameter("@ISBudgetHead", objParty.IsBudgetHead);
                        objParams[26] = new SqlParameter("@P_Work_Nature", objParty.Work_Nature);
                        objParams[27] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_UPD_PARTY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PartyController.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetPartyByPartyNo(int party_no, string code_year)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", party_no);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_PARTY", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PartyController.GetPartyByPartyNo-> " + ex.ToString());
                    }
                    return dtr;
                }


                public int SetBankLedgerLinking(Party objParty, string Code_Year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CODEYEAR", Code_Year);
                        objParams[1] = new SqlParameter("@P_PARTYNO", objParty.Party_No);
                        objParams[2] = new SqlParameter("@P_ACNO", objParty.Bank_Account_No);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_LEDGER_BANK_LINKING", objParams, true);

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

                    }
                    return retStatus;
                }


                public DataSet GetTotalOpeningBalances(string code_year)
                {
                    DataSet dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_TOTAL_OPENING_BALANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PartyController.GetTotalOpeningBalances-> " + ex.ToString());
                    }
                    return dtr;
                }
                public DataSet GetPartyAccountCode(string accountCode, string colCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_ACCOUNT_CODE", accountCode);
                        //HttpContext.Current.Session["DataBase"].ToString()
                        objParams[1] = new SqlParameter("@P_College_Code", colCode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_ACCOUNT_CODE", objParams);

                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                public DataSet getParentNo(string compcode, string mgrp_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COMPCODE", compcode);
                        //HttpContext.Current.Session["DataBase"].ToString()
                        objParams[1] = new SqlParameter("@P_MGRPNO", mgrp_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_PARENT_NO", objParams);

                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                //for Multiple Ledger Insertion

                public int AddPartyWithMultipleCompanies(Party objParty, Trans objTrans, string code_year, int CCApplicable, string OPBal, string Status_ADD, string CCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New Party
                        objParams = new SqlParameter[30];
                        //Acc_Party
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR_ADD", CCode);
                        objParams[2] = new SqlParameter("@P_PARTY_NAME", objParty.Party_Name);
                        objParams[3] = new SqlParameter("@P_PARTY_ADDRESS", objParty.Party_Address);
                        objParams[4] = new SqlParameter("@P_CREDIT", objParty.Credit);
                        objParams[5] = new SqlParameter("@P_DEBIT", objParty.Debit);
                        objParams[6] = new SqlParameter("@P_PAYMENT_TYPE_NO", objParty.Payment_Type_No);
                        objParams[7] = new SqlParameter("@P_STATUS", objParty.Status);
                        objParams[8] = new SqlParameter("@P_STATUS_ADD", Status_ADD);
                        objParams[9] = new SqlParameter("@P_OPBALANCE", objParty.OpeningBalance);
                        objParams[10] = new SqlParameter("@P_OPBALANCE_ADD", OPBal);
                        objParams[11] = new SqlParameter("@P_MGRP_NO", objParty.Mgrp_No);
                        objParams[12] = new SqlParameter("@P_RP_NO", objParty.RP_No);
                        objParams[13] = new SqlParameter("@P_PGRP_NO", objParty.PGrp_No);
                        objParams[14] = new SqlParameter("@P_ACC_CODE", objParty.Account_Code);
                        objParams[15] = new SqlParameter("@P_FREEZE", objParty.Freeze);
                        objParams[16] = new SqlParameter("@P_STOPOB", objParty.StopOB);
                        objParams[17] = new SqlParameter("@ISBudgetHead", Convert.ToInt32(objParty.IsBudgetHead));
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objParty.College_Code);
                        //Acc_Trans
                        objParams[19] = new SqlParameter("@P_TRANSACTION_DATE", objTrans.Transaction_Date);
                        objParams[20] = new SqlParameter("@P_TRANSACTION_TYPE", objTrans.Transaction_Type);
                        objParams[21] = new SqlParameter("@P_TRAN", objTrans.Tran);
                        //Acc party Contact
                        objParams[22] = new SqlParameter("@P_PARTY_CONTACT", objParty.Party_Contact);
                        objParams[23] = new SqlParameter("@P_BANK_ACCOUNT_NO", objParty.Bank_Account_No);
                        objParams[24] = new SqlParameter("@P_SETDEFAULT", objParty.SetDefault);
                        objParams[25] = new SqlParameter("@P_ISCCAPPLICABLE", CCApplicable);
                        //Acc Party
                        objParams[26] = new SqlParameter("@P_TINNO", objParty.TINNO);
                        objParams[27] = new SqlParameter("@P_PANNO", objParty.PANNO);
                        objParams[28] = new SqlParameter("@P_Work_Nature", objParty.Work_Nature);
                        objParams[29] = new SqlParameter("@P_PARTY_NO", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_ACC_SP_INS_PARTY_WITH_OTHER_COMPANY]", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PartyController.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }




            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS