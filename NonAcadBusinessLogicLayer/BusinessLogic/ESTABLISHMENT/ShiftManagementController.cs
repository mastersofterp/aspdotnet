
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SHIFT MGT.                                
// CREATION DATE : 05/10/2016
// CREATED BY    : SURAJ CHOUDHARI
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

            public class ShiftManagementController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                /// <summary>
                /// Added By:Swati Ghate
                /// Purpose: To get double duty assigned employee for approval
                /// </summary>
                /// <param name="objSM"></param>
                /// <returns></returns>
                #region DoubleDutyApproval
                public DataSet GetEmployeeListforDoubleDutyApproval(ShiftManagement objSM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDATE", objSM.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TODATE", objSM.TODATE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objSM.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_STNO", objSM.STNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_GET_DOUBLE_DUTY_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.GetEmployeeListforDoubleDutyApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddUpdateDoubleDutyApproval(DataTable DtDoubleDutyRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@ESTB_SHIFT_DOUBLE_DUTY_RECORD", DtDoubleDutyRecord);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INSERT_DOUBLE_DUTY_APPROVAL", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftMgtController.AddUpdateDoubleDutyApproval->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion
                #region BiometricDataDownload
                /// <summary>
                /// Added By :Swati GHate
                /// Purpose:To download Biometric SHift mgt. data
                /// </summary>
                /// <param name="objSM"></param>
                /// <returns></returns>
                public int DownloadBiometricData(ShiftManagement objSM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", objSM.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TODATE", objSM.TODATE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_BIOATTENDANCE_RECORD_TRANSFER_SHIFT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion
                #region Shift_Master
                /// <summary>
                /// to insert update data in Payroll_ShiftMaster Table
                /// </summary>
                /// <param name="SHIFTNO"></param>
                /// <param name="SHIFTNAME"></param>
                /// <param name="INTIME"></param>
                /// <param name="OUTTIME"></param>
                /// <returns></returns>
                public int InsertUpdateShiftMaster(ShiftManagement objSM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SHIFTNO", objSM.SHIFTNO);
                        objParams[1] = new SqlParameter("@P_SHIFTNAME", objSM.SHIFTNAME);
                        objParams[2] = new SqlParameter("@P_INTIME", objSM.INTIME);
                        objParams[3] = new SqlParameter("@P_OUTTIME", objSM.OUTTIME);

                        objParams[4] = new SqlParameter("@P_INTIME_MID", objSM.INTIME_MID);
                        objParams[5] = new SqlParameter("@P_OUTTIME_MID", objSM.OUTTIME_MID);
                        objParams[6] = new SqlParameter("@P_IsNightShift", objSM.IsNightShift);
                        objParams[7] = new SqlParameter("@P_IsAllowCompOffLeave", objSM.IsAllowCompOffLeave);
                        objParams[8] = new SqlParameter("@P_IsDoubleDuty", objSM.IsDoubleDuty);
                        objParams[9] = new SqlParameter("@P_College_no", objSM.COLLEGE_NO);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SHIFT_INSERT_UPDATE", objParams, false) != null)
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_INSERT_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.InsertUpdateShiftMaster-> " + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// To get Shift Master details
                /// </summary>
                /// <returns></returns>
                public DataSet GetShiftMasterDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SHIFT_MASTER", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_GET_SHIFT_MASTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.GetShiftMasterDetails->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion
                #region Shift_Mgt
                // TO GET EMPLOYEES
                public DataSet GetEmployeesForShiftManagement(ShiftManagement objSME)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDT", objSME.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TODT", objSME.TODATE);
                        objParams[2] = new SqlParameter("@P_INCHARGEEMPLOYEEIDNO", objSME.INCHARGEEMPLOYEEIDNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objSME.COLLEGE_NO);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SHIFT_MANAGEMENT_EMPLOYEES", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_GET_SHIFT_MANAGEMENT_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.GetEmployeesForShiftManagement-> " + ex.ToString());
                    }
                    return ds;
                }


                // TO GET EMPLOYEES
                public DataSet GetWeekDates(ShiftManagement objSME)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDT", objSME.FROMDATE);
                        objParams[1] = new SqlParameter("@P_TODT", objSME.TODATE);
                        objParams[2] = new SqlParameter("@P_INCHARGEEMPLOYEEIDNO", objSME.INCHARGEEMPLOYEEIDNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objSME.COLLEGE_NO);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SHIFT_MANAGEMENT_WEEK_DATES", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SHIFT_GET_SHIFT_MANAGEMENT_WEEK_DATES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ShiftManagementController.GetEmployeesForShiftManagement-> " + ex.ToString());
                    }
                    return ds;
                }


                public int InsertShiftManagementDetails(DataTable dt, ShiftManagement objSME)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@PayTableType", dt);
                        objParams[1] = new SqlParameter("@P_INCHARGEIDNO", objSME.INCHARGEEMPLOYEEIDNO);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", objSME.COLLEGE_NO);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SHIFT_MANAGEMENT_INSERT_UPDATE", objParams, false) != null)
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SHIFT_MANAGEMENT_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.ShiftManamgementController.InsertShiftManagementDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region  Event log

                public int InsertEventlog(String PageName, String EventName, String UserLogin, int Userid, string IpAddress, string MacAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@PageName", PageName);
                        objParams[1] = new SqlParameter("@EventName", EventName);
                        objParams[2] = new SqlParameter("@UserLogin", UserLogin);
                        objParams[3] = new SqlParameter("@UserId", Userid);
                        objParams[4] = new SqlParameter("@IPAddress", IpAddress);
                        objParams[5] = new SqlParameter("@MACAddress", MacAddress);


                        objSQLHelper.ExecuteNonQuerySP("SP_EVENTLOG_ADD", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateByDate -> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }


                #endregion

            }
        }
    }
}
