//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ATTENDANCE CONTROLLER]                                  
// CREATION DATE : 23-JULY-2018                                                        
// CREATED BY    : ROHIT MASKE                                     
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
            public class AdvancePassingAuthorityController
            {
                /// <SUMMARY>
                /// ConnectionStrings 
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                # region ADVANCE_PASSING_AUTHORITY
                // To insert New ADVANCE_PASSING_AUTHORITY
                public int AddPassAuthority(AdvancePassingAuthority objAdvPassAuth)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PANAME", objAdvPassAuth.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objAdvPassAuth.UA_NO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objAdvPassAuth.COLLEGE_NO);
                     //   objParams[3] = new SqlParameter("@P_PNO", SqlDbType.Int);
                       // objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADVANCE_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To modify existing ADVANCE_PASSING_AUTHORITY
                public int UpdatePassAuthority(AdvancePassingAuthority objAdvPassAuth)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PNO", objAdvPassAuth.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objAdvPassAuth.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objAdvPassAuth.UA_NO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objAdvPassAuth.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADVANCE_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To Delete existing  ADVANCE Passing Authority
                public int DeletePassAuthority(int PANo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PNO", PANo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADVANCE_PASSING_AUTHORITY_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.DeletePassAuthority->" + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADVANCE_PASSING_AUTHORITY_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.RetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                // To fetch existing  ADVANCE Passing Authority details by passing Passing Authority No.
                public DataSet GetSingPassAuthority(int PANo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PNO", PANo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_ADVANCE_PASSING_AUTHORITY_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.RetSingPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion


                # region PASSING_AUTHORITY_PATH
                // To insert New PASSING_AUTHORITY_PATH
                public int AddPAPath(AdvancePassingAuthority objAPA, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_PAN01", objAPA.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objAPA.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objAPA.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objAPA.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objAPA.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objAPA.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objAPA.DEPTNO);
                        objparams[7] = new SqlParameter("@P_COLLEGE_NO", objAPA.COLLEGE_NO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objAPA.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_PAYROLL_EMP_RECORD", dtEmpRecord);
                        objparams[10] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                //Update Advance Passing Authority Path
                public int UpdatePAPath(AdvancePassingAuthority objAPA, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_PAPNO", objAPA.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objAPA.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objAPA.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objAPA.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objAPA.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objAPA.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objAPA.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objAPA.DEPTNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objAPA.COLLEGE_NO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objAPA.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_PAYROLL_EMP_RECORD", dtEmpRecord);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                //Get Specific Path Advance Apply Process
                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //Get Specific Path Advance Apply Process
                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                # region ADVANCE_APPLY_PROCESS
                // Insert  Advance Apply Process for this PAYROLL_ADVANCE_APP_ENTRY
                public int AdvAppPro(AdvancePassingAuthority objAPA,int IDNo)
                {
                    int retstatus = 0;
                    try
                    {                        
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNo);
                      
                        objparams[1] = new SqlParameter("@P_DEPTNO", objAPA.DEPTNO);
                        objparams[2] = new SqlParameter("@P_STAFFNO", objAPA.STAFFNO);
                        objparams[3] = new SqlParameter("@P_COLLEGE_NO", objAPA.COLLEGE_NO);
                        objparams[4] = new SqlParameter("@P_ADVCANEDATE", objAPA.ADVCANEDATE);
                        objparams[5] = new SqlParameter("@P_ADVANCEAMOUNT", objAPA.ADVANCEAMOUNT);
                        objparams[6] = new SqlParameter("@P_REASON", objAPA.REASON);
                        objparams[7] = new SqlParameter("@P_ADVCANSTATUS", "P");
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objAPA.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_PAPNO", objAPA.PANO);
                        objparams[10] = new SqlParameter("@P_APPLYDATE", objAPA.APPLYDATE);
                        objparams[11] = new SqlParameter("@P_OUT",SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        retstatus=Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_ADVANCE_APP_ENTRY_INSERT1", objparams, false));
                        if (retstatus == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_ADVANCE_APP_ENTRY_INSERT1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To Fetch all existing Process Details for table PAYROLL_ADVANCE_APP_ENTRY 
                public DataSet GetAdvanceAppProcess(int emp_no,int dept_no,int college_no,int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_STAFFNO", emp_no);
                        objparams[1] = new SqlParameter("@P_DEPTNO", dept_no);
                        objparams[2] = new SqlParameter("@P_COLLEGE_NO",college_no);
                        objparams[3] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APP_ENTRY_GET_ALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdvancePassingAuthorityController.GetLeaveApplStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                #endregion

                #region Advance Apply Process Approval
                //Fetch Pending List of Advance application for approval to passing Authority
                public DataSet GetPendListforAdvanceApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APPLY_PROCESS_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            
                public DataSet GetAdvanceApplDetail(int ANO)
                //fetch  Advance Application details of Particular advance aaplication by Passing ANO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ANO", ANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APPLY_PROCESS_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateAppPassEntry(AdvancePassingAuthority objAPAuth)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_ANO", objAPAuth.ANO);
                        objparams[1] = new SqlParameter("@P_PANO", objAPAuth.PANO);
                        objparams[2] = new SqlParameter("@P_ADVCANAPRROVALAMOUNT", objAPAuth.ADVANCEAMOUNT);                      
                        objparams[3] = new SqlParameter("@P_APPROVEREJECTDATE", objAPAuth.APPROVEREJECTDATE);
                        objparams[4] = new SqlParameter("@P_APPROVEREJECTREMARK", objAPAuth.APPROVEREJECTREMARK);
                        objparams[5] = new SqlParameter("@P_ADVCANAPRROVALSTATUS", objAPAuth.ADVCANSTATUS); 
                        objparams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_ADVANCE_APP_PASSING_PATH_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                       
               /// <summary>
                /// created by Rohit Maske 18-07-2018 
                /// </summary>
                /// <param name="UA_No"></param>
                /// <returns></returns>
                /// 
                public DataSet GetPendListforAdvanceApprovalStatusALL(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APPLY_PROCESS_GET_LIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusALL->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //DATE WAISE GET DATA 
                public DataSet GetPendListforLVApprovalStatusParticular(int UA_No,string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APPLY_PROCESS_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
               
                #endregion


                public DataSet GetPendListforLVApprovalPendingStatusParticular(int UA_No, string frmdt, string todt,string status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STATUS", status);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ADVANCE_APPLY_PROCESS_APPROVAL_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
