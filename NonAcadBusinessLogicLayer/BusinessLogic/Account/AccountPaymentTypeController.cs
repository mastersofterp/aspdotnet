using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This AccountPaymentTypeController is used to Bind PartyName ListView.
            /// </summary>
            public class AccountPaymentTypeController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                //public string _client_constr = string.Empty;
                //string connectionString = ;

                public AccountPaymentTypeController()
                {
                    //Blank;
                }

                /// <summary>
                /// Created By : NAKUL CHAWRE
                /// Creation Date : 16/08/2015
                /// Purpose : To Insert Payment Type Data. 
                /// </summary>
                /// <returns></returns>
                public int AddPaymentData(string GroupName, string PaymentNo, int UserId, int CollId,string compcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        //objParams[0] = new SqlParameter("@P_PGROUP_NO", GroupName);
                        objParams[0] = new SqlParameter("@P_GROUPNAME", GroupName);
                        objParams[1] = new SqlParameter("@P_PAYTYPENO", PaymentNo);
                        objParams[2] = new SqlParameter("@P_USERID", UserId);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", CollId);
                        objParams[4] = new SqlParameter("@P_COMPCODE", compcode);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYMENTTYPE_INSERT", objParams, true);
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
                    catch (Exception Ex)
                    {
                        
                    }
                    return retStatus;
                }

                /// <summary>
                /// Created By : NAKUL CHAWRE
                /// Creation Date : 16/08/2015
                /// Purpose : To Update Payment Type Data. 
                /// </summary>
                /// <returns></returns>
                public int UpdatePaymentData(int PGrpNo, string GroupName, string PaymentNo, int UserId, int CollId, string COMPCODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_PGROUP_NO", PGrpNo);
                        objParams[1] = new SqlParameter("@P_GROUPNAME", GroupName);
                        objParams[2] = new SqlParameter("@P_PAYTYPENO", PaymentNo);
                        objParams[3] = new SqlParameter("@P_USERID", UserId);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", CollId);
                        objParams[5] = new SqlParameter("@P_COMPCODE", COMPCODE);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYMENTTYPE_UPDATE", objParams, true);
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
                    catch (Exception Ex)
                    {

                    }
                    return retStatus;
                }

                //Fill Party List From BEC Database
                public DataSet FillPartyList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString);
                        //SqlParameter[] param = null;
                        //param = new SqlParameter[1];
                        //param[0] = new SqlParameter("@P_COLL_ID", CollegeID);
                        ds = objSqlHelper.ExecuteDataSet("SELECT PAYTYPENO, PAYTYPENAME, REMARK FROM ACD_PAYMENTTYPE WHERE PAYTYPENO<>0");
                    }
                    catch (Exception Ex)
                    {

                        throw;
                    }
                    return ds;
                }

                //Fill Party List Details From BEC_FINANCE Database
                public DataSet FillPartyDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);
                        //SqlParameter[] param = null;
                        //param = new SqlParameter[1];
                        //param[0] = new SqlParameter("@P_COLL_ID", CollegeID);
                        ds = objSqlHelper.ExecuteDataSet("SELECT PGROUP_NO, GROUPNAME, PAYTYPENO FROM acc_001_PAYMENT_GROUP");
                    }
                    catch (Exception Ex)
                    {

                        throw;
                    }
                    return ds;
                }

                //Get Party Data PartyID wise.
                public DataSet GetPartyDetails(int PGROUPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);
                        //SqlParameter[] param = null;
                        //param = new SqlParameter[1];
                        //param[0] = new SqlParameter("@P_COLL_ID", CollegeID);
                        ds = objSqlHelper.ExecuteDataSet("SELECT PGROUP_NO, GROUPNAME, PAYTYPENO FROM acc_001_PAYMENT_GROUP WHERE PGROUP_NO=" + PGROUPNO);
                    }
                    catch (Exception Ex)
                    {

                        throw;
                    }
                    return ds;
                }

            }
        }
    }
}