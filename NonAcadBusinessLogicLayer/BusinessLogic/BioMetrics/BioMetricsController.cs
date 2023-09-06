
//======================================================================================
// PROJECT NAME  : NITPRM                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[LEAVE CONTROLLER]                                  
// CREATION DATE : 07-OCT-2009                                                        
// CREATED BY    : JAYANT DHOMNE                                   
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
            public class BioMetricsController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Holiday
                // To insert New Holiday
                public int AddHoliday(BioMetrics objBioMetrics)
                {
                    int retstatus = 0;
                    //
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_HOLIDAYDATE", objBioMetrics.HOLIDAYDATE);
                        objParams[1] = new SqlParameter("@P_HOLIDAYNAME", objBioMetrics.HOLIDAYNAME);
                        objParams[2] = new SqlParameter("@P_NUMBEROFDAYS ", objBioMetrics.HOLIDAYDAYS);
                        if (objSQLHelper.ExecuteNonQuerySP("HOLIDAY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BioMetricsController.AddHoliday-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To update Existing Holiday Entry
                public int UpdateHoliday(BioMetrics objBioMetrics)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_HOLIDAYDATE", objBioMetrics.HOLIDAYDATE);
                        objParams[1] = new SqlParameter("@P_HOLIDAYNAME", objBioMetrics.HOLIDAYNAME);
                        objParams[2] = new SqlParameter("@P_NUMBEROFDAYS ", objBioMetrics.HOLIDAYDAYS);
                        objParams[3] = new SqlParameter("@P_HOLIDAYNO", objBioMetrics.HOLIDAYNO);
                        if (objSQLHelper.ExecuteNonQuerySP("HOLIDAY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Delete Holiday 
                public int DeleteHoliday(int HOLIDAYNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HOLIDAYNO", HOLIDAYNO);
                        if (objSQLHelper.ExecuteNonQuerySP("HOLIDAY_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Holidays
                public DataSet GetAllHolidays()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("HOLIDAY_GEL_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Leave Types detail by passing Leave No
                public DataSet GetSingleHoliday(int HOLIDAYNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HOLIDAYNO", HOLIDAYNO);
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_HOLIDAY_GET_BY_HOLNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Holiday Master Region

                #region Tour
                // To insert New Holiday
                public int AddTour(BioMetrics objBioMetrics)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_USERID", objBioMetrics.USERID);
                        objParams[1] = new SqlParameter("@P_EMPNAME", objBioMetrics.EMPNAME);
                        objParams[2] = new SqlParameter("@P_TOURFROMDT", objBioMetrics.TOURFROMDT);
                        objParams[3] = new SqlParameter("@P_TOURTODT", objBioMetrics.TOURTODT);
                        objParams[4] = new SqlParameter("@P_CLIENT", objBioMetrics.CLIENT);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TOUR_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BioMetricsController.AddHoliday-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To update Existing Tour Entry
                public int UpdateTour(BioMetrics objBioMetrics)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_TOURNO", objBioMetrics.TOURNO);
                        objParams[1] = new SqlParameter("@P_USERID", objBioMetrics.USERID);
                        objParams[2] = new SqlParameter("@P_EMPNAME", objBioMetrics.EMPNAME);
                        objParams[3] = new SqlParameter("@P_TOURFROMDT", objBioMetrics.TOURFROMDT);
                        objParams[4] = new SqlParameter("@P_TOURTODT", objBioMetrics.TOURTODT);
                        objParams[5] = new SqlParameter("@P_CLIENT", objBioMetrics.CLIENT);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TOUR_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }




                // To Fetch all Holidays
                public DataSet GetAllTours()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TOUR_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Leave Types detail by passing Leave No
                public DataSet GetSingleTour(int TOURNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TOURNO", TOURNO);
                        ds = objSQLHelper.ExecuteDataSetSP("HOLIDAY_GET_BY_HOLNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Holiday Master Region

                #region Average_Hour_work_report

                //public DataSet GetLoginDetails(DateTime fromDate, DateTime toDate, int idno,int deptno,int college_no)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                //        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                //        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                //        objParams[3] = new SqlParameter("@P_DEPTNO ", deptno);
                //        objParams[4] = new SqlParameter("@P_COLLEGE_NO ", college_no);
                //        ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }

                //    return ds;
                //}
                public DataSet GetLoginDetails(DateTime fromDate, DateTime toDate, int idno, int deptno, string time_from, string time_to, string inout, int stno, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO ", deptno);

                        //objParams[4] = new SqlParameter("@P_INTIME_FROM ", intime_from);
                        //objParams[5] = new SqlParameter("@P_INTIME_TO ", intime_to);

                        if (!time_from.Equals(string.Empty))
                            objParams[4] = new SqlParameter("@P_TIME_FROM", time_from);
                        else
                            objParams[4] = new SqlParameter("@P_TIME_FROM", DBNull.Value);

                        if (!time_to.Equals(string.Empty))
                            objParams[5] = new SqlParameter("@P_TIME_TO", time_to);
                        else
                            objParams[5] = new SqlParameter("@P_TIME_TO", DBNull.Value);

                        //@P_INOUT
                        objParams[6] = new SqlParameter("@P_INOUT ", inout);//IN/OUT/N
                        //@P_STNO
                        objParams[7] = new SqlParameter("@P_STNO ", stno);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO ", collegeid);

                        ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public DataSet GetLoginDetailsForExcel(DateTime fromDate, DateTime toDate, int idno, int deptno, string time_from, string time_to, string inout, int stno, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO ", deptno);

                        //objParams[4] = new SqlParameter("@P_INTIME_FROM ", intime_from);
                        //objParams[5] = new SqlParameter("@P_INTIME_TO ", intime_to);

                        if (!time_from.Equals(string.Empty))
                            objParams[4] = new SqlParameter("@P_TIME_FROM", time_from);
                        else
                            objParams[4] = new SqlParameter("@P_TIME_FROM", DBNull.Value);

                        if (!time_to.Equals(string.Empty))
                            objParams[5] = new SqlParameter("@P_TIME_TO", time_to);
                        else
                            objParams[5] = new SqlParameter("@P_TIME_TO", DBNull.Value);

                        //@P_INOUT
                        objParams[6] = new SqlParameter("@P_INOUT ", inout);//IN/OUT/N
                        //@P_STNO
                        objParams[7] = new SqlParameter("@P_STNO ", stno);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO ", collegeid);

                        ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                
                
                public DataSet GetSingleTourBYID(string userid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERID", userid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TOUR_DATE_GET_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetHolidayBetDates(DateTime fromDate, DateTime toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate.ToString());
                        objParams[1] = new SqlParameter("@P_TODATE", toDate.ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("HOLIDAY_CAL_HOLIDAYS_BET_DATES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion


                #region Daily_Report
                public DataSet GetLoginInfoByDate(DateTime entDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ENTDATE", entDate);
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_DAILY_LOGIN_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                public DataSet GetLeaveByID(string userid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_USERID", userid);
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_CAL_LEAVE_BY_ID", objParam);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
          

                //public DataSet GetEmployee(int deptno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                //        //ds = objSQLHelper.ExecuteDataSet("EMP_USERINFO_USERNAME");
                //        ds = objSQLHelper.ExecuteDataSetSP("EMP_USERINFO_USERNAME", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}
                public DataSet GetEmployee(int deptno, int stno, int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", collegeno);
                        //ds = objSQLHelper.ExecuteDataSet("EMP_USERINFO_USERNAME");
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_USERINFO_USERNAME", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #region shiftmanagement
                public DataSet GetLoginDetailsnew(DateTime fromDate, DateTime toDate, int idno, int deptno, int college_no, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO ", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO ", college_no);
                        objParams[5] = new SqlParameter("@P_STNO ", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET_SHIFT_INT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public DataSet GetLoginDetails_ShiftMgt(DateTime fromDate, DateTime toDate, int idno, int deptno, int college_no, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO ", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO ", college_no);
                        objParams[5] = new SqlParameter("@P_STNO ", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMP_ATTENDANCESHEET_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                #endregion

                #region Absent Report

                public DataSet GetEmployeeUser(int deptno, int stno, int collegeno, int type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", collegeno);
                        objParams[3] = new SqlParameter("@P_EmpType", type);
                        //ds = objSQLHelper.ExecuteDataSet("EMP_USERINFO_USERNAME");
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_USERINFO_USERNAME_TYPEUSER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion


                #region ManuallogBiometric
                public DataSet GetManuallogBiometric(DateTime fromDate, DateTime toDate, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO ", idno);
                        //ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET_MANUAL_BIOMETRICSHOW", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_ATTENDANCESHEET_MANUAL_BIOMETRICSHOW_UPDATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                public int LoginDetailsUpdation(DataTable dtAppRecord, string Userid)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_ESTB_LOGIN_RECORD", dtAppRecord);
                        objParams[1] = new SqlParameter("@P_Userid", Userid);
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ATTENDANCESHEET_MANUAL_BIOMETRIC_INSERTION", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }
                #endregion

            }
        }
    }
}
