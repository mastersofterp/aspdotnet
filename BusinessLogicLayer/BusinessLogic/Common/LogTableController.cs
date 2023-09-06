using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This LogTableController is used to control Logfile table.
            /// </summary>
            public static class LogTableController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private static string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method is used to add username & logintime in the Logfile table.
                /// </summary>
                /// <param name="ua_name">String parameter contains username.</param>
                /// <param name="login">DateTime parameter contains login time.</param>
                /// <returns>Integer CustomStatus - RecordAdded or Error.</returns>
                public static int AddtoLog(string ua_name,string ipAddress,string macAddress,DateTime login)
                {
                    int retID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
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

                /// <summary>
                /// This method is used to update Logfile table.
                /// </summary>
                /// <param name="objLog">objLog is the object of LogFile class.</param>
                /// <returns>Integer CustomStatus - RecordUpdated or Error.</returns>
                public static int UpdateLog(LogFile objLog)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty LogFile
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NAME", objLog.Ua_Name);
                        objParams[1] = new SqlParameter("@P_LOGOUTTIME", objLog.LogoutTime);
                        objParams[2] = new SqlParameter("@P_LOGID", objLog.ID);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_LOGFILE_SP_UPD_LOGFILE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This table is used to get all login details from Logfile table.
                /// </summary>
                /// <param name="objLog">objLog is the object of LogFile class</param>
                /// <returns>DataSet</returns>
                public static DataSet GetAllLogFile(LogFile objLog, string uaname)
                {
                    DataSet dsLog = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANAME", uaname);
                        dsLog = objSQLHelper.ExecuteDataSetSP("PKG_LOGFILE_SP_ALL_LOGFILE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsLog;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetAllLogFile-> " + ex.ToString());
                    }
                    return dsLog;
                }
                public static DataSet GetAllLogDetails(int LogId)
                {
                    DataSet dsLogDetails = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LOG_ID", LogId);
                        dsLogDetails = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_VIEW_USER_ACTIVITY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsLogDetails;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetAllLogFile-> " + ex.ToString());
                    }
                    return dsLogDetails;
                }
                public static DataSet GetAllLogFileDate(LogFile objLog, string uaname, string Fromdt, string todate)
                {
                    DataSet dsLog = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UANAME", uaname);
                        objParams[1] = new SqlParameter("@P_FRMDATE", Fromdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todate);
                        dsLog = objSQLHelper.ExecuteDataSetSP("PKG_LOGFILE_SP_ALL_LOGFILE_DATE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsLog;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetAllLogFile-> " + ex.ToString());
                    }
                    return dsLog;
                }
                /// <summary>
                /// This method is used to get login details of selected user.
                /// </summary>
                /// <param name="name">Get login details as per this name.</param>
                /// <returns>SqlDataReader</returns>
                public static SqlDataReader GetLogDetail(string name)
                {
                    SqlDataReader dr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NAME", name);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_LOGFILE_SP_RET_LOGBYUA_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetLogDetail-> " + ex.ToString());
                    }

                    return dr;
                }
                /// <summary>
                /// Added by abhishek shirpurkar
                /// Date : 06/09/2019
                /// </summary>
                /// <param name="userid"></param>
                /// <returns></returns>
                public static DataSet SearchUser(string userid)
                {
                    DataSet dsGetUser = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        {
                            objParams[0] = new SqlParameter("@P_UA_NAME", userid);
                        };
                        dsGetUser = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RECENT_USER_ACTIVITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsGetUser;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetAllLogFile-> " + ex.ToString());
                    }
                    return dsGetUser;
                }

                //ADDED TODOLIST BY Deepali ON 24082020
                public static DataSet SearchUserToDoList(string userid)
                {
                    DataSet dsGetUser = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        {
                            objParams[0] = new SqlParameter("@P_UA_NO", userid);
                        };
                        dsGetUser = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_TODOLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsGetUser;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.GetAllLogFile-> " + ex.ToString());
                    }
                    return dsGetUser;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS