//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [EMP APPRAISAL]                                  
// CREATION DATE : 10-06-2021                                                        
// CREATED BY    : TANU BALGOTE                                       
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
            public class AppraisalPassingAuthorityCon
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                # region PASSING_AUTHORITY
                // To insert New PASSING_AUTHORITY
                public int AddPassAuthority(AppraisalPassingAuthorityEnt objEmpAuthority)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PANAME", objEmpAuthority.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objEmpAuthority.UANO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objEmpAuthority.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objEmpAuthority.COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_AUTHORITY", objEmpAuthority.COMMON_AUTHORITY);
                        objParams[5] = new SqlParameter("@P_PANO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To update New PASSING_AUTHORITY
                public int UpdatePassAuthority(AppraisalPassingAuthorityEnt objEmpAuthority)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PANO", objEmpAuthority.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objEmpAuthority.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objEmpAuthority.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objEmpAuthority.COLLEGE_NO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objEmpAuthority.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_AUTHORITY", objEmpAuthority.COMMON_AUTHORITY);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.RetSingPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To Delete PASSING_AUTHORITY
                public int DeletePassAuthority(int PANo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic. EmpPassAuthorityController.DeletePassAuthority->" + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region PASSING AUTHORITY PATH
                // To insert and Update New PASSING_AUTHORITY PATH
                public int AddPAPath(AppraisalPassingAuthorityEnt EmpAuthority, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[17];
                        objparams[0] = new SqlParameter("@P_PAN01", EmpAuthority.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", EmpAuthority.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", EmpAuthority.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", EmpAuthority.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", EmpAuthority.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", EmpAuthority.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", EmpAuthority.DEPTNO);
                        objparams[7] = new SqlParameter("@P_COLLEGE_NO", EmpAuthority.COLLEGE_NO);
                        objparams[8] = new SqlParameter("@P_STNO", EmpAuthority.STNO);
                        objparams[9] = new SqlParameter("@P_APPRISAL_EMP_RECORD", dtEmpRecord);
                        objparams[10] = new SqlParameter("@P_PAPNO", EmpAuthority.PAPNO);
                        objparams[11] = new SqlParameter("@P_ATID", EmpAuthority.ATID);
                        objparams[12] = new SqlParameter("@P_COLLEGE_CODE", EmpAuthority.COLLEGE_CODE);
                        objparams[13] = new SqlParameter("@P_SESSION_NO", EmpAuthority.SESSION_NO);
                        objparams[14] = new SqlParameter("@P_CREATEDBY", EmpAuthority.CREATEDBY);
                        objparams[15] = new SqlParameter("@P_MODIFIEDBY", EmpAuthority.MODIFIEDBY);
                        objparams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[16].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_PATH_IU", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }



                // To Get All APPRAISAL PASSING_AUTHORITY PATH
                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_ALL_PASSING_AUTHORITY_PATH", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To Get APPRAISAL PASSING_AUTHORITY PATH By ID
                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_PATH_BY_ID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpPassAuthorityController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To Delete PASSING_AUTHORITY PATH
                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IqacPassingAuthorityController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion 

          


                #region Published Journal Details

                //public DataSet GetPublishedJournalDetails(int EMPLOYEE_ID, int SESSION_ID)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_EMPLOYEE_ID", EMPLOYEE_ID);
                //        //objParams[1] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", APPRAISAL_EMPLOYEE_ID);
                //        objParams[1] = new SqlParameter("@P_SESSION_ID", SESSION_ID);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_IQAC_GET_PHD_RESARCH_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IQACController.GetPhdResarchDetails-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                
                #endregion

                #region Research Guidance 

                public DataSet GetResearchGuidanceDetails(int EMPLOYEE_ID, int SESSION_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPLOYEE_ID", EMPLOYEE_ID);
                        //objParams[1] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_SESSION_ID", SESSION_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_IQAC_GET_PHD_RESARCH_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IQACController.GetPhdResarchDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }
    }
}