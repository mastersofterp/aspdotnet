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
            /// This Access_LinkController is used to control Access_Link table.
            /// </summary>
            public class Access_LinkController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// AddAccessLink method is used to add new access link.
                /// </summary>
                /// <param name="objAL">objAL is the object of Access_Link class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                /// 

                //Commented by Mr k.Sandeep on 18-11-2017 

                //public int AddAccessLink(Access_Link objAL)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Access Link
                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_AL_LINK", objAL.Al_Link);
                //        if (objAL.Al_Url != string.Empty)
                //            objParams[1] = new SqlParameter("@P_AL_URL", objAL.Al_Url);
                //        else
                //        objParams[1] = new SqlParameter("@P_AL_URL", DBNull.Value);
                //        objParams[2] = new SqlParameter("@P_AL_ASNO", objAL.Al_AsNo);
                //        objParams[3] = new SqlParameter("@P_SRNO", objAL.SrNo);
                //        objParams[4] = new SqlParameter("@P_MASTNO", objAL.MastNo);
                //        objParams[5] = new SqlParameter("@P_LEVELNO", objAL.levelno);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_INS_LINK", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.AddAccessLink -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// UpdateAccessLink method is used to update existing access link.
                /// </summary>
                /// <param name="objAL">objAL is the object of Access_Link class</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>
                /// 

                //Commented by Mr k.Sandeep on 18-11-2017 

                //public int UpdateAccessLink(Access_Link objAL)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //UpdateFaculty Access Link
                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_AL_NO", objAL.Al_No);
                //        objParams[1] = new SqlParameter("@P_AL_LINK", objAL.Al_Link);
                //        objParams[2] = new SqlParameter("@P_AL_URL", objAL.Al_Url);
                //        objParams[3] = new SqlParameter("@P_AL_ASNO", objAL.Al_AsNo);
                //        objParams[4] = new SqlParameter("@P_SRNO", objAL.SrNo);
                //        objParams[5] = new SqlParameter("@P_MASTNO", objAL.MastNo);
                //        objParams[6] = new SqlParameter("@P_LEVELNO", objAL.levelno);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_UPD_LINK", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.UpdateAccessLink-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                ///// <summary>
                ///// DeleteAccessLink method is used to delete existing access link of parameter al_no.
                ///// </summary>
                ///// <param name="al_no">Delete access_link as per this al_no</param>
                ///// <returns>Integer CustomStatus - Record Deleted or Error</returns>
                //public int DeleteAccessLink(int al_no)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("P_AL_NO", OracleDbType.Int32, al_no, System.Data.ParameterDirection.Input);

                //        objSQLHelper.ExecuteNonQuerySP("Pkg_Access_Link.SP_DEL_LINK", objParams, false);
                //        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.DeleteAccessLink-> " + ex.ToString());
                //    }

                //    return Convert.ToInt32(retStatus);
                //}

                /// <summary>
                /// GetAllLinks method is used to retrieve all access links.
                /// </summary>
                /// <returns>Return Dataset</returns>
                public DataSet GetAllLinks()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];                        

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACCESS_LINK_SP_ALL_LINK", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetAllLinks-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// GetSingleRecord method is used to retrieve single access link.
                /// </summary>
                /// <param name="al_no">Get single record as per this al_no</param>
                /// <returns>OracleDataReader</returns>
                public SqlDataReader GetSingleRecord(int al_no)
                {
                    SqlDataReader dr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AL_NO", al_no);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACCESS_LINK_SP_RET_LINK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetSingleRecord -> " + ex.ToString());
                    }
                    return dr;

                }

                //added by Mr k.Sandeep on 18-11-2017 
                public int AddAccessLink(Access_Link objAL)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Access Link
                        objParams = new SqlParameter[8]; //Added By Hemanth G
                        objParams[0] = new SqlParameter("@P_AL_LINK", objAL.Al_Link);
                        if (objAL.Al_Url != string.Empty)
                            objParams[1] = new SqlParameter("@P_AL_URL", objAL.Al_Url);
                        else
                            objParams[1] = new SqlParameter("@P_AL_URL", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_AL_ASNO", objAL.Al_AsNo);
                        objParams[3] = new SqlParameter("@P_SRNO", objAL.SrNo);
                        objParams[4] = new SqlParameter("@P_MASTNO", objAL.MastNo);
                        objParams[5] = new SqlParameter("@P_LEVELNO", objAL.levelno);
                        objParams[6] = new SqlParameter("@P_STATUS", objAL.chklinkstatus);//Added By Hemanth G
                        objParams[7] = new SqlParameter("@P_TRANS", objAL.chkTrans);//Added By Nikhil l. on 24/04/2023

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_INS_LINK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.AddAccessLink -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateAccessLink(Access_Link objAL)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Access Link
                        objParams = new SqlParameter[9]; //Added By Hemanth G
                        objParams[0] = new SqlParameter("@P_AL_NO", objAL.Al_No);
                        objParams[1] = new SqlParameter("@P_AL_LINK", objAL.Al_Link);
                        objParams[2] = new SqlParameter("@P_AL_URL", objAL.Al_Url);
                        objParams[3] = new SqlParameter("@P_AL_ASNO", objAL.Al_AsNo);
                        objParams[4] = new SqlParameter("@P_SRNO", objAL.SrNo);
                        objParams[5] = new SqlParameter("@P_MASTNO", objAL.MastNo);
                        objParams[6] = new SqlParameter("@P_LEVELNO", objAL.levelno);
                        objParams[7] = new SqlParameter("@P_STATUS", objAL.chklinkstatus);//Added By Hemanth G
                        objParams[8] = new SqlParameter("@P_TRANS", objAL.chkTrans);//Added By Nikhil L. on 24/04/2023
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_UPD_LINK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.UpdateAccessLink-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllLinksForSearch(string search, int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SEARCH_SP_RET_PAGE_LINK", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetAllLinks-> " + ex.ToString());
                    }
                    return ds;
                }


                #region Quick Links
                // added by Vaishali for Quick links
                public DataSet GetUserAccAssignLinks(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_ACC_ASIGNLINKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetUserAccAssignLinks-> " + ex.ToString());
                    }
                    return ds;
                }

                //public int AddUserQLinks(int ua_no, string QL_AL_NO)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[]
                //        {
                //            new SqlParameter("@P_UA_NO", ua_no),
                //            new SqlParameter("@P_QL_AL_NO",QL_AL_NO),
                //            new SqlParameter("@P_OUT",SqlDbType.Int)
                //        };
                //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_INS_USER_QLINKS", objParams, false) != null)

                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.AddUserQLinks -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                /// <summary>
                /// uPDATED BY ABHISHEK SHIRPURKAR
                /// Date : 06092019
                /// </summary>
                /// <param name="ua_no"></param>
                /// <param name="QL_AL_NO"></param>
                /// <param name="QL_AL_NAME"></param>
                /// <returns></returns>
                public int AddUserQLinks(int ua_no, string QL_AL_NO, string QL_AL_NAME)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_QL_AL_NO",QL_AL_NO),
                            new SqlParameter("@P_QL_AL_NAME",QL_AL_NAME),
                            new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_INS_USER_QLINKS", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.AddUserQLinks -> " + ex.ToString());
                    }
                    return retStatus;
                }



                //public DataSet GetUserQLinks(int ua_no)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_QLINKS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetUserQLinks ->" + ex.ToString());
                //    }
                //    return ds;
                //}


                public DataSet GetUserQLinks(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_QLINKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.GetUserQLinks ->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                // ADDED BY PRANITA ON 26/10/2021 FOR SHORTCUT KEY FOR LINKS
                public DataSet GetDomaindata(int AL_ASNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] Sqlparam = new SqlParameter[1];
                        Sqlparam[0] = new SqlParameter("@P_AL_ASNO", AL_ASNO);
                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_CONFIG_DOMAIN_GETDATA", Sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Access_LinkController.GetDomaindata() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public int UpdateAccessLinkShortcutKey(Access_Link objAL)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_AL_NO", objAL.Al_No);
                        objParams[1] = new SqlParameter("@P_AL_LINK", objAL.Al_Link);
                        objParams[2] = new SqlParameter("@P_AL_URL", objAL.Al_Url);
                        objParams[3] = new SqlParameter("@P_SHORTCUT_KEY", objAL.Shortcut_key);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_UPD_LINK_SHORTCUT_KEY", objParams, true);
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_ACCESS_LINK_SP_UPD_LINK_SHORTCUT_KEY", objParams, false) != null)
                        //retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Access_LinkController.UpdateAccessLinkShortcutKey-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // ENDED BY PRANITA ON 26/10/2021 FOR SHORTCUT KEY FOR LINKS

            }


        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS