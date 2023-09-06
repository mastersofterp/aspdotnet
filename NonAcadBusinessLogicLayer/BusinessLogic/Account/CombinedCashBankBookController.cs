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
            public class CombinedCashBankBookController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
               // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public CombinedCashBankBookController()
                {
                }

                public CombinedCashBankBookController(string DbPassword, string DbUserName, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }
                public int AddReceiptCashBook(DateTime date,int tr_no,string parti,double amt,string pname,int vno,int subtr_no,string tr_type,int tentry,int queryType,int cashbank)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                                                                                     
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_DATE", date);

                        if (tr_no == 0)
                            objParams[1] = new SqlParameter("@P_TR_NO", '0');
                        else
                            objParams[1] = new SqlParameter("@P_TR_NO", Convert.ToString( tr_no));

                       
                        objParams[2] = new SqlParameter("@P_PARTI", parti);
                        objParams[3] = new SqlParameter("@P_AMOUNT", amt);

                        if (pname == "")
                            objParams[4] = new SqlParameter("@PARTY_NAME", "");
                        else
                            objParams[4] = new SqlParameter("@PARTY_NAME", pname);

                        objParams[5] = new SqlParameter("@VNO", vno);
                        objParams[6] = new SqlParameter("@P_SUBTR_NO", subtr_no);
                        objParams[7] = new SqlParameter("@P_TR_TYPE", tr_type);
                        objParams[8] = new SqlParameter("@P_TENTRY", tentry);

                        //if (queryType == 1)
                        //objParams[9] = new SqlParameter("@P_QUERY_TYPE", 1);
                        //else
                        //    objParams[9] = new SqlParameter("@P_QUERY_TYPE", 2);
                        
                        objParams[9] = new SqlParameter("@P_QUERY_TYPE", queryType);
                       
                        objParams[10] = new SqlParameter("@P_CASHBANK", cashbank);//cash entry Or bank entry
                       // objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMBINED_ADDREC_CASHBOOK", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.AddReceiptCashBook-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsUpdateBankOpClBal(CombinedCashBankBook objccb, string code_year, int queryType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        
                        objParams = new SqlParameter[10];
                        
                        objParams[0] = new SqlParameter("@P_OPBALANCE", objccb.OpBal );
                        objParams[1] = new SqlParameter("@P_CLBALANCE", objccb.ClBal );
                        objParams[2] = new SqlParameter("@P_OPBALANCEB", objccb.OpBalb );
                        objParams[3] = new SqlParameter("@P_CLBALANCEB", objccb.ClBalb);
                        objParams[4] = new SqlParameter("@P_TRB", objccb.Trb);

                        objParams[5] = new SqlParameter("@P_QUERY_TYPE", queryType);
                        objParams[6] = new SqlParameter("@P_TR_DATE", objccb.Tr_Date);
                        objParams[7] = new SqlParameter("@P_TR", objccb.Tr );
                        objParams[8] = new SqlParameter("@P_OPBALANCE_NO", objccb.OpBalNo);
                        objParams[9] = new SqlParameter("@P_BANK_NM", objccb.BankNm);
                        //objParams[6] = new SqlParameter("@P_TR", SqlDbType.Int);
                        //objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMBINED_INSUP_CWDUMMY3", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.InsUpdateBankOpClBal-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Project Name :UAIMS 
                /// Created By   :Kapil Budhlani
                /// Purpose      :This method is used to
                /// </summary>
                /// <returns>dataset</returns>
                public DataSet Get_bcwdummy1(int typeNo)
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_TYPE_NO", typeNo);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_COMBINED_GET_BCWDUMMY1", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.Get_bcwdummy1-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet  Get_OPBAL(int partyNo,int oParty,int wthCreDebit)
                {
                    DataSet ds = new DataSet();
                    
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_College_Code", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_PARTY_NO", partyNo );
                        objParams[2] = new SqlParameter("@P_OPARTY", oParty);
                        objParams[3] = new SqlParameter("@P_WTH_CRE_DEBIT", wthCreDebit);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_COMBINED_GET_OPBAL", objParams);
                        //obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMBINED_GET_OPBAL", objParams, true);

                        

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.Get_OPBAL-> " + ex.ToString());
                    }
                    return ds;
                }
                public decimal Get_OPBAL(int partyNo, string FromDate, int wthCreDebit)
                {
                    decimal CL_AMOUNT = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_PARTY_NO", partyNo);
                        objParams[2] = new SqlParameter("@fromDate", FromDate);
                        objParams[3] = new SqlParameter("@p_amount", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        objParams[4] = new SqlParameter("@isledger", 0);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_Get_opbal", objParams, true);
                        //obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMBINED_GET_OPBAL", objParams, true);



                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.Get_OPBAL-> " + ex.ToString());
                    }
                    return CL_AMOUNT;
                }
                public int LoadDataInDummyTbl(string frmdt,string todate,string bcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todate);
                        objParams[3] = new SqlParameter("@P_BCODE", bcode);
                        //objParams[3] = new SqlParameter("@P_BCODE","'"+ bcode+"'");
                        
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_COMBINED_CASH_BANK_NEW", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.LoadDataInDummyTbl-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int PrepareData(string frmdt, string todate,string cashNo,string bankNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_COLCODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDT", frmdt);
                        objParams[2] = new SqlParameter("@P_TODT", todate);
                        objParams[3] = new SqlParameter("@P_CashNo", cashNo);
                        objParams[4] = new SqlParameter("@P_BankNo", bankNo);
                        
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMBINED_PREPDATA_MINA", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CombinedCashBankBookController.LoadDataInDummyTbl-> " + ex.ToString());
                    }
                    return retStatus;
                }


            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS