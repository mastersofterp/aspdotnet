//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 24-APR-2017   
//MODIFY BY     : 
//MODIFIED DATE :    
//================================================================================================   
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
            public class PassingAuthorityController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                # region PASSING_AUTHORITY

                //To Add Update Passing Authority
                public int AddUpdatePassAuthority(PassignAuthorityEnt objPAEnt)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PANO", objPAEnt.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objPAEnt.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objPAEnt.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objPAEnt.COLLEGE_NO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objPAEnt.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PASSING_AUTHORITY_IU", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassingAuthorityController.AddUpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
             
                //To Delete existing Passing Authority
                public int DeletePassingAuthority(int PANo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_APPROVAL_AUTHORITY_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassingAuthorityController.DeletePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all existing passing authority details
                public DataSet GetAllPassAuthority(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_PASSING_AUTHORITY_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassingAuthorityController.GetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing Passing Authority details by passing Passing Authority No.
                public DataSet GetSingPassAuthority(int PANo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_PASSING_AUTHORITY_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassingAuthorityController.GetSingPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion


                #region Approval Authority Path

                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_ALL_APPROVAL_AUTHORITY_PATH", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassignAuthority.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddPAPath(PassignAuthorityEnt objPAEnt)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_PAPNO", objPAEnt.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objPAEnt.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objPAEnt.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objPAEnt.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objPAEnt.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objPAEnt.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objPAEnt.PAPATH);                                  
                        objparams[7] = new SqlParameter("@P_COLLEGE_NO", objPAEnt.COLLEGE_NO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objPAEnt.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_EVENTID", objPAEnt.EVENTNO);
                        objparams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_APPROVAL_AUTHORITY_PATH_IU", objparams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 2)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);                      
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassignAuthority.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }


                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_APPROVAL_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }            


                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_APPROVAL_AUTHORITY_PATH_BY_ID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PassingAuthorityController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion


                #region Event Approval

                // get list of events for approval

                public DataSet GetEventsPendingList(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_EVENT_LIST_FOR_APPROVE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEventsPendingList->" + ex.ToString());
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
