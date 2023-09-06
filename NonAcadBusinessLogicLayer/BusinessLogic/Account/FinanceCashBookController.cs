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
            /// <summary>
            /// This CashBookController is used to control Acc_Section table.
            /// </summary>
            public class FinanceCashBookController
            {


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public FinanceCashBookController()
                {
                }
               public  FinanceCashBookController(string DbPassword,string DbUserName,String DataBase)
               {
                   _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
               }
               // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


               public int AddComp_CashBook(FinCashBook objCashBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New Company/Cash Book
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);
                        objParams[1] = new SqlParameter("@P_COMPANY_NAME", objCashBook.Company_Name);
                        objParams[2] = new SqlParameter("@P_COMPANY_FINDATE_FROM", objCashBook.Company_FindDate_From);
                        objParams[3] = new SqlParameter("@P_COMPANY_FINDATE_TO", objCashBook.Company_FindDate_To);
                        objParams[4] = new SqlParameter("@P_BOOKWRTDATE", objCashBook.BookWriteDate);
                        objParams[5] = new SqlParameter("@P_YEAR", objCashBook.Year);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objCashBook.College_Code);
                        objParams[7] = new SqlParameter("@P_IsCompanyLock", objCashBook.IsCompanyLock);
                        objParams[8] = new SqlParameter("@P_COMPANY_NO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COMPANY_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.AddComp_CashBook-> " + ex.ToString());
                    }
                    return retStatus;
                }
               public int AddChildComp_CashBook(FinCashBook objCashBook)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(connectionString);
                       SqlParameter[] objParams = null;

                       //Add New Company/Cash Book
                       objParams = new SqlParameter[9];
                       objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);
                       objParams[1] = new SqlParameter("@P_COMPANY_NAME", objCashBook.Company_Name);
                       objParams[2] = new SqlParameter("@P_COMPANY_FINDATE_FROM", objCashBook.Company_FindDate_From);
                       objParams[3] = new SqlParameter("@P_COMPANY_FINDATE_TO", objCashBook.Company_FindDate_To);
                       objParams[4] = new SqlParameter("@P_BOOKWRTDATE", objCashBook.BookWriteDate);
                       objParams[5] = new SqlParameter("@P_YEAR", objCashBook.Year);
                       objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objCashBook.College_Code);
                       objParams[7] = new SqlParameter("@P_PARENT_CODE", objCashBook.Parent_Comp_Code);
                       objParams[8] = new SqlParameter("@P_COMPANY_NO", SqlDbType.Int);
                       objParams[8].Direction = ParameterDirection.Output;

                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CHILD_COMPANY_INSERT", objParams, true);
                       if (ret != null)
                       {
                           if (ret.ToString().Equals("-99"))
                               retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                           else if (ret.ToString().Equals("-1001"))
                               retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                           else
                               retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       }
                       else
                           retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.AddComp_CashBook-> " + ex.ToString());
                   }
                   return retStatus;
               }
                /// <summary>
                /// Creating run Time tables For Company/Cash Book
                /// </summary>
                /// <param name="objCashBook"></param>
                /// <returns>int</returns>
                public int Create_Tables(FinCashBook objCashBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //@P_College_Code
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);
                       
                        objParams[1] = new SqlParameter("@P_YEAR", objCashBook.Year);
                        objParams[2] = new SqlParameter("@P_College_Code", objCashBook.College_Code);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_USER_TABLE_SCRIPTS", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.Create_Tables-> " + ex.ToString());
                    }
                    return retStatus;
                }
                 public int Create_ChildComp_Tables(FinCashBook objCashBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //@P_College_Code
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);
                       
                        objParams[1] = new SqlParameter("@P_YEAR", objCashBook.Year);
                        objParams[2] = new SqlParameter("@P_College_Code", objCashBook.College_Code);
                        objParams[3] = new SqlParameter("@P_PARENT_COMPANY_CODE", objCashBook.Parent_Comp_Code);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_USER_TABLE_SCRIPTS_FINANCIAL_YEAR_END", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.Create_Tables-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int Create_Synonym_For_Tables(FinCashBook objCashBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //@P_College_Code
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);

                        objParams[1] = new SqlParameter("@P_YEAR", objCashBook.Year);
                        objParams[2] = new SqlParameter("@P_College_Code", objCashBook.College_Code);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_USER_TABLE_SCRIPTS_SYNONYM", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.Create_Synonym_For_Tables-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateComp_CashBook(string fromdate,string todate,string bookWritingdate,string CompanyCode,string CompanyName,int CompanyNo,string OldFinYear,string NewFinyear,string companyName,int IsCompanyLock)
                {
                   

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams1 = null;
                        objParams1 = new SqlParameter[6];
                        objParams1[0] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams1[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams1[2] = new SqlParameter("@P_UPTO_DATE", todate);
                        objParams1[3] = new SqlParameter("@P_BW_DATE", bookWritingdate);
                        objParams1[4] = new SqlParameter("@P_COMPANY_NAME", companyName);
                        objParams1[5] = new SqlParameter("@P_IsCompanyLock", IsCompanyLock);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_USER_TABLE_SCRIPTS_UPDATE", objParams1, true);
                       
                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.UpdateComp_CashBook-> " + ex.ToString());
                    }
                    return 1;
                }

                public int DeleteComp_CashBook(string CompanyCode)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //delete New Company/Cash Book
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_USER_TABLE_SCRIPTS_DELETE", objParams, true);

                        

                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.DeleteComp_CashBook-> " + ex.ToString());
                    }
                    return 1;
                }

                public DataTableReader GetCashBookByCompanyNo(object comp_no)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_COMPANY_NO", comp_no);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_COMPANY", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.GetCashBookByCompanyNo-> " + ex.ToString());
                    }
                    return dtr;
                }


                //cashbook Report

                public int PrepareData(string frmdt, string todate, string pcode,int Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        object ret = null;                      
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());
                        objParams[1] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todate);
                        objParams[3] = new SqlParameter("@P_PCODE", pcode);
                        //objParams[3] = new SqlParameter("@P_BCODE","'"+ bcode+"'");
                        if(Status==0)
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CASH_REPORT_PREPDATA", objParams, true);
                        else
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CASH_REPORT_PREPDATA_WITHALL_PREVIOUSBALANCE", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FinanceCashBookController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region cashbook Report
                /// <summary>
                /// Developed by :Kapil Budhlani
                /// Purpose      :
                /// </summary>
                public DataSet GetReceiptSide()
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", HttpContext.Current.Session["comp_code"].ToString());

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_CASH_REPORT_RECEIPT_SIDE", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FinanceCashBookController.GetReceiptSide-> " + ee.ToString());
                    }
                }

                public int AddRecieptSideEntry(FinCashBook objCashBook, double opbal,double clbal,double tr,int insup, int isOpbal)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                                                                         
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_DATE", objCashBook.Date.ToShortDateString());
                        objParams[1] = new SqlParameter("@P_PARTICULAR", objCashBook.Particular );
                        objParams[2] = new SqlParameter("@P_AMOUNT", objCashBook.Amt);
                        objParams[3] = new SqlParameter("@P_VNO", objCashBook.VNo);
                        objParams[4] = new SqlParameter("@P_PARTI_NAME", objCashBook.PartyName  );
                        objParams[5] = new SqlParameter("@P_TR_NO", objCashBook.TrNo );
                        objParams[6] = new SqlParameter("@P_TR_TYPE", objCashBook.TrType );
                        objParams[7] = new SqlParameter("@P_SUBTR_NO", objCashBook.SubTrNo);
                        objParams[8] = new SqlParameter("@P_MGRP_NO", objCashBook.MgrpNo);
                        objParams[9] = new SqlParameter("@P_TENTRYR", objCashBook.TEntry );
                        objParams[10] = new SqlParameter("@P_INSUP", insup);//insert or update
                        objParams[11] = new SqlParameter("@P_OPBALANCE", opbal);
                        objParams[12] = new SqlParameter("@P_CLBALANCE", clbal);
                        objParams[13] = new SqlParameter("@P_TR", tr);
                        objParams[14] = new SqlParameter("@P_ISOPBAL_INSERT", isOpbal);//Is opening closing balance insert
                        //objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CASH_REPORT_INSUP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FinanceCashBookController.AddRecieptSideEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                public int UpdateCashBook(FinCashBook objCashBookEntity)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //@P_College_Code
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMP_CODE", objCashBookEntity.Company_Code);

                        objParams[1] = new SqlParameter("@P_COMP_FROM_DATE", objCashBookEntity.Company_FindDate_From);
                        objParams[2] = new SqlParameter("@P_COMP_TODATE", objCashBookEntity.Company_FindDate_To);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPDATE_COMPANY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (ret.ToString().Equals("-1001"))
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.Create_Tables-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          