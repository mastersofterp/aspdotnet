
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ShifInCharge CONTROLLER]                                  
// CREATION DATE : 
// CREATED BY    : 
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System.Data.SqlClient;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ShiftInchargeController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetInchargeList(ShiftInchargeEntity objSME)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", objSME.COLLEGE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_INCHARGE_FILL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.GetInchargeList-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetInchargeEmployees(ShiftInchargeEntity objLIE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);
                        objParams[1] = new SqlParameter("@P_STNO", objLIE.STNO);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objLIE.DEPTNO);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_INCHARGEMASTER_GET_EMPLOYEES", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_INCHARGEMASTER_GET_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveInchargeController.GetInchargeEmployees-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// to insert update data in Payroll_ShiftMaster Table
                /// </summary>
                /// <param name="SHIFTNO"></param>
                /// <param name="SHIFTNAME"></param>
                /// <param name="INTIME"></param>
                /// <param name="OUTTIME"></param>
                /// <returns></returns>
                public int InsertUpdateInchargeMaster(DataTable table, ShiftInchargeEntity objLIE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@PayTableType", table);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_WARD_NO", objLIE.WARD_NO);
                        objParams[3] = new SqlParameter("@P_Created_By", objLIE.Created_By);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INCHARGEMASTER_INSERT_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.InsertUpdateShiftMaster-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int RemoveUpdateInchargeMaster(DataTable table, ShiftInchargeEntity objLIE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@PayTableType", table);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_WARD_NO", objLIE.WARD_NO);
                        objParams[3] = new SqlParameter("@P_Created_By", objLIE.Created_By);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INCHARGEMASTER_INSERT_REMOVE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.InsertUpdateShiftMaster-> " + ex.ToString());
                    }
                    return retstatus;
                }


                #region Incharge Detail Master

                public DataSet GetInchargeDetailsEmployees(ShiftInchargeEntity objLIE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_INCHARGEEMPLOYEEIDNO", objLIE.INCHARGEEMPLOYEEIDNO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_INCHARGEMASTER_DETAIL_GET_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveInchargeController.GetInchargeDetailsEmployees-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// to insert update data in Payroll_InchargeMaster_Details Table
                /// </summary>
                /// <param name="table"></param>
                /// <param name="INCHARGEEMPLOYEEIDNO"></param>
                /// <param name="COLLEGE_NO"></param>
                /// <returns></returns>
                public int InsertUpdateInchargeDetailMaster(DataTable table, ShiftInchargeEntity objLIE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@ESTB_InchargeDetailMasterTbl", table);
                        objParams[1] = new SqlParameter("@P_INCHARGEEMPLOYEEIDNO", objLIE.INCHARGEEMPLOYEEIDNO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INCHARGEMASTER_DETAILS_INSERT_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.InsertUpdateInchargeDetailMaster-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int TempPermanentRemoveInchargeDetails(DataTable table, ShiftInchargeEntity objLIE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@ESTB_InchargeDetailMasterTbl", table);
                        objParams[1] = new SqlParameter("@P_INCHARGEEMPLOYEEIDNO", objLIE.INCHARGEEMPLOYEEIDNO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLIE.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_IsTempRemove", objLIE.IsTempRemove);
                        objParams[4] = new SqlParameter("@P_IsPermanentRemove", objLIE.IsPermanentRemove);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INCHARGEMASTER_DETAILS_INSERT_UPDATE_MEMBER_REMOVE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.InsertUpdateInchargeDetailMaster-> " + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion
            }
        }
    }
}
