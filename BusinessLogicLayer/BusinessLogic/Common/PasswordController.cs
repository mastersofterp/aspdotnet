using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Data.SqlClient;
using IITMS.UAIMS;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This PasswordController is used to control User_Acc table.
            /// </summary>
            public class PasswordController
            {                
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                Common objCommon = new Common();

                /// <summary>
                /// This method is used to get password from User_Acc table.
                /// </summary>
                /// <param name="ua_name">Get password as per this ua_name.</param>
                /// <returns>String</returns>
                //public string GetPassword(string ua_name)
                //{
                //    string password = string.Empty;

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);

                //        object ret = objSQLHelper.ExecuteScalarSP("PKG_USER_ACC_SP_RET_PASSWORD", objParams);

                //        if (ret != null) password = ret.ToString();

                //    }
                //    catch (Exception ex)
                //    {
                //        objCommon.WriteErrorDetails(ex.Message.ToString());
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PasswordController.GetPassword-> " + ex.ToString());
                //    }
                //    return password;
                //}
                public DataSet GetUSER(string ua_name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public int UpdateBlockUser(UserAcc objUserAcc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UANO", objUserAcc.UA_No);
                        objParams[1] = new SqlParameter("@P_STATUS", objUserAcc.UA_Status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = System.Data.ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_RET_BLOCKUSER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetPassword(string ua_name, string ua_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_PASSWORD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public DataSet Deveoper_GetPassword(string ua_name, string ua_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_PASSWORD_DEVELOPER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PasswordController.Deveoper_GetPassword() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public DataSet GetPassword(string ua_name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_PASSWORD_DEVELOPER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PasswordController.Deveoper_GetPassword() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                    //throw new NotImplementedException();
                }
            }

            //public class Password
            //{
            //    #region Private Members
            //    private string _ua_name = string.Empty;
            //    private string _ua_pwd = string.Empty;
            //    #endregion

            //    #region Public Property Fields
            //    public string Ua_Name
            //    {
            //        get { return _ua_name; }
            //        set { _ua_name = value; }
            //    }

            //    public string Ua_Pwd
            //    {
            //        get { return _ua_pwd; }
            //        //set { _ua_pwd = value; }
            //    }
            //    #endregion
            //}

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS