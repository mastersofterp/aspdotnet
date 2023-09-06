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
            public class QuotaController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region QuotaMaster
              
                // To insert New Quota Entry

                public int AddUpdateQuota(QuotaMaster objQuota)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_QUOTA_NO", objQuota.QUOTA_NO);                       
                        objParams[1] = new SqlParameter("@P_POST_NO", objQuota.POST_NO);
                        objParams[2] = new SqlParameter("@P_QUOTA_RULE", objQuota.QUOTA_RULE);
                    
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objQuota.COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                    
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_QUOTA_INSUPD", objParams, true);
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

                // To Delete Quota
                public int DeleteQuota(int QUOTA_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUOTA_NO", QUOTA_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_QUOTA_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuotaController.DeleteQuota-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Quota
                public DataSet GetAllQuota()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_QUOTA_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuotaController.GetAllQuota-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Quota detail by passing Quota No
                public DataSet GetSingleQuota(int QUOTA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUOTA_NO", QUOTA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_QUOTA_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuotaController.GetSingleQuota->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Quota Master Region
             
            }
        }
    }
}
