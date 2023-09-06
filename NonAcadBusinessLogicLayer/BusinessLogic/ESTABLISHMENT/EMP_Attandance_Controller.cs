
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ATTANDANCE CONTROLLER]                                  
// CREATION DATE : 08-APRIL-2011                                                        
// CREATED BY    : KUMAR PREMANKIT                                   
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
using System.DAC;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class EMP_Attandance_Controller
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Attandance
                // To insert New Attandance Type
                public int AddAttandance(EMP_ATTANDANCE objattandance)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_LOGINDEX", objattandance.LOGINDEX);
                        objParams[1] = new SqlParameter("@P_NODEINDEX", objattandance.NODEINDEX);
                        objParams[2] = new SqlParameter("@P_LOGTIME", objattandance.LOGTIME);
                        objParams[3] = new SqlParameter("@P_USERID", objattandance.USERID);
                        objParams[4] = new SqlParameter("@P_NODEID", objattandance.NODEID);
                        objParams[5] = new SqlParameter("@P_AUTHTYPE", objattandance.AUTHTYPE);
                        objParams[6] = new SqlParameter("@P_AUTHRESULT", objattandance.AUTHRESULT);
                        objParams[7] = new SqlParameter("@P_OPENRESULT", objattandance.OPENRESULT);
                        objParams[8] = new SqlParameter("@P_FUNCTIONNO", objattandance.FUNCTIONNO);
                        objParams[9] = new SqlParameter("@P_SLOGTIME", objattandance.SLOGTIME);
                        objParams[10] = new SqlParameter("@P_CHECKED", objattandance.CHECKED);
                        objParams[11] = new SqlParameter("@P_RESERVE", objattandance.RESERVE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCE_INS", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeleteAttandance(DateTime Date)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DATE", Date);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCE_DEL", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int SaveUploadEntry(DateTime attend, int user, string filename, int update)
                {
                    int retstatus = 0;
                    int trno = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_TRNO", trno);
                        objParams[1] = new SqlParameter("@P_FILENAME", filename);
                        objParams[2] = new SqlParameter("@P_ATTEND", attend);
                        objParams[3] = new SqlParameter("@P_USERID", user);
                        objParams[4] = new SqlParameter("@P_AUDIT", System.DateTime.Now);
                        objParams[5] = new SqlParameter("@P_UPDATE", update);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCE_UPLOAD_LOG_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                // To Fetch all Attandance  
                public DataSet GetAllLeave()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMP_ATTANDANCE_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                //To Maintain the Upload Record of File And User
                public int AddUPDDetails(string filename, DateTime Date, int userno, int trno)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FILENAME", filename);
                        objParams[1] = new SqlParameter("@P_UPDDate", Date);
                        objParams[2] = new SqlParameter("@P_USERNO", userno);
                        objParams[3] = new SqlParameter("@P_TRNO", trno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCE_UPDDETAILS_ADD", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion // End Attandance Region

                #region AttendenceConfiguration
                public DataSet GetShiftDetails(int shiftNo)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SHIFTNO", shiftNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_SHIFTMAS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetShiftDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                /*public int ShiftInsUpdate(Shifts objShift, int shiftNo, int insUpd)
                {
                    int retstatus = 0;
                    try
                    {
                        int i = 0;
                        for (i = 0; i < 7; i++)
                        {
                            DAC dc = new DAC();
                            dc.StoredProcedure = "PKG_PAY_LEAVE_SHIFTMAS_INS_UPD";
                            dc.Params.Add("@P_SHIFTNO", SqlDbType.Int, shiftNo);
                            dc.Params.Add("@P_SHIFTNAME", SqlDbType.NVarChar, objShift.SHIFTNAME);
                            dc.Params.Add("@P_STATUS", SqlDbType.Int, objShift.STATUS[i]);
                            dc.Params.Add("@P_DAYNO", SqlDbType.Int, i + 1);
                            dc.Params.Add("@P_INTIME", SqlDbType.NVarChar, objShift.INTIME[i]);
                            dc.Params.Add("@P_OUTTIME", SqlDbType.NVarChar, objShift.OUTTIME[i]);
                            dc.Params.Add("@P_INS", SqlDbType.Int, insUpd);
                            DAC.Commands.Add(dc);
                        }
                        DAC.ExecuteBatch();
                        DAC.Commands.Clear();
                        retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.ShiftInsUpdate-> " + ex.ToString());
                    }
                    return retstatus;
                }*/
                public int ShiftInsUpdate(Shifts objShift, DataTable dtShiftRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SHIFTNO", objShift.SHIFTNO);
                        objParams[1] = new SqlParameter("@P_ESTB_SHIFT_RECORD", dtShiftRecord);
                        objParams[2] = new SqlParameter("@P_UA_NO", objShift.UA_NO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objShift.COLLEGE_NO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHIFTMAS_INS_UPD", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Emp_Attendance_Controller.ShiftInsUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdateShiftRef()
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_UPD_SHIFT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.UpdateShiftRef-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLeaveRef(string EarlyGoing, string LateComing, String EarlyLate, string leaveType, int ODdays, int ODAppdays, int lwp, int compl, int medical)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_EARLGO", EarlyGoing);
                        objParams[1] = new SqlParameter("@P_LATECOME", LateComing);
                        objParams[2] = new SqlParameter("@P_EARLYLATE", EarlyLate);
                        objParams[3] = new SqlParameter("@P_LEAVETYPE", leaveType);
                        objParams[4] = new SqlParameter("@P_OD_DAYS", ODdays);
                        objParams[5] = new SqlParameter("@P_OD_APP_DAYS", ODAppdays);//18-feb-2015
                        objParams[6] = new SqlParameter("@P_LWP_NO", lwp);//15-JUN-2015
                        objParams[7] = new SqlParameter("@P_COMPL_NO", compl);//15-JUN-2015
                        objParams[8] = new SqlParameter("@P_MEDICAL_NO", medical);//15-JUN-2015

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_UPD_REF", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.UpdateLeaveRef-> " + ex.ToString());
                    }
                    return retstatus;
                }
                /// <summary>
                /// to update PAYROLL_CONFIG_TIMING_ENTRY table
                /// </summary>
                /// <param name="Fromtime"></param>
                /// <param name="ToTime"></param>
                /// <param name="statusno"></param>
                /// <returns></returns>
                public int UpdateConfigTimingEntry(string Fromtime, string ToTime, double Noofleave, int statusno, double allowed_up_to)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FROMTIME", Fromtime);
                        objParams[1] = new SqlParameter("@P_TOTIME", ToTime);
                        objParams[2] = new SqlParameter("@P_NOLEAVE", Noofleave);
                        objParams[3] = new SqlParameter("@P_STATUSNO", statusno);
                        objParams[4] = new SqlParameter("@P_ALLOWED_UP_TO", allowed_up_to);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_CONFIG_TIME_ENTRY", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.UpdateConfigTimingEntry-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetCurrentConfig()
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_REF", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetCurrentConfig-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetShiftList()
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_SHIFTLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetShiftList-> " + ex.ToString());
                    }
                    return ds;
                }
                //To Delete existing Shift Type
                //Added by Saket Singh on 16-Dec-2016
                public int DeleteShifttype(int SHIFTNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SHIFTNO", SHIFTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHIFTMAS_INS_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.DeleteShifttype->" + ex.ToString());
                    }
                    return retstatus;
                }
                /// <summary>
                /// to retrive status info on config page.
                /// </summary>
                /// <returns></returns>
                public DataSet GetStatus(int stno, int college_no)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STNO", stno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_CONFIG_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetStatus-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion


                public DataSet GetFileDetails(int mno, int yrno)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MONTH", mno);
                        objParams[1] = new SqlParameter("@P_YEAR", yrno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_FILEDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetFileDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                #region ServiceBookMenu
                public int insertMenuData(int UserTypeId, DataTable Menutbl)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_UserTypeId", UserTypeId);
                        objParams[1] = new SqlParameter("@MENUTbl", Menutbl);


                        if (objSQLHelper.ExecuteNonQuerySP("Usp_Pay_Ins_menu_Detail", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);

                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet BindMenulist(int usertypeid)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UserTypeId", usertypeid);

                        ds = objSQLHelper.ExecuteDataSetSP("BindMenuList", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetShiftList-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region ModifyShift
                public DataSet GetShiftDetails(int shiftNo, int college_no)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SHIFTNO", shiftNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_SHIFTMAS_MODIFY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetShiftDetails-> " + ex.ToString());
                    }
                    return ds;
                }         
                #endregion

                #region AttendanceProcessconfiguration
                public int AddAttandanceProcess(EMP_ATTANDANCE objattandance)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLNO", objattandance.COLLEGE_NO);
                        objParams[1] = new SqlParameter("@P_STNO", objattandance.STNO);
                        objParams[2] = new SqlParameter("@P_PROCESSFROM", objattandance.PROCESS_FROM);
                        objParams[3] = new SqlParameter("@P_PROCESSTO", objattandance.PROCESS_TO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCEPROCESS_INS", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.AddAttandanceProcess-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllAttendanceProcess()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMP_ATTENDANCEPROCESS_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetAllAttendanceProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public DataSet GetSingleAttendanceProcess(int ConfigNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ConfigNo", ConfigNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMP_ATTENDANCEPROCESS_GETSINGLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetSingleAttendanceProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public int UpdAttandanceProcess(EMP_ATTANDANCE objattandance, int ConfigNo)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLNO", objattandance.COLLEGE_NO);
                        objParams[1] = new SqlParameter("@P_STNO", objattandance.STNO);
                        objParams[2] = new SqlParameter("@P_PROCESSFROM", objattandance.PROCESS_FROM);
                        objParams[3] = new SqlParameter("@P_PROCESSTO", objattandance.PROCESS_TO);
                        objParams[4] = new SqlParameter("@P_ConfigNo", ConfigNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_EMP_ATTANDANCEPROCESS_UPD", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.UpdAttandanceProcess-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion




            }
        }
    }
}
