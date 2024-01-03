//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : TO CREATE DEFAULT                                               
// CREATION DATE : 13-April-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using mastersofterp_MAKAUAT;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This User_AccController is used to control User_Acc table.
            /// </summary>
            public class User_AccController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                private string rfcString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS_RFCCONFIG"].ConnectionString;
                /// <summary>
                /// This method is used to change password.
                /// </summary>
                /// <param name="objUserAcc">Object of UserAcc class.</param>
                /// <returns>Integer CustomStatus</returns>
                public int ChangePassword(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        //Check whether username/password exists
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", objUserAcc.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_OLD_UA_PWD", objUserAcc.UA_OldPwd);
                        objParams[3] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[3].Direction = System.Data.ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_PASSWORD", objParams, true);

                        if (Convert.ToInt16(ret) == 0)
                            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                        else
                            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }



                /// <summary>
                /// This method is used to change password only for admin.
                /// </summary>
                /// <param name="objUserAcc">Object of UserAcc class.</param>
                /// <returns>Integer CustomStatus</returns>
                /// added by sandeep k
                /// 

                //changed by arjun for maintain session user and source page no 28-01-2023
                public int UpdateUserAccLink(int UserNo, int SourcePageNo, UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Faculty Existing User
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);//new for maintin log table
                        objParams[1] = new SqlParameter("@P_SOURCEPAGENO", SourcePageNo);//new for maintin log table

                        objParams[2] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[3] = new SqlParameter("@P_UA_NO", objUA.UA_No);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_USERACC_LINK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserAccLink -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetUserLinkDomain(int ua_no, int UA_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", UA_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LINK_ACCESS_DOMAIN_USER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserLinkDomain-> " + ex.ToString());
                    }
                    return ds;
                }

                //end

                public int ChangePasswordByadmin(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        //Check whether username/password exists
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", objUserAcc.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_OLD_UA_PWD", objUserAcc.UA_OldPwd);
                        objParams[3] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[4].Direction = System.Data.ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_PASSWORD_BYADMIN", objParams, true);

                        if (Convert.ToInt16(ret) == 0)
                            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                        else
                            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// This method is used to validate Current logged user.
                /// </summary>
                /// <param name="username">Gets name of logged user</param>
                /// <param name="password">Gets password of logged user</param>
                /// <param name="user_no">validate login of this user_no</param>
                /// <returns>Integer Custom Status</returns>
                ///  /// <returns>Integer Custom Status</returns>
                //public int ValidateLogin(string username, string password, out int user_no)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[3];

                //        objParams[0] = new SqlParameter("@P_UA_NAME", username);
                //        objParams[1] = new SqlParameter("@P_UA_PWD", password);
                //        objParams[2] = new SqlParameter("@P_UA_NO", SqlDbType.Int);
                //        objParams[2].Direction = ParameterDirection.Output;

                //        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_RET_UA_NO", objParams, true);

                //        if (ret != null)
                //        {
                //            user_no = int.Parse(ret.ToString());
                //            retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //username, password correct
                //        }
                //        else
                //        {
                //            user_no = -1;
                //            retStatus = Convert.ToInt32(CustomStatus.Error);     //username, password incorrect
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateLogin-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                /// <summary>
                /// Add by Rita M....01112019----for three layer encryption......
                /// </summary>
                /// <param name="username"></param>
                /// <param name="password"></param>
                /// <param name="user_no"></param>
                /// <returns></returns>
                public int ValidateLogin(string username, string password, out int user_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    Common objCommon = new Common();
                    string user_pwd = string.Empty;

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_UA_NAME", username);
                        objParams[1] = new SqlParameter("@P_UA_PWD", SqlDbType.VarChar, 500);
                        //  objParams[2] = new SqlParameter("@P_UA_NO", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        // object ret = objSqlHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_RET_UA_NO", objParams, true);

                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_CHECK_UA", objParams, true);
                        user_pwd = clsTripleLvlEncyrpt.EncryptPassword(password);       // Encrypt withMasterSoft Logic
                        user_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(user_pwd);         // Encrypt with One AES

                        //string db_pwd = user_pwd;       //clsTripleLvlEncyrpt.RSADecryption(ret.ToString());
                        string db_pwd = clsTripleLvlEncyrpt.RSADecryption(ret.ToString());
                        if (user_pwd == db_pwd)
                        {
                            user_no = int.Parse(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + username + "'"));
                            retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //username, password correct
                        }
                        else
                        {
                            user_no = -1;
                            retStatus = Convert.ToInt32(CustomStatus.Error);     //username, password incorrect
                        }
                        //if (ret != null)
                        //{
                        //    user_no = int.Parse(ret.ToString());
                        //    retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //username, password correct
                        //}
                        //else
                        //{
                        //    user_no = -1;
                        //    retStatus = Convert.ToInt32(CustomStatus.Error);     //username, password incorrect
                        //}
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateLogin-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to retrieve single record from User_Acc table.
                /// </summary>
                /// <param name="userno">Gets record as per this userno</param>
                /// <returns>SqlDataReader</returns>
                public UserAcc GetSingleRecordByUANo(int userno)
                {
                    UserAcc objUA = new UserAcc();
                    SQLHelper objSQLHelper;
                    try
                    {

                        objSQLHelper = new SQLHelper(uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);

                        SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_USER_ACC_SP_RET_USER_ACC", objParams);

                        if (dr != null)
                        {
                            if (dr.Read())
                            {
                                objUA.UA_No = dr["ua_no"] == DBNull.Value ? 0 : int.Parse(dr["ua_no"].ToString());
                                objUA.UA_Name = dr["ua_name"] == DBNull.Value ? string.Empty : dr["ua_name"].ToString();
                                objUA.UA_Type = dr["ua_type"] == DBNull.Value ? 0 : int.Parse(dr["ua_type"].ToString());
                                objUA.UA_FullName = dr["ua_fullname"] == DBNull.Value ? string.Empty : dr["ua_fullname"].ToString();
                                objUA.UA_Acc = dr["ua_acc"] == DBNull.Value ? string.Empty : dr["ua_acc"].ToString();
                                objUA.UA_Dec = dr["ua_dec"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_dec"].ToString());
                                objUA.UA_IDNo = dr["ua_idno"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_idno"].ToString());
                                objUA.UA_DeptNo = dr["ua_deptno"] == DBNull.Value ? string.Empty : dr["ua_deptno"].ToString();
                                objUA.UA_FirstLogin = dr["ua_firstlog"] == DBNull.Value ? true : Convert.ToBoolean(dr["ua_firstlog"]);
                                objUA.UA_Status = dr["ua_status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_status"]);
                                objUA.UA_section = dr["ua_section"] == DBNull.Value ? string.Empty : dr["ua_section"].ToString();
                                objUA.COLLEGE_CODE = dr["UA_COLLEGE_NOS"].ToString();
                                objUA.UA_Desig = dr["ua_desig"] == DBNull.Value ? string.Empty : dr["ua_desig"].ToString();
                                objUA.UA_EmpDeptNo = dr["UA_EMPDEPTNO"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UA_EMPDEPTNO"]);
                                objUA.OrganizationId = dr["ORGANIZATIONID"] == DBNull.Value ? 0 : int.Parse(dr["ORGANIZATIONID"].ToString());
                            }
                        }
                        if (dr != null)
                            dr.Close();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetSingleRecordByUANo-> " + ex.ToString());
                    }
                    return objUA;
                }

                public int Failedlogin(string ua_name, string ipAddress, string macAddress, DateTime login)
                {
                    int retID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New log
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);
                        objParams[1] = new SqlParameter("@P_LOGINTIME", login);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[3] = new SqlParameter("@P_MACADDRESS", macAddress);
                        objParams[4] = new SqlParameter("@P_ID", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOGFILE_SP_INS_LOGFILE_FAIL", objParams, true);
                        if (ret != null)
                            retID = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.AddtoLog-> " + ex.ToString());
                    }

                    return retID;
                }
                public int ValidateResetPassword(string username, string email, string pwd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_UA_NAME", username);
                        objParams[1] = new SqlParameter("@P_UA_EMAIL", email);
                        objParams[2] = new SqlParameter("@P_PWD", pwd);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;


                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_RESET_PASSWORD", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            //pwd = (ret.ToString());
                            retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //password reset
                        }
                        else
                        {
                            //    pwd = "-1";
                            retStatus = Convert.ToInt32(CustomStatus.Error);     // password not set
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateLogin-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataTableReader GetUserByUANo(int userno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_USER_ACC", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserByUANo-> " + ex.ToString());
                    }
                    return dtr;
                }
                /// <summary>
                /// This method is used to retrieve single record from User_Acc table.
                /// </summary>
                /// <param name="userId">Gets record as per this userId</param>
                /// <param name="usertype">Gets record as per this usertype</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleRecordByUAID(int userId, int usertype)
                {
                    SqlDataReader dr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("P_UA_IDNO", userId);
                        objParams[1] = new SqlParameter("P_UA_TYPE", usertype);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_USER_ACC_SP_RET_USER_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetSingleRecordByUAID-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This method is used to check user 
                /// </summary>
                /// <param name="userno">Check user as per this userno</param>
                /// <param name="usertype">Check user as per this usertype</param>
                /// <returns>Integer Custom Status</returns>
                public int CheckUser(int userno, int usertype)
                {
                    //int retval;
                    int retstatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", userno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", usertype);
                        objParams[2] = new SqlParameter("@P_FLAG", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_CHECKUSERID", objParams, true);
                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) > 0)
                                retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                            else
                                retstatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                        }
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        if (!ex.ToString().Contains("ORA-01403"))
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.CheckUser-> " + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// This method is used to add new user.
                /// </summary>
                /// <param name="objUA">Object of UserAcc class</param>
                /// <returns>Integer Custom Status</returns>

                /// <summary>
                /// This method is used to add new user.
                /// </summary>
                /// <param name="objUA">Object of UserAcc class</param>
                /// <returns>Integer Custom Status</returns>
                public int AddUser(UserAcc objUA, string deptNos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[3] = new SqlParameter("@P_UA_IDNO", objUA.UA_IDNo);
                        objParams[4] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[5] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[6] = new SqlParameter("@P_UA_DESIG", objUA.UA_Desig);
                        objParams[7] = new SqlParameter("@P_UA_EMAIL", objUA.UA_Email);
                        objParams[8] = new SqlParameter("@P_UA_DEPTNO", deptNos);      //Added by Nikhil L. on 15/01/2022 to insert multiple department.
                        objParams[9] = new SqlParameter("@P_UA_QTRNO", objUA.UA_QtrNo);
                        objParams[10] = new SqlParameter("@P_UA_STATUS", objUA.UA_Status);
                        objParams[11] = new SqlParameter("@P_UA_EMPST", objUA.UA_EmpSt);
                        objParams[12] = new SqlParameter("@P_UA_EMPDEPTNO", objUA.UA_EmpDeptNo);
                        objParams[13] = new SqlParameter("@P_PARENT_USERTYPE", objUA.Parent_UserType);
                        objParams[14] = new SqlParameter("@P_UA_MOBILE", objUA.MOBILE);
                        objParams[15] = new SqlParameter("@P_UA_DEC", objUA.UA_Dec);
                        objParams[16] = new SqlParameter("@P_UA_SECTION", objUA.UA_section);
                        objParams[17] = new SqlParameter("@P_COLLEGE_ID", objUA.College_No);
                        objParams[18] = new SqlParameter("@P_REMARK", objUA.UA_Remark);
                        objParams[19] = new SqlParameter("@P_USERNO", objUA.UA_Userno);
                        objParams[20] = new SqlParameter("@P_SOURCEPAGENO", objUA.UA_SourcePageNo);
                        objParams[21] = new SqlParameter("@P_ORGID", objUA.OrganizationId);
                        objParams[22] = new SqlParameter("@P_UA_NO", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_INS_USER", objParams, true);//) != null)
                        retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// This method is used to update user details from User_Acc table.
                /// </summary>
                /// <param name="objUA">Object of UserAcc class</param>
                /// <returns>Integer Custom Status</returns>
                public int UpdateUser(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Faculty Existing User
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[2] = new SqlParameter("@P_UA_IDNO", objUA.UA_IDNo);
                        objParams[3] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[4] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[5] = new SqlParameter("@P_UA_DESIG", objUA.UA_Desig);
                        objParams[6] = new SqlParameter("@P_UA_EMAIL", objUA.UA_Email);
                        objParams[7] = new SqlParameter("@P_UA_DEPTNO", objUA.UA_DeptNo);
                        objParams[8] = new SqlParameter("@P_UA_QTRNO", objUA.UA_QtrNo);
                        objParams[9] = new SqlParameter("@P_UA_STATUS", objUA.UA_Status);
                        objParams[10] = new SqlParameter("@P_UA_EMPST", objUA.UA_EmpSt);
                        objParams[11] = new SqlParameter("@P_UA_EMPDEPTNO", objUA.UA_EmpDeptNo);
                        objParams[12] = new SqlParameter("@P_UA_DEC", objUA.UA_Dec);
                        objParams[13] = new SqlParameter("@P_UA_SECTION", objUA.UA_section);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_USER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUser -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateFirstLogin(string username)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NAME", username);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_FIRSTLOG", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateFirstLogin -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Updated by S.Patil - 13122021
                /// </summary>
                /// <param name="objUA"></param>
                /// <returns></returns>
                public int UpdateUserAcc(UserAcc objUA, string deptNos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Faculty Existing User
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[3] = new SqlParameter("@P_UA_NO", objUA.UA_No);
                        objParams[4] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[5] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[6] = new SqlParameter("@P_UA_DESIG", objUA.UA_Desig);
                        objParams[7] = new SqlParameter("@P_UA_EMAIL", objUA.UA_Email);
                        objParams[8] = new SqlParameter("@P_UA_DEPTNO", deptNos);       //Added by Nikhil L. on 15/01/2022 to insert multiple department.
                        objParams[9] = new SqlParameter("@P_UA_QTRNO", objUA.UA_QtrNo);
                        objParams[10] = new SqlParameter("@P_UA_STATUS", objUA.UA_Status);
                        objParams[11] = new SqlParameter("@P_UA_EMPST", objUA.UA_EmpSt);
                        objParams[12] = new SqlParameter("@P_UA_EMPDEPTNO", objUA.UA_EmpDeptNo);
                        objParams[13] = new SqlParameter("@P_UA_DEC", objUA.UA_Dec);
                        objParams[14] = new SqlParameter("@P_UA_MOBILE", objUA.MOBILE);
                        objParams[15] = new SqlParameter("@P_UA_SECTION", objUA.UA_section);
                        objParams[16] = new SqlParameter("@P_COLLEGE_ID", objUA.College_No);
                        objParams[17] = new SqlParameter("@P_USERNO", objUA.UA_Userno);
                        objParams[18] = new SqlParameter("@P_REMARK", objUA.UA_Remark);
                        objParams[19] = new SqlParameter("@P_SOURCEPAGENO", objUA.UA_SourcePageNo);

                        objParams[20] = new SqlParameter("@P_ORGID", objUA.OrganizationId);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_USERACC", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUser -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddtoLogTran(string ua_name, string ipAddress, string macAddress, DateTime login)
                {
                    int retID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New log
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UA_NAME", ua_name);
                        objParams[1] = new SqlParameter("@P_LOGINTIME", login);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[3] = new SqlParameter("@P_MACADDRESS", macAddress);
                        objParams[4] = new SqlParameter("@P_ID", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOGFILE_SP_INS_LOGFILE", objParams, true);
                        if (ret != null)
                            retID = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.AddtoLog-> " + ex.ToString());
                    }

                    return retID;
                }
                public DataSet FindUser(string search, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_SEARCH_USER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.FindUser-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetUsersByUserType(int usertype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", usertype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_USERS_BY_USERTYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }

                public object GetUserLinksByUserType(int usertype)
                {
                    object links = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", usertype);

                        links = objSQLHelper.ExecuteScalarSP("PKG_USER_ACC_SP_RET_USERLINK_BY_USERTYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserLinksByUserType-> " + ex.ToString());
                    }
                    return links;
                }

                public int UpdateUserLinks(int usertype, string links, string uanos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Links of Existing User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UATYPE", usertype);
                        objParams[1] = new SqlParameter("@P_UA_ACC", links);
                        objParams[2] = new SqlParameter("@P_UA_NOS", uanos);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_ASSIGNLINKS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Modified By Rishabh on 16/12/2021 to add Orgnization Id
                /// </summary>
                /// <param name="objUA"></param>
                /// <returns></returns>
                public int AddStudentUser(int UserNo, int SourcePageNo, UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[4] = new SqlParameter("@P_STATUS", objUA.UA_Status);
                        objParams[5] = new SqlParameter("@P_EMAILID", objUA.EMAIL);
                        objParams[6] = new SqlParameter("@P_MOBILENO", objUA.MOBILE);
                        objParams[7] = new SqlParameter("@P_USERNO", UserNo);//new for maintin log table
                        objParams[8] = new SqlParameter("@P_SOURCEPAGENO", SourcePageNo);//new for maintin log table
                        objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 16/12/2021

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_CREATE_STUD_LOGIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddStudentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added by Ruchika Dhakate on 20.09.2022
                public int AddParentUser(int UserNo, int SourcePageNo, UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[4] = new SqlParameter("@P_STATUS", objUA.UA_Status);
                        objParams[5] = new SqlParameter("@P_EMAILID", objUA.EMAIL);
                        objParams[6] = new SqlParameter("@P_MOBILENO", objUA.MOBILE);
                        objParams[7] = new SqlParameter("@P_USERNO", UserNo);//new for maintin log table
                        objParams[8] = new SqlParameter("@P_SOURCEPAGENO", SourcePageNo);//new for maintin log table
                        objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_CREATE_PARENT_LOGIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddParentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Modified By Rishabh on 16/12/2021 to add Orgnization Id
                /// </summary>
                /// <param name="objUA"></param>
                /// <returns></returns>
                public int AddEmployeeUser(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[4] = new SqlParameter("@P_STATUS", objUA.UA_Status);
                        objParams[5] = new SqlParameter("@P_DEPTNO", objUA.UA_DeptNo);
                        objParams[6] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[7] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 16/12/2021

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_CREATE_EMPLOYEE_LOGIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddEmployeeUser-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //============================17-10-2014===========================
                //Reason: Tp insert & update login informion if entry is through emp. info. entry form.
                public int AddUpdateEmployeeUser(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[4] = new SqlParameter("@P_STATUS", objUA.UA_Status);
                        objParams[5] = new SqlParameter("@P_DEPTNO", objUA.UA_DeptNo);
                        objParams[6] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_INS_UPD_EMPLOYEE_LOGIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddEmployeeUser-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //=======================================================                    
                public bool CheckUser(string username)
                {
                    bool flag = false;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NAME", username);

                        object ret = objSQLHelper.ExecuteScalarSP("PKG_USER_ACC_SP_CHECK_USER", objParams);
                        if (ret == null)
                            flag = true;
                    }

                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.CheckUser-> " + ex.ToString());
                    }
                    return flag;
                }


                #region MIS_ACCOUNT_SIGNING


                public int AddMisAccountSigning(UserAcc objmis)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        //AddDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_UA_IDNO",objmis.UA_IDNo),
                                 new SqlParameter("@P_SIGNED_DATE",objmis.SIGNED_DATE),
                                 new SqlParameter("@P_SIGNED",objmis.SIGNED),
                                 new SqlParameter("@P_IP_ADDRESS",objmis.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objmis.MAC_ADDRESS),                                 
                                 new SqlParameter("@P_AUDIT_DATE ",objmis.AUDIT_DATE),
                                 new SqlParameter("@P_USER_ID",objmis.USER_ID),
                                 new SqlParameter("@P_COLLEGE_CODE",objmis.COLLEGE_CODE)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_INST_MIS_ACCOUNT_SIGNING", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.User_AccController.AddMisAccountSigning-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMisAccountSigningByUAtype(int uaType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USER_TYPE",uaType)                              
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_MIS_ACCOUNT_SIGNING", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.User_AccController.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetEmployeeInformation(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_IDNO",idno)                              
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_EMPLOYEE_INFO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeInformation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetMisStudentInformation(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_IDNO",idno)                              
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MIS_STUDENT", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetMisStudentInformation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                #endregion

                public int UpdateLinkDeptwise(string ua_nos, string links)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[3];
                        objParam[0] = new SqlParameter("@P_UA_NO", ua_nos);
                        objParam[1] = new SqlParameter("@P_UA_ACC", links);
                        objParam[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ASSIGN_LINK_BY_DEPARTMENTWISE", objParam, false);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.UpdateUserLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }


                public void UpdateUserPassword(int p, int p_2, string p_3)
                {
                    throw new NotImplementedException();
                }

                public int UpdateUserCategory(int usertype, string links, string uanos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Links of Existing User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UATYPE", usertype);
                        objParams[1] = new SqlParameter("@P_UA_ACC", links);
                        objParams[2] = new SqlParameter("@P_UA_NOS", uanos);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_UPD_ASSIGN_CATEGORY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserCategory -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetUsertype(int UATYPE)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UATYPE);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_USER_Type", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsertype-> " + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet CheckUserEmailMobile(int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_LOGFILE_CHECK_UA_EMAIL_UA_MOBILE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.CheckUserEmailMobile-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateUserEmailMobile(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        //Check whether username/password exists
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[1] = new SqlParameter("@P_UA_EMAIL", objUserAcc.EMAIL);
                        objParams[2] = new SqlParameter("@P_UA_MOBILE", objUserAcc.MOBILE);
                        objParams[3] = new SqlParameter("@P_UA_STATUS", SqlDbType.Int);
                        objParams[3].Direction = System.Data.ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOGFILE_UPDATE_UA_EMAIL_UA_MOBILE", objParams, true);

                        if (Convert.ToInt32(ret) == 0)
                            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                        else
                            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int ChangePasswordByadminFirstLog_1(UserAcc objUserAcc)
                //{
                //    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                //    SqlParameter[] objParams = null;
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        //Check whether username/password exists
                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                //        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", objUserAcc.UA_Pwd);
                //        objParams[2] = new SqlParameter("@P_OLD_UA_PWD", objUserAcc.UA_OldPwd);
                //        objParams[3] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                //        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
                //        objParams[4].Direction = System.Data.ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_PASSWORD_BYADMIN_FIRSTLOG_1", objParams, true);

                //        if (Convert.ToInt16(ret) == 0)
                //            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                //        else
                //            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32((CustomStatus.Error));
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                //modification done on date 04/11/2019 
                //public int ChangePasswordByadminFirstLog_1(UserAcc objUserAcc)
                //{
                //    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                //    SqlParameter[] objParams = null;
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    Common objCommon = new Common();
                //    string user_pwd = string.Empty;
                //    string db_pwd = string.Empty;
                //    db_pwd = (objCommon.LookUp("USER_ACC", "UA_PWD", "UA_NAME='" + (objUserAcc.UA_Name).ToString() + "'"));
                //    try
                //    {
                //        //Check whether username/password exists
                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                //        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_Pwd));
                //        objParams[2] = new SqlParameter("@P_OLD_UA_PWD", objUserAcc.UA_OldPwd);
                //        objParams[3] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                //        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
                //        objParams[4].Direction = System.Data.ParameterDirection.Output;
                //        user_pwd = clsTripleLvlEncyrpt.EncryptPassword(objUserAcc.UA_OldPwd);       // Encrypt withMasterSoft Logic
                //        user_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(user_pwd);         // Encrypt with One AES
                //        db_pwd = clsTripleLvlEncyrpt.RSADecryption(db_pwd.ToString());

                //        if (user_pwd == db_pwd)
                //        {

                //            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_PASSWORD_BYADMIN_FIRSTLOG_1", objParams, true);


                //            if (Convert.ToInt16(ret) == 0)
                //                retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                //            else
                //                retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));
                //        }
                //        else
                //        {

                //            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32((CustomStatus.Error));
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int ChangePasswordByadminFirstLog_1(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    Common objCommon = new Common();
                    string user_pwd = string.Empty;
                    string db_pwd = string.Empty;
                    db_pwd = (objCommon.LookUp("USER_ACC", "UA_PWD", "UA_NAME='" + (objUserAcc.UA_Name).ToString() + "'"));
                    try
                    {
                        //Check whether username/password exists
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_Pwd));
                        objParams[2] = new SqlParameter("@P_OLD_UA_PWD", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_OldPwd));
                        objParams[3] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[4].Direction = System.Data.ParameterDirection.Output;
                        user_pwd = clsTripleLvlEncyrpt.EncryptPassword(objUserAcc.UA_OldPwd);       // Encrypt withMasterSoft Logic
                        user_pwd = clsTripleLvlEncyrpt.OneAESEncrypt(user_pwd);         // Encrypt with One AES
                        db_pwd = clsTripleLvlEncyrpt.RSADecryption(db_pwd.ToString());

                        if (user_pwd == db_pwd)
                        {

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_PASSWORD_BYADMIN_FIRSTLOG_1", objParams, true);


                            if (Convert.ToInt16(ret) == 0)
                                retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                            else
                                retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateUserACCOUNTNO(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        //Check whether username/password exists
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[1] = new SqlParameter("@P_UA_ACCNO", objUserAcc.UA_AccNo);
                        objParams[2] = new SqlParameter("@P_UA_IFSC", objUserAcc.UA_IFSC);
                        objParams[3] = new SqlParameter("@P_UA_STATUS", SqlDbType.Int);
                        objParams[3].Direction = System.Data.ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOGFILE_UPDATE_UA_ACCNO", objParams, true);


                        if (Convert.ToInt32(ret) == 0)
                            retStatus = Convert.ToInt32(CustomStatus.Others);
                        else
                            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //added by Sandeep Karupalli on 12-12-2017 for Dashboard
                public int InsertDashboardconfigdetails(int usertype, string dashboard)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        objParams[1] = new SqlParameter("@P_DASHBOARD", dashboard);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_DASHBOARD_CONFIG", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboardconfigdetails-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //added by Aayushi Gupta on 04-02-2019 for Dashboard
                public int InsertDashboardconfigdetails_Onlineadm(int usertype, string dashboard)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        objParams[1] = new SqlParameter("@P_DASHBOARD", dashboard);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_DASHBOARD_CONFIG_ONLINE_ADM", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboardconfigdetails-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int Insertacademicroommap(int deptno, string roomno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACADEMIC_ROOMMAPPING", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboardconfigdetails-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataTableReader GetUserBydashidno(int userno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_idno", userno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_GETIDNODASHBOARD", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserByUANo-> " + ex.ToString());
                    }
                    return dtr;
                }

                //AAYUSHI GUPTA 04/02/2019
                public DataTableReader GetUserBydashidno_online(int userno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_idno", userno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_GETIDNODASHBOARD_ONLINE", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserByUANo-> " + ex.ToString());
                    }
                    return dtr;
                }
                //Modified By Rishabh on 30/10/2021
                public int InsertDashboarddetails(string dashboardname, int sessionno, bool status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DASHBOARDNAME", dashboardname);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_DASHBOARDNAME", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboarddetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //Modified By Rishabh on 30/10/2021
                public int InsertDashboarddetails_OnlineADM(string dashboardname, int admbatch, bool status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DASHBOARDNAME", dashboardname);
                        objParams[1] = new SqlParameter("@P_BATCHNO", admbatch);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_DASHBOARDNAME_ONLINE_ADM", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboarddetails-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int UpdateDashboarddetails(string idno, string dashboardname, bool status, int session)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add New User
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DASHBOARDNAME", dashboardname);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_DASHBOARDNAME", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboarddetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //Modified By Rishabh on 30/10/2021
                public int UpdateDashboarddetails_OnlineAdm(string idno, string dashboardname, bool status, int admbatch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add New User
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DASHBOARDNAME", dashboardname);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_BATCHNO", admbatch);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_DASHBOARDNAME_ONLINE_ADM", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertDashboarddetails-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet Admbatchwisecount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_ADMBATCHCOUNT");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet getlistrooms()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_GETDEPMAPROOMLIST");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetallDashboard(int value, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VALUE", value);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DASHBOARD_MASTER_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;

                }


                public DataSet GetallDashboard_onlineadm(int value, int batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VALUE", value);
                        objParams[1] = new SqlParameter("@P_BATCHNO", batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DASHBOARD_MASTER_ALL_ONLINE_ADMISSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;

                }


                public DataSet GetAttendancedetails(int idno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_IDNO", idno);
                        objparams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETATTENDANCE_DETAILS", objparams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetDegreeResult(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DEGREERESULT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFeecollection(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];

                        objparams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TOTALFEECOLLECTION", objparams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeeReceipttype(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETFEERECEIPTCODE", objparams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet Getresultdetails(string regno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_REGNO", regno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETRESULT_DETAILS", objparams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTotaladmcountyear(int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_BRANCHNO", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETTOTAL_ADMISSION_COUNT", objparams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTotalfeeamountyearwise(int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETTOTAL_FEES_AMOUNT", objparams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Dashboard Payroll
                public DataSet GetAllEmployees(int value)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VALUE", value);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_EMPLOYEE_DASHBOARD_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet GetEmployeeStaffDetails(int value)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VALUE", value);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_EMPLOYEE_DASHBOARD_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmployeeAttendanceDetails(DateTime dt, int value)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Date", dt);
                        objParams[1] = new SqlParameter("@P_VALUE", value);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_DASHBOARD_GET_EMP_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                //End DashBoard

                //Added by Deepali on 21/09/2020
                public UserAcc GetSingleRecordByGoogleUser(string slogdata, string idtoken)
                {
                    UserAcc objUA = new UserAcc();



                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_SLOGDATA", slogdata);
                        objParams[1] = new SqlParameter("@P_UA_IDTOKEN", idtoken);



                        SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_USER_ACC_SP_RET_USER_ACC_GOOGLESIGN", objParams);



                        if (dr != null)
                        {
                            if (dr.Read())
                            {
                                objUA.UA_No = dr["ua_no"] == DBNull.Value ? 0 : int.Parse(dr["ua_no"].ToString());
                                objUA.UA_Name = dr["ua_name"] == DBNull.Value ? string.Empty : dr["ua_name"].ToString();
                                objUA.UA_Pwd = dr["ua_pwd"] == DBNull.Value ? string.Empty : dr["ua_pwd"].ToString();
                                objUA.UA_Type = dr["ua_type"] == DBNull.Value ? 0 : int.Parse(dr["ua_type"].ToString());
                                objUA.UA_FullName = dr["ua_fullname"] == DBNull.Value ? string.Empty : dr["ua_fullname"].ToString();
                                objUA.UA_Acc = dr["ua_acc"] == DBNull.Value ? string.Empty : dr["ua_acc"].ToString();
                                objUA.UA_Dec = dr["ua_dec"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_dec"].ToString());
                                objUA.UA_IDNo = dr["ua_idno"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_idno"].ToString());
                                objUA.UA_DeptNo = dr["ua_deptno"] == DBNull.Value ? string.Empty : dr["ua_deptno"].ToString();
                                objUA.UA_FirstLogin = dr["ua_firstlog"] == DBNull.Value ? true : Convert.ToBoolean(dr["ua_firstlog"]);
                                objUA.UA_Status = dr["ua_status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_status"]);
                                objUA.UA_Desig = dr["UA_DESIG"] == DBNull.Value ? string.Empty : dr["UA_DESIG"].ToString();
                                objUA.UA_EmpDeptNo = dr["ua_empdeptno"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ua_empdeptno"]);
                                //  Help and Freshdesk Feedback CSS  added by Swapnil start  // 23/09/2019
                                // objUA.ua_Freshdesk_Status = dr["FRESHDESK_STATUS"] == DBNull.Value ? 0 : int.Parse(dr["FRESHDESK_STATUS"].ToString());
                                //  Help and Freshdesk Feedback CSS  added by Swapnil end  // 23/09/2019
                            }
                        }
                        if (dr != null) dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetSingleRecordByUANo-> " + ex.ToString());
                    }
                    return objUA;
                }

                //Added by Deepali For checking last 5 paasword
                public DataSet CheckPassword(UserAcc objUserAcc)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        string user_pwd = string.Empty;
                        string db_pwd = string.Empty;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[2] = new SqlParameter("@P_UA_TYPE", objUserAcc.UA_Type);
                        objParams[3] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[3].Direction = System.Data.ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_USER_PASSWORD_HIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.CheckUserEmailMobile-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by S.Patil -05112020
                /// </summary>
                /// <param name="ua_no"></param>
                /// <param name="acc_token"></param>
                /// <param name="FbID"></param>
                /// <returns></returns>
                public int UpdateFB_Token(int ua_no, string acc_token, string FbID, string IP)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Links of Existing User
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UANO", ua_no);
                        objParams[1] = new SqlParameter("@P_ACCT", acc_token);
                        objParams[2] = new SqlParameter("@P_FBID", FbID);
                        objParams[3] = new SqlParameter("@P_IP", IP);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_FB_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateUserLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }
                //Added by Deepali on 05/11/2020
                public int InsertChangePassword(UserAcc objUserAcc)
                {
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams = null;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUserAcc.UA_Name);
                        objParams[1] = new SqlParameter("@P_NEW_UA_PWD", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_Pwd));
                        objParams[2] = new SqlParameter("@P_UA_NO", objUserAcc.UA_No);
                        objParams[3] = new SqlParameter("@P_IP_ADDRESS", objUserAcc.IP_ADDRESS);
                        objParams[4] = new SqlParameter("@P_UA_TYPE", objUserAcc.UA_Type);
                        objParams[5] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[5].Direction = System.Data.ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_USER_PASSWORD_CHANGE", objParams, true);


                        if (Convert.ToInt16(ret) == 0)
                            retStatus = Convert.ToInt32(CustomStatus.InvalidUserNamePassword);
                        else
                            retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY SUMIT FOR GET T&P STUDENT FOR TABULER FORM ON 28042020
                public DataSet GetTpAppliedRejectOnTabulerform(int value, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VALUE", value);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_RECORD_ON_TABULERFORM_ON_TP_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;

                }

                // Added by Nikhil Vinod Lambe on 23/03/2021 to add Affiliated User
                public int AddAffiliatedUser(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[3] = new SqlParameter("@P_UA_IDNO", objUA.UA_IDNo);
                        objParams[4] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[5] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[6] = new SqlParameter("@P_UA_DESIG", objUA.UA_Desig);
                        objParams[7] = new SqlParameter("@P_UA_EMAIL", objUA.UA_Email);
                        objParams[8] = new SqlParameter("@P_UA_DEPTNO", objUA.UA_DeptNo);
                        objParams[9] = new SqlParameter("@P_UA_QTRNO", objUA.UA_QtrNo);
                        objParams[10] = new SqlParameter("@P_UA_STATUS", objUA.UA_Status);
                        objParams[11] = new SqlParameter("@P_UA_EMPST", objUA.UA_EmpSt);
                        objParams[12] = new SqlParameter("@P_UA_EMPDEPTNO", objUA.UA_EmpDeptNo);
                        objParams[13] = new SqlParameter("@P_PARENT_USERTYPE", objUA.Parent_UserType);
                        objParams[14] = new SqlParameter("@P_UA_MOBILE", objUA.MOBILE);
                        objParams[15] = new SqlParameter("@P_UA_DEC", objUA.UA_Dec);
                        objParams[16] = new SqlParameter("@P_UA_SECTION", objUA.UA_section);
                        objParams[17] = new SqlParameter("@P_COLLEGE_ID", objUA.College_No);
                        objParams[18] = new SqlParameter("@P_UA_NO", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_INS_AFFILIATED_USER", objParams, true);//) != null)
                        retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }
                // Added by Nikhil Vinod Lambe on 23/03/2021 to update Affiliated User
                public int UpdateAffiliatedUserAcc(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Faculty Existing User
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[1] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[2] = new SqlParameter("@P_UA_ACC", objUA.UA_Acc);
                        objParams[3] = new SqlParameter("@P_UA_NO", objUA.UA_No);
                        objParams[4] = new SqlParameter("@P_UA_TYPE", objUA.UA_Type);
                        objParams[5] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[6] = new SqlParameter("@P_UA_DESIG", objUA.UA_Desig);
                        objParams[7] = new SqlParameter("@P_UA_EMAIL", objUA.UA_Email);
                        objParams[8] = new SqlParameter("@P_UA_DEPTNO", objUA.UA_DeptNo);
                        objParams[9] = new SqlParameter("@P_UA_QTRNO", objUA.UA_QtrNo);
                        objParams[10] = new SqlParameter("@P_UA_STATUS", objUA.UA_Status);
                        objParams[11] = new SqlParameter("@P_UA_EMPST", objUA.UA_EmpSt);
                        objParams[12] = new SqlParameter("@P_UA_EMPDEPTNO", objUA.UA_EmpDeptNo);
                        objParams[13] = new SqlParameter("@P_UA_DEC", objUA.UA_Dec);
                        objParams[14] = new SqlParameter("@P_UA_MOBILE", objUA.MOBILE);
                        objParams[15] = new SqlParameter("@P_UA_SECTION", objUA.UA_section);
                        objParams[16] = new SqlParameter("@P_COLLEGE_ID", objUA.College_No);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPD_AFFILIATED_USER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.UpdateAffiliatedUserAcc -> " + ex.ToString());
                    }
                    return retStatus;
                }
                // Added by Nikhil Vinod Lambe on 23/03/2021 to get Affiliated User
                public DataTableReader GetAffiliatedUserByUANo(int userno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_AFFILIATED_USER_ACC_SP_RET_USER_ACC", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserByUANo-> " + ex.ToString());
                    }
                    return dtr;
                }

                public int AddStudentUserForExcelUpload(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_UA_FULLNAME", objUA.UA_FullName);
                        objParams[4] = new SqlParameter("@P_STATUS", objUA.UA_Status);
                        objParams[5] = new SqlParameter("@P_EMAILID", objUA.EMAIL);
                        objParams[6] = new SqlParameter("@P_MOBILENO", objUA.MOBILE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CREATE_STUD_LOGIN_FOR_EXCEL_UPLOAD", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_CREATE_STUD_LOGIN_FOR_EXCEL_UPLOAD", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddStudentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ValidateResetPassword(string email, string pwd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_UA_EMAIL", email);
                        objParams[1] = new SqlParameter("@P_PWD", pwd);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;


                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_RESET_PASSWORD", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            //pwd = (ret.ToString());
                            retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //password reset
                        }
                        else
                        {
                            //    pwd = "-1";
                            retStatus = Convert.ToInt32(CustomStatus.Error);     // password not set
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateLogin-> " + ex.ToString());
                    }
                    return retStatus;
                }



                // reset password on mobile 

                public int ValidateResetPasswordMobile(string mobile, string pwd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MOBILENO", mobile);
                        objParams[1] = new SqlParameter("@P_PWD", pwd);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;


                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_RESET_PASSWORDMOB", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            //pwd = (ret.ToString());
                            retStatus = Convert.ToInt32(CustomStatus.ValidUser);      //password reset
                        }
                        else
                        {
                            //    pwd = "-1";
                            retStatus = Convert.ToInt32(CustomStatus.Error);     // password not set
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateLogin-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added By Dileep Kare on 02.04.2022
                /// </summary>
                /// <param name="dt"></param>
                /// <returns></returns>
                public int Insert_TimeTable_RoomConfig(DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TIME_TABLE_ROOM_CONFIG", dt);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_INS_TIME_TABLE_ROOM_CONFIG", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    return retStatus;
                }

                //User_RightReport

                public DataSet GetUserwiseRightsAccessLinkExcel(int ua_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_UA_TYPE", ua_type);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ACCESS_LINK_FOR_REPORT_EXCEL", objparams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserwiseRightsAccessLinkExcel-> " + ex.ToString());
                    }
                    return ds;
                }
                #region SMS TEMPLATE
                public DataSet GetSMSTemplate(int PageNo, string TempName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PAGENO", PageNo);
                        objParams[1] = new SqlParameter("@P_TEMPLATE_NAME", TempName);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SMS_TEMPLATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;

                }
                #endregion SMS TEMPLATE


                #region Employee User Creation

                public DataSet GetEmployeeForUserCreation(int userTypeId, int collegeno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USERTYPEID",userTypeId) , 
                               new SqlParameter("@P_COLLEGE_NO",collegeno),
                              new SqlParameter("@P_STAFFNO",staffno)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEE_FOR_USER_CREATION", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GetEmployeeForUserCreationGetList(int userTypeId, int collegeno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USERTYPEID",userTypeId) , 
                               new SqlParameter("@P_COLLEGE_NO",collegeno),
                              new SqlParameter("@P_STAFFNO",staffno)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEE_FOR_USER_CREATION_FOR_ALL_USER", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Nikhil L. on 18/01/2022 to get users by org ID.
                /// </summary>
                /// <param name="userno"></param>
                /// <param name="orgID"></param>
                /// <returns></returns>
                public DataTableReader GetUserByUANo_New(int userno, int orgID)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);
                        objParams[1] = new SqlParameter("@P_ORGANIZATION_ID", orgID);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_USER_ACC_SP_RET_USER_ACC_NEW", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserByUANo-> " + ex.ToString());
                    }
                    return dtr;
                }

                #endregion

                #region  Role Creation
                // User Role & Bulk Link Assign added by Sneha Doble on 02/07/2021

                public int InsertUserRole(int Roleno, int Uatype, string rolename, string Description, int status, int uano, string Ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add New User
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ROLE_NO", Roleno);
                        objParams[1] = new SqlParameter("@P_USER_TYPE", Uatype);
                        objParams[2] = new SqlParameter("@P_ROLE_NAME", rolename);
                        objParams[3] = new SqlParameter("@P_ROLE_DES", Description);
                        objParams[4] = new SqlParameter("@P_STATUS", status);
                        objParams[5] = new SqlParameter("@P_UANO", uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", Ipaddress);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_USER_ROLE_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.InsertUserRole-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetUserRoleDetails(int ROLENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_ROLE_NO", ROLENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_USER_ROLE_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserRoleDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateRoleLinkUaTypewise(int uatype, int Roleno, string links)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_UA_TYPE", uatype);
                        objParam[1] = new SqlParameter("@P_ROLENO", Roleno);
                        objParam[2] = new SqlParameter("@P_UA_ACC", links);
                        objParam[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ASSIGN_LINK_TO_USER_ROLE", objParam, false);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.UpdateRoleLinkUaTypewise -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetUserRoleLinkDetails(int Uatype, int Flag)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_UA_TYPE", Uatype);
                        objparams[1] = new SqlParameter("@P_FLAG", Flag);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_USER_ROLE_LINK_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserRoleLinkDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetUserRoleLinkDomain(int RoleNo, int UA_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ROLE_NO", RoleNo);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", UA_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BULKLINK_ACCESS_DOMAIN_USERROLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserRoleLinkDomain-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetRolewiseAssignlinks(string RoleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROLE_NO", RoleNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UA_ACC_LINK", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetRolewiseAssignlinks-> " + ex.ToString());
                    }
                    return ds;
                }

                //changed by arjun for maintain session user and source page no 13-01-2023
                public int UpdateLinkRolewise(int UserNo, int SourcePageNo, string ua_nos, string links, string RoleNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParam[1] = new SqlParameter("@P_SOURCEPAGENO", SourcePageNo);
                        objParam[2] = new SqlParameter("@P_UA_NO", ua_nos);
                        objParam[3] = new SqlParameter("@P_UA_ACC", links);
                        objParam[4] = new SqlParameter("@P_ROLE_NO", RoleNo);
                        objParam[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_USER_ROLE_ASSIGN", objParam, false);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.UpdateLinkRolewise -> " + ex.ToString());
                    }
                    return retStatus;
                }

                // End Role Creation
                #endregion End Role Creation

              
                public int UpdateStudentUserPwdRandom(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 16/12/2021

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPDATE_RANDOM_PWD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddStudentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CopyStudentUserPwdRandom(UserAcc objUA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objUA.UA_IDNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", objUA.UA_Name);
                        objParams[2] = new SqlParameter("@P_UA_PWD", objUA.UA_Pwd);
                        objParams[3] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 16/12/2021

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ACC_SP_UPDATE_COPY_PWD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddStudentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added By Dileep Kare 
                /// Date :10.11.2021
                /// </summary>
                /// <param name="value"></param>
                /// <param name="sessionno"></param>
                /// <returns></returns>
                public DataSet Get_Faculty_For_Active_Deative(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_FOR_ACTIVE_DEATCIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.Get_Faculty_For_Active_Deative-> " + ex.ToString());
                    }
                    return ds;

                }

                /// <summary>
                /// Added Dileep Kare
                /// Date :10.11.2021
                /// </summary>
                /// <param name="Sessionno"></param>
                /// <param name="Ua_nos"></param>
                /// <param name="Active_Deactive_by"></param>
                /// <param name="ipaddress"></param>
                /// <returns></returns>
                public int Active_Deactive_Faculty_Login(int Sessionno, string Ua_nos, int Active_Deactive_by, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NOS", Ua_nos);
                        objParams[2] = new SqlParameter("@P_ACTVIE_DEACTIVE_BY", Active_Deactive_by);
                        objParams[3] = new SqlParameter("@P_IP_ADDRESS", ipaddress);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ACTVIE_DEACTIVE_FACULTY_LOGIN", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_CREATE_STUD_LOGIN_FOR_EXCEL_UPLOAD", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddStudentUser-> " + ex.ToString());
                    }
                    return retStatus;
                }
                
                
                ///<summary>
                /// This Method is used to Dipslay Department Wise Faculty
                /// Created By Nikhil Shende Date:-02/09/2022
                /// For Display Department Wise Faculty As Per Yograj Sir
                ///</summary>
                ///
                public DataSet GetDepartmentWiseFaculty(int faculty, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FACULTY_TYPE", faculty);
                        objParams[1] = new SqlParameter("@P_DEPARTMENT", deptno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = System.Data.ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DEPARTMENT_WISE_FACULTIES", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserLinkDomain-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ValidateActiveStatusOfStudent(int sessionno, int DegreeNo, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_CHECK_UA_STATUS", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ValidateActiveStatusOfStudent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEmailTemplateConfigData(int TEMPTYPEID, int TEMPLATEID, int USERNO, string APPLICATIONID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TEMPTYPEID", TEMPTYPEID);
                        objParams[1] = new SqlParameter("@P_TEMPLATEID", TEMPLATEID);
                        objParams[2] = new SqlParameter("@P_USERNO", USERNO);
                        objParams[3] = new SqlParameter("@P_APPLICATIONID", APPLICATIONID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_EMAILTEMPLATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetEmailTemplateConfigData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //added by kajal jaiswal on 22-12-2022
                public DataSet GetUserRole(int usertype, int degree, int branch, int semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_USER_TYPE", usertype);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_ROLE_ASSIGN", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserLinkDomain-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By Tejaswini Dhoble
                public DataSet GetTemplateData(int TEMPTYPEID, int USERTYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TEMPTYPEID", TEMPTYPEID);
                        objParams[1] = new SqlParameter("@P_USERTYPEID", USERTYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TEMPLATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetTemplateData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GetWhatsappTemplate(int PageNo, string TempName)
                    {
                    DataSet ds = null;
                    try
                        {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PAGENO", PageNo);
                        objParams[1] = new SqlParameter("@P_TEMPLATE_NAME", TempName);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WHATSAPP_TEMPLATE_DETAILS", objParams);
                        }
                    catch (Exception ex)
                        {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                        }
                    return ds;

                    }

                public int InsertOTP(string OTP, int userno, int otpid, bool IsSucess, string CommandType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_OTP", OTP);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        objParams[2] = new SqlParameter("@P_OTPID", otpid);
                        objParams[3] = new SqlParameter("@P_CommandType", CommandType);
                        objParams[4] = new SqlParameter("@P_IsSuccess", IsSucess);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INSERT_USER_OTP", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32((CustomStatus.RecordSaved));
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.VerifyEmail-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Vipul Tichakule on 16-11-2023
                /// </summary>
                /// <returns></returns>
                public DataSet GetDataValidation(string queryname)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_DATE",);
                        objParams[0] = new SqlParameter("@P_QUERYNAME", queryname);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA_VALIDATION_CHECK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.GetDataValidation -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Vipul Tichakule on 16-11-2023
                /// </summary>
                /// <returns></returns>
                public int UpdateDataValidation(string value)
                {
                    //DataSet ds = null;
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_NEWDATE", DateTime.Now.ToString("yyyy-MM-dd"));
                        objParams[1] = new SqlParameter("@P_ID", value);

                        if (objSQLHelper.ExecuteNonQuerySP("ACD_PGK_UPDATE_DATA_VALIDATEE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA_VALIDATION_CHECK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.UpdateDataValidation -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Vipul Tichakule on 16-11-2023
                /// </summary>
                /// <returns></returns>
                public DataSet Downloaddatavalidate(int ID, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        //objParams[0] = new SqlParameter("@P_DATE",);
                        objParams[0] = new SqlParameter("@P_ID", ID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_PKG_QUERY_TO_DOWNLOAD_DATAVALIDATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.GetDataValidation -> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS  