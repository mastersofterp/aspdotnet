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
            public class AccountingVouchersController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
               public string _client_constr = string.Empty;
               string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
               Common objcommon = new Common();
               public AccountingVouchersController()
               {
               }


               public AccountingVouchersController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }



                //private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddVocherTransactions(AccountConfiguration objMG,string Company_Code, string code_year)
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
                        objParams[5] = new SqlParameter("@p_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_REF_INSERT", objParams, true);
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

                public DataSet GetTDSRecordsExcel(string Comp_Code, int party_no, string FromDate, string ToDate)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TODATE", ToDate);
                        objParams[3] = new SqlParameter("@P_Party_No", party_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_TDS_Details_Excel_Report", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GetTDSRecordsExcel.GetReceiptSide-> " + ee.ToString());
                    }
                }

                public DataSet GetTDSRecords(string Comp_Code, int party_no, string FromDate, string ToDate)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TODATE", ToDate);
                        objParams[3] = new SqlParameter("@P_Party_No", party_no);
                        //objParams[1] = new SqlParameter("@P_Quarter", Quarter);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_TDS_RECORDS", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                public int InsertTDSTran(DataTable dtTDS,int college_code,string comp_code,int UserNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE",comp_code);
                        objParams[1] = new SqlParameter("@P_TDSDETAILS",dtTDS);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE",college_code);
                        objParams[3] = new SqlParameter("@P_USERNO",UserNo);
                        objParams[4] = new SqlParameter("@P_OUT",SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TDS_DETAILS_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch(Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.InsertTDSTran-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int InsertTDSTranbk(DataTable dtTDS, int college_code, string comp_code, int UserNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", comp_code);
                        objParams[1] = new SqlParameter("@P_TDSDETAILS", dtTDS);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[3] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TDS_DETAILS_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.InsertTDSTran-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int DeleteVoucher(string voucherno, string compcode,string VCH_TYPE)
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.DeleteVoucher-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int UpdateChequePrintStatusToTransaction(int VoucherNo,string ChqNo,string ChqDate,string can, string Company_Code,string RollBack,string partyno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_VRNO", VoucherNo);
                        objParams[1] = new SqlParameter("@P_CHQNO", ChqNo);
                        objParams[2] = new SqlParameter("@P_CHQDATE", ChqDate);
                        objParams[3] = new SqlParameter("@P_CAN", can);
                        objParams[4] = new SqlParameter("@P_COMPANY_CODE", Company_Code);
                        objParams[5] = new SqlParameter("@P_PARTY", partyno);
                        objParams[6] = new SqlParameter("@P_ROLLBACK", RollBack);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_TRANS_CHQ_UPDATE", objParams, true);
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
                      //  throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.UpdateChequePrintStatusToTransaction-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.UpdateChequePrintStatusToTransaction-> " + ex.ToString());
                    }
                    return retStatus;
                }
                
                public int InsertUpdateFD(FixedDepositeClass objFDC,DataTable dtContact,int userno,int College_Code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[33];
                        objParams[0] = new SqlParameter("@P_FDID",objFDC.FDID);
                        objParams[1] = new SqlParameter("@P_FD_SRNO",objFDC.Serial_No);
                        objParams[2] = new SqlParameter("@P_FD_COMPNO",objFDC.Comp_No);
                        objParams[3] = new SqlParameter("@P_FD_COMPCODE",objFDC.Comp_code);
                        objParams[4] = new SqlParameter("@P_FD_PARTYID",objFDC.PARTY_NO);
                        objParams[5] = new SqlParameter("@P_CUSTOMER_ID",objFDC.Customer_Id);
                        objParams[6] = new SqlParameter("@P_FDR_NO",objFDC.FDR_NO);
                        objParams[7] = new SqlParameter("@P_INVESTMENT_DATE",objFDC.Investment_Date);
                        objParams[8] = new SqlParameter("@P_MATURITY_DATE",objFDC.Maturity_Date);
                        objParams[9] = new SqlParameter("@P_AMT_INVESTED",objFDC.Invested_Amt);
                        objParams[10] = new SqlParameter("@P_MATURITY_AMT",objFDC.Maturity_Amt);
                        objParams[11] = new SqlParameter("@P_ROI",objFDC.RateOfIntrest);
                        objParams[12] = new SqlParameter("@P_PAN_NO",objFDC.PAN_No);
                        objParams[13] = new SqlParameter("@P_PERIOD_FROM",objFDC.Period_From_Date);
                        objParams[14] = new SqlParameter("@P_PERIOD_TO",objFDC.Period_To_Date);
                        objParams[15] = new SqlParameter("@P_SCHEME",objFDC.Scheme);
                        objParams[16] = new SqlParameter("@P_ACCOUNT_HOLDER",objFDC.Account_Holder);
                        objParams[17] = new SqlParameter("@P_CUSTOMER_ADDRESS",objFDC.Address);
                        objParams[18] = new SqlParameter("@P_NOMINATION",objFDC.Nomination_For);
                        objParams[19] = new SqlParameter("@P_REMARK",objFDC.Remark);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE",College_Code);
                        objParams[21] = new SqlParameter("@P_USER_NO",userno);
                        objParams[22] = new SqlParameter("@P_BANK_PARTY_ID",objFDC.Bank_Id);
                        objParams[23] = new SqlParameter("@P_ISCLOSED", objFDC.IsClosed);
                        objParams[24] = new SqlParameter("@P_CONTACT_DETAILS",dtContact);

                        objParams[25] = new SqlParameter("@P_BANK_ADDRESS", objFDC.BankAddress);
                        objParams[26] = new SqlParameter("@P_REFERENCE", objFDC.Reference);
                        objParams[27] = new SqlParameter("@P_FD_DURATION", objFDC.Fd_Duration);
                        objParams[28] = new SqlParameter("@P_FD_WITHDRAWN_AMOUNT", objFDC.FdWithdrawnAmount);
                        objParams[29] = new SqlParameter("@P_ACCUMULATED_INTEREST", objFDC.AccumulatedInterest);
                        objParams[30] = new SqlParameter("@P_REGISTER_BOOK_NO", objFDC.RegisterBookNo);
                        objParams[31] = new SqlParameter("@P_FD_ADVISE_ATTACHMENT", objFDC.FdAdviseAttachment);

                        objParams[32] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[32].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_FIXED_DEPOSIT_ENTRY", objParams, true);
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
                            else
                                retStatus = Convert.ToInt32(ret.ToString());
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //  throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.UpdateChequePrintStatusToTransaction-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.UpdateChequePrintStatusToTransaction-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetFdExcelReport(string FromDate, string ToDate)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);

                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@FROM_DATE",FromDate);
                        objParams[1] = new SqlParameter("@TODATE",ToDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_GET_FD_REPORT",objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetFdExcelReport-> " + ee.ToString());
                    }
                }

                public DataSet GetFDDetails(int FDId)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FDID", FDId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_FD_DETAILS", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                public int DeleteVoucherMakaut(string voucherno, string compcode, string VCH_TYPE)
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

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VOUCHER_DELETE_MAKAUT", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.DeleteVoucher-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateVoucherVerifyApprove(int VoucherSqn, int Userno, string AuthorityType, string Comp_Code,char PaymentMode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_VOUCHER_SQN", VoucherSqn);
                        objParams[1] = new SqlParameter("@P_USERNO", Userno);
                        objParams[2] = new SqlParameter("@P_AUTHORITY_TYPE", AuthorityType);
                        objParams[3] = new SqlParameter("@P_CompCode", Comp_Code);
                        objParams[4] = new SqlParameter("@P_PAYMENT_MODE", PaymentMode);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_VERIFY_APPROVE_VOUCHER", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
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

                #region Upload Files in Folder

                public void upload_new_files(string folder, int idno, string primary, string table, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/UPLOAD_FILES/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    //Upload the File
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        int newfilename = Convert.ToInt32(objcommon.LookUp(table, "max(" + primary + ")", ""));
                        string uploadFile = initials + newfilename + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        //fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }
                }

                public void update_upload(string folder, int trxno, string lastfilename, int idno, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    //Upload the File
                    string uploadPath = HttpContext.Current.Server.MapPath("~/UPLOAD_FILES/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        //Update Assignment
                        string oldFileName = string.Empty;

                        oldFileName = initials + trxno + System.IO.Path.GetExtension(lastfilename);

                        if (System.IO.File.Exists(uploadPath + oldFileName))
                        {
                            System.IO.File.Delete(uploadPath + oldFileName);
                        }

                        string uploadFile = initials + trxno + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }

                }

                #endregion

                //added by tanu 29/09/2022
                // this method used in GST TDS Report
                public DataSet GetGSTONTDSRecordsExcel(string Comp_Code, DateTime From_date, DateTime To_date)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROM_DATE",From_date);
                        objParams[2] = new SqlParameter("@P_TO_DATE",To_date);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_TDS_ON_GST_REPORT", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                public DataSet GetGSTDetailsExcel(string Comp_Code, DateTime From_date, DateTime To_date)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROM_DATE", From_date);
                        objParams[2] = new SqlParameter("@P_TO_DATE", To_date);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GST_REPORT", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }
                
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS