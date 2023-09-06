using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Text;
using IITMS.NITPRM;



namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This AssignmentController is used to control ASSIGNMASTER table.
            /// </summary>
            public partial class IForumMasterController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public  DataSet GetAllForumByUaNo(int sessionno, int courseno, int uano)
                {
                    DataSet ds = null;
                    SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                    objParams[2] = new SqlParameter("@P_UA_NO", uano);

                    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_FORUMS", objParams);
                    return ds;
                }
                public DataSet GetAllForumByCourseNo(int sessionno, int courseno)
                {
                    DataSet ds = null;
                    SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_FORUM_BYCOURSE_NO", objParams);
                    return ds;
                }
                public DataSet GetAllMessageByForum_No(int FORUM_NO)
                {
                    DataSet ds = null;
                    SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_FORUM_NO", FORUM_NO);
                    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_MESSAGES_BY_FORUM_NO", objParams);
                    return ds;
                }
                public DataTableReader GetSingleForum(int FORUM_NO)
               {
                   DataTableReader dtr = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("P_FORUM_NO", FORUM_NO);
                       dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_FORUM_BYFORUM_NO", objParams).Tables[0].CreateDataReader();
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.GetSingleForum-> " + ex.ToString());
                   }
                   return dtr;
               }
                public int AddForum(IForumMaster objAssign)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New FORUM
                            objParams = new SqlParameter[7];
                            objParams[0] = new SqlParameter("@P_SESSIONNO", objAssign.SESSIONNO);
                            objParams[1] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objAssign.COURSENO);
                            objParams[3] = new SqlParameter("@P_FORUM", objAssign.FORUM );
                            objParams[4] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[5] = new SqlParameter("@P_CREATEDDATE", objAssign.CREATEDDATE);
                            objParams[6] = new SqlParameter("@P_FORUM_NO", SqlDbType.Int);
                            objParams[6].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FORUM", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.AddForum-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateForum(IForumMaster  objAssign)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[5];
                            objParams[0] = new SqlParameter("@P_FORUM_NO", objAssign.FORUM_NO);
                            objParams[1] = new SqlParameter("@P_FORUM", objAssign.FORUM);
                            objParams[2] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[3] = new SqlParameter("@P_CREATEDDATE", objAssign.CREATEDDATE);
                            objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_FORUM", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.UpdateForum-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteForum(int forum_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FORUM_NO", forum_no);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_FORUM", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.DeleteForum -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetAllForumPostByForumNo(int forum_no)  ///For Student
                {
                    DataSet ds = null;
                    SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_FORUM_NO", forum_no);

                    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_THREADS_BY_FORUM_NO", objParams);
                    return ds;
                }
                public DataSet GetAllThreadByForumNoForFaculty(int forum_no)  ///For Faculty
                {
                    DataSet ds = null;
                    SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_FORUM_NO", forum_no);

                    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_THREADS_BY_FORUM_NO_FOR_FACULTY", objParams);
                    return ds;
                }
                public DataTableReader GetSingleThread(int THREAD_NO)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_THREAD_NO", THREAD_NO);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_THREAD_BYTHREAD_NO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.GetSingleThread-> " + ex.ToString());
                    }
                    return dtr;
                }
                public int AddThread(IForumMaster objForum)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New FORUM
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_FORUM_NO", objForum.FORUM_NO );
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objForum.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objForum.COURSENO);
                            objParams[3] = new SqlParameter("@P_UA_NO", objForum.UA_NO);
                            objParams[4] = new SqlParameter("@P_THREAD", objForum.THREAD);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objForum.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_CREATEDDATE", objForum.CREATEDDATE);
                            objParams[7] = new SqlParameter("@P_THREAD_NO", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FORUMTHREAD", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.AddThread-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateThread(IForumMaster objFM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[5];
                            objParams[0] = new SqlParameter("@P_THREAD_NO", objFM.THREAD_NO);
                            objParams[1] = new SqlParameter("@P_THREAD", objFM.THREAD);
                            objParams[2] = new SqlParameter("@P_DESCRIPTION", objFM.DESCRIPTION);
                            objParams[3] = new SqlParameter("@P_CREATEDDATE", objFM.CREATEDDATE);
                            objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_THREAD", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.UpdateThread-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteThread(int thread_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_THREAD_NO", thread_no);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_THREAD", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.DeleteThread -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int AddMessage(IForumMaster objForum)
                {
                    
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New MESSAGE
                            objParams = new SqlParameter[6];
                            objParams[0] = new SqlParameter("@P_FORUM_NO", objForum.FORUM_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objForum.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_UA_NO", objForum.UA_NO);
                            objParams[3] = new SqlParameter("@P_MESSAGE", objForum.MESSAGE);
                            objParams[4] = new SqlParameter("@P_POSTDATE", objForum.CREATEDDATE);
                            objParams[5] = new SqlParameter("@P_MESSAGE_NO", SqlDbType.Int);
                            objParams[5].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FORUMMESSAGE", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IForumMasterController.AddMessage-> " + ex.ToString());
                    }

                    return retStatus;
                 }



                public string NewForumNotification(string username, int idno)
                {
                    string retNews = string.Empty;

                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        //dr = objSqlHelper.ExecuteReaderSP("PKG_NEWS_SP_ACTIVE_NEWS", objParams);

                        objParams[0] = new SqlParameter("@P_USERNAME", username);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        dr = objSqlHelper.ExecuteReaderSP("PKG_ITLE_SP_RET_FORUM_NOTIFICATIONS", objParams);

                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            DataTable dt = new DataTable();
                            while (dr.Read())
                            {
                                //str.Append(dr["TITLE"].ToString() + "<br>");
                               

                                //str.Append("<br>");
                                str.Append(" New Forum Topic <b> \"");
                                str.Append(dr["Title"].ToString());
                                str.Append("\" </b> posted in ");
                                str.Append(dr["COURSE_NAME"].ToString());
                                str.Append("<br>");
                                str.Append("<br>");
                                

                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            retNews = newshtml.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }




            }

      }
    }
}