//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE MGT.                               
// CREATION DATE : 27-02-2016                                                        
// CREATED BY    : SWATI GHATE
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class PostController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region PostMaster
              
                // To insert New Post Entry

                //public int AddUpdatePost(PostMaster objPM)
                //{
                //    int pkid = 0;

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[11];
                //        objParams[0] = new SqlParameter("@P_POST_NO", objPM.POST_NO);
                //        objParams[1] = new SqlParameter("@P_POST_NAME", objPM.POST_NAME);

                //        objParams[2] = new SqlParameter("@P_STAFFNO", objPM.STAFFNO);
                //        objParams[3] = new SqlParameter("@P_SUBDEPTNO", objPM.SUBDEPTNO);
                //        objParams[4] = new SqlParameter("@P_MODENO", objPM.MODE_NO);                      
                //        objParams[5] = new SqlParameter("@P_ORDERNO", objPM.ORDERNO);
                //        if (!objPM.ORDER_DATE.Equals(DateTime.MinValue))
                //            objParams[6] = new SqlParameter("@P_ORDER_DATE", objPM.ORDER_DATE);
                //        else
                //            objParams[6] = new SqlParameter("@P_ORDER_DATE", DBNull.Value);                       
                //        objParams[7] = new SqlParameter("@P_RESERVATION_QUOTA", objPM.QUOTA);
                //        objParams[8] = new SqlParameter("@P_STATUS", objPM.STATUS);
                //        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[10].Direction = ParameterDirection.Output;

                        
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_INSUPD", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //            {
                //                pkid = -99;
                //            }
                //            else
                //                pkid = Convert.ToInt32(ret.ToString());
                //        }
                //        else
                //        {
                //            pkid = -99;
                           
                //        }

                //    }
                //    catch (Exception ee)
                //    {
                //        pkid = -99;
                //    }
                //    return pkid;
                //}

                //public int AddUpdatePost(PostMaster objPM)
                //{
                //    int pkid = 0;

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[12];
                //        objParams[0] = new SqlParameter("@P_POST_NO", objPM.POST_NO);
                //        objParams[1] = new SqlParameter("@P_POST_NAME", objPM.POST_NAME);

                //        objParams[2] = new SqlParameter("@P_STAFFNO", objPM.STAFFNO);
                //        objParams[3] = new SqlParameter("@P_SUBDEPTNO", objPM.SUBDEPTNO);
                //        objParams[4] = new SqlParameter("@P_MODENO", objPM.MODE_NO);
                //        objParams[5] = new SqlParameter("@P_ORDERNO", objPM.ORDERNO);
                //        if (!objPM.ORDER_DATE.Equals(DateTime.MinValue))
                //            objParams[6] = new SqlParameter("@P_ORDER_DATE", objPM.ORDER_DATE);
                //        else
                //            objParams[6] = new SqlParameter("@P_ORDER_DATE", DBNull.Value);
                //        objParams[7] = new SqlParameter("@P_RESERVATION_QUOTA", objPM.QUOTA);
                //        objParams[8] = new SqlParameter("@P_STATUS", objPM.STATUS);
                //        objParams[9] = new SqlParameter("@P_NO_OF_POST", objPM.TOTAL_POST);

                //        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                //        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[11].Direction = ParameterDirection.Output;


                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_INSUPD", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-99"))
                //            {
                //                pkid = -99;
                //            }
                //            else
                //                pkid = Convert.ToInt32(ret.ToString());
                //        }
                //        else
                //        {
                //            pkid = -99;

                //        }

                //    }
                //    catch (Exception ee)
                //    {
                //        pkid = -99;
                //    }
                //    return pkid;
                //}

                public int AddUpdatePost(PostMaster objPM)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_POST_NO", objPM.POST_NO);
                        objParams[1] = new SqlParameter("@P_POST_NAME", objPM.POST_NAME);

                        objParams[2] = new SqlParameter("@P_STAFFNO", objPM.STAFFNO);
                        objParams[3] = new SqlParameter("@P_SUBDEPTNO", objPM.SUBDEPTNO);
                        objParams[4] = new SqlParameter("@P_MODENO", objPM.MODE_NO);
                        objParams[5] = new SqlParameter("@P_ORDERNO", objPM.ORDERNO);
                        if (!objPM.ORDER_DATE.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_ORDER_DATE", objPM.ORDER_DATE);
                        else
                            objParams[6] = new SqlParameter("@P_ORDER_DATE", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_RESERVATION_QUOTA", objPM.QUOTA);
                        objParams[8] = new SqlParameter("@P_STATUS", objPM.STATUS);
                        objParams[9] = new SqlParameter("@P_NO_OF_POST", objPM.TOTAL_POST);
                        objParams[10] = new SqlParameter("@P_PLAN_NO", objPM.PLAN_NO);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_INSUPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                pkid = -99;
                            }
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                        {
                            pkid = -99;

                        }

                    }
                    catch (Exception ee)
                    {
                        pkid = -99;
                    }
                    return pkid;
                }


                // To Delete School
                public int DeletePost(int POST_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_NO", POST_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.DeletePost-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Post
                public DataSet GetAllPost()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.GetAllPost-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Post detail by passing Post No
                public DataSet GetSinglePost(int POST_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_NO", POST_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.GetSinglePost->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Post Master Region
                #region PostCodeMaster

                // To insert New Post CODE Entry

                public int AddUpdatePostCode(PostMaster objPM)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_POST_CODE_NO", objPM.POST_CODE_NO);
                        objParams[1] = new SqlParameter("@P_POST_NO", objPM.POST_NO);
                        objParams[2] = new SqlParameter("@P_POST_CODE_NAME", objPM.POST_CODE);

                        objParams[3] = new SqlParameter("@P_STAFFNO", objPM.STAFFNO);
                        objParams[4] = new SqlParameter("@P_SUBDEPTNO", objPM.SUBDEPTNO);
                      
                        objParams[5] = new SqlParameter("@P_ORDERNO", objPM.ORDERNO);
                      
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_CODE_INSUPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                pkid = -99;
                            }
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                        {
                            pkid = -99;

                        }

                    }
                    catch (Exception ee)
                    {
                        pkid = -99;
                    }
                    return pkid;
                }

                // To Delete School
                public int DeletePostCode(int POST_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_CODE_NO", POST_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_CODE_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.DeletePostCode-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Post
                public DataSet GetAllPostCode()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_CODE_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.GetAllPostCode-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Post detail by passing Post No
                public DataSet GetSinglePostCode(int POST_CODE_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_CODE_NO", POST_CODE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_CODE_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostController.GetSinglePostCode->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Post Master Region
            }
        }
    }
}
