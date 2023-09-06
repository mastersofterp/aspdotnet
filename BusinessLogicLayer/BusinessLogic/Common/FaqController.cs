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
            /// This FaqController is used to control Faq Table.
            /// </summary>
            public class FaqController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method adds a new Faq in the Faq Table.
                /// </summary>
                /// <param name="objFaq">objFaq is the object of Faq class.</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int AddQue(Faq objFaq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Faq
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_PARENTID", objFaq.PARENTID);
                        objParams[1] = new SqlParameter("@P_IDNO", objFaq.IDNO);
                        objParams[2] = new SqlParameter("@P_TITLE", objFaq.TITLE);
                        objParams[3] = new SqlParameter("@P_FDATE", DateTime.Now);
                        objParams[4] = new SqlParameter("@P_FNAME", objFaq.FNAME);
                        objParams[5] = new SqlParameter("@P_DEL", objFaq.DEL);
                        objParams[6] = new SqlParameter("@P_STATUS", objFaq.STATUS);
                        objParams[7] = new SqlParameter("@P_FID", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_FAQ_SP_INS_FAQ", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.AddQue-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by pooja on date 19-05-2023
                public int Add_Feedback_Reply_Lead(int userno, string feedback_reply, string ipaddress, int categoryno, int status, int queryno, string replied_user, string UserID, int orgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_FEEDBACK_REPLY", feedback_reply);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[3] = new SqlParameter("@P_CATEGORYNO", categoryno);
                        objParams[4] = new SqlParameter("@P_STATUS", status);
                        objParams[5] = new SqlParameter("@P_QUERYNO", queryno);
                        objParams[6] = new SqlParameter("@P_REPLIED_USER", replied_user);
                        objParams[7] = new SqlParameter("@P_USERID", UserID);
                        objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        // objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ONLINE_STUDENT_FEEDBACK_REPLY_LEAD", objParams, true);
                        if (obj != null)
                        {
                            int ret = Convert.ToInt32(obj);
                            if (ret == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }


                /// <summary>
                /// This method adds answer of existing Faq in the Faq table.
                /// </summary>
                /// <param name="objFaq">objFaq is the object of Faq class.</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int AddAns(Faq objFaq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        //Add New Faq
                        objParams[0] = new SqlParameter("@P_PARENTID", objFaq.PARENTID);
                        objParams[1] = new SqlParameter("@P_IDNO", objFaq.IDNO);
                        objParams[2] = new SqlParameter("@P_TITLE", objFaq.TITLE);
                        objParams[3] = new SqlParameter("@P_FDATE", DateTime.Now);
                        objParams[4] = new SqlParameter("@P_FNAME", objFaq.FNAME);
                        objParams[5] = new SqlParameter("@P_DEL", objFaq.DEL);
                        objParams[6] = new SqlParameter("@P_STATUS", objFaq.STATUS);
                        objParams[7] = new SqlParameter("@P_FID", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_FAQ_SP_INS_FAQ", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.AddAns-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to delete existing Faq.
                /// </summary>
                /// <param name="fid">Delete faq as per this fid.</param>
                /// <returns>Integer CustomStatus- Record Deleted or Error</returns>
                public int Delete(int fid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FID", fid);

                        objSQLHelper.ExecuteNonQuerySP("PKG_FAQ_SP_DEL_FAQ", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.GetSingleFaq-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }
                /// <summary>
                /// This method is used to get all answers of current selected faq.
                /// </summary>
                /// <param name="fid">Gets all answers for this selected fid.</param>
                /// <returns>DataSet</returns>
                public DataSet GetAllAnsFaq(int fid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FID", fid);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FAQ_SP_ALL_QA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.LvListBound-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// This method is used to retrieve all faq from Faq table.
                /// </summary>
                /// <returns>DataSet</returns>
                public DataSet GetAllFaq()
                {
                    DataSet dsFaq = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dsFaq = objSQLHelper.ExecuteDataSetSP("PKG_FAQ_SP_ALL_FAQ", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsFaq;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.GetAllFaq-> " + ex.ToString());
                    }
                    return dsFaq;
                }
                /// <summary>
                /// This method is used to retrieve single faq.
                /// </summary>
                /// <param name="fid">Retrieve single faq as per this fid.</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleFaq(int fid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FID", fid);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_FAQ_SP_RET_FAQ", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.GetSingleFaq-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetAllStudent_Feadback(int categoryno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CATEGORYNO", categoryno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ONLINE_STUDENT_FEEDBACK_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController-> " + ex.ToString());
                    }

                    return ds;
                }

                public int Add_Feedback_Reply(int userno, string feedback_reply, int categoryno, int status, int queryno, int replied_user, int QUERY_DET_ID, string UserID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_QUERY_REPLY", feedback_reply);
                        //objParams[3] = new SqlParameter("@P_IPADDRESS", ipaddress);
                         objParams[2] = new SqlParameter("@P_CATEGORYNO", categoryno);
                        objParams[3] = new SqlParameter("@P_STATUS", status);
                        objParams[4] = new SqlParameter("@P_QUERYNO", queryno);
                        objParams[5] = new SqlParameter("@P_REPLIED_USER", replied_user);
                        objParams[6] = new SqlParameter("@P_QUERY_DET_ID", QUERY_DET_ID);
                        objParams[7] = new SqlParameter("@P_USERID", UserID);
                        //objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[7].Direction = ParameterDirection.Output;
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ONLINE_STUDENT_FEEDBACK_REPLY", objParams, true);
                        if (obj != null)
                        {
                            int ret = Convert.ToInt32(obj);
                            if (ret == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }

           
                public int AddFAQ(int qid, string question, string answer, int orgId)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_QID", qid);
                        objParams[1] = new SqlParameter("@P_QUESTION", question);
                        objParams[2] = new SqlParameter("@P_ANSWER", answer);
                        objParams[3] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_FAQ", objParams, true);

                        if (obj != null && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() == "2")
                            status = Convert.ToInt32(2);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineEligibleCriteriaController.AddFAQ() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int GET_QUERY_DET_ID(int Queryno,int userno)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_QUERYNO", Queryno);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        object obj = objSQLHelper.ExecuteScalarSP("GET_QUERY_DET_ID", objParams);

                        if (obj != null)
                        {
                            int ret =  Convert.ToInt32(obj.ToString());
                            if (ret != -99)
                            {
                                retStatus = ret;
                            }
                            else 
                            {
                                retStatus = -99;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                      
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet BindConversationForQueryManagement(int userno, int queryNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_QUERY_NO", queryNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_CONVERSATION_QUERY_MANAGE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FaqController.BindConversationForQueryManagement-> " + ex.ToString());
                    }

                    return ds;
                }



            }



        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS