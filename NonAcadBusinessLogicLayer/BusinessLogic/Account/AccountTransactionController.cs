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
            /// <summary>D:\_D\code\SVN CODE - 01\17.02.2020\BusinessLogicLayer\bin\Debug\BusinessLogicLayer.dll
            /// This AccountTransactionController is used to control Transaction table.
            /// </summary>
            public class AccountTransactionController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public AccountTransactionController()
                {
                }
                public AccountTransactionController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                #region Bank Master
                public int AddBankName(string bankName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        //objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        objParams[0] = new SqlParameter("@P_BANKNAME", bankName);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_MASTER_INSERT", objParams, true);
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


                public int AddTaxName(string TaxName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TAX_NAME", TaxName);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TAX_MASTER_INSERT", objParams, true);
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
                public int AddAccountName(string AccountName, string Comp_COde, bool Istransfer)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        //objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        objParams[0] = new SqlParameter("@P_ACCOUNTNAME", AccountName);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_COde);
                        objParams[2] = new SqlParameter("@P_ISTransfer", Istransfer.ToString());
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACC_ABSTRACT_ACCOUNT_INSERT]", objParams, true);
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

                public DataSet GetAllBanks()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        //objParams[0] = new SqlParameter("@P_BANKNAME", bankName);
                        //objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BANK_MASTER_GETALL", objParams);
                        //if (ret != null)
                        //{
                        //    if (ret.ToString().Equals("-99"))
                        //        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //    else

                        //        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }
                public DataSet GetAllTaxes()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        //objParams[0] = new SqlParameter("@P_BANKNAME", bankName);
                        //objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BANK_MASTER_GETALL", objParams);
                        //if (ret != null)
                        //{
                        //    if (ret.ToString().Equals("-99"))
                        //        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //    else

                        //        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                public DataSet GetAbstractBillAccounts(string comp_code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", comp_code);
                        //objParams[0] = new SqlParameter("@P_BANKNAME", bankName);
                        //objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ABSTRACT_BILL_ACCOUNT", objParams);
                        //if (ret != null)
                        //{
                        //    if (ret.ToString().Equals("-99"))
                        //        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //    else

                        //        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

                public DataSet GetAccountByAccountNo(int AccountId, string Comp_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ACCOUNTNO", AccountId);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        //objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ACCOUNT_GET_BY_ACCOUNTNO", objParams);
                        // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_MASTER_GET_BY_BANKNO", objParams, true);
                        //if (ret != null)
                        //{
                        //    if (ret.ToString().Equals("-99"))
                        //        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //    else

                        //        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }

                public DataSet GetBankByBankNo(int bankId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        //objParams[0] = new SqlParameter("@P_BANKNAME", bankName);
                        //objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BANK_MASTER_GET_BY_BANKNO", objParams);
                        // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_MASTER_GET_BY_BANKNO", objParams, true);
                        //if (ret != null)
                        //{
                        //    if (ret.ToString().Equals("-99"))
                        //        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //    else

                        //        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }

                public int UpdatBankMaster(int bankId, string Bankname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BANKNO", bankId);
                        objParams[1] = new SqlParameter("@P_BANKNAME", Bankname);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_MASTER_UPDATE", objParams, true);
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

                    }
                    return retStatus;
                }
                public int UpdatTaxMaster(int TaxId, string Taxname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TAX_ID", TaxId);
                        objParams[1] = new SqlParameter("@P_TAX_NAME", Taxname);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TAX_MASTER_UPDATE", objParams, true);
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

                    }
                    return retStatus;
                }
                public int UpdatAbstractAccount(int AccountId, string Accountname, string Comp_Code, bool Istransfer)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ACCID", AccountId);
                        objParams[1] = new SqlParameter("@P_ACCOUNTNAME", Accountname);
                        objParams[2] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[3] = new SqlParameter("@P_ISTransfer", Istransfer.ToString());
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACC_UPDATE_ABSTRACT_BILL_ACCOUNT]", objParams, true);
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

                    }
                    return retStatus;
                }
                #endregion

                public int AddBankDetail(BankMaster obank)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_BANKNO", obank.BANKNO);
                        objParams[1] = new SqlParameter("@P_BANKNAME", obank.BANKNAME);
                        objParams[2] = new SqlParameter("@P_CHECKHEIGHT", obank.CHECKHEIGHT);
                        objParams[3] = new SqlParameter("@P_CHECKWIDTH", obank.CHECKWIDTH);
                        objParams[4] = new SqlParameter("@P_PARTYTOP", obank.PARTYTOP);
                        objParams[5] = new SqlParameter("@P_PARTYLEFT", obank.PARTYLEFT);
                        objParams[6] = new SqlParameter("@P_AMTTOP", obank.AMTTOP);
                        objParams[7] = new SqlParameter("@P_AMTWIDTH", obank.AMTWIDTH);
                        objParams[8] = new SqlParameter("@P_AMTLEFT", obank.AMTLEFT);
                        objParams[9] = new SqlParameter("@P_WAMTTOP", obank.WAMTTOP);
                        objParams[10] = new SqlParameter("@P_WAMTWIDTH", obank.WAMTWIDTH);
                        objParams[11] = new SqlParameter("@P_WAMTLEFT", obank.WAMTLEFT);
                        objParams[12] = new SqlParameter("@P_CHECKTOP", obank.CHECKTOP);
                        objParams[13] = new SqlParameter("@P_CHECKLEFT", obank.CHECKLEFT);
                        objParams[14] = new SqlParameter("@P_CHECKRIGHT", obank.CHECKRIGHT);
                        objParams[15] = new SqlParameter("@P_CKDTTOP", obank.CKDTTOP);
                        objParams[16] = new SqlParameter("@P_CKDTLEFT", obank.CKDTLEFT);
                        objParams[17] = new SqlParameter("@P_CKDTWIDTH", obank.CKDTWIDTH);
                        objParams[18] = new SqlParameter("@P_PARTYWIDTH", obank.PARTYWIDTH);
                        objParams[19] = new SqlParameter("@P_STAMP1TOP", obank.STAMP1TOP);
                        objParams[20] = new SqlParameter("@P_STAMP1LEFT", obank.STAMP1LEFT);
                        objParams[21] = new SqlParameter("@P_STAMP1WIDTH", obank.STAMP1WIDTH);
                        objParams[22] = new SqlParameter("@P_STAMP2TOP", obank.STAMP2TOP);
                        objParams[23] = new SqlParameter("@P_STAMP2LEFT", obank.STAMP2LEFT);
                        objParams[24] = new SqlParameter("@P_STAMP2WIDTH", obank.STAMP2WIDTH);
                        objParams[25] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_DETAIL_INSERT", objParams, true);
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
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddBankDetail-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddBankDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAccountDetails(Account_Master oAcc, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_TRNO", oAcc.TRNO);
                        objParams[1] = new SqlParameter("@P_BNO", oAcc.BNO);
                        objParams[2] = new SqlParameter("@P_ACCNO", oAcc.ACCNO);
                        objParams[3] = new SqlParameter("@P_ACCNAME", oAcc.ACCNAME);
                        objParams[4] = new SqlParameter("@P_SRNO", oAcc.SRNO);
                        objParams[5] = new SqlParameter("@P_CFRNO", oAcc.CFRNO);
                        objParams[6] = new SqlParameter("@P_CTONO", oAcc.CTONO);
                        objParams[7] = new SqlParameter("@P_CCURNO", oAcc.CCURNO);
                        objParams[8] = new SqlParameter("@P_CISSUEDT", oAcc.CISSUEDT.ToString("dd-MMM-yyyy"));
                        objParams[9] = new SqlParameter("@P_STATUS", oAcc.STATUS);
                        objParams[10] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_ACCOUNT_MASTER_INSERT", objParams, true);
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
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public int AddVoucherPhotoTemp(VoucherPhoto oVch, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", oVch.Idno);
                        objParams[1] = new SqlParameter("@P_PHOTOPATH", oVch.PhotoPath);
                        objParams[2] = new SqlParameter("@P_PHOTOSIZE", oVch.PhotoSize);
                        objParams[3] = new SqlParameter("@P_PHOTO", oVch.Photo1);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", oVch.CollegeCode);
                        objParams[5] = new SqlParameter("@P_VOUCHER_NO", oVch.VoucherNo);
                        objParams[6] = new SqlParameter("@P_COMP_CODE", CompanyCode);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_PHOTOS_TEMP_INSERT", objParams, true);
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
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddVoucherPhotoTemp-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddVoucherPhotoTemp-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddVoucherPhoto(VoucherPhoto oVch, String CompanyCode, string UPDATE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", oVch.Idno);
                        objParams[1] = new SqlParameter("@P_PHOTOPATH", oVch.PhotoPath);
                        objParams[2] = new SqlParameter("@P_PHOTOSIZE", oVch.PhotoSize);
                        objParams[3] = new SqlParameter("@P_PHOTO", oVch.Photo1);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", oVch.CollegeCode);
                        objParams[5] = new SqlParameter("@P_VOUCHER_NO", oVch.VoucherNo);
                        objParams[6] = new SqlParameter("@P_COMP_CODE", CompanyCode);
                        objParams[7] = new SqlParameter("@P_UPDATE", UPDATE);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_PHOTOS_INSERT", objParams, true);
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
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddVoucherPhoto-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddVoucherPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddChequeEntryDetails(ChequePrintMaster cpm, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_PARTYNAME", cpm.PARTYNAME);
                        objParams[1] = new SqlParameter("@P_AMOUNT", cpm.AMOUNT);
                        objParams[2] = new SqlParameter("@P_CHECKDT", cpm.CHECKDT);
                        objParams[3] = new SqlParameter("@P_CHECKNO", cpm.CHECKNO);
                        objParams[4] = new SqlParameter("@P_BANKNO", cpm.BANKNO);
                        objParams[5] = new SqlParameter("@P_CTRNO", cpm.CTRNO);
                        objParams[6] = new SqlParameter("@P_REMARK", cpm.REMARK);
                        objParams[7] = new SqlParameter("@P_ACCNO", cpm.ACCNO);
                        objParams[8] = new SqlParameter("@P_BILLNO", cpm.BILLNO);
                        objParams[9] = new SqlParameter("@P_BILLDT", cpm.BILLDT);
                        objParams[10] = new SqlParameter("@P_CANCEL", cpm.CANCEL);
                        objParams[11] = new SqlParameter("@P_USERNAME", cpm.USERNAME);
                        objParams[12] = new SqlParameter("@P_VNO", cpm.VNO);
                        objParams[13] = new SqlParameter("@P_VDT", cpm.VDT);
                        objParams[14] = new SqlParameter("@P_ADDRESS", cpm.ADDRESS);
                        objParams[15] = new SqlParameter("@P_COPY1", cpm.COPY1);
                        objParams[16] = new SqlParameter("@P_COPY2", cpm.COPY2);
                        objParams[17] = new SqlParameter("@P_COPY3", cpm.COPY3);
                        objParams[18] = new SqlParameter("@P_REASON1", cpm.REASON1);
                        objParams[19] = new SqlParameter("@P_REASON2", cpm.REASON2);
                        objParams[20] = new SqlParameter("@P_REASON3", cpm.REASON3);
                        objParams[21] = new SqlParameter("@P_PRINTSTATUS", cpm.PRINTSTATUS);
                        objParams[22] = new SqlParameter("@P_DEDSTATUS", cpm.DEDSTATUS);
                        objParams[23] = new SqlParameter("@P_DEDAMT", cpm.DEDAMT);
                        objParams[24] = new SqlParameter("@P_DEPT", cpm.DEPT);
                        objParams[25] = new SqlParameter("@P_ACCNAME", cpm.ACCNAME);
                        objParams[26] = new SqlParameter("@P_STAMP", cpm.STAMP);
                        objParams[27] = new SqlParameter("@P_TRNO", cpm.TRNO);
                        objParams[28] = new SqlParameter("@P_PARTYACCOUNTNO", cpm.PARTYACCOUNTNO);
                        objParams[29] = new SqlParameter("@P_PARTYNO", cpm.PARTYNO);
                        objParams[30] = new SqlParameter("@P_COMPANY_CODE", cpm.COMPANY_CODE);
                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CHEQUE_PRINT_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(ret);// (CustomSatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddChequeEntryDetails_New(ChequePrintMaster cpm, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_PARTYNAME", cpm.PARTYNAME);
                        objParams[1] = new SqlParameter("@P_AMOUNT", cpm.AMOUNT);
                        objParams[2] = new SqlParameter("@P_CHECKDT", cpm.CHECKDT);
                        objParams[3] = new SqlParameter("@P_CHECKNO", cpm.CHECKNO);
                        objParams[4] = new SqlParameter("@P_BANKNO", cpm.BANKNO);

                        objParams[5] = new SqlParameter("@P_USERNAME", cpm.USERNAME);
                        objParams[6] = new SqlParameter("@P_VNO", cpm.VNO);
                        objParams[7] = new SqlParameter("@P_VDT", cpm.VDT);

                        //objParams[8] = new SqlParameter("@P_REASON1", cpm.REASON1);

                        objParams[8] = new SqlParameter("@P_ACCNAME", cpm.ACCNAME);
                        objParams[9] = new SqlParameter("@P_STAMP", cpm.STAMP);
                        objParams[10] = new SqlParameter("@P_PARTYNO", cpm.PARTYNO);
                        objParams[11] = new SqlParameter("@P_COMPANY_CODE", cpm.COMPANY_CODE);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CHEQUE_PRINT_INSERT_NEW", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(ret);// (CustomSatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteChequeEntryDetails_New(string Vno, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from acc_" + CompanyCode + "_CHECK_PRINT  where VNO=" + Vno.ToString());

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateChequeEntryDetails(ChequePrintMaster cpm, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[32];

                        objParams[0] = new SqlParameter("@P_PARTYNAME", cpm.PARTYNAME);
                        objParams[1] = new SqlParameter("@P_AMOUNT", cpm.AMOUNT);
                        objParams[2] = new SqlParameter("@P_CHECKDT", cpm.CHECKDT);
                        objParams[3] = new SqlParameter("@P_CHECKNO", cpm.CHECKNO);
                        objParams[4] = new SqlParameter("@P_BANKNO", cpm.BANKNO);
                        objParams[5] = new SqlParameter("@P_CTRNO", cpm.CTRNO);
                        objParams[6] = new SqlParameter("@P_REMARK", cpm.REMARK);
                        objParams[7] = new SqlParameter("@P_ACCNO", cpm.ACCNO);
                        objParams[8] = new SqlParameter("@P_BILLNO", cpm.BILLNO);
                        objParams[9] = new SqlParameter("@P_BILLDT", cpm.BILLDT);
                        objParams[10] = new SqlParameter("@P_CANCEL", cpm.CANCEL);
                        objParams[11] = new SqlParameter("@P_USERNAME", cpm.USERNAME);
                        objParams[12] = new SqlParameter("@P_VNO", cpm.VNO);
                        objParams[13] = new SqlParameter("@P_VDT", cpm.VDT);
                        objParams[14] = new SqlParameter("@P_ADDRESS", cpm.ADDRESS);
                        objParams[15] = new SqlParameter("@P_COPY1", cpm.COPY1);
                        objParams[16] = new SqlParameter("@P_COPY2", cpm.COPY2);
                        objParams[17] = new SqlParameter("@P_COPY3", cpm.COPY3);
                        objParams[18] = new SqlParameter("@P_REASON1", cpm.REASON1);
                        objParams[19] = new SqlParameter("@P_REASON2", cpm.REASON2);
                        objParams[20] = new SqlParameter("@P_REASON3", cpm.REASON3);
                        objParams[21] = new SqlParameter("@P_PRINTSTATUS", cpm.PRINTSTATUS);
                        objParams[22] = new SqlParameter("@P_DEDSTATUS", cpm.DEDSTATUS);
                        objParams[23] = new SqlParameter("@P_DEDAMT", cpm.DEDAMT);
                        objParams[24] = new SqlParameter("@P_DEPT", cpm.DEPT);
                        objParams[25] = new SqlParameter("@P_ACCNAME", cpm.ACCNAME);
                        objParams[26] = new SqlParameter("@P_STAMP", cpm.STAMP);
                        objParams[27] = new SqlParameter("@P_TRNO", cpm.TRNO);
                        objParams[28] = new SqlParameter("@P_PARTYACCOUNTNO", cpm.PARTYACCOUNTNO);
                        objParams[29] = new SqlParameter("@P_PARTYNO", cpm.PARTYNO);
                        objParams[30] = new SqlParameter("@P_COMPANY_CODE", cpm.COMPANY_CODE);
                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CHEQUE_PRINT_UPDATE", objParams, true);
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
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateChequeEntryDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateChequeEntryDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //public int AddPayeeDetails(PayeeMasterClass oPayee, String CompanyCode)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[7];

                //        objParams[0] = new SqlParameter("@P_IDNO", oPayee.IDNO);
                //        objParams[1] = new SqlParameter("@P_PARTYNAME", oPayee.PARTYNAME);
                //        objParams[2] = new SqlParameter("@P_ADDRESS", oPayee.ADDRESS);
                //        objParams[3] = new SqlParameter("@P_ACCNO", oPayee.ACCNO);
                //        objParams[4] = new SqlParameter("@P_CAN", oPayee.CAN);
                //        objParams[5] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYEE_MASTER_INSERT", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayeeDetails-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddPayeeDetails(PayeeMasterClass oPayee, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_IDNO", oPayee.IDNO);
                        objParams[1] = new SqlParameter("@P_PARTYNAME", oPayee.PARTYNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", oPayee.ADDRESS);
                        objParams[3] = new SqlParameter("@P_ACCNO", oPayee.ACCNO);
                        objParams[4] = new SqlParameter("@P_CAN", oPayee.CAN);
                        objParams[5] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams[6] = new SqlParameter("@P_IFSC_CODE", oPayee.IFSC);
                        objParams[7] = new SqlParameter("@P_BRANCH", oPayee.BRANCH);
                        objParams[8] = new SqlParameter("@P_PARTY_NO", oPayee.PARTY_NO);
                        objParams[9] = new SqlParameter("@P_BANK_NO", oPayee.BANK_NO);
                        objParams[10] = new SqlParameter("@P_NATURE_ID", oPayee.NATURE_ID);
                        objParams[11] = new SqlParameter("@P_PAN_NO", oPayee.PAN_NUMBER);
                        objParams[12] = new SqlParameter("@P_CONTACT_NO", oPayee.CONTACT_NO);
                        objParams[13] = new SqlParameter("@P_Email_ID", oPayee.EMAIL_ID);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYEE_MASTER_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayeeDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdatePayeeDetails(PayeeMasterClass oPayee, String CompanyCode)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;


                //        objParams = new SqlParameter[7];

                //        objParams[0] = new SqlParameter("@P_IDNO", oPayee.IDNO);
                //        objParams[1] = new SqlParameter("@P_PARTYNAME", oPayee.PARTYNAME);
                //        objParams[2] = new SqlParameter("@P_ADDRESS", oPayee.ADDRESS);
                //        objParams[3] = new SqlParameter("@P_ACCNO", oPayee.ACCNO);
                //        objParams[4] = new SqlParameter("@P_CAN", oPayee.CAN);
                //        objParams[5] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYEE_MASTER_UPDATE", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayeeDetails-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UpdatePayeeDetails(PayeeMasterClass oPayee, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_IDNO", oPayee.IDNO);
                        objParams[1] = new SqlParameter("@P_PARTYNAME", oPayee.PARTYNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", oPayee.ADDRESS);
                        objParams[3] = new SqlParameter("@P_ACCNO", oPayee.ACCNO);
                        objParams[4] = new SqlParameter("@P_CAN", oPayee.CAN);
                        objParams[5] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams[6] = new SqlParameter("@P_IFSC_CODE", oPayee.IFSC);
                        objParams[7] = new SqlParameter("@P_BRANCH", oPayee.BRANCH);
                        objParams[8] = new SqlParameter("@P_PARTY_NO", oPayee.PARTY_NO);
                        objParams[9] = new SqlParameter("@P_BANK_NO", oPayee.BANK_NO);
                        objParams[10] = new SqlParameter("@P_PAN_NO", oPayee.PAN_NUMBER);
                        objParams[11] = new SqlParameter("@P_CONTACT_NO", oPayee.CONTACT_NO);
                        objParams[12] = new SqlParameter("@P_Email_ID", oPayee.EMAIL_ID);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYEE_MASTER_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayeeDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPayeeDetailsInExcel(String CompanyCode)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PAYEE_REPORT", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.GetMessBillBalance-> " + ee.ToString());
                    }
                }

                public int UpdateBankDetail(BankMaster obank)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[26];

                        objParams[0] = new SqlParameter("@P_BANKNO", obank.BANKNO);
                        objParams[1] = new SqlParameter("@P_BANKNAME", obank.BANKNAME);
                        objParams[2] = new SqlParameter("@P_CHECKHEIGHT", obank.CHECKHEIGHT);
                        objParams[3] = new SqlParameter("@P_CHECKWIDTH", obank.CHECKWIDTH);
                        objParams[4] = new SqlParameter("@P_PARTYTOP", obank.PARTYTOP);
                        objParams[5] = new SqlParameter("@P_PARTYLEFT", obank.PARTYLEFT);
                        objParams[6] = new SqlParameter("@P_AMTTOP", obank.AMTTOP);
                        objParams[7] = new SqlParameter("@P_AMTWIDTH", obank.AMTWIDTH);
                        objParams[8] = new SqlParameter("@P_AMTLEFT", obank.AMTLEFT);
                        objParams[9] = new SqlParameter("@P_WAMTTOP", obank.WAMTTOP);
                        objParams[10] = new SqlParameter("@P_WAMTWIDTH", obank.WAMTWIDTH);
                        objParams[11] = new SqlParameter("@P_WAMTLEFT", obank.WAMTLEFT);
                        objParams[12] = new SqlParameter("@P_CHECKTOP", obank.CHECKTOP);
                        objParams[13] = new SqlParameter("@P_CHECKLEFT", obank.CHECKLEFT);
                        objParams[14] = new SqlParameter("@P_CHECKRIGHT", obank.CHECKRIGHT);
                        objParams[15] = new SqlParameter("@P_CKDTTOP", obank.CKDTTOP);
                        objParams[16] = new SqlParameter("@P_CKDTLEFT", obank.CKDTLEFT);
                        objParams[17] = new SqlParameter("@P_CKDTWIDTH", obank.CKDTWIDTH);

                        objParams[18] = new SqlParameter("@P_PARTYWIDTH", obank.PARTYWIDTH);

                        objParams[19] = new SqlParameter("@P_STAMP1TOP", obank.STAMP1TOP);

                        objParams[20] = new SqlParameter("@P_STAMP1LEFT", obank.STAMP1LEFT);

                        objParams[21] = new SqlParameter("@P_STAMP1WIDTH", obank.STAMP1WIDTH);

                        objParams[22] = new SqlParameter("@P_STAMP2TOP", obank.STAMP2TOP);

                        objParams[23] = new SqlParameter("@P_STAMP2LEFT", obank.STAMP2LEFT);

                        objParams[24] = new SqlParameter("@P_STAMP2WIDTH", obank.STAMP2WIDTH);

                        objParams[25] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BANK_DETAIL_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateBankDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAccountMaster(Account_Master oacc, string CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_TRNO", oacc.TRNO);
                        objParams[1] = new SqlParameter("@P_BNO", oacc.BNO);
                        objParams[2] = new SqlParameter("@P_ACCNO", oacc.ACCNO);
                        objParams[3] = new SqlParameter("@P_ACCNAME", oacc.ACCNAME);
                        objParams[4] = new SqlParameter("@P_SRNO", oacc.SRNO);
                        objParams[5] = new SqlParameter("@P_CFRNO", oacc.CFRNO);
                        objParams[6] = new SqlParameter("@P_CTONO", oacc.CTONO);
                        objParams[7] = new SqlParameter("@P_CCURNO", oacc.CCURNO);
                        objParams[8] = new SqlParameter("@P_CISSUEDT", oacc.CISSUEDT.ToString("dd-MMM-yyyy"));
                        objParams[9] = new SqlParameter("@P_STATUS", oacc.STATUS);
                        objParams[10] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_ACCOUNT_MASTER_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateAccountMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ResetTransaction(AccountTransaction objParty, string COMPANY_CODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_TRANSACTION_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                        objParams[1] = new SqlParameter("@P_TRANSACTION_TYPE", objParty.TRANSACTION_TYPE);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", objParty.COMPANY_CODE);
                        objParams[3] = new SqlParameter("@P_VOUCHER_NO", objParty.VOUCHER_NO);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_RESETTING", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddTransaction(AccountTransaction objParty, double CurrentBalance, string comp_code, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[34];

                        objParams[0] = new SqlParameter("@P_SUBTR_NO", objParty.SUBTR_NO);
                        objParams[1] = new SqlParameter("@P_TRANSACTION_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                        objParams[2] = new SqlParameter("@P_TRANSACTION_TYPE", objParty.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@P_TRAN", objParty.TRAN);
                        objParams[4] = new SqlParameter("@P_PARTY_NO", objParty.PARTY_NO);
                        objParams[5] = new SqlParameter("@P_AMOUNT", objParty.AMOUNT);
                        objParams[6] = new SqlParameter("@P_DEGREE_NO", objParty.DEGREE_NO);
                        objParams[7] = new SqlParameter("@P_VOUCHER_NO", objParty.VOUCHER_NO);
                        objParams[8] = new SqlParameter("@P_TRANSFER_ENTRY", objParty.TRANSFER_ENTRY);
                        objParams[9] = new SqlParameter("@P_CBTYPE_STATUS", objParty.CBTYPE_STATUS);
                        objParams[10] = new SqlParameter("@P_CBTYPE", objParty.CBTYPE);
                        objParams[11] = new SqlParameter("@P_RECIEPT_PAYMENT_FEES", objParty.RECIEPT_PAYMENT_FEES);
                        objParams[12] = new SqlParameter("@P_REC_NO", objParty.REC_NO);
                        objParams[13] = new SqlParameter("@P_CHQ_NO", objParty.CHQ_NO);
                        if (objParty.CHQ_DATE == DateTime.MinValue)
                            objParams[14] = new SqlParameter("@P_CHQ_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                        else
                            objParams[14] = new SqlParameter("@P_CHQ_DATE", objParty.CHQ_DATE.ToString("dd-MMM-yyyy"));


                        objParams[15] = new SqlParameter("@P_CHALLAN", objParty.CHALLAN);
                        objParams[16] = new SqlParameter("@P_CAN", objParty.P_CAN1);

                        if (objParty.RECON_DATE == DateTime.MinValue)
                            objParams[17] = new SqlParameter("@P_RECON_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                        else
                            objParams[17] = new SqlParameter("@P_RECON_DATE", objParty.RECON_DATE.ToString("dd-MMM-yyyy"));


                        objParams[18] = new SqlParameter("@P_DCR_NO", objParty.DCR_NO);

                        objParams[19] = new SqlParameter("@P_IDF_NO", objParty.IDF_NO);

                        objParams[20] = new SqlParameter("@P_CASH_BANK_NO", objParty.CASH_BANK_NO);

                        objParams[21] = new SqlParameter("@P_ADVANCE_REFUND_NONE", objParty.ADVANCE_REFUND_NONE);

                        objParams[22] = new SqlParameter("@P_PAGENO", objParty.PAGENO);

                        objParams[23] = new SqlParameter("@P_PARTICULARS", objParty.PARTICULARS);

                        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objParty.COLLEGE_CODE);

                        objParams[25] = new SqlParameter("@P_USER", objParty.USER);

                        objParams[26] = new SqlParameter("@P_CREATED_MODIFIED_DATE", objParty.CREATED_MODIFIED_DATE.ToString("dd-MMM-yyyy"));

                        objParams[27] = new SqlParameter("@P_COMPANY_CODE", objParty.COMPANY_CODE);

                        objParams[28] = new SqlParameter("@P_YEAR", objParty.P_YEAR);
                        objParams[29] = new SqlParameter("@P_CBALANCE", CurrentBalance);
                        objParams[30] = new SqlParameter("@P_OPARTY", objParty.OPARTY_NO);
                        objParams[31] = new SqlParameter("@P_STR_VOUCHER_NO", objParty.STR_VOUCHER_NO);
                        objParams[32] = new SqlParameter("@P_STR_CB_VOUCHER_NO", objParty.STR_CB_VOUCHER_NO);
                        objParams[33] = new SqlParameter("@P_TRANSACTION_NO", SqlDbType.Int);
                        objParams[33].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddTransactionWithXML(DataSet dsXML, string compcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@XML", dsXML.GetXml());
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);

                        objParams[2] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public int SetVoucherNo()
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int voucherNo;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        DataSet ds = new DataSet();

                        objParams = new SqlParameter[1];
                        int VCHNO = objSQLHelper.ExecuteNonQuery("UPDATE KeyPool SET Voucher_No = Voucher_No+1");
                        //Object res=objSQLHelper.ExecuteScalar("SELECT Voucher_No FROM KeyPool");
                        ds = objSQLHelper.ExecuteDataSet("SELECT Voucher_No FROM KeyPool");
                        voucherNo = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return voucherNo;
                }

                public int VoucherUpdate(string compcode, string tranType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        int ret = objSQLHelper.ExecuteNonQuery("UPDATE ACC_" + compcode + "_TRANS SET VOUCHER_NO = A.VOUCHER_NO1 from (select  distinct STR_VOUCHER_NO, TRANSACTION_DATE,VOUCHER_NO,ROW_NUMBER() over(order by TRANSACTION_DATE, STR_VOUCHER_NO) VOUCHER_NO1 from ACC_" + compcode + "_TRANS where TRANSACTION_TYPE ='" + tranType + "' group by TRANSACTION_DATE, STR_VOUCHER_NO,voucher_no)A, ACC_" + compcode + "_TRANS B where A.STR_VOUCHER_NO = B.STR_VOUCHER_NO and A.TRANSACTION_DATE = B.TRANSACTION_DATE   and A.VOUCHER_NO = B.VOUCHER_NO and TRANSACTION_TYPE ='" + tranType + "'");
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public int AddReceiptPaymentFormat(int RptId,string RPName,double Amount,string RPFlag)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;


                //        objParams = new SqlParameter[5];

                //        objParams[0] = new SqlParameter("@V_RPTID", RptId);
                //        objParams[1] = new SqlParameter("@V_RECEIPT_PAYMENT", RPName);
                //        objParams[2] = new SqlParameter("@V_AMOUNT", Amount);
                //        objParams[3] = new SqlParameter("@V_RP_FLAG", RPFlag);
                //        objParams[4] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_RECEIPT_PAYMENT_INSERT", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddReceiptPaymentFormat-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddDayBookReportFormat(int RptId, string TranDate, string particulars, string VchType, string VchNo, double Debit, double Credit, int Bold, string party_no, string Narration, string TRAN)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_RPT_ID", RptId);
                        objParams[1] = new SqlParameter("@P_TRAN_DATE", TranDate);
                        objParams[2] = new SqlParameter("@P_PARTICULARS", particulars);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VchType);
                        objParams[4] = new SqlParameter("@P_VCH_NO", VchNo);
                        objParams[5] = new SqlParameter("@P_DEBIT", Debit);
                        objParams[6] = new SqlParameter("@P_CREDIT", Credit);
                        objParams[7] = new SqlParameter("@P_BOLD", Bold);
                        objParams[8] = new SqlParameter("@P_PARTY_NO", party_no);
                        objParams[9] = new SqlParameter("@P_NARRATION", Narration);
                        objParams[10] = new SqlParameter("@P_TRAN", TRAN);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_DAYBOOK_RECORD_INS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddDayBookReportFormat-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetTransactionResult(int VoucherNo, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_TRANSACTION_RESULT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionResult-> " + ex.ToString());
                    }
                    return ds;
                }


                //public DataSet GetAmountForFeesTransferRF(string frmDate, string toDate, string recCode, string DegreeNo, string con, string _CASH_BANK_TRANSFER, string PaymentTypeWise, string PayTypeNo)
                public DataSet GetAmountForFeesTransferRF(string frmDate, string toDate, string recCode, string DegreeNo, string con, string _CASH_BANK_TRANSFER, string PaymentTypeWise, string PayTypeNo, string Branchno)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        if (PaymentTypeWise == "N")
                        {
                            if (_CASH_BANK_TRANSFER == "N")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and PAY_MODE_CODE='C' and pay_tyPE='C' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR where CAST(REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and can=0 and PAY_MODE_CODE='C' and pay_tyPE='C' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "' AND BRANCHNO='" + Branchno + "'");//Added by vijay andoju forDepartment wise filter on 050920202

                            if (_CASH_BANK_TRANSFER == "Y")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and PAY_MODE_CODE='C'  and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR where CAST(REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and can=0 and PAY_MODE_CODE='C'  and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'AND BRANCHNO='" + Branchno + "'");//Added by vijay andoju forDepartment wise filter on 050920202
                        }
                        else if (PaymentTypeWise == "Y")
                        {
                            if (_CASH_BANK_TRANSFER == "N")
                                ////ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where DCR.REC_Dt between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and DCR.PAY_MODE_CODE='C' and DCR.pay_tyPE='C' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and STD.PTYPE IN (" + PayTypeNo + ")");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where CAST(DCR.REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and DCR.PAY_MODE_CODE='C' and DCR.pay_tyPE='C' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and DCR.BRANCHNO='" + Branchno + "' and STD.PTYPE IN (" + PayTypeNo + ")");//Added by vijay andoju forDepartment wise filter on 050920202
                            if (_CASH_BANK_TRANSFER == "Y")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where DCR.REC_Dt between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and DCR.PAY_MODE_CODE='C'  and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and STD.PTYPE IN (" + PayTypeNo + ")");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF,sum(LATE_FEE) LF from ACD_DCR DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where CAST(DCR.REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and DCR.PAY_MODE_CODE='C'  and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and DCR.BRANCHNO='" + Branchno + "'  and STD.PTYPE IN (" + PayTypeNo + ")");//Added by vijay andoju forDepartment wise filter on 050920202

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAmountForFeesRefundRF(string frmDate, string toDate, string recCode, string DegreeNo, string con, string BranchNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 ,SUM(EXCESS_AMOUNT) EF from ACD_DCR where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and PAY_MODE_CODE='C' and pay_tyPE='C' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                        ds = objSQLHelper.ExecuteDataSet("select sum(A.F1) as F1,sum(A.F2) as F2,sum(A.F3) as F3,sum(A.F4) as F4,sum(A.F5) as F5,sum(A.F6) as F6,sum(A.F7) as F7,sum(A.F8)as F8,sum(A.F9)as F9,sum(A.F10)as F10,sum(A.F11)as F11,sum(A.F12) as F12,sum(A.F13)as F13,sum(A.F14)as F14,sum(A.F15)as F15,sum(A.F16)as F16,sum(A.F17)as F17,sum(A.F18)as F18,sum(A.F19)as F19,sum(A.F20)as F20,sum(A.F21)as F21,sum(A.F22)as F22,sum(A.F23)as F23,sum(A.F24)as F24,sum(A.F25)as F25,sum(A.F26)as F26,sum(A.F27)as F27,sum(A.F28)as F28,sum(A.F29)as F29,sum(A.F30)as F30 from ACD_REFUND A INNER JOIN ACD_DCR B ON (A.DCR_NO=B.DCR_NO) where A.VOUCHER_DATE between '" + frmDate + "' and '" + toDate + "' and B.can=0 and B.PAY_MODE_CODE='C'  and B.RECIEPT_CODE='" + recCode + "' and B.DEGREENO='" + DegreeNo + "' AND B.BRANCHNO='" + BranchNo + "'");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                //public DataSet GetAmountForFeesTransferRF(string frmDate, string toDate, string recCode, string DegreeNo, string con)
                //{
                //    DataSet ds;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(con);
                //        ds = objSQLHelper.ExecuteDataSet("select sum(f1) as F1,sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30 from ACD_DCR where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and pay_tyPE='C' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetbankChequeDetails(int PartyNo, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTYNO", PartyNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_CHEQUEBANK_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetbankChequeDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetReceiptPaymentResult(string code_year, string Ledger, string FromDate, string ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_LEDGER", Ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[3] = new SqlParameter("@P_TODATE", ToDate);
                        objParams[4] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RECEIPT_PAYMENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetReceiptPaymentResult-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBalancesForRowDeletion(string code_year, string PartyIds, string VoucherNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTID", PartyIds);
                        objParams[2] = new SqlParameter("@P_VCHNO", VoucherNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BALANCE_FOR_DELETION", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RECEIPT_PAYMENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBalancesForRowDeletion-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBalancesPartyBalances(string code_year, string FromDate, string ToDate, Int32 partyNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CODE", code_year);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", ToDate);
                        objParams[3] = new SqlParameter("@P_PARTY_NO", partyNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_ret_closing_balance", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBalancesPartyBalances-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetPartyBalancesForRowDeletion(string code_year, string PartyIds)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTID", PartyIds);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_PARTY_BALANCE_FOR_DELETION", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RECEIPT_PAYMENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetPartyBalancesForRowDeletion-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteReceiptPaymentData(string code_year)
                {

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_RECEIPT_PAYEMENT_DELETE", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.DeleteReceiptPaymentData-> " + ex.ToString());
                    }
                    return 1;
                }
                public DataSet DeleteTransactionResult(int VoucherNo, string code_year, string COMPANY_CODE, string VCH_TYPE, string IsModified)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_COMPANY_CODE", COMPANY_CODE);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_ISMODIFIED", IsModified);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_TRANS_DELETE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.DeleteTransactionResult-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet UpdateLedgerBalance(int PartyNo, double Balance, string code_year, string COMPANY_CODE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_PARTY_NO", PartyNo);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", COMPANY_CODE);
                        objParams[3] = new SqlParameter("@P_CBALANCE", Balance);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PARTY_BALANCE_UPDATE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateLedgerBalance-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet UpdStrCbVNo(string compCode, string vType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", compCode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_TYPE", vType);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_VOUCHERTYPENO_UPD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdStrCbVNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet UpdateMaxStringVoucherNo(string code_year, string Voucher_Type, int vchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", vchno);
                        objParams[2] = new SqlParameter("@P_VOUCHER_TYPE", Voucher_Type);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_UPDATE_MAX_VOUCHERNO_STRING", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateMaxStringVoucherNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int IncrementChequeNo(int BankNo, String AccountNo, string COMPANY_CODE)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_BNO", BankNo);
                        objParams[1] = new SqlParameter("@P_ACCNO", AccountNo);
                        objParams[2] = new SqlParameter("@P_CODE_YEAR", COMPANY_CODE);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CHEQUE_NO_UPDATE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(ret);// Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.IncrementChequeNo-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet UpdateLedgerBalanceForRowDeletion(int PartyNo, double Balance, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTID", PartyNo);
                        objParams[2] = new SqlParameter("@P_BALANCE", Balance);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_BALANCE_UPDATE_FOR_DELETION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateLedgerBalanceForRowDeletion-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet UpdateBankReconcilation(int PartyNo, string ReconcileDate, string TransactionDate, string code_year, string voucherNo, string VCH_TYPE, int VOUCHER_SQN, string CHEQNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_PARTY_NO", PartyNo);
                        objParams[1] = new SqlParameter("@P_COMPANY_CODE", code_year);
                        objParams[2] = new SqlParameter("@P_TRANDATE", TransactionDate);
                        objParams[3] = new SqlParameter("@P_RECONCILEDATE", ReconcileDate);
                        objParams[4] = new SqlParameter("@P_VOUCHERNO", voucherNo);
                        objParams[5] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[6] = new SqlParameter("@P_VOUCHER_SQN", VOUCHER_SQN);
                        objParams[7] = new SqlParameter("@P_CHEQUENO", CHEQNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BANK_RECONCILATION_UPDATE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateBankReconcilation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTransactionForModification(int PartyNo, string VOUCHER_NO, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", PartyNo);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTransactionForModification(int VOUCHER_NO, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_DETAILS_UPDATE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY TANU  GET EMP DATA FOR   MODIFICATION OF BULK VOUCHAR  19/7/2021

                public DataSet GetTransEmpDataForModification(string code_year, int VOUCHER_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_EMP_DATA_DETAILS_UPDATE_MAKAUT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransEmpDataForModification-> " + ex.ToString());
                    }
                    return ds;
                }


                //Method For CCMS Head Mapping Added by Nitin Meshram on Date 29-Apr-2014
                public DataSet GetReceiptTypeForCCMS(string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select CODE as RECIEPT_CODE,TITLE as RECIEPT_TITLE from CASHBOOK_REF_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetReceiptTypeMFForCCMS(string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select CODE as RECIEPT_CODE,TITLE as RECIEPT_TITLE from CASHBOOK_REF_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeeHeadForCCMS(string recType, string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select HEADS as FEE_HEAD,HEADDESC as FEE_LONGNAME from FEEHEAD_MASTER where HEADDESC<>'' and CODE='" + recType + "'");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                //Method For CCMS Head Mapping Added by Nitin Meshram on Date 29-Apr-2014
                public DataSet GetMiscFeeHeadForCCMS(string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select MISCHEADCODE  as FEE_HEAD,MISCHEAD as FEE_LONGNAME from MISCHEAD_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                //Method For CCMS Fees Transfer Added by Nitin Meshram on Date 29-Apr-2014
                public DataSet GetCashAmountForFeesTransferCCMS(string fromDate, string toDate, string recCode, string pay_type, string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select  sum(f1) as F1, sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30,sum(f31)as F31,sum(f32)as F32,sum(f33)as F33,sum(f34)as F34,sum(f35)as F35,sum(f36)as F36,sum(f37)as F37,sum(f38)as F38,sum(f39)as F39,sum(f40)as F40 from DCR where [DATE] BETWEEN '" + fromDate + "' AND '" + toDate + "' and REM<>'CAN' and CHDD='" + pay_type + "' and CB_CODE='" + recCode + "'");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCashAmountForFeesTransfer(string frmDate, string toDate, string con)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE between '" + frmDate + "' and '" + toDate + "' and can=0 and CHDD='C' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where MISCRECPTDATE between '" + frmDate + "' and '" + toDate + "' and  CHDD='C' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCashAmountForFeesTransfer-> " + ex.ToString());
                    }
                    return ds;
                }


                //public DataSet GetBankAmountForFeesTransfer(string frmDate, string toDate, string recCode, string DegreeNo, string con)
                //{
                //    DataSet ds;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(con);
                //        ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30 from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and pay_type='D' and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForFeesTransfer-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetBankAmountForFeesTransfer(string frmDate, string toDate, string recCode, string DegreeNo, string con, string _CASH_BANK_TRANSFER, string PaymentTypeWise, string PayTypeNos, string Branchno)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        if (PaymentTypeWise == "N")
                        {
                            if (_CASH_BANK_TRANSFER == "N")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE='B' OR pay_tyPE='D')  and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE in ('B','O') OR pay_tyPE='D') and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr where CAST(REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE in ('B','O') OR pay_tyPE='D') and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "' and BRANCHNO='" + Branchno + "'");//Added by vijay andoju on 05092020 for filter for department

                            if (_CASH_BANK_TRANSFER == "Y")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE='B')  and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE in ('B','O'))  and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr where CAST(REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE in ('B','O'))  and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "' and BRANCHNO='" + Branchno + "'");//Added by vijay andoju for filter branchno on 05092020

                        }
                        else if (PaymentTypeWise == "Y")
                        {
                            if (_CASH_BANK_TRANSFER == "N")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where DCR.REC_Dt between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and (PAY_MODE_CODE='B' OR pay_tyPE='D')  and DCR.RECON='1' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and STD.PTYPE IN (" + PayTypeNos + ")");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where CAST(DCR.REC_Dt AS DATE)between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and (PAY_MODE_CODE='B' OR pay_tyPE='D')  and DCR.RECON='1' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' AND DCR.BRANCHNO='" + Branchno + "' and STD.PTYPE IN (" + PayTypeNos + ")");//ADDED BY VIJAY ANDOJU ON 05092020 FOR DEPARTMENT WISE FILTER

                            if (_CASH_BANK_TRANSFER == "Y")
                                //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where DCR.REC_Dt between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and (PAY_MODE_CODE='B')  and DCR.RECON='1' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' and STD.PTYPE IN (" + PayTypeNos + ")");
                                ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF,sum(Late_Fee) LF from acd_dcr DCR INNER JOIN ACD_STUDENT STD ON (DCR.IDNO=STD.IDNO) where CAST(DCR.REC_Dt AS DATE) between '" + frmDate + "' and '" + toDate + "' and DCR.can=0 and (PAY_MODE_CODE='B')  and DCR.RECON='1' and DCR.RECIEPT_CODE='" + recCode + "' and DCR.DEGREENO='" + DegreeNo + "' AND DCR.BRANCHNO='" + Branchno + "'  and STD.PTYPE IN (" + PayTypeNos + ")");//ADDED BY VIJAY ANDOJU ON 05092020 FOR DEPARTMENT WISE FILTER

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForFeesTransfer-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBankAmountForFeesRefund(string frmDate, string toDate, string recCode, string DegreeNo, string con, string BranchNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //ds = objSQLHelper.ExecuteDataSet("select sum(f1)F1,sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30,SUM(EXCESS_AMOUNT) EF from acd_dcr where REC_Dt between '" + frmDate + "' and '" + toDate + "' and can=0 and (PAY_MODE_CODE='B' OR pay_tyPE='D')  and RECON='1' and RECIEPT_CODE='" + recCode + "' and DEGREENO='" + DegreeNo + "'");
                        ds = objSQLHelper.ExecuteDataSet("select sum(A.F1) as F1,sum(A.F2) as F2,sum(A.F3) as F3,sum(A.F4) as F4,sum(A.F5) as F5,sum(A.F6) as F6,sum(A.F7) as F7,sum(A.F8)as F8,sum(A.F9)as F9,sum(A.F10)as F10,sum(A.F11)as F11,sum(A.F12) as F12,sum(A.F13)as F13,sum(A.F14)as F14,sum(A.F15)as F15,sum(A.F16)as F16,sum(A.F17)as F17,sum(A.F18)as F18,sum(A.F19)as F19,sum(A.F20)as F20,sum(A.F21)as F21,sum(A.F22)as F22,sum(A.F23)as F23,sum(A.F24)as F24,sum(A.F25)as F25,sum(A.F26)as F26,sum(A.F27)as F27,sum(A.F28)as F28,sum(A.F29)as F29,sum(A.F30)as F30 from ACD_REFUND A INNER JOIN ACD_DCR B ON (A.DCR_NO=B.DCR_NO) where A.VOUCHER_DATE between '" + frmDate + "' and '" + toDate + "' and B.can=0 and (B.PAY_MODE_CODE='B')  and B.RECON='1' and RECIEPT_CODE='" + recCode + "' and B.DEGREENO='" + DegreeNo + "'  AND B.BRANCHNO='" + BranchNo + "'");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForFeesTransfer-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBankAmountForMF(string frmDate, string toDate, string con)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE BETWEEN '" + frmDate + "' and '" + toDate + "' and can=0 and CHDD='Q' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where MISCRECPTDATE BETWEEN '" + frmDate + "' and '" + toDate + "' and (CHDD='D' or CHDD='T' )  GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForMF-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCashAmountForFeesTransferMFCCMS(string fromDate, string toDate, string recCode, string pay_type, string conn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        ds = objSQLHelper.ExecuteDataSet("select  MISCHEADSRNO,MISCHEADCODE,MISCHEAD, sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where MISCRECPTDATE BETWEEN '" + fromDate + "' AND '" + toDate + "' and RECCANDT is null and CHDD='" + pay_type + "' and CB_CODE='" + recCode + "' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD ");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet PopulateDegreeFromRF(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select DEGREENO,DEGREENAME from acd_degree where DEGREENAME!='' AND DEGREENO>0");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateDegree-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet PopulateReceiptType(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select RECIEPT_CODE,RECIEPT_TITLE from ACD_RECIEPT_TYPE");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GetCountOfFeeHeadForCCMS(string recType, string conn)
                {
                    int Count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        DataSet ds = objSQLHelper.ExecuteDataSet("select COUNT(HEADS) from FEEHEAD_MASTER where HEADDESC<>'' and CODE='" + recType + "'");
                        Count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCountOfFeeHeadForCCMS-> " + ex.ToString());
                    }
                    return Count;
                }
                public int GetCountOfFeeHeadForRFCampus(string recType, string conn)
                {
                    int count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        DataSet ds = objSQLHelper.ExecuteDataSet("select COUNT(FEE_HEAD) from ACD_FEE_TITLE where RECIEPT_CODE='" + recType + "' and FEE_HEAD !='' and FEE_LONGNAME<>''");
                        count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCountOfFeeHeadForRFCampus-> " + ex.ToString());
                    }
                    return count;
                }
                public int GetCountMiscFeeHaedForCCMS(string conn)
                {
                    int count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        DataSet ds = objSQLHelper.ExecuteDataSet("select count(MISCHEADCODE) from MISCHEAD_MASTER");
                        count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCountMiscFeeHaedForCCMS-> " + ex.ToString());
                    }
                    return count;
                }
                //public int GetCountOfMiscFeeHeadForRFCampus(string conn)
                //{
                //    int count = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(conn);
                //        DataSet ds = objSQLHelper.ExecuteDataSet("select count(MHEADCODE) from ACD_MISCELLANEOUS_HEAD");
                //        count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCountOfMiscFeeHeadForRFCampus-> " + ex.ToString());
                //    }
                //    return count;
                //}


                //Edited by vijay on 29092020
                public int GetCountOfMiscFeeHeadForRFCampus(string conn)
                {
                    DataSet ds;
                    int count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(conn);
                        DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCHEAD_MASTER'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MHEADCODE as FEE_HEAD,MHEADNAME as FEE_LONGNAME from ACD_MISCELLANEOUS_HEAD");
                        }
                        else
                            count = 0;
                        //ds = objSQLHelper.ExecuteDataSet("select MISCHEADCODE as FEE_HEAD,MISCHEAD as FEE_LONGNAME from MISCHEAD_MASTER");
                        ds = objSQLHelper.ExecuteDataSet("select count(MISCHEADCODE) from MISCHEAD_MASTER");
                        count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadForMF-> " + ex.ToString());
                    }
                    return count;
                }


                public int AddFeeAccountTransfer_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_CASH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransfer_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, string Paytypenos, int pgroupno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_PAYTYPENOS", Paytypenos);
                        objParams[11] = new SqlParameter("@P_PGROUP_NO", pgroupno);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_CASH_PYTYPEWISE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransfer_CASH_manual(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_CASH_MANUAL_VCH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }


                public int AddFeeAccountTransfer_CASH_manual(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, string paytypenos, int pgroupno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_PAYTYPENOS", paytypenos);
                        objParams[11] = new SqlParameter("@P_PGROUP_NO", pgroupno);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_CASH_MANUAL_VCH_PAYTYPEWISE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountRefund_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_REFUND_GENRAL_CASH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                //public int AddFeeAccountTransfer_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                public int AddFeeAccountTransfer_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, string BRANCHNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_BANK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransfer_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, string paytypenos, int pgroupno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_PAYTYPENOS", paytypenos);
                        objParams[11] = new SqlParameter("@P_PGROUP_NO", pgroupno);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_BANK_PAYMENTTYPE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransfer_BANK_MANUAL(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_BANK_MANUAL_VCH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransfer_BANK_MANUAL(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, string Paytypenos, int pgroupno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_PAYTYPENOS", Paytypenos);
                        objParams[11] = new SqlParameter("@P_PGROUP_NO", pgroupno);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_GENRAL_BANK_MANUAL_VCH_PAYTYPEWISE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }


                public int AddFeeAccountRefund_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database, int BRANCHNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_Database", database);
                        objParams[10] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_REFUND_GENRAL_BANK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_MANUAL_VCH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_MANUAL_VCH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_MANUAL_VCH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetTransactionForModification(int VOUCHER_NO, string code_year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_DETAILS_UPDATE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddFeeLedgerHeadMaping(FeeLedgerHeadMapingClass objFLHM, string COMPANYCODE, int BranchNo, string Aided_Noaided, int DEPTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[25];

                        objParams[0] = new SqlParameter("@P_RECIEPT_TYPE", objFLHM.RECIEPT_TYPE);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objFLHM.DegreeNo);
                        objParams[2] = new SqlParameter("@P_FEE_HEAD_NO", objFLHM.FEE_HEAD_NO);
                        //@P_FEE_HEAD_NAME
                        objParams[3] = new SqlParameter("@P_FEE_HEAD_NAME", objFLHM.FeeLedger_NAme);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objFLHM.COLLEGE);
                        objParams[5] = new SqlParameter("@P_LEDGERNO", objFLHM.LEDGER_NO);
                        objParams[6] = new SqlParameter("@P_CASHNO", objFLHM.CASH_NO);
                        objParams[7] = new SqlParameter("@P_BANKNO", objFLHM.BANK_NO);
                        objParams[8] = new SqlParameter("@P_CNAME", objFLHM.CName);
                        objParams[9] = new SqlParameter("@P_CPASS", objFLHM.CPass);
                        objParams[10] = new SqlParameter("@P_CREATOR", objFLHM.CREATER_NAME);
                        objParams[11] = new SqlParameter("@P_CREATIONDATE", objFLHM.CREATE_DATE.ToString("dd-MMM-yyyy"));
                        objParams[12] = new SqlParameter("@P_LMODIFIER", objFLHM.LASTMODIFIER);
                        objParams[13] = new SqlParameter("@P_LMTIME", objFLHM.LASTMODIFIER_DATE.ToString("dd-MMM-yyyy"));
                        objParams[14] = new SqlParameter("@P_MBANK", objFLHM.MBank);
                        objParams[15] = new SqlParameter("@P_MFEELED", objFLHM.Mfeeled);
                        objParams[16] = new SqlParameter("@P_CODE", COMPANYCODE);
                        objParams[17] = new SqlParameter("@P_NARATION", objFLHM.Naration);
                        objParams[18] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[19] = new SqlParameter("@P_COLLEGE_JSS", Aided_Noaided);
                        objParams[20] = new SqlParameter("@P_SEQUENCE", objFLHM.SequenceId);
                        objParams[21] = new SqlParameter("@p_DEPTNO", DEPTNO);
                        objParams[22] = new SqlParameter("@P_BATCHNO", objFLHM.BatchNo);
                        objParams[23] = new SqlParameter("@P_SEMESTERNO", objFLHM.SemesterNo);

                        objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEE_LEDGERHEAD_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeLedgerHeadMaping-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateFeeLedgerHeadMaping(FeeLedgerHeadMapingClass objFLHM, string COMPANYCODE, int BranchNo, string Aided_Noaided)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[21];

                        objParams[0] = new SqlParameter("@P_RECIEPT_TYPE", objFLHM.RECIEPT_TYPE);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objFLHM.DegreeNo);
                        objParams[2] = new SqlParameter("@P_FEE_HEAD_NO", objFLHM.FEE_HEAD_NO);
                        objParams[3] = new SqlParameter("@P_FEE_HEAD_NAME", objFLHM.FeeLedger_NAme);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objFLHM.COLLEGE);
                        objParams[5] = new SqlParameter("@P_LEDGERNO", objFLHM.LEDGER_NO);
                        objParams[6] = new SqlParameter("@P_CASHNO", objFLHM.CASH_NO);
                        objParams[7] = new SqlParameter("@P_BANKNO", objFLHM.BANK_NO);
                        objParams[8] = new SqlParameter("@P_CNAME", objFLHM.CName);
                        objParams[9] = new SqlParameter("@P_CPASS", objFLHM.CPass);
                        //objParams[8] = new SqlParameter("@P_CREATOR", objFLHM.CREATER_NAME);
                        //objParams[9] = new SqlParameter("@P_CREATIONDATE", objFLHM.CREATE_DATE.ToString("dd-MMM-yyyy"));
                        objParams[10] = new SqlParameter("@P_LMODIFIER", objFLHM.LASTMODIFIER);
                        objParams[11] = new SqlParameter("@P_LMTIME", objFLHM.LASTMODIFIER_DATE.ToString("dd-MMM-yyyy"));
                        objParams[12] = new SqlParameter("@P_MBANK", objFLHM.MBank);
                        objParams[13] = new SqlParameter("@P_MFEELED", objFLHM.Mfeeled);
                        objParams[14] = new SqlParameter("@P_CODE", COMPANYCODE);
                        objParams[15] = new SqlParameter("@P_NARATION", objFLHM.Naration);
                        objParams[16] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[17] = new SqlParameter("@P_COLLEGE_JSS", Aided_Noaided);
                        objParams[18] = new SqlParameter("@P_BATCHNO", objFLHM.BatchNo);
                        objParams[19] = new SqlParameter("@P_SEMESTERNO", objFLHM.SemesterNo);
                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEE_LEDGERHEAD_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateFeeLedgerHeadMaping-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateFeeLedgerHeadMapSetBlank(string COMPANYCODE, int degNo, string recType, int branchno, string College_JSS, int BATCHNO, int SEMNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_CODE", COMPANYCODE);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degNo);
                        objParams[2] = new SqlParameter("@P_RECIEPT_TYPE", recType);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_JSS", College_JSS);
                        objParams[5] = new SqlParameter("@P_BATCHNO", BATCHNO);
                        objParams[6] = new SqlParameter("@P_SEMNO", SEMNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEE_LEDGERHEAD_UPD_SETBLANK", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdateFeeLedgerHeadMapSetBlank-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddMISCFeeAccountTransfer(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddMISCFeeAccountTransferFromCCMS_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_FROM_CCMS_BANK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransferFromCCMS_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_FROM_CCMS_CASH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddFeeAccountTransferFromCCMS_CASH(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_FROM_CCMS_CASH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddFeeAccountTransferFromCCMS_BANK(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string database)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", database);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER_FROM_CCMS_BANK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddFeeAccountTransfer(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FEES_TRANSFER", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int deleteTransactionForTransfer(string DateFrom, string DateTo, string compCode, int DegNo, string CBtype)
                {
                    try
                    {
                        SQLHelper objSqHp = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_Cbtype", CBtype);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DegNo);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", compCode);
                        objParams[3] = new SqlParameter("@P_DATE", DateFrom);
                        objParams[4] = new SqlParameter("@P_DATETo", DateTo);

                        objSqHp.ExecuteNonQuerySP("PKG_ACC_TRANS_DELETE_FOR_TRANSFER", objParams, true);
                        // return 1;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.deleteTransactionForTransfer-> " + ex.ToString());
                    }
                    return 1;
                }


                public int AddSupplimentaryLedgerMaping(string PayheadNo, string payHeadName, int ledgerNo, int BNo, int staffNo, string compCode, int HeadFor, int CRLEDGERNO, int DRLEDGERNO, int CollegeId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SUPLHNO", PayheadNo);
                        objParams[1] = new SqlParameter("@P_SUPL_HEAD_NAME", payHeadName);
                        objParams[2] = new SqlParameter("@P_LEDGER_NO", ledgerNo);
                        objParams[3] = new SqlParameter("@P_BANK_NO", BNo);
                        objParams[4] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[5] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[7] = new SqlParameter("@P_HEADFOR", HeadFor);
                        objParams[8] = new SqlParameter("@P_CRLEDGERNO", CRLEDGERNO);
                        objParams[9] = new SqlParameter("@P_DRLEDGERNO", DRLEDGERNO);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SUPLIMENTRY_LEDGER_MAPPING_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerMaping-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by vihjay andoju "SUbdeptNo" fro department wise fillter on 19092020
                public int AddPayRollLedgerMaping(string PayheadNo, string payHeadName, int ledgerNo, int BNo, int staffNo, string compCode, int HeadFor, int CRLEDGERNO, int DRLEDGERNO, int CollegeId, int SubdeptNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_PAY_HEAD_NO", PayheadNo);
                        objParams[1] = new SqlParameter("@P_PAY_HEAD_NAME", payHeadName);
                        objParams[2] = new SqlParameter("@P_LEDGER_NO", ledgerNo);
                        objParams[3] = new SqlParameter("@P_BANK_NO", BNo);
                        objParams[4] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[5] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[7] = new SqlParameter("@P_HEADFOR", HeadFor);
                        objParams[8] = new SqlParameter("@P_CRLEDGERNO", CRLEDGERNO);
                        objParams[9] = new SqlParameter("@P_DRLEDGERNO", DRLEDGERNO);
                        //Added by vijay andoju on 19092020 for depatment
                        objParams[10] = new SqlParameter("@P_SUBDEPTNO", SubdeptNo);
                        //---------------------------------------------------------------
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYROLL_LEDGER_MAPPING_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerMaping-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePayRollLedgerMaping(string PayheadNo, string payHeadName, int ledgerNo, int BNo, int staffNo, string compCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_PAY_HEAD_NO", PayheadNo);
                        objParams[1] = new SqlParameter("@P_PAY_HEAD_NAME", payHeadName);
                        objParams[2] = new SqlParameter("@P_LEDGER_NO", ledgerNo);
                        objParams[3] = new SqlParameter("@P_BANK_NO", BNo);
                        objParams[4] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[5] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYROLL_LEDGER_MAPPING_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.UpdatePayRollLedgerMaping-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddPayRollLedgerTransfer(string staffNo, string MonYear, int userNo, string compCode, string Database, string Date, int CollegeId, int Deptno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        objParams[2] = new SqlParameter("@P_USERNO", userNo);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DATABASE", Database);
                        objParams[5] = new SqlParameter("@P_TRANSACTION_DATE", Date);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[7] = new SqlParameter("@P_DEPT_NO", Deptno);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_PAYROLL_TRANSFER", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteSuppliTransfer(string compCode, string Id,string STAFNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_STAFF_NO", STAFNO);
                        objParams[1] = new SqlParameter("@P_SUPLTRXID", Id);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", compCode);
                      
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_SUPPLIMENTRY_BILL_DELETE_TRANSFER", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddPayrollSuplimentryTransferGeust(string staffNo, string MonYear, int userNo, string compCode, string Database, string Date, int CollegeId, decimal FinAmount, string Id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        objParams[2] = new SqlParameter("@P_USERNO", userNo);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DATABASE", Database);
                        objParams[5] = new SqlParameter("@P_TRANSACTION_DATE", Date);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[7] = new SqlParameter("@P_FINAMOUNT", FinAmount);
                        objParams[8] = new SqlParameter("@P_SUPLTRXID", Id);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_SUPPLIMENTRY_BILL_GUEST_EMPWISE_TRANSFER", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddPayrollSuplimentryTransfer(string staffNo, string MonYear, int userNo, string compCode, string Database, string Date, int CollegeId, decimal FinAmount, string Id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        objParams[2] = new SqlParameter("@P_USERNO", userNo);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DATABASE", Database);
                        objParams[5] = new SqlParameter("@P_TRANSACTION_DATE", Date);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[7] = new SqlParameter("@P_FINAMOUNT", FinAmount);
                        objParams[8] = new SqlParameter("@P_SUPLTRXID", Id);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_SUPPLIMENTRY_BILL_EMPWISE_TRANSFER", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddPayRollLedgerTransferByEmp(string staffNo, string MonYear, int userNo, string compCode, string NetPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_STAFF_NO ", staffNo);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        objParams[2] = new SqlParameter("@P_USERNO", userNo);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_NETPAY", NetPay);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("ACC_PAYROLL_TRANSFER_TEST", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddPayRollLedgerTransferByEmp-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeletePayRollTransfer(string PAGENO, string CBTYPE, string COMPCODE, string DEPT_NO, int COLLEGE_ID)
                {
                    try
                    {
                        SQLHelper objSqHp = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", COMPCODE);
                        objParams[1] = new SqlParameter("@P_MONYEAR", PAGENO);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", CBTYPE);
                        objParams[3] = new SqlParameter("@P_DEPT_NO", DEPT_NO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);  
                        objSqHp.ExecuteNonQuerySP("PKG_ACC_TRANS_DELETE_FOR_PAYROLL_TRANSFER", objParams, true);
                        // return 1;
                    }

                    catch (Exception ex)
                    {
                        return 0;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.deleteTransactionForTransfer-> " + ex.ToString());
                    }
                    return 1;
                }
                public DataSet GetVoucherByCheakPrinting(string codeYear, string partyNo, string Fdate, string toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", codeYear);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", partyNo);
                        objParams[2] = new SqlParameter("@P_FROMDATE", Fdate);
                        objParams[3] = new SqlParameter("@P_TODATE", toDate);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_VOUCHER_PRINT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetVoucherByCheakPrinting-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVoucherByCheakPrinting_NEW(string codeYear, string partyNo, string Fdate, string toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", codeYear);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", partyNo);
                        objParams[2] = new SqlParameter("@P_FROMDATE", Fdate);
                        objParams[3] = new SqlParameter("@P_TODATE", toDate);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_VOUCHER_PRINT_NEW", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetVoucherByCheakPrinting-> " + ex.ToString());
                    }
                    return ds;
                }
                //-------------------------------------------
                public int InsertFixedDepositeDetails(FixedDepositeClass fdcObj, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_Party_No", fdcObj.PARTY_NO);
                        objParams[1] = new SqlParameter("@P_FDScheme", fdcObj.SCHEME);
                        objParams[2] = new SqlParameter("@P_FDR_No", fdcObj.FDR_NO);
                        objParams[3] = new SqlParameter("@P_ROI", fdcObj.RateOfIntrest);
                        objParams[4] = new SqlParameter("@P_Deposit_Date", fdcObj.Deposite_Date);
                        objParams[5] = new SqlParameter("@P_Maturity_Date", fdcObj.Maturity_Date);
                        objParams[6] = new SqlParameter("@P_Deposit_Amount", fdcObj.Amount);

                        objParams[7] = new SqlParameter("@P_CODE", CompanyCode);
                        //objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FIXEDDEPOSITE_DETAILS_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertFixedDepositeDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                // PKG_ACC_GET_FIXEDDEPOSITE_DETAILS
                public DataSet GetFIXEDDEPOSITE_DETAILS(string compCode)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[1];



                        objParams[0] = new SqlParameter("@P_CODE", compCode);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_FIXEDDEPOSITE_DETAILS", objParams);


                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFIXEDDEPOSITE_DETAILS-> " + ex.ToString());
                    }
                    return ds;


                }
                public DataSet GetFIXEDDEPOSITE_MATURED_DETAILS(string compCode)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[1];



                        objParams[0] = new SqlParameter("@P_CODE", compCode);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_FIXEDDEPOSITE_MATURIED_DETAILS", objParams);


                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFIXEDDEPOSITE_DETAILS-> " + ex.ToString());
                    }
                    return ds;


                }
                public int UpdateFixedDepositeDetails(FixedDepositeClass fdcObj, String CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_Party_No", fdcObj.PARTY_NO);
                        objParams[1] = new SqlParameter("@P_FDScheme", fdcObj.SCHEME);
                        objParams[2] = new SqlParameter("@P_FDR_No", fdcObj.FDR_NO);
                        objParams[3] = new SqlParameter("@P_ROI", fdcObj.RateOfIntrest);
                        objParams[4] = new SqlParameter("@P_Deposit_Date", fdcObj.Deposite_Date);
                        objParams[5] = new SqlParameter("@P_Maturity_Date", fdcObj.Maturity_Date);
                        objParams[6] = new SqlParameter("@P_Deposit_Amount", fdcObj.Amount);

                        objParams[7] = new SqlParameter("@P_CODE", CompanyCode);
                        //objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FIXEDDEPOSITE_DETAILS_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertFixedDepositeDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteFixedDepositeDetails(string FDR_NO, string CompanyCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[2];


                        objParams[0] = new SqlParameter("@P_CODE", CompanyCode);
                        objParams[1] = new SqlParameter("@P_FDR_NO", FDR_NO);
                        //objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FIXEDDEPOSITE_DETAILS_DELETE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertFixedDepositeDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #region Merge ledger
                /// <summary>
                /// this method is used to transfer the transaction of the one ledger to another ledger based on the category and used in the page MergeLedger.aspx
                /// for ex: payment type no (1-cash in hand,2-bank accounts,3-other)
                /// </summary>
                /// <param name="code_year"></param>
                /// <param name="partyNo_ToMerge"></param>
                /// <param name="partyNo_ToBe_Merge"></param>
                /// <returns></returns>
                public int MergeLedger(string code_year, int partyNo_ToMerge, int partyNo_ToBe_Merge, string deleteStatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTYNO_TOMERGE", partyNo_ToMerge);
                        objParams[2] = new SqlParameter("@P_PARTYNO_TOBE_MERGE", partyNo_ToBe_Merge);
                        objParams[3] = new SqlParameter("@P_DELETESTATUS", deleteStatus);

                        //objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_LEDGER_MERGE", objParams, true);
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
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.MergeLedger-> " + ex.ToString());

                    }
                    return retStatus;
                }
                public int DeleteMergeLedger(string code_year, int partyNo_ToDelete, string deleteStatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_PARTYNO_TODELETE", partyNo_ToDelete);
                        objParams[2] = new SqlParameter("@P_DELETESTATUS", deleteStatus);

                        //objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_LEDGER_MERGE_DELETE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.MergeLedger-> " + ex.ToString());

                    }
                    return retStatus;
                }
                /// <summary>
                /// This method is used to get the transaction of particular ledger and used in the page MergeLedger.aspx
                /// </summary>
                /// <param name="party_no"></param>
                /// <param name="colcode"></param>
                /// <returns></returns>
                public DataSet GetTransactionFromParty(int party_no, string colcode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PARTY_NO", party_no);
                        objParams[1] = new SqlParameter("@P_colcode", colcode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_TRANSACTION", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }

                /// <summary>
                /// this method is used to get the payment_type no based on the particular ledger and used in the page MergeLedger.aspx
                /// </summary>
                /// <param name="partyno"></param>
                /// <param name="colcode"></param>
                /// <returns></returns>
                public DataSet GetPartynoForTransaction(int partyno, string colcode)
                {
                    int paymenttypeNo = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PARTY_NO", partyno);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", colcode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_TRANSACTION_PARTY_NO", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }
                #endregion
                //-----------------------------------------------
                public int AddReceiptPaymentFormat(int RptId, string RPName, double Amount, string RPFlag)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@V_RPTID", RptId);
                        objParams[1] = new SqlParameter("@V_RECEIPT_PAYMENT", RPName);
                        objParams[2] = new SqlParameter("@V_AMOUNT", Amount);
                        objParams[3] = new SqlParameter("@V_RP_FLAG", RPFlag);
                        objParams[4] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["DataBase"].ToString());

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_RECEIPT_PAYMENT_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddReceiptPaymentFormat-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetFeeHeadForMF(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCHEAD_MASTER'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MHEADCODE as FEE_HEAD,MHEADNAME as FEE_LONGNAME from ACD_MISCELLANEOUS_HEAD");
                        }
                        else
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADCODE as FEE_HEAD,MISCHEAD as FEE_LONGNAME from MISCHEAD_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadForMF-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeeHeadAndNo(string recType, string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select FEE_HEAD,FEE_LONGNAME from ACD_FEE_TITLE where RECIEPT_CODE='" + recType + "' AND FEE_HEAD !='' AND FEE_LONGNAME <>''");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddAbstractBill(AccountTransaction objtran, string codeyear, string billNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", objtran.VOUCHER_TYPE);
                        objParams[1] = new SqlParameter("@P_AMOUNT", objtran.AMOUNT);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", objtran.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@P_AccountID", Convert.ToInt32(objtran.HEADACC));
                        objParams[4] = new SqlParameter("@P_GROSS_AMOUNT", objtran.GROSSAMOUNT);
                        objParams[5] = new SqlParameter("@P_Narration", objtran.NARRATION);
                        objParams[6] = new SqlParameter("@P_TRansaction_Date", DateTime.Now.ToString("dd-MMM-yyyy"));
                        objParams[7] = new SqlParameter("@P_Head_AccountId", Convert.ToInt32(objtran.HEADACCOUNTID));
                        objParams[8] = new SqlParameter("@P_Tot_Payable", Convert.ToDouble(objtran.TOTALPAYBLE));
                        objParams[9] = new SqlParameter("@P_COMP_CODE", codeyear);
                        objParams[10] = new SqlParameter("@P_ABS_BILL_NO", billNo);
                        objParams[11] = new SqlParameter("@P_ADVANCE_AMOUNT", objtran.AdvanceAmount);
                        objParams[12] = new SqlParameter("@P_Display_Name", objtran.DisplayName);
                        objParams[13] = new SqlParameter("@P_TAXPER", objtran.TaxPer);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSERT_ABSTRACT_BILL_DETAILS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());

                    }
                    return retStatus;
                }
                public int AddAbstractBillForPayment(AccountTransaction objtran, string codeyear, string billNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", objtran.VOUCHER_TYPE);
                        objParams[1] = new SqlParameter("@P_AMOUNT", objtran.AMOUNT);
                        objParams[2] = new SqlParameter("@P_GROSS_AMOUNT", objtran.GROSSAMOUNT);
                        objParams[3] = new SqlParameter("@P_Narration", objtran.NARRATION);
                        objParams[4] = new SqlParameter("@P_TRansaction_Date", DateTime.Now.ToString("dd-MMM-yyyy"));
                        objParams[5] = new SqlParameter("@P_AccountId", Convert.ToInt32(objtran.HEADACC));
                        objParams[6] = new SqlParameter("@P_Tot_Payable", Convert.ToDouble(objtran.TOTALPAYBLE));
                        objParams[7] = new SqlParameter("@P_COMP_CODE", codeyear);
                        objParams[8] = new SqlParameter("@P_ABS_BILL_NO", billNo);
                        objParams[9] = new SqlParameter("@P_Display_Name", objtran.DisplayName);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSERT_ABSTRACT_BILL_PAYMENT_DETAILS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public int AddAbstractBillForPayment_SetReport(AccountTransaction objtran, string codeyear, string billNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_COMP_CODE", objtran.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", objtran.BILLNO);
                        objParams[2] = new SqlParameter("@P_VOUCHER_TYPE", objtran.VOUCHER_TYPE);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSERT_ABSTRACT_BILL_PAYMENT_DETAILS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public int SetAbstractBillForPayment_Report(AccountTransaction objtran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_COMP_CODE", objtran.COMPANY_CODE);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", objtran.BILLNO);
                        objParams[2] = new SqlParameter("@P_VOUCHER_TYPE", objtran.VOUCHER_TYPE);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_ABSTRACT_BILL_REPORT_DATA_PAYMENT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public int AddAbstractBillForPaymentDeduction(AccountTransaction objtran, string codeyear, string billNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", objtran.VOUCHER_TYPE);
                        objParams[1] = new SqlParameter("@P_AMOUNT", objtran.AMOUNT);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", objtran.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@P_AccountID", Convert.ToInt32(objtran.HEADACC));
                        objParams[4] = new SqlParameter("@P_TRansaction_Date", DateTime.Now.ToString("dd-MMM-yyyy"));
                        objParams[5] = new SqlParameter("@P_Head_AccountId", Convert.ToInt32(objtran.HEADACCOUNTID));

                        objParams[6] = new SqlParameter("@P_COMP_CODE", codeyear);
                        objParams[7] = new SqlParameter("@P_ABS_BILL_NO", billNo);
                        objParams[8] = new SqlParameter("@P_Display_Name", objtran.DisplayName);
                        objParams[9] = new SqlParameter("@P_TAXPER", objtran.TaxPer);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSERT_ABSTRACT_BILL_PAYMENT_DEDUCTION_DETAILS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public void deleteBillNoForUpdate(int billNo, string CompCode, string Voucher_Type)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from acc_" + CompCode + "_PAYMENT_VOUCHER_TRANS_" + Voucher_Type + " where abs_bill_no=" + billNo);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                }

                public int AddMapping(AccountTransaction objTran, string compCode, string Tranasction_No)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_BILL_NO", objTran.BILLNO);
                        objParams[1] = new SqlParameter("@P_BANKNO", objTran.BANK_NO);
                        objParams[2] = new SqlParameter("@P_JVPARTY", objTran.JV_PARTY);
                        objParams[3] = new SqlParameter("@P_CHEQUE_NO", objTran.CHQ_NO);
                        if (objTran.CHQ_DATE != DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_CHEQUE_DATE", objTran.CHQ_DATE);
                        else
                            objParams[4] = new SqlParameter("@P_CHEQUE_DATE", "0");
                        objParams[5] = new SqlParameter("@P_DEDUCT_HEAD_NO", objTran.HEADACCOUNTID);
                        objParams[6] = new SqlParameter("@P_LEDGER_NO", objTran.PARTY_NO);
                        objParams[7] = new SqlParameter("@P_VOUCHER_TYPE", objTran.VOUCHER_TYPE);
                        objParams[8] = new SqlParameter("@P_COMPCODE", compCode);
                        objParams[9] = new SqlParameter("@P_Tranasction_No", Tranasction_No);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MAP_ABSTRACT_VOUCHER", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddAbsractTransferEntry(AccountTransaction objTran, string compCode)
                {
                    int retStatus;
                    object ret = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", objTran.VOUCHER_TYPE);
                        objParams[1] = new SqlParameter("@P_ABS_BILL_NO", objTran.BILLNO);
                        objParams[2] = new SqlParameter("@P_TRANSACTION_DATE", DateTime.Now.ToString("dd-MMM-yyyy"));

                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (objTran.VOUCHER_TYPE == "PV")
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_ABSTRACT_BILL_TRANSFER", objParams, true);
                        else if (objTran.VOUCHER_TYPE == "AV" || objTran.VOUCHER_TYPE == "APV")
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_ABSTRACT_BILL_TRANSFER_AV_APV", objParams, true);
                        else if (objTran.VOUCHER_TYPE == "AAPV")
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_ABSTRACT_BILL_TRANSFER_AAPV", objParams, true);


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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetError_TypeWise(string sessionComp, int i)
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    DataSet DS = null;
                    if (i == 3)
                    {
                        SqlParameter[] Param = new SqlParameter[1];
                        Param[0] = new SqlParameter("@P_COMPCODE", sessionComp);
                        DS = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_ERRORTYPE_OPARTY_MISMATCH", Param);
                    }
                    else
                    {
                        SqlParameter[] Param = new SqlParameter[2];
                        Param[0] = new SqlParameter("@P_COMPCODE", sessionComp);
                        Param[1] = new SqlParameter("@P_TYPE", i);
                        DS = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_ERROR_TYPEWISE", Param);
                    }

                    return DS;
                }

                public DataSet GetSupliPayHead(string sessionComp)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        DataSet DS = null;

                        SqlParameter[] Param = new SqlParameter[1];

                        Param[0] = new SqlParameter("@P_COMMANDTYPE", sessionComp);

                        DS = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_SUPPLIMENTRY_HEAD_DETAILS", Param);

                        return DS;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                }

                public DataSet GetSuplitransferData(string P_COMPANY_CODE, int P_STAFF_NO, int P_COLLEGE_NO, string P_MONYEAR, int P_BANKNO, string FromDate, string Totdate, int type)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        DataSet DS = null;

                        SqlParameter[] Param = new SqlParameter[7];

                        Param[0] = new SqlParameter("@P_COMPANY_CODE", P_COMPANY_CODE);
                        Param[1] = new SqlParameter("@P_STAFF_NO", P_STAFF_NO);
                        Param[2] = new SqlParameter("@P_COLLEGE_NO", P_COLLEGE_NO);
                        Param[3] = new SqlParameter("@P_BANKNO", P_BANKNO);
                        Param[4] = new SqlParameter("@P_FROMDATE", FromDate);
                        Param[5] = new SqlParameter("@P_TODATE", Totdate);
                        Param[6] = new SqlParameter("@P_TYPE", type);

                        DS = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_SUPPLIMENTRY_EMPWISE_DATA", Param);

                        return DS;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                }

                public DataSet GetSuplitransferDataDelete(string P_COMPANY_CODE, int P_STAFF_NO, int P_COLLEGE_NO, string P_MONYEAR, int P_BANKNO, string FromDate, string Totdate, int type)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        DataSet DS = null;

                        SqlParameter[] Param = new SqlParameter[7];

                        Param[0] = new SqlParameter("@P_COMPANY_CODE", P_COMPANY_CODE);
                        Param[1] = new SqlParameter("@P_STAFF_NO", P_STAFF_NO);
                        Param[2] = new SqlParameter("@P_COLLEGE_NO", P_COLLEGE_NO);
                        Param[3] = new SqlParameter("@P_BANKNO", P_BANKNO);
                        Param[4] = new SqlParameter("@P_FROMDATE", FromDate);
                        Param[5] = new SqlParameter("@P_TODATE", Totdate);
                        Param[6] = new SqlParameter("@P_TYPE", type);

                        DS = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_SUPPLIMENTRY_EMPWISE_DATA_FOR_DELETE", Param);

                        return DS;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
                    }
                }


                public DataSet PopulateEmpPayroll(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select * from PAYROLL_STAFF where STAFFNO<>0");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetEmpPayHeads(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        //ds = objSQLHelper.ExecuteDataSet("select * from (SELECT 'PAY' PAYHEAD,'PAY' FULLNAME,0 SRNO FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'' UNION  SELECT PAYHEAD,PAYFULL AS FULLNAME,SRNO FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'')A  order by SRNO");

                        ds = objSQLHelper.ExecuteDataSet("select * from (SELECT 'PAY' PAYHEAD,'PAY' FULLNAME,0 SRNO FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'-' UNION  SELECT PAYHEAD,PAYFULL AS FULLNAME,SRNO FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'-')A  order by SRNO");

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetMonth(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("select DISTINCT MONYEAR from PAYROLL_SALFILE ORDER BY MONYEAR DESC");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }
                //public int AddDayBookReportFormat(int RptId, string TranDate, string particulars, string VchType, string VchNo, double Debit, double Credit, int Bold, string party_no, string Narration)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;


                //        objParams = new SqlParameter[10];

                //        objParams[0] = new SqlParameter("@P_RPT_ID", RptId);
                //        objParams[1] = new SqlParameter("@P_TRAN_DATE", TranDate);
                //        objParams[2] = new SqlParameter("@P_PARTICULARS", particulars);
                //        objParams[3] = new SqlParameter("@P_VCH_TYPE", VchType);
                //        objParams[4] = new SqlParameter("@P_VCH_NO", VchNo);
                //        objParams[5] = new SqlParameter("@P_DEBIT", Debit);
                //        objParams[6] = new SqlParameter("@P_CREDIT", Credit);
                //        objParams[7] = new SqlParameter("@P_BOLD", Bold);
                //        objParams[8] = new SqlParameter("@P_PARTY_NO", party_no);
                //        objParams[9] = new SqlParameter("@P_NARRATION", Narration);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_DAYBOOK_RECORD_INS", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddDayBookReportFormat-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                //public DataSet GetTransactionResult(int VoucherNo, string code_year)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[2];

                //        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                //        objParams[1] = new SqlParameter("@P_VCH_NO", VoucherNo);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_TRANSACTION_RESULT", objParams);

                //        objParams[8] = new SqlParameter("@P_TRANSFER_ENTRY", objParty.TRANSFER_ENTRY);
                //        objParams[9] = new SqlParameter("@P_CBTYPE_STATUS", objParty.CBTYPE_STATUS);
                //        objParams[10] = new SqlParameter("@P_CBTYPE", objParty.CBTYPE);
                //        objParams[11] = new SqlParameter("@P_RECIEPT_PAYMENT_FEES", objParty.RECIEPT_PAYMENT_FEES);
                //        objParams[12] = new SqlParameter("@P_REC_NO", objParty.REC_NO);
                //        objParams[13] = new SqlParameter("@P_CHQ_NO", objParty.CHQ_NO);
                //        if (objParty.CHQ_DATE == DateTime.MinValue)
                //            objParams[14] = new SqlParameter("@P_CHQ_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                //        else
                //            objParams[14] = new SqlParameter("@P_CHQ_DATE", objParty.CHQ_DATE.ToString("dd-MMM-yyyy"));


                //        objParams[15] = new SqlParameter("@P_CHALLAN", objParty.CHALLAN);
                //        objParams[16] = new SqlParameter("@P_CAN", objParty.P_CAN1);

                //        if (objParty.RECON_DATE == DateTime.MinValue)
                //            objParams[17] = new SqlParameter("@P_RECON_DATE", objParty.TRANSACTION_DATE.ToString("dd-MMM-yyyy"));
                //        else
                //            objParams[17] = new SqlParameter("@P_RECON_DATE", objParty.RECON_DATE.ToString("dd-MMM-yyyy"));


                //        objParams[18] = new SqlParameter("@P_DCR_NO", objParty.DCR_NO);

                //        objParams[19] = new SqlParameter("@P_IDF_NO", objParty.IDF_NO);

                //        objParams[20] = new SqlParameter("@P_CASH_BANK_NO", objParty.CASH_BANK_NO);

                //        objParams[21] = new SqlParameter("@P_ADVANCE_REFUND_NONE", objParty.ADVANCE_REFUND_NONE);

                //        objParams[22] = new SqlParameter("@P_PAGENO", objParty.PAGENO);

                //        objParams[23] = new SqlParameter("@P_PARTICULARS", objParty.PARTICULARS);

                //        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objParty.COLLEGE_CODE);

                //        objParams[25] = new SqlParameter("@P_USER", objParty.USER);

                //        objParams[26] = new SqlParameter("@P_CREATED_MODIFIED_DATE", objParty.CREATED_MODIFIED_DATE.ToString("dd-MMM-yyyy"));

                //        objParams[27] = new SqlParameter("@P_COMPANY_CODE", objParty.COMPANY_CODE);

                //        objParams[28] = new SqlParameter("@P_YEAR", objParty.P_YEAR);
                //        objParams[29] = new SqlParameter("@P_CBALANCE", CurrentBalance);
                //        objParams[30] = new SqlParameter("@P_OPARTY", objParty.OPARTY_NO);
                //        objParams[31] = new SqlParameter("@P_STR_VOUCHER_NO", objParty.STR_VOUCHER_NO);
                //        objParams[32] = new SqlParameter("@P_STR_CB_VOUCHER_NO", objParty.STR_CB_VOUCHER_NO);
                //        objParams[33] = new SqlParameter("@P_TRANSACTION_NO", SqlDbType.Int);
                //        objParams[33].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //public int AddTransactionWithXML(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    object ret;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;


                //        objParams = new SqlParameter[3];

                //        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                //        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                //        objParams[2] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                //        objParams[2].Direction = ParameterDirection.Output;

                //        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                //        if (IsModified == "Y")
                //            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                //        else
                //            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //            else

                //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                //    }
                //    return Convert.ToInt32(ret.ToString());
                //}


                public int AddTransactionWithXML(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                        if (IsModified == "Y")
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                        else
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);

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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }




                public int AddTransactionWithXMLForManual(System.Xml.XmlDocument objXMLDoc, string compcode, int VoucherSqn, string IsModify, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];


                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);

                        objParams[2] = new SqlParameter("@P_VOUCHER_SQN", VoucherSqn);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (IsModify == "Y")
                        {
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MANUAL_VCHNO_UPDATE", objParams, true);
                        }
                        else
                        {
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MANUAL_VCHNO", objParams, true);
                        } if (ret != null)
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                //added bu vijay andoju for transaction
                public int AddBudgetTransaction(string compcode, string VoucherNo, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_TRANSACTION", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public int UpdateBalanceAllLedger()
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPCODE", HttpContext.Current.Session["comp_code"].ToString());
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPDATE_OPENING_BALANCE", objParams, true);
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

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetPayHeadEmployer(string comp_code, string month, string Database)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", comp_code);
                        objParams[1] = new SqlParameter("@P_MONTH", month);
                        objParams[2] = new SqlParameter("@P_DATABASE", Database);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_EMPLOYEE_SHARE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }


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

                // Used for Binding College Dropdown in Payroll Ledger mapping form

                public DataSet GetCollegeForPayHeadmapping(string con)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        ds = objSQLHelper.ExecuteDataSet("SELECT COLLEGE_ID, COLLEGE_NAME FROM ACD_COLLEGE_MASTER");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateReceiptType-> " + ex.ToString());
                    }
                    return ds;
                }


                //Added By Mr. Nokhlal Kumar
                // This Method is Used to the Form Acc_Account_Master Page.....
                public int AddUpdateCompAccountMaster(int Acc_Id, string AccName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_Acc_Id", Acc_Id);
                        objParams[1] = new SqlParameter("@P_Account_Name", AccName);
                        objParams[2] = new SqlParameter("@P_IsActive", 1);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[4] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["colcode"].ToString());
                        objParams[5] = new SqlParameter("@P_UserNo", HttpContext.Current.Session["userno"].ToString());
                        objParams[6] = new SqlParameter("@P_Out", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_COMP_ACCOUNT_MASTER", objParams, true);
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

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //Added By Mr. Nokhlal Kumar
                //This Method is used to Get Alert Details
                public DataSet GetAlertDetails(int alertId, string comp_code)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ALERTID", alertId);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", comp_code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_ALERT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAlertDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By Mr. Nokhlal Kumar
                //This Method is used to Insert Alert Details
                public int InsertUpdAlertDetails(int AlertId, string Comp_Code, string AlertName, int LedgerNo, DataTable dtLIst)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ALERTID", AlertId);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[2] = new SqlParameter("@P_ALERTNAME", AlertName);
                        objParams[3] = new SqlParameter("@P_LEDGERNO", LedgerNo);
                        objParams[4] = new SqlParameter("@P_ALERT_TRAN", dtLIst);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", HttpContext.Current.Session["colcode"].ToString());
                        objParams[6] = new SqlParameter("@P_USERNO", HttpContext.Current.Session["userno"].ToString());
                        objParams[7] = new SqlParameter("@P_Out", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSUPD_ALERT", objParams, true);
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

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IncomeExpenBalanceSheetController.AddShedule-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //Toget the balance budget 
                //Added by vijay andoju for budget balance show on 01102020
                public DataSet GetBudegetBalanceNEW(int Dept_No, int Budget_No, DateTime Date)
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

                //ADDED BY VIJAY ANDOJU ON 290820202 FOR FIXED DIPOSIT
                public int AddTransactionWithXMLforFIXEDDEPOSIT(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                        if (IsModified == "Y")
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                        else
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_FIXED_DEPOSIT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }


                //ADDE BY VIJAY ANDOJU ON 08102020 TO GET INVOICE DETAILS

                public DataSet GetInvoiceDetails()
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        // objParams[0] = new SqlParameter("@P_INVOICE", InvoiceNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_INVOICE_DEATILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAlertDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                //ADDE BY VIJAY ANDOJU ON 08102020 TO GET INVOICE DETAILS

                public DataSet GetInvoiceTaxDetails(int InvoiceNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", InvoiceNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_TAX_STORE_INVOICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAlertDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddBudgetTransactionForStore(string compcode, string VoucherNo, string VCH_TYPE, int Invtrno, int Pno, string Pamount, int Party_number)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_INVTRNO", Invtrno);
                        objParams[4] = new SqlParameter("@P_PNO", Pno);
                        objParams[5] = new SqlParameter("@P_PARTY_AMOUNT", Pamount);
                        objParams[6] = new SqlParameter("@P_PARTY_NO", Party_number);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_STORETRANSACTION", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public int AddTransactionPaymentStore(string compcode, string VoucherNo, string VCH_TYPE, int Vpid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_VPID", Vpid);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INS_STR_PAYMENT_TRANS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public DataSet GetPaymentInvoice(int INVTRNO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_INVOICE_PAYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAlertDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : AddTransactionWithXMLMakaut
                /// USED FOR : Save the data in ACC_ALL_TEMP_TRANS table for Makaut
                /// PARAMETER : System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE
                /// 
                /// </summary>
                public int AddTransactionWithXMLMakaut(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                        if (IsModified == "Y")
                            // ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO_MAKAUT", objParams, true);
                        else
                            //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MAKAUT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : GetTransactionForModificationMakaut
                /// USED FOR : GETTING DATA FOR VOUCHER MODIFICATION
                /// PARAMETER : int VOUCHER_NO, string code_year, string VCH_TYPE
                /// </summary>
                public DataSet GetTransactionForModificationMakaut(int VOUCHER_NO, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_DETAILS_UPDATE_MAKAUT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : GetTransactionResultMakaut
                /// USED FOR : GETTING RESULT DATA AFTER VOUCHER CREATION
                /// PARAMETER : int VOUCHER_NO, string code_year, string VCH_TYPE
                /// </summary>
                public DataSet GetTransactionResultMakaut(int VoucherNo, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_TRANSACTION_RESULT_MAKAUT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionResult-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : AddBudgetTransaction
                /// USED FOR : USED TO ADD AND MODIFY THE BUDGET TRANSACTION TABLE
                /// PARAMETER : string compcode, string VoucherNo, string VCH_TYPE
                /// </summary>
                public int AddBudgetTransactionMakaut(string compcode, string VoucherNo, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_TRANSACTION_MAKAUT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : InsertBulkEmployeeVoucherData
                /// USED FOR : USED TO ADD AND MODIFY Bulk Employee Voucher Data
                /// PARAMETER : string compcode, string VoucherNo, string VCH_TYPE
                /// </summary>
                public int InsertBulkEmployeeVoucherData(DataTable EmpData, int PARTY_NO1, string TRAN1, int PARTY_NO2, string TRAN2, int PARTY_NO3, string TRAN3, int VOUCHER_SQN, DateTime TRAN_DATE, string COMP_CODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@ACC_ALL_TEMP_TRANS_EMP_DATA", EmpData);
                        objParams[1] = new SqlParameter("@PARTY_NO1", PARTY_NO1);
                        objParams[2] = new SqlParameter("@TRAN1", TRAN1);
                        objParams[3] = new SqlParameter("@PARTY_NO2", PARTY_NO2);
                        objParams[4] = new SqlParameter("@TRAN2", TRAN2);
                        objParams[5] = new SqlParameter("@PARTY_NO3", PARTY_NO3);
                        objParams[6] = new SqlParameter("@TRAN3", TRAN3);
                        objParams[7] = new SqlParameter("@VOUCHER_SQN", VOUCHER_SQN);
                        objParams[8] = new SqlParameter("@P_CompCode", COMP_CODE);
                        objParams[9] = new SqlParameter("@P_TransactionDate", TRAN_DATE);

                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BULK_VOUCHER_INSERT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }


                //ADDED BY AKSHAY DIXIT ON 18-07-2022 TO ADD DATA IN MAIN TABLE WHICH HAVE CONFIG "ISTEMPVOUCHER=N" 
                public int InsertBulkEmployeeVoucherDataNew(DataTable EmpData, int PARTY_NO1, string TRAN1, int PARTY_NO2, string TRAN2, int PARTY_NO3, string TRAN3, int VOUCHER_SQN, DateTime TRAN_DATE, string COMP_CODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@ACC_ALL_MAIN_TRANS_EMP_DATA", EmpData);
                        objParams[1] = new SqlParameter("@PARTY_NO1", PARTY_NO1);
                        objParams[2] = new SqlParameter("@TRAN1", TRAN1);
                        objParams[3] = new SqlParameter("@PARTY_NO2", PARTY_NO2);
                        objParams[4] = new SqlParameter("@TRAN2", TRAN2);
                        objParams[5] = new SqlParameter("@PARTY_NO3", PARTY_NO3);
                        objParams[6] = new SqlParameter("@TRAN3", TRAN3);
                        objParams[7] = new SqlParameter("@VOUCHER_SQN", VOUCHER_SQN);
                        objParams[8] = new SqlParameter("@P_CompCode", COMP_CODE);
                        objParams[9] = new SqlParameter("@P_TransactionDate", TRAN_DATE);

                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BULK_VOUCHER_INSERT_NEW", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }


                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 02-08-2021
                /// METHOD NAME : AddTransactionWithXMLMakautAdvance
                /// USED FOR : Save the data in ACC_ALL_TEMP_TRANS table for Advance Voucher Makaut
                /// PARAMETER : System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE
                /// 
                /// </summary>
                public int AddTransactionWithXMLMakautAdvance(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                        if (IsModified == "Y")
                            // ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO_MAKAUT", objParams, true);
                        else
                            //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MAKAUT_ADVANCE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                //Added by gopal anthati on 26/08/2021
                public DataSet GetApprovedVPDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_VP_APPROVED_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetApprovedVPDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVendorPaymentDetails(int VpId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VPID", VpId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_VENDOR_PAYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetVendorPaymentDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetApprovedCondmnedSaleDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_CONDMNED_SALE_APPROVED_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetApprovedCondmnedSaleDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCondmnedSaleAmountById(int CisId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CISID", CisId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_CONDMNED_SALE_AMOUNT_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCondmnedSaleAmountById-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddTransactionReceiptStore(string compcode, string VoucherNo, string VCH_TYPE, int CisId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", compcode);
                        objParams[1] = new SqlParameter("@P_VOUCHER_NO", VoucherNo);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", VCH_TYPE);
                        objParams[3] = new SqlParameter("@P_CISID", CisId);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INS_STR_CONDMNED_SALE_TRANS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransactionReceiptStore-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public int TagUserForVoucherEntry(AccountTransaction objATEnt, string IsModify)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_ISEMPLOYEE", objATEnt.ISEMPLOYEE);
                        objParams[1] = new SqlParameter("@P_IDNO", objATEnt.IDNO);
                        objParams[2] = new SqlParameter("@P_TRANSACTION_TYPE", objATEnt.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@P_TRAN", objATEnt.TRAN);
                        objParams[4] = new SqlParameter("@P_VOUCHER_NO", objATEnt.VOUCHER_NO);
                        objParams[5] = new SqlParameter("@P_VOUCHER_SQN", objATEnt.VOUCHER_SQN);
                        objParams[6] = new SqlParameter("@P_PARENTID", objATEnt.PARENTID);
                        objParams[7] = new SqlParameter("@P_LEDGER_ID", objATEnt.PARTY_NO);
                        objParams[8] = new SqlParameter("@P_AMOUNT", objATEnt.AMOUNT);
                        objParams[9] = new SqlParameter("@P_BALANCEAMOUNT", objATEnt.BALANCEAMOUNT);
                        objParams[10] = new SqlParameter("@P_NARRATION", objATEnt.NARRATION);
                        objParams[11] = new SqlParameter("@P_COMPANY_CODE", objATEnt.COMPANY_CODE);
                        objParams[12] = new SqlParameter("@P_TRANSACTION_DATE", objATEnt.TRANSACTION_DATE);
                        objParams[13] = new SqlParameter("@P_CREATEDBY", objATEnt.CREATEDBY);
                        objParams[14] = new SqlParameter("@P_MODIFIEDBY", objATEnt.MODIFIEDBY);
                        objParams[15] = new SqlParameter("@P_ISMODIFY", IsModify);
                        objParams[16] = new SqlParameter("@P_OLD_VOUCHER_SQN", objATEnt.OLD_VOUCHER_SQN);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_TAG_USER_INS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransactionReceiptStore-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                public DataSet BindLedgers(int ProjectId, int SubProjectId, string CompCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ProjectId", ProjectId);
                        objParams[1] = new SqlParameter("@P_SubProjectId", SubProjectId);
                        objParams[2] = new SqlParameter("@P_CompCode", CompCode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_LEDGERS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.BindLedgers-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAdvanceVoucherStmntForExcel(string FromDate, string UpToDate ,int UserType, int Idno,string Comp_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[1] = new SqlParameter("@P_UPTO_DATE", UpToDate);
                        objParams[2] = new SqlParameter("@P_USER_TYPE", UserType);
                        objParams[3] = new SqlParameter("@P_IDNO", Idno);
                        objParams[4] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GENERATE_ADVANCE_STATEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetAdvanceVoucherStmntForExcel-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 03-09-2021
                /// METHOD NAME : GetEmployeeWiseAmountDetails
                /// USED FOR : Fetch Amount Sum According to employee
                /// PARAMETER : string Comp_Code, int EmpId
                /// 
                /// </summary>
                public DataSet GetEmployeeWiseAmountDetails(string Comp_Code, int EmpId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[1] = new SqlParameter("@EMP_ID", EmpId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_VOUCHER_TOTAL_AMT_EMP_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetEmployeeWiseAmountDetails-> " + ex.ToString());
                    }
                    return ds;
                }



                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 03-09-2021
                /// METHOD NAME : AddTransactionWithXMLMakautBulk
                /// USED FOR : Save the data in ACC_ALL_TEMP_TRANS table for Bulk Voucher Makaut
                /// PARAMETER : System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE
                /// 
                /// </summary>
                public int AddTransactionWithXMLMakautBulk(System.Xml.XmlDocument objXMLDoc, string compcode, string IsModified, int VoucherNo, string code_year, string VCH_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;



                        objParams = new SqlParameter[5];



                        objParams[0] = new SqlParameter("@XML", objXMLDoc.InnerXml);
                        objParams[1] = new SqlParameter("@P_CompCode", compcode);
                        objParams[2] = new SqlParameter("@P_VCH_NO", VoucherNo);
                        objParams[3] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);
                        objParams[4] = new SqlParameter("@P_VoucherNo", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;



                        //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                        if (IsModified == "Y")
                            // ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_FIXED_VOUCHERNO_MAKAUT", objParams, true);
                        else
                            //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW", objParams, true);
                            //ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MAKAUT", objParams, true);
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_INSERT_NEW_MAKAUT_BULK", objParams, true);



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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }




                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 28-09-2021
                /// METHOD NAME : GetBulkPaymentDetailsInfo
                /// USED FOR : Get Voucher Type wise data
                /// PARAMETER : string Voucher_Type, DateTime Date, string Comp_Code
                /// 
                /// </summary>
                public DataSet GetBulkPaymentDetailsInfo(string Voucher_Type, DateTime Date, string Comp_Code, int Party_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", Voucher_Type);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[2] = new SqlParameter("@P_TRANSACTIONDATE", Date);
                        objParams[3] = new SqlParameter("@P_PARTY_NO", Party_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_VOUCHER_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBulkPaymentDetailsInfo-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 28-09-2021
                /// METHOD NAME : GetBulkPaymentDetailsInfo
                /// USED FOR : Get Voucher Type wise data
                /// PARAMETER : string Voucher_Type, DateTime Date, string Comp_Code
                /// 
                /// </summary>
                public DataSet GetBulkPaymentEmployeeDetails(string VoucherSqn, string Comp_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VOUCHER_SQN", VoucherSqn);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BULK_PAYMENT_BANK_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBulkPaymentEmployeeDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 29-09-2021
                /// METHOD NAME : InsertBulkEmployeeVoucherChequeNo
                /// USED FOR : USED TO ADD AND MODIFY Bulk Employee Voucher Data
                /// PARAMETER : string compcode, string VoucherNo, string VOUCHER_SQN
                /// </summary>
                public int InsertBulkEmployeeVoucherChequeNo(DataTable EmpData, int VOUCHER_SQN, int VOUCHER_NO, string COMP_CODE, DateTime CHEQUEDATE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@VOUCHER_NO", VOUCHER_NO);
                        objParams[1] = new SqlParameter("@VOUCHER_SQN", VOUCHER_SQN);
                        objParams[2] = new SqlParameter("@BULK_UPDATE_CHEQUENO", EmpData);
                        objParams[3] = new SqlParameter("@COMP_CODE", COMP_CODE);
                        objParams[4] = new SqlParameter("@CHEQUEDATE", CHEQUEDATE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BULK_VOUCHER_CHEQUENO_UPDATE_EMPWISE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransaction-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 29-09-2021
                /// METHOD NAME : InsertBulkVoucherChequeNo
                /// USED FOR : USED TO ADD AND MODIFY Bulk Employee Voucher Data
                /// PARAMETER : string compcode, string VoucherNo, string VOUCHER_SQN
                /// </summary>
                public int InsertBulkVoucherChequeNo(DataTable VoucherData, string COMP_CODE, DateTime CHEQUEDATE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        // objParams[0] = new SqlParameter("@VOUCHER_NO", VOUCHER_NO);
                        // objParams[1] = new SqlParameter("@VOUCHER_SQN", VOUCHER_SQN);
                        objParams[0] = new SqlParameter("@CHEQUEDATE", CHEQUEDATE);
                        objParams[1] = new SqlParameter("@COMP_CODE", COMP_CODE);
                        objParams[2] = new SqlParameter("@BULK_UPDATE_CHEQUENO_MAIN", VoucherData);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_BULK_VOUCHER_CHEQUENO_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertBulkVoucherChequeNo-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                /// <summary>
                /// CREATED BY : VIDISHA KAMATKAR
                /// CREATED DATE : 29-09-2021
                /// METHOD NAME : InsertBulkVoucherChequeNo
                /// USED FOR : USED TO ADD AND MODIFY Bulk Employee Voucher Data
                /// PARAMETER : string compcode, string VoucherNo, string VOUCHER_SQN
                /// </summary>
                public int InsertOtherVoucherChequeNo(DataTable VoucherData, string COMP_CODE, DateTime CHEQUEDATE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        // objParams[0] = new SqlParameter("@VOUCHER_NO", VOUCHER_NO);
                        // objParams[1] = new SqlParameter("@VOUCHER_SQN", VOUCHER_SQN);
                        objParams[0] = new SqlParameter("@CHEQUEDATE", CHEQUEDATE);
                        objParams[1] = new SqlParameter("@COMP_CODE", COMP_CODE);
                        objParams[2] = new SqlParameter("@BULK_UPDATE_CHEQUENO_MAIN", VoucherData);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_OTHER_VOUCHER_CHEQUENO_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertBulkVoucherChequeNo-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }
               
                //added by tanu 13/12/2021 for add multiple bill in voucher creation 

                public int AddBillVoucherCreation(AccountTransaction objRPB, int Userno, DataTable dtDoc, string Comp_Code, int Comp_No, string ConfigIsTempVoucher)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_VOUCHER_NO", objRPB.VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_VOUCHER_SQN", objRPB.VOUCHER_SQN);
                        objParams[2] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[3] = new SqlParameter("@P_COMPANY_CODE", Comp_Code);
                        objParams[4] = new SqlParameter("@P_COMPANY_NO", Comp_No);
                        objParams[5] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH);
                        objParams[6] = new SqlParameter("@P_ACC_VOUCHER_CREATION_UPLOAD_DOC", dtDoc);
                        objParams[7] = new SqlParameter("@P_OLD_VOUCHER_SQN", objRPB.OLD_VOUCHER_SQN);
                        objParams[8] = new SqlParameter("@P_Config_IsTempVoucher", ConfigIsTempVoucher);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_BILL_VOUCHER_CREATION", objParams, true);

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
                                objRPB.VOUCHER_NO = Convert.ToInt32(ret);
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

                public int AddBillBulkVoucherCreation(AccountTransaction objRPB, int Userno, DataTable dtDoc, string Comp_Code, int Comp_No)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_VOUCHER_NO", objRPB.VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_VOUCHER_SQN", objRPB.VOUCHER_SQN);
                        objParams[2] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[3] = new SqlParameter("@P_COMPANY_CODE", Comp_Code);
                        objParams[4] = new SqlParameter("@P_COMPANY_NO", Comp_No);
                        objParams[5] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH);
                        objParams[6] = new SqlParameter("@P_ACC_VOUCHER_CREATION_UPLOAD_DOC", dtDoc);
                        objParams[7] = new SqlParameter("@P_OLD_VOUCHER_SQN", objRPB.OLD_VOUCHER_SQN);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_BILL_BULK_VOUCHER_CREATION", objParams, true);

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
                                objRPB.VOUCHER_NO = Convert.ToInt32(ret);
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

                public int Addbillmodification(AccountTransaction objRPB, int Userno, DataTable dtDoc, string Comp_Code, int Comp_No, string IsBulk)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_VOUCHER_NO", objRPB.VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_VOUCHER_SQN", objRPB.VOUCHER_SQN);
                        objParams[2] = new SqlParameter("@P_USER_NO", Userno);
                        objParams[3] = new SqlParameter("@P_COMPANY_CODE", Comp_Code);
                        objParams[4] = new SqlParameter("@P_COMPANY_NO", Comp_No);
                        objParams[5] = new SqlParameter("@P_FILEPATH", objRPB.FILEPATH);
                        objParams[6] = new SqlParameter("@P_IsBulk", IsBulk);
                        objParams[7] = new SqlParameter("@P_ACC_VOUCHER_CREATION_UPLOAD_DOC", dtDoc);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_UPD_BILL_BULK_VOUCHER_MODIFICATION", objParams, true);

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
                                objRPB.VOUCHER_NO = Convert.ToInt32(ret);
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
                //Added by gopal anthati on 06/01/2021
                #region Online Payment 

                public DataSet GetVouchers(String FromDate, String ToDate, int Party_no, String CompCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", ToDate);
                        objParams[2] = new SqlParameter("@P_PARTY_NO", Party_no);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", CompCode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_BULK_VOUCHERS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBulkPaymentEmployeeDetails-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetBulkVoucherPaymentDetails(string VoucherSqn, string Comp_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VOUCHER_SQN", VoucherSqn);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_BULK_VOUCHERS_FOR_PAYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBulkPaymentEmployeeDetails-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetVoucherPaymentDetails(string Voucher_Type, String FromDate, String ToDate, string Comp_Code, int Party_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_VOUCHER_TYPE", Voucher_Type);
                        objParams[1] = new SqlParameter("@P_COMP_CODE", Comp_Code);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[3] = new SqlParameter("@P_TO_DATE", ToDate);
                        objParams[4] = new SqlParameter("@P_PARTY_NO", Party_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_BIND_VOUCHER_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBulkPaymentDetailsInfo-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetPaymentTrnDetails(string Voucher_No, string Voucher_Sqn, string Voucher_Type, string EmpDataId,string comp_code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_VOUCHER_NO", Voucher_No);
                        objParams[1] = new SqlParameter("@P_VOUCHER_SQN", Voucher_Sqn);
                        objParams[2] = new SqlParameter("@P_VOUCHER_TYPE", Voucher_Type);
                        objParams[3] = new SqlParameter("@P_EMPDATAID", EmpDataId);
                        objParams[4] = new SqlParameter("@P_COMP_CODE", comp_code);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_PAYMENT_TRAN_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetPaymentTrnDetails-> " + ex.ToString());
                    }

                    return ds;
                }
                public int UpdateTranStatus(string BenficiaryCode, string BenficiaryAccNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_BenficiaryCode", BenficiaryCode);
                        objParams[1] = new SqlParameter("@P_BenficiaryAccNo", BenficiaryAccNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPDATE_ONLINE_PAYMENT_TRAN_STATUS", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.InsertBulkVoucherChequeNo-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                #endregion
                //end

                //ADDED BY TANU 07/01/2022 FOR TRACK VOUCHER REPORT
                public DataSet GetVouchertrackList(string Status, string FromDate, string Todate, string Comp_Code)
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

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_VOUCHER_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RaisingPaymentBillController.GetBillList-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// CREATED BY : Tanu Balgote
                /// CREATED DATE : 15-07-2022
                /// METHOD NAME : TagUserForDirectVoucherEntry
                /// USED FOR : USED TO ADD AND MODIFY Direct Tag User Data
                /// PARAMETER : string compcode, string VoucherNo, string VOUCHER_SQN
                /// </summary>
                public int TagUserForDirectVoucherEntry(AccountTransaction objATEnt, string IsModify)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_ISEMPLOYEE", objATEnt.ISEMPLOYEE);
                        objParams[1] = new SqlParameter("@P_IDNO", objATEnt.IDNO);
                        objParams[2] = new SqlParameter("@P_TRANSACTION_TYPE", objATEnt.TRANSACTION_TYPE);
                        objParams[3] = new SqlParameter("@P_TRAN", objATEnt.TRAN);
                        objParams[4] = new SqlParameter("@P_VOUCHER_NO", objATEnt.VOUCHER_NO);
                        objParams[5] = new SqlParameter("@P_VOUCHER_SQN", objATEnt.VOUCHER_SQN);
                        objParams[6] = new SqlParameter("@P_PARENTID", objATEnt.PARENTID);
                        objParams[7] = new SqlParameter("@P_LEDGER_ID", objATEnt.PARTY_NO);
                        objParams[8] = new SqlParameter("@P_AMOUNT", objATEnt.AMOUNT);
                        objParams[9] = new SqlParameter("@P_BALANCEAMOUNT", objATEnt.BALANCEAMOUNT);
                        objParams[10] = new SqlParameter("@P_NARRATION", objATEnt.NARRATION);
                        objParams[11] = new SqlParameter("@P_COMPANY_CODE", objATEnt.COMPANY_CODE);
                        objParams[12] = new SqlParameter("@P_TRANSACTION_DATE", objATEnt.TRANSACTION_DATE);
                        objParams[13] = new SqlParameter("@P_CREATEDBY", objATEnt.CREATEDBY);
                        objParams[14] = new SqlParameter("@P_MODIFIEDBY", objATEnt.MODIFIEDBY);
                        objParams[15] = new SqlParameter("@P_ISMODIFY", IsModify);
                        objParams[16] = new SqlParameter("@P_OLD_VOUCHER_SQN", objATEnt.OLD_VOUCHER_SQN);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_TAG_USER_INS_MAIN", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddTransactionReceiptStore-> " + ex.ToString());
                    }
                    return Convert.ToInt32(ret.ToString());
                }

                /// <summary>
                /// CREATED BY : Tanu Balgote
                /// CREATED DATE : 15-07-2022
                /// METHOD NAME : GetTransactionForVoucherModification
                /// USED FOR : USED  MODIFY  DIRECT VOUCHER 
                /// PARAMETER : string compcode, string VoucherNo, string VOUCHER_SQN
                /// </summary>
                public DataSet GetTransactionForVoucherModification(int VOUCHER_NO, string code_year, string VCH_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_VCH_NO", VOUCHER_NO);
                        objParams[1] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[2] = new SqlParameter("@P_VCH_TYPE", VCH_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_LEDGER_DETAILS_UPDATE_MODIFICATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetTransactionForModification-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCashAndBankAmountForFeesTransfer(string frmDate, string toDate, string con, string paytype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE between '" + frmDate + "' and '" + toDate + "'  and can=0 and CHDD='C' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where CAST(MISCRECPTDATE AS DATE) between '" + frmDate + "' and '" + toDate + "' and   CHDD='C' AND ISNULL(MD.IsTransfer,0) != 1 AND PAY_TYPE='" + paytype + "' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCashAmountForFeesTransfer-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCashAmountFormisFeesTransfer(string frmDate, string toDate, string con)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        ////For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS

                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select  MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE between '" + frmDate + "' and '" + toDate + "' and can=0 and CHDD='C' and RECIEPT_CODE='MF' GROUP BY  MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select  MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where CAST(MISCRECPTDATE AS DATE) between '" + frmDate + "' and '" + toDate + "' and  CHDD='C' AND ISNULL(MD.IsTransfer,0) != 1  GROUP BY  MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetCashAmountForFeesTransfer-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMisBankAmountForMF(string frmDate, string toDate, string con, string paytype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE BETWEEN '" + frmDate + "' and '" + toDate + "' and can=0 and CHDD='Q' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where CAST(MISCRECPTDATE AS DATE) BETWEEN '" + frmDate + "' and '" + toDate + "'  and (CHDD='D' or CHDD='T' ) AND ISNULL((MD.IsTransfer),0) != 1 AND PAY_TYPE='" + paytype + "'  GROUP BY MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForMF-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetBankAmountmissForMF(string frmDate, string toDate, string con)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(con);
                        int count = 0; DataSet ds1 = new DataSet();
                        //For Checking Tables in MINI-RFCAMPUS OR RF-CAMPUS
                        ds1 = objSQLHelper.ExecuteDataSet("SELECT name  FROM sys.tables where name = 'MISCDCR_TRANS'");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    count = 1;
                                }
                            }
                        }
                        if (count == 0)
                        {
                            ds = objSQLHelper.ExecuteDataSet("select MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where RECPTDATE BETWEEN '" + frmDate + "' and '" + toDate + "' and can=0 and CHDD='Q' and RECIEPT_CODE='MF' GROUP BY MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                        }
                        if (count == 1)
                            ds = objSQLHelper.ExecuteDataSet("select MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD,sum(MIscamt)AMT from MISCDCR_TRANS MT inner join MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO) where cast(MISCRECPTDATE as date) BETWEEN '" + frmDate + "' and '" + toDate + "' AND ISNULL(MD.IsTransfer,0) != 1  and (CHDD='D' or CHDD='T' )  GROUP BY MD.MISCDCRSRNO,MISCHEADSRNO,MISCHEADCODE,MISCHEAD");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetBankAmountForMF-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddMISCFeeAccountTransfer_BANK_FOR_RECEIPT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_FOR_RECEIPT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_FOR_PAYMENT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_FOR_PAYMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_NPAYT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_NPAYT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_FOR_RECEIPT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_MANUAL_VCH_FOR_RECEIPT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_FOR_PAYMENT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_MANUAL_VCH_FOR_PAYMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_NPAYT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_BANK_MANUAL_VCH_NPAYT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_FOR_RECEIPT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_FOR_RECEIPT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_FOR_PAYMENT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_FOR_PAYMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_NPAYT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_NPAYT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_FOR_RECEIPT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_MANUAL_VCH_FOR_RECEIPT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_FOR_PAYMENT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, string TransType, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_TRANS_TYPE", TransType);
                        objParams[11] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_MANUAL_VCH_FOR_PAYMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_NPAYT(string Fdate, string tdate, int Uno, string compCode, int degreeNo, string REtype, string ColCode, string DegreeName, string RecieptType, string databaseName, AccountTransaction objTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", tdate);
                        objParams[2] = new SqlParameter("@P_USERNO", Uno);
                        objParams[3] = new SqlParameter("@P_COMP_CODE", compCode);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[5] = new SqlParameter("@P_RECIEPT_TYPE", REtype);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_DegreeName", DegreeName);
                        objParams[8] = new SqlParameter("@P_RecieptName", RecieptType);
                        objParams[9] = new SqlParameter("@P_DATABASE", databaseName);
                        objParams[10] = new SqlParameter("@P_Transfer_Fee_Voucher_TBL", objTran.TransFieldsTbl_TRAN);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_MISC_FEES_TRANSFER_CASH_MANUAL_VCH_NPAYT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;

                }

            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS  
}//END: IITMSVOUCHER_NO", objParty.VOUCHER_NO);



