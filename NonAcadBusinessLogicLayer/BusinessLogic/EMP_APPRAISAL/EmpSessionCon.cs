//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [EMP APPRAISAL]                                  
// CREATION DATE : 6-03-2021                                                        
// CREATED BY    : TEJAS JAISWAL                                     
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
            public class EmpSessionCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                #region Session Creation

                public int AddUpdSessionCreation(EmpSessionEnt objESEnt)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSION_NAME", objESEnt.SESSION_NAME);
                        objParams[1] = new SqlParameter("@P_SESSION_SHORTNAME", objESEnt.SESSION_SHORTNAME);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", objESEnt.FROMDATE);
                        objParams[3] = new SqlParameter("@P_TO_DATE", objESEnt.TODATE);
                        objParams[4] = new SqlParameter("@P_ISACTIVE", objESEnt.ISACTIVE);
                        objParams[5] = new SqlParameter("@P_IS_SPECIAL", objESEnt.IS_SPECIAL);
                        objParams[6] = new SqlParameter("@CREATEDBY", objESEnt.CREATEDBY);
                        objParams[7] = new SqlParameter("@MODIFIEDBY", objESEnt.MODIFIEDBY);
                        objParams[8] = new SqlParameter("@P_SESSION_ID", objESEnt.SESSION_ID);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_SESSION_CREATION_IU", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionCon.AddUpdSessionCreation->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleSessionEntry(int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", SessionNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_SESSION_ENTRY_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionCon.GetSingleSessionEntry->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public DataSet GetAllSessionEntry()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_ALL_SESSION_ENTRY", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionCon.GetAllSessionEntry->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public int DeleteSessionEntry(int sessionno)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", sessionno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_DELETE_SESSION_ENTRY", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionCon.DeleteSessionEntry->" + ex.ToString());
                    }
                    return retstatus;

                }

                #endregion

                #region Session Activity
                public int AddUpdSessionActivity(EmpSessionEnt objempSession)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objempSession.SESSION_NO);
                        objParams[1] = new SqlParameter("@P_ATID", objempSession.SESSION_TYPE_NO);
                        objParams[2] = new SqlParameter("@P_STARTDATE", objempSession.STARTDATE);
                        objParams[3] = new SqlParameter("@P_ENDDATE", objempSession.ENDDATE);
                        objParams[4] = new SqlParameter("@P_IS_STARTED", objempSession.IS_STARTED);
                        objParams[5] = new SqlParameter("@CREATEDBY", objempSession.CREATEDBY);
                        objParams[6] = new SqlParameter("@MODIFIEDBY", objempSession.MODIFIEDBY);
                        objParams[7] = new SqlParameter("@P_SESSION_ACTIVITY_NO", objempSession.SESSION_ACTIVITY_NO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO", objempSession.COLLEGE_NO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objempSession.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_SESSION_ACTIVITY_IU", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionController.AddUpdSessionCreation->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleActivitySessionEntry(int SessionNu)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSION_ACTIVITY_NO", SessionNu);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_SESSION_ACTIVITY_ENTRY_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionController.GetSingleSessionEntry->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }


                public DataSet GetAllActivitySessionEntry(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_ALL_SESSION_ACTIVITY_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionController.GetAllSessionEntry->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteSessionActivity(int SESSION_ACTIVITY_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SESSION_ACTIVITY_NO", SESSION_ACTIVITY_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_DELETE_SESSION_ACTIVITY_ENTRY_BY_NO", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic. EmpPassAuthorityController.DeletePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetSingleSessionEntryDetail(EmpSessionEnt objempSession)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objempSession.SESSION_NO);
                        objParams[1] = new SqlParameter("@P_AT_ID", objempSession.SESSION_TYPE_NO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objempSession.COLLEGE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_SESSION_ACTIVITY_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpSessionController.GetSingleSessionEntry->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                #endregion
            }
        }
    }
}
