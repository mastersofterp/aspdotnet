using System;
using System.Data;
using System.Web;

//using IITMS;
//using IITMS.UAIMS.BusinessLayer;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.SQLServer.SQLDAL;

using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;
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
            public class FinCashBookController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public int AddComp_CashBook(FinCashBook objCashBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Company/Cash Book
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMPANY_CODE", objCashBook.Company_Code);
                        objParams[1] = new SqlParameter("@P_COMPANY_NAME", objCashBook.Company_Name);
                        objParams[2] = new SqlParameter("@P_COMPANY_FINDATE_FROM", objCashBook.Company_FindDate_From);
                        objParams[3] = new SqlParameter("@P_COMPANY_FINDATE_TO", objCashBook.Company_FindDate_To);
                        objParams[4] = new SqlParameter("@P_BOOKWRTDATE", objCashBook.BookWriteDate);
                        objParams[5] = new SqlParameter("@P_YEAR", objCashBook.Year);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objCashBook.College_Code);
                        objParams[7] = new SqlParameter("@P_COMPANY_NO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

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




                public int UpdateComp_CashBook(string fromdate, string todate, string bookWritingdate, string CompanyCode, string CompanyName, int CompanyNo, string OldFinYear, string NewFinyear, string companyName)
                {


                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams1 = null;
                        objParams1 = new SqlParameter[5];
                        objParams1[0] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams1[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams1[2] = new SqlParameter("@P_UPTO_DATE", todate);
                        objParams1[3] = new SqlParameter("@P_BW_DATE", bookWritingdate);
                        objParams1[4] = new SqlParameter("@P_COMPANY_NAME", companyName);
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
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
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
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
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

                #region AssignCashBook

                public int AssignCashBook(DataTable dtUserNo, string CashBookId, string Date, int UserId, string IPAddress, int UserType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Links of Existing User
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_UA_NO", dtUserNo);
                        objParams[1] = new SqlParameter("@P_CashBookId", CashBookId);
                        objParams[2] = new SqlParameter("@P_UserId", UserId);
                        objParams[3] = new SqlParameter("@P_CreatedDate", Date);
                        objParams[4] = new SqlParameter("@P_ModifiedDate", Date);
                        objParams[5] = new SqlParameter("@P_ModifiedBy", UserId);
                        objParams[6] = new SqlParameter("@P_IPAddress", IPAddress);
                        objParams[7] = new SqlParameter("@P_UserType", UserType);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INSUPD_ASSIGNCASHBOOK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAssignCashBookData(object comp_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_COMPANY_NO", comp_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_COMPANY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CashBookController.GetCashBookByCompanyNo-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion AssignCashBook

                #region CompanyCollegeAllocation

                public int AssignCollege(string CompanyId, DataTable dtCollegeNo, string Date, int UserId, string IPAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Links of Existing User
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_Company_No", CompanyId);
                        objParams[1] = new SqlParameter("@P_CreatedDate", Date);
                        objParams[2] = new SqlParameter("@P_ModifiedDate", Date);
                        objParams[3] = new SqlParameter("@P_ModifiedBy", UserId);
                        objParams[4] = new SqlParameter("@P_IPAddress", IPAddress);
                        objParams[5] = new SqlParameter("@P_UserId", UserId);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", dtCollegeNo);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_ALLOCATE_COLLEGE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion CompanyCollegeAllocation



            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS